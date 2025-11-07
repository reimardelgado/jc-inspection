using Backend.Application.Specifications.ProfileSpecs;

namespace Backend.Application.Commands.ProfileCommands
{
    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, EntityResponse<bool>>
    {
        private readonly IRepository<Profile> _repository;
        private readonly IRepository<ProfilePermission> _profilePermisionRepository;

        public UpdateProfileCommandHandler(IRepository<Profile> repository,
            IRepository<ProfilePermission> profilePermisionRepository)
        {
            _repository = repository;
            _profilePermisionRepository = profilePermisionRepository;
        }

        public async Task<EntityResponse<bool>> Handle(UpdateProfileCommand command, CancellationToken cancellationToken)
        {
            var spec = new ProfileSpec(command.ProfileId);
            var profile = await _repository.GetBySpecAsync(spec, cancellationToken);

            if (profile == null)
            {
                return EntityResponse<bool>.Error(EntityResponseUtils.GenerateMsg(MessageHandler.ProfileAlreadyExit));
            }

            if (profile.ProfilePermissions.Any())
                await _profilePermisionRepository.DeleteRangeAsync(profile.ProfilePermissions, cancellationToken);

            profile.Name = command.Name;
            profile.Description = command.Description;

            var permissionIds = command.ProfilePermissions
                .Where(s => Guid.TryParse(s, out _))
                .Select(Guid.Parse);

            foreach (var item in permissionIds)
            {
                await _profilePermisionRepository.AddAsync(new ProfilePermission(profile.Id, item), cancellationToken);
            }

            await _repository.UpdateAsync(profile, cancellationToken);

            return true;
        }
    }
}