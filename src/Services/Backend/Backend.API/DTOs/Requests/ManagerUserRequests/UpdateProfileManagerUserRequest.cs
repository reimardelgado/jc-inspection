using Backend.Application.Commands.UserCommands;

namespace Backend.API.DTOs.Requests.ManagerUserRequests;

public class UpdateProfileManagerUserRequest
{
    #region Contructor && Properties
    public string? Username { get; set; }
    public string? FirstName { get; }
    public string? LastName { get; }
    public string? IdentificationType { get; }
    public string? Identification { get; }
    public string? Email { get; }
    public string? Phone { get; }
    public string? Gender { get; }


    public UpdateProfileManagerUserRequest(string? userName, string? firstName, string? lastName, string? email,
        string? phone, string? identification, string? identificationType, string? gender)
    {
        Username = userName;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Identification = identification;
        IdentificationType = identificationType;
        Gender = gender;
    }

    #endregion

    public UpdateProfileUserCommand ToApplicationRequest(Guid managerId)
    {
        return new UpdateProfileUserCommand(managerId, Username, FirstName, LastName,
            Identification, Email, Phone, Gender);
    }
}