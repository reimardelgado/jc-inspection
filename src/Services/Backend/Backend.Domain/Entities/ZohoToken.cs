using Backend.Domain.SeedWork;

namespace Backend.Domain.Entities;

public class ZohoToken : BaseEntity
{
    public string AccessToken { get; set; }
    public string Status { get; set; } = StatusEnum.Active;

    public ZohoToken(string accessToken)
    {
        AccessToken = accessToken;
    }
}