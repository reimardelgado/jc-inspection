
namespace Backend.Application.Commands.ZohoTokenCommands
{
    public class CreateZohoTokenCommand : IRequest<EntityResponse<Guid>>
    {
        public string AccessToken { get; set; }
        public string Status { get; set; }

        public CreateZohoTokenCommand(string accessToken, string status)
        {
            AccessToken = accessToken;
            Status = status;
        }
    }
}