namespace Backend.Application.Commands.ZohoTokenCommands
{
    public class DeleteZohoTokenCommand : IRequest<EntityResponse<bool>>
    {
        public Guid ZohoTokenId { get; }

        public DeleteZohoTokenCommand(Guid zohoTokenId)
        {
            ZohoTokenId = zohoTokenId;
        }
    }
}