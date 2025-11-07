namespace Backend.Domain.DTOs.Responses.Zoho;

public class ZohoCreateRecordResponse
{
    public List<DataZohoResponse> data { get; set; }
}

public class CreatedBy
{
    public string name { get; set; }
    public string id { get; set; }
}

public class DataZohoResponse
{
    public string code { get; set; }
    public DataZohoResponseDetails details { get; set; }
    public string message { get; set; }
    public string status { get; set; }
}

public class DataZohoResponseDetails
{
    public DateTime Modified_Time { get; set; }
    public ModifiedBy Modified_By { get; set; }
    public DateTime Created_Time { get; set; }
    public string id { get; set; }
    public CreatedBy Created_By { get; set; }
}

public class ModifiedBy
{
    public string name { get; set; }
    public string id { get; set; }
}