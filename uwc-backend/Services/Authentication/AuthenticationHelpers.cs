using System.Security.Cryptography;
using Models;

namespace Services.Authentication;

public static class AuthenticationHelpers
{
    public static void ProvideSaltAndHash(this Account account)
    {
        var salt = GenerateSalt();
        account.Salt = Convert.ToBase64String(salt);
        account.PasswordHash = ComputeHash(account.PasswordHash, account.Salt);
    }

    private static byte[] GenerateSalt()
    {
        var rng = RandomNumberGenerator.Create();
        var salt = new byte[24];
        rng.GetBytes(salt);
        return salt;
    }

    public static string ComputeHash(string password, string saltString)
    {
        var salt = Convert.FromBase64String(saltString);

        using var hashGenerator = new Rfc2898DeriveBytes(password, salt, 10101);
        var bytes = hashGenerator.GetBytes(24);
        return Convert.ToBase64String(bytes);
    }
}