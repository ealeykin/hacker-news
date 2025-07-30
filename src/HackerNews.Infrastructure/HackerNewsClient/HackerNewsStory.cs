namespace HackerNews.HackerNews.Infrastructure.HackerNewsClient;

public record HackerNewsStory
{
    public required string Title { get; init; }
    
    public required string By { get; init; }
    
    public required string Url { get; init; }
    
    public required int Score { get; init; }
    
    public required int Time { get; init; }
    
    public int Descendants { get; set; }
}