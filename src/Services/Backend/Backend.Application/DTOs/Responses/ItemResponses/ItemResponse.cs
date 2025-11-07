namespace Backend.Application.DTOs.Responses.ItemResponses
{
    public class ItemResponse
    {
        public Guid Id { get; set; }
        public Guid ItemSectionId { get; set; }
        public string ItemSectionName { get; set; }
        public Guid FormTemplateId { get; set; }
        public string FormTemplateName { get; set; }
        public string? Name { get; set; }
        public string? DataType { get; set; }
        public string? Value { get; set; }
        public string? Comment { get; set; }
        public Guid? CatalogId { get; set; }
        public string CatalogName { get; set; }
        public string Status { get; set; }

        public ItemResponse(Guid id, Guid itemSectionId, string itemSectionName, Guid formTemplateId,
            string formTemplateName, string? name, string? dataType, string? value, string? comment,
            Guid? catalogId, string catalogName, string status)
        {
            Id = id;
            ItemSectionId = itemSectionId;
            ItemSectionName = itemSectionName;
            FormTemplateId = formTemplateId;
            FormTemplateName = formTemplateName;
            Name = name;
            DataType = dataType;
            Value = value;
            Comment = comment;
            CatalogId = catalogId;
            CatalogName = catalogName;
            Status = status;
        }

        public static ItemResponse FromEntity(Item entity)
        {
            var catalogName = entity.Catalog != null ? entity.Catalog.Name : string.Empty; 
            return new ItemResponse(entity.Id, entity.ItemSectionId, entity.ItemSection!.Name, entity.FormTemplateId,
                 entity.FormTemplate!.Name!, entity.Name!, entity.DataType, entity.Value,
                entity.Comment, entity.CatalogId, catalogName, entity.Status);
        }
    }
}