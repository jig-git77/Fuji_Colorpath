using System;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business.ValidationRule;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Business
{
    public class LogActivityManager
    {
        private const string _loggedInFormat = "User Logged-in to the system";
        private const string _loggedOutFormat = "User Logged-out from the system";
        private const string _createProjectFormat = "User has created project with id={0} and name={1}";
        private const string _optimizeDlpFormat = "User has optimized dlp {0} for project id={1}";
        private const string _createDlpFormat = "User has created desitnation profile and dlp for project id={0}";
        private const string _changeProfileFormat = "User has change his/her profile";

        private static void WriteLogActivity(LogActivity logActivity)
        {
            var logActivityBusiness = BusinessFactory<IMiscBusiness>.CreateObject();
            logActivityBusiness.InsertLogActivity(logActivity);

            ValidationManager.ValidateRule(logActivity);
        }

        private static void WriteLogActivity(LogActivitySystemType systemType, LogActivityType activityType, object[] parameters, string ipAddress)
        {
            //If config settings sets EnableActivityLog to false, then don't log it...
            if (!ApplicationConfiguration.Instance.EnableActivityLog) return;

            var logActivity = new LogActivity
                                  {
                                      ActivityType = activityType,
                                      ActivityDate = DateTime.Now,
                                      UserId = (SessionManager.Instance.CurrentUser == null) ? 0 : SessionManager.Instance.CurrentUser.UserId,
                                      IpAddress = ipAddress,
                                      SystemType = systemType
                                  };

            switch (activityType)
            {
                case LogActivityType.None:
                    break;
                case LogActivityType.LoggedIn:
                    logActivity.Details = _loggedInFormat;
                    break;
                case LogActivityType.LoggedOut:
                    logActivity.Details = _loggedOutFormat;
                    break;
                case LogActivityType.CreateProject:
                    logActivity.Details = String.Format(_createProjectFormat, parameters);
                    break;
                case LogActivityType.OptimizeDlp:
                    logActivity.Details = String.Format(_optimizeDlpFormat, parameters);
                    break;
                case LogActivityType.CreateDlp:
                    logActivity.Details = String.Format(_createDlpFormat, parameters);
                    break;
                case LogActivityType.ChangeProfile:
                    logActivity.Details = _changeProfileFormat;
                    break;
            }

            WriteLogActivity(logActivity);
        }

        public static void WebLogActivity(LogActivityType activityType, object[] parameters, string ipAddress)
        {
            WriteLogActivity(LogActivitySystemType.Web, activityType, parameters, ipAddress);
        }

        public static void ServiceLogActivity(LogActivityType activityType, object[] parameters, string ipAddress)
        {
            WriteLogActivity(LogActivitySystemType.Service, activityType, parameters, ipAddress);
        }

        public static void ColorPathLogActivity(LogActivityType activityType, object[] parameters, string ipAddress)
        {
            WriteLogActivity(LogActivitySystemType.ColorPath, activityType, parameters, ipAddress);
        }
    }
}