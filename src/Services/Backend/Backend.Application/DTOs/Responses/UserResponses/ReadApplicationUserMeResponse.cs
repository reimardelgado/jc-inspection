namespace Backend.Application.DTOs.Responses.UserResponses
{
    public class ReadUserMeResponse
    {
        public Guid UserId { get; }
        public string UserName { get; }
        public string Email { get; }
        public string? FullName { get; }
        public string? Phone { get; }
        public string? Identification { get; }
        public string Token { get; set; }
        public IEnumerable<string> Permissions { get; }

        public ReadUserMeResponse(Guid userId, string userName, string email, string? fullName,
            string? phone, string? identification, string token, IEnumerable<string> permissions)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            FullName = fullName;
            Phone = phone;
            Identification = identification;
            Token = token;
            Permissions = permissions;
        }
    }
}