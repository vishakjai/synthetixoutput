using Microsoft.AspNetCore.Mvc;

namespace Customer and Account.Controllers;

[ApiController]
public sealed class HealthController : ControllerBase
{
    [HttpGet("/health")]
    public IActionResult Health() => Ok(new { status = "healthy" });

    [HttpGet("/ready")]
    public IActionResult Ready() => Ok(new { status = "ready" });
}
