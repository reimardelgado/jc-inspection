using Shared.Domain.Interfaces.Services;
using Module = Autofac.Module;

namespace Backend.API.Configurations.AutofacConfig;

public class CommonModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<JwtTokenService>()
            .As<IJwtTokenService>()
            .InstancePerLifetimeScope();

        builder.RegisterType<MfaCodeGenerator>()
            .As<IMfaCodeGenerator>()
            .InstancePerLifetimeScope();
    }
}