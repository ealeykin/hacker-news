using HackerNews.HackerNews.Application.Handlers.GetTopStories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNews.HackerNews.Application;

public static class Extensions
{
    public static WebApplicationBuilder AddApplication(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<GetTopStoriesHandler>();
        
        return builder;
    }
}