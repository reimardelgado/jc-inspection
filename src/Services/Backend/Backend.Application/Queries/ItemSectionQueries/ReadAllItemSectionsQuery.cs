
using Backend.Application.DTOs.Responses.ItemSectionResponses;

namespace Backend.Application.Queries.ItemSectionQueries
{
    public class ReadAllItemSectionsQuery : IRequest<EntityResponse<List<ItemSectionModelResponse>>>
    {
        public Guid FormTemplateId { get; set; }
        public ReadAllItemSectionsQuery(Guid formTemplateId)
        {
            FormTemplateId = formTemplateId;
        }
    }
}