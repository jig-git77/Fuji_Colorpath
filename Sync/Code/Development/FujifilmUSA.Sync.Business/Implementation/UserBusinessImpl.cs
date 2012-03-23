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
                       Role = new Role() { RoleId = DataReader.ReadByte(x, "RoleId") },
                       Email = DataReader.ReadString(x, "Email"),
                       RegionId = DataReader.ReadByte(x, "RegionId"),
                       LastModifiedUserId = DataReader.ReadInt(x, "LastModifiedUserId"),
                       LastModifiedDate = DataReader.ReadDate(x, "LastModifiedDate"),
                       IsActive = DataReader.ReadBoolean(x, "IsActive")
                   }).FirstOrDefault();
            }
            return objUser;
        }

        public List<User> GetUsers(int userId)
        {
            var objUsers = new List<User>();
            var dsUser = DataAccessFactory.Instance.GetDsResult("[CP].[uspGetUsers]", new object[] { userId });

            if (dsUser != null && dsUser.Tables.Count > 0 && dsUser.Tables[0].Rows.Count > 0)
            {
                objUsers = dsUser.Tables[0].AsEnumerable().
                   Select(x => new User()
                   {
                       UserId = DataReader.ReadInt(x, "UserId"),
                       LoginId = DataReader.ReadString(x, "LoginId"),
                       LoginPass = DataReader.ReadString(x, "LoginPass"),
                       LastName = DataReader.ReadString(x, "LastName"),
                       FirstName = DataReader.ReadString(x, "FirstName"),
                       Email = DataReader.ReadString(x, "Email"),
                       Role = new Role() { RoleId = DataReader.ReadByte(x, "RoleId") },
                       RegionId = DataReader.ReadByte(x, "RegionId"),
                       LastModifiedUserId = DataReader.ReadInt(x, "LastModifiedUserId"),
                       LastModifiedDate = DataReader.ReadDate(x, "LastModifiedDate"),
                       IsActive = DataReader.ReadBoolean(x, "IsActive"),
                       Region = new Region()
                       {
                           RegionId = DataReader.ReadByte(x, "RegionId"),
                           RegionName = DataReader.ReadString(x, "Region")
                       },
                       UserCompanies = new UserCompany()
                       {
                           UserCompanyId = DataReader.ReadInt(x, "UserCompanyId"),
                           CompanyId = DataReader.ReadInt(x, "CompanyId"),
                           UserId = DataReader.ReadInt(x, "UserId"),
                           Company = new Company() { CompanyId = DataReader.ReadInt(x, "CompanyId"), CompanyName = DataReader.ReadString(x, "CompanyName") }
                       },
                       UserBranches = BranchList(dsUser.Tables[1])
                   }).ToList();
            }
            return objUsers;
        }

        private List<UserBranch> BranchList(DataTable dt)
        {
            return dt.AsEnumerable().
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
        }

        public int InsertUser(User user)
        {
            int returnValue = 0;
            string branchIds = GetBranchIds(user.UserBranches);
            returnValue = DataAccessFactory.Instance.ExecuteProcedure("[CP].[uspInsertUser]", new object[]{
                                                                                                                user.LoginId
					                                                                                            ,user.LoginPass
					                                                                                            ,user.LastName
					                                                                                            ,user.FirstName
					                                                                                            ,user.Email
					                                                                                            ,user.Role
					                                                                                            ,user.RegionId
					                                                                                            ,user.LastModifiedUserId
					                                                                                            ,user.IsActive
                                                                                                                ,user.UserCompanies.CompanyId
                                                                                                                ,branchIds
                                                                                                });

            return returnValue;
        }

        public int UpdateUser(User user)
        {
            int returnValue = 0;
            string branchIds = GetBranchIds(user.UserBranches);
            returnValue = DataAccessFactory.Instance.ExecuteProcedure("[CP].[uspUpdateUser]", new object[]{
                                                                                                                 user.UserId
                                                                                                                ,user.LoginId
					                                                                                            ,user.LoginPass
					                                                                                            ,user.LastName
					                                                                                            ,user.FirstName
					                                                                                            ,user.Email
					                                                                                            ,user.Role
					                                                                                            ,user.RegionId
					                                                                                            ,user.LastModifiedUserId
					                                                                                            ,user.IsActive
                                                                                                                ,user.UserCompanies.CompanyId
                                                                                                                ,branchIds
                                                                                                });

            return returnValue;
        }


        private string GetBranchIds(List<UserBranch> userBranches)
        {
            return userBranches.Aggregate(new StringBuilder(),
                                 (sb, v) => sb.Append(v.BranchId).Append(","),
                                 sb => { if (0 < sb.Length) sb.Length--; return sb.ToString(); });

        }

        public bool DeleteUser(int userId)
        {
            DataAccessFactory.Instance.ExecuteProcedure("[CP].[uspDeleteUser]", new object[]{
             userId
            });
            return true;
        }


    }

}
