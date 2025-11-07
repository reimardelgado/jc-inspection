using Backend.Application.DTOs.Responses.InspectionResultResponses;
using Backend.Application.Specifications.InspectionResultSpecs;
using Backend.Application.Specifications.InspectionSpecs;

namespace Backend.Application.Queries.InspectionResultQueries
{
    public class ReadAllInspectionResultsQueryHandler : IRequestHandler<ReadAllInspectionResultsQuery,
        EntityResponse<List<InspectionResultResponse>>>
    {
        private readonly IRepository<InspectionResult> _repository;

        public ReadAllInspectionResultsQueryHandler(IRepository<InspectionResult> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<List<InspectionResultResponse>>> Handle(ReadAllInspectionResultsQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new InspectionResultSpec();

            //Get entity list
            var entityCollection = await _repository.ListAsync(spec, cancellationToken);

            return entityCollection.Select(InspectionResultResponse.FromEntity).ToList();
        }
    }
}