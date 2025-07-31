using System.Collections.Concurrent;
using HackerNews.HackerNews.Application.Abstractions;
using HackerNews.HackerNews.Application.Contract;
using Microsoft.Extensions.Logging;

namespace HackerNews.HackerNews.Application.Handlers.GetTopStories;

public class GetTopStoriesHandler(
    ILogger<GetTopStoriesHandler> logger,
    IStoriesService storiesService) : IHandler<GetTopStoriesRequest, IReadOnlyCollection<Story>>
{
    private const int MaxDegreeOfParallelism = 4;
    
    public async Task<IReadOnlyCollection<Story>> InvokeAsync(GetTopStoriesRequest request, CancellationToken cancellationToken)
    {
        var topStories = await storiesService.GetTopStoriesAsync(cancellationToken);
        var storyIds = topStories.Take(request.Count);

        var result = new ConcurrentBag<Story>();
        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = MaxDegreeOfParallelism,
            CancellationToken = cancellationToken
        };

        await Parallel.ForEachAsync(storyIds, options, async (storyId, ct) =>
        {
            try
            {
                result.Add(await storiesService.GetStoryAsync(storyId, ct));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get story. StoryId: {StoryId}", storyId);
            }
        });
        
        return result
            .OrderByDescending(x => x.Score)
            .ToList();
    }
}