using Backend.Application.DTOs.Responses.UserResponses;

namespace Backend.Application.Commands.UserCommands
{
    public class LoginUserCommand : IRequest<EntityResponse<ReadUserMeResponse>>
    {
        public string Username { get; }
        public string Password { get; }

        public LoginUserCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}