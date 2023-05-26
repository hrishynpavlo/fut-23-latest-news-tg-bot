using AngleSharp.Html.Parser;
using FUT_23_LATEST_NEWS.Infrastructure.Models;
using Microsoft.Extensions.Logging;

namespace FUT_23_LATEST_NEWS.FeedFetcher.Parsers
{
    public class FutwizParser : IHtmlParser
    {
        private const string NEW_PACKS = "New Packs";
        private const string NEW_SBCS = "New SBCs";

        private readonly ILogger<IHtmlParser> _logger;

        public FutwizParser(ILogger<IHtmlParser> logger) 
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<FutContent>> ParseAsync(string html)
        {
            try
            {
                var parser = new HtmlParser();
                var document = await parser.ParseDocumentAsync(html);

                var contentBlock = document.QuerySelectorAll(".latest-content-container>.latest-content-block");
                var result = new List<FutContent>();

                foreach (var child in contentBlock)
                {
                    var contentTypeName = child.QuerySelector("h3")?.TextContent?.Trim();
                    var futContentType = MapFutContentType(contentTypeName);

                    if (futContentType == FutContentType.Unknown)
                    {
                        continue;
                    }

                    var parsedContents = new List<FutContentItem>();

                    var items = child.QuerySelectorAll(".pack-container");
                    foreach (var item in items)
                    {
                        var packName = item.QuerySelector(".pack-name-txt")?.TextContent?.Trim();
                        var packDescription = item.QuerySelector(".pack-contents>.pack-desc>.pack-desc-txt")?.TextContent?.Trim();
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        private FutContentType MapFutContentType(string? contentName)
        {
            if (string.Equals(NEW_PACKS, contentName, StringComparison.OrdinalIgnoreCase))
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
