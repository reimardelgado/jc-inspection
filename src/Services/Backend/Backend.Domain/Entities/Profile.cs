using Backend.Domain.SeedWork;

namespace Backend.Domain.Entities;

public class Profile : BaseEntity
{
    #region Constructor & properties

    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? Status { get; set; } = ProfileStatus.Active;

    private readonly List<ProfilePermission> _profilePermissions = new();
    public IReadOnlyCollection<ProfilePermission> ProfilePermissions => _profilePermissions;


    protected Profile()
    {
        Id = Guid.NewGuid(); 
        _profilePermissions = new List<ProfilePermission>();
    }

    public Profile(string? name, string? description, DateTime? deletedAt, string? status) : this()
    {
        Name = name;
        Description = description;
        DeletedAt = deletedAt;
        Status = status;
    }

    protected Profile(Guid id) : base(id)
    {
        _profilePermissions = new List<ProfilePermission>();
    }

    public Profile(string name, string description)
        : this()
    {
        SetName(name);
        SetDescription(description);
    }

    public Profile(Guid id, string name, string description)
        : this(id)
    {
        SetName(name);
        SetDescription(description);
        Status = ProfileStatus.Active;
    }

    #endregion

    #region Setters

    public void SetName(string value)
    {
        Name = value;
    }

    public void SetDescription(string value)
    {
        Description = value;
    }

    public void SetUpdateAt()
    {
        UpdatedAt = DateTime.Now;
    }

    public void SetDeleteAt()
    {
        UpdatedAt = DateTime.Now;
    }

    #endregion

    #region Public methods

    public void AddProfilePermission(Guid permissionId)
    {
        var profilePermission = new ProfilePermission(Id, permissionId);
        _profilePermissions.Add(profilePermission);
    }

    #endregion
}