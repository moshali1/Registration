using Microsoft.AspNetCore.Components.Forms;

namespace RegistrationPortal.Shared.Helpers;
public class DefaultBrowserFile : IBrowserFile
{
    public DefaultBrowserFile(string name, long size, string contentType, DateTimeOffset lastModified)
    {
        Name = name;
        Size = size;
        ContentType = contentType;
        LastModified = lastModified;
    }

    public long Size { get; }
    public string ContentType { get; }
    public string Name { get; }
    public DateTimeOffset LastModified { get; }

    public Task CopyToAsync(Stream stream)
    {
        // Implement if needed
        return Task.CompletedTask;
    }

    public Stream OpenReadStream(long maxAllowedSize = 512000, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<byte[]> ReadAllBytesAsync()
    {
        // Implement if needed
        return Task.FromResult(Array.Empty<byte>());
    }

    public Task<string> ReadAsStringAsync()
    {
        // Implement if needed
        return Task.FromResult("");
    }
}
