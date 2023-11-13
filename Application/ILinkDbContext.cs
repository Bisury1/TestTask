using Domain;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Application;

public interface ILinkDbContext
{
    public DbSet<LinkWithFilesId> LinksWithFileId { get; set; }
}