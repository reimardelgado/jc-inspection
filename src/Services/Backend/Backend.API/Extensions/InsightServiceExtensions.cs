namespace Backend.API.Extensions;

public static class InsightServiceExtensions
{
    public static IServiceCollection AddAppInsight(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddApplicationInsightsTelemetry(configuration);
        //services.AddApplicationInsightsKubernetesEnricher();

        return services;
    }
}