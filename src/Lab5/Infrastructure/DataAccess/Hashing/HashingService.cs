using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Hashing;

[SuppressMessage("StringBuilder", "CA1305", Justification = "Hash transform")]
public static class HashingService
{
    private static readonly byte[] GlobalSalt = new byte[] { 4, 2, 0, 6, 9 };

    public static string GetNewSalt()
    {
        return Guid.NewGuid().ToString();
    }

    public static string GenerateHash(string salt, int number)
    {
        return GenerateHash(salt, number.ToString());
    }

    public static string GenerateHash(string salt, string text)
    {
        byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
        byte[] textBytes = Encoding.UTF8.GetBytes(text);
        return GenerateHashFromBytes(saltBytes, textBytes);
    }

    private static string GenerateHashFromBytes(byte[] salt, byte[] data)
    {
        byte[] dataReversed = data.Reverse().ToArray();
        byte[] value = new byte[GlobalSalt.Length + data.Length + dataReversed.Length + salt.Length];

        GlobalSalt.CopyTo(value, 0);
        data.CopyTo(value, GlobalSalt.Length);
        dataReversed.CopyTo(value, GlobalSalt.Length + data.Length);
        salt.CopyTo(value, GlobalSalt.Length + data.Length + dataReversed.Length);

        byte[] sha256Hash = SHA256.HashData(value);

        var stringBuilder = new StringBuilder();
        foreach (byte b in sha256Hash)
            stringBuilder.AppendFormat("{0:X2}", b);

        return stringBuilder.ToString();
    }
}