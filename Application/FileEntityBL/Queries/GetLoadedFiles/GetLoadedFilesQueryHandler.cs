using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Application.FileEntityBL.Queries.GetLoadedFiles
{
    public class GetFileLoadPercentQueryHandler: IRequestHandler<GetLoadedFilesQuery, FileEntitiesLookup>
    {
        private readonly IMapper _mapper;
        private readonly IFileEntityDbContext _fileEntityDbContext;

        public GetFileLoadPercentQueryHandler(IMapper mapper, IFileEntityDbContext fileEntityDbContext)
        {
            _mapper = mapper;
            _fileEntityDbContext = fileEntityDbContext;
        }

        public async Task<FileEntitiesLookup> Handle(GetLoadedFilesQuery request, CancellationToken cancellationToken)
        {
            var files = await _fileEntityDbContext.FileEntities
                .Where(file => file.OwnerId == request.UserId).ProjectTo<FileEntityVm>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new FileEntitiesLookup() { FileEntityVms = files };
        }
    }
}
