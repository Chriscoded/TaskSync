namespace TaskSync.Application.Interfaces;

public interface IFileStorageService
{
    Task<string> UploadAsync(
        Stream stream,
        string fileName,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        string path,
        CancellationToken cancellationToken = default);
}