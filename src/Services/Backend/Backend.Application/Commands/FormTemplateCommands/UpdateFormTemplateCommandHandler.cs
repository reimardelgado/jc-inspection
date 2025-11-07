using Backend.Application.Commands.ItemCommands;
using Backend.Application.Commands.ItemSectionCommands;
using Backend.Domain.DTOs.Requests.Zoho;
using Backend.Domain.Interfaces.Services;

namespace Backend.Application.Commands.FormTemplateCommands
{
    public class UpdateFormTemplateCommandHandler : IRequestHandler<UpdateFormTemplateCommand, EntityResponse<Guid>>
    {
        private readonly IRepository<FormTemplate> _repository;
        private readonly IZohoTemplateService _zohoTemplateService;
        private readonly IRepository<ItemSection> _itemSectionRepository;
        private readonly IRepository<Item> _itemRepository;
        private FormTemplate? _entity;

        public UpdateFormTemplateCommandHandler(IRepository<FormTemplate> repository, IZohoTemplateService zohoTemplateService,
            IRepository<ItemSection> itemSectionRepository, IRepository<Item> itemRepository)
        {
            _repository = repository;
            _zohoTemplateService = zohoTemplateService;
            _itemSectionRepository = itemSectionRepository;
            _itemRepository = itemRepository;
        }

        public async Task<EntityResponse<Guid>> Handle(UpdateFormTemplateCommand command,
            CancellationToken cancellationToken)
        {
            var validateResponse = await Validations(command, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<Guid>.Error(validateResponse);
            }
            var dbFormTemplate = validateResponse.Value;
            dbFormTemplate.Name = $"{dbFormTemplate.Name} - {DateTime.Now.ToString("u")}";
            await _repository.UpdateAsync(dbFormTemplate, cancellationToken);
            await _zohoTemplateService.UpdateInZoho(dbFormTemplate.IdZoho,dbFormTemplate.Id.ToString(), cancellationToken);
            
            // Create new version template
            var newFormTemplate = CreateFormTemplate(new CreateFormTemplateCommand(command.Name, 
                command.Description, StatusEnum.Active, command.ItemSectionsDtos));
            await _repository.AddAsync(newFormTemplate, cancellationToken);
            
            //Item section
            foreach (var obj in command.ItemSectionsDtos)
            {
                var itemSectionCommand = new CreateItemSectionCommand(obj.Name, newFormTemplate.Id, command.Name, StatusEnum.Active);
                var itemSectionEntity = CreateItemSection(itemSectionCommand);
                await _itemSectionRepository.AddAsync(itemSectionEntity, cancellationToken);
                
                //Items
                foreach (var it in obj.Items)
                {
                    var catalogId = !string.IsNullOrEmpty(it.ItemCatalogId) ? Guid.Parse(it.ItemCatalogId) : (Guid?)null;
                    var itemCommmand = new CreateItemCommand(itemSectionEntity.Id, newFormTemplate.Id, it.ItemName,
                        it.ItemDatatype, string.Empty, string.Empty, catalogId, StatusEnum.Active);
                    var itemEntity = CreateItem(itemCommmand);
                    await _itemRepository.AddAsync(itemEntity, cancellationToken);
                }
            }
            //Creating Template TC in Zoho
            var idZoho = await _zohoTemplateService.CreateInZoho(new ZohoTemplate(newFormTemplate.Id.ToString(), newFormTemplate.Name!,
                newFormTemplate.Description!, newFormTemplate.Status), cancellationToken);
            if (!string.IsNullOrEmpty(idZoho))
            {
                newFormTemplate.IdZoho = idZoho;
                await _repository.UpdateAsync(newFormTemplate, cancellationToken);
            }

            return EntityResponse.Success(newFormTemplate.Id);
        }
        
        #region Private Methods

        private async Task<EntityResponse<FormTemplate>> Validations(UpdateFormTemplateCommand command,
            CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(command.FormTemplateId, cancellationToken);
            return entity is null
                ? EntityResponse<FormTemplate>.Error(MessageHandler.FormTemplateNotFound)
                : EntityResponse.Success(entity);
        }
        
        private FormTemplate CreateFormTemplate(CreateFormTemplateCommand command)
        {
            var entity = new FormTemplate(command.Name, command.Description);
            return entity;
        }
        
        private ItemSection CreateItemSection(CreateItemSectionCommand command)
        {
            var entity = new ItemSection(command.Name, command.FormTemplateId, command.FormTemplateName);
            return entity;
        } 
        
        private Item CreateItem(CreateItemCommand command)
        {
            var entity = new Item(command.ItemSectionId, command.FormTemplateId, command.Name,
                command.DataType,command.Value, command.Comment, command.CatalogId);
            return entity;
        }

        #endregion
    }
}