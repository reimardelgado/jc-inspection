namespace Backend.Domain.Entities;

public class ProfilePermission : BaseEntity
{
    #region Constructor & properties

    public Guid ProfileId { get; set; }
    public Profile? Profile { get; set; }
    public Guid PermissionId { get; set; }
    public Permission? Permission { get; set; }

    public ProfilePermission(Guid profileId, Guid permissionId)
    {
        ProfileId = profileId;
        PermissionId = permissionId;
    }



    #endregion
}