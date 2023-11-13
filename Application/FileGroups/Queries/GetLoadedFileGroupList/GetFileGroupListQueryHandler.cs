using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Application.FileGroups.Queries.GetLoadedFileGroupList
{
    public class GetFileGroupListQueryHandler: IRequestHandler<GetFileGroupListQuery, FileGroupListVm>
    {
        private readonly IFileGroupDbContext _fileGroupDbContext;
        private readonly IFileEntityDbContext _fileEntityDbContext;
        private readonly IMapper _mapper;

        public GetFileGroupListQueryHandler(IFileGroupDbContext dbContext,
            IMapper mapper, IFileEntityDbContext fileEntityDbContext)
        {
            _fileEntityDbContext = fileEntityDbContext;
            (_fileGroupDbContext, _mapper) = (dbContext, mapper);
        }

        public async Task<FileGroupListVm> Handle(GetFileGroupListQuery request, CancellationToken cancellationToken)
        {
            var fileGroups = await _fileGroupDbContext.FileGroups
                .Where(fileGroup => fileGroup.OwnerId == request.UserId).Include(fileGroup => fileGroup.Files)
                .ProjectTo<FileGroupLookup>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            
            return new FileGroupListVm() { FileGroups = fileGroups }; 
        }
    }
}
