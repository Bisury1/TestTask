using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Common.CustomException;

namespace TestTask.Application.FileGroups.Command.CreateDisposableFileGroupLink
{
    public class CreateDisposableFileGroupLinkCommandHandler 
        : IRequestHandler<CreateDisposableFileGroupLinkCommand, Guid>
    {
        private readonly ILinkDbContext _linkDbContext;
        private readonly IFileEntityDbContext _fileEntityDbContext;
        private readonly ISaveFileChanger _saveFileChanger;

        public CreateDisposableFileGroupLinkCommandHandler(ILinkDbContext linkDbContext,
            ISaveFileChanger saveFileChanger, IFileEntityDbContext fileEntityDbContext)
        {
            _linkDbContext = linkDbContext;
            _saveFileChanger = saveFileChanger;
            _fileEntityDbContext = fileEntityDbContext;
        }
        
        
        public async Task<Guid> Handle(CreateDisposableFileGroupLinkCommand request, CancellationToken cancellationToken)
        {
            var file = await _fileEntityDbContext.FileEntities
                .Where(file => file.Id == request.FileId && file.OwnerId == request.UserId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (file == null)
            {
                //копипаста...
                throw new NotEntityException();
            }

            var hashLinkGuid = Guid.NewGuid(); 
            var hashLink = new LinkWithFilesId()
            {
                Id = hashLinkGuid,
                FileOrFileGroupId = request.FileId
            };

            await _linkDbContext.LinksWithFileId.AddAsync(hashLink, cancellationToken);
            await _saveFileChanger.SaveChangesAsync(cancellationToken);

            return hashLinkGuid;
        }
    }
}
