
namespace Backend.API.DTOs.Requests.UserRequests;

public class RegisterMemberUserRequest
{
    #region Contructor && Properties

    public string UserName { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Email { get; }
    public string Phone { get; }
    public string? IdentificationType { get; }
    public string? Identification { get; }
    public string? Location { get; }
    public string? Gender { get; }
    public DateTime? DateOfBirth { get; }
    public string? MaritalStatus { get; }
    public Guid OperationCode { get; }
    public string OtpCode { get; }
    public RegisterMemberUserRequest(string userName, string firstName, string lastName, string email, string phone,
        string? location, string? identification, string? identificationType, string? gender, DateTime? dateOfBirth,
        string? maritalStatus, Guid operationCode, string otpCode)
    {
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Location = location;
        Identification = identification;
        IdentificationType = identificationType;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        MaritalStatus = maritalStatus;
        OperationCode = operationCode;
        OtpCode = otpCode;
    }

    #endregion

}