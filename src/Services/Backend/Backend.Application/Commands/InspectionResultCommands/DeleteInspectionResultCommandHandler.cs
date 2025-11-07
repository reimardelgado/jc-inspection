namespace Backend.Application.Commands.InspectionCommands
{
    public class DeleteInspectionResultCommandHandler : IRequestHandler<DeleteInspectionResultCommand, EntityResponse<bool>>
    {
        private readonly IRepository<InspectionResult> _repository;

        public DeleteInspectionResultCommandHandler(IRepository<InspectionResult> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<bool>> Handle(DeleteInspectionResultCommand command, CancellationToken cancellationToken)
        {
            var validateResponse = await Validations(command, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            var dbInspection = validateResponse.Value;
            await UpdateInspectionResult(dbInspection!, cancellationToken);

            return EntityResponse.Success(true);
        }
        
        #region Private Methods

        private async Task<EntityResponse<InspectionResult>> Validations(DeleteInspectionResultCommand command,
            CancellationToken cancellationToken)
        {
            var area = await _repository.GetByIdAsync(command.InspectionResultId, cancellationToken);

            return area is null
                ? EntityResponse<InspectionResult>.Error(MessageHandler.InspectionNotFound)
                : EntityResponse.Success(area);
        }

        private async Task UpdateInspectionResult(InspectionResult entity, CancellationToken cancellationToken)
        {
            entity.InspectionResultStatus = InspectionStatusEnum.Deleted;
            await _repository.UpdateAsync(entity, cancellationToken);
        }

        #endregion
    }
}