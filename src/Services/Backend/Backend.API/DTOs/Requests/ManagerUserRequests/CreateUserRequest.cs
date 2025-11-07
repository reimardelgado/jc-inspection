using Backend.Application.Commands.UserCommands;

namespace Backend.API.DTOs.Requests.ManagerUserRequests;

public class CreateUserRequest
{
    #region Contructor && Properties

    public string UserName { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Identification { get; set; }
    public string? Phone { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Avatar { get; set; }
    public List<string> ProfileIds { get;}

    public CreateUserRequest(string userName, string firstName, string lastName, string email,
        string? identification, string? phone, DateTime? dateOfBirth, string? avatar, List<string> profileIds)
    {
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Identification = identification;
        Phone = phone;
        DateOfBirth = dateOfBirth;
        Avatar = avatar;
        ProfileIds = profileIds;
    }



    #endregion

    public CreateUserCommand ToApplicationRequest()
    {
        return new CreateUserCommand(UserName, FirstName, LastName, Email, Identification, Phone, DateOfBirth, Avatar, ProfileIds);
    }
}