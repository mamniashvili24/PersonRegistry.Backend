using Microsoft.Extensions.DependencyInjection;
using PersonRegistry.Application.Repositories;
using PersonRegistry.Persistence.Repositories;

namespace PersonRegistry.Persistence;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IPersonRepository, PersonRepository>();

        return services;
    }
}