using FUT_23_LATEST_NEWS.FeedFetcher;
using Microsoft.AspNetCore.Mvc;

namespace FUT_23_LATEST_NEWS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedController : ControllerBase
    {
        private readonly ILogger<FeedController> _logger;

        public FeedController(ILogger<FeedController> logger)
        {
            _logger = logger;
        }

        [HttpGet("futwiz")]
        public async Task<ActionResult> Get()
        {
            var fetcher = new PupeteerFeedFetcher();
            var content = await fetcher.FetchAsync();
            return Ok(content);
        }
    }
}