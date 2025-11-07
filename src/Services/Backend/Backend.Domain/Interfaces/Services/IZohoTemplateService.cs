using Backend.Domain.DTOs.Requests.Zoho;

namespace Backend.Domain.Interfaces.Services;

public interface IZohoTemplateService
{
    Task<string> CreateInZoho(ZohoTemplate entity, CancellationToken cancellationToken);
    Task<string> UpdateInZoho(string idTemplateZoho, string templateId, CancellationToken cancellationToken);
    Task<ZohoTemplate> ReadTemplateZoho(string idZoho, string templateId,
        CancellationToken cancellationToken);
}