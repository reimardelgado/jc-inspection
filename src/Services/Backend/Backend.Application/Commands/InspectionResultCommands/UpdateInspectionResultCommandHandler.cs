namespace Backend.Application.Commands.InspectionCommands
{
    public class UpdateInspectionResultCommandHandler : IRequestHandler<UpdateInspectionResultCommand, EntityResponse<bool>>
    {
        private readonly IRepository<InspectionResult> _repository;
        private InspectionResult? _entity;

        public UpdateInspectionResultCommandHandler(IRepository<InspectionResult> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<bool>> Handle(UpdateInspectionResultCommand command,
            CancellationToken cancellationToken)
        {
            var validateResponse = await Validations(command, cancellationToken);

            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            var dbInspection = await _repository.GetByIdAsync(command.InspectionResultId, cancellationToken);

            if (dbInspection == null)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            await UpdateInspectionResult(command, dbInspection, cancellationToken);

            return EntityResponse.Success(true);
        }
        
        #region Private Methods

        private async Task<EntityResponse<bool>> Validations(UpdateInspectionResultCommand command,
            CancellationToken cancellationToken)
        {
            _entity = await _repository.GetByIdAsync(command.InspectionResultId, cancellationToken);
            return _entity is null
                ? EntityResponse<bool>.Error(MessageHandler.InspectionNotFound)
                : EntityResponse.Success(true);
        }

        private async Task UpdateInspectionResult(UpdateInspectionResultCommand command, InspectionResult entity,
            CancellationToken cancellationToken)
        {
            entity.InspectionId = command.InspectionId;
            entity.FormTemplateId = command.FormTemplateId;
            entity.IdZoho = command.IdZoho;
            entity.SectionId = command.SectionId;
            entity.SectionName = command.SectionName;
            entity.ItemId = command.ItemId;
            entity.ItemName = command.ItemName;
            entity.ItemDatatype = command.ItemDatatype;
            entity.ItemValue = command.ItemValue;
            entity.CatalogId = command.CatalogId;
            entity.ItemComment = command.ItemComment;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        #endregion
    }
}