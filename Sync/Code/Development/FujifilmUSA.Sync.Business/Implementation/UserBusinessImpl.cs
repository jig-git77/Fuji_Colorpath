using System.Linq;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;
using System.Collections.Generic;
using System.Text;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class UserBusinessImpl : IUserBusiness
    {
        public User UserLogin(string userName, string password)
        {
            var objUser = new User();
            Common.ImplementBusinessLogic(
                () =>
                    {
                        var dsUser = DataAccessFactory.Instance.GetDsResult("[CP].[GetUser]", new object[] {userName, password});
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
                                                    Role = new Role()
                                                               {
                                                                    RoleId = DataReader.ReadByte(x, "RoleId"),
                                                                    RolePermissions = GetRolePermissions(dsUser.Tables[1])
                                                               },
                                                    Email = DataReader.ReadString(x, "Email"),
                                                    RegionId = DataReader.ReadByte(x, "RegionId"),
                                                    LastModifiedUserId = DataReader.ReadInt(x, "LastModifiedUserId"),
                                                    LastModifiedDate = DataReader.ReadDate(x, "LastModifiedDate"),
                                                    IsActive = DataReader.ReadBoolean(x, "IsActive")
                                                }).FirstOrDefault();
                        }
                    },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UserLogin"));
            return objUser;
        }
        private static List<RolePermission> GetRolePermissions(DataTable dt)
        {
            var permissions = dt.AsEnumerable().
                 Select(x => new RolePermission()
                 {
                     Permission = (ERolePermission)DataReader.ReadInt(x, "PermissionID"),
                     Allow = DataReader.ReadBoolean(x, "Allow") 
                 }).ToList();
            return permissions;
        }


        public List<User> GetUser(int userId)
        {
            var objUsers = new List<User>();
            Common.ImplementBusinessLogic(
                () =>
                    {
                        var dsUser = DataAccessFactory.
                            Instance.
                            GetDsResult(
                                "[CP].[GetUsers]",
                                new object[]
                                    {
                                        userId
                                    });
                        if (dsUser != null && dsUser.Tables.Count > 0 && dsUser.Tables[0].Rows.Count > 0)
                        {
                            objUsers = dsUser.Tables[0].AsEnumerable().
                                Select(x =>
                                       new User()
                                           {
                                               UserId = DataReader.ReadInt(x, "UserId"),
                                               LoginId = DataReader.ReadString(x, "LoginId"),
                                               LoginPass = DataReader.ReadString(x, "LoginPass"),
                                               LastName = DataReader.ReadString(x, "LastName"),
                                               FirstName = DataReader.ReadString(x, "FirstName"),
                                               Email = DataReader.ReadString(x, "Email"),
                                               Role = new Role() {RoleId = DataReader.ReadByte(x, "RoleId")},
                                               RegionId = DataReader.ReadByte(x, "RegionId"),
                                               LastModifiedUserId = DataReader.ReadInt(x, "LastModifiedUserId"),
                                               LastModifiedDate = DataReader.ReadDate(x, "LastModifiedDate"),
                                               IsActive = DataReader.ReadBoolean(x, "IsActive"),
                                               Region = new Region()
                                                            {
                                                                RegionId = DataReader.ReadByte(x, "RegionId"),
                                                                RegionName = DataReader.ReadString(x, "Region")
                                                            },
                                               UserCompany = new UserCompany()
                                                                   {
                                                                       UserCompanyId = DataReader.ReadInt(x, "UserCompanyId"),
                                                                       CompanyId = DataReader.ReadInt(x, "CompanyId"),
                                                                       UserId = DataReader.ReadInt(x, "UserId"),
                                                                       Company =
                                                                           new Company()
                                                                               {
                                                                                   CompanyId = DataReader.ReadInt(x, "CompanyId"),
                                                                                   CompanyName = DataReader.ReadString(x, "CompanyName")
                                                                               }
                                                                   },
                                               UserBranches = GetBranchList(DataReader.ReadInt(x, "UserId"), dsUser.Tables[1])
                                           }).ToList();
                        }
                    },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetUsers"));
            return objUsers;
        }

        private static List<UserBranch> GetBranchList(int userId, DataTable dt)
        {
            var userbarnch = dt.AsEnumerable().
                 Where(x => DataReader.ReadInt(x, "UserId").Equals(userId)).
                 Select(x => new UserBranch()
                                       {
                                           UserBranchId = DataReader.ReadShort(x, "UserBranchId"),
                                           UserId = DataReader.ReadInt(x, "UserId"),
                                           BranchId = DataReader.ReadInt(x, "BranchId"),
                                           Branch = new Branch()
                                                      {
                                                          BranchId = DataReader.ReadInt(x, "BranchId"),
                                                          BranchName = DataReader.ReadString(x, "BranchName"),
                                                          CompanyId = DataReader.ReadInt(x, "CompanyId"),
                                                          IsActive = DataReader.ReadBoolean(x, "IsActive")
                                                      }
                                       }).ToList();
            return userbarnch;
        }

        public int InsertUser(User user)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                    {
                        string branchIds = GetBranchIds(user.UserBranches);
                        returnValue = DataAccessFactory.
                            Instance.
                            ExecuteProcedure(
                                "[CP].[InsertUser]",
                                new object[]
                                    {
                                        user.LoginId
                                        , user.LoginPass
                                        , user.LastName
                                        , user.FirstName
                                        , user.Email
                                        , user.Role.RoleId
                                        , user.RegionId
                                        , user.LastModifiedUserId
                                        , user.IsActive
                                        , user.UserCompany.CompanyId
                                        , branchIds
                                    });
                    },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertUser"));
            return returnValue;
        }

        public int UpdateUser(User user)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                    {
                        string branchIds = GetBranchIds(user.UserBranches);
                        returnValue = DataAccessFactory.
                            Instance.
                            ExecuteProcedure(
                                "[CP].[UpdateUser]",
                                new object[]
                                    {
                                        user.UserId
                                        , user.LoginId
                                        , user.LoginPass
                                        , user.LastName
                                        , user.FirstName
                                        , user.Email
                                        , user.Role.RoleId
                                        , user.RegionId
                                        , user.LastModifiedUserId
                                        , user.IsActive
                                        , user.UserCompany.CompanyId
                                        , branchIds
                                    });
                    },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateUser"));
            return returnValue;
        }

        private static string GetBranchIds(IEnumerable<UserBranch> userBranches)
        {
            return userBranches.Aggregate(new StringBuilder(),
                                 (sb, v) => sb.Append(v.BranchId).Append(","),
                                 sb => { if (0 < sb.Length) sb.Length--; return sb.ToString(); });

        }

        public bool DeleteUser(int userId)
        {
            Common.ImplementBusinessLogic(
                () => DataAccessFactory.
                          Instance.
                          ExecuteProcedure(
                              "[CP].[DeleteUser]",
                              new object[]
                                  {
                                      userId
                                  }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteUser"));
            return true;
        }

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }

}
