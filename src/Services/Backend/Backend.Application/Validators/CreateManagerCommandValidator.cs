using Backend.Application.Commands.UserCommands;

namespace Backend.Application.Validators
{
    public class CreateManagerCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateManagerCommandValidator()
        {
            RuleFor(command => command.Email)
                .NotEmpty()
                .EmailAddress().WithMessage(MessageHandler.ErrorEmailFormat)
                .When(command => string.IsNullOrEmpty(command.Email));

            RuleFor(command => command.Username)
                .NotEmpty()
                .WithMessage(MessageHandler.ManagerUsernameNotEmpty);

            RuleFor(command => command.FirstName)
                .NotEmpty()
                .WithMessage(MessageHandler.ManagerFirstnameNotEmpty);

            RuleFor(command => command.LastName)
                .NotEmpty()
                .WithMessage(MessageHandler.ManagerFirstnameNotEmpty);
        }
    }
}