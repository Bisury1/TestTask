using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Common.CustomException;

namespace TestTask.Application.FileEntityBL.Command.CreateDisposableFileLink
{
    public class CreateDisposableFileLinkCommandHandler 
        : IRequestHandler<CreateDisposableFileLinkCommand, Guid>
    {
        private readonly ILinkDbContext _linkDbContext;
        private readonly IFileEntityDbContext _fileEntityDbContext;
        private readonly ISaveFileChanger _saveFileChanger;

        public CreateDisposableFileLinkCommandHandler(ILinkDbContext linkDbContext,
            ISaveFileChanger saveFileChanger, IFileEntityDbContext fileEntityDbContext)
        {
            _linkDbContext = linkDbContext;
            _saveFileChanger = saveFileChanger;
            _fileEntityDbContext = fileEntityDbContext;
        }
        
        
        public async Task<Guid> Handle(CreateDisposableFileLinkCommand request, CancellationToken cancellationToken)
        {
            var file = await _fileEntityDbContext.FileEntities
                .Where(file => file.Id == request.FileId && file.OwnerId == request.UserId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (file == null)
            {
                //всё так упрощённо для экономии времени, потому что задача реально большая, если писать в продакшен,
                //то нужно пояснительные сообщение для всех ошибок в проекте и желательно сделать класс ThrowExceptionHelper,
                //который будет выбрасывать ошибки подставляя нужные аргументы в сообщения ошибок. Плюс сделать мидлвари для
                //логирования ошибок, а также ещё одна для проставления нужных http кодов
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
