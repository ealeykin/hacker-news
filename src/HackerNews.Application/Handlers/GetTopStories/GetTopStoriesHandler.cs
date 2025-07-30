using System.Collections.Concurrent;
using HackerNews.HackerNews.Application.Abstractions;
using HackerNews.HackerNews.Application.Contract;

namespace HackerNews.HackerNews.Application.Handlers.GetTopStories;

public class GetTopStoriesHandler(IStoriesService storiesService) : IHandler<GetTopStoriesRequest, IReadOnlyCollection<Story>>
{
    private const int MacDegreeOfParallelism = 4;
    
    public async Task<IReadOnlyCollection<Story>> InvokeAsync(GetTopStoriesRequest request, CancellationToken cancellationToken)
    {
        var topStories = await storiesService.GetTopStoriesAsync(cancellationToken);
        var storyIds = topStories.Take(request.Count);

        var result = new ConcurrentBag<Story>();
        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = MacDegreeOfParallelism,
            CancellationToken = cancellationToken
        };

        await Parallel.ForEachAsync(storyIds, options, async (storyId, ct) =>
        {
            result.Add(await storiesService.GetStoryAsync(storyId, ct));
        });
        
        return result
            .OrderByDescending(x => x.Score)
            .ToList();
    }
}