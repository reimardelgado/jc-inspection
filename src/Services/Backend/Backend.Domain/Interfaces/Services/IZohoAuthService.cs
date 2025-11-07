using Backend.Domain.DTOs.Responses.Zoho;

namespace Backend.Domain.Interfaces.Services;

public interface IZohoAuthService
{
    Task<RefreshTokenModel> GetAccessToken(string refreshToken, string clientId, string clientSecret, CancellationToken cancellationToken);
}