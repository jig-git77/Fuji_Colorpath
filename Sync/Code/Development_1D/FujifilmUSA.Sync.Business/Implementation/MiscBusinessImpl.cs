using System;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using FujifilmUSA.Sync.Common;

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

        public List<LogActivity> GetLogActivity(LogActivity logActivity)
        {
            var logActivityList = new List<LogActivity>();

            Common.ImplementBusinessLogic(
                () =>
                {
                    var logList = DataAccessFactory.Instance.GetDsResult("[dbo].[GetLogActivity]",
                                                                              new object[]
                                                                                      {
                                                                                          logActivity.UserId,
                                                                                          (int)logActivity.ActivityType,
                                                                                          logActivity.SystemType,
                                                                                          logActivity.ActivityDateFrom,
                                                                                          logActivity.ActivityDateTo
                                                                                      });

                    if (logList != null && logList.Tables.Count > 0 && logList.Tables[0].Rows.Count > 0)
                    {
                        logActivityList = logList.Tables[0].AsEnumerable().
                           Select(x => new LogActivity()
                           {
                               LogActivityId = DataReader.ReadInt(x, "LogActivityID"),
                               UserId = DataReader.ReadInt(x, "UserID"),
                               ActivityType = (LogActivityType)DataReader.ReadInt(x, "ActivityType"),
                               IpAddress = DataReader.ReadString(x, "IPAddress"),
                               SystemType = (LogActivitySystemType)DataReader.ReadInt(x, "SystemType"),
                               Details = DataReader.ReadString(x, "Details"),
                               ActivityDate = DataReader.ReadDate(x, "ActivityDate")
                           }).ToList();
                    }
                }, new BusinessHandlerRequest(this, ErrorOccured, ""));
            return logActivityList;
        }
        #endregion

        #region Browser Test methods
        public bool Ping()
        {
            bool returnValue = false;
            returnValue=Utilities.GetProfilerServiceClientCall<string,bool>(string.Empty,Utilities.ProfilerCallMethod.Ping,Utilities.ProfilerRequestCall.HttpGet);
            //using (ProfilerService.ProfilerClient client = new ProfilerService.ProfilerClient())
            //{
            //    
            //    Common.ImplementBusinessLogic(() =>
            //         returnValue = client.Ping(),
            //      new BusinessHandlerRequest(this, ErrorOccured, string.Empty));
            //}
            return returnValue;
        }
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}