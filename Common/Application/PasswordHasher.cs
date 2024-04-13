using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace Common.Application;

public class PasswordHasher(IOptions<HashingOptions> options) : IPasswordHasher
{
    private const int _SaltSize = 16;
    private const int _KeySize = 32;
    private HashingOptions Options { get; } = options.Value;

    public string Hash(string password)
    {
        using var algorithm = new Rfc2898DeriveBytes(password, _SaltSize, Options.Iterations, HashAlgorithmName.SHA256);

        var key = Convert.ToBase64String(algorithm.GetBytes(_KeySize));
        var salt = Convert.ToBase64String(algorithm.Salt);
        return $"{Options.Iterations}.{salt}.{key}";
    }

    public (bool verified, bool needsUpgrade) Check(string hash, string password)
    {
        var parts = hash.Split('.', 3);
        if (parts.Length != 3) return (false, false);

        var iterations = Convert.ToInt32(parts[0]);
        var salt = Convert.FromBase64String(parts[1]);
        var key = Convert.FromBase64String(parts[2]);

        var needsUpgrade = iterations != Options.Iterations;

        using var algorithm = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        var keyToCheck = algorithm.GetBytes(_KeySize);

        var verified = keyToCheck.SequenceEqual(key);

        return (verified, needsUpgrade);
    }
}
