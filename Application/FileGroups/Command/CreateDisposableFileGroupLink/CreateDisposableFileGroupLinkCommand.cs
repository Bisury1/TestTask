using MediatR;

namespace TestTask.Application.FileGroups.Command.CreateDisposableFileGroupLink
{
    public class CreateDisposableFileGroupLinkCommand: IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid FileId { get; set; }
    }
}
