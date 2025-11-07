using Backend.Application.Commands.UserCommands;

namespace Backend.API.DTOs.Requests.ManagerUserRequests;

public class UpdateUserRequest
{
    #region Contructor && Properties

    public string Username { get; set; }
    public string FirstName { get; }
    public string LastName { get; }
    public string? Identification { get; }
    public string Email { get; }
    public string Phone { get; }
    public DateTime? DateOfBirth { get; }
    public string? Avatar { get; set; }
    public List<string> ProfileIds { get; set; }

    public UpdateUserRequest(string username, string firstName, string lastName, string email, string phone,
        string? identification, DateTime? dateOfBirth, string? avatar, List<string> profileIds)
    {
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Identification = identification;
        DateOfBirth = dateOfBirth;
        Avatar = avatar;
        ProfileIds = profileIds;
    }

    #endregion

    public UpdateUserCommand ToApplicationRequest(Guid userId)
    {
        return new UpdateUserCommand(userId, Username, FirstName, LastName, Email, Identification, Phone,
            DateOfBirth, Avatar, ProfileIds);
    }
}