using System;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class MiscBusinessMock : IMiscBusiness
    {
        #region IErrorHandlingBusiness Members

        public int InsertAppErrorLog(Entity.AppErrorLog errorLog)
        {
            return 0;
        }

        public string EncryptPassword(string password, string key)
        {
            return string.Empty;
        }

        public string DecryptPassword(string encryptedpassword, string key)
        {
            return string.Empty;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
