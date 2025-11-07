using System.Configuration;
using Amazon.S3;
using Backend.Application.Configurations;

namespace Backend.API.Extensions;

public static class CustomOptionsServiceExtensions
{
    public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<ZohoAuthSettings>(config.GetSection("ZohoAuthSettings"));
        services.Configure<ZohoCrmSettings>(config.GetSection("ZohoCrmSettings"));
        services.Configure<ModuleSettings>(config.GetSection("ModuleSettings"));
        services.Configure<ComunSettings>(config.GetSection("ComunSettings"));
        // services.Configure<MailSettings>(config.GetSection("MailSettings"));
        // services.Configure<AWSSettings>(config.GetSection("AWSSettings"));
        // services.Configure<S3Settings>(config.GetSection("S3Settings"));
        // // Configure Aws S3
        // services.AddDefaultAWSOptions(config.GetAWSOptions());
        // services.AddAWSService<IAmazonS3>();
        return services;
    }
}