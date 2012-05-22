using System;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.DataAccess;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class MiscBusinessImpl : IMiscBusiness
    {
        #region IErrorHandlingBusiness Members

        public int InsertAppErrorLog(AppErrorLog errorLog)
        {
            int returnValue = 0;
            returnValue = DataAccessFactory.
                Instance.
                ExecuteProcedure(
                    "[dbo].[InsertAppErrorLog]",
                    new object[]
                        {
                            errorLog.TimeStamp,
                            errorLog.ExType,
                            errorLog.ExMessage,
                            errorLog.ExStackTracke,
                            errorLog.UserId
                        });
            return returnValue;
        }

        //public string EncryptPassword(string password, string key)
        //{
        //    return LoginSecurity.Encrypt(password, key);
        //}

        //public string DecryptPassword(string encryptedpassword, string key)
        //{
        //    return LoginSecurity.Decrypt(encryptedpassword, key);
        //}

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}