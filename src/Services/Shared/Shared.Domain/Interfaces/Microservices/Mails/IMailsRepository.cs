namespace Shared.Domain.Interfaces.Microservices.Mails;

public interface IMailsRepository
{
    /// <summary>
    /// Send Mails
    /// </summary>
    /// <param name="microserviceName"></param>
    /// <param name="presignedUrl"></param>
    /// <param name="template"></param>
    /// <param name="receiverId"></param>
    /// <param name="receiverName"></param>
    /// <param name="receiverEmail"></param>
    /// <param name="bcc"></param>
    /// <param name="subject"></param>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> SendMail(string microserviceName, string? presignedUrl, string template, string receiverId,
        string receiverName,
        string receiverEmail, string bcc, string subject, string content, CancellationToken cancellationToken);
}