using Backend.Application.Specifications.MemberSpecs;
using Backend.Application.Specifications.UserProfileSpecs;

namespace Backend.Application.Commands.UserCommands
{
    public class UpdateUserCommandHandler
        : IRequestHandler<UpdateUserCommand, EntityResponse<bool>>
    {
        public readonly IRepository<UserProfile> _userPrefileRepository;
        private readonly IRepository<User> _repository;
        private readonly IMediator _mediator;

        public UpdateUserCommandHandler(IRepository<User> repository,
            IRepository<UserProfile> userPrefileRepository, IMediator mediator)
        {
            _userPrefileRepository = userPrefileRepository;
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<EntityResponse<bool>> Handle(UpdateUserCommand command,
            CancellationToken cancellationToken)
        {
            var spec = new ManagerSpec(command.UserId);
            var user = await _repository.GetBySpecAsync(spec, cancellationToken);
            if (user == null)
            {
                return EntityResponse<bool>.Error(EntityResponseUtils.GenerateMsg(MessageHandler.UserAlreadyRegistered));
            }

            await UpdateUser(command, user, cancellationToken);

            await UpdateUserProfile(command, user, cancellationToken);

            return true;
        }

        private async Task UpdateUser(UpdateUserCommand command, User user, CancellationToken cancellationToken)
        {
            user.Email = command.Email;
            user.FirstName = command.FirstName;
            user.LastName = command.LastName;
            user.Username = command.Username;
            user.Phone = command.Phone;
            user.Dni = command.Identification;


            await _repository.UpdateAsync(user, cancellationToken);
        }

        private async Task UpdateUserProfile(UpdateUserCommand command, User user, CancellationToken cancellationToken)
        {
            var profileId = command.ProfileIds
                .Where(s => Guid.TryParse(s, out _))
                .Select(Guid.Parse);

            var userProfileSpec = new UserProfileSpec(user.Id, null);
            var userProfiles = await _userPrefileRepository.ListAsync(userProfileSpec, cancellationToken);

            if (userProfiles.Any())
            {
                await _userPrefileRepository.DeleteRangeAsync(userProfiles, cancellationToken);
            }

            await _userPrefileRepository.SaveChangesAsync(cancellationToken);

            foreach (var item in profileId)
            {
                await _userPrefileRepository.AddAsync(new UserProfile
                {
                    ProfileId = item,
                    UserId = user.Id
                }, cancellationToken);
            }
        }
    }
}