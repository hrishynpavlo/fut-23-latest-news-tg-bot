using FUT_23_LATEST_NEWS.FeedFetcher;
using FUT_23_LATEST_NEWS.Notifier.Telegram;
using Microsoft.AspNetCore.Mvc;

namespace FUT_23_LATEST_NEWS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedController : ControllerBase
    {
        private readonly IFeedProvider _feed;
        private readonly IFutContentNotifier _notifier;
        private readonly ILogger<FeedController> _logger;

        public FeedController(IFeedProvider feedProvider, IFutContentNotifier futContentNotifier, ILogger<FeedController> logger)
        {
            _feed = feedProvider ?? throw new ArgumentNullException(nameof(feedProvider));
            _notifier = futContentNotifier ?? throw new ArgumentNullException(nameof(futContentNotifier));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("futwiz")]
        public async Task<ActionResult> Get()
        {
            var content = await _feed.FetchAsync();
            await _notifier.NotifyAllAsync(content);
            return Ok(content);
        }
    }
}