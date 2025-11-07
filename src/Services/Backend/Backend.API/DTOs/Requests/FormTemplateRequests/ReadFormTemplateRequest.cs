using Backend.Application.Queries.FormTemplateQueries;
using Shared.API.DTOs;

namespace Backend.API.DTOs.Requests.FormTemplateRequests;

public class ReadFormTemplateRequest : BaseFilterDto
{
    private Guid FormTemplateId { get; }

    public ReadFormTemplateRequest(Guid formTemplated)
    {
        FormTemplateId = formTemplated;
    }

    public ReadFormTemplateQuery ToApplicationRequest()
    {
        return new ReadFormTemplateQuery(FormTemplateId);
    }
}