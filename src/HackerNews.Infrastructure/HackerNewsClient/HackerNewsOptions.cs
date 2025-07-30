namespace HackerNews.HackerNews.Infrastructure.HackerNewsClient;

public class HackerNewsOptions
{
    public required string Url { get; set; }
    
    public required TimeSpan Timeout { get; set; }
}