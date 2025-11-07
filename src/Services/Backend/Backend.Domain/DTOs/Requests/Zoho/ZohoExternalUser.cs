using Newtonsoft.Json;

namespace Backend.Domain.DTOs.Requests.Zoho;

public class ZohoExternalUser
{
    [JsonProperty(PropertyName = "id")]
    public string? Id { get; set; }
    [JsonProperty(PropertyName = "Id_User")]
    public string IdUser { get; set; }
    [JsonProperty(PropertyName = "Name")]
    public string Username { get; set; }
    [JsonProperty(PropertyName = "First_Name")]
    public string FirstName { get; set; }
    [JsonProperty(PropertyName = "Last_Name")]
    public string LastName { get; set; }
    [JsonProperty(PropertyName = "Email")]
    public string Email { get; set; }
    [JsonProperty(PropertyName = "Phone")]
    public string? Phone { get; set; }
    [JsonProperty(PropertyName = "Status")]
    public string Status { get; set; }

    public ZohoExternalUser(string idUser, string username, string firstName, string lastName, string email, string? phone, string status)
    {
        IdUser = idUser;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Status = status;
    }
}

public class ZohoExternalUserData
{
    [JsonProperty(PropertyName = "data")]
    public List<ZohoExternalUser> Data { get; set; }

    public ZohoExternalUserData()
    {
        Data = new List<ZohoExternalUser>();
    }
}