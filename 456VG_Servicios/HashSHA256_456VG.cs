﻿using System;
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
        public string GenerarSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
        public string HashPassword(string password, string salt)
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
        public bool VerificarPassword(string passwordIngresada, string hashAlmacenado, string saltAlmacenado)
        {
            string hashIngresado = HashPassword(passwordIngresada, saltAlmacenado);
            return hashIngresado == hashAlmacenado;
        }
    }
}
