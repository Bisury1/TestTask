using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestTask.Application.Common.CustomException;
using TestTask.Application.CommonFileBl.Queries.CommonGetFilesByHashLink;

namespace TestTask.Application.FileEntityBL.Queries.GetFileById
{
    public class GetFileByIdQueryHandler: IRequestHandler<GetFileByIdQuery, FilePath>
    {
        private readonly IFileEntityDbContext _fileEntityDbContext;
        private readonly IMapper _mapper;

        public GetFileByIdQueryHandler(IFileEntityDbContext fileEntityDbContext, IMapper mapper)
        {
            _fileEntityDbContext = fileEntityDbContext;
            _mapper = mapper;
        }


        public async Task<FilePath> Handle(GetFileByIdQuery request, CancellationToken cancellationToken)
        {
            var filePath = await _fileEntityDbContext.FileEntities
                .Where(file => file.Id == request.Id && file.OwnerId == request.UserId)
                .ProjectTo<FilePath>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            
            return filePath ?? throw new NotEntityException();
        }
    }
}
