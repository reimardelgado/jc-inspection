using Backend.Domain.DTOs.Requests.Zoho;

namespace Backend.Domain.Interfaces.Services;

public interface IZohoInspectionService
{
    Task<string> CreateInZoho(ZohoInspection entity, CancellationToken cancellationToken);
    
    Task<ZohoInspection> ReadInspectionZoho(string idZoho, string inspectionId, CancellationToken cancellationToken);
    
    Task<string> SendInspectionDataToZoho(string inspectionIdZoho, string inspectionId, string urlReport, CancellationToken cancellationToken);
}