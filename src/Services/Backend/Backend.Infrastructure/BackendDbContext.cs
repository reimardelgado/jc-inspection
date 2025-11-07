namespace Backend.Infrastructure;

public class BackendDbContext : DbContext
{    
    // Business
    public DbSet<Inspection> Inspections => Set<Inspection>();
    public DbSet<FormTemplate> FormTemplates => Set<FormTemplate>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Catalog> Catalogs => Set<Catalog>();
    public DbSet<CatalogValue> CatalogValues => Set<CatalogValue>();
    public DbSet<Photo> Photos => Set<Photo>();
    public DbSet<ItemSection> ItemSections => Set<ItemSection>();
    public DbSet<InspectionResult> InspectionResults => Set<InspectionResult>();
    public DbSet<FormTemplateHistory> FormTemplateHistories => Set<FormTemplateHistory>();
    
    //Zoho
    public DbSet<ZohoToken> ZohoTokens => Set<ZohoToken>();
    public DbSet<User> Users => Set<User>();
    // Permissions
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<Profile> Profiles => Set<Profile>();
    
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<ProfilePermission> ProfilePermissions => Set<ProfilePermission>();
    
    public DbSet<UserGlobalPermission> UserGlobalPermissions => Set<UserGlobalPermission>();
    

    public BackendDbContext(DbContextOptions<BackendDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserEfConfig());
    }
    //
    // public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    // {
    //     await base.SaveChangesAsync(cancellationToken);
    //
    //     return true;
    // }
}