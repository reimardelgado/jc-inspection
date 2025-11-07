using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.Extensions.FileProviders;
using ServiceReference;

var configuration = GetConfiguration();
ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("es_EC");

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    Log.Information("Configuring web host ({ApplicationContext})...", Backend.API.Program.AppName);
    var builder = WebApplication.CreateBuilder(args);

    builder
        .Host
        .ConfigureAppConfiguration(x => x.AddConfiguration(configuration));

    builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy",
            builder => builder
                .SetIsOriginAllowed(_ => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
    });

    var app = builder
        .ConfigureServices(configuration)
        .ConfigurePipeline(configuration);

    Log.Information("Applying migrations ({ApplicationContext})...", Backend.API.Program.AppName);
    app.MigrateDbContext<BackendDbContext>((context, services) =>
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogInformation("Seeding data");

          UserSeeder.SeedData(context);
          ProfileSeeder.SeedData(context);
          PermissionSeeder.SeedData(context);
          ProfilePermissionSeeder.SeedData(context);

        logger.LogInformation("Seeding data DONE");
    });
    Log.Information("Starting web host ({ApplicationContext})...", Backend.API.Program.AppName);

    if (app.Environment.IsDevelopment())
    {
        //app.UseExceptionHandler("/Error");
        app.UseHsts();
    }

    //app.UseHttpsRedirection();
    app.UseCors("CorsPolicy"); //"CorsPolicy"
    app.UseSpaStaticFiles();
    app.UseStaticFiles(new StaticFileOptions { 
        FileProvider = new PhysicalFileProvider($"{app.Environment.ContentRootPath}\\AppFiles\\inspections\\images"),
        RequestPath = "/inspection/image"
    });
    app.UseDefaultFiles();
    app.MapDefaultControllerRoute();
    
    app.Run();
}
catch (Exception ex) when
    (ex.GetType().Name is not "StopTheHostException") // https://github.com/dotnet/runtime/issues/60600
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}

namespace Backend.API
{
    public class Program
    {
        public static string Namespace = typeof(Program).Namespace!;

        public static string AppName =
            Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
    }
}