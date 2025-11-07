namespace Backend.Application.Commands.UserCommands
{
    public class UpdatePasswordUserCommand : IRequest<EntityResponse<bool>>
    {
        public Guid UserId { get; }
        public string ActualPassword { get; set; }
        public string NewPassword { get; set; }

        public UpdatePasswordUserCommand(Guid userId, string actualPassword, string newPassword)
        {
            UserId = userId;
            ActualPassword = actualPassword;
            NewPassword = newPassword;
        }
    }
}