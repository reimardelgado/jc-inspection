using Backend.Application.Commands.AuthJwtCommands;
using Backend.Application.Commands.NotificationCommands;
using Backend.Application.DTOs.Responses.UserResponses;
using Backend.Application.Queries.UserQueries;
using Backend.Application.Services.Reads;
using Backend.Application.Specifications.UserByEmailSpecs;
using Backend.Application.Utils;

namespace Backend.Application.Commands.UserCommands
{
    public class RecoverPasswordCommandHandler : IRequestHandler<RecoverPasswordCommand, EntityResponse<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IRepository<User> _repository;

        private User? _user;

        public RecoverPasswordCommandHandler(IMediator mediator, IRepository<User> repository)
        {
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<EntityResponse<bool>> Handle(RecoverPasswordCommand command, CancellationToken cancellationToken)
        {
            var spec = new UserByEmailSpec(command.Email);
            var user = await _repository.GetBySpecAsync(spec, cancellationToken);
            if (user is null)
            {
                return EntityResponse<bool>.Error(EntityResponseUtils.GenerateMsg(MessageHandler.EmailFound));
            }

            var newPassword = GeneratePassword();

            //Send email
            var sendEmailModel = new SendEmailNotificationCommand(command.Email,"","","TuConstruction - Recover password",
                String.Format("Your new password has been generated: <br> Password: {0}", newPassword),new List<string>());
            await _mediator.Send(sendEmailModel, cancellationToken);

            user.Password = StringHandler.CreateMD5Hash(newPassword); 
            await _repository.UpdateAsync(user, cancellationToken);

            return EntityResponse<bool>.Success(true);
        }

        #region Private methods

        private string GeneratePassword()
        {
            Random random = new Random();
            var passLineal = string.Format("TC{0}", random.Next(1000000));
            return passLineal;
        }

        #endregion
    }
}