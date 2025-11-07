using Ardalis.Specification;

namespace Backend.Application.Specifications.MemberSpecs
{
    public sealed class UserSpec : Ardalis.Specification.Specification<User>,
        ISingleResultSpecification
    {
        public UserSpec(Guid? id, string? username, string? password)
        {
            //Query.Include(user => user.UserProfiles).ThenInclude(i => i.Profile);

            Query.Where(user => user.Status != UserState.Deleted);

            if (id != null)
                Query.Where(user => user.Id == id);

            if (!string.IsNullOrEmpty(username))
                Query.Where(user => user.Username.ToUpper().Equals(username.ToUpper()));

            if (!string.IsNullOrEmpty(password))
                Query.Where(user => user.Password!.ToUpper().Equals(password.ToUpper()));
        }

        public UserSpec(string identification, string? userType)
        {
            Query.Where(i => i.Email == identification);

            Query.Where(user => user.Status == UserState.Active);
        }

        public UserSpec(string identification)
        {
            Query.Where(i => i.Dni == identification);

            Query.Where(user => user.Status == UserState.Active);
        }
    }
}