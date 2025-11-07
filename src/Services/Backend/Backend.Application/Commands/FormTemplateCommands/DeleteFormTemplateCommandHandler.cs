using Backend.Application.Commands.ItemCommands;
using Backend.Application.Commands.ItemSectionCommands;
using Backend.Application.Specifications.ItemSectionSpecs;
using Backend.Application.Specifications.ItemSpecs;
using Backend.Domain.DTOs.Requests.Zoho;
using Backend.Domain.Interfaces.Services;

namespace Backend.Application.Commands.FormTemplateCommands
{
    public class DeleteFormTemplateCommandHandler : IRequestHandler<DeleteFormTemplateCommand, EntityResponse<bool>>
    {
        private readonly IRepository<FormTemplate> _repository;
        private readonly IZohoTemplateService _zohoTemplateService;
        private readonly IRepository<ItemSection> _itemSectionRepository;
        private readonly IRepository<Item> _itemRepository;
        private readonly IMediator _mediator;

        public DeleteFormTemplateCommandHandler(IRepository<FormTemplate> repository, IZohoTemplateService zohoTemplateService,
            IRepository<ItemSection> itemSectionRepository, IRepository<Item> itemRepository, IMediator mediator)
        {
            _repository = repository;
            _zohoTemplateService = zohoTemplateService;
            _itemSectionRepository = itemSectionRepository;
            _itemRepository = itemRepository;
            _mediator = mediator;
        }

        public async Task<EntityResponse<bool>> Handle(DeleteFormTemplateCommand command, CancellationToken cancellationToken)
        {
            var validateResponse = await Validations(command, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            var dbFormTemplate = validateResponse.Value;
            await UpdateFormTemplate(dbFormTemplate!, cancellationToken);

            return EntityResponse.Success(true);
        }
        
        #region Private Methods

        private async Task<EntityResponse<FormTemplate>> Validations(DeleteFormTemplateCommand command,
            CancellationToken cancellationToken)
        {
            var ent = await _repository.GetByIdAsync(command.FormTemplateId, cancellationToken);

            return ent is null
                ? EntityResponse<FormTemplate>.Error(MessageHandler.FormTemplateNotFound)
                : EntityResponse.Success(ent);
        }

        private async Task UpdateFormTemplate(FormTemplate entity, CancellationToken cancellationToken)
        {
            entity.Status = StatusEnum.Deleted;
            await _repository.UpdateAsync(entity, cancellationToken);
            
            //Update Status in Zoho
            var zohoModel = new ZohoTemplate(entity.Id.ToString(), entity.Name!, entity.Description!, StatusEnum.Deleted);
            zohoModel.Id = entity.IdZoho;
            await _zohoTemplateService.UpdateInZoho(entity.IdZoho,entity.Id.ToString(), cancellationToken);
            
            //Deleted in cascade
            var itemSectionSpec = new ItemSectionSpec(entity.Id);
            var itemSections = await _itemSectionRepository.ListAsync(itemSectionSpec, cancellationToken);
            foreach (var itemSection in itemSections)
            {
                var itemSpec = new ItemSpec(entity.Id, itemSection.Id, string.Empty);
                var items = await _itemRepository.ListAsync(itemSpec, cancellationToken);
                foreach (var item in items)
                {
                    await _mediator.Send(new DeleteItemCommand(item.Id), cancellationToken);
                }
                await _mediator.Send(new DeleteItemSectionCommand(itemSection.Id), cancellationToken);
            }
        }

        #endregion
    }
}