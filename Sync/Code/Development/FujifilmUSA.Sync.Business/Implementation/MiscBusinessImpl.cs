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

        public int InsertLogActivity(LogActivity logActivity)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[dbo].[InsertLogActivity]",
                            new object[]
                                    {
                                        logActivity.UserId,
                                        Convert.ToInt32(logActivity.ActivityType),
                                        logActivity.IpAddress,
                                        Convert.ToInt32(logActivity.SystemType),
                                        logActivity.Details,
                                        logActivity.ActivityDate
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertLogActivity"));
            return returnValue;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}