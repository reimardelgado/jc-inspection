using Backend.Application.DTOs.Responses.ProfileResponses;
using Backend.Application.Specifications.ProfileSpecs;

namespace Backend.Application.Queries.ProfileQueries
{
    public class ReadProfileQueryHandler : IRequestHandler<ReadProfileQuery, EntityResponse<ReadProfileResponse>>
    {
        #region Constructor && Properties

        private readonly IReadRepository<Profile> _repository;

        public ReadProfileQueryHandler(IReadRepository<Profile> repository)
        {
            _repository = repository;
        }

        #endregion

        public async Task<EntityResponse<ReadProfileResponse>> Handle(ReadProfileQuery request,
            CancellationToken cancellationToken)
        {
            var spec = new ProfileSpec(request.ProfileId);
            var profile = await _repository.GetBySpecAsync(spec, cancellationToken);
            if (profile is null)
            {
                return EntityResponse<ReadProfileResponse>.Error(MessageHandler.ProfileNotFound);
            }

            return ReadProfileResponse.FromEntity(profile);
        }
    }
}