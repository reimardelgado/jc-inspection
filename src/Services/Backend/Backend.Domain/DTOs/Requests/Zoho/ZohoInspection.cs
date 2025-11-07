using Backend.Domain.DTOs.Responses.Zoho;
using Newtonsoft.Json;

namespace Backend.Domain.DTOs.Requests.Zoho;

public class ZohoInspection
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    [JsonProperty(PropertyName = "Id_Inspection")]
    public string? IdInspection { get; set; }
    [JsonProperty(PropertyName = "Name")]
    public string? Name { get; set; }
    [JsonProperty(PropertyName = "Deal_Number")]
    public Deal_Number DealNumber { get; set; }
    [JsonProperty(PropertyName = "Inspection_Date")]
    public string InspectionDate { get; set; }
    [JsonProperty(PropertyName = "Inspector")]
    public Inspector Inspector { get; set; }
    [JsonProperty(PropertyName = "Template")]
    public Template Template { get; set; }
    [JsonProperty(PropertyName = "Status")]
    public string Status { get; set; }
    [JsonProperty(PropertyName = "Url_Inspection_Report")]
    public string? UrlInspectionReport { get; set; }
    
}

public class ZohoInspectionData
{
    [JsonProperty(PropertyName = "data")]
    public List<ZohoInspection> Data { get; set; }

    public ZohoInspectionData()
    {
        Data = new List<ZohoInspection>();
    }
}