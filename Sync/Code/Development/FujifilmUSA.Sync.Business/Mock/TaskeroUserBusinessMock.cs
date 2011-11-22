using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class TaskeroUserBusinessMock : ITaskeroUserBusiness
    {
        #region IBusTaskeroUser Members

        public TaskeroUser UserLogin(string userName, string password, string allowedIp)
        {
            return new TaskeroUser();
        }

        #endregion
    }
}
