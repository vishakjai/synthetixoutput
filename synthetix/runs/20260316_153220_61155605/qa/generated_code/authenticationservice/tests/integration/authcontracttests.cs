using System.Net;
using System.Net.Http.Json;
using AuthenticationService.Models;
using AuthenticationService.Data;
using AuthenticationService.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;

namespace AuthenticationService.Tests.Integration;

public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(Microsoft.AspNetCore.Hosting.IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Replace infrastructure with test doubles
            services.AddSingleton<IConnectionFactory, FakeConnFactory>();
            services.AddScoped<IUserRepository, FakeUserRepository>();
            Environment.SetEnvironmentVariable("JWT_SECRET", "testsecret_1234567890");
        });
    }
}

internal class FakeConnFactory : IConnectionFactory
{
    public ValueTask<Npgsql.NpgsqlConnection> CreateOpenConnectionAsync(CancellationToken ct = default)
        => throw new NotImplementedException();
}

internal class FakeUserRepository : IUserRepository
{
    public Task<UserRecord?> GetUserByUsernameAsync(string username, CancellationToken ct)
    {
        if (username == "validuser")
        {
            // password is "password" hashed
            var hash = BCrypt.Net.BCrypt.HashPassword("password");
            return Task.FromResult<UserRecord?>(new UserRecord(Guid.Parse("11111111-1111-1111-1111-111111111111"), username, hash));
        }
        return Task.FromResult<UserRecord?>(null);
    }
}

public class AuthContractTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public AuthContractTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("http://localhost")
        });
    }

    [Fact]
    public async Task Login_Fails_For_Unknown_User_BR005()
    {
        var req = new LoginRequest { Username = "missing", Password = "whatever" };
        var resp = await _client.PostAsJsonAsync("/auth/login", req);
        resp.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Login_Succeeds_And_Returns_ExpiresAt()
    {
        var req = new LoginRequest { Username = "validuser", Password = "password" };
        var resp = await _client.PostAsJsonAsync("/auth/login", req);
        resp.EnsureSuccessStatusCode();
        var body = await resp.Content.ReadFromJsonAsync<LoginResponse>();
        body.Should().NotBeNull();
        body!.Token.Should().NotBeNullOrWhiteSpace();
        body.ExpiresAt.Should().BeAfter(DateTimeOffset.UtcNow.AddMinutes(-1));
    }
}
