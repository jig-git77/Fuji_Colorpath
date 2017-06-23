using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IUserBusiness : ITaskBase
    {
        User UserLogin(string userName, string password, UserType userType);
        List<User> GetUser(int userId, int currentUserId);
        int InsertUser(User user);
        int UpdateUser(User user);
        bool DeleteUser(int userId);
        User GetUserAuthenticationInformation(Guid authenticationKey);
        int UpdateUserAuthenticationInformation(User user, UserAuthenticationUpdateType updateType);
        User UpdatePreResetPasswordInformation(string loginId);
        int UpdatePassword(int loginId, string password);
        int UpdateSecurityInformation(int loginId, int securityQuestionId, string securityAnswer);
        void SendEmailToUser(User user, UserAuthenticationUpdateType updateType);
        int GetUserType(string loginId);
        List<Language> GetLanguages();
        int UpdateLanguage(int loginId, string languageCultureCode);
        int UpdateUserDeviceInfo(int loginId, int measurementDeviceId, int densityType, int UVMode);
        List<MeasurementDevice> GetMeasurementDeviceList(int loginId, int deviceId);
        int InsertLoginActivityLogs(int UserId, string OSName, string BrowserName, string EntryTime);
    }
}
