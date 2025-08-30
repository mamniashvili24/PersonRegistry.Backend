using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonRegistry.Application.ConfigurationOptions;
using PersonRegistry.Application.FileStorage;
using PersonRegistry.Application.Repositories;
using PersonRegistry.Persistence.Database;
using PersonRegistry.Persistence.Database.Repositories;
using PersonRegistry.Persistence.FileStorage;

namespace PersonRegistry.Persistence;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<PersonRegistryDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("Database")));


        FileStorageOptions fileStorageOptions = new();
        configuration.GetSection(nameof(FileStorageOptions)).Bind(fileStorageOptions);
        string? blobConnectionString = configuration.GetConnectionString("BlobStorage");

        services.AddSingleton(_ =>
            new BlobContainerClient(blobConnectionString, fileStorageOptions.ContainerName));

        services.AddSingleton<IFileStorage, AzureBlobFileStorage>();
        
        services.AddScoped<IPersonRepository, PersonRepository>();

        return services;
    }
}