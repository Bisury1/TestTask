using MediatR;
using Microsoft.AspNetCore.Http;

namespace TestTask.Application.CommonFileBl.Command.CommonCreateFiles
{
    public class CommonCreateFileCommand: IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public IFormFileCollection FileCollection { get; set; } = null!;
    }
}
