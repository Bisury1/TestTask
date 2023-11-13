namespace Domain;

public class FileGroup
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public IList<FileEntity> Files { get; set; } = null!;
}