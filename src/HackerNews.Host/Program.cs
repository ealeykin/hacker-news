using System.Text.Json;
using System.Text.Json.Serialization;
using HackerNews.HackerNews.Application;
using HackerNews.HackerNews.Host.Extensions;
using HackerNews.HackerNews.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddSerilog()
    .AddHealthChecks()
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

var app = builder.Build();

app.UseExceptionHandler(_ => { });
app.UseSwagger().UseSwaggerUI();
app.UseAuthentication().UseAuthorization();
app.MapControllers();

app.UseHealthChecksPrometheusExporter("/health");
app.UseOpenTelemetryPrometheusScrapingEndpoint();

await app.RunAsync();
