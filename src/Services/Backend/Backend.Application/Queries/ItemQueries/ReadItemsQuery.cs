using Backend.Application.DTOs.Responses.ItemResponses;
using Shared.Domain.Specification;

namespace Backend.Application.Queries.ItemQueries
{
    public class ReadItemsQuery : BaseFilter, IRequest<EntityResponse<GetEntitiesResponse<ItemResponse>>>
    {
        public Guid? FormTemplateId { get; set; }
        public Guid? ItemSectionId { get; set; }
        public string? QueryParam { get; set; }

        public ReadItemsQuery(Guid? formTemplateId, Guid? itemSectionId, string? queryParam, bool loadChildren,
            bool isPagingEnabled, int page, int pageSize)
        {
            FormTemplateId = formTemplateId;
            ItemSectionId = itemSectionId;
            QueryParam = queryParam;
            LoadChildren = loadChildren;
            IsPagingEnabled = isPagingEnabled;
            Page = page;
            PageSize = pageSize;
        }
    }
}