namespace Backend.Application.Services.Reads
{
    public class ReadUserGlobalPermissions : IRequest<ICollection<UserGlobalPermission>>
    {
        public Guid UserId { get; }

        public ReadUserGlobalPermissions(Guid userId)
        {
            UserId = userId;
        }
    }
}