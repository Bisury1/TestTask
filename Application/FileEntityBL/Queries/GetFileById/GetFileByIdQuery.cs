using MediatR;
using TestTask.Application.CommonFileBl.Queries.CommonGetFilesByHashLink;

namespace TestTask.Application.FileEntityBL.Queries.GetFileById;

public class GetFileByIdQuery: IRequest<FilePath>
{
    public Guid Id { get; set; } 
    public Guid UserId { get; set; }
}