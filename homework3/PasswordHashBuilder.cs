using System.Security.Cryptography;
using System.Text;

namespace homework3;

public static class PasswordHashBuilder
{
    private static string ComputeSha256Hash(string rawPassword) // использую встроенный алгоритм хеширования SHA256
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawPassword));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
    
    private static string GenerateSalt(int size = 16)
    {
        var rng = new RNGCryptoServiceProvider();
        var buffer = new byte[size];
        rng.GetBytes(buffer);
        return Convert.ToBase64String(buffer);
    }
    
    public static string HashPasswordWithSalt(string password)
    {
        string salt = GenerateSalt();

        string hash = ComputeSha256Hash(password);

        return $"{salt}:{hash}";
    }
    
    public static bool VerifyPassword(string enteredPassword, string storedHashWithSalt)
    {
        var parts = storedHashWithSalt.Split(':');
        if (parts.Length != 2) return false;

        string storedHash = parts[1];

        string hash = ComputeSha256Hash(enteredPassword);

        return hash == storedHash;
    }
}