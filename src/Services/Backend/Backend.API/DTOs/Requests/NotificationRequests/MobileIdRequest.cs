using Backend.Application.Commands.NotificationCommands;

namespace Backend.API.DTOs.Requests.NotificationRequests;

public class MobileIdRequest
{
    public string Email { get; set; }
    public string MobileId { get;}

    public MobileIdRequest(string email, string mobileId)
    {
        Email = email;
        MobileId = mobileId;
    }

    public UpdateMobileIdUserCommand ToApplicationRequest()
    {
        return new UpdateMobileIdUserCommand(Email, MobileId);
    }
}