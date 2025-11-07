
using Backend.Application.DTOs.Responses.FormTemplateResponses;

namespace Backend.Application.Queries.FormTemplateQueries
{
    public class ReadFormTemplateQuery : IRequest<EntityResponse<FormTemplateModelResponse>>
    {
        public Guid FormTemplateId { get; }

        public ReadFormTemplateQuery(Guid id)
        {
            FormTemplateId = id;
        }
    }
}