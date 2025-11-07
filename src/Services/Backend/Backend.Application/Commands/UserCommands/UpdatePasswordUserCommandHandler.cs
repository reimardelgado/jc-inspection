using Backend.Application.Specifications.MemberSpecs;
using Backend.Application.Specifications.UserProfileSpecs;

namespace Backend.Application.Commands.UserCommands
{
    public class UpdatePasswordUserCommandHandler
        : IRequestHandler<UpdatePasswordUserCommand, EntityResponse<bool>>
    {
        private readonly IRepository<User> _repository;
        private readonly IMediator _mediator;

        public UpdatePasswordUserCommandHandler(IRepository<User> repository,
            IMediator mediator)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<EntityResponse<bool>> Handle(UpdatePasswordUserCommand command,
            CancellationToken cancellationToken)
        {
            var spec = new ManagerSpec(command.UserId);
            var user = await _repository.GetBySpecAsync(spec, cancellationToken);
            if (user == null)
            {
                return EntityResponse<bool>.Error(EntityResponseUtils.GenerateMsg(MessageHandler.UserAlreadyRegistered));
            }
            if (user.Password.ToUpper() == command.ActualPassword.ToUpper())
            {
                await UpdatePasswordUser(command, user, cancellationToken);

                return true;
            }
            return EntityResponse<bool>.Error(EntityResponseUtils.GenerateMsg(MessageHandler.PasswordNotEquals));
        }

        private async Task UpdatePasswordUser(UpdatePasswordUserCommand command, User user, CancellationToken cancellationToken)
        {
            user.Password = command.NewPassword;
            await _repository.UpdateAsync(user, cancellationToken);
        }

    }
}