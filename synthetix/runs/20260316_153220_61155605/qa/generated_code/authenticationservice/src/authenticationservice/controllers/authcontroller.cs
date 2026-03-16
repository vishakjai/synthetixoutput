using AuthenticationService.Models;
using AuthenticationService.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers;

[ApiController]
[Route("auth")] 
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authService;
    private readonly IValidator<LoginRequest> _validator;

    public AuthController(IAuthenticationService authService, IValidator<LoginRequest> validator)
    {
        _authService = authService;
        _validator = validator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        var validation = await _validator.ValidateAsync(request, ct);
        if (!validation.IsValid)
        {
            return ValidationProblem(new ValidationProblemDetails(validation.ToDictionary())
            {
                Title = "Validation failed",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var result = await _authService.AuthenticateAsync(request.Username, request.Password, ct);
        if (!result.Success)
        {
            // BR-005: Threshold rule (no matching user)
            return Problem(title: "Invalid credentials", statusCode: StatusCodes.Status401Unauthorized, detail: result.Error);
        }

        var response = new LoginResponse
        {
            Token = result.Token,
            ExpiresAt = result.ExpiresAt,
            UserId = result.UserId,
            Username = result.Username
        };

        return Ok(response);
    }
}
