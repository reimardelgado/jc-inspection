using Backend.Application.Queries.ManagerUserQueries;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.ManagerUserRequests;

public class ReadUsersRequest : BaseFilterDto
{
    public string? QueryParam { get; set; }

    public ReadManagerUsersQuery ToApplicationRequest()
    {
        return new ReadManagerUsersQuery(QueryParam, LoadChildren, IsPagingEnabled, Page, PageSize);
    }
}