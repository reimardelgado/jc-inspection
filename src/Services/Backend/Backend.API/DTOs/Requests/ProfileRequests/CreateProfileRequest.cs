using Backend.Application.Commands.ProfileCommands;

namespace Backend.API.DTOs.Requests.ProfileRequests;

public class CreateProfileRequest
{
    public string Name { get; }
    public string Description { get; }
    public List<string> ProfilePermissions { get;}

    public CreateProfileRequest(string name, string description, List<string> profilePermissions)
    {
        Name = name;
        Description = description;
        ProfilePermissions = profilePermissions;
    }

    public CreateProfileCommand ToApplicationRequest()
    {
        return new CreateProfileCommand( Name, Description, ProfilePermissions);
    }
}