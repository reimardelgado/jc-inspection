
using Backend.Application.Commands.ProfileCommands;
using Backend.Domain.SeedWork;

namespace Backend.API.DTOs.Requests.ProfileRequests;

public class DeleteProfileRequest
{
    public UpdateProfileStatusCommand ToApplicationRequest(Guid profileId)
    {
        return new UpdateProfileStatusCommand(profileId, ProfileStatus.Deleted);
    }
}