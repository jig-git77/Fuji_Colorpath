using System;
using System.Collections;
using System.Net;

namespace FujifilmUSA.Sync.Common
{
    public class WebMails
    {
        #region Send Error Email
        public static void SendErrorEmail(Exception exc)
        {
            //========== Get The Template ==========//
            var filePath = System.Web.HttpContext.Current.Server.MapPath("MailTemplates/ErrorMessage.htm");
            var sr = new System.IO.StreamReader(filePath);

            //========== Replace With Variables ==========//
            var htmlBody = sr.ReadToEnd();
            htmlBody = htmlBody.Replace("##URL##", System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
            htmlBody = htmlBody.Replace("##MESSAGE##", exc.Message);
            htmlBody = htmlBody.Replace("##STACKTRACE##", exc.StackTrace);
            htmlBody = htmlBody.Replace("##IPADDRESS##", System.Web.HttpContext.Current.Request.UserHostAddress);
            //HtmlBody = HtmlBody.Replace("##USERNAME##", Fuji.Common.Sessions.FirstName + " " + Fuji.Common.Sessions.LastName + "(" + Fuji.Common.Sessions.Username + ")");

            //========== Send Email ==========//
            if (FujifilmUSA.Sync.Common.WebSettings.Domain != null || FujifilmUSA.Sync.Common.WebSettings.Domain != "")
            {
                SendEmail("", "", "", "", "website error from :- " + FujifilmUSA.Sync.Common.WebSettings.Domain + exc.Message, htmlBody, null);
            }
            else
            {
                SendEmail("", "", "", "", "[Taskero WebSite Error :- " + exc.Message, htmlBody, null);
            }
        }
        #endregion


        #region MailSend
        /// <summary>
        /// Used to send the email
        /// </summary>
        /// <param name="to">Email Address of recipient</param>
        /// <param name="from">Email Address of sender</param>
        /// <param name="cc">Other Email Address of recipient</param>
        /// <param name="bcc">Other Email Address of recipient</param>
        /// <param name="subject">Subject of the email</param>
        /// <param name="body">Email body</param>
        /// <param name="emailAttechment">Attachment with the email address</param>
        public static void SendEmail(string to, string from, string cc, string bcc, string subject, string body, ArrayList emailAttechment)
        {

            System.Net.Mail.Attachment attachFile = null;


            //========== Find the config application path ==========//
            
            System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath);

            //========== Find the mail setting from the web config file ==========//
            var mailSettings = (System.Net.Configuration.MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");

            //========== Obtain the Network Credentials from the mailSettings section ==========//
            if (mailSettings != null)
            {
                var credential = new NetworkCredential(mailSettings.Smtp.Network.UserName, mailSettings.Smtp.Network.Password);

                //========== Create the SMTP Client ==========//
                var client = new System.Net.Mail.SmtpClient {Host = mailSettings.Smtp.Network.Host, Credentials = credential};

                //========== Create the mail object ==========//
                var objEmail = new System.Net.Mail.MailMessage
                                   {
                                       From = from == "" ? new System.Net.Mail.MailAddress(FujifilmUSA.Sync.Common.WebSettings.Email_FROM) : new System.Net.Mail.MailAddress(from)
                                   };

                //========== Assign the values to the mail object ==========//
                objEmail.To.Add(to == "" ? FujifilmUSA.Sync.Common.WebSettings.Email : to);
                if (bcc.Length != 0)
                    objEmail.Bcc.Add(bcc);
                if (cc.Length != 0)
                    objEmail.CC.Add(cc);
                objEmail.Subject = subject.Replace("\n", " ").Replace("\r", " ").Replace("\t", "");
                objEmail.IsBodyHtml = true;
                objEmail.Priority = System.Net.Mail.MailPriority.Normal;
                objEmail.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnFailure;
                objEmail.Body = body;

                //========== Check the email attachment for the email ==========//
                if (emailAttechment != null && emailAttechment.Count != 0)
                {
                    for (var i = 0; i <= emailAttechment.Count - 1; i++)
                    {
                        attachFile = new System.Net.Mail.Attachment(emailAttechment[i].ToString());

                        objEmail.Attachments.Add(attachFile);
                    }
                }
                client.Send(objEmail);
            }

            if (attachFile != null)
            {
                attachFile.Dispose();
            }

        }
        #endregion

    }
}
