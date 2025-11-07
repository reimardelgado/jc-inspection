using Backend.Application.Specifications.ProfileSpecs;

namespace Backend.Application.Services.Reads
{
    public class ReadProfileServiceHandler : IRequestHandler<ReadProfileService, Profile?>
    {
        private readonly IRepository<Profile> _repository;

        public ReadProfileServiceHandler(IRepository<Profile> repository)
        {
            _repository = repository;
        }

        public async Task<Profile?> Handle(ReadProfileService query, CancellationToken cancellationToken)
        {
            var spec = new ProfileSpec(query.Id, query.Name);
            var profile = await _repository.GetBySpecAsync(spec, cancellationToken);

            return profile;
        }
    }
}