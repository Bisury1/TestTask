using TestTask.Application.Common.ProgressTrackerInfrastructure.ProgressEntities;
using TestTask.Application.Common.ProgressTrackerInfrastructure.ProgressTrackerRepository;

namespace TestTask.Application.Common.ProgressTrackerInfrastructure.ProgressTrackerFactory;

public class FileProgressTrackerFactory: IProgressTrackerFactory
{
    private readonly IProgressTrackersRepository _trackersRepository;
    
    public FileProgressTrackerFactory(IProgressTrackersRepository trackersRepository)
    {
        _trackersRepository = trackersRepository;
    }
    
    public void RemoveProgressTracker(Guid id)
    {
        var progressTracker = _trackersRepository.GetProgressTracker(id);
        if (progressTracker is IDisposable disposable)
        {
            disposable.Dispose();
        }
        
        _trackersRepository.RemoveProgressTracker(id);
    }
    
    public void RemoveProgressTracker(string name)
    {
        var progressTracker = _trackersRepository.GetProgressTracker(name);
        if (progressTracker is IDisposable disposable)
        {
            disposable.Dispose();
        }
        
        _trackersRepository.RemoveProgressTracker(name);
    }
    
    

    public IProgressTracker CreateFileProgressTracker(Guid fileId, string fileName, Stream stream, long size)
    {
        var fileProgressTracker = new FileProgressTracker(stream, size);
        _trackersRepository.AddProgressTracker(fileId, fileProgressTracker);
        _trackersRepository.AddProgressTrackerAlias(fileName, fileId);
        return fileProgressTracker;
    }

    public IProgressTracker CreateFileGroupProgressTracker(Guid fileGroupId, List<IProgressTracker> progressTrackers)
    {
        var fileGroupProgressTracker = new FileGroupProgressTracker(progressTrackers);
        _trackersRepository.AddProgressTracker(fileGroupId, fileGroupProgressTracker);
        return fileGroupProgressTracker;
    }
}