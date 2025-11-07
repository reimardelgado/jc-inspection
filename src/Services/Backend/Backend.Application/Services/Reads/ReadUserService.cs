namespace Backend.Application.Services.Reads
{
    public class ReadUserService : IRequest<User?>
    {
        public Guid? Id { get; }
        public string? Username { get; }
        public string? Password { get; }
        public string? Email { get; set; }

        public ReadUserService(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public ReadUserService(Guid id)
        {
            Id = id;
        }

        public ReadUserService(string email)
        {
            Email = email;
        }
    }
}