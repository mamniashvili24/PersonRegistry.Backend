using System.Reflection.Metadata;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using PersonRegistry.Application.ConfigurationOptions;
using PersonRegistry.Application.FileStorage;
using PersonRegistry.Domain.Models;

namespace PersonRegistry.Persistence.FileStorage;

public class AzureBlobFileStorage : IFileStorage
{
    private readonly BlobContainerClient containerClient;
    
    public AzureBlobFileStorage(BlobContainerClient containerClient)
    {
        this.containerClient = containerClient;
    }

    public async Task UploadAsync(FileContainer file, CancellationToken cancellationToken)
    {
        BlobClient blobClient = containerClient.GetBlobClient(file.Name);
        await blobClient.UploadAsync(file.Stream, new BlobHttpHeaders { ContentType = file.ContentType }, cancellationToken: cancellationToken);
    }

    public async Task<Stream?> DownloadAsync(string fileName, CancellationToken cancellationToken)
    {
        BlobClient blobClient = containerClient.GetBlobClient(fileName);
        if (await blobClient.ExistsAsync(cancellationToken))
        {
            Response<BlobDownloadInfo>? response = await blobClient.DownloadAsync(cancellationToken);
            return response.Value.Content;
        }

        return null;
    }

    public async Task DeleteAsync(string fileName, CancellationToken cancellationToken)
    {
        BlobClient blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> ExistsAsync(string fileName, CancellationToken cancellationToken)
    {
        BlobClient blobClient = containerClient.GetBlobClient(fileName);
        return await blobClient.ExistsAsync(cancellationToken);
    }
    
    public string GetUrl(string fileName)
    {
        BlobClient blobClient = containerClient.GetBlobClient(fileName);
        return blobClient.Uri.ToString();
    }
}