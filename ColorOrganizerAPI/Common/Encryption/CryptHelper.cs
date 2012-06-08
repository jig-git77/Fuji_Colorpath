using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Service.Common.Encryption
{
    public class CryptHelper
    {
        private const string _salt = "$!FujiUSA_!XMF_!SALT$";

        /// <summary>
        /// Encrypt a string using AES
        /// </summary>
        /// <param name="dataToEncrypt">String to encrypt</param>
        /// <param name="key">Encryption password</param>
        /// <returns>Encrypted string in case of success; otherwise - empty string</returns>
        public static string EncryptString(string dataToEncrypt, string key)
        {
            try
            {
                using (Aes aes = new AesManaged())
                {
                    var deriveBytes = new Rfc2898DeriveBytes(key, Encoding.UTF8.GetBytes(_salt));
                    aes.Key = deriveBytes.GetBytes(128 / 8);
                    aes.IV = aes.Key;
                    using (var encryptionStream = new MemoryStream())
                    {
                        using (var encrypt = new CryptoStream(encryptionStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            byte[] utfD1 = UTF8Encoding.UTF8.GetBytes(dataToEncrypt);
                            encrypt.Write(utfD1, 0, utfD1.Length);
                            encrypt.FlushFinalBlock();
                        }
                        return Convert.ToBase64String(encryptionStream.ToArray());
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Decrypt encrypted string
        /// </summary>
        /// <param name="dataToDencrypt">Encrypted string</param>
        /// <param name="key">Password used for encryption</param>
        /// <returns>Decrypted string if success; otherwise - empty string</returns>
        public static string DecryptString(string dataToDencrypt, string key)
        {
            try
            {
                using (Aes aes = new AesManaged())
                {
                    var deriveBytes = new Rfc2898DeriveBytes(key, Encoding.UTF8.GetBytes(_salt));
                    aes.Key = deriveBytes.GetBytes(128 / 8);
                    aes.IV = aes.Key;
                    using (var decryptionStream = new MemoryStream())
                    {
                        using (var decrypt = new CryptoStream(decryptionStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            byte[] encryptedData = Convert.FromBase64String(dataToDencrypt);
                            decrypt.Write(encryptedData, 0, encryptedData.Length);
                            decrypt.Flush();
                        }
                        byte[] decryptedData = decryptionStream.ToArray();
                        return UTF8Encoding.UTF8.GetString(decryptedData, 0, decryptedData.Length);
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}