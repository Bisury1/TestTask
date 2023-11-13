using MediatR;

namespace TestTask.Application.FileGroups.Queries.GetLoadedFileGroupList
{
    public class GetFileGroupListQuery: IRequest<FileGroupListVm>
    {
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
    }
}
