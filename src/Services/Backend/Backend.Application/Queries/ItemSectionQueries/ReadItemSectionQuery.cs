
using Backend.Application.DTOs.Responses.ItemSectionResponses;

namespace Backend.Application.Queries.ItemSectionQueries
{
    public class ReadItemSectionQuery : IRequest<EntityResponse<ItemSectionResponse>>
    {
        public Guid ItemSectionId { get; }

        public ReadItemSectionQuery(Guid id)
        {
            ItemSectionId = id;
        }
    }
}