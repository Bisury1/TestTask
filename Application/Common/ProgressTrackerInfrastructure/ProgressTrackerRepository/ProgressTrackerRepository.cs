using System.Collections.Concurrent;
using TestTask.Application.Common.ProgressTrackerInfrastructure.ProgressEntities;

namespace TestTask.Application.Common.ProgressTrackerInfrastructure.ProgressTrackerRepository;

public class ProgressTrackersRepository: IProgressTrackersRepository
{
    private readonly ConcurrentDictionary<Guid, IProgressTracker> _downloadableFiles = new();

    private readonly ConcurrentDictionary<string, Guid> _trackersIds = new();

    public void AddProgressTracker(Guid progressTrackerId, IProgressTracker progressTracker) 
        => _downloadableFiles.TryAdd(progressTrackerId, progressTracker);

    public Guid? GetIdByAlias(string alias) => _trackersIds.TryGetValue(alias, out var id) ? id : null;

    public void AddProgressTrackerAlias(string alias, Guid progressTrackerId)
        => _trackersIds.TryAdd(alias, progressTrackerId);

    public double? GetProgress(string alias)
    {
        Guid? id;
        return (id = GetIdByAlias(alias)) is not null ? GetProgress(id.Value) : null;
    }

    public IProgressTracker? GetProgressTracker(string alias)
    {
        Guid? id;
        return (id = GetIdByAlias(alias)) is not null ? GetProgressTracker(id.Value) : null;
    }

    public double? GetProgress(Guid id) => GetProgressTracker(id)?.GetProgress();

    public bool RemoveProgressTracker(Guid id) => _downloadableFiles.Remove(id, out _);

    public bool RemoveProgressTracker(string alias)
    {
        var id = GetIdByAlias(alias);
        if (!id.HasValue) return false;
        
        _trackersIds.Remove(alias, out _);
        return RemoveProgressTracker(id.Value);

    }
    
    public IProgressTracker? GetProgressTracker(Guid id) => _downloadableFiles.TryGetValue(id,
        out var value) ? value : null;
}