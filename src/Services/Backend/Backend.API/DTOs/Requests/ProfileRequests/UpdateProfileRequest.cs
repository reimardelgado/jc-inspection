using Backend.Application.Commands.ProfileCommands;

namespace Backend.API.DTOs.Requests.ProfileRequests;

public class UpdateProfileRequest
{
    public string Name { get; }
    public string Description { get; }
    public List<string> ProfilePermissions { get;}

    public UpdateProfileRequest(string name, string description, List<string> profilePermissions)
    {
        Name = name;
        Description = description;
        ProfilePermissions = profilePermissions;
    }

    public UpdateProfileCommand ToApplicationRequest(Guid profileId)
    {
        return new UpdateProfileCommand(profileId, Name, Description, ProfilePermissions);
    }
}