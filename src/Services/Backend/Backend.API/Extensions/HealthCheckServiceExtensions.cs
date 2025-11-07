namespace Backend.API.Extensions;

public static class HealthCheckServiceExtensions
{
    public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration config)
    {
        var hcBuilder = services.AddHealthChecks();

        hcBuilder
            .AddCheck("self", () => HealthCheckResult.Healthy())
            .AddSqlServer(
                config["ConnectionString"],
                name: "Backend-check",
                tags: new[] { "localdb" });

        return services;
    }
}