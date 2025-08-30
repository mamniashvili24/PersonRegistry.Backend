namespace PersonRegistry.Domain.Models;

public class FileContainer : IDisposable
{
    public FileContainer(
        string name,
        Stream stream,
        string extension,
        string contentType)
    {
        Name = name;
        Stream = stream;
        Extension = extension;
        ContentType = contentType;
    }
    
    public string Name { get; }
    public Stream Stream { get; }
    public string Extension { get; }
    public string ContentType { get; }

    public void Dispose()
    {
        Stream?.Dispose();
    }
}