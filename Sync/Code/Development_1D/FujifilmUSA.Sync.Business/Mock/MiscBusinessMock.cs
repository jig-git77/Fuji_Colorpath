using System;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class MiscBusinessMock : IMiscBusiness
    {
        #region IErrorHandlingBusiness Members

        public int InsertAppErrorLog(Entity.AppErrorLog errorLog)
        {
            return 0;
        }

        public int InsertLogActivity(LogActivity logActivity)
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

        #region IMiscBusiness Members


        public bool Ping()
        {
            return true;
        }

        public System.Collections.Generic.List<LogActivity> GetLogActivity(LogActivity logActivity)
        {
            return new List<LogActivity>();
        }
        #endregion        
    }
}
