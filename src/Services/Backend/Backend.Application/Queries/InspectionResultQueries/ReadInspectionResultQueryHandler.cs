using Backend.Application.DTOs.Responses.InspectionResultResponses;

namespace Backend.Application.Queries.InspectionResultQueries
{
    public class ReadInspectionResultQueryHandler : IRequestHandler<ReadInspectionResultQuery,
        EntityResponse<InspectionResultResponse>>
    {
        private readonly IRepository<InspectionResult> _repository;
        private InspectionResult? _entity;

        public ReadInspectionResultQueryHandler(IRepository<InspectionResult> repository)
        {
            _repository = repository;
        }


        public async Task<EntityResponse<InspectionResultResponse>> Handle(ReadInspectionResultQuery query,
            CancellationToken cancellationToken)
        {
            var validateResponse = await Validate(query, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<InspectionResultResponse>.Error(validateResponse);
            }

            var entity = await _repository.GetByIdAsync(query.InspectionResultId, cancellationToken);

            var response = EntityResponse.Success(InspectionResultResponse.FromEntity(entity!));

            return response;
        }

        private async Task<EntityResponse<bool>> Validate(ReadInspectionResultQuery query,
            CancellationToken cancellationToken)
        {
            _entity = await _repository.GetByIdAsync(query.InspectionResultId, cancellationToken);
            return _entity is null
                ? EntityResponse<bool>.Error(MessageHandler.InspectionNotFound)
                : EntityResponse.Success(true);
        }
    }
}