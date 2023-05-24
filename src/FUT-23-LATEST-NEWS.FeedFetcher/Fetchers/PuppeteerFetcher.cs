using Microsoft.Extensions.Logging;
using PuppeteerSharp;

namespace FUT_23_LATEST_NEWS.FeedFetcher.Fetchers
{
    public class PuppeteerFetcher : IHtmlFeedFetcher
    {
        private readonly ILogger<IHtmlFeedFetcher> _logger;

        public PuppeteerFetcher(ILogger<IHtmlFeedFetcher> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));   
        }

        public async Task<string> FetchAsync()
        {
            try
            {
                using var browserFetcher = new BrowserFetcher();
                var revision = await browserFetcher.DownloadAsync();
                _logger.LogInformation("{@revision}", revision);

                await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = false });

                await using var page = await browser.NewPageAsync();

                var response = await page.GoToAsync("https://www.futwiz.com/en/daily-content");
                await page.WaitForSelectorAsync(".latest-content-container");
                return await page.GetContentAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }
    }
}
