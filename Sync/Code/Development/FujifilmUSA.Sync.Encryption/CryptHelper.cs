using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace FujifilmUSA.Sync.Encryption
{
    public class CryptHelper
    {
        private const string _salt = "$!FujiUSA_!XMF_!SALT$";

        //public static string Encrypt(string strText, string strEncrypt)
        //{
        //    var byKey = new byte[20];
        //    byte[] dv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        //    byKey = Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
        //    var des = new DESCryptoServiceProvider();
        //    var inputArray = System.Text.Encoding.UTF8.GetBytes(strText);
        //    var ms = new MemoryStream();
        //    var cs = new CryptoStream(ms, des.CreateEncryptor(byKey, dv), CryptoStreamMode.Write);
        //    cs.Write(inputArray, 0, inputArray.Length);
        //    cs.FlushFinalBlock();
        //    return Convert.ToBase64String(ms.ToArray());
        //}

        //public static string Decrypt(string strText, string strEncrypt)
        //{
        //    var bKey = new byte[20];
        //    byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        //    bKey = Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
        //    var des = new DESCryptoServiceProvider();
        //    var inputByteArray = Convert.FromBase64String(strText);
        //    var ms = new MemoryStream();
        //    var cs = new CryptoStream(ms, des.CreateDecryptor(bKey, iv), CryptoStreamMode.Write);
        //    cs.Write(inputByteArray, 0, inputByteArray.Length);
        //    cs.FlushFinalBlock();
        //    var encoding = Encoding.UTF8;
        //    return encoding.GetString(ms.ToArray());
        //}

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
                            byte[] utfD1 = Encoding.UTF8.GetBytes(dataToEncrypt);
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
                        return Encoding.UTF8.GetString(decryptedData, 0, decryptedData.Length);
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