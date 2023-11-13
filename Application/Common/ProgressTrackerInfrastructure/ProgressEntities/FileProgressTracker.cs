namespace TestTask.Application.Common.ProgressTrackerInfrastructure.ProgressEntities;

public class FileProgressTracker: IProgressTracker, IDisposable, IAsyncDisposable
{
    private Stream _fileStream;
    private long _maxSize;

    private bool _disposing;

    public FileProgressTracker(Stream fileStream, long maxSize)
    {
        _fileStream = fileStream;
        _maxSize = maxSize;
    }

    public double GetProgress() => _disposing ? Math.Round((double)_fileStream.Length / _maxSize * 100) : 100;

    public void Dispose()
    {
        _disposing = true;
        _fileStream.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        _disposing = true;
        await _fileStream.DisposeAsync();
    }
}