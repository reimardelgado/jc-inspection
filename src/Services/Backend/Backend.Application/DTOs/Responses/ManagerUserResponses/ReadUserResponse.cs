namespace Backend.Application.DTOs.Responses.ManagerUserResponses;
    public class ReadUserResponse
    {
        public Guid Id { get; }
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Password { get; set; }
        public string? Dni { get; set; }
        public string? Phone { get; set; }
        public string? MobileId { get; set; }
        public string Status { get; set; }
        public string? Avatar { get; set; }
    public string FullName { get; set; }

    public List<ReadUserProfileDto> Profiles { get; } = new();

    public ReadUserResponse(Guid id, string username, string email, string firstName, string lastName,
        string? dni, string? phone, string status, string? avatar, List<ReadUserProfileDto> profiles)
    {
        Id = id;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Dni = dni;
        Phone = phone;
        Status = status;
        Avatar = avatar;
        Profiles = profiles;
        FullName = $"{firstName} {lastName}";
    }

    public static ReadUserResponse FromEntity(User user)
        {
            List<ReadUserProfileDto> profiles = new();

            var userProfiles = user.UserProfiles.Any();
            if (userProfiles)
            {
                profiles = user.UserProfiles
                    .Select(userProfile => ReadUserProfileDto.FromEntity(userProfile.Profile))
                    .ToList();
            }

            return new ReadUserResponse(user.Id, user.Username, user.Email, user.FirstName, user.LastName, 
                user.Dni, user.Phone, user.Status, user.Avatar, profiles);
        }
    }

    public record ReadUserProfileDto(Guid Id, string Name, string Description)
    {
        public static ReadUserProfileDto FromEntity(Profile profile)
        {
            return new ReadUserProfileDto(profile.Id, profile.Name, profile.Description);
        }
    }
