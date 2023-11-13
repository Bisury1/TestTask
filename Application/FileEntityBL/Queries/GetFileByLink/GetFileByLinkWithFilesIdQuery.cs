using MediatR;
using TestTask.Application.CommonFileBl.Queries.CommonGetFilesByHashLink;

namespace TestTask.Application.FileEntityBL.Queries.GetFileByLink;

public class GetFileByLinkWithFilesIdQuery: IRequest<FilePath>
{
    public Guid LinkId { get; set; } 
}