using Domain;

namespace TestTask.Application.Common.DownloadableFiles;

public interface IDownloadableFilesRepository
{
    void AddDownloadableOwnerId(Guid id, Guid downloadable);

    bool RemoveDownloadableOwnerId(Guid fileId);

    Guid? GetDownloadableOwnerId(Guid fileId);
}