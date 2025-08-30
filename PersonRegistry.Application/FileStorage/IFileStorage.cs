using PersonRegistry.Domain.Models;

namespace PersonRegistry.Application.FileStorage;

public interface IFileStorage
{
    Task UploadAsync(FileContainer file, CancellationToken cancellationToken);
    Task<Stream?> DownloadAsync(string fileName, CancellationToken cancellationToken);
    Task DeleteAsync(string fileName, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(string fileName, CancellationToken cancellationToken);
    string GetUrl(string fileName);
}