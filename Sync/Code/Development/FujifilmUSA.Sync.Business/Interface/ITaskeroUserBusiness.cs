using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface ITaskeroUserBusiness : ITaskBase
    {
        TaskeroUser UserLogin(string userName, string password, string allowedIp);
    }
}
