using Backend.Application.Queries.InspectionQueries;
using Backend.Application.Queries.InspectionResultQueries;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.InspectionResultRequests;

public class ReadInspectionResultsRequest : BaseFilterDto
{
    public string? QueryParam { get; set; }

    public ReadInspectionResultsQuery ToApplicationRequest()
    {
        return new ReadInspectionResultsQuery(QueryParam, LoadChildren, IsPagingEnabled, Page, PageSize);
    }

}