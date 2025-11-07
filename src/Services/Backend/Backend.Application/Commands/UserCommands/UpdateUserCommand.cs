namespace Backend.Application.Commands.UserCommands
{
    public class UpdateUserCommand : IRequest<EntityResponse<bool>>
    {
        public Guid UserId { get; }
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Identification { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Avatar { get; set; }
        public List<string> ProfileIds { get; set; }

        public UpdateUserCommand(Guid userId, string username, string firstName, string lastName,
            string email, string? identification, string? phone, DateTime? dateOfBirth,
            string? avatar, List<string> profileIds)
        {
            UserId = userId;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Identification = identification;
            Phone = phone;
            DateOfBirth = dateOfBirth;
            Avatar = avatar;
            ProfileIds = profileIds;
        }
    }
}