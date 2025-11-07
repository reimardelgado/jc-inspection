using Newtonsoft.Json;

namespace Backend.Domain.DTOs.Requests.Zoho;

public class ZohoDeal
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
}