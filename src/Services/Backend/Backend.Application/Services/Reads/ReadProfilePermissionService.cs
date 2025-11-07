namespace Backend.Application.Services.Reads
{
    public class ReadProfilePermissionService : IRequest<ICollection<ProfilePermission>>
    {
        public ICollection<Guid> Ids { get; }

        public ReadProfilePermissionService(ICollection<Guid> ids)
        {
            Ids = ids;
        }
    }
}