namespace Shared.Domain.DTOs.Mail;

public class MailRequest
{
    public string Template { get; set; }
    public string RecieverId { get; set; }
    public string RecieverName { get; set; }
    public string RecieverEmail { get; set; }
    public string Bcc { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }

    public MailRequest(string template, string recieverId, string recieverName, string recieverEmail, string bcc,
        string subject, string content)
    {
        Template = template;
        RecieverId = recieverId;
        RecieverName = recieverName;
        RecieverEmail = recieverEmail;
        Bcc = bcc;
        Subject = subject;
        Content = content;
    }
}