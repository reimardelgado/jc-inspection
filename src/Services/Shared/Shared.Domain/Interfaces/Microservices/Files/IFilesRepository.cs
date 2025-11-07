using Shared.Domain.DTOs.File;
using Shared.Domain.Models;

namespace Shared.Domain.Interfaces.Microservices.Files;

public interface IFilesRepository
{
    /// <summary>
    /// Download file since AwsS3
    /// </summary>
    /// <param name="presignedUrl"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Stream> DownloadFileAsync(string presignedUrl, CancellationToken cancellationToken);

    /// <summary>
    /// Upload file since AwsS3
    /// </summary>
    /// <param name="createFileUrl">Endpoint URL to upload a file</param>
    /// <param name="fileBytes"></param>
    /// <param name="fileName">The file name</param>
    /// <param name="contentType">File content type</param>
    /// <param name="nameService">The service name that make the request</param>
    /// <returns></returns>
    Task<EntityResponse<CreateFileResponseDto>> UploadFileAsync(byte[] fileBytes, string fileName, string contentType,
        string? nameService);

    /// <summary>
    /// Generate the PreSignedUrl
    /// </summary>
    /// <param name="url"></param>
    /// <param name="nameService"></param>
    /// <returns></returns>
    Task<PresignedUrlDto> PreSignedUrl(string url, string nameService);

    /// <summary>
    /// Delete file since AwsS3
    /// </summary>
    /// <param name="url"></param>
    /// <param name="nameService"></param>
    /// <returns></returns>
    Task<bool> DeleteFile(string url, string nameService);
}