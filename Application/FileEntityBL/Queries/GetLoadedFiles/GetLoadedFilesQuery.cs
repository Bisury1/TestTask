using MediatR;

namespace TestTask.Application.FileEntityBL.Queries.GetLoadedFiles;

public class GetLoadedFilesQuery: IRequest<FileEntitiesLookup>
{
    public Guid UserId { get; set; }
}