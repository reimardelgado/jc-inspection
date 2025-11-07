namespace Shared.Domain.Interfaces.Microservices.Files;

public interface ISmsRepository
{
    /// <summary>
    /// Download file since AwsS3
    /// </summary>
    /// <param name="presignedUrl"></param>
    /// <param name="phone"></param>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> SendSms(string? presignedUrl, string phone, string message, CancellationToken cancellationToken);
}