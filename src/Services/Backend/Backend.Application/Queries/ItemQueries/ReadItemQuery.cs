
using Backend.Application.DTOs.Responses.ItemResponses;

namespace Backend.Application.Queries.ItemQueries
{
    public class ReadItemQuery : IRequest<EntityResponse<ItemResponse>>
    {
        public Guid ItemId { get; }

        public ReadItemQuery(Guid id)
        {
            ItemId = id;
        }
    }
}