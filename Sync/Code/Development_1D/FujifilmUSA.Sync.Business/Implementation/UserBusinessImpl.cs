using System.Linq;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;
using System.Collections.Generic;
using FujifilmUSA.Sync.Business.CustomExceptions;
using System;
using Service.Common.Encryption;

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
                        password = CryptHelper.DecryptString(password, ApplicationConfiguration.Instance.XmfColorOrganizerEncryptionKey);
                    }
                    string apiAuthKey = LoginSecurity.EncryptPasswords(userName + DateTime.Now.ToString("yyyyMMddHHmmssffff"));
                    var dsUser = DataAccessFactory.Instance.GetDsResult("[CP].[GetLoginInformation]",
                                                                        new object[] { userName, LoginSecurity.EncryptPasswords(password), Convert.ToInt32(userType), apiAuthKey });

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
                            UserType = DataReader.ReadInt(dsUser.Tables[0].Rows[0], "UserType"),
                            LanguageCultureCode = DataReader.ReadString(dsUser.Tables[0].Rows[0], "LanguageCultureCode"),
                            MeasurementDeviceId = DataReader.ReadInt(dsUser.Tables[0].Rows[0], "MeasurementDeviceID"),
                            DensityType = DataReader.ReadShort(dsUser.Tables[0].Rows[0], "DensityType"),
                            UVMode = DataReader.ReadShort(dsUser.Tables[0].Rows[0], "UVMode"),
                            APIAuthKey = DataReader.ReadString(dsUser.Tables[0].Rows[0], "APIAuthKey")
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
                                       LanguageCultureCode = DataReader.ReadString(x, "LanguageCultureCode"),
                                       MeasurementDeviceId = DataReader.ReadInt(x, "MeasurementDeviceID"),
                                       DensityType = DataReader.ReadShort(x, "DensityType"),
                                       UVMode = DataReader.ReadShort(x, "UVMode"),
                                       LastActiveDate = DataReader.ReadDate(x, "LastActiveDate"),
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
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetUser"));
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
                                        , user.LanguageCultureCode
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
                                        , user.LanguageCultureCode
                                    });
                    if (returnValue == -1)
                    {
                        throw new DuplicateException() { UserFriendlyMessage = "Duplicate_UpdateUser" };
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
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetUserAuthenticationInformation"));
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
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateUserAuthenticationInformation"));
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
                                      new Parameters() {ParameterName = "@LoginID", Size = 50, Type = DbType.String, Value = user.LoginId},
                                      new Parameters() {ParameterName = "@AuthenticationKey",Size = 16,Type = DbType.Guid, Value = user.AuthenticationKey},
                                  },
                              new List<Parameters>()
                                  {
                                      isActivatedOutParam,
                                      emailOutParam,
                                      isExistsOutParam
                                  }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdatePreResetPasswordInformation"));
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
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdatePassword"));
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
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateSecurityInformation"));
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
                new BusinessHandlerRequest(this, ErrorOccured, "Error_SendEmailToUser"));
        }

        public int GetUserType(string loginId)
        {
            int userType = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsUser = DataAccessFactory.
                        Instance.
                        GetDsResult(
                            "[CP].[GetUserType]",
                            new object[]
                                    {
                                        loginId
                                    });
                    if (dsUser != null && dsUser.Tables.Count > 0 && dsUser.Tables[0].Rows.Count > 0)
                    {
                        userType = DataReader.ReadInt(dsUser.Tables[0].Rows[0], "UserType");
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetUserType"));
            return userType;


        }

        public List<Language> GetLanguages()
        {
            List<Language> languageList = new List<Language>();

            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsLanguage = DataAccessFactory.Instance.GetDsResult("[dbo].[GetLanguages]");
                    if (dsLanguage != null && dsLanguage.Tables[0].Rows.Count > 0)
                    {

                        languageList = dsLanguage.
                        Tables[0].
                        AsEnumerable().
                        Select(x => new Entity.Language()
                        {
                            LanguageId = DataReader.ReadInt(x, "LanguageId"),
                            EnglishName = DataReader.ReadString(x, "EnglishName"),
                            DisplayText = DataReader.ReadString(x, "DisplayText"),
                            CultureCode = DataReader.ReadString(x, "CultureCode"),
                            IsDefault = DataReader.ReadBoolean(x, "IsDefault"),
                            IsDeleted = DataReader.ReadBoolean(x, "IsDeleted")
                        }).ToList();
                    }
                }, new BusinessHandlerRequest(this, ErrorOccured, "Error_GetLanguages"));
            return languageList;
        }

        public int UpdateLanguage(int loginId, string languageCultureCode)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[UpdateLanguage]",
                            new object[]
                                    {
                                        loginId, languageCultureCode
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateLanguage"));
            return returnValue;
        }

        public List<MeasurementDevice> GetMeasurementDeviceList(int loginId, int deviceId, bool isSpotTargetType = false)
        {
            var measurementDevice = new List<MeasurementDevice>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsMeasurementDevice = DataAccessFactory.Instance.GetDsResult("CP.GetMeasurementDeviceList",
                                                                                     new object[] { loginId, deviceId, isSpotTargetType });
                    measurementDevice = dsMeasurementDevice.Tables[0].AsEnumerable().
                        Select(x =>
                               new MeasurementDevice()
                               {
                                   MeasurementDeviceId = DataReader.ReadShort(x, "MeasurementDeviceID"),
                                   InstrumentTypeId = DataReader.ReadShort(x, "InstrumentTypeID"),
                                   InstrumentType = new InstrumentType()
                                   {
                                       InstrumentTypeId = DataReader.ReadShort(x, "InstrumentTypeID"),
                                       InstrumentName = DataReader.ReadString(x, "InstrumentName")
                                   },
                                   SerialNumber = DataReader.ReadString(x, "MeasurementDevi")
                               }).ToList();
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMeasurementDeviceList"));
            return measurementDevice;
        }

        public int UpdateUserDeviceInfo(int loginId, int measurementDeviceId, int densityType, int UVMode)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[UpdateUserDevice]",
                            new object[]
                                    {
                                        loginId, measurementDeviceId, densityType, UVMode
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateUserDevice"));
            return returnValue;
        }

        public int InsertLoginActivityLogs(int UserId, string OSName, string BrowserName, string EntryTime)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[dbo].[InsertLoginActivityLogs]",
                            new object[]
                                    {
                                       UserId, OSName, BrowserName, EntryTime
                                    });

                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertLoginActivityLogs"));
            return returnValue;
        }

        public bool ValidateUser(string tocken)
        {
            bool isValidUser = false;
            var isValidUserParam = new Parameters() { ParameterName = "@validUser", Type = DbType.Boolean };
            try
            {
                //isValidUser = true;
                Common.ImplementBusinessLogic(
                () =>
                        DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[GetValidateUser]",
                            new List<Parameters>()
                            {
                                new Parameters() {ParameterName = "@Token", Type = DbType.String, Value = tocken},
                            },
                             new List<Parameters>()
                             {
                                 isValidUserParam
                             }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_ValidateUser"));

                isValidUser = Convert.ToBoolean(isValidUserParam.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isValidUser;
        }

        public UserReferenceChart UserLoginReferenceChart(string userName, string password, UserType userType)
        {
            var user = new UserReferenceChart();
            Common.ImplementBusinessLogic(
                () =>
                {
                    if (userType == UserType.XmfColorOrganizerUser)
                    {
                        password = CryptHelper.DecryptString(password, ApplicationConfiguration.Instance.XmfColorOrganizerEncryptionKey);
                    }
                    var dsUser = DataAccessFactory.Instance.GetDsResult("[CP].[GetLoginInformationByApplication]",
                                                                        new object[] { userName, LoginSecurity.EncryptPasswords(password), Convert.ToInt32(userType) });
                    if (dsUser != null && dsUser.Tables.Count > 0 && dsUser.Tables[0].Rows.Count > 0)
                    {
                        user.User = new User()
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
                            UserType = DataReader.ReadInt(dsUser.Tables[0].Rows[0], "UserType"),
                            LanguageCultureCode = DataReader.ReadString(dsUser.Tables[0].Rows[0], "LanguageCultureCode"),
                            MeasurementDeviceId = DataReader.ReadInt(dsUser.Tables[0].Rows[0], "MeasurementDeviceID"),
                            DensityType = DataReader.ReadShort(dsUser.Tables[0].Rows[0], "DensityType"),
                            UVMode = DataReader.ReadShort(dsUser.Tables[0].Rows[0], "UVMode"),
                        };
                        //user.Role.RolePermissions = dsUser.Tables[1].AsEnumerable().
                        //    Select(x => new RolePermission()
                        //    {
                        //        Permission = (ERolePermission)DataReader.ReadInt(x, "PermissionID"),
                        //        Allow = DataReader.ReadBoolean(x, "Allow")
                        //    }).ToList();
                        //if (dsUser.Tables[2].Rows.Count > 0)
                        //{
                        //    user.UserCompany = new UserCompany()
                        //    {
                        //        CompanyId = DataReader.ReadInt(dsUser.Tables[2].Rows[0], "CompanyID"),
                        //        Company = new Company()
                        //        {
                        //            CompanyId = DataReader.ReadInt(dsUser.Tables[2].Rows[0], "CompanyID"),
                        //            CompanyName = DataReader.ReadString(dsUser.Tables[2].Rows[0], "CompanyName")
                        //        }
                        //    };
                        //}
                        //if (dsUser.Tables[3].Rows.Count > 0)
                        //{
                        //    user.UserBranches = dsUser.Tables[3].AsEnumerable().
                        //        Select(x => new UserBranch()
                        //        {
                        //            BranchId = DataReader.ReadInt(x, "BranchID"),
                        //            Branch = new Branch()
                        //            {
                        //                BranchId = DataReader.ReadInt(x, "BranchID"),
                        //                BranchName = DataReader.ReadString(x, "BranchName")
                        //            }
                        //        }).ToList();
                        //}
                    }

                    if (dsUser.Tables[1].Rows.Count > 0)
                    {
                        user.ProjectId = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "ProjectID");
                        user.ProjectName = DataReader.ReadString(dsUser.Tables[1].Rows[0], "ProjectName");
                        
                        user.OptimizationRunID = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "OptimizationRunID");
                        if (Convert.ToInt32(dsUser.Tables[1].Rows[0]["ProfileTypeID"]) == (int)ELicenseType.SpotLibrary)//spot project
                        {
                            user.ReferenceChartData = new ReferenceChartData();
                            if (dsUser.Tables[2].Rows.Count > 0)
                            {
                                user.ReferenceChartData.ReferenceChartTypeId = DataReader.ReadShort(dsUser.Tables[2].Rows[0], "ReferenceChartTypeID");
                                user.ReferenceChartData.ChartName = DataReader.ReadString(dsUser.Tables[2].Rows[0], "ChartName");
                                user.ReferenceChartData.NumOfCols = DataReader.ReadInt(dsUser.Tables[2].Rows[0], "NumOfCols");
                                user.ReferenceChartData.NumOfRows = DataReader.ReadInt(dsUser.Tables[2].Rows[0], "NumOfRows");
                                user.ReferenceChartData.TotalPatches = DataReader.ReadInt(dsUser.Tables[2].Rows[0], "TotalPatches");
                                user.ReferenceChartData.InstrumentTypeId = DataReader.ReadShort(dsUser.Tables[2].Rows[0], "InstrumentTypeID");
                                user.ReferenceChartData.MeasureDeviceSrNo = DataReader.ReadString(dsUser.Tables[2].Rows[0], "MeasureDeviceSrNo");
                                user.ReferenceChartData.ActivationKey =
                                    Utilities.GetMd5Hash(DateTime.Now.Year.ToString() + DateTime.Now.DayOfYear.ToString() + "+yg92s+" + user.ReferenceChartData.MeasureDeviceSrNo);
                                var UVMode = DataReader.ReadShort(dsUser.Tables[2].Rows[0], "UVMode");
                                var DensityType = DataReader.ReadShort(dsUser.Tables[2].Rows[0], "DensityType");

                                user.ReferenceChartData.DeviceSetting = new MeasurementDevice
                                {
                                    SerialNumber = user.ReferenceChartData.MeasureDeviceSrNo,
                                    MeasurementSettings = new MeasurementSetting()
                                    {
                                        ProjectId = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "ProjectID"),
                                        IlluminantName = DataReader.ReadString(dsUser.Tables[2].Rows[0], "IlluminantType"),
                                        ObserverTypeName = Utilities.GetValueFromDescription<EObserverType>(DataReader.ReadString(dsUser.Tables[2].Rows[0], "ObserverType")),
                                        UvIncludedName = ((UVMode == 0) ? EUVIncludedMode.UV.ToString() : (UVMode == 1) ? EUVIncludedMode.UVPlus.ToString() : EUVIncludedMode.UVCut.ToString()),
                                        UVMode = DataReader.ReadShort(dsUser.Tables[2].Rows[0], "UVMode"),
                                        ReadMode = DataReader.ReadString(dsUser.Tables[2].Rows[0], "ReadMode"),
                                        MeasurementCount = DataReader.ReadShort(dsUser.Tables[2].Rows[0], "MeasurementCount"),
                                        DensityType = ((DensityType == 1) ? EDensityType.ANSIE.ToString() : EDensityType.ANSIT.ToString()) //---Pass by deafult "Status_T"
                                    }
                                };
                                user.ReferenceChartData.ReferenceCharts = dsUser.Tables[3].AsEnumerable().
                                    Select(x =>
                                           new ReferenceChart()
                                           {
                                               SampleName = DataReader.ReadString(x, "SampleName"),
                                               HtmlColor = DataReader.ReadString(x, "HTMLColor")
                                           }).ToList();

                                var dsReferenceChartSetting = DataAccessFactory.Instance.GetDsResult("[CPSpot].[GetSpotReferenceChartSetting]",
                                                                                         new object[] { Convert.ToInt32(dsUser.Tables[1].Rows[0]["ProjectID"]) });
                                if (dsReferenceChartSetting.Tables[0].Rows.Count > 0)
                                {
                                    user.ReferenceChartData.ChartSetting = new ChartSetting();
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("ChartSizeX"))
                                        user.ReferenceChartData.ChartSetting.ChartSizeX = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "ChartSizeX");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("ChartSizeY"))
                                        user.ReferenceChartData.ChartSetting.ChartSizeY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "ChartSizeY");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("BarSizeX"))
                                        user.ReferenceChartData.ChartSetting.BarSizeX = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "BarSizeX");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("BarSizeY"))
                                        user.ReferenceChartData.ChartSetting.BarSizeY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "BarSizeY");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("MarkerDistance"))
                                        user.ReferenceChartData.ChartSetting.MarkerDistance = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "MarkerDistance");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("PatchSizeX"))
                                        user.ReferenceChartData.ChartSetting.PatchSizeX = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "PatchSizeX");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("PatchSizeY"))
                                        user.ReferenceChartData.ChartSetting.PatchSizeY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "PatchSizeY");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("DistToFirstLineY"))
                                        user.ReferenceChartData.ChartSetting.DistToFirstLineY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "DistToFirstLineY");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("DistToFirstField"))
                                        user.ReferenceChartData.ChartSetting.DistToFirstField = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "DistToFirstField");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("DistToBar"))
                                        user.ReferenceChartData.ChartSetting.DistToBar = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "DistToBar");

                                    //For IO devices...
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("FirstSample"))
                                        user.ReferenceChartData.ChartSetting.FirstSample = DataReader.ReadString(dsReferenceChartSetting.Tables[0].Rows[0], "FirstSample");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("ReadDirection"))
                                        user.ReferenceChartData.ChartSetting.ReadDirection = DataReader.ReadString(dsReferenceChartSetting.Tables[0].Rows[0], "ReadDirection");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("ReadMode"))
                                        user.ReferenceChartData.ChartSetting.ReadMode = DataReader.ReadString(dsReferenceChartSetting.Tables[0].Rows[0], "ReadMode");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("RowsPerPage"))
                                        user.ReferenceChartData.ChartSetting.RowsPerPage = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "RowsPerPage");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("Pages"))
                                        user.ReferenceChartData.ChartSetting.Pages = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "Pages");
                                }
                            }
                        }
                        else
                        {
                            if (dsUser.Tables[2].Rows.Count > 0)
                            {
                                user.ReferenceChartData = new ReferenceChartData();

                                user.ReferenceChartData.ReferenceChartTypeId = DataReader.ReadShort(dsUser.Tables[2].Rows[0], "ReferenceChartTypeID");
                                user.ReferenceChartData.ChartName = DataReader.ReadString(dsUser.Tables[2].Rows[0], "ChartName");
                                user.ReferenceChartData.NumOfCols = DataReader.ReadInt(dsUser.Tables[2].Rows[0], "NumOfCols");
                                user.ReferenceChartData.NumOfRows = DataReader.ReadInt(dsUser.Tables[2].Rows[0], "NumOfRows");
                                user.ReferenceChartData.TotalPatches = DataReader.ReadInt(dsUser.Tables[2].Rows[0], "TotalPatches");
                                user.ReferenceChartData.InstrumentTypeId = DataReader.ReadShort(dsUser.Tables[2].Rows[0], "InstrumentTypeID");
                                user.ReferenceChartData.MeasureDeviceSrNo = DataReader.ReadString(dsUser.Tables[2].Rows[0], "MeasureDeviceSrNo");
                                user.ReferenceChartData.ActivationKey =
                                    Utilities.GetMd5Hash(DateTime.Now.Year.ToString() + DateTime.Now.DayOfYear.ToString() + "+yg92s+" + user.ReferenceChartData.MeasureDeviceSrNo);
                                var UVMode = DataReader.ReadShort(dsUser.Tables[2].Rows[0], "UVMode");
                                var DensityType = DataReader.ReadShort(dsUser.Tables[2].Rows[0], "DensityType");

                                user.ReferenceChartData.DeviceSetting = new MeasurementDevice
                                {
                                    SerialNumber = user.ReferenceChartData.MeasureDeviceSrNo,
                                    InstrumentTypeId = Convert.ToInt16(DataReader.ReadInt(dsUser.Tables[2].Rows[0], "InstrumentTypeID")),
                                    MeasurementSettings = new MeasurementSetting()
                                    {
                                        ProjectId = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "ProjectID"),
                                        InstrumentTypeId = Convert.ToInt16(DataReader.ReadInt(dsUser.Tables[2].Rows[0], "InstrumentTypeID")),
                                        IlluminantName =
                                                                                                    DataReader.ReadString(dsUser.Tables[2].Rows[0], "IlluminantType"),
                                        ObserverTypeName =
                                                                                                    Utilities.GetValueFromDescription<EObserverType>(
                                                                                                        DataReader.ReadString(dsUser.Tables[2].Rows[0], "ObserverType")),
                                        UvIncludedName = ((UVMode == 0) ? EUVIncludedMode.UV.ToString() : (UVMode == 1) ? EUVIncludedMode.UVPlus.ToString() : EUVIncludedMode.UVCut.ToString()),
                                        UVMode = DataReader.ReadShort(dsUser.Tables[2].Rows[0], "UVMode"),
                                        ReadMode = DataReader.ReadString(dsUser.Tables[2].Rows[0], "ReadMode"),
                                        MeasurementCount = DataReader.ReadShort(dsUser.Tables[2].Rows[0], "MeasurementCount"),
                                        DensityType = ((DensityType == 1) ? EDensityType.ANSIE.ToString() : EDensityType.ANSIT.ToString()) //---Pass by deafult "Status_T"
                                    }
                                };
                                user.ReferenceChartData.ReferenceCharts = dsUser.Tables[3].AsEnumerable().
                                    Select(x =>
                                           new ReferenceChart()
                                           {
                                               ChartTypeId = DataReader.ReadShort(x, "ChartTypeID"),
                                               SampleName = DataReader.ReadString(x, "SampleName"),
                                               CmykC = DataReader.ReadDecimal(x, "CMYK_C"),
                                               CmykM = DataReader.ReadDecimal(x, "CMYK_M"),
                                               CmykY = DataReader.ReadDecimal(x, "CMYK_Y"),
                                               CmykK = DataReader.ReadDecimal(x, "CMYK_K"),
                                               PatchTypeId = DataReader.ReadShort(x, "PatchTypeID"),
                                               HtmlColor = DataReader.ReadString(x, "HTMLColor")
                                           }).ToList();

                                var reader = DataAccessFactory.Instance.GetXmlResult("CPSync4D.GetChartSettings", new object[] { user.ReferenceChartData.ReferenceChartTypeId });
                                var dsChartSettings = new DataSet();
                                dsChartSettings.ReadXml(reader);
                                if (dsChartSettings.Tables.Count > 0)
                                {
                                    user.ReferenceChartData.ChartSetting = new ChartSetting();
                                    if (dsChartSettings.Tables[0].Columns.Contains("ChartSizeX"))
                                        user.ReferenceChartData.ChartSetting.ChartSizeX = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "ChartSizeX");
                                    if (dsChartSettings.Tables[0].Columns.Contains("ChartSizeY"))
                                        user.ReferenceChartData.ChartSetting.ChartSizeY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "ChartSizeY");
                                    if (dsChartSettings.Tables[0].Columns.Contains("BarSizeX"))
                                        user.ReferenceChartData.ChartSetting.BarSizeX = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "BarSizeX");
                                    if (dsChartSettings.Tables[0].Columns.Contains("BarSizeY"))
                                        user.ReferenceChartData.ChartSetting.BarSizeY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "BarSizeY");
                                    if (dsChartSettings.Tables[0].Columns.Contains("MarkerDistance"))
                                        user.ReferenceChartData.ChartSetting.MarkerDistance = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "MarkerDistance");
                                    if (dsChartSettings.Tables[0].Columns.Contains("PatchSizeX"))
                                        user.ReferenceChartData.ChartSetting.PatchSizeX = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "PatchSizeX");
                                    if (dsChartSettings.Tables[0].Columns.Contains("PatchSizeY"))
                                        user.ReferenceChartData.ChartSetting.PatchSizeY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "PatchSizeY");
                                    if (dsChartSettings.Tables[0].Columns.Contains("DistToFirstLineY"))
                                        user.ReferenceChartData.ChartSetting.DistToFirstLineY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToFirstLineY");
                                    if (dsChartSettings.Tables[0].Columns.Contains("DistToFirstField"))
                                        user.ReferenceChartData.ChartSetting.DistToFirstField = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToFirstField");
                                    if (dsChartSettings.Tables[0].Columns.Contains("DistToBar"))
                                        user.ReferenceChartData.ChartSetting.DistToBar = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToBar");

                                    //For IO devices...
                                    if (dsChartSettings.Tables[0].Columns.Contains("FirstSample"))
                                        user.ReferenceChartData.ChartSetting.FirstSample = DataReader.ReadString(dsChartSettings.Tables[0].Rows[0], "FirstSample");
                                    if (dsChartSettings.Tables[0].Columns.Contains("ReadDirection"))
                                        user.ReferenceChartData.ChartSetting.ReadDirection = DataReader.ReadString(dsChartSettings.Tables[0].Rows[0], "ReadDirection");
                                    if (dsChartSettings.Tables[0].Columns.Contains("ReadMode"))
                                        user.ReferenceChartData.ChartSetting.ReadMode = DataReader.ReadString(dsChartSettings.Tables[0].Rows[0], "ReadMode");
                                    if (dsChartSettings.Tables[0].Columns.Contains("RowsPerPage"))
                                        user.ReferenceChartData.ChartSetting.RowsPerPage = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "RowsPerPage");
                                    if (dsChartSettings.Tables[0].Columns.Contains("Pages"))
                                        user.ReferenceChartData.ChartSetting.Pages = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "Pages");
                                }
                            }
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UserLogin"));
            return user;
        }

        public ReferenceChartApp UserLoginReferenceChartBySerial(string serial, short InstrumentTypeId)
        {
            //var user = new UserReferenceChart();
            var user = new ReferenceChartApp();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsUser = DataAccessFactory.Instance.GetDsResult("[CP].[GetLoginInformationBySerial]",
                                                                        new object[] { serial, InstrumentTypeId });


                    if (dsUser.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dsUser.Tables[0].Rows[0]["ProfileTypeID"]) == (int)ELicenseType.SpotLibrary)//spot project
                        {
                            if (dsUser.Tables[1].Rows.Count > 0)
                            {
                                user.ProjectId = DataReader.ReadInt(dsUser.Tables[0].Rows[0], "ProjectID");
                                user.ProjectName = DataReader.ReadString(dsUser.Tables[0].Rows[0], "ProjectName");
                                user.OptimizationRunID = DataReader.ReadInt(dsUser.Tables[0].Rows[0], "OptimizationRunID");
                                user.DeviceName = DataReader.ReadString(dsUser.Tables[0].Rows[0], "DeviceName");
                                user.ReferenceChartTypeId = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "ReferenceChartTypeID");
                                user.ChartName = DataReader.ReadString(dsUser.Tables[1].Rows[0], "ChartName");
                                user.NumOfCols = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "NumOfCols");
                                user.NumOfRows = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "NumOfRows");
                                user.TotalPatches = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "TotalPatches");
                                user.InstrumentTypeId = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "InstrumentTypeID");
                                user.ProjectType = new ProjectType()
                                    {
                                        ProjectTypeId = Convert.ToInt32(dsUser.Tables[0].Rows[0]["ProfileTypeID"]) != 3 ? DataReader.ReadShort(dsUser.Tables[0].Rows[0], "LicenseTypeID") : (short)51 ,
                                        ProjectTypeName= Convert.ToInt32(dsUser.Tables[0].Rows[0]["ProfileTypeID"]) != 3 ? Convert.ToString((ELicenseType)DataReader.ReadShort(dsUser.Tables[0].Rows[0], "LicenseTypeID")) : Convert.ToString((ELicenseType)51),
                                } ;
                                user.EntryDate = DataReader.ReadString(dsUser.Tables[0].Rows[0], "EntryDate");
                                //user.MeasureDeviceSrNo = DataReader.ReadString(dsUser.Tables[1].Rows[0], "MeasureDeviceSrNo");
                                //user.ActivationKey =
                                //    Utilities.GetMd5Hash(DateTime.Now.Year.ToString() + DateTime.Now.DayOfYear.ToString() + "+yg92s+" + user.ReferenceChartData.MeasureDeviceSrNo);
                                var UVMode = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "UVMode");
                                var DensityType = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "DensityType");

                                user.MeasurementSettings = new MeasurementSettingApp()
                                {
                                    IlluminantName = DataReader.ReadString(dsUser.Tables[1].Rows[0], "IlluminantType"),
                                    ObserverTypeName = Utilities.GetValueFromDescription<EObserverType>(DataReader.ReadString(dsUser.Tables[1].Rows[0], "ObserverType")),
                                    UvIncludedName = ((UVMode == 0) ? EUVIncludedMode.UV.ToString() : (UVMode == 1) ? EUVIncludedMode.UVPlus.ToString() : EUVIncludedMode.UVCut.ToString()),
                                    UVMode = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "UVMode"),
                                    //ReadMode = DataReader.ReadString(dsUser.Tables[1].Rows[0], "ReadMode"),
                                    MeasurementCount = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "MeasurementCount"),
                                    DensityType = ((DensityType == 1) ? EDensityType.ANSIE.ToString() : EDensityType.ANSIT.ToString()) //---Pass by deafult "Status_T"
                                };

                                user.ReferenceCharts = dsUser.Tables[2].AsEnumerable().
                                    Select(x =>
                                           new ReferenceChart()
                                           {
                                               SampleName = DataReader.ReadString(x, "SampleName"),
                                               HtmlColor = DataReader.ReadString(x, "HTMLColor")
                                           }).ToList();

                                var dsReferenceChartSetting = DataAccessFactory.Instance.GetDsResult("[CPSpot].[GetSpotReferenceChartSetting]",
                                                                                         new object[] { Convert.ToInt32(dsUser.Tables[0].Rows[0]["ProjectID"]) });
                                if (dsReferenceChartSetting.Tables[0].Rows.Count > 0)
                                {
                                    user.ChartSetting = new ChartSetting();
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("ChartSizeX"))
                                        user.ChartSetting.ChartSizeX = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "ChartSizeX");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("ChartSizeY"))
                                        user.ChartSetting.ChartSizeY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "ChartSizeY");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("BarSizeX"))
                                        user.ChartSetting.BarSizeX = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "BarSizeX");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("BarSizeY"))
                                        user.ChartSetting.BarSizeY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "BarSizeY");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("MarkerDistance"))
                                        user.ChartSetting.MarkerDistance = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "MarkerDistance");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("PatchSizeX"))
                                        user.ChartSetting.PatchSizeX = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "PatchSizeX");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("PatchSizeY"))
                                        user.ChartSetting.PatchSizeY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "PatchSizeY");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("DistToFirstLineY"))
                                        user.ChartSetting.DistToFirstLineY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "DistToFirstLineY");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("DistToFirstField"))
                                        user.ChartSetting.DistToFirstField = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "DistToFirstField");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("DistToBar"))
                                        user.ChartSetting.DistToBar = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "DistToBar");

                                    //For IO devices...
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("FirstSample"))
                                        user.ChartSetting.FirstSample = DataReader.ReadString(dsReferenceChartSetting.Tables[0].Rows[0], "FirstSample");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("ReadDirection"))
                                        user.ChartSetting.ReadDirection = DataReader.ReadString(dsReferenceChartSetting.Tables[0].Rows[0], "ReadDirection");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("ReadMode"))
                                        user.ChartSetting.ReadMode = DataReader.ReadString(dsReferenceChartSetting.Tables[0].Rows[0], "ReadMode");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("RowsPerPage"))
                                        user.ChartSetting.RowsPerPage = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "RowsPerPage");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("Pages"))
                                        user.ChartSetting.Pages = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "Pages");
                                }
                            }
                        }
                        else
                        {
                            if (dsUser.Tables[1].Rows.Count > 0)
                            {
                                user.ProjectId = DataReader.ReadInt(dsUser.Tables[0].Rows[0], "ProjectID");
                                user.ProjectName = DataReader.ReadString(dsUser.Tables[0].Rows[0], "ProjectName");
                                user.OptimizationRunID = DataReader.ReadInt(dsUser.Tables[0].Rows[0], "OptimizationRunID");
                                user.DeviceName = DataReader.ReadString(dsUser.Tables[0].Rows[0], "DeviceName");
                                user.ReferenceChartTypeId = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "ReferenceChartTypeID");
                                user.ChartName = DataReader.ReadString(dsUser.Tables[1].Rows[0], "ChartName");
                                user.NumOfCols = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "NumOfCols");
                                user.NumOfRows = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "NumOfRows");
                                user.TotalPatches = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "TotalPatches");
                                user.InstrumentTypeId = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "InstrumentTypeID");
                                user.ProjectType = new ProjectType()
                                {
                                    ProjectTypeId = Convert.ToInt32(dsUser.Tables[0].Rows[0]["ProfileTypeID"]) != 3 ? DataReader.ReadShort(dsUser.Tables[0].Rows[0], "LicenseTypeID") : (short)51,
                                    ProjectTypeName = Convert.ToInt32(dsUser.Tables[0].Rows[0]["ProfileTypeID"]) != 3 ? Convert.ToString((ELicenseType)DataReader.ReadShort(dsUser.Tables[0].Rows[0], "LicenseTypeID")) : Convert.ToString((ELicenseType)51),
                                };
                                user.EntryDate = DataReader.ReadString(dsUser.Tables[0].Rows[0], "EntryDate");
                                //user1.MeasureDeviceSrNo = DataReader.ReadString(dsUser.Tables[1].Rows[0], "MeasureDeviceSrNo");
                                //user1.ActivationKey =
                                //  Utilities.GetMd5Hash(DateTime.Now.Year.ToString() + DateTime.Now.DayOfYear.ToString() + "+yg92s+" + user.ReferenceChartData.MeasureDeviceSrNo);
                                var UVMode = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "UVMode");
                                var DensityType = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "DensityType");

                                user.MeasurementSettings = new MeasurementSettingApp()
                                {
                                    IlluminantName = DataReader.ReadString(dsUser.Tables[1].Rows[0], "IlluminantType"),
                                    ObserverTypeName = Utilities.GetValueFromDescription<EObserverType>(DataReader.ReadString(dsUser.Tables[1].Rows[0], "ObserverType")),
                                    UvIncludedName = ((UVMode == 0) ? EUVIncludedMode.UV.ToString() : (UVMode == 1) ? EUVIncludedMode.UVPlus.ToString() : EUVIncludedMode.UVCut.ToString()),
                                    UVMode = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "UVMode"),
                                    //ReadMode = DataReader.ReadString(dsUser.Tables[1].Rows[0], "ReadMode"),
                                    MeasurementCount = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "MeasurementCount"),
                                    DensityType = ((DensityType == 1) ? EDensityType.ANSIE.ToString() : EDensityType.ANSIT.ToString()) //---Pass by deafult "Status_T"
                                };

                                user.ReferenceCharts = dsUser.Tables[2].AsEnumerable().
                                    Select(x =>
                                           new ReferenceChart()
                                           {
                                               ChartTypeId = DataReader.ReadShort(x, "ChartTypeID"),
                                               SampleName = DataReader.ReadString(x, "SampleName"),
                                               CmykC = DataReader.ReadDecimal(x, "CMYK_C"),
                                               CmykM = DataReader.ReadDecimal(x, "CMYK_M"),
                                               CmykY = DataReader.ReadDecimal(x, "CMYK_Y"),
                                               CmykK = DataReader.ReadDecimal(x, "CMYK_K"),
                                               PatchTypeId = DataReader.ReadShort(x, "PatchTypeID"),
                                               HtmlColor = DataReader.ReadString(x, "HTMLColor")
                                           }).ToList();

                                var reader = DataAccessFactory.Instance.GetXmlResult("CPSync4D.GetChartSettings", new object[] { user.ReferenceChartTypeId });
                                var dsChartSettings = new DataSet();
                                dsChartSettings.ReadXml(reader);
                                if (dsChartSettings.Tables.Count > 0)
                                {
                                    user.ChartSetting = new ChartSetting();
                                    if (dsChartSettings.Tables[0].Columns.Contains("ChartSizeX"))
                                        user.ChartSetting.ChartSizeX = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "ChartSizeX");
                                    if (dsChartSettings.Tables[0].Columns.Contains("ChartSizeY"))
                                        user.ChartSetting.ChartSizeY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "ChartSizeY");
                                    if (dsChartSettings.Tables[0].Columns.Contains("BarSizeX"))
                                        user.ChartSetting.BarSizeX = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "BarSizeX");
                                    if (dsChartSettings.Tables[0].Columns.Contains("BarSizeY"))
                                        user.ChartSetting.BarSizeY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "BarSizeY");
                                    if (dsChartSettings.Tables[0].Columns.Contains("MarkerDistance"))
                                        user.ChartSetting.MarkerDistance = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "MarkerDistance");
                                    if (dsChartSettings.Tables[0].Columns.Contains("PatchSizeX"))
                                        user.ChartSetting.PatchSizeX = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "PatchSizeX");
                                    if (dsChartSettings.Tables[0].Columns.Contains("PatchSizeY"))
                                        user.ChartSetting.PatchSizeY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "PatchSizeY");
                                    if (dsChartSettings.Tables[0].Columns.Contains("DistToFirstLineY"))
                                        user.ChartSetting.DistToFirstLineY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToFirstLineY");
                                    if (dsChartSettings.Tables[0].Columns.Contains("DistToFirstField"))
                                        user.ChartSetting.DistToFirstField = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToFirstField");
                                    if (dsChartSettings.Tables[0].Columns.Contains("DistToBar"))
                                        user.ChartSetting.DistToBar = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToBar");

                                    //For IO devices...
                                    if (dsChartSettings.Tables[0].Columns.Contains("FirstSample"))
                                        user.ChartSetting.FirstSample = DataReader.ReadString(dsChartSettings.Tables[0].Rows[0], "FirstSample");
                                    if (dsChartSettings.Tables[0].Columns.Contains("ReadDirection"))
                                        user.ChartSetting.ReadDirection = DataReader.ReadString(dsChartSettings.Tables[0].Rows[0], "ReadDirection");
                                    if (dsChartSettings.Tables[0].Columns.Contains("ReadMode"))
                                        user.ChartSetting.ReadMode = DataReader.ReadString(dsChartSettings.Tables[0].Rows[0], "ReadMode");
                                    if (dsChartSettings.Tables[0].Columns.Contains("RowsPerPage"))
                                        user.ChartSetting.RowsPerPage = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "RowsPerPage");
                                    if (dsChartSettings.Tables[0].Columns.Contains("Pages"))
                                        user.ChartSetting.Pages = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "Pages");
                                }
                            }
                        }
                    }

                    //if (dsUser.Tables[0].Rows.Count > 0)
                    //{
                    //    if (Convert.ToInt32(dsUser.Tables[0].Rows[0]["ProfileTypeID"]) == (int)ELicenseType.SpotLibrary)//spot project
                    //    {
                    //        user.ReferenceChartData = new ReferenceChartData();
                    //        if (dsUser.Tables[1].Rows.Count > 0)
                    //        {
                    //            user.ReferenceChartData.ReferenceChartTypeId = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "ReferenceChartTypeID");
                    //            user.ReferenceChartData.ChartName = DataReader.ReadString(dsUser.Tables[1].Rows[0], "ChartName");
                    //            user.ReferenceChartData.NumOfCols = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "NumOfCols");
                    //            user.ReferenceChartData.NumOfRows = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "NumOfRows");
                    //            user.ReferenceChartData.TotalPatches = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "TotalPatches");
                    //            user.ReferenceChartData.InstrumentTypeId = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "InstrumentTypeID");
                    //            user.ReferenceChartData.MeasureDeviceSrNo = DataReader.ReadString(dsUser.Tables[1].Rows[0], "MeasureDeviceSrNo");
                    //            user.ReferenceChartData.ActivationKey =
                    //                Utilities.GetMd5Hash(DateTime.Now.Year.ToString() + DateTime.Now.DayOfYear.ToString() + "+yg92s+" + user.ReferenceChartData.MeasureDeviceSrNo);
                    //            var UVMode = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "UVMode");
                    //            var DensityType = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "DensityType");

                    //            user.ReferenceChartData.DeviceSetting = new MeasurementDevice
                    //            {
                    //                SerialNumber = user.ReferenceChartData.MeasureDeviceSrNo,
                    //                MeasurementSettings = new MeasurementSetting()
                    //                {
                    //                    IlluminantName = DataReader.ReadString(dsUser.Tables[1].Rows[0], "IlluminantType"),
                    //                    ObserverTypeName = Utilities.GetValueFromDescription<EObserverType>(DataReader.ReadString(dsUser.Tables[1].Rows[0], "ObserverType")),
                    //                    UvIncludedName = ((UVMode == 0) ? EUVIncludedMode.UV.ToString() : (UVMode == 1) ? EUVIncludedMode.UVPlus.ToString() : EUVIncludedMode.UVCut.ToString()),
                    //                    UVMode = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "UVMode"),
                    //                    ReadMode = DataReader.ReadString(dsUser.Tables[1].Rows[0], "ReadMode"),
                    //                    MeasurementCount = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "MeasurementCount"),
                    //                    DensityType = ((DensityType == 1) ? EDensityType.ANSIE.ToString() : EDensityType.ANSIT.ToString()) //---Pass by deafult "Status_T"
                    //                }
                    //            };
                    //            user.ReferenceChartData.ReferenceCharts = dsUser.Tables[2].AsEnumerable().
                    //                Select(x =>
                    //                       new ReferenceChart()
                    //                       {
                    //                           SampleName = DataReader.ReadString(x, "SampleName"),
                    //                           HtmlColor = DataReader.ReadString(x, "HTMLColor")
                    //                       }).ToList();

                    //            var dsReferenceChartSetting = DataAccessFactory.Instance.GetDsResult("[CPSpot].[GetSpotReferenceChartSetting]",
                    //                                                                     new object[] { Convert.ToInt32(dsUser.Tables[0].Rows[0]["ProjectID"]) });
                    //            if (dsReferenceChartSetting.Tables[0].Rows.Count > 0)
                    //            {
                    //                user.ReferenceChartData.ChartSetting = new ChartSetting();
                    //                if (dsReferenceChartSetting.Tables[0].Columns.Contains("ChartSizeX"))
                    //                    user.ReferenceChartData.ChartSetting.ChartSizeX = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "ChartSizeX");
                    //                if (dsReferenceChartSetting.Tables[0].Columns.Contains("ChartSizeY"))
                    //                    user.ReferenceChartData.ChartSetting.ChartSizeY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "ChartSizeY");
                    //                if (dsReferenceChartSetting.Tables[0].Columns.Contains("BarSizeX"))
                    //                    user.ReferenceChartData.ChartSetting.BarSizeX = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "BarSizeX");
                    //                if (dsReferenceChartSetting.Tables[0].Columns.Contains("BarSizeY"))
                    //                    user.ReferenceChartData.ChartSetting.BarSizeY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "BarSizeY");
                    //                if (dsReferenceChartSetting.Tables[0].Columns.Contains("MarkerDistance"))
                    //                    user.ReferenceChartData.ChartSetting.MarkerDistance = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "MarkerDistance");
                    //                if (dsReferenceChartSetting.Tables[0].Columns.Contains("PatchSizeX"))
                    //                    user.ReferenceChartData.ChartSetting.PatchSizeX = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "PatchSizeX");
                    //                if (dsReferenceChartSetting.Tables[0].Columns.Contains("PatchSizeY"))
                    //                    user.ReferenceChartData.ChartSetting.PatchSizeY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "PatchSizeY");
                    //                if (dsReferenceChartSetting.Tables[0].Columns.Contains("DistToFirstLineY"))
                    //                    user.ReferenceChartData.ChartSetting.DistToFirstLineY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "DistToFirstLineY");
                    //                if (dsReferenceChartSetting.Tables[0].Columns.Contains("DistToFirstField"))
                    //                    user.ReferenceChartData.ChartSetting.DistToFirstField = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "DistToFirstField");
                    //                if (dsReferenceChartSetting.Tables[0].Columns.Contains("DistToBar"))
                    //                    user.ReferenceChartData.ChartSetting.DistToBar = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "DistToBar");

                    //                //For IO devices...
                    //                if (dsReferenceChartSetting.Tables[0].Columns.Contains("FirstSample"))
                    //                    user.ReferenceChartData.ChartSetting.FirstSample = DataReader.ReadString(dsReferenceChartSetting.Tables[0].Rows[0], "FirstSample");
                    //                if (dsReferenceChartSetting.Tables[0].Columns.Contains("ReadDirection"))
                    //                    user.ReferenceChartData.ChartSetting.ReadDirection = DataReader.ReadString(dsReferenceChartSetting.Tables[0].Rows[0], "ReadDirection");
                    //                if (dsReferenceChartSetting.Tables[0].Columns.Contains("ReadMode"))
                    //                    user.ReferenceChartData.ChartSetting.ReadMode = DataReader.ReadString(dsReferenceChartSetting.Tables[0].Rows[0], "ReadMode");
                    //                if (dsReferenceChartSetting.Tables[0].Columns.Contains("RowsPerPage"))
                    //                    user.ReferenceChartData.ChartSetting.RowsPerPage = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "RowsPerPage");
                    //                if (dsReferenceChartSetting.Tables[0].Columns.Contains("Pages"))
                    //                    user.ReferenceChartData.ChartSetting.Pages = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "Pages");
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (dsUser.Tables[1].Rows.Count > 0)
                    //        {
                    //            user.ReferenceChartData = new ReferenceChartData();
                    //            user.ReferenceChartData.ReferenceChartTypeId = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "ReferenceChartTypeID");
                    //            user.ReferenceChartData.ChartName = DataReader.ReadString(dsUser.Tables[1].Rows[0], "ChartName");
                    //            user.ReferenceChartData.NumOfCols = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "NumOfCols");
                    //            user.ReferenceChartData.NumOfRows = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "NumOfRows");
                    //            user.ReferenceChartData.TotalPatches = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "TotalPatches");
                    //            user.ReferenceChartData.InstrumentTypeId = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "InstrumentTypeID");
                    //            user.ReferenceChartData.MeasureDeviceSrNo = DataReader.ReadString(dsUser.Tables[1].Rows[0], "MeasureDeviceSrNo");
                    //            user.ReferenceChartData.ActivationKey =
                    //                Utilities.GetMd5Hash(DateTime.Now.Year.ToString() + DateTime.Now.DayOfYear.ToString() + "+yg92s+" + user.ReferenceChartData.MeasureDeviceSrNo);
                    //            var UVMode = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "UVMode");
                    //            var DensityType = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "DensityType");

                    //            user.ReferenceChartData.DeviceSetting = new MeasurementDevice
                    //            {
                    //                SerialNumber = user.ReferenceChartData.MeasureDeviceSrNo,
                    //                InstrumentTypeId = Convert.ToInt16(DataReader.ReadInt(dsUser.Tables[1].Rows[0], "InstrumentTypeID")),
                    //                MeasurementSettings = new MeasurementSetting()
                    //                {
                    //                    ProjectId = DataReader.ReadInt(dsUser.Tables[0].Rows[0], "ProjectID"),
                    //                    InstrumentTypeId = Convert.ToInt16(DataReader.ReadInt(dsUser.Tables[1].Rows[0], "InstrumentTypeID")),
                    //                    IlluminantName =
                    //                                                                                DataReader.ReadString(dsUser.Tables[1].Rows[0], "IlluminantType"),
                    //                    ObserverTypeName =
                    //                                                                                Utilities.GetValueFromDescription<EObserverType>(
                    //                                                                                    DataReader.ReadString(dsUser.Tables[1].Rows[0], "ObserverType")),
                    //                    UvIncludedName = ((UVMode == 0) ? EUVIncludedMode.UV.ToString() : (UVMode == 1) ? EUVIncludedMode.UVPlus.ToString() : EUVIncludedMode.UVCut.ToString()),
                    //                    UVMode = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "UVMode"),
                    //                    ReadMode = DataReader.ReadString(dsUser.Tables[1].Rows[0], "ReadMode"),
                    //                    MeasurementCount = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "MeasurementCount"),
                    //                    DensityType = ((DensityType == 1) ? EDensityType.ANSIE.ToString() : EDensityType.ANSIT.ToString()) //---Pass by deafult "Status_T"
                    //                }
                    //            };
                    //            user.ReferenceChartData.ReferenceCharts = dsUser.Tables[2].AsEnumerable().
                    //                Select(x =>
                    //                       new ReferenceChart()
                    //                       {
                    //                           ChartTypeId = DataReader.ReadShort(x, "ChartTypeID"),
                    //                           SampleName = DataReader.ReadString(x, "SampleName"),
                    //                           CmykC = DataReader.ReadDecimal(x, "CMYK_C"),
                    //                           CmykM = DataReader.ReadDecimal(x, "CMYK_M"),
                    //                           CmykY = DataReader.ReadDecimal(x, "CMYK_Y"),
                    //                           CmykK = DataReader.ReadDecimal(x, "CMYK_K"),
                    //                           PatchTypeId = DataReader.ReadShort(x, "PatchTypeID"),
                    //                           HtmlColor = DataReader.ReadString(x, "HTMLColor")
                    //                       }).ToList();

                    //            var reader = DataAccessFactory.Instance.GetXmlResult("CPSync4D.GetChartSettings", new object[] { user.ReferenceChartData.ReferenceChartTypeId });
                    //            var dsChartSettings = new DataSet();
                    //            dsChartSettings.ReadXml(reader);
                    //            if (dsChartSettings.Tables.Count > 0)
                    //            {
                    //                user.ReferenceChartData.ChartSetting = new ChartSetting();
                    //                if (dsChartSettings.Tables[0].Columns.Contains("ChartSizeX"))
                    //                    user.ReferenceChartData.ChartSetting.ChartSizeX = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "ChartSizeX");
                    //                if (dsChartSettings.Tables[0].Columns.Contains("ChartSizeY"))
                    //                    user.ReferenceChartData.ChartSetting.ChartSizeY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "ChartSizeY");
                    //                if (dsChartSettings.Tables[0].Columns.Contains("BarSizeX"))
                    //                    user.ReferenceChartData.ChartSetting.BarSizeX = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "BarSizeX");
                    //                if (dsChartSettings.Tables[0].Columns.Contains("BarSizeY"))
                    //                    user.ReferenceChartData.ChartSetting.BarSizeY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "BarSizeY");
                    //                if (dsChartSettings.Tables[0].Columns.Contains("MarkerDistance"))
                    //                    user.ReferenceChartData.ChartSetting.MarkerDistance = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "MarkerDistance");
                    //                if (dsChartSettings.Tables[0].Columns.Contains("PatchSizeX"))
                    //                    user.ReferenceChartData.ChartSetting.PatchSizeX = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "PatchSizeX");
                    //                if (dsChartSettings.Tables[0].Columns.Contains("PatchSizeY"))
                    //                    user.ReferenceChartData.ChartSetting.PatchSizeY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "PatchSizeY");
                    //                if (dsChartSettings.Tables[0].Columns.Contains("DistToFirstLineY"))
                    //                    user.ReferenceChartData.ChartSetting.DistToFirstLineY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToFirstLineY");
                    //                if (dsChartSettings.Tables[0].Columns.Contains("DistToFirstField"))
                    //                    user.ReferenceChartData.ChartSetting.DistToFirstField = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToFirstField");
                    //                if (dsChartSettings.Tables[0].Columns.Contains("DistToBar"))
                    //                    user.ReferenceChartData.ChartSetting.DistToBar = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToBar");

                    //                //For IO devices...
                    //                if (dsChartSettings.Tables[0].Columns.Contains("FirstSample"))
                    //                    user.ReferenceChartData.ChartSetting.FirstSample = DataReader.ReadString(dsChartSettings.Tables[0].Rows[0], "FirstSample");
                    //                if (dsChartSettings.Tables[0].Columns.Contains("ReadDirection"))
                    //                    user.ReferenceChartData.ChartSetting.ReadDirection = DataReader.ReadString(dsChartSettings.Tables[0].Rows[0], "ReadDirection");
                    //                if (dsChartSettings.Tables[0].Columns.Contains("ReadMode"))
                    //                    user.ReferenceChartData.ChartSetting.ReadMode = DataReader.ReadString(dsChartSettings.Tables[0].Rows[0], "ReadMode");
                    //                if (dsChartSettings.Tables[0].Columns.Contains("RowsPerPage"))
                    //                    user.ReferenceChartData.ChartSetting.RowsPerPage = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "RowsPerPage");
                    //                if (dsChartSettings.Tables[0].Columns.Contains("Pages"))
                    //                    user.ReferenceChartData.ChartSetting.Pages = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "Pages");
                    //            }
                    //        }
                    //    }
                    //}
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UserLogin"));
            return user;
        }

        public ReferenceChartApp UserLogInByDeviceLAB(string serial, short InstrumentTypeId)
        {
            //var user = new UserReferenceChart();
            var user = new ReferenceChartApp();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsUser = DataAccessFactory.Instance.GetDsResult("[CP].[GetLoginInformationBySerial_LAB]",
                                                                        new object[] { serial, InstrumentTypeId });


                    if (dsUser.Tables[0].Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dsUser.Tables[0].Rows[0]["ProfileTypeID"]) == (int)ELicenseType.SpotLibrary)//spot project
                        {
                            if (dsUser.Tables[1].Rows.Count > 0)
                            {
                                user.ProjectId = DataReader.ReadInt(dsUser.Tables[0].Rows[0], "ProjectID");
                                user.ProjectName = DataReader.ReadString(dsUser.Tables[0].Rows[0], "ProjectName");
                                user.OptimizationRunID = DataReader.ReadInt(dsUser.Tables[0].Rows[0], "OptimizationRunID");
                                user.DeviceName = DataReader.ReadString(dsUser.Tables[0].Rows[0], "DeviceName");
                                user.ReferenceChartTypeId = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "ReferenceChartTypeID");
                                user.ChartName = DataReader.ReadString(dsUser.Tables[1].Rows[0], "ChartName");
                                user.NumOfCols = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "NumOfCols");
                                user.NumOfRows = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "NumOfRows");
                                user.TotalPatches = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "TotalPatches");
                                user.InstrumentTypeId = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "InstrumentTypeID");
                                user.ProjectType = new ProjectType()
                                {
                                    ProjectTypeId = Convert.ToInt32(dsUser.Tables[0].Rows[0]["ProfileTypeID"]) != 3 ? DataReader.ReadShort(dsUser.Tables[0].Rows[0], "LicenseTypeID") : (short)51,
                                    ProjectTypeName = Convert.ToInt32(dsUser.Tables[0].Rows[0]["ProfileTypeID"]) != 3 ? Convert.ToString((ELicenseType)DataReader.ReadShort(dsUser.Tables[0].Rows[0], "LicenseTypeID")) : Convert.ToString((ELicenseType)51),
                                };
                                user.EntryDate = DataReader.ReadString(dsUser.Tables[0].Rows[0], "EntryDate");
                                user.PrintStandardType = DataReader.ReadString(dsUser.Tables[0].Rows[0], "PrintStandard");
                                //user.MeasureDeviceSrNo = DataReader.ReadString(dsUser.Tables[1].Rows[0], "MeasureDeviceSrNo");
                                //user.ActivationKey =
                                //    Utilities.GetMd5Hash(DateTime.Now.Year.ToString() + DateTime.Now.DayOfYear.ToString() + "+yg92s+" + user.ReferenceChartData.MeasureDeviceSrNo);
                                var UVMode = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "UVMode");
                                var DensityType = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "DensityType");

                                user.MeasurementSettings = new MeasurementSettingApp()
                                {
                                    IlluminantName = DataReader.ReadString(dsUser.Tables[1].Rows[0], "IlluminantType"),
                                    ObserverTypeName = Utilities.GetValueFromDescription<EObserverType>(DataReader.ReadString(dsUser.Tables[1].Rows[0], "ObserverType")),
                                    UvIncludedName = ((UVMode == 0) ? EUVIncludedMode.UV.ToString() : (UVMode == 1) ? EUVIncludedMode.UVPlus.ToString() : EUVIncludedMode.UVCut.ToString()),
                                    UVMode = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "UVMode"),
                                    //ReadMode = DataReader.ReadString(dsUser.Tables[1].Rows[0], "ReadMode"),
                                    MeasurementCount = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "MeasurementCount"),
                                    DensityType = ((DensityType == 1) ? EDensityType.ANSIE.ToString() : EDensityType.ANSIT.ToString()) //---Pass by deafult "Status_T"
                                };

                                user.ReferenceCharts = dsUser.Tables[2].AsEnumerable().
                                    Select(x =>
                                           new ReferenceChart()
                                           {
                                               SampleName = DataReader.ReadString(x, "SampleName"),
                                               HtmlColor = DataReader.ReadString(x, "HTMLColor"),
                                               LabL = DataReader.ReadDecimalNullValue(x, "LabL"),
                                               LabA = DataReader.ReadDecimalNullValue(x, "LabA"),
                                               LabB = DataReader.ReadDecimalNullValue(x, "LabB")
                                           }).ToList();

                                var dsReferenceChartSetting = DataAccessFactory.Instance.GetDsResult("[CPSpot].[GetSpotReferenceChartSetting]",
                                                                                         new object[] { Convert.ToInt32(dsUser.Tables[0].Rows[0]["ProjectID"]) });
                                if (dsReferenceChartSetting.Tables[0].Rows.Count > 0)
                                {
                                    user.ChartSetting = new ChartSetting();
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("ChartSizeX"))
                                        user.ChartSetting.ChartSizeX = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "ChartSizeX");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("ChartSizeY"))
                                        user.ChartSetting.ChartSizeY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "ChartSizeY");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("BarSizeX"))
                                        user.ChartSetting.BarSizeX = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "BarSizeX");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("BarSizeY"))
                                        user.ChartSetting.BarSizeY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "BarSizeY");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("MarkerDistance"))
                                        user.ChartSetting.MarkerDistance = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "MarkerDistance");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("PatchSizeX"))
                                        user.ChartSetting.PatchSizeX = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "PatchSizeX");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("PatchSizeY"))
                                        user.ChartSetting.PatchSizeY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "PatchSizeY");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("DistToFirstLineY"))
                                        user.ChartSetting.DistToFirstLineY = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "DistToFirstLineY");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("DistToFirstField"))
                                        user.ChartSetting.DistToFirstField = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "DistToFirstField");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("DistToBar"))
                                        user.ChartSetting.DistToBar = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "DistToBar");

                                    //For IO devices...
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("FirstSample"))
                                        user.ChartSetting.FirstSample = DataReader.ReadString(dsReferenceChartSetting.Tables[0].Rows[0], "FirstSample");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("ReadDirection"))
                                        user.ChartSetting.ReadDirection = DataReader.ReadString(dsReferenceChartSetting.Tables[0].Rows[0], "ReadDirection");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("ReadMode"))
                                        user.ChartSetting.ReadMode = DataReader.ReadString(dsReferenceChartSetting.Tables[0].Rows[0], "ReadMode");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("RowsPerPage"))
                                        user.ChartSetting.RowsPerPage = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "RowsPerPage");
                                    if (dsReferenceChartSetting.Tables[0].Columns.Contains("Pages"))
                                        user.ChartSetting.Pages = DataReader.ReadDecimal(dsReferenceChartSetting.Tables[0].Rows[0], "Pages");
                                }
                            }
                        }
                        else
                        {
                            if (dsUser.Tables[1].Rows.Count > 0)
                            {
                                user.ProjectId = DataReader.ReadInt(dsUser.Tables[0].Rows[0], "ProjectID");
                                user.ProjectName = DataReader.ReadString(dsUser.Tables[0].Rows[0], "ProjectName");
                                user.OptimizationRunID = DataReader.ReadInt(dsUser.Tables[0].Rows[0], "OptimizationRunID");
                                user.DeviceName = DataReader.ReadString(dsUser.Tables[0].Rows[0], "DeviceName");
                                user.ReferenceChartTypeId = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "ReferenceChartTypeID");
                                user.ChartName = DataReader.ReadString(dsUser.Tables[1].Rows[0], "ChartName");
                                user.NumOfCols = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "NumOfCols");
                                user.NumOfRows = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "NumOfRows");
                                user.TotalPatches = DataReader.ReadInt(dsUser.Tables[1].Rows[0], "TotalPatches");
                                user.InstrumentTypeId = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "InstrumentTypeID");
                                user.ProjectType = new ProjectType()
                                {
                                    ProjectTypeId = Convert.ToInt32(dsUser.Tables[0].Rows[0]["ProfileTypeID"]) != 3 ? DataReader.ReadShort(dsUser.Tables[0].Rows[0], "LicenseTypeID") : (short)51,
                                    ProjectTypeName = Convert.ToInt32(dsUser.Tables[0].Rows[0]["ProfileTypeID"]) != 3 ? Convert.ToString((ELicenseType)DataReader.ReadShort(dsUser.Tables[0].Rows[0], "LicenseTypeID")) : Convert.ToString((ELicenseType)51),
                                };
                                user.EntryDate = DataReader.ReadString(dsUser.Tables[0].Rows[0], "EntryDate");
                                user.PrintStandardType = DataReader.ReadString(dsUser.Tables[0].Rows[0], "PrintStandard");
                                //user1.MeasureDeviceSrNo = DataReader.ReadString(dsUser.Tables[1].Rows[0], "MeasureDeviceSrNo");
                                //user1.ActivationKey =
                                //  Utilities.GetMd5Hash(DateTime.Now.Year.ToString() + DateTime.Now.DayOfYear.ToString() + "+yg92s+" + user.ReferenceChartData.MeasureDeviceSrNo);
                                var UVMode = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "UVMode");
                                var DensityType = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "DensityType");

                                user.MeasurementSettings = new MeasurementSettingApp()
                                {
                                    IlluminantName = DataReader.ReadString(dsUser.Tables[1].Rows[0], "IlluminantType"),
                                    ObserverTypeName = Utilities.GetValueFromDescription<EObserverType>(DataReader.ReadString(dsUser.Tables[1].Rows[0], "ObserverType")),
                                    UvIncludedName = ((UVMode == 0) ? EUVIncludedMode.UV.ToString() : (UVMode == 1) ? EUVIncludedMode.UVPlus.ToString() : EUVIncludedMode.UVCut.ToString()),
                                    UVMode = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "UVMode"),
                                    //ReadMode = DataReader.ReadString(dsUser.Tables[1].Rows[0], "ReadMode"),
                                    MeasurementCount = DataReader.ReadShort(dsUser.Tables[1].Rows[0], "MeasurementCount"),
                                    DensityType = ((DensityType == 1) ? EDensityType.ANSIE.ToString() : EDensityType.ANSIT.ToString()) //---Pass by deafult "Status_T"
                                };

                                user.ReferenceCharts = dsUser.Tables[2].AsEnumerable().
                                    Select(x =>
                                           new ReferenceChart()
                                           {
                                               ChartTypeId = DataReader.ReadShort(x, "ChartTypeID"),
                                               SampleName = DataReader.ReadString(x, "SampleName"),
                                               CmykC = DataReader.ReadDecimal(x, "CMYK_C"),
                                               CmykM = DataReader.ReadDecimal(x, "CMYK_M"),
                                               CmykY = DataReader.ReadDecimal(x, "CMYK_Y"),
                                               CmykK = DataReader.ReadDecimal(x, "CMYK_K"),
                                               PatchTypeId = DataReader.ReadShort(x, "PatchTypeID"),
                                               HtmlColor = DataReader.ReadString(x, "HTMLColor"),
                                               LabL = DataReader.ReadDecimalNullValue(x, "LabL"),
                                               LabA = DataReader.ReadDecimalNullValue(x, "LabA"),
                                               LabB = DataReader.ReadDecimalNullValue(x, "LabB")
                                               
                                           }).ToList();

                             

                                var reader = DataAccessFactory.Instance.GetXmlResult("CPSync4D.GetChartSettings", new object[] { user.ReferenceChartTypeId });
                                var dsChartSettings = new DataSet();
                                dsChartSettings.ReadXml(reader);
                                if (dsChartSettings.Tables.Count > 0)
                                {
                                    user.ChartSetting = new ChartSetting();
                                    if (dsChartSettings.Tables[0].Columns.Contains("ChartSizeX"))
                                        user.ChartSetting.ChartSizeX = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "ChartSizeX");
                                    if (dsChartSettings.Tables[0].Columns.Contains("ChartSizeY"))
                                        user.ChartSetting.ChartSizeY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "ChartSizeY");
                                    if (dsChartSettings.Tables[0].Columns.Contains("BarSizeX"))
                                        user.ChartSetting.BarSizeX = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "BarSizeX");
                                    if (dsChartSettings.Tables[0].Columns.Contains("BarSizeY"))
                                        user.ChartSetting.BarSizeY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "BarSizeY");
                                    if (dsChartSettings.Tables[0].Columns.Contains("MarkerDistance"))
                                        user.ChartSetting.MarkerDistance = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "MarkerDistance");
                                    if (dsChartSettings.Tables[0].Columns.Contains("PatchSizeX"))
                                        user.ChartSetting.PatchSizeX = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "PatchSizeX");
                                    if (dsChartSettings.Tables[0].Columns.Contains("PatchSizeY"))
                                        user.ChartSetting.PatchSizeY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "PatchSizeY");
                                    if (dsChartSettings.Tables[0].Columns.Contains("DistToFirstLineY"))
                                        user.ChartSetting.DistToFirstLineY = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToFirstLineY");
                                    if (dsChartSettings.Tables[0].Columns.Contains("DistToFirstField"))
                                        user.ChartSetting.DistToFirstField = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToFirstField");
                                    if (dsChartSettings.Tables[0].Columns.Contains("DistToBar"))
                                        user.ChartSetting.DistToBar = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "DistToBar");

                                    //For IO devices...
                                    if (dsChartSettings.Tables[0].Columns.Contains("FirstSample"))
                                        user.ChartSetting.FirstSample = DataReader.ReadString(dsChartSettings.Tables[0].Rows[0], "FirstSample");
                                    if (dsChartSettings.Tables[0].Columns.Contains("ReadDirection"))
                                        user.ChartSetting.ReadDirection = DataReader.ReadString(dsChartSettings.Tables[0].Rows[0], "ReadDirection");
                                    if (dsChartSettings.Tables[0].Columns.Contains("ReadMode"))
                                        user.ChartSetting.ReadMode = DataReader.ReadString(dsChartSettings.Tables[0].Rows[0], "ReadMode");
                                    if (dsChartSettings.Tables[0].Columns.Contains("RowsPerPage"))
                                        user.ChartSetting.RowsPerPage = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "RowsPerPage");
                                    if (dsChartSettings.Tables[0].Columns.Contains("Pages"))
                                        user.ChartSetting.Pages = DataReader.ReadDecimal(dsChartSettings.Tables[0].Rows[0], "Pages");
                                }
                            }
                        }
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UserLogin"));
            return user;
        }

        public int validateUserByAuthKey(string authToken)
        {
            var outParam = new Parameters() { ParameterName = "@valid", Type = DbType.Int32 };
            Common.ImplementBusinessLogic(
                () =>
                {
                    //returnValue = DataAccessFactory.
                    //    Instance.
                    //    ExecuteProcedure(
                    //        "[CP].[CheckUserAuthentication]",
                    //        new object[]
                    //                {
                    //                    authToken
                    //                },new List<Parameters>{
                    //                    );

                    DataAccessFactory.
                       Instance.
                       ExecuteProcedure("[CP].[CheckUserAuthentication]",
                                        new List<Parameters>()
                                                {
                                                     new Parameters() {ParameterName = "@authToken", Size = 5000, Type = DbType.String, Value = authToken},
                                                },
                                        new List<Parameters>()
                                                {
                                                     outParam
                                                });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateUserDevice"));
            return outParam.Value != null ? Convert.ToInt32(outParam.Value) : -1; ;
        }


        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
