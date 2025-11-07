namespace Shared.Domain.DTOs.File;

public class CreateFileResponseDto
{
    public string Url { get; set; }

    public CreateFileResponseDto(string url)
    {
        Url = url;
    }
}