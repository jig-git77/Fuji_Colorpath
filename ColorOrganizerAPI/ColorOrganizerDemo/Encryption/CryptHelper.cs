using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace FujifilmUSA.Sync.Encryption
{
    public class CryptHelper
    {
        public static string Encrypt(string strText, string strEncrypt)
        {
            var byKey = new byte[20];
            byte[] dv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            byKey = Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
            var des = new DESCryptoServiceProvider();
            var inputArray = System.Text.Encoding.UTF8.GetBytes(strText);
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, des.CreateEncryptor(byKey, dv), CryptoStreamMode.Write);
            cs.Write(inputArray, 0, inputArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string strText, string strEncrypt)
        {
            var bKey = new byte[20];
            byte[] iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
            bKey = Encoding.UTF8.GetBytes(strEncrypt.Substring(0, 8));
            var des = new DESCryptoServiceProvider();
            var inputByteArray = Convert.FromBase64String(strText);
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, des.CreateDecryptor(bKey, iv), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            var encoding = Encoding.UTF8;
            return encoding.GetString(ms.ToArray());
        }
    }
}