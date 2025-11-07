using Module = Autofac.Module;

namespace Backend.API.Configurations.AutofacConfig;

public class EventsModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //builder.RegisterType<IntegrationEventService>()
        //    .As<IIntegrationEventService>()
        //    .InstancePerLifetimeScope();

        //builder.RegisterType<IntegrationEventLogService<BackendDbContext>>()
        //    .As<IIntegrationEventLogService>()
        //    .InstancePerLifetimeScope();
    }
}