namespace Backend.Application.Commands.InspectionCommands
{
    public class UpdateInspectionResultCommand : IRequest<EntityResponse<bool>>
    {
        public Guid InspectionResultId { get; set; }
        public Guid InspectionId { get; set; }
        public Guid FormTemplateId { get; set; }
        public string? IdZoho { get; set; }
        public Guid? SectionId { get; set; }
        public string? SectionName { get; set; }
        public Guid? ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? ItemDatatype { get; set; }
        public string? ItemValue { get; set; }
        public Guid? CatalogId { get; set; }
        public string? ItemComment { get; set; }

        public UpdateInspectionResultCommand(Guid inspectionResultId, Guid inspectionId, Guid formTemplateId,
            string? idZoho, Guid? sectionId, string? sectionName, Guid? itemId, string? itemName, 
            string? itemDatatype, string? itemValue, Guid? catalogId, string? itemComment)
        {
            InspectionResultId = inspectionResultId;
            InspectionId = inspectionId;
            FormTemplateId = formTemplateId;
            IdZoho = idZoho;
            SectionId = sectionId;
            SectionName = sectionName;
            ItemId = itemId;
            ItemName = itemName;
            ItemDatatype = itemDatatype;
            ItemValue = itemValue;
            CatalogId = catalogId;
            ItemComment = itemComment;
        }
    }
}