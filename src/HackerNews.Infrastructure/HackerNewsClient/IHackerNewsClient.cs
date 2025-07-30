using Refit;

namespace HackerNews.HackerNews.Infrastructure.HackerNewsClient;

public interface IHackerNewsClient
{
    [Get("/v0/beststories.json")]
    Task<int[]> GetTopStoriesAsync(CancellationToken cancellationToken);
    
    [Get("/v0/item/{id}.json")]
    Task<HackerNewsStory> GetStoryAsync(int id, CancellationToken cancellationToken);
}