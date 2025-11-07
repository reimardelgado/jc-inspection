using Backend.Domain.SeedWork;

namespace Backend.Domain.Entities;

public class Inspection : BaseEntity
{
    public string Name { get; set; }
    public string DealNumber { get; set; }
    public Guid InspectorId { get; set; }
    public User? Inspector { get; set; }
    public string? ZohoOwnerId { get; set; }
    public string? ZohoOwnerName { get; set; }
    public DateTime InspectionDate { get; set; }
    public Guid FormTemplateId { get; set; }
    public FormTemplate? FormTemplate { get; set; }
    public string InspectionStatus { get; set; } = InspectionStatusEnum.Created;
    public string? IdZoho { get; set; }
    //Address Deals
    public string AddressProjectCity { get; set; }
    public string AddressProjectState { get; set; }
    public string AddressProjectStreet { get; set; }
    public string AddressProjectZip { get; set; }
    public string? ReasonRejection { get; set; }
    public DateTime? DateReasonRejection { get; set; }

    public Inspection(string name, string dealNumber, Guid inspectorId,
        string? zohoOwnerId, string? zohoOwnerName, DateTime inspectionDate,
        Guid formTemplateId, string addressProjectCity, string addressProjectState,
        string addressProjectStreet, string addressProjectZip, string? reasonRejection,
        DateTime? dateReasonRejection)
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
        ReasonRejection = reasonRejection;
        DateReasonRejection = dateReasonRejection;
    }
}