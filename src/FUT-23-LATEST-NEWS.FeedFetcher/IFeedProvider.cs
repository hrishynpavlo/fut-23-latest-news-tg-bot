using FUT_23_LATEST_NEWS.Infrastructure.Models;

namespace FUT_23_LATEST_NEWS.FeedFetcher
{
    public interface IFeedProvider
    {
        Task<List<FutContent>> FetchAsync();
    }
}
