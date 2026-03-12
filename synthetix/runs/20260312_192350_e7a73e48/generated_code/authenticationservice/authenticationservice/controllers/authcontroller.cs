using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Placeholder for authentication logic
            if (request.Username == "admin" && request.Password == "password")
            {
                return Ok(new { token = "fake-jwt-token" });
            }
            return Unauthorized();
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}