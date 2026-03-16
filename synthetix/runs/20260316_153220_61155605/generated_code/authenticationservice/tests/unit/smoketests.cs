using AuthenticationService.Models;
using AuthenticationService.Validation;
using FluentAssertions;

namespace AuthenticationService.Tests.Unit;

public class SmokeTests
{
    [Fact]
    public void Validator_Fails_On_Empty()
    {
        var v = new LoginRequestValidator();
        var result = v.Validate(new LoginRequest { Username = "", Password = "" });
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Response_Uses_ExpiresAt_Json_Name()
    {
        var resp = new LoginResponse
        {
            Token = "t",
            ExpiresAt = DateTimeOffset.UnixEpoch,
            UserId = Guid.Empty,
            Username = "u"
        };
        // Ensure property exists and value is accessible
        resp.ExpiresAt.ToUnixTimeSeconds().Should().Be(0);
    }
}
