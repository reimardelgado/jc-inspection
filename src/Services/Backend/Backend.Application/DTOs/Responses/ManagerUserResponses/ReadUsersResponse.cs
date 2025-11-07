namespace Backend.Application.DTOs.Responses.ManagerUserResponses
{
    public record ReadUsersResponse(Guid Id, string UserName, string Email, string? FirstName, string? LastName,
        string? identification, string? phone, string Status, string? avatar, string fullName, List<ReadUsersProfileDto> Profiles)
    {
        public static ReadUsersResponse FromEntity(User user)
        {
            List<ReadUsersProfileDto> profiles = new();

            var userProfiles = user.UserProfiles.Any();
            if (userProfiles)
            {
                profiles = user.UserProfiles
                    .Select(userProfile => ReadUsersProfileDto.FromEntity(userProfile.Profile))
                    .ToList();
            }

            return new ReadUsersResponse(user.Id, user.Username, user.Email, user.FirstName, user.LastName,
                user.Dni, user.Phone, user.Status, user.Avatar, user.FullName, profiles);
        }
    }

    public record ReadUsersProfileDto(Guid Id, string Name, string Description)
    {
        public static ReadUsersProfileDto FromEntity(Profile profile)
        {
            return new ReadUsersProfileDto(profile.Id, profile.Name, profile.Description);
        }
    }
}