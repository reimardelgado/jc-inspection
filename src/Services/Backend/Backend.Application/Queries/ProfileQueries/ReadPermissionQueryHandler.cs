using Backend.Application.DTOs.Responses.ProfileResponses;

namespace Backend.Application.Queries.ProfileQueries
{
    public class ReadPermissionQueryHandler : IRequestHandler<ReadPermissionQuery, EntityResponse<List<PermissionResponse>>>
    {
        #region Constructor && Properties

        private readonly IReadRepository<Permission> _repository;

        public ReadPermissionQueryHandler(IReadRepository<Permission> repository)
        {
            _repository = repository;
        }

        #endregion

        public async Task<EntityResponse<List<PermissionResponse>>> Handle(ReadPermissionQuery request,
            CancellationToken cancellationToken)
        {
            var permissions = await _repository.ListAsync(cancellationToken);
            if (!permissions.Any())
            {
                return EntityResponse<List<PermissionResponse>>.Error(MessageHandler.PermissionNotFound);
            }

            return PermissionResponse.FromEntity(permissions);
        }
    }
}