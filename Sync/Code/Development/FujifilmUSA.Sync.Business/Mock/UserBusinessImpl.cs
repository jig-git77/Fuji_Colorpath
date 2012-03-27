using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.Interface;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class UserBusinessImpl : IUserBusiness
    {
        #region IUser Members

        public User UserLogin(string userName, string password)
        {
            return new User();
        }
        public List<User> GetUsers(int userId)
        {
            return new List<User>();
        }
        public int InsertUser(User user)
        {
            return 0;
        }
        public int UpdateUser(User user)
        {
            return 0;
        }
        public bool DeleteUser(int userId)
        {
            return false;
        }
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
