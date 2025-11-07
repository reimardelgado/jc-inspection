using Backend.Application.Commands.InspectionCommands;
using Backend.Domain.SeedWork;

namespace Backend.API.DTOs.Requests.InspectionRequests;

public class CreateInspectionRequest
{
    public string Name { get; set; }
    public string DealNumber { get; set; }
    public string InspectorId { get; set; }
    public string? ZohoOwnerId { get; set; }
    public string? ZohoOwnerName { get; set; }
    public DateTime InspectionDate { get; set; }
    public string FormTemplateId { get; set; }
    public string AddressProjectCity { get; set; }
    public string AddressProjectState { get; set; }
    public string AddressProjectStreet { get; set; }
    public string AddressProjectZip { get; set; }

    public CreateInspectionRequest(string name, string dealNumber, string inspectorId, string? zohoOwnerId, 
        string? zohoOwnerName, DateTime inspectionDate, string formTemplateId, string addressProjectCity,
        string addressProjectState, string addressProjectStreet, string addressProjectZip)
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

    public CreateInspectionCommand ToApplicationRequest()
    {
        return new CreateInspectionCommand(Name, DealNumber, Guid.Parse(InspectorId),
            ZohoOwnerId, ZohoOwnerName, InspectionDate, Guid.Parse(FormTemplateId),
            AddressProjectCity, AddressProjectState, AddressProjectStreet, AddressProjectZip);
    }
}