using System.Security.Cryptography;

namespace AuthenticationService.Services;

public interface IPasswordHashVerifier
{
    bool Verify(string providedPassword, string storedHash);
}

public sealed class PasswordHashVerifier : IPasswordHashVerifier
{
    public bool Verify(string providedPassword, string storedHash)
    {
        if (string.IsNullOrWhiteSpace(providedPassword) || string.IsNullOrWhiteSpace(storedHash))
        {
            return false;
        }

        var segments = storedHash.Split('.', 3, StringSplitOptions.TrimEntries);
        if (segments.Length != 3)
        {
            return false;
        }

        if (!int.TryParse(segments[0], out var iterations))
        {
            return false;
        }

        try
        {
            var salt = Convert.FromBase64String(segments[1]);
            var expected = Convert.FromBase64String(segments[2]);
            var actual = Rfc2898DeriveBytes.Pbkdf2(
                providedPassword,
                salt,
                iterations,
                HashAlgorithmName.SHA256,
                expected.Length);
            return CryptographicOperations.FixedTimeEquals(actual, expected);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
