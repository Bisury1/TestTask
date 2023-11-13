using TestTask.Application.Common.ProgressTrackerInfrastructure.ProgressEntities;

namespace TestTask.Application.Common.ProgressTrackerInfrastructure.ProgressTrackerFactory;

public interface IProgressTrackerFactory
{
    public void RemoveProgressTracker(string name);
    void RemoveProgressTracker(Guid id);
    
    IProgressTracker CreateFileProgressTracker(Guid fileId, string fileName, Stream stream, long size);

    IProgressTracker CreateFileGroupProgressTracker(Guid fileGroupId, List<IProgressTracker> progressTrackers);
}