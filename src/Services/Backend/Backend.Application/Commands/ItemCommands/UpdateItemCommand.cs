namespace Backend.Application.Commands.ItemCommands
{
    public class UpdateItemCommand : IRequest<EntityResponse<bool>>
    {
        public Guid ItemId { get; }
        public Guid ItemSectionId { get; set; }
        public Guid FormTemplateId { get; set; }
        public string? Name { get; set; }
        public string? DataType { get; set; }
        public string? Value { get; set; }
        public string? Comment { get; set; }
        public Guid? CatalogId { get; set; }

        public UpdateItemCommand(Guid itemId, Guid itemSectionId, Guid formTemplateId, string? name, string? dataType,
            string? value, string? comment, Guid? catalogId)
        {
            ItemId = itemId;
            ItemSectionId = itemSectionId;
            FormTemplateId = formTemplateId;
            Name = name;
            DataType = dataType;
            Value = value;
            Comment = comment;
            CatalogId = catalogId;
        }
    }
}