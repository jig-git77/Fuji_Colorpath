using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class UserBusinessImpl : IUserBusiness
    {
        #region IUser Members

        public User UserLogin(string userName, string password)
        {
            return new User();
        }

        #endregion
    }
}
