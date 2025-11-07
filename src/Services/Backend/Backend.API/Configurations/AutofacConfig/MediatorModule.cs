using Backend.Application.Queries.ManagerUserQueries;
using Shared.Application.Behaviors;

namespace Backend.API.Configurations.AutofacConfig;

public class MediatorModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            .AsImplementedInterfaces();


        // Register the Command's Validators (Validators based on FluentValidation library)
        builder.RegisterAssemblyTypes(typeof(ReadManagerUserQuery).GetTypeInfo().Assembly)
            .AsClosedTypesOf(typeof(IRequestHandler<,>));
        
        // builder
        //     .RegisterAssemblyTypes(typeof(CreateUserCommandValidator).GetTypeInfo().Assembly)
        //     .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
        //     .AsImplementedInterfaces();

        builder.Register<ServiceFactory>(context =>
        {
            var componentContext = context.Resolve<IComponentContext>();
            return type => (componentContext.TryResolve(type, out var @object) ? @object : null)!;
        });

        //builder.RegisterGeneric(typeof(LogTransactionBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        //builder.RegisterGeneric(typeof(IntegrationTransactionBehavior<,>)).As(typeof(IPipelineBehavior<,>));
    }
}