using System.Security.Cryptography;
using System.Text;
using System;

namespace FujifilmUSA.Sync.Business
{
    internal class LoginSecurity
    {
        internal static string ErrorCode = "Wrong Input";
        private static readonly string _fujiKey = ApplicationConfiguration.Instance.EncryptionKey;

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
    }
}
