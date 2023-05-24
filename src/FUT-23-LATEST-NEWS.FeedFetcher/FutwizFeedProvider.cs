using FUT_23_LATEST_NEWS.FeedFetcher.Fetchers;
using FUT_23_LATEST_NEWS.FeedFetcher.Parsers;
using Microsoft.Extensions.Logging;

namespace FUT_23_LATEST_NEWS.FeedFetcher
{
    public class FutwizFeedProvider : IFeedProvider
    {
        private readonly IHtmlFeedFetcher _feedFetcher;
        private readonly IHtmlParser _htmlParser;
        private readonly ILogger<IFeedProvider> _logger;

        public FutwizFeedProvider(IHtmlFeedFetcher feedFetcher, IHtmlParser htmlParser, ILogger<IFeedProvider> logger) 
        {
            _feedFetcher = feedFetcher ?? throw new ArgumentNullException(nameof(feedFetcher));
            _htmlParser= htmlParser ?? throw new ArgumentNullException(nameof(htmlParser));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<FutContent>> FetchAsync()
        {
            try
            {
                var html = await _feedFetcher.FetchAsync();
                var content = await _htmlParser.ParseAsync(html);

                return content;
            }
            catch
            {
                _logger.LogInformation("");
                return new();
            }
        }
    }
}
