using AutoMapper;
using TestTask.Application.Common.Mapping;

namespace TestTask.Application.FileEntityBL.Queries.GetLoadedFiles;

public class FileEntityVm : IMapWith<Domain.FileEntity>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public long Length { get; set; }
    public Guid GroupId { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.FileEntity, FileEntityVm>()
            .ForMember(file => file.Id,
                opt => opt.MapFrom(fileEntity => fileEntity.Id))
            .ForMember(file => file.Name,
                opt => opt.MapFrom(fileEntity => fileEntity.Name))
            .ForMember(file => file.Length,
                opt => opt.MapFrom(fileEntity => fileEntity.Length))
            .ForMember(file => file.GroupId,
                opt => opt.MapFrom(fileEntity => fileEntity.GroupId));

    }
}