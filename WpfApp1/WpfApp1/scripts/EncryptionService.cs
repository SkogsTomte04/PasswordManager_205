using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.scripts
{
    class EncryptionService
    {
        private readonly byte[] _key;
        
        public static string GenerateAESKey()
        {
            using var aes = Aes.Create();
            aes.KeySize = 256;
            aes.GenerateKey();
            return Convert.ToBase64String(aes.Key);
        }
        public static string Encrypt(byte[] plainBytes, byte[] key)
        {
            using var aes = Aes.Create();
            aes.Key = key;
            aes.GenerateIV();

            using var ms = new MemoryStream();
            ms.Write(aes.IV, 0, aes.IV.Length); // prepend IV

            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(plainBytes, 0, plainBytes.Length);
                cs.FlushFinalBlock();
            }

            return Convert.ToBase64String(ms.ToArray());
        }

        public static byte[] Decrypt(string base64CipherText, byte[] key)
        {
            byte[] fullCipher = Convert.FromBase64String(base64CipherText);

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
}
