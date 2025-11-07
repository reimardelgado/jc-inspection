using Backend.Application.Specifications.UserByEmailSpecs;

namespace Backend.Application.Commands.NotificationCommands
{
    public class UpdateMobileIdUserCommandHandler : IRequestHandler<UpdateMobileIdUserCommand, EntityResponse<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<User> _repository;

        private User? _user;

        public UpdateMobileIdUserCommandHandler(IMediator mediator, IRepository<User> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<EntityResponse<bool>> Handle(UpdateMobileIdUserCommand command, CancellationToken cancellationToken)
        {
            var spec = new UserByEmailSpec(command.Email);
            var user = await _repository.GetBySpecAsync(spec, cancellationToken);
            if (user is null)
            {
                return EntityResponse<bool>.Error(EntityResponseUtils.GenerateMsg(MessageHandler.UserNotFound));
            }

            await UpdateUser(command, user, cancellationToken);

            return EntityResponse.Success(true);
        }

        #region Private methods

        private async Task UpdateUser(UpdateMobileIdUserCommand command, User user, CancellationToken cancellationToken)
        {
            user.MobileId = command.MobileId;

            await _repository.UpdateAsync(user, cancellationToken);
        }

        #endregion
    }
}