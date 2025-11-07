using Ardalis.Specification;

namespace Backend.Application.Specifications.PermissionSpecs
{
    public sealed class PermissionByIdsSpec : Specification<Permission>
    {
        public PermissionByIdsSpec(ICollection<Guid> ids)
        {
            Query.Where(permission => ids.Contains(permission.Id));
        }
    }
}