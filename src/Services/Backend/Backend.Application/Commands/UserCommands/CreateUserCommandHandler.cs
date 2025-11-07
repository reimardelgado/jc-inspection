using Backend.Application.Commands.NotificationCommands;
using Backend.Application.Events.Domain;
using Backend.Application.Specifications.MemberSpecs;
using Backend.Application.Utils;
using Backend.Domain.DTOs.Requests.Zoho;
using Backend.Domain.Interfaces.Services;

namespace Backend.Application.Commands.UserCommands
{
    public class CreateUserCommandHandler
        : IRequestHandler<CreateUserCommand, EntityResponse<Guid>>
    {
        private readonly IRepository<UserProfile> _userProfileRepository;
        private readonly IRepository<User> _repository;
        private readonly IZohoExternalUserService _zohoExternalUserService;
        private readonly IMediator _mediator;

        public CreateUserCommandHandler(IRepository<User> repository,
            IRepository<UserProfile> userProfileRepository, IZohoExternalUserService zohoExternalUserService, IMediator mediator)
        {
            _userProfileRepository = userProfileRepository;
            _zohoExternalUserService = zohoExternalUserService;
            _mediator = mediator;
            _repository = repository;
        }

        public async Task<EntityResponse<Guid>> Handle(CreateUserCommand command,
            CancellationToken cancellationToken)
        {
            var spec = new UserSpec(command.Email, UserTypes.Manager);
            var User = await _repository.GetBySpecAsync(spec, cancellationToken);
            if (User != null)
            {
                return EntityResponse<Guid>.Error(EntityResponseUtils.GenerateMsg(MessageHandler.UserFound));
            }

            var managerUser = CreateUser(command);
            await _repository.AddAsync(managerUser, cancellationToken);

            if (command.ProfileIds.Any())
            {
                foreach (var profileId in command.ProfileIds)
                {
                    await _userProfileRepository.AddAsync(new UserProfile
                    {
                        ProfileId = Guid.Parse(profileId),
                        UserId = managerUser.Id
                    }, cancellationToken);
                }
            }
            
            var sendEmailModel = new SendEmailNotificationCommand(command.Email,"","","TuConstruction - Recover password",
                $"Dear {managerUser.FullName}. <br>You have been registered as a user of the TuConstruction inspection registration application. <br> Your password is: {managerUser.Password}"
                ,null);
            await _mediator.Send(sendEmailModel, cancellationToken);

            await _repository.SaveChangesAsync(cancellationToken); 
            
            //Creating user in Zoho
            var idZoho = await _zohoExternalUserService.CreateInZoho(new ZohoExternalUser(managerUser.Id.ToString(),
                    managerUser.Username, managerUser.FirstName, managerUser.LastName, managerUser.Email,
                    managerUser.Phone, managerUser.Status),
                cancellationToken);
            if (!string.IsNullOrEmpty(idZoho))
            {
                managerUser.IdZoho = idZoho;
                await _repository.UpdateAsync(managerUser, cancellationToken);
            }

            return EntityResponse.Success(managerUser.Id);
        }

        private User CreateUser(CreateUserCommand command)
        {
            var newUser = new User(command.Username, command.FirstName, command.LastName, command.Email,
                 command.Identification, command.Phone, UserState.Active, command.Avatar)
            {
                Password = StringHandler.CreateMD5Hash(GeneratePassword())
            };
            return newUser;
        }
        
        private string GeneratePassword()
        {
            Random random = new Random();
            var passLineal = string.Format("TC{0}", random.Next(1000000));
            return passLineal;
        }

    }
}