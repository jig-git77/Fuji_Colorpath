using System;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
using System.Net.Mail;

namespace FujifilmUSA.Sync.Business
{
    public class LogManager
    {
        #region Set Global Variables
        private delegate void SendEmail(AppErrorLog log);
        //public static string Username = (SessionManager.Instance.CurrentUser != null)
        //                                      ? (SessionManager.Instance.CurrentUser.FirstName + " " + SessionManager.Instance.CurrentUser.LastName)
        //                                      : "NULL";
        //public static string UserEmail = (SessionManager.Instance.CurrentUser != null)
        //                                     ? (SessionManager.Instance.CurrentUser.Email)
        //                                     : "NULL";
        #endregion

        public static void WriteLog(Exception e, BusinessHandlerRequest request, bool SendEmail = true)
        {
            var appErrorLog = new AppErrorLog
            {
                ExMessage = e.Message,
                ExStackTracke = e.StackTrace,
                TimeStamp = DateTime.Now,
                UserId =
                                          (SessionManager.Instance.CurrentUser != null)
                                              ? SessionManager.Instance.CurrentUser.UserId
                                              : 0
            };
            var errorHandlingBusiness = BusinessFactory<IMiscBusiness>.CreateObject();
            errorHandlingBusiness.InsertAppErrorLog(appErrorLog);

            string currentUrl = System.Web.HttpContext.Current.Request.Url != null ? System.Web.HttpContext.Current.Request.Url.ToString() : "";
            if (currentUrl != "" && currentUrl.Contains("PluginDataService.svc"))
            {
                int ProjectID = 0;
                string ProjectType = "";
                if (SessionManager.Instance.CurrentProject != null)
                {
                    ProjectID = SessionManager.Instance.CurrentProject.ProjectId;
                    if (currentUrl.Contains("/SaveMeasurement") || currentUrl.Contains("/GetMeasurementsByOptimizeId") || currentUrl.Contains("/DeleteMeasurement"))
                    {
                        ProjectType = "Sync4D";
                    }
                    else if (currentUrl.Contains("/Save1DMeasurement") || currentUrl.Contains("/Get1DMeasurementsByOptimizeId") || currentUrl.Contains("/Delete1DMeasurement"))
                    {
                        ProjectType = "Sync1D";
                    }
                    else if (currentUrl.Contains("/SaveVMeasurement") || currentUrl.Contains("/GetVMeasurementsByProjectId") || currentUrl.Contains("/DeleteVMeasurement"))
                    {
                        ProjectType = "SyncValidation";
                    }
                    else if (currentUrl.Contains("/SaveSpotMeasurement") || currentUrl.Contains("/GetSpotMeasurementsByOptimizeId") || currentUrl.Contains("/DeleteSpotMeasurement"))
                    {
                        ProjectType = "SyncSpot";
                    }

                    if (ProjectType != "")
                        errorHandlingBusiness.InsertMeasurementAppErrorLog(appErrorLog, SessionManager.Instance.CurrentProject.ProjectId, ProjectType);
                }
            }

            if (ApplicationConfiguration.Instance.SendEmailNotificationOnError && SendEmail)
            {
                SendEmail errorEmail = SendErrorEmail;
                errorEmail.BeginInvoke(appErrorLog, null, null);
            }
        }

        public static void SendErrorEmail(AppErrorLog log)
        {
            var errorMessage = new MailMessage
            {
                Subject = ApplicationConfiguration.Instance.SendEmailNotificationOnErrorSubject,
                IsBodyHtml = true,
                Body = "ErrorMessage: " + log.ExMessage + "<br/><br/>" + "ErrorStackTrace: " + log.ExStackTracke + "<br/><br/>" + "GeneratedTime: " + log.TimeStamp + "<br/><br/>" + "UserName: " +
                                                ((SessionManager.Instance.CurrentUser != null)
                                                  ? (SessionManager.Instance.CurrentUser.FirstName + " " + SessionManager.Instance.CurrentUser.LastName)
                                                  : "NULL")
                                       + "<br/>" + "Email: " +
                                                ((SessionManager.Instance.CurrentUser != null)
                                                     ? (SessionManager.Instance.CurrentUser.Email)
                                                     : "NULL")
            };
            errorMessage.To.Add(ApplicationConfiguration.Instance.SendEmailNotificationOnErrorTo);
            var mySmtpClient = new SmtpClient();
            mySmtpClient.Send(errorMessage);
        }

