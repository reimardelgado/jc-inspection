using Backend.Domain.Entities;

namespace Backend.Infrastructure.SeedData.SeederConfigurations;

public class ProfileSeeder
{
    public static void SeedData(BackendDbContext context)
    {
        if (context.Profiles.Any())
            return;
        
        context.Set<Profile>().AddRange(
            new Profile(Guid.Parse("aff7f513-ddaf-4f0a-82fe-525973efa60e"), "Administrador", "Perfil con todos los permisos")
            );
        
        context.SaveChangesAsync().Wait();
    }
}