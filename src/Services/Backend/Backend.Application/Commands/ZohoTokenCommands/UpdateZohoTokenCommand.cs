namespace Backend.Application.Commands.ZohoTokenCommands
{
    public class UpdateZohoTokenCommand : IRequest<EntityResponse<bool>>
    {
        public Guid ZohoTokenId { get; }
        public string AccessToken { get; set; }
        public string Status { get; set; }

        public UpdateZohoTokenCommand(Guid zohoTokenId, string accessToken, string status)
        {
            ZohoTokenId = zohoTokenId;
            AccessToken = accessToken;
            Status = status;
        }
    }
}