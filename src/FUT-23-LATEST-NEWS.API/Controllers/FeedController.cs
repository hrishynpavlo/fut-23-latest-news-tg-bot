using FUT_23_LATEST_NEWS.FeedFetcher;
using Microsoft.AspNetCore.Mvc;

namespace FUT_23_LATEST_NEWS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedController : ControllerBase
    {
        private readonly IFeedProvider _feed;
        private readonly ILogger<FeedController> _logger;

        public FeedController(IFeedProvider feedProvider, ILogger<FeedController> logger)
        {
            _feed = feedProvider ?? throw new ArgumentNullException(nameof(feedProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("futwiz")]
        public async Task<ActionResult> Get()
        {
            var content = await _feed.FetchAsync();
            return Ok(content);
        }
    }
}