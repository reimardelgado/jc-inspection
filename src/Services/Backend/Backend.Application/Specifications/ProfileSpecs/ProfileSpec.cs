using Ardalis.Specification;
using Shared.Domain.Specification;

namespace Backend.Application.Specifications.ProfileSpecs
{
    public sealed class ProfileSpec : Ardalis.Specification.Specification<Profile>, ISingleResultSpecification
    {
        public ProfileSpec(ICollection<Guid> ids)
        {
            Query
                .Include(profile => profile.ProfilePermissions)
                .ThenInclude(permission => permission.Permission)
                .Where(profile => ids.Contains(profile.Id) && profile.Status != ProfileStatus.Deleted);
        }

        public ProfileSpec(Guid id)
        {
            Query
                .Include(profile => profile.ProfilePermissions)
                .ThenInclude(permission => permission.Permission)
                .Where(profile => profile.Status != ProfileStatus.Deleted);

            Query.Where(profile => profile.Id == id);
        }

        public ProfileSpec(Guid? id, string? name)
        {
            Query.Where(profile => profile.Status != ProfileStatus.Deleted);
            if (id != null)
            {
                Query.Where(profile => profile.Id == id);
            }
            else
            {
                if (!string.IsNullOrEmpty(name))
                    Query.Where(profile => profile.Name.ToLower().Equals(name.ToLower()));

                Query.OrderBy(profile => profile.CreatedAt);
            }
        }

        public ProfileSpec(string? name, string? description, bool isPagingEnabled, int page, int pageSize)
        {
            Query
                .Include(profile => profile.ProfilePermissions)
                .ThenInclude(permission => permission.Permission);

            Query.Where(profile => profile.Status != ProfileStatus.Deleted);

            if (!string.IsNullOrEmpty(name))
                Query.Where(profile => profile.Name.Contains(name));

            if (!string.IsNullOrEmpty(description))
                Query.Where(profile => profile.Description.Contains(description));

            if (isPagingEnabled)
                Query
                    .OrderBy(user => user.CreatedAt)
                    .Skip(PaginationHelper.CalculateSkip(pageSize, page))
                    .Take(PaginationHelper.CalculateTake(pageSize));
        }

        public ProfileSpec()
        {
            Query.Where(x => x.Status != StatusEnum.Deleted);

        }
    }
}