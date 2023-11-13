using Domain;

namespace TestTask.Application.Common.DownloadableFiles;

public class DownloadableFilesRepository : IDownloadableFilesRepository
{
    //по-хорошему это должно быть in-memory хранилище
    private readonly Dictionary<Guid, Guid> _downloadableOwnerIds = new();
    
    public void AddDownloadableOwnerId(Guid id, Guid downloadable)
    {
        _downloadableOwnerIds.Add(id, downloadable);
    }
    
    public bool RemoveDownloadableOwnerId(Guid fileId) => _downloadableOwnerIds.Remove(fileId);

    public Guid? GetDownloadableOwnerId(Guid fileId)
    {
        _downloadableOwnerIds.TryGetValue(fileId, out var value);
        return value;
    }
}