namespace Backend.Application.Commands.UserCommands
{
    public class DeleteUserCommand : IRequest<EntityResponse<bool>>
    {
        public Guid UserId { get; }

        public DeleteUserCommand(Guid viaticId)
        {
            UserId = viaticId;
        }
    }
}