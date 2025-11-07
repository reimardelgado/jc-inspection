using Backend.Application.Queries.InspectionQueries;
using Backend.Application.Queries.InspectionResultQueries;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.InspectionResultRequests;

public class ReadInspectionResultRequest : BaseFilterDto
{
    public string InspectionResultId { get; }

    public ReadInspectionResultRequest(string inspectionResultId)
    {
        InspectionResultId = inspectionResultId;
    }

    public ReadInspectionResultQuery ToApplicationRequest()
    {
        return new ReadInspectionResultQuery(Guid.Parse(InspectionResultId));
    }
}