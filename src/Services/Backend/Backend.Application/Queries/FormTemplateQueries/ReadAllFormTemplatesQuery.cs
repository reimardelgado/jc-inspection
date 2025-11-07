
using Backend.Application.DTOs.Responses.FormTemplateResponses;

namespace Backend.Application.Queries.FormTemplateQueries
{
    public class ReadAllFormTemplatesQuery : IRequest<EntityResponse<List<FormTemplateModelResponse>>>
    {
        public ReadAllFormTemplatesQuery()
        {
        }
    }
}