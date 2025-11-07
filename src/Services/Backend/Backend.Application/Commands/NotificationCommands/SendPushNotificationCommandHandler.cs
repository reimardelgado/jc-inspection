using Backend.Domain.DTOs.Requests;
using Backend.Domain.Interfaces.Services;

namespace Backend.Application.Commands.NotificationCommands
{
    public class SendPushNotificationCommandHandler : IRequestHandler<SendPushNotificationCommand, EntityResponse<bool>>
    {
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;

        private User? _user;

        public SendPushNotificationCommandHandler(IMediator mediator, INotificationService notificationService)
        {
            _mediator = mediator;
            _notificationService = notificationService;
        }

        public async Task<EntityResponse<bool>> Handle(SendPushNotificationCommand command, CancellationToken cancellationToken)
        {
            var model = new PushNotificationModel()
            {
                To = command.To,
                Title = command.Title,
                Badge = command.Badge,
                Body = command.Body,
                Priority = command.Priority,
                Sound = command.Sound
            };
            var response = _notificationService.SendPushNotification(model);

            return EntityResponse.Success(response);
        }

        #region Private methods


        #endregion
    }
}