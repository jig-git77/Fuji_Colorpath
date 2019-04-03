using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;
namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMiscBusiness : ITaskBase
    {
        int InsertAppErrorLog(AppErrorLog errorLog);
        int InsertLogActivity(LogActivity logActivity);
        List<LogActivity> GetLogActivity(LogActivity logActivity);
        bool Ping();
        int InsertMeasurementAppErrorLog(AppErrorLog errorLog, int ProjectID, string projectType);
    }
}
