﻿using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace SledgePlus.WPF.Models.Text;

public static class Cryptography
{
    public static string HashPassword(string password)
    {
        byte[] salt;
        byte[] buffer2;

        if (password == null)
        {
            throw new ArgumentNullException("Не допускается пустое значение");
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
        byte[] buffer4;
        byte[] src = Convert.FromBase64String(hashedPassword);
        if ((src.Length != 0x31) || (src[0] != 0)) throw new ArgumentNullException();

        byte[] dst = new byte[0x10];
        Buffer.BlockCopy(src, 1, dst, 0, 0x10);
        byte[] buffer3 = new byte[0x20];
        Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
        using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
        {
            buffer4 = bytes.GetBytes(0x20);
        }
        return ByteArraysEqual(buffer3, buffer4);
    }

    private static bool ByteArraysEqual(byte[] b1, byte[] b2)
    {
        if (b1 == b2) return true;
        if (b1 == null || b2 == null) return false;
        if (b1.Length != b2.Length) return false;
        return !b1.Where((t, i) => t != b2[i]).Any();
    }
}