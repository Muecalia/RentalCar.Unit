using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using RentalCar.Unit.Core.Repositories;
using RentalCar.Unit.Core.Services;
using RentalCar.Unit.Infrastructure.MessageBus;
using RentalCar.Unit.Infrastructure.Prometheus;
using RentalCar.Unit.Infrastructure.Repositories;
using RentalCar.Unit.Infrastructure.Services;

namespace RentalCar.Unit.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) 
    {
        services
            .AddServices()
            .AddOpenTelemetryConfig();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services) 
    {
        services.AddSingleton<ILoggerService, LoggerService>();
        services.AddSingleton<IRabbitMqService, RabbitMqService>();

        services.AddSingleton<IPrometheusService, PrometheusService>();

        services.AddScoped<IUnitRepository, UnitRepository>();

        return services;
    }
    
    private static IServiceCollection AddOpenTelemetryConfig(this IServiceCollection services)
    {
        const string serviceName = "RentalCar Category";
        const string serviceVersion = "v1";
        
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName))
            .WithTracing(tracing => tracing
                .SetResourceBuilder(ResourceBuilder.CreateDefault()
                    .AddService(serviceName: serviceName, serviceVersion:serviceVersion))
                .AddAspNetCoreInstrumentation()
                .AddOtlpExporter()
                .AddConsoleExporter())
            .WithMetrics(metrics => metrics
                .AddConsoleExporter()
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddPrometheusExporter()
            );
        
        /*builder.Logging.AddOpenTelemetry(options =>
   {
       options
           .SetResourceBuilder(
               ResourceBuilder.CreateDefault()
                   .AddService(serviceName))
           .AddConsoleExporter();
   });*/

        return services;
    }
}