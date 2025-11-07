
namespace Backend.Application.Commands.InspectionCommands
{
    public class CreateInspectionCommand : IRequest<EntityResponse<Guid>>
    {
        public string Name { get; set; }
        public string DealNumber { get; set; }
        public Guid InspectorId { get; set; }
        public string? ZohoOwnerId { get; set; }
        public string? ZohoOwnerName { get; set; }
        public DateTime InspectionDate { get; set; }
        public Guid FormTemplateId { get; set; }
        public string AddressProjectCity { get; set; }
        public string AddressProjectState { get; set; }
        public string AddressProjectStreet { get; set; }
        public string AddressProjectZip { get; set; }

        public CreateInspectionCommand(string name, string dealNumber, Guid inspectorId,
            string? zohoOwnerId, string? zohoOwnerName, DateTime inspectionDate, Guid formTemplateId,
            string addressProjectCity, string addressProjectState, string addressProjectStreet,
            string addressProjectZip)
        {
            Name = name;
            DealNumber = dealNumber;
            InspectorId = inspectorId;
            ZohoOwnerId = zohoOwnerId;
            ZohoOwnerName = zohoOwnerName;
            InspectionDate = inspectionDate;
            FormTemplateId = formTemplateId;
            AddressProjectCity = addressProjectCity;
            AddressProjectState = addressProjectState;
            AddressProjectStreet = addressProjectStreet;
            AddressProjectZip = addressProjectZip;
        }
    }
}