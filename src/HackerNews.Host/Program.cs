using System.Text.Json;
using System.Text.Json.Serialization;
using HackerNews.HackerNews.Application;
using HackerNews.HackerNews.Host.Extensions;
using HackerNews.HackerNews.Infrastructure;
using HackerNews.HackerNews.Infrastructure.HackerNewsClient;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddSerilog()
    .AddOpenTelemetry("hacker-news")
    .AddApplication()
    .AddInfrastructure();

builder.Services
    .AddSwaggerGen()
    .AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        o.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        o.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

var hackerNewsOptions = builder.Configuration.GetSection("HackerNews").Get<HackerNewsOptions>()!;

Prometheus.Metrics.SuppressDefaultMetrics();

builder.Services
    .AddHealthChecks()
    .AddUrlGroup(new Uri(hackerNewsOptions.Url), "hacker-news");

var app = builder.Build();

app.UseExceptionHandler(_ => { });
app.UseSwagger().UseSwaggerUI();
app.UseAuthentication().UseAuthorization();
app.MapControllers();

app
    .UseHealthChecksPrometheusExporter("/health")
    .UseOpenTelemetryPrometheusScrapingEndpoint();

await app.RunAsync();
