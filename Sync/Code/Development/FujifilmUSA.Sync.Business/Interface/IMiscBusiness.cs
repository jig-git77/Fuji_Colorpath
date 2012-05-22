using FujifilmUSA.Sync.Entity;
namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMiscBusiness : ITaskBase
    {
        int InsertAppErrorLog(AppErrorLog errorLog);
        //string EncryptPassword(string password, string key);
        //string DecryptPassword(string encryptedpassword, string key);
    }
}
