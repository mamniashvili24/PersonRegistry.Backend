using System.ComponentModel.DataAnnotations;

namespace PersonRegistry.Application.ConfigurationOptions;

public class FileStorageOptions
{
    public string ContainerName { get; set; }
}