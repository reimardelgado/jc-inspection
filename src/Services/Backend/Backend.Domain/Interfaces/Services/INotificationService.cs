using Backend.Domain.DTOs.Requests;

namespace Backend.Domain.Interfaces.Services;

public interface INotificationService
{
    public bool SendEmailNotification(EmailNotifictionModel notification);
    public bool SendPushNotification(PushNotificationModel notification);
}
