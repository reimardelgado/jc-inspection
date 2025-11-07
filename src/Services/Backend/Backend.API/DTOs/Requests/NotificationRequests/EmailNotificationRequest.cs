using Backend.Application.Commands.NotificationCommands;

namespace Backend.API.DTOs.Requests.NotificationRequests;

public class EmailNotificationRequest
{
    public string To { get; set; }
    public string Cc { get; set; }
    public string Cco { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<string> Attachment { get; set; } = new();

    public EmailNotificationRequest(string to, string cc, string cco, string subject, string body, List<string> attachment)
    {
        To = to;
        Cc = cc;
        Cco = cco;
        Subject = subject;
        Body = body;
        Attachment = attachment;
    }

    public SendEmailNotificationCommand ToApplicationRequest()
    {
        return new SendEmailNotificationCommand(To, Cc, Cco, Subject, Body, Attachment);
    }
}