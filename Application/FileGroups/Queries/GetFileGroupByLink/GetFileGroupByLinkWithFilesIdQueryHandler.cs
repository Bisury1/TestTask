using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Common.CustomException;
using TestTask.Application.CommonFileBl.Queries.CommonGetFilesByHashLink;

namespace TestTask.Application.FileGroups.Queries.GetFileGroupByLink
{
    public class GetFileGroupByLinkWithFilesIdQueryHandler: IRequestHandler<GetFileGroupByLinkWithFilesIdQuery, FilePathsLookup>
    {
        private readonly IFileGroupDbContext _fileGroupDbContext;
        private readonly ILinkDbContext _linkDbContext;
        private readonly ISaveFileChanger _saveFileChanger;
        private readonly IMapper _mapper;

        public GetFileGroupByLinkWithFilesIdQueryHandler(ISaveFileChanger saveFileChanger, ILinkDbContext linkDbContext,
            IFileGroupDbContext fileGroupDbContext, IMapper mapper)
        {
            _saveFileChanger = saveFileChanger;
            _linkDbContext = linkDbContext;
            _fileGroupDbContext = fileGroupDbContext;
            _mapper = mapper;
        }


        public async Task<FilePathsLookup> Handle(GetFileGroupByLinkWithFilesIdQuery request, CancellationToken cancellationToken)
        {
            //извиняюсь за копипасту, просто уже очень долго делаю проект, пытаюсь ускориться таким образом
            var link = await _linkDbContext.LinksWithFileId
                .Where(link => link.Id == request.LinkId)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (link == null)
            {
                throw new NotEntityException();
            }
            
            _linkDbContext.LinksWithFileId.Remove(link);
            await _saveFileChanger.SaveChangesAsync(cancellationToken);
            var filePath = await _fileGroupDbContext.FileGroups
                .Where(file => file.Id == link.FileOrFileGroupId)
                .ProjectTo<FilePath>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);

            return new FilePathsLookup() { Paths = filePath };
        }
    }
}
