using Backend.Application.DTOs.Responses.ItemSectionResponses;
using Shared.Domain.Specification;

namespace Backend.Application.Queries.ItemSectionQueries
{
    public class ReadItemSectionsQuery : BaseFilter, IRequest<EntityResponse<GetEntitiesResponse<ItemSectionResponse>>>
    {
        public Guid? FormTemplateId { get; set; }
        public string? QueryParam { get; set; }

        public ReadItemSectionsQuery(Guid? formTemplateId, string? queryParam, bool loadChildren,
            bool isPagingEnabled, int page, int pageSize)
        {
            FormTemplateId = formTemplateId;
            QueryParam = queryParam;
            LoadChildren = loadChildren;
            IsPagingEnabled = isPagingEnabled;
            Page = page;
            PageSize = pageSize;
        }
    }
}