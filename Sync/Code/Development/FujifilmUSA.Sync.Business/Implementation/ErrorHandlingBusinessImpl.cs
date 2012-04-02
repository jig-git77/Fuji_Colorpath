using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.DataAccess;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class ErrorHandlingBusinessImpl : IErrorHandlingBusiness
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

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}