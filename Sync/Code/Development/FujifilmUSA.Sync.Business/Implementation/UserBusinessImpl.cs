using System.Linq;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;
using System.Collections.Generic;
using FujifilmUSA.Sync.Business.CustomExceptions;
using System;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class UserBusinessImpl : IUserBusiness
    {
        public User UserLogin(string userName, string password, UserType userType)
        {
            var user = new User();
            Common.ImplementBusinessLogic(
                () =>
                {
                    if (userType == UserType.XmfColorOrganizerUser)
                    {
                        password = Encryption.CryptHelper.Decrypt(password, Common.XmfColorOrganizerEncryptionKey);
                    }
                    var dsUser = DataAccessFactory.Instance.GetDsResult("[CP].[GetLoginInformation]",
                                                                        new object[] { userName, LoginSecurity.EncryptPasswords(password), Convert.ToInt32(userType) });
                    if (dsUser != null && dsUser.Tables.Count > 0 && dsUser.Tables[0].Rows.Count > 0)
                    {
                        user = new User()
                                   {
                                       UserId = DataReader.ReadInt(dsUser.Tables[0].Rows[0], "UserId"),
                                       LoginId = DataReader.ReadString(dsUser.Tables[0].Rows[0], "LoginId"),
                                       LastName = DataReader.ReadString(dsUser.Tables[0].Rows[0], "LastName"),
                                       FirstName = DataReader.ReadString(dsUser.Tables[0].Rows[0], "FirstName"),
                                       Role = new Role()
                                                  {
                                                      RoleId = DataReader.ReadShort(dsUser.Tables[0].Rows[0], "RoleId"),
                                                  },
                                       Email = DataReader.ReadString(dsUser.Tables[0].Rows[0], "Email"),
                                       RegionId = DataReader.ReadShort(dsUser.Tables[0].Rows[0], "RegionId"),
                                       LastModifiedUserId = DataReader.ReadInt(dsUser.Tables[0].Rows[0], "LastModifiedUserId"),
                                       LastModifiedDate = DataReader.ReadDate(dsUser.Tables[0].Rows[0], "LastModifiedDate"),
                                       IsActive = DataReader.ReadBoolean(dsUser.Tables[0].Rows[0], "IsActive"),
                                       AuthenticationKey = DataReader.ReadGuid(dsUser.Tables[0].Rows[0], "AuthenticationKey"),
                                       SecurityQuestionId = DataReader.ReadInt(dsUser.Tables[0].Rows[0], "SecurityQuestionID"),
                                       SecurityAnswer = DataReader.ReadString(dsUser.Tables[0].Rows[0], "SecurityAnswer"),
                                       IsActivated = DataReader.ReadBoolean(dsUser.Tables[0].Rows[0], "IsActivated"),
                                   };
                        user.Role.RolePermissions = dsUser.Tables[1].AsEnumerable().
                            Select(x => new RolePermission()
                                            {
                                                Permission = (ERolePermission)DataReader.ReadInt(x, "PermissionID"),
                                                Allow = DataReader.ReadBoolean(x, "Allow")
                                            }).ToList();
                        if (dsUser.Tables[2].Rows.Count > 0)
                        {
                            user.UserCompany = new UserCompany()
                                                   {
                                                       CompanyId = DataReader.ReadInt(dsUser.Tables[2].Rows[0], "CompanyID"),
                                                       Company = new Company()
                                                                     {
                                                                         CompanyId = DataReader.ReadInt(dsUser.Tables[2].Rows[0], "CompanyID"),
                                                                         CompanyName = DataReader.ReadString(dsUser.Tables[2].Rows[0], "CompanyName")
                                                                     }
                                                   };
                        }
                        if (dsUser.Tables[3].Rows.Count > 0)
                        {
                            user.UserBranches = dsUser.Tables[3].AsEnumerable().
                                Select(x => new UserBranch()
                                                {
                                                    BranchId = DataReader.ReadInt(x, "BranchID"),
                                                    Branch = new Branch()
                                                                 {
                                                                     BranchId = DataReader.ReadInt(x, "BranchID"),
                                                                     BranchName = DataReader.ReadString(x, "BranchName")
                                                                 }
                                                }).ToList();
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UserLogin"));
            return user;
        }

        public List<User> GetUser(int userId, int currentUserId)
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
                                        userId,currentUserId
                                    });
                    if (dsUser != null && dsUser.Tables.Count > 0 && dsUser.Tables[0].Rows.Count > 0)
                    {
                        objUsers = dsUser.Tables[0].AsEnumerable().
                            Select(x =>
                                   new User()
                                       {
                                           UserId = DataReader.ReadInt(x, "UserId"),
                                           LoginId = DataReader.ReadString(x, "LoginId"),
                                           //LoginPass = LoginSecurity.Decrypt(DataReader.ReadString(x, "LoginPass")),
                                           LastName = DataReader.ReadString(x, "LastName"),
                                           FirstName = DataReader.ReadString(x, "FirstName"),
                                           Email = DataReader.ReadString(x, "Email"),
                                           Role = new Role() { RoleId = DataReader.ReadByte(x, "RoleId") },
                                           RegionId = DataReader.ReadShort(x, "RegionId"),
                                           LastModifiedUserId = DataReader.ReadInt(x, "LastModifiedUserId"),
                                           LastModifiedDate = DataReader.ReadDate(x, "LastModifiedDate"),
                                           IsActive = DataReader.ReadBoolean(x, "IsActive"),
                                           AuthenticationKey = DataReader.ReadGuid(x, "AuthenticationKey"),
                                           SecurityQuestionId = DataReader.ReadInt(x, "SecurityQuestionID"),
                                           SecurityAnswer = DataReader.ReadString(x, "SecurityAnswer"),
                                           IsActivated = DataReader.ReadBoolean(x, "IsActivated"),
                                           UserType = DataReader.ReadInt(x, "UserType"),
                                           Region = new Region()
                                                        {
                                                            RegionId = DataReader.ReadByte(x, "CompanyRegionId"),
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
                    user.AuthenticationKey = Guid.NewGuid();
                    user.IsActivated = false;
                    string branchIds = GetBranchIds(user.UserBranches);
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[InsertUser]",
                            new object[]
                                    {
                                        user.LoginId
                                        , LoginSecurity.GetDefaultEncryptedPassword()
                                        , user.LastName
                                        , user.FirstName
                                        , user.Email
                                        , user.Role.RoleId
                                        , user.RegionId
                                        , user.LastModifiedUserId
                                        , user.IsActive
                                        , user.UserCompany.CompanyId
                                        , branchIds
                                        , user.AuthenticationKey
                                        , user.IsActivated
                                        , user.UserType
                                    });
                    if (returnValue == -1)
                    {
                        throw new DuplicateException() { UserFriendlyMessage = "Duplicate_InsertUser" };
                    }
                    else
                    {
                        LogManager.SendUserCreatedEmail(user);
                    }
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
                                        //, LoginSecurity.Encrypt(user.LoginPass)
                                        , user.LastName
                                        , user.FirstName
                                        , user.Email
                                        , user.Role.RoleId
                                        , user.RegionId
                                        , user.LastModifiedUserId
                                        , user.IsActive
                                        , user.UserCompany.CompanyId
                                        , branchIds
                                        , user.UserType
                                    });
                    if (returnValue == -1)
                    {
                        throw new DuplicateException() { UserFriendlyMessage = "Duplicate_InsertUser" };
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateUser"));
            return returnValue;
        }

        private static string GetBranchIds(IEnumerable<UserBranch> userBranches)
        {
            return string.Join(",", userBranches.Select(x => x.BranchId).ToList());
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

        public User GetUserAuthenticationInformation(Guid authenticationKey)
        {
            var user = new User();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsUser = DataAccessFactory.Instance.GetDsResult("[CP].[GetUserAuthenticationInformation]",
                                                                        new object[] { authenticationKey });
                    if (dsUser != null && dsUser.Tables.Count > 0 && dsUser.Tables[0].Rows.Count > 0)
                    {
                        user = new User()
                                   {
                                       UserId = DataReader.ReadInt(dsUser.Tables[0].Rows[0], "UserId"),
                                       LoginId = DataReader.ReadString(dsUser.Tables[0].Rows[0], "LoginId"),
                                       LastName = DataReader.ReadString(dsUser.Tables[0].Rows[0], "LastName"),
                                       FirstName = DataReader.ReadString(dsUser.Tables[0].Rows[0], "FirstName"),
                                       Email = DataReader.ReadString(dsUser.Tables[0].Rows[0], "Email"),
                                       IsActivated = DataReader.ReadBoolean(dsUser.Tables[0].Rows[0], "IsActivated"),
                                       SecurityQuestionId = DataReader.ReadInt(dsUser.Tables[0].Rows[0], "SecurityQuestionId"),
                                       SecurityAnswer = DataReader.ReadString(dsUser.Tables[0].Rows[0], "SecurityAnswer"),
                                       AuthenticationKey = authenticationKey
                                   };
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UserLogin"));
            return user;
        }

        public int UpdateUserAuthenticationInformation(User user, UserAuthenticationUpdateType updateType)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    user.IsActivated = true;
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[UpdateUserAuthenticationInformation]",
                            new object[]
                                    {
                                        user.AuthenticationKey
                                        , user.SecurityQuestionId
                                        , user.SecurityAnswer
                                        , LoginSecurity.EncryptPasswords(user.LoginPass)
                                        , user.IsActivated
                                        , Convert.ToInt32(updateType)
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateUser"));
            return returnValue;
        }

        public User UpdatePreResetPasswordInformation(string loginId)
        {
            var user = new User { LoginId = loginId, AuthenticationKey = Guid.NewGuid() };
            var isActivatedOutParam = new Parameters() { ParameterName = "@IsActivated", Type = DbType.Boolean };
            var emailOutParam = new Parameters() { ParameterName = "@Email", Size = 50, Type = DbType.String };
            var isExistsOutParam = new Parameters() { ParameterName = "@IsExists", Type = DbType.Boolean };
            Common.ImplementBusinessLogic(
                () => DataAccessFactory.
                          Instance.
                          ExecuteProcedure(
                              "[CP].[UpdatePreResetPasswordInformation]",
                              new List<Parameters>()
                                  {
                                      new Parameters() {ParameterName = "@LoginID", Size = 30, Type = DbType.String, Value = user.LoginId},
                                      new Parameters() {ParameterName = "@AuthenticationKey",Size = 16,Type = DbType.Guid, Value = user.AuthenticationKey},
                                  },
                              new List<Parameters>()
                                  {
                                      isActivatedOutParam,
                                      emailOutParam,
                                      isExistsOutParam
                                  }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateUser"));
            if (Convert.ToBoolean(isExistsOutParam.Value))
            {
                user.IsActivated = Convert.ToBoolean(isActivatedOutParam.Value);
                if (user.IsActivated)
                {
                    user.Email = emailOutParam.Value.ToString();
                    LogManager.SendPasswordResetEmail(user);
                }
            }
            return user;
        }

        public int UpdatePassword(int loginId, string password)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[UpdatePassword]",
                            new object[]
                                    {
                                        loginId, LoginSecurity.EncryptPasswords(password)
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertUser"));
            return returnValue;
        }

        public int UpdateSecurityInformation(int loginId, int securityQuestionId, string securityAnswer)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[UpdateSecurityInformation]",
                            new object[]
                                    {
                                        loginId, securityQuestionId, securityAnswer
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertUser"));
            return returnValue;
        }

        public void SendEmailToUser(User user, UserAuthenticationUpdateType updateType)
        {
            Common.ImplementBusinessLogic(
                () =>
                {
                    switch (updateType)
                    {
                        case UserAuthenticationUpdateType.FirstTimeSet:
                            LogManager.SendUserCreatedEmail(user);
                            break;
                        case UserAuthenticationUpdateType.Reset:
                            LogManager.SendPasswordResetEmail(user);
                            break;
                        case UserAuthenticationUpdateType.ForceReSet:
                            LogManager.SendPasswordForceResetEmail(user);
                            break;
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertUser"));
        }

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
