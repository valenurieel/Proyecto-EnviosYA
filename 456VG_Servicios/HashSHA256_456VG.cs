using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace _456VG_Servicios
{
    public class HashSHA256_456VG
    {
        public string GenerarSalt456VG()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
        public string HashPassword456VG(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string combinado = salt + password;
                byte[] bytes = Encoding.UTF8.GetBytes(combinado);
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
        public bool VerificarPassword456VG(string passwordIngresada, string hashAlmacenado, string saltAlmacenado)
        {
            string hashIngresado = HashPassword456VG(passwordIngresada, saltAlmacenado);
            return hashIngresado == hashAlmacenado;
        }
        public string HashSimple456VG(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
                return sb.ToString();
            }
        }
        private const string AES_KEY = "TuClaveSuperSecreta_De32BytesExactos!!";
        private static byte[] GetAesKey(string key, int length = 32)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            Array.Resize(ref keyBytes, length);
            return keyBytes;
        }
        public string EncryptAes(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;
            byte[] encrypted;
            byte[] keyBytes = GetAesKey(AES_KEY, 32);
            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = keyBytes.Take(16).ToArray();
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs, Encoding.UTF8))
                    {
                        sw.Write(plainText);
                    }
                    encrypted = ms.ToArray();
                }
            }
            return Convert.ToBase64String(encrypted);
        }
        public string DecryptAes(string cipherTextBase64)
        {
            if (string.IsNullOrEmpty(cipherTextBase64))
                return string.Empty;
            byte[] cipherBytes = Convert.FromBase64String(cipherTextBase64);
            byte[] keyBytes = GetAesKey(AES_KEY, 32);
            string plaintext;

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = keyBytes.Take(16).ToArray();

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (var ms = new MemoryStream(cipherBytes))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs, Encoding.UTF8))
                {
                    plaintext = sr.ReadToEnd();
                }
            }
            return plaintext;
        }
    }
}
