using Newtonsoft.Json;

namespace Backend.Domain.DTOs.Responses.Zoho;

public class RefreshTokenModel
{
    [JsonProperty(PropertyName = "access_token")]
    public string AccessToken { get; set; }
    [JsonProperty(PropertyName = "expires_in_sec")]
    public int ExpireInSec { get; set; }
    [JsonProperty(PropertyName = "api_domain")]
    public string ApiDomain { get; set; }
    [JsonProperty(PropertyName = "token_type")]
    public string TokenType { get; set; }
    [JsonProperty(PropertyName = "expires_in")]
    public int ExpireIn { get; set; }
}