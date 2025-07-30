using HackerNews.HackerNews.Application.Abstractions;
using HackerNews.HackerNews.Infrastructure.HackerNewsClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;

namespace HackerNews.HackerNews.Infrastructure;

public static class Extensions
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddOptions<HackerNewsOptions>()
            .Bind(builder.Configuration.GetSection("HackerNews"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        builder.Services.AddHybridCache();
        
        builder.Services
            .AddScoped<IStoriesService, StoriesService>()
            .AddRefitClient<IHackerNewsClient>()
            .ConfigureHttpClient((sp, client) =>
            {
                var options = sp.GetRequiredService<IOptions<HackerNewsOptions>>().Value;
                
                client.BaseAddress = new Uri(options.Url);
                client.Timeout = options.Timeout;
            });

        return builder;
    }
}