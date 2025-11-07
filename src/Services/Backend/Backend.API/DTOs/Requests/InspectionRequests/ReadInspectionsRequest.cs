using Backend.Application.Queries.InspectionQueries;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.InspectionRequests;

public class ReadInspectionsRequest : BaseFilterDto
{
    public string? QueryParam { get; set; }

    public ReadInspectionsQuery ToApplicationRequest()
    {
        return new ReadInspectionsQuery(QueryParam, LoadChildren, IsPagingEnabled, Page, PageSize);
    }

}