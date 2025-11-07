using Ardalis.Specification;
using Shared.Domain.Specification;

namespace Backend.Application.Specifications.MemberSpecs;

public sealed class ManagerSpec : Ardalis.Specification.Specification<User>, ISingleResultSpecification
{
    public ManagerSpec(Guid id)
    {
        Query
            .Include(user => user.UserProfiles)
            .ThenInclude(userProfiles => userProfiles.Profile)
            .Where(user => user.Status != UserState.Deleted);

        Query
            .Where(user => user.Id == id);
    }

    public ManagerSpec(string? queryParam, bool isPagingEnabled, int page, int pageSize)
    {
        Query
            .Include(user => user.UserProfiles)
            .ThenInclude(userProfiles => userProfiles.Profile);
            // .Where(user => user.Status != UserState.Deleted);

        if (!string.IsNullOrEmpty(queryParam))
            Query.Where(user => user.Username.ToLower().Contains(queryParam.ToLower())
            || user.FirstName.ToLower().Contains(queryParam.ToLower())
            || user.LastName.ToLower().Contains(queryParam.ToLower())
            || user.Email.ToLower().Contains(queryParam.ToLower())
            || user.Dni!.ToLower().Equals(queryParam.ToLower())
            );
        if (isPagingEnabled)
            Query
                .OrderBy(user => user.CreatedAt)
                .Skip(PaginationHelper.CalculateSkip(pageSize, page))
                .Take(PaginationHelper.CalculateTake(pageSize));
    }
    public ManagerSpec()
    {
        Query.Where(x => x.Status != StatusEnum.Deleted);

    }
}
