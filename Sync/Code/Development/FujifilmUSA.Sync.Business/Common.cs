using System;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.CustomExceptions;
using System.Configuration;

namespace FujifilmUSA.Sync.Business
{
    public class Common
    {
        internal static string ProfileDescriptionKey = "Description";
        internal static bool SendEmailNotificationOnError = Convert.ToBoolean(ConfigurationManager.AppSettings["SendEmailNotificationOnError"]);
        internal static string SendEmailNotificationOnErrorTo = ConfigurationManager.AppSettings["SendEmailNotificationOnErrorTo"];
        internal static string SendEmailNotificationOnErrorSubject = ConfigurationManager.AppSettings["SendEmailNotificationOnErrorSubject"];
        internal static string PasswordSetLink = ConfigurationManager.AppSettings["PasswordSetLink"];
        internal static string PasswordReSetLink = ConfigurationManager.AppSettings["PasswordReSetLink"];
        internal static string PasswordForceReSetLink = ConfigurationManager.AppSettings["PasswordForceReSetLink"];
        internal static string XmfColorOrganizerEncryptionKey = ConfigurationManager.AppSettings["XmfColorOrganizerEncryptionKey"];

        public static void ImplementBusinessLogic(Action action, BusinessHandlerRequest request)
        {
            try
            {
                action();
            }
            catch (DuplicateException e)
            {
                //LogManager.WriteLog(e, request);
                if (request.Handler != null)
                {
                    request.Handler(
                        request.TaskBase,
                        new ErrorArgs(
                            FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage(e.UserFriendlyMessage)));
                }
                else
                {
                    throw;
                }
            }
            catch (Exception e)
            {
                LogManager.WriteLog(e, request);
                if (request.Handler != null)
                {
                    request.Handler(
                        request.TaskBase,
                        new ErrorArgs(
                            FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage(request.UserFriendlyMessage)));
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
