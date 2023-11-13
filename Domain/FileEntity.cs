namespace Domain;

public class FileEntity
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public long Length { get; set; }
    public string Name { get; set; } = null!;
    public string Path { get; set; } = null!;
    public Guid? GroupId { get; set; }
    public FileGroup? Group { get; set; }
}