        public static void SendUserCreatedEmail(User user)
        {
            var dicTemplate = BusinessFactory<IMiscDataBusiness>.CreateObject().GetTemplate((int)Entity.Template.UserCreatedEmail);
            var message = new MailMessage
            {
                Subject = FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage("Email_Subject_SendUserCreatedEmail"),
                IsBodyHtml = true,
                Body = dicTemplate[(int)Entity.Template.Base].Replace("##Content##",
                                          string.Format(
                                              dicTemplate[(int)Entity.Template.UserCreatedEmail],
                                              user.FirstName, user.LastName,
                                              string.Format(ApplicationConfiguration.Instance.PasswordSetLink,
                                                            user.AuthenticationKey)))
            };
            message.To.Add(user.Email);
            message.Bcc.Add(ApplicationConfiguration.Instance.SendEmailNotificationOnErrorTo);
            var mySmtpClient = new SmtpClient();
            mySmtpClient.Send(message);
        }

        public static void SendPasswordResetEmail(User user)
        {
            var dicTemplate = BusinessFactory<IMiscDataBusiness>.CreateObject().GetTemplate((int)Entity.Template.PasswordResetEmail);
            var message = new MailMessage
            {
                Subject = FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage("Email_Subject_SendPasswordResetEmail"),
                IsBodyHtml = true,
                Body = dicTemplate[(int)Entity.Template.Base].Replace("##Content##",
                                                            string.Format(
                                                                dicTemplate[(int)Entity.Template.PasswordResetEmail],
                                                                user.FirstName, user.LastName, string.Format(ApplicationConfiguration.Instance.PasswordReSetLink, user.AuthenticationKey)))
            };
            message.To.Add(user.Email);
            message.Bcc.Add(ApplicationConfiguration.Instance.SendEmailNotificationOnErrorTo);
            var mySmtpClient = new SmtpClient();
            mySmtpClient.Send(message);
        }

        public static void SendPasswordForceResetEmail(User user)
        {
            var dicTemplate = BusinessFactory<IMiscDataBusiness>.CreateObject().GetTemplate((int)Entity.Template.PasswordResetEmail);
            var message = new MailMessage
            {
                Subject = FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage("Email_Subject_SendPasswordForceResetEmail"),
                IsBodyHtml = true,
                Body = dicTemplate[(int)Entity.Template.Base].Replace("##Content##",
                                                            string.Format(
                                                                dicTemplate[(int)Entity.Template.PasswordResetEmail],
                                                                user.FirstName, user.LastName, string.Format(ApplicationConfiguration.Instance.PasswordReSetLink, user.AuthenticationKey)))
            };

            message.To.Add(user.Email);
            message.Bcc.Add(ApplicationConfiguration.Instance.SendEmailNotificationOnErrorTo);
            var mySmtpClient = new SmtpClient();
            mySmtpClient.Send(message);
        }

        public static void SendActivityValidationRuleEmail(string subject, string body)
        {
            var dicTemplate = BusinessFactory<IMiscDataBusiness>.CreateObject().GetTemplate(0);
            var message = new MailMessage
            {
                Subject = subject,
                IsBodyHtml = true,
                Body = dicTemplate[(int)Entity.Template.Base].Replace("##Content##", body)
            };
            message.To.Add(ApplicationConfiguration.Instance.SendEmailNotificationOnErrorTo);
            var mySmtpClient = new SmtpClient();
            mySmtpClient.Send(message);
        }
    }
}
