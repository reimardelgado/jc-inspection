using Backend.Application.Queries.ProfileQueries;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.ProfileRequests;

public class ReadProfilesRequest : BaseFilterDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public ReadProfilesQuery ToApplicationRequest()
    {
        return new ReadProfilesQuery(Name, Description, LoadChildren, IsPagingEnabled, Page, PageSize);
    }
}