using Backend.Application.Commands.UserCommands;

namespace Backend.API.DTOs.Requests.ManagerUserRequests;

public class ValidateLoginRequest
{
    public string Username { get; }
    public string Password { get; }

    public ValidateLoginRequest(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public LoginUserCommand ToApplicationRequest()
    {
        return new LoginUserCommand(Username, Password);
    }
}