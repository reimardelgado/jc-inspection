
using Backend.Application.Commands.UserCommands;

namespace Backend.API.DTOs.Requests.UserRequests;

public class DeleteUserRequest
{
    public DeleteUserCommand ToApplicationRequest(Guid userId)
    {
        return new DeleteUserCommand(userId);
    }
}