using Backend.Domain.DTOs.Requests.Zoho;

namespace Backend.Domain.Interfaces.Services;

public interface IZohoExternalUserService
{
    Task<string> CreateInZoho(ZohoExternalUser entity, CancellationToken cancellationToken);
    Task<string> UpdateInZoho(ZohoExternalUser entity, CancellationToken cancellationToken);
}