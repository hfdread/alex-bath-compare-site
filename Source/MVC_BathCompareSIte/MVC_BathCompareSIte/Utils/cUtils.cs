using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MVC_BathCompareSIte.Utils
{
    public static class cUtils
    {
        public static string Format_GTIN(string source)
        {
            var ret = "0";
            if (!string.IsNullOrWhiteSpace(source))
            {
                var dotIndex = source.LastIndexOf('.');
                ret = source.Substring(0, dotIndex);
            }
            return ret;
        }

        public static string Format_Date(string source)
        {
            var ret = "0001-01-01";
            if (!string.IsNullOrWhiteSpace(source))
            {
                ret = source.Substring(0, 10);
            }
            return ret;
        }

        public static string Format_Price(string source)
        {
            var ret = "0";
            if (!string.IsNullOrWhiteSpace(source))
            {
                var usdIndex = source.ToLower().LastIndexOf('u');
                ret = source.Substring(0, usdIndex - 1);
            }

            return ret.Trim();
        }

        static readonly string PasswordHash = "P@@Sw0rd";
        static readonly string SaltKey = "S@LT&KEY";
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";

        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "E31p4Bn12826Mkd6";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "E31p4Bn12826Mkd6";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}