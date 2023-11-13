using AutoMapper;

namespace TestTask.Application.Common.Mapping
{
    public interface IMapWith<T>
    {
        void Mapping(Profile profile);
    }
}
