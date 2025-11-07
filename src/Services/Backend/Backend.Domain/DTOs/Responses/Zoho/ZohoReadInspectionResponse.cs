namespace Backend.Domain.DTOs.Responses.Zoho;

public class ZohoReadInspectionResponse
{
    public List<ZohoReadInspectionDataResponse> data { get; set; }
}

public class ZohoReadInspectionDataResponse
{
    public Owner Owner { get; set; }
    public object Reason_for_rejection { get; set; }
    public string Inspection_Date { get; set; }
    public string Name { get; set; }
    public Template Template { get; set; }
    public string Last_Activity_Time { get; set; }
    public object Record_Image { get; set; }
    public object Unsubscribed_Mode { get; set; }
    public Inspector Inspector { get; set; }
    public string id { get; set; }
    public object Owner_Id { get; set; }
    public string Status { get; set; }
    public string Modified_Time { get; set; }
    public string Created_Time { get; set; }
    public object Unsubscribed_Time { get; set; }
    public Deal_Number Deal_Number { get; set; }
    public string Url_Inspection_Report { get; set; }
    public bool Locked__s { get; set; }
    public object[] Tag { get; set; }

}

public class Owner
{
    public string name { get; set; }
    public string id { get; set; }
    public string email { get; set; }
}

public class Template
{
    public string name { get; set; }
    public string id { get; set; }
}

public class Inspector
{
    public string name { get; set; }
    public string id { get; set; }
}

public class Deal_Number
{
    public string name { get; set; }
    public string id { get; set; }
}

