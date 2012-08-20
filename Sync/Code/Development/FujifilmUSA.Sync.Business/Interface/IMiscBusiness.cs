using FujifilmUSA.Sync.Entity;
namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMiscBusiness : ITaskBase
    {
        int InsertAppErrorLog(AppErrorLog errorLog);
        int InsertLogActivity(LogActivity logActivity);
        bool Ping();
    }
}
