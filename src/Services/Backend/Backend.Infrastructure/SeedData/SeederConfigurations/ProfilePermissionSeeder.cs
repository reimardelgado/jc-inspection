using Backend.Domain.Entities;

namespace Backend.Infrastructure.SeedData.SeederConfigurations;

public class ProfilePermissionSeeder
{
    public static void SeedData(BackendDbContext context)
    {
        if (context.ProfilePermissions.Any())
            return;

        context.Set<ProfilePermission>().AddRange(
            new ProfilePermission(Guid.Parse("aff7f513-ddaf-4f0a-82fe-525973efa60e"), Guid.Parse("83e67812-50e1-4a35-939d-362cc77b560a"))  // Backend:Users:FullAccess
        );

        context.SaveChangesAsync().Wait();
    }
}