using System.Security.Cryptography;

namespace SledgePlus.WPF.Models.Math;

public class Cryptography
{
    public static string HashPassword(string password)
    {
        byte[] salt;
        byte[] buffer2;

        if (password == null)
        {
            throw new ArgumentNullException("password");
        }

        using (Rfc2898DeriveBytes bytes = new(password, 0x10, 0x3e8))
        {
            salt = bytes.Salt;
            buffer2 = bytes.GetBytes(0x20);
        }

        var dst = new byte[0x31];
        Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
        Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);

        return Convert.ToBase64String(dst);
    }

    public static bool VerifyHashedPassword(string? hashedPassword, string password)
    {
        string? buffer4;

        if (hashedPassword == null) return false;
        if (password == null) throw new ArgumentNullException("password");

        var src = hashedPassword;
        if ((src.Length != 0x31) || (src[0] != 0)) return false;

        var dst = new byte[0x10];
        Buffer.BlockCopy(src.ToCharArray(), 1, dst, 0, 0x10);
        var buffer3 = new byte[0x20];
        Buffer.BlockCopy(src.ToCharArray(), 0x11, buffer3, 0, 0x20);
        using (Rfc2898DeriveBytes bytes = new(password, dst, 0x3e8))
        {
            buffer4 = bytes.GetBytes(0x20).ToString();
        }

        return Equals(buffer3, buffer4);

    }
}