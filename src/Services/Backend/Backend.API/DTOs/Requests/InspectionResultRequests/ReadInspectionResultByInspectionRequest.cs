using Backend.Application.Queries.InspectionQueries;
using Backend.Application.Queries.InspectionResultQueries;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.InspectionResultRequests;

public class ReadInspectionResultByInspectionRequest : BaseFilterDto
{
    public string InspectionId { get; }
    public string FormTemplateId { get; set; }

    public ReadInspectionResultByInspectionRequest(string inspectionId, string formTemplateId)
    {
        InspectionId = inspectionId;
        FormTemplateId = formTemplateId;
    }

    public ReadInspectionResultByInspectionQuery ToApplicationRequest()
    {
        return new ReadInspectionResultByInspectionQuery(Guid.Parse(InspectionId), Guid.Parse(FormTemplateId));
    }
}