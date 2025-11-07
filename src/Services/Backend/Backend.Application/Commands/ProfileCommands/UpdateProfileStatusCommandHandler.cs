using Backend.Application.Specifications.ProfileSpecs;

namespace Backend.Application.Commands.ProfileCommands
{
    public class UpdateProfileStatusCommandHandler : IRequestHandler<UpdateProfileStatusCommand, EntityResponse<bool>>
    {
        #region Constructor & Properties

        private readonly IMediator _mediator;
        private readonly IRepository<Profile> _repository;

        public UpdateProfileStatusCommandHandler(IMediator mediator, IRepository<Profile> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        #endregion

        #region Public Methods

        public async Task<EntityResponse<bool>> Handle(UpdateProfileStatusCommand command, CancellationToken cancellationToken)
        {
            var spec = new ProfileSpec(command.ProfileId);
            var profile = await _repository.GetBySpecAsync(spec, cancellationToken);
            if (profile is null)
            {
                return EntityResponse<bool>.Error(MessageHandler.ProfileNotFound);
            }

            await UpdateProfile(command, profile, cancellationToken);
            return EntityResponse.Success(true);
        }

        #endregion

        #region Private Methods
        private async Task UpdateProfile(UpdateProfileStatusCommand command, Profile profile, CancellationToken cancellationToken)
        {
            profile.Status = command.Status;
            await _repository.UpdateAsync(profile, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        #endregion
    }
}