namespace FUT_23_LATEST_NEWS.FeedFetcher
{
    public interface IFeedFetcher
    {
        Task<List<FutContent>> FetchAsync();
    }
}
