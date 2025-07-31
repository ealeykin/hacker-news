using System.ComponentModel.DataAnnotations;
using HackerNews.HackerNews.Application.Contract;
using HackerNews.HackerNews.Application.Handlers.GetTopStories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.HackerNews.Host.Controllers;

[Authorize]
[Route("api/v1/stories")]
public class StoriesController : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IEnumerable<Story>> GetTopStories(
        [FromServices] GetTopStoriesHandler handler,
        [FromQuery, Range(1, 100)] int count = 10,
        CancellationToken cancellationToken = default)
    {
        return await handler.InvokeAsync(new GetTopStoriesRequest(count), cancellationToken);
    }
}