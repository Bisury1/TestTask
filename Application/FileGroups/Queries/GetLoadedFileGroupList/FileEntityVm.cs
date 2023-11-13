using AutoMapper;
using TestTask.Application.Common.Mapping;

namespace TestTask.Application.FileGroups.Queries.GetLoadedFileGroupList;

public class FileEntityVm : IMapWith<Domain.FileEntity>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.FileEntity, FileEntityVm>()
            .ForMember(file => file.Id,
                opt => opt.MapFrom(fileEntity => fileEntity.Id))
            .ForMember(file => file.Name,
                opt => opt.MapFrom(fileEntity => fileEntity.Name));
    }
}