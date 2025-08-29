using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonRegistry.Application.Repositories;
using PersonRegistry.Persistence.Database;
using PersonRegistry.Persistence.Database.Repositories;

namespace PersonRegistry.Persistence;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<PersonRegistryDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<IPersonRepository, PersonRepository>();

        return services;
    }
}