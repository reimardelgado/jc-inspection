namespace Backend.API.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<BackendDbContext>(options =>
        {
            options.UseSqlServer(config["ConnectionString"]);
        });

        return services;
    }
}