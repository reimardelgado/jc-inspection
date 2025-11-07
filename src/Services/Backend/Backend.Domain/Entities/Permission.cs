namespace Backend.Domain.Entities;

public class Permission : BaseEntity
{
    #region Constructor & properties

    public string? Code { get; set; }
    public string? Description { get; set; }
    public string? ResourceCode { get; set; }
    public string? Type { get; set; }

    private readonly List<ProfilePermission> _profilePermissions = new();
    public IReadOnlyCollection<ProfilePermission> ProfilePermissions => _profilePermissions;

    private readonly List<UserGlobalPermission> _userGlobalPermissions = new();
    public IReadOnlyCollection<UserGlobalPermission> UserGlobalPermissions => _userGlobalPermissions;

    protected Permission()
    {
    }

    public Permission(string? code, string? description, string? resourceCode, string? type)
        : this()
    {
        Code = code;
        Description = description;
        ResourceCode = resourceCode;
        Type = type;
    }

    public Permission(Guid id, string? code, string? description, string? resourceCode, string? type)
        : base(id)
    {
        Code = code;
        Description = description;
        ResourceCode = resourceCode;
        Type = type;
    }

    #endregion

    #region Public methods

    public void AddProfilePermission(Guid profileId)
    {
        var profilePermission = new ProfilePermission(profileId, Id);
        _profilePermissions.Add(profilePermission);
    }

    #endregion
}