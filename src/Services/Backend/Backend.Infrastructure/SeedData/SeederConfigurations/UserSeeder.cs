using Backend.Application.Utils;
using Backend.Domain.Entities;

namespace Backend.Infrastructure.SeedData.SeederConfigurations;

public class UserSeeder
{
    public static void SeedData(BackendDbContext context)
    {
        if (context.Users.Any())
            return;

        context.Set<User>().AddRange(
            new User("admin", "Administrador", "Principal", "reimardelgado@gmail.com", "0123456789", "", "active", "")
            { 
                Password = StringHandler.CreateMD5Hash("admin12.")
            });
        
        context.SaveChangesAsync().Wait();
    }
}