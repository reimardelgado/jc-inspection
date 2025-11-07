namespace Backend.Domain.Entities;

public class UserGlobalPermission : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid PermissionId { get; set; }
}