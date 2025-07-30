namespace HackerNews.HackerNews.Application.Contract;

public class Story
{
    public required string Title { get; init; }
    public required string Uri { get; init; }
    public required string PostedBy { get; init; }
    public required DateTimeOffset Time { get; init; }
    public required int Score { get; init; }
    public required int CommentCount { get; init; }
}