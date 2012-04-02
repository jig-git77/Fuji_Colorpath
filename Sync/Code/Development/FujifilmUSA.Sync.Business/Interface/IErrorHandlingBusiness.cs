using FujifilmUSA.Sync.Entity;
namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IErrorHandlingBusiness : ITaskBase
    {
        int InsertAppErrorLog(AppErrorLog errorLog);
    }
}
