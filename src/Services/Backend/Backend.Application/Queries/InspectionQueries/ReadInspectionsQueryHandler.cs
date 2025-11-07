using Backend.Application.DTOs.Responses.InspectionResponses;
using Backend.Application.Specifications.InspectionSpecs;

namespace Backend.Application.Queries.InspectionQueries
{
    public class ReadInspectionsQueryHandler : IRequestHandler<ReadInspectionsQuery,
        EntityResponse<GetEntitiesResponse<InspectionResponse>>>
    {
        private readonly IRepository<Inspection> _repository;

        public ReadInspectionsQueryHandler(IRepository<Inspection> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<GetEntitiesResponse<InspectionResponse>>> Handle(ReadInspectionsQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new InspectionSpec(query.QueryParam, query.IsPagingEnabled, query.Page, query.PageSize);

            //Get the total amount of entities
            var total = await _repository.CountAsync(spec, cancellationToken);
            //Get entity list
            var entityCollection = await _repository.ListAsync(spec, cancellationToken);

            var filterResponse = new PaginationResponse(query.Page, query.PageSize, total);

            return new GetEntitiesResponse<InspectionResponse>(
                entityCollection.Select(InspectionResponse.FromEntity).ToList(),
                filterResponse
            );
        }
    }
}
