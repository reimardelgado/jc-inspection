using Newtonsoft.Json;

namespace Backend.Domain.DTOs.Responses.Zoho;

public class TokenModel
{
    [JsonProperty(PropertyName = "access_token")]
    public string AccessToken { get; set; }

    [JsonProperty(PropertyName = "expire")]
    public bool Expire { get; set; } = false;
}