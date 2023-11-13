using TestTask.Application.Common.ProgressTrackerInfrastructure.ProgressEntities;

namespace TestTask.Application.Common.ProgressTrackerInfrastructure.ProgressTrackerRepository;

public interface IProgressTrackersRepository
{
    void AddProgressTracker(Guid progressTrackerId, IProgressTracker progressTracker);
    
    double? GetProgress(Guid id);

    IProgressTracker? GetProgressTracker(string alias);

    bool RemoveProgressTracker(Guid id);
    
    IProgressTracker? GetProgressTracker(Guid id);
    
    double? GetProgress(string alias);

    Guid? GetIdByAlias(string alias);
    
    bool RemoveProgressTracker(string alias);

    void AddProgressTrackerAlias(string alias, Guid progressTrackerId);
}