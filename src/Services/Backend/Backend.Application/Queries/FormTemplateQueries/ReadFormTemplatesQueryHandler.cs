using Backend.Application.DTOs.Responses.FormTemplateResponses;
using Backend.Application.Specifications.FormTemplateSpecs;

namespace Backend.Application.Queries.FormTemplateQueries
{
    public class ReadFormTemplatesQueryHandler : IRequestHandler<ReadFormTemplatesQuery,
        EntityResponse<GetEntitiesResponse<FormTemplateResponse>>>
    {
        private readonly IRepository<FormTemplate> _repository;

        public ReadFormTemplatesQueryHandler(IRepository<FormTemplate> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<GetEntitiesResponse<FormTemplateResponse>>> Handle(ReadFormTemplatesQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new FormTemplateSpec(query.QueryParam, query.IsPagingEnabled, query.Page, query.PageSize);

            //Get the total amount of entities
            var total = await _repository.CountAsync(spec, cancellationToken);
            //Get entity list
            var entityCollection = await _repository.ListAsync(spec, cancellationToken);

            var filterResponse = new PaginationResponse(query.Page, query.PageSize, total);

            return new GetEntitiesResponse<FormTemplateResponse>(
                entityCollection.Select(FormTemplateResponse.FromEntity).ToList(),
                filterResponse
            );
        }
    }
}
