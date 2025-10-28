using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;


namespace InventoryManagement.OpenTelemetry
{
    public static class OpenTelemtryExtentsion
    {
        public static void AddOpenTelemtryTracing(this IServiceCollection services, string serviceName)
        {
            services.AddOpenTelemetry()
                 .ConfigureResource(resource => resource.AddService(serviceName))
                 .WithTracing(tracing => tracing
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddOtlpExporter(opts => { opts.Endpoint = new Uri("http://localhost:4317"); }));
        }
        public static void AddOpenTelemtryMetrics(this IServiceCollection services, string serviceName)
        {
            services.AddOpenTelemetry()
                 .ConfigureResource(resource => resource.AddService(serviceName))
                 .WithMetrics(metrics => metrics
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddOtlpExporter(opts => { opts.Endpoint = new Uri("http://localhost:4317"); }));
        }
        public static void AddOpenTelemtryLogging(this ILoggingBuilder logging, string serviceName)
        {
            logging.AddOpenTelemetry(options =>
           {
               options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
               .AddOtlpExporter(opts => { opts.Endpoint = new Uri("http://localhost:9411"); });

        });
        }
    }
}
