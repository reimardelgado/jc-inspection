using Backend.Domain.DTOs.Requests.Zoho;
using Backend.Domain.Interfaces.Services;

namespace Backend.Application.Commands.UserCommands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, EntityResponse<bool>>
    {
        #region Constructor & Properties

        private readonly IMediator _mediator;
        private readonly IRepository<User> _userRepository;
        private readonly IZohoExternalUserService _zohoExternalUserService;

        public DeleteUserCommandHandler(IMediator mediator, IRepository<User> userRepository, IZohoExternalUserService zohoExternalUserService)
        {
            _mediator = mediator;
            _userRepository = userRepository;
            _zohoExternalUserService = zohoExternalUserService;
        }


        #endregion

        #region Public Methods

        public async Task<EntityResponse<bool>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var validateResponse = await Validations(command, cancellationToken);
            if (!validateResponse.IsSuccess)
            {
                return EntityResponse<bool>.Error(validateResponse);
            }

            var dbUser = validateResponse.Value;
            await UpdateUser(dbUser!, cancellationToken);

            return EntityResponse.Success(true);
        }

        #endregion

        #region Private Methods

        private async Task<EntityResponse<User>> Validations(DeleteUserCommand command,
            CancellationToken cancellationToken)
        {
            var area = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);

            return area is null
                ? EntityResponse<User>.Error(MessageHandler.UserNotFound)
                : EntityResponse.Success(area);
        }

        private async Task UpdateUser(User user, CancellationToken cancellationToken)
        {
            user.Status = StatusEnum.Deleted;
            await _userRepository.UpdateAsync(user, cancellationToken);
            
            //Update Status in Zoho
            var zohoModel = new ZohoExternalUser(user.Id.ToString(), user.Username!, user.FirstName!,
                user.LastName,  user.Email, user.Phone,StatusEnum.Deleted);
            zohoModel.Id = user.IdZoho;
            await _zohoExternalUserService.UpdateInZoho(zohoModel, cancellationToken);
        }

        #endregion
    }
}