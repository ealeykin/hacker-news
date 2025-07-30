namespace HackerNews.HackerNews.Application.Abstractions;

public interface IHandler<in TRequest, TResponse>
{
    public Task<TResponse> InvokeAsync(TRequest request, CancellationToken cancellationToken);
}