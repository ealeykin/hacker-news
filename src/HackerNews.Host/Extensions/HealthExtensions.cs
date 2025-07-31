using HackerNews.HackerNews.Infrastructure.HackerNewsClient;

namespace HackerNews.HackerNews.Host.Extensions;

public static class HealthExtensions
{
    public static WebApplicationBuilder AddHealthChecks(this WebApplicationBuilder builder)
    {
        var hackerNewsOptions = builder.Configuration.GetSection("HackerNews").Get<HackerNewsOptions>()!;

        Prometheus.Metrics.SuppressDefaultMetrics();

        builder.Services
            .AddHealthChecks()
            .AddUrlGroup(new Uri(hackerNewsOptions.Url), "hacker-news");

        return builder;
    }
}