/*
 * DDO.Launcher
 * Copyright (C) 2024 DDO.Launcher Contributors
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published
 * by the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.

 * You should have received a copy of the GNU Affero General Public License
 * along with this program.  If not, see <https://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using MiniCommon.IO;

namespace MiniCommon.Cryptography.Helpers;

public static class CryptographyHelper
{
    /// <summary>
    /// Create a MD5 hash from a filestream, and return as string.
    /// </summary>
    public static string CreateMD5(string fileName, bool formatting) =>
        CreateHash(fileName, formatting, MD5.Create());

    /// <summary>
    /// Create a MD5 hash from a string, and return as string.
    /// </summary>
    public static string CreateMD5(string value, Encoding enc) =>
        CreateHash(value, enc, MD5.Create());

    /// <summary>
    /// Create a SHA1 hash from a filestream, and return as string.
    /// </summary>
    public static string CreateSHA1(string fileName, bool formatting) =>
        CreateHash(fileName, formatting, SHA1.Create());

    /// <summary>
    /// Create a SHA1 hash from a string, and return as string.
    /// </summary>
    public static string CreateSHA1(string value, Encoding enc) =>
        CreateHash(value, enc, SHA1.Create());

    /// <summary>
    /// Create a SHA256 hash from a filestream, and return as string.
    /// </summary>
    public static string CreateSHA256(string fileName, bool formatting) =>
        CreateHash(fileName, formatting, SHA256.Create());

    /// <summary>
    /// Create a SHA256 hash from a string, and return as string.
    /// </summary>
    public static string CreateSHA256(string value, Encoding enc) =>
        CreateHash(value, enc, SHA256.Create());

    /// <summary>
    /// Create a hash from a filestream, and return as string.
    /// </summary>
    public static string CreateHash(string fileName, bool formatting, HashAlgorithm algorithm)
    {
        if (!VFS.Exists(fileName))
            return string.Empty;

        using FileStream stream = VFS.OpenRead(fileName);
        byte[] hash = algorithm.ComputeHash(stream);

        if (formatting)
            return Convert.ToHexStringLower(hash).Replace("-", string.Empty);
        return BitConverter.ToString(hash);
    }

    /// <summary>
    /// Create a hash from a string, and return as string.
    /// </summary>
    public static string CreateHash(string value, Encoding enc, HashAlgorithm algorithm)
    {
        byte[] hash = algorithm.ComputeHash(enc.GetBytes(value));
        StringBuilder stringBuilder = new();
        foreach (byte b in hash)
        {
            stringBuilder.Append(b.ToString("x2"));
        }
        return stringBuilder.ToString();
    }

    /// <summary>
    /// Create an UUID from an MD5 hash, and return as string.
    /// </summary>
    public static string CreateUUID(string value)
    {
        byte[] digestedHash = MD5.HashData(Encoding.UTF8.GetBytes(value));
        digestedHash[6] = (byte)((digestedHash[6] & 0x0f) | 0x30);
        digestedHash[8] = (byte)((digestedHash[8] & 0x3f) | 0x80);
        return Convert.ToHexStringLower(digestedHash).Replace("-", string.Empty);
    }

    /// <summary>
    /// Computes a combined hash code for a sequence of string patterns.
    /// </summary>
    public static int ComputeHash(List<string> patterns)
    {
        unchecked
        {
            int hash = 17;
            foreach (string pattern in patterns)
                hash = (hash * 23) + pattern.GetHashCode();
            return hash;
        }
    }
}
