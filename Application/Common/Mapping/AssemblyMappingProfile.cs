using AutoMapper;
using System.Reflection;

namespace TestTask.Application.Common.Mapping
{
    public class AssemblyMappingProfile: Profile
    {
        public AssemblyMappingProfile(Assembly assembly)
        {
            ApplyMappingFromAssembly(assembly);
        }
        public void ApplyMappingFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(x => x.GetInterfaces()
                    .Any(t => t.IsGenericType 
                    && t.GetGenericTypeDefinition() == typeof(IMapWith<>)));

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var method = type.GetMethod("Mapping");
                method?.Invoke(instance, new object[] { this });
            }
        }
    }
}
