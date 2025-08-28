using System.Reflection;
using AutoMapper;

namespace PersonRegistry.Application.Commons.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        Type[] types = Assembly.GetExecutingAssembly().GetExportedTypes();

        foreach (Type type in types)
        {
            if (typeof(IMap).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            {
                IMap? instance = Activator.CreateInstance(type) as IMap;
                instance?.Mapping(this);
            }
        }
    }
}