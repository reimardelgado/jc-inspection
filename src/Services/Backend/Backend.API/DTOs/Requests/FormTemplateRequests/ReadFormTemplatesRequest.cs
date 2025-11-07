using Backend.Application.Queries.FormTemplateQueries;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.FormTemplateRequests;

public class ReadFormTemplatesRequest : BaseFilterDto
{
    public string? QueryParam { get; set; }

    public ReadFormTemplatesQuery ToApplicationRequest()
    {
        return new ReadFormTemplatesQuery(QueryParam, LoadChildren, IsPagingEnabled, Page, PageSize);
    }

}