using AuthenticationService.Data;
using AuthenticationService.Models;
using System;

namespace AuthenticationService.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IRepository _repository;

    public AuthenticationService(IRepository repository)
    {
        _repository = repository;
    }

    public AuthenticationResult Authenticate(LoginRequest request)
    {
        var user = _repository.GetUserByUsername(request.Username);
        if (user != null && user.Password == request.Password) // Simplified for example
        {
            return new AuthenticationResult
            {
                IsAuthenticated = true,
                Token = GenerateToken(user),
                ExpiresAt = DateTime.UtcNow.AddHours(1)
            };
        }
        return new AuthenticationResult { IsAuthenticated = false };
    }

    private string GenerateToken(User user)
    {
        // Token generation logic here
        return "generated-token";
    }
}