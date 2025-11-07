using Backend.Application.Specifications.MemberSpecs;

namespace Backend.Application.Services.Reads
{
    public class ReadUserServiceHandler : IRequestHandler<ReadUserService, User?>
    {
        private readonly IReadRepository<User> _userRepository;

        public ReadUserServiceHandler(IReadRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> Handle(ReadUserService request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetBySpecAsync(
                new UserSpec(request.Id, request.Username, request.Password), cancellationToken);

            return user;
        }
    }
}