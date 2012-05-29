using System;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
using System.Net.Mail;

namespace FujifilmUSA.Sync.Business
{
    public class LogManager
    {
        private delegate void SendEmail(AppErrorLog log);
        public static void WriteLog(Exception e, BusinessHandlerRequest request)
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

            if (ApplicationConfiguration.Instance.SendEmailNotificationOnError)
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
                                       Body = log.ExMessage + "</br>" + log.ExStackTracke + "</br>" + log.TimeStamp
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
                Subject = FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage("Email_Subject_WelcomeToColorPath"),
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
                Subject = FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage("Email_Subject_ResetYourPassword"),
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
                Subject = FujifilmUSA.Sync.Common.ResourceManager.GetResourceMessage("Email_Subject_ResetYourPassword"),
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
    }
}