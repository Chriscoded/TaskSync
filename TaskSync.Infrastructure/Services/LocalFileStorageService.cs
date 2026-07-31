using Microsoft.AspNetCore.Hosting;
using TaskSync.Application.Interfaces;

namespace TaskSync.Infrastructure.Services;

public sealed class LocalFileStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment _environment;

    public LocalFileStorageService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public async Task<string> UploadAsync(
        Stream stream,
        string fileName,
        CancellationToken cancellationToken = default)
    {
        var uploads = Path.Combine(
            _environment.WebRootPath,
            "uploads");

        Directory.CreateDirectory(uploads);

        var uniqueName =
            $"{Guid.NewGuid()}_{fileName}";

        var path = Path.Combine(
            uploads,
            uniqueName);

        await using var file = File.Create(path);

        await stream.CopyToAsync(
            file,
            cancellationToken);

        return $"/uploads/{uniqueName}";
    }

    public Task DeleteAsync(
        string path,
        CancellationToken cancellationToken = default)
    {
        var physicalPath = Path.Combine(
            _environment.WebRootPath,
            path.TrimStart('/'));

        if (File.Exists(physicalPath))
            File.Delete(physicalPath);

        return Task.CompletedTask;
    }
}