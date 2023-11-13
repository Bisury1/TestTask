using AutoMapper;
using Domain;
using TestTask.Application.Common.Mapping;

namespace TestTask.Application.CommonFileBl.Queries.CommonGetFilesByHashLink;

public class FilePath: IMapWith<FileEntity>
{
    public string Path { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<FileEntity, FilePath>()
            .ForMember(filePath => filePath.Path, opt
                => opt.MapFrom(file => file.Path));
    }
}