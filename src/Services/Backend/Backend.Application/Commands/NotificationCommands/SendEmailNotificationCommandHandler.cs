using Backend.Domain.DTOs.Requests;
using Backend.Domain.Interfaces.Services;

namespace Backend.Application.Commands.NotificationCommands
{
    public class SendEmailNotificationCommandHandler : IRequestHandler<SendEmailNotificationCommand, EntityResponse<bool>>
    {
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;

        private User? _user;

        public SendEmailNotificationCommandHandler(IMediator mediator, INotificationService notificationService)
        {
            _mediator = mediator;
            _notificationService = notificationService;
        }

        public async Task<EntityResponse<bool>> Handle(SendEmailNotificationCommand command, CancellationToken cancellationToken)
        {
            var model = new EmailNotifictionModel()
            {
                To = command.To,
                Subject = command.Subject,  
                Body = command.Body,
                Attachment = command.Attachment,
                Cco = command.Cco,
                Cc = command.Cc
            };
            var response = _notificationService.SendEmailNotification(model);

            return EntityResponse.Success(response);
        }

        #region Private methods


        #endregion
    }
}