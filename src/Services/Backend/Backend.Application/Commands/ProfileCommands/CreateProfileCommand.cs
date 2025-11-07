namespace Backend.Application.Commands.ProfileCommands
{
    public class CreateProfileCommand : IRequest<EntityResponse<Guid>>
    {
        public string Name { get; }
        public string Description { get; }
        public List<string> ProfilePermissions { get; }

        public CreateProfileCommand(string name, string description,
            List<string> profilePermissions)
        {
            Description = description;
            Name = name;
            ProfilePermissions = profilePermissions;
        }
    }
}