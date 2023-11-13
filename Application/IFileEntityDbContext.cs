using Microsoft.EntityFrameworkCore;

namespace TestTask.Application;

public interface IFileEntityDbContext
{
    public DbSet<Domain.FileEntity> FileEntities { get; set; }
}