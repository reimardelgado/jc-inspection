namespace Backend.API;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        builder
            .Services
            .AddAppInsight(configuration)
            .AddCustomMvc(configuration)
            .AddIdentityServices(configuration)
            .AddCustomHealthCheck(configuration)
            .AddCustomDbContext(configuration)
            .AddSwagger(configuration)
            //.AddEventBusServices(configuration)
            .AddCustomOptions(configuration)
            .AddBackgroundTasks(configuration)
            .AddGrpc();

        // Configure Autofac
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(b => b.RegisterModule(new CommonModule()));
        builder.Host.ConfigureContainer<ContainerBuilder>(b => b.RegisterModule(new MediatorModule()));
        builder.Host.ConfigureContainer<ContainerBuilder>(b => b.RegisterModule(new RepositoryModule()));
        builder.Host.ConfigureContainer<ContainerBuilder>(b => b.RegisterModule(new EventsModule()));

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app, IConfiguration configuration)
    {
        var pathBase = configuration["PATH_BASE"];

        if (!string.IsNullOrEmpty(pathBase))
        {
            var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
            loggerFactory.CreateLogger<Program>().LogDebug("Using PATH BASE '{PathBase}'", pathBase);
            app.UsePathBase(pathBase);
        }

        //app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseCors("CorsPolicy");

        // Authentication & Authorization
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapDefaultControllerRoute();
            endpoints.MapControllers();
            endpoints.MapGet("/_proto/", async ctx =>
            {
                ctx.Response.ContentType = "text/plain";
                await using var fs = new FileStream(
                    Path.Combine(app.Environment.ContentRootPath, "Proto", "Backend.proto"),
                    FileMode.Open, FileAccess.Read);
                using var sr = new StreamReader(fs);
                while (!sr.EndOfStream)
                {
                    var line = await sr.ReadLineAsync();
                    if (line != "/* >>" || line != "<< */")
                    {
                        await ctx.Response.WriteAsync(line ?? string.Empty);
                    }
                }
            });
            endpoints.MapHealthChecks("/hc", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });
        });

        //ConfigureEventBus(app);

        return app;
    }

    private static void ConfigureEventBus(WebApplication app)
    {
        
    }
}