using Backend.Application.Commands.UserCommands;

namespace Backend.API.DTOs.Requests.ManagerUserRequests;

public class RecoverPasswordRequest
{
    public string Email { get; }

    public RecoverPasswordRequest(string email)
    {
        Email = email;
    }

    public RecoverPasswordCommand ToApplicationRequest()
    {
        return new RecoverPasswordCommand(Email);
    }
}