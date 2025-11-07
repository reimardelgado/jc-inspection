using Newtonsoft.Json;

namespace Backend.Application.DTOs.Responses.ZohoResponses;

public class ZohoRefreshTokenResponse
{
    public string AccessToken { get; set; }
    public int ExpireInSec { get; set; }
    public string ApiDomain { get; set; }
    public string TokenType { get; set; }
    public int ExpireIn { get; set; }

    public ZohoRefreshTokenResponse(string accessToken, int expireInSec, string apiDomain, string tokenType, int expireIn)
    {
        AccessToken = accessToken;
        ExpireInSec = expireInSec;
        ApiDomain = apiDomain;
        TokenType = tokenType;
        ExpireIn = expireIn;
    }
}