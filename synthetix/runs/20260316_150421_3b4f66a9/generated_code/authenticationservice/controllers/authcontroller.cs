using AuthenticationService.Models;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var result = _authenticationService.Authenticate(request);
        if (result.IsAuthenticated)
        {
            return Ok(new { token = result.Token, expires_at = result.ExpiresAt });
        }
        return Unauthorized(new { error = "invalid_credentials" });
    }
}