namespace Backend.Application.Commands.UserCommands
{
    public class UpdateProfileUserCommand : IRequest<EntityResponse<bool>>
    {
        public Guid UserId { get; }
        public string? Username { get; }
        public string? FirstName { get; }
        public string? LastName { get; }
        public string? Identification { get; }
        public string? Email { get; }
        public string? Phone { get; }
        public string? Avatar { get; }

        public UpdateProfileUserCommand(Guid userId, string? username, string? firstName,
            string? lastName, string? identification, string? email, string? phone, string? avatar)
        {
            UserId = userId;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Identification = identification;
            Email = email;
            Phone = phone;
            Avatar = avatar;
        }
    }
}