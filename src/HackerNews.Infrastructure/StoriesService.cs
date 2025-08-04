using HackerNews.HackerNews.Application.Abstractions;
using HackerNews.HackerNews.Application.Contract;
using HackerNews.HackerNews.Infrastructure.HackerNewsClient;
using Microsoft.Extensions.Caching.Hybrid;

namespace HackerNews.HackerNews.Infrastructure;

public class StoriesService(
    HybridCache cache,
    IHackerNewsClient client) : IStoriesService
{
    private static readonly TimeSpan TopStoriesTtl = TimeSpan.FromMinutes(60);
    private static readonly TimeSpan StoryTtl = TimeSpan.FromMinutes(60);

    private static readonly HybridCacheEntryOptions TopStoriesCacheOptions = new()
    {
        Expiration = TopStoriesTtl,
        LocalCacheExpiration = TopStoriesTtl
    };
    
    private static readonly HybridCacheEntryOptions StoriesCacheOptions = new()
    {
        Expiration = StoryTtl,
        LocalCacheExpiration = StoryTtl
    };

    public async Task<int[]> GetTopStoriesAsync(CancellationToken cancellationToken)
        => await cache.GetOrCreateAsync(
            key: "hacker-news-top-stories",
            factory: async ct => await client.GetTopStoriesAsync(ct),
            options: TopStoriesCacheOptions,
            cancellationToken: cancellationToken);

    public async Task<Story> GetStoryAsync(int id, CancellationToken cancellationToken)
        => await cache.GetOrCreateAsync(
            key: $"hacker-news-story-{id}",
            factory: async ct =>
            {
                var story = await client.GetStoryAsync(id, ct);

                return new Story
                {
                    Title = story.Title,
                    Uri = story.Url,
                    PostedBy = story.By,
                    Time = DateTimeOffset.FromUnixTimeSeconds(story.Time),
                    Score = story.Score,
                    CommentCount = story.Descendants
                };
            },
            options: StoriesCacheOptions,
            cancellationToken: cancellationToken);
}