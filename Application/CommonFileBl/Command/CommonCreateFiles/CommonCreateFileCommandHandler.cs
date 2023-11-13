using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using TestTask.Application.Common.DownloadableFiles;
using TestTask.Application.Common.ProgressTrackerInfrastructure.ProgressEntities;
using TestTask.Application.Common.ProgressTrackerInfrastructure.ProgressTrackerFactory;

namespace TestTask.Application.CommonFileBl.Command.CommonCreateFiles
{
    public class CommonCreateFileCommandHandler 
        : IRequestHandler<CommonCreateFileCommand, Guid>
    {
        private readonly IFileGroupDbContext _fileGroupDbContext;
        private readonly IFileEntityDbContext _fileEntityDbContext;
        private readonly IProgressTrackerFactory _progressTrackerFactory;
        private readonly IDownloadableFilesRepository _downloadableFilesRepository;
        private readonly ISaveFileChanger _saveFileChanger;

        public CommonCreateFileCommandHandler(IFileGroupDbContext fileGroupDbContext, ISaveFileChanger saveFileChanger,
            IProgressTrackerFactory progressTrackerFactory, IDownloadableFilesRepository downloadableFilesRepository, IFileEntityDbContext fileEntityDbContext)
        {
            _fileGroupDbContext = fileGroupDbContext;
            _saveFileChanger = saveFileChanger;
            _progressTrackerFactory = progressTrackerFactory;
            _downloadableFilesRepository = downloadableFilesRepository;
            _fileEntityDbContext = fileEntityDbContext;
        }

        private async Task StartDownloadFileGroup(FileGroup group, Guid userId, IFormFileCollection files, CancellationToken token)
        {
            var loadedFiles = new List<FileEntity>();
            var filesTrackers = new List<IProgressTracker>();
            var copyWithContinuationTasks = new List<Task>();

            if (files == null)
            {
                throw new ArgumentNullException();
            }
            foreach (var file in files)
            {
                var fileName = file.FileName;
                var dirPath = Path.Combine(Environment.CurrentDirectory, userId.ToString());
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);
                var filePath = Path.Combine(dirPath, fileName);
                var fileId = Guid.NewGuid();
                var fileEntity = new FileEntity()
                {
                    Id = fileId,
                    Name = fileName,
                    Path = filePath,
                    Length = file.Length,
                    OwnerId = userId,
                    GroupId = group.Id
                };
                
                _downloadableFilesRepository.AddDownloadableOwnerId(fileEntity.Id, fileEntity.OwnerId);
                
                var fileStream = File.OpenWrite(filePath);
                var copyTask = file.CopyToAsync(fileStream, token);
                var tracker = _progressTrackerFactory.CreateFileProgressTracker(fileEntity.Id,
                    fileName, fileStream, file.Length);
                filesTrackers.Add(tracker);
                
                copyWithContinuationTasks.Add(copyTask.ContinueWith(_ =>
                {
                    _progressTrackerFactory.RemoveProgressTracker(fileName); 
                    _downloadableFilesRepository.RemoveDownloadableOwnerId(fileId);
                    _fileEntityDbContext.FileEntities.Add(fileEntity);
                }, token));
            }
            
            _progressTrackerFactory.CreateFileGroupProgressTracker(group.Id, filesTrackers);
            _downloadableFilesRepository.AddDownloadableOwnerId(group.Id, group.OwnerId);
            group.Files = loadedFiles;
            
            await Task.WhenAll(copyWithContinuationTasks).ContinueWith(async task =>
            {
                if (!task.IsCompletedSuccessfully) return;
                
                _progressTrackerFactory.RemoveProgressTracker(group.Id);
                _downloadableFilesRepository.RemoveDownloadableOwnerId(group.Id);
                await _fileGroupDbContext.FileGroups.AddAsync(group, token);
                await _saveFileChanger.SaveChangesAsync(token);
            }, token);
        }
        
        public async Task<Guid> Handle(CommonCreateFileCommand request, CancellationToken cancellationToken)
        {
            var userId = request.UserId;
            
            var fileGroup = new FileGroup()
            {
                Id = Guid.NewGuid(),
                OwnerId = userId
            };
            
            await StartDownloadFileGroup(fileGroup, userId, request.FileCollection, cancellationToken);
            return fileGroup.Id;
        }
    }
}
