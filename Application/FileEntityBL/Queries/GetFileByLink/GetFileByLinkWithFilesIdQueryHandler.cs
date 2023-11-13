using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Common.CustomException;
using TestTask.Application.CommonFileBl.Queries.CommonGetFilesByHashLink;

namespace TestTask.Application.FileEntityBL.Queries.GetFileByLink
{
    public class GetFileByLinkWithFilesIdQueryHandler: IRequestHandler<GetFileByLinkWithFilesIdQuery, FilePath>
    {
        private readonly IFileEntityDbContext _fileEntityDbContext;
        private readonly ILinkDbContext _linkDbContext;
        private readonly ISaveFileChanger _saveFileChanger;
        private readonly IMapper _mapper;

        public GetFileByLinkWithFilesIdQueryHandler(ISaveFileChanger saveFileChanger, ILinkDbContext linkDbContext,
            IFileEntityDbContext fileEntityDbContext, IMapper mapper)
        {
            _saveFileChanger = saveFileChanger;
            _linkDbContext = linkDbContext;
            _fileEntityDbContext = fileEntityDbContext;
            _mapper = mapper;
        }


        public async Task<FilePath> Handle(GetFileByLinkWithFilesIdQuery request, CancellationToken cancellationToken)
        {
            var link = await _linkDbContext.LinksWithFileId
                .Where(link => link.Id == request.LinkId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (link == null)
            {
                throw new NotEntityException();
            }
            
            var filePath = await _fileEntityDbContext.FileEntities
                .Where(file => file.Id == link.FileOrFileGroupId)
                .ProjectTo<FilePath>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            
            _linkDbContext.LinksWithFileId.Remove(link);
            await _saveFileChanger.SaveChangesAsync(cancellationToken);
            return filePath ?? throw new NotEntityException();
        }
    }
}
