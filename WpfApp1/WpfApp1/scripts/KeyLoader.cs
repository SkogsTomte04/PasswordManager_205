using System;
using System.IO;
using System.Security.Cryptography;

public static class KeyLoader
{
    private static readonly string BaseAppDataPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "PasswordManager",
        "Users");

    // --- Public Setup ---

    public static void FirstTimeSetup(string userId, string password)
    {
        string userPath = GetUserPath(userId);
        Directory.CreateDirectory(userPath);

        byte[] salt = GenerateSalt();
        File.WriteAllBytes(Path.Combine(userPath, "user_salt.bin"), salt);

        byte[] masterKey = DeriveMasterKey(password, salt);
        byte[] dataKey = GenerateRandomKey(32); // AES-256 key

        string encrypted = Encrypt(dataKey, masterKey);
        File.WriteAllText(Path.Combine(userPath, "user_data_key.aes"), encrypted);
    }

    public static byte[] LoadDataKey(string userId, string password)
    {
        string userPath = GetUserPath(userId);

        if (!File.Exists(Path.Combine(userPath, "user_salt.bin")) ||
            !File.Exists(Path.Combine(userPath, "user_data_key.aes")))
            throw new Exception("User data not initialized.");

        byte[] salt = File.ReadAllBytes(Path.Combine(userPath, "user_salt.bin"));
        byte[] masterKey = DeriveMasterKey(password, salt);

        string encryptedDataKey = File.ReadAllText(Path.Combine(userPath, "user_data_key.aes"));
        return DecryptToBytes(encryptedDataKey, masterKey);
    }

    public static bool IsUserInitialized(string userId)
    {
        string userPath = GetUserPath(userId);
        return File.Exists(Path.Combine(userPath, "user_salt.bin")) &&
               File.Exists(Path.Combine(userPath, "user_data_key.aes"));
    }

    // --- Internal Helpers ---

    private static string GetUserPath(string userId)
    {
        return Path.Combine(BaseAppDataPath, userId);
    }

    private static byte[] GenerateSalt(int size = 16) => RandomNumberGenerator.GetBytes(size);

    private static byte[] GenerateRandomKey(int bytes = 32) => RandomNumberGenerator.GetBytes(bytes);

    private static byte[] DeriveMasterKey(string password, byte[] salt, int iterations = 100_000)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        return pbkdf2.GetBytes(32); // 256-bit key
    }

    public static string Encrypt(byte[] plainBytes, byte[] key)
    {
        using var aes = Aes.Create();
        aes.Key = key;
        aes.GenerateIV();

        using var ms = new MemoryStream();
        ms.Write(aes.IV, 0, aes.IV.Length);

        using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
        {
            cs.Write(plainBytes, 0, plainBytes.Length);
            cs.FlushFinalBlock();
        }

        return Convert.ToBase64String(ms.ToArray());
    }

    public static byte[] DecryptToBytes(string base64Cipher, byte[] key)
    {
        byte[] fullCipher = Convert.FromBase64String(base64Cipher);

        using var aes = Aes.Create();
        aes.Key = key;

        byte[] iv = new byte[aes.BlockSize / 8];
        Array.Copy(fullCipher, iv, iv.Length);
        aes.IV = iv;

        using var ms = new MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length);
        using var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
        using var result = new MemoryStream();
        cs.CopyTo(result);

        return result.ToArray();
    }
}
