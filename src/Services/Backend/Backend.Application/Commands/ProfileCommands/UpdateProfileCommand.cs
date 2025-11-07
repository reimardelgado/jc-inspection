namespace Backend.Application.Commands.ProfileCommands
{
    public class UpdateProfileCommand : IRequest<EntityResponse<bool>>
    {
        public Guid ProfileId { get; }
        public string Name { get; }
        public string Description { get; }
        public List<string> ProfilePermissions { get; }

        public UpdateProfileCommand(Guid profileId, string name, string description,
            List<string> profilePermissions)
        {
            Description = description;
            ProfileId = profileId;
            Name = name;
            ProfilePermissions = profilePermissions;
        }
    }
}