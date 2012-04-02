using System;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class ErrorHandlingBusinessMock : IErrorHandlingBusiness
    {
        #region IErrorHandlingBusiness Members

        public int InsertAppErrorLog(Entity.AppErrorLog errorLog)
        {
            return 0;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
