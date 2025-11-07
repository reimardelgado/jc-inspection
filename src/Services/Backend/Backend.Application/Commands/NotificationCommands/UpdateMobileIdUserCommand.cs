namespace Backend.Application.Commands.NotificationCommands
{
    public class UpdateMobileIdUserCommand : IRequest<EntityResponse<bool>>
    {
        public string Email { get; }
        public string MobileId { get; }

        public UpdateMobileIdUserCommand(string email, string mobileId)
        {
            Email = email;
            MobileId = mobileId;
        }
    }
}