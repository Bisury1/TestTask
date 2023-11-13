using MediatR;

namespace TestTask.Application.CommonFileBl.Queries.CommonGetFilesByHashLink;

public class CommonGetFilesByHashLinkQueries: IRequest<FilePathsLookup>
{
    public Guid HashLinkId { get; set; } 
}