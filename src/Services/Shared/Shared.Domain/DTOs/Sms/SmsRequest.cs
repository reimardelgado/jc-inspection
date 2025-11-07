namespace Shared.Domain.DTOs.Sms;

public class SmsRequest
{
    public string PhoneNumber { get; set; }
    public string Message { get; set; }

    public SmsRequest(string phoneNumber, string message)
    {
        PhoneNumber = phoneNumber;
        Message = message;
    }
}