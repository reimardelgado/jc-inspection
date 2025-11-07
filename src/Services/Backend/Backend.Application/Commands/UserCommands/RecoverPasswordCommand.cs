using Backend.Application.DTOs.Responses.UserResponses;

namespace Backend.Application.Commands.UserCommands
{
    public class RecoverPasswordCommand : IRequest<EntityResponse<bool>>
    {
        public string Email { get; }

        public RecoverPasswordCommand(string email)
        {
            Email = email;
        }
    }
}