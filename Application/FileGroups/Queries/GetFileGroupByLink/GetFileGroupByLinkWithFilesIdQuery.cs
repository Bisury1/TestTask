using MediatR;
using TestTask.Application.CommonFileBl.Queries.CommonGetFilesByHashLink;

namespace TestTask.Application.FileGroups.Queries.GetFileGroupByLink;

public class GetFileGroupByLinkWithFilesIdQuery: IRequest<FilePathsLookup>
{
    public Guid LinkId { get; set; } 
}