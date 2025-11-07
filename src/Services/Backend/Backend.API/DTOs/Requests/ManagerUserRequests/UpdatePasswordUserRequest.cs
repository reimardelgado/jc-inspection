using Backend.Application.Commands.UserCommands;

namespace Backend.API.DTOs.Requests.ManagerUserRequests;

public class UpdatePasswordUserRequest
{
    #region Contructor && Properties

    public string ActualPassword { get; set; }
    public string NewPassword { get; set; }

    public UpdatePasswordUserRequest(string actualPassword, string newPassword)
    {
        ActualPassword = actualPassword;
        NewPassword = newPassword;
    }


    #endregion

    public UpdatePasswordUserCommand ToApplicationRequest(Guid userId)
    {
        return new UpdatePasswordUserCommand(userId, ActualPassword, NewPassword);
    }
}