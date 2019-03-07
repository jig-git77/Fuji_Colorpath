using System;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.Interface;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class UserBusinessImpl : IUserBusiness
    {
        #region IUser Members

        public User UserLogin(string userName, string password, UserType userType)
        {
            return new User();
        }
        public List<User> GetUser(int userId,int currentUserId)
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
        
        public User GetUserAuthenticationInformation(System.Guid authenticationKey)
        {
            return new User();
        }

        public int UpdateUserAuthenticationInformation(User user, UserAuthenticationUpdateType updateType)
        {
            return 0;
        }

        public User UpdatePreResetPasswordInformation(string loginId)
        {
            return new User();
        }

        public int UpdatePassword(int loginId, string password)
        {
            return 0;
        }

        public int UpdateSecurityInformation(int loginId, int securityQuestionId, string securityAnswer)
        {
            return 0;
        }

        public void SendEmailToUser(User user, UserAuthenticationUpdateType updateType)
        {
            //Do Nothing...
        }

        public int GetUserType(string loginId)
        {
            return 0;
        }

        public List<Language> GetLanguages()
        {
            return new List<Language>();
        }

        public int UpdateLanguage(int loginId, string languageCultureCode)
        {
            return 0;
        }

        public int UpdateUserDeviceInfo(int loginId, int measurementDeviceId, int densityType, int UVMode)
        {
            return 0;
        }

        public List<MeasurementDevice> GetMeasurementDeviceList(int loginId, int deviceId, bool isSpotTargetType = false)
        {
            return new List<MeasurementDevice>();
        }

        public int InsertLoginActivityLogs(int UserId, string OSName, string BrowserName, string EntryTime)
        {
            return 0;
        }

        public bool ValidateUser(string tocken)
        {
            return true;
        }

        public UserReferenceChart UserLoginReferenceChart(string userName, string password, UserType userType)
        {
            return new UserReferenceChart();
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion

       
    }
}
