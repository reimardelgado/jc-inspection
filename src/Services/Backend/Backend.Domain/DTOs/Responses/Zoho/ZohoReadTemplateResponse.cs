namespace Backend.Domain.DTOs.Responses.Zoho;

public class ZohoReadTemplateResponse
{
    public List<ZohoReadTemplateDataResponse> data { get; set; }
}

public class ZohoReadTemplateDataResponse
{
    public Owner Owner { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public object Last_Activity_Time { get; set; }
    public object Unsubscribed_Mode { get; set; }
    public string id { get; set; }
    public string Status { get; set; }
    public string Modified_Time { get; set; }
    public string Created_Time { get; set; }
    public object Unsubscribed_Time { get; set; }
    public bool Locked__s { get; set; }
    public object[] Tag { get; set; }
    public string Id_Template { get; set; }
}


