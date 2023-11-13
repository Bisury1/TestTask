namespace TestTask.Application.Common.ProgressTrackerInfrastructure.ProgressEntities;

public class FileGroupProgressTracker: IProgressTracker
{
    private List<IProgressTracker> _progressTrackers;

    public FileGroupProgressTracker(List<IProgressTracker> progressTrackers)
    {
        _progressTrackers = progressTrackers;
    }

    public double GetProgress() => _progressTrackers.Sum(tracker => tracker.GetProgress()) / _progressTrackers.Count;
}