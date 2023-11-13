using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestTask.Persistence.EntityFileConfiguration
{
    internal class FileConfiguration : IEntityTypeConfiguration<FileEntity>
    {
        public void Configure(EntityTypeBuilder<FileEntity> builder)
        {
            builder.HasKey(file => file.Id);
            builder.Property(file => file.Path).HasMaxLength(300);
        }
    }
}
