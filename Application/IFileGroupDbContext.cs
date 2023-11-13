using Domain;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Application
{
    public interface IFileGroupDbContext
    {
        public DbSet<FileGroup> FileGroups { get; set; }
    }
}
