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
        public ActionResult Get()
        {
            return Ok(new { status = "OK" });
        }
    }
}