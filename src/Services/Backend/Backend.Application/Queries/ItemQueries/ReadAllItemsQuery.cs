
using Backend.Application.DTOs.Responses.ItemResponses;

namespace Backend.Application.Queries.ItemQueries
{
    public class ReadAllItemsQuery : IRequest<EntityResponse<List<ItemModelResponse>>>
    {
        public Guid FormTemplateId { get; set; }
        public Guid ItemSectionId { get; set; }
        public ReadAllItemsQuery(Guid formTemplateId, Guid itemSectionId)
        {
            FormTemplateId = formTemplateId;
            ItemSectionId = itemSectionId;
        }
    }
}