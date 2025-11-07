using Backend.Application.DTOs.Responses.InspectionResultResponses;
using Backend.Application.Specifications.InspectionResultSpecs;

namespace Backend.Application.Queries.InspectionResultQueries
{
    public class ReadInspectionResultsQueryHandler : IRequestHandler<ReadInspectionResultsQuery,
        EntityResponse<GetEntitiesResponse<InspectionResultResponse>>>
    {
        private readonly IRepository<InspectionResult> _repository;

        public ReadInspectionResultsQueryHandler(IRepository<InspectionResult> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<GetEntitiesResponse<InspectionResultResponse>>> Handle(ReadInspectionResultsQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new InspectionResultSpec(query.QueryParam, query.IsPagingEnabled, query.Page, query.PageSize);

            //Get the total amount of entities
            var total = await _repository.CountAsync(spec, cancellationToken);
            //Get entity list
            var entityCollection = await _repository.ListAsync(spec, cancellationToken);

            var filterResponse = new PaginationResponse(query.Page, query.PageSize, total);

            return new GetEntitiesResponse<InspectionResultResponse>(
                entityCollection.Select(InspectionResultResponse.FromEntity).ToList(),
                filterResponse
            );
        }
    }
}
