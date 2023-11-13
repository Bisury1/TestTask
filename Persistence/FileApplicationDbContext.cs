using Domain;
using Microsoft.EntityFrameworkCore;
using TestTask.Application;
using TestTask.Persistence.EntityFileConfiguration;

namespace TestTask.Persistence
{
    public class FileApplicationDbContext : DbContext, IFileGroupDbContext, IFileEntityDbContext, ILinkDbContext, ISaveFileChanger
    {
        public DbSet<FileGroup> FileGroups { get; set; }
        public DbSet<FileEntity> FileEntities { get; set; }
        public DbSet<LinkWithFilesId> LinksWithFileId { get; set; }
        
        public FileApplicationDbContext(DbContextOptions<FileApplicationDbContext> dbContextOptions)
            : base(dbContextOptions) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FileConfiguration());
        }
    }
}
