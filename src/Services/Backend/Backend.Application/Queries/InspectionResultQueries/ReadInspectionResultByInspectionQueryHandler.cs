using Backend.Application.DTOs.Responses.CatalogValueResponses;
using Backend.Application.DTOs.Responses.InspectionResultResponses;
using Backend.Application.Specifications.CatalogValueSpecs;
using Backend.Application.Specifications.InspectionResultSpecs;

namespace Backend.Application.Queries.InspectionResultQueries
{
    public class ReadInspectionResultByInspectionQueryHandler : IRequestHandler<ReadInspectionResultByInspectionQuery,
        EntityResponse<List<InspectionResultResponse>>>
    {
        private readonly IRepository<InspectionResult> _repository;
        private readonly IRepository<CatalogValue> _catalogValueRepository;
        private InspectionResult? _entity;

        public ReadInspectionResultByInspectionQueryHandler(IRepository<InspectionResult> repository, IRepository<CatalogValue> catalogValueRepository)
        {
            _repository = repository;
            _catalogValueRepository = catalogValueRepository;
        }


        public async Task<EntityResponse<List<InspectionResultResponse>>> Handle(ReadInspectionResultByInspectionQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new InspectionResultByResultSpec(query.InspectionId, query.FormTemplateId);
            var entityCollection = await _repository.ListAsync(spec, cancellationToken);
            if (!entityCollection.Any())
            {
                return EntityResponse<List<InspectionResultResponse>>.Error(MessageHandler.InspectionResultNotFound);
            }
            
            var response = entityCollection.Select(InspectionResultResponse.FromEntity).ToList();   
            foreach (var item in response)
            {
                if (item.CatalogId != null)
                {
                    var specCatalog = new CatalogValueSpec(item.CatalogId.Value);
                    //Get the total amount of entities
                    var catalogValues = await _catalogValueRepository.ListAsync(specCatalog, cancellationToken);
                    var catalogValueResponses = catalogValues.Select(CatalogValueResponse.FromEntity).ToList();
                    item.CatalogValues = catalogValueResponses;
                }
            }

            return response;
        }
    }
}