namespace Backend.Application.Commands.ProfileCommands
{
    public class UpdateProfileStatusCommand : IRequest<EntityResponse<bool>>
    {
        public Guid ProfileId { get; }
        public string Status { get; }

        public UpdateProfileStatusCommand(Guid profileId, string status)
        {
            ProfileId = profileId;
            Status = status;
        }
    }
}