using Quartz;

namespace Backend.API.Extensions;

public static class AddBackgroundTasksExtensions
{
    public static IServiceCollection AddBackgroundTasks(this IServiceCollection services, IConfiguration config)
    {
        // Add the required Quartz.NET services
        services.AddQuartz(q =>
        {
            // Use a Scoped container to create jobs. I'll touch on this later
            q.UseMicrosoftDependencyInjectionJobFactory();

            //q.AddJobAndTrigger<>(config);
        });

        // Add the Quartz.NET hosted service
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return services;
    }
}

