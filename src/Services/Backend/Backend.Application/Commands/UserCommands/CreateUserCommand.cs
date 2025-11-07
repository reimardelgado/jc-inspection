namespace Backend.Application.Commands.UserCommands
{
    public class CreateUserCommand : IRequest<EntityResponse<Guid>>
    {
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Identification { get; set; }
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Status { get; set; } = UserState.Active;
        public string? Avatar { get; set; }
        public List<string> ProfileIds { get; } = new();

        public CreateUserCommand(string username, string firstName, string lastName,
            string email, string? identification, string? phone,
            DateTime? dateOfBirth, string? avatar, List<string> profileIds)
        {
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