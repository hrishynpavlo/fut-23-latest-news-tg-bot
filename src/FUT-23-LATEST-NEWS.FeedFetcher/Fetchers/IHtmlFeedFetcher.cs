namespace FUT_23_LATEST_NEWS.FeedFetcher.Fetchers
{
    public interface IHtmlFeedFetcher
    {
        Task<string> FetchAsync();
    }
}
