using Backend.Application.DTOs.Responses.InspectionResponses;
using Backend.Application.Specifications.InspectionSpecs;

namespace Backend.Application.Queries.InspectionQueries
{
    public class ReadAllInspectionsQueryHandler : IRequestHandler<ReadAllInspectionsQuery,
        EntityResponse<List<InspectionResponse>>>
    {
        private readonly IRepository<Inspection> _repository;

        public ReadAllInspectionsQueryHandler(IRepository<Inspection> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<List<InspectionResponse>>> Handle(ReadAllInspectionsQuery query,
            CancellationToken cancellationToken)
        {
            var spec = new InspectionSpec(query.UserId, null);

            //Get the total amount of entities
            var total = await _repository.CountAsync(spec, cancellationToken);

            //Get entity list
            var entityCollection = await _repository.ListAsync(spec, cancellationToken);

            foreach (var item in entityCollection)
            {
                if (item.InspectionStatus == InspectionStatusEnum.Created && item.InspectionDate.DayOfYear <= DateTime.Now.DayOfYear)
                {
                    item.InspectionStatus = InspectionStatusEnum.InAction;
                    await _repository.UpdateAsync(item, cancellationToken);
                }
            }

            return entityCollection.Select(InspectionResponse.FromEntity).ToList();
        }
    }
}