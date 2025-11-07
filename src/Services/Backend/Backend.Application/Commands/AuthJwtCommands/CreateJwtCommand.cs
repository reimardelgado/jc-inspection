namespace Backend.Application.Commands.AuthJwtCommands
{
    public class CreateJwtCommand : IRequest<EntityResponse<JwtResponse>>
    {
        public Guid UserId { get; }

        public CreateJwtCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}