namespace TestTask.Application;

public interface ISaveFileChanger
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    int SaveChanges();
}