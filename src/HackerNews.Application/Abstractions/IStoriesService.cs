using HackerNews.HackerNews.Application.Contract;

namespace HackerNews.HackerNews.Application.Abstractions;

public interface IStoriesService
{
    public Task<int[]> GetTopStoriesAsync(CancellationToken cancellationToken);
    
    public Task<Story> GetStoryAsync(int id, CancellationToken cancellationToken);
}