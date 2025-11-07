using Backend.Application.Specifications.ProfileSpecs;

namespace Backend.Application.Commands.ProfileCommands
{
    public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, EntityResponse<Guid>>
    {
        private readonly IRepository<Profile> _repository;

        public CreateProfileCommandHandler(IRepository<Profile> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<Guid>> Handle(CreateProfileCommand command, CancellationToken cancellationToken)
        {
            var spec = new ProfileSpec(null, command.Name);
            var profileDb = await _repository.GetBySpecAsync(spec, cancellationToken);

            if (profileDb != null)
            {
                return EntityResponse<Guid>.Error(EntityResponseUtils.GenerateMsg(MessageHandler.ProfileAlreadyExit));
            }

            var profile = new Profile(command.Name, command.Description);

            command.ProfilePermissions
                .Where(s => Guid.TryParse(s, out _))
                .Select(Guid.Parse)
                .ToList()
                .ForEach(profile.AddProfilePermission);

            await _repository.AddAsync(profile, cancellationToken);

            return EntityResponse.Success(profile.Id);
        }
    }
}