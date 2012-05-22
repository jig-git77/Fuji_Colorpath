using System.Security.Cryptography;
using System.Text;
using System;
using System.Configuration;
namespace FujifilmUSA.Sync.Business
{
    internal class LoginSecurity
    {
        internal static string ErrorCode = "Wrong Input";
        private static readonly string _fujiKey = ConfigurationManager.AppSettings["EncryptionKey"];

        public static string GetDefaultEncryptedPassword()
        {
            return EncryptPasswords(Guid.NewGuid().ToString());
        }

        public static string EncryptPasswords(string text)
        {
            var hasher = new SHA1CryptoServiceProvider();
            byte[] textWithSaltBytes = Encoding.UTF8.GetBytes(string.Concat(text, _fujiKey));
            byte[] hashedBytes = hasher.ComputeHash(textWithSaltBytes);
            hasher.Clear();
            return Convert.ToBase64String(hashedBytes);
        }
        //internal static string Encrypt(string strToEncrypt, string key = null)
        //{
        //    try
        //    {
        //        var objDesCrypto = new TripleDESCryptoServiceProvider();
        //        var objHashMd5 = new MD5CryptoServiceProvider();
        //        var strTempKey = key ?? _fujiKey;
        //        var byteHash = objHashMd5.ComputeHash(Encoding.ASCII.GetBytes(strTempKey));
        //        objDesCrypto.Key = byteHash;
        //        objDesCrypto.Mode = CipherMode.ECB; //CBC, CFB
        //        var byteBuff = Encoding.ASCII.GetBytes(strToEncrypt);
        //        return Convert.ToBase64String(objDesCrypto.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
        //    }
        //    catch (Exception)
        //    {
        //        return ErrorCode;
        //    }
        //}
        //internal static string Decrypt(string strEncrypted, string key = null)
        //{
        //    try
        //    {
        //        var objDesCrypto = new TripleDESCryptoServiceProvider();
        //        var objHashMd5 = new MD5CryptoServiceProvider();
        //        var strTempKey = key ?? _fujiKey;
        //        var byteHash = objHashMd5.ComputeHash(Encoding.ASCII.GetBytes(strTempKey));
        //        objDesCrypto.Key = byteHash;
        //        objDesCrypto.Mode = CipherMode.ECB; //CBC, CFB
        //        var byteBuff = Convert.FromBase64String(strEncrypted);
        //        var strDecrypted = Encoding.ASCII.GetString
        //        (objDesCrypto.CreateDecryptor().TransformFinalBlock
        //        (byteBuff, 0, byteBuff.Length));
        //        return strDecrypted;
        //    }
        //    catch (Exception)
        //    {
        //        return ErrorCode;
        //    }
        //}
    }
}
