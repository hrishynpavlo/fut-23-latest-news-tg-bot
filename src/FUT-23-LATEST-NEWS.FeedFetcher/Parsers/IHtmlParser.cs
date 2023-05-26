using FUT_23_LATEST_NEWS.Infrastructure.Models;

namespace FUT_23_LATEST_NEWS.FeedFetcher.Parsers
{
    public interface IHtmlParser
    {
        Task<List<FutContent>> ParseAsync(string html);
    }
}
