using Backend.Domain.SeedWork;

namespace Backend.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Password { get; set; }
    public string? Dni { get; set; }
    public string? Phone { get; set; }
    public string? MobileId { get; set; }
    public string Status { get; set; } = UserState.Active;
    public string? Avatar { get; set; }
    public string? IdZoho { get; set; }

    private readonly List<UserProfile> _userProfiles = new();
    public IReadOnlyCollection<UserProfile> UserProfiles => _userProfiles;

    public string FullName => $"{FirstName} {LastName}".Trim();

    protected User()
    {
        Id = Guid.NewGuid();
    }

    public User(string username, string firstName, string lastName, string email,
        string? dni, string? phone, string status, string? avatar) : this()
    {
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Dni = dni;
        Phone = phone;
        Status = status;
        Avatar = avatar;
    }
}