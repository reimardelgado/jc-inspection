namespace Backend.Application.Services.Reads
{
    public class ReadPermissionsService : IRequest<ICollection<Permission>>
    {
        public ICollection<Guid> Ids { get; }

        public ReadPermissionsService(ICollection<Guid> ids)
        {
            Ids = ids;
        }
    }
}