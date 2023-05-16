using AngleSharp.Html.Parser;
using PuppeteerSharp;

namespace FUT_23_LATEST_NEWS.FeedFetcher
{
    public class PupeteerFeedFetcher : IFeedFetcher
    {
        private const string NEW_PACKS = "New Packs";
        private const string NEW_SBCS = "New SBCs";

        public async Task<List<FutContent>> FetchAsync()
        {
            using var browserFetcher = new BrowserFetcher();
            var revision = await browserFetcher.DownloadAsync();
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = false });
            await using var page = await browser.NewPageAsync();
            var response = await page.GoToAsync("https://www.futwiz.com/en/daily-content");
            await page.WaitForSelectorAsync(".latest-content-container");
            var html = await page.GetContentAsync();

            var parser = new HtmlParser();
            var document = await parser.ParseDocumentAsync(html);
            var contentChildren = document.QuerySelectorAll(".latest-content-container>.latest-content-block");

            var result = new List<FutContent>();

            foreach(var child in contentChildren)
            {
                var contentTypeName = child.QuerySelector("h3")?.TextContent?.Trim();
                var futContentType = MapFutContentType(contentTypeName);

                if(futContentType == FutContentType.Unknown)
                {
                    continue; 
                }

                var parsedContents = new List<FutContentItem>();

                var packs = child.QuerySelectorAll(".pack-container");
                foreach(var pack in packs)
                {
                    var packName = pack.QuerySelector(".pack-name-txt")?.TextContent?.Trim();
                    var packDescription = pack.QuerySelector(".pack-contents>.pack-desc>.pack-desc-txt")?.TextContent?.Trim();
                    parsedContents.Add(new FutContentItem 
                    {
                        Name = packName,
                        Description = packDescription
                    });
                }

                result.Add(new FutContent 
                {
                    Type = futContentType,
                    ContentItems = parsedContents,
                    Name = contentTypeName
                });
            }

            return result;
        }

        private FutContentType MapFutContentType(string? contentName)
        {
            if(string.Equals(NEW_PACKS, contentName, StringComparison.OrdinalIgnoreCase))
            {
                return FutContentType.NewPacks;
            }

            if (string.Equals(NEW_SBCS, contentName, StringComparison.OrdinalIgnoreCase))
            {
                return FutContentType.NewSBCs;
            }

            return FutContentType.Unknown;
        }
    }
}
