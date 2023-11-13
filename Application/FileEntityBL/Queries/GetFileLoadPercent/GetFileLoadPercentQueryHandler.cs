using System.Security.Authentication;
using MediatR;
using TestTask.Application.Common.DownloadableFiles;
using TestTask.Application.Common.ProgressTrackerInfrastructure.ProgressTrackerRepository;

namespace TestTask.Application.FileEntityBL.Queries.GetFileLoadPercent
{
    public class GetFileLoadPercentQueryHandler: IRequestHandler<GetFileLoadPercentQuery, double>
    {
        private readonly IDownloadableFilesRepository _downloadableFilesRepository;
        private readonly IProgressTrackersRepository _progressTrackersRepository;

        public GetFileLoadPercentQueryHandler(IDownloadableFilesRepository downloadableFilesRepository,
            IProgressTrackersRepository progressTrackersRepository)
        {
            _downloadableFilesRepository = downloadableFilesRepository;
            _progressTrackersRepository = progressTrackersRepository;
        }

        public async Task<double> Handle(GetFileLoadPercentQuery request, CancellationToken cancellationToken)
        {
            var fileId = _progressTrackersRepository.GetIdByAlias(request.FileName);
            if (!fileId.HasValue)
                throw new ArgumentNullException();
            
            if (_downloadableFilesRepository.GetDownloadableOwnerId(fileId.Value)! != request.UserId)
            {
                throw new AuthenticationException();
            }

            return (double)_progressTrackersRepository.GetProgress(fileId.Value)!;
        }
    }
}
