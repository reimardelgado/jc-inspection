using Backend.Application.DTOs.Responses.InspectionResponses;

namespace Backend.Application.Queries.InspectionQueries
{
    public class ReadInspectionQueryHandler : IRequestHandler<ReadInspectionQuery,
        EntityResponse<InspectionResponse>>
    {
        private readonly IRepository<Inspection> _repository;
        private Inspection? _entity;

        public ReadInspectionQueryHandler(IRepository<Inspection> repository)
        {
            _repository = repository;
        }


        public async Task<EntityResponse<InspectionResponse>> Handle(ReadInspectionQuery query,
            CancellationToken cancellationToken)
        {
            var validateResponse = await Validate(query, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<InspectionResponse>.Error(validateResponse);
            }

            var entity = await _repository.GetByIdAsync(query.InspectionId, cancellationToken);

            var response = EntityResponse.Success(InspectionResponse.FromEntity(entity!));

            return response;
        }

        private async Task<EntityResponse<bool>> Validate(ReadInspectionQuery query,
            CancellationToken cancellationToken)
        {
            _entity = await _repository.GetByIdAsync(query.InspectionId, cancellationToken);
            return _entity is null
                ? EntityResponse<bool>.Error(MessageHandler.InspectionNotFound)
                : EntityResponse.Success(true);
        }
    }
}