using System.Linq;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class UserBusinessImpl : IUserBusiness
    {
        public User UserLogin(string userName, string password)
        {
            var objUser = new User();
            var dsUser = DataAccessFactory.Instance.GetDsResult("[uspGetUser]", new object[] { userName, password });

            if (dsUser != null && dsUser.Tables.Count > 0 && dsUser.Tables[0].Rows.Count > 0)
            {
                objUser = dsUser.Tables[0].AsEnumerable().
                   Select(x => new User()
                   {
                       UserId = DataReader.ReadInt(x, "UserId"),
                       LoginId = DataReader.ReadString(x, "LoginId"),
                       LoginPass = DataReader.ReadString(x, "LoginPass"),
                       LastName = DataReader.ReadString(x, "LastName"),
                       FirstName = DataReader.ReadString(x, "FirstName"),
                       RoleId = DataReader.ReadByte(x, "RoleId"),
                       Email = DataReader.ReadString(x, "Email"),
                       RegionId = DataReader.ReadByte(x, "RegionId"),
                       LastModifiedUserId = DataReader.ReadInt(x, "LastModifiedUserId"),
                       LastModifiedDate = DataReader.ReadDate(x, "LastModifiedDate"),
                       IsActive = DataReader.ReadBoolean(x, "IsActive")
                   }).FirstOrDefault();
            }
            return objUser;
        }
    }

}
