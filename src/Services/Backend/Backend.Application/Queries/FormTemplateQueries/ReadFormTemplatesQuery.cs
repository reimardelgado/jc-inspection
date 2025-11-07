using Backend.Application.DTOs.Responses.FormTemplateResponses;
using Shared.Domain.Specification;

namespace Backend.Application.Queries.FormTemplateQueries
{
    public class ReadFormTemplatesQuery : BaseFilter, IRequest<EntityResponse<GetEntitiesResponse<FormTemplateResponse>>>
    {
        public string? QueryParam { get; set; }

        public ReadFormTemplatesQuery(string? queryParam, bool loadChildren,
            bool isPagingEnabled, int page, int pageSize)
        {
            QueryParam = queryParam;
            LoadChildren = loadChildren;
            IsPagingEnabled = isPagingEnabled;
            Page = page;
            PageSize = pageSize;
        }
    }
}