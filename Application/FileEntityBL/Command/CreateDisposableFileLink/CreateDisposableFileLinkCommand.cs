using MediatR;

namespace TestTask.Application.FileEntityBL.Command.CreateDisposableFileLink
{
    public class CreateDisposableFileLinkCommand: IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid FileId { get; set; }
    }
}
