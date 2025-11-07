using Backend.Application.Specifications.PermissionSpecs;

namespace Backend.Application.Services.Reads
{
    public class ReadPermissionsServiceHandler : IRequestHandler<ReadPermissionsService, ICollection<Permission>>
    {
        private readonly IReadRepository<Permission> _permissionRepository;

        public ReadPermissionsServiceHandler(IReadRepository<Permission> permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<ICollection<Permission>> Handle(ReadPermissionsService request,
            CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.ListAsync(new PermissionByIdsSpec(request.Ids),
                cancellationToken);

            return permissions;
        }
    }
}