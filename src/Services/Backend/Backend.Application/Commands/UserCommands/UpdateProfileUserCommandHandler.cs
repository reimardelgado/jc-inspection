using Backend.Application.Specifications.MemberSpecs;

namespace Backend.Application.Commands.UserCommands
{
    public class UpdateProfileUserCommandHandler
        : IRequestHandler<UpdateProfileUserCommand, EntityResponse<bool>>
    {
        private readonly IRepository<User> _repository;

        public UpdateProfileUserCommandHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<EntityResponse<bool>> Handle(UpdateProfileUserCommand command,
            CancellationToken cancellationToken)
        {
            var spec = new ManagerSpec(command.UserId);
            var User = await _repository.GetBySpecAsync(spec, cancellationToken);
            if (User == null)
            {
                return EntityResponse<bool>.Error(EntityResponseUtils.GenerateMsg(MessageHandler.UserFound));
            }

            await UpdateUser(command, cancellationToken, User);

            return true;
        }

        private async Task UpdateUser(UpdateProfileUserCommand command, CancellationToken cancellationToken,
            User user)
        {
            user.Email = command.Email ?? user.Email;
            user.FirstName = command.FirstName ?? user.FirstName;
            user.LastName = command.LastName ?? user.LastName;
            user.Username = command.Username ?? user.Username;
            user.Phone = command.Phone ?? user.Phone;
            user.Dni = command.Identification ?? user.Dni;

            await _repository.UpdateAsync(user, cancellationToken);
        }
    }
}