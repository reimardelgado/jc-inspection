using Newtonsoft.Json;

namespace Backend.Domain.DTOs.Requests.Zoho;

public class ZohoTemplate
{
    [JsonProperty(PropertyName = "id")]
    public string? Id { get; set; }
    [JsonProperty(PropertyName = "Id_Template")]
    public string IdTemplate { get; set; }
    [JsonProperty(PropertyName = "Name")]
    public string NameTemplate { get; set; }
    [JsonProperty(PropertyName = "Description")]
    public string DescriptionTemplate { get; set; }
    [JsonProperty(PropertyName = "Status")]
    public string StatusTemplate { get; set; }

    public ZohoTemplate(string idTemplate, string nameTemplate, string descriptionTemplate, string statusTemplate)
    {
        IdTemplate = idTemplate;
        NameTemplate = nameTemplate;
        DescriptionTemplate = descriptionTemplate;
        StatusTemplate = statusTemplate;
    }
}

public class ZohoTemplateData
{
    [JsonProperty(PropertyName = "data")]
    public List<ZohoTemplate> Data { get; set; }

    public ZohoTemplateData()
    {
        Data = new List<ZohoTemplate>();
    }
}