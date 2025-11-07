using Newtonsoft.Json;

namespace Backend.Application.DTOs.Responses.ZohoResponses;

public class ZohoTokenResponse
{
    public string AccessToken { get; set; }
    // public int ExpireInSec { get; set; }
    // public string ApiDomain { get; set; }
    // public string TokenType { get; set; }
    // public int ExpireIn { get; set; }

    public ZohoTokenResponse(string accessToken)
    {
        AccessToken = accessToken;
    }
}