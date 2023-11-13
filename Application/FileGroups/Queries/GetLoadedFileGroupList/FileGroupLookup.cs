using AutoMapper;
using Domain;
using TestTask.Application.Common.Mapping;

namespace TestTask.Application.FileGroups.Queries.GetLoadedFileGroupList
{
    public class FileGroupLookup: IMapWith<FileGroup>
    {
        public Guid Id { get; set; }
        public IList<FileEntityVm> Files { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<FileGroup, FileGroupLookup>()
                .ForMember(fileGroupLookup => fileGroupLookup.Id
                    , opt => opt.MapFrom(fileGroup => fileGroup.Id))
                .ForMember(fileGroupLookup => fileGroupLookup.Files
                    , opt => opt.MapFrom(fileGroup => fileGroup.Files));

        }
    }
}
