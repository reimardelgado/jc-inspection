namespace Shared.Domain.DTOs.File;

public class PresignedUrlDto
{
    public string PresignedUrl { get; private set; }

    public PresignedUrlDto(string presignedUrl)
    {
        PresignedUrl = presignedUrl;
    }
}