using Microsoft.AspNetCore.Mvc;

namespace BusinessLogicService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet("/health")]
        public IActionResult GetHealth()
        {
            return Ok(new { status = "healthy" });
        }

        [HttpGet("/ready")]
        public IActionResult GetReady()
        {
            return Ok(new { status = "ready" });
        }
    }
}