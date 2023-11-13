using System.Security.Authentication;
using MediatR;
using TestTask.Application.Common.DownloadableFiles;
using TestTask.Application.Common.ProgressTrackerInfrastructure.ProgressTrackerRepository;
using TestTask.Application.FileEntityBL.Queries.GetFileLoadPercent;

namespace TestTask.Application.FileGroups.Queries.GetFileGroupLoadPercent
{
    public class GetFileLoadPercentQueryHandler: IRequestHandler<GetFileGroupLoadPercentQuery, double>
    {
        private readonly IDownloadableFilesRepository _downloadableFilesRepository;
        private readonly IProgressTrackersRepository _progressTrackersRepository;

        public GetFileLoadPercentQueryHandler(IDownloadableFilesRepository downloadableFilesRepository,
            IProgressTrackersRepository progressTrackersRepository)
        {
            _downloadableFilesRepository = downloadableFilesRepository;
            _progressTrackersRepository = progressTrackersRepository;
        }

        public async Task<double> Handle(GetFileGroupLoadPercentQuery request, CancellationToken cancellationToken)
        {
            var fileProgress = _progressTrackersRepository.GetProgress(request.GroupId);
            if (fileProgress == null)
                throw new ArgumentNullException();
            
            if (_downloadableFilesRepository.GetDownloadableOwnerId(request.GroupId)!.Value != request.UserId)
            {
                throw new AuthenticationException();
            }

            return fileProgress.Value;
        }
    }
}
