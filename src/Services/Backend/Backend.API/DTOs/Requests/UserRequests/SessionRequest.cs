
namespace Backend.API.DTOs.Requests.UserRequests;

public class SessionRequest
{
    public string Email { get; set; }
    public Guid OperationCode { get; }
    public string OtpCode { get; }

    public SessionRequest(string email, Guid operationCode, string otpCode)
    {
        Email = email;
        OperationCode = operationCode;
        OtpCode = otpCode;
    }

}