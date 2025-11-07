namespace Backend.Application.Commands.NotificationCommands
{
    public class SendPushNotificationCommand : IRequest<EntityResponse<bool>>
    {
        public string To { get; }
        public string Title { get; }
        public string Body { get; set; }
        public int Badge { get; set; }
        public string Priority { get; set; }
        public string Sound { get; set; }

        public SendPushNotificationCommand(string to, string title, string body)
        {
            To = to;
            Title = title;
            Body = body;
            Badge = 1;
            Priority = "high";
            Sound = "default";
        }
    }
}