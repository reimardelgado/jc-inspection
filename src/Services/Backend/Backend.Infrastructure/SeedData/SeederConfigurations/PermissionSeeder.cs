using Backend.Domain.Entities;
using Backend.Domain.SeedWork;

namespace Backend.Infrastructure.SeedData.SeederConfigurations;

public class PermissionSeeder
{
    public static void SeedData(BackendDbContext context)
    {
        if (context.Permissions.Any())
            return;

        context.Set<Permission>().AddRange(
            // Manager
            new Permission(Guid.Parse("83e67812-50e1-4a35-939d-362cc77b560a"), "Backend:Users:FullAccess", "Acceso total a los usuarios del backoffice", "USERS", PermissionTypes.Global),
            new Permission(Guid.Parse("c556dfb8-b66a-4393-81cc-864a56381e05"), "Backend:Users:ReadEditAccess", "Acceso de lectura y edición a los usuarios del backoffice", "USERS", PermissionTypes.Global),
            new Permission(Guid.Parse("92a282ec-808c-4a3e-a8fd-a7562aad1c11"), "Backend:Users:ReadOnlyAccess", "Acceso de sólo lectura a los usuarios del backoffice", "USERS", PermissionTypes.Global),
            new Permission(Guid.Parse("79fc0f03-849f-4968-8224-02b6b6f14aa1"), "Backend:Users:Delete", "Acceso eliminar a los usuarios del backoffice", "USERS", PermissionTypes.Global),
            new Permission(Guid.Parse("f737b076-c96b-4863-9be5-13d6808f1b0c"), "Backend:Users:CreateAccess", "Acceso a la creación de usuarios del backoffice", "USERS", PermissionTypes.Global)
        );

        context.SaveChangesAsync().Wait();
    }
}