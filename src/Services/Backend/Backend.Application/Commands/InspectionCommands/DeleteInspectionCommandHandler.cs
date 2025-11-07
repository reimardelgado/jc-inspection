namespace Backend.Application.Commands.InspectionCommands
{
    public class DeleteInspectionCommandHandler : IRequestHandler<DeleteInspectionCommand, EntityResponse<bool>>
    {
        private readonly IRepository<Inspection> _repository;

        public DeleteInspectionCommandHandler(IRepository<Inspection> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<bool>> Handle(DeleteInspectionCommand command, CancellationToken cancellationToken)
        {
            var validateResponse = await Validations(command, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            var dbInspection = validateResponse.Value;
            await UpdateInspection(dbInspection!, cancellationToken);

            return EntityResponse.Success(true);
        }
        
        #region Private Methods

        private async Task<EntityResponse<Inspection>> Validations(DeleteInspectionCommand command,
            CancellationToken cancellationToken)
        {
            var area = await _repository.GetByIdAsync(command.InspectionId, cancellationToken);

            return area is null
                ? EntityResponse<Inspection>.Error(MessageHandler.InspectionNotFound)
                : EntityResponse.Success(area);
        }

        private async Task UpdateInspection(Inspection entity, CancellationToken cancellationToken)
        {
            entity.InspectionStatus = InspectionStatusEnum.Deleted;
            await _repository.UpdateAsync(entity, cancellationToken);
        }

        #endregion
    }
}