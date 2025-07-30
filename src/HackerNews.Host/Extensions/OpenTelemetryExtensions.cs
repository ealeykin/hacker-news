using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

namespace HackerNews.HackerNews.Host.Extensions;

public static class OpenTelemetryExtensions
{
    public static WebApplicationBuilder AddOpenTelemetry(this WebApplicationBuilder builder, string name)
    {
        var resource = ResourceBuilder.CreateDefault().AddService(
            serviceName: name,
            serviceVersion: "1.0");

        builder.Services
            .AddMetrics()
            .AddOpenTelemetry()
            .WithMetrics(options =>
            {
                options
                    .SetResourceBuilder(resource)
                    .AddAspNetCoreInstrumentation()
                    .AddPrometheusExporter();
            });

        return builder;
    }
}