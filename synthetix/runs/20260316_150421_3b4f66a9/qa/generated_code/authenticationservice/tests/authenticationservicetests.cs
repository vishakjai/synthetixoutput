using AuthenticationService.Models;
using AuthenticationService.Services;
using Moq;
using Xunit;

namespace AuthenticationService.Tests;

public class AuthenticationServiceTests
{
    [Fact]
    public void Authenticate_ValidCredentials_ReturnsAuthenticatedResult()
    {
        var mockRepo = new Mock<IRepository>();
        mockRepo.Setup(repo => repo.GetUserByUsername("validUser")).Returns(new User { Username = "validUser", Password = "validPass" });

        var authService = new AuthenticationService(mockRepo.Object);
        var result = authService.Authenticate(new LoginRequest { Username = "validUser", Password = "validPass" });

        Assert.True(result.IsAuthenticated);
        Assert.NotNull(result.Token);
        Assert.True(result.ExpiresAt > DateTime.UtcNow);
    }

    [Fact]
    public void Authenticate_InvalidCredentials_ReturnsUnauthenticatedResult()
    {
        var mockRepo = new Mock<IRepository>();
        mockRepo.Setup(repo => repo.GetUserByUsername("invalidUser")).Returns((User?)null);

        var authService = new AuthenticationService(mockRepo.Object);
        var result = authService.Authenticate(new LoginRequest { Username = "invalidUser", Password = "invalidPass" });

        Assert.False(result.IsAuthenticated);
    }
}