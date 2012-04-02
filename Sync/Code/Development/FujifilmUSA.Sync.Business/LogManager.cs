using System;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Business
{
    public class LogManager
    {
        public static void WriteLog(Exception e, BusinessHandlerRequest request)
        {
            var errorHandlingBusiness = BusinessFactory<IErrorHandlingBusiness>.CreateObject();
            errorHandlingBusiness.
                InsertAppErrorLog(
                    new AppErrorLog()
                        {
                            ExMessage = e.Message,
                            ExStackTracke = e.StackTrace,
                            TimeStamp = DateTime.Now,
                            UserId =
                                (SessionManager.Instance.CurrentUser != null)
                                    ? SessionManager.Instance.CurrentUser.UserId
                                    : 0
                        });
        }
    }
}
