using Backend.Application.DTOs.Responses.FormTemplateResponses;

namespace Backend.Application.DTOs.Responses.InspectionResponses
{
    public class InspectionResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DealNumber { get; set; }
        public Guid? InspectorId { get; set; }
        public string? InspectorName { get; set; }
        public string? ZohoOwnerId { get; set; }
        public string? ZohoOwnerName { get; set; }
        public DateTime InspectionDate { get; set; }
        public Guid? FormTemplateId { get; set; }
        public string? FormTemplateName { get; set; }
        public string AddressProjectCity { get; set; }
        public string AddressProjectState { get; set; }
        public string AddressProjectStreet { get; set; }
        public string AddressProjectZip { get; set; }
        public string? ReasonRejection { get; set; }
        public string Status { get; set; }

        public InspectionResponse(Guid id, string name, string dealNumber, Guid? inspectorId, string? inspectorName,
            string? zohoOwnerId, string? zohoOwnerName, DateTime inspectionDate, Guid? formTemplateId,
            string? formTemplateName, string addressProjectCity, string addressProjectState, string addressProjectStreet,
            string addressProjectZip, string? reasonRejection, string status)
        {
            Id = id;
            Name = name;
            DealNumber = dealNumber;
            InspectorId = inspectorId;
            InspectorName = inspectorName;
            ZohoOwnerId = zohoOwnerId;
            ZohoOwnerName = zohoOwnerName;
            InspectionDate = inspectionDate;
            FormTemplateId = formTemplateId;
            FormTemplateName = formTemplateName;
            AddressProjectCity = addressProjectCity;
            AddressProjectState = addressProjectState;
            AddressProjectStreet = addressProjectStreet;
            AddressProjectZip = addressProjectZip;
            Status = status;
            ReasonRejection = reasonRejection;
        }


        public static InspectionResponse FromEntity(Inspection entity)
        {
            var inspector = InspectorDto.FromEntity(entity.Inspector!);
            var formTemplate = FormTemplateDto.FromEntity(entity.FormTemplate!);
            return new InspectionResponse(entity.Id, entity.Name!, entity.DealNumber, inspector.Id, inspector.Name, entity.ZohoOwnerId,
                entity.ZohoOwnerName, entity.InspectionDate, formTemplate.Id, formTemplate.Name, entity.AddressProjectCity,
                entity.AddressProjectState, entity.AddressProjectStreet, entity.AddressProjectZip, entity.ReasonRejection, entity.InspectionStatus);
        }
    }

    public record InspectorDto(Guid Id, string Username, string Name, string Email)
    {
        public static InspectorDto FromEntity(User inspector)
        {
            return new InspectorDto(inspector.Id, inspector.Username, inspector.FullName!, inspector.Email);
        }
    }

    public record FormTemplateDto(Guid Id, string Name, string Status)
    {
        public static FormTemplateDto FromEntity(FormTemplate formTemplate)
        {
            return new FormTemplateDto(formTemplate.Id, formTemplate.Name!, formTemplate.Status);
        }
    }
}