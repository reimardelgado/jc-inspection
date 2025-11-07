using Backend.Application.Commands.ItemCommands;
using Backend.Application.Commands.ItemSectionCommands;
using Backend.Domain.DTOs.Requests.Zoho;
using Backend.Domain.Interfaces.Services;

namespace Backend.Application.Commands.FormTemplateCommands
{
    public class CreateFormTemplateCommandHandler : IRequestHandler<CreateFormTemplateCommand, EntityResponse<Guid>>
    {
        
        private readonly IRepository<FormTemplate> _repository;
        private readonly IRepository<ItemSection> _itemSectionRepository;
        private readonly IRepository<Item> _itemRepository;
        private readonly IZohoTemplateService _zohoTemplateService;

        public CreateFormTemplateCommandHandler(IRepository<FormTemplate> repository, 
            IRepository<ItemSection> itemSectionRepository, IRepository<Item> itemRepository,
            IZohoTemplateService zohoTemplateService)
        {
            _repository = repository;
            _itemSectionRepository = itemSectionRepository;
            _itemRepository = itemRepository;
            _zohoTemplateService = zohoTemplateService;
        }

        public async Task<EntityResponse<Guid>> Handle(CreateFormTemplateCommand command,
            CancellationToken cancellationToken)
        {
            // Repository
            var newFormTemplate = CreateFormTemplate(command);
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
            if (command.DataType == DataTypeEnum.TypeDecision)
            {
                command.Value = "false";
            }
            var entity = new Item(command.ItemSectionId, command.FormTemplateId, command.Name,
                command.DataType,command.Value, command.Comment, command.CatalogId);
            return entity;
        }

        #endregion
    }
}