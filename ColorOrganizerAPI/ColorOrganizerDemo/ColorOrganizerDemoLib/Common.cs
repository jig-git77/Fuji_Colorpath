using System;
namespace ColorOrganizerDemoLib
{
    public class Common
    {
        //TODO: read from config file...
        public static string SecurityToken = "vcf1iZhWzQuNJH26scud4qzm+bZZ7uAKFF7xGIMueAJ+7B0PuRotCnjUYPcX90Q+";
        private const string _xmfColorOrganizerEncryptionKey = "!Fuji$XMF_CO!";
        public const string UserName = "jayparikh";
        private const string _password = "fuji";
        public static Guid AddProjectInfoId;
        public static Guid EditProjectInfoId;
        public static Guid ValidationInfoId;
        public static string FujiWebSite;

        public static string GetEncryptPassword()
        {
            return Service.Common.Encryption.CryptHelper.EncryptString(_password, _xmfColorOrganizerEncryptionKey);
        }

        public delegate void PassCommandToParent(EControl command);
    }
}
