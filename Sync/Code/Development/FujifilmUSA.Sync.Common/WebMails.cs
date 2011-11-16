using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;

namespace FujifilmUSA.Sync.Common
{
    public class WebMails
    {
        #region Send Error Email
        public static void SendErrorEmail(Exception Exc)
        {
            //========== Get The Template ==========//
            string FilePath = System.Web.HttpContext.Current.Server.MapPath("MailTemplates/ErrorMessage.htm");
            System.IO.StreamReader SR = new System.IO.StreamReader(FilePath);

            //========== Replace With Variables ==========//
            string HtmlBody = SR.ReadToEnd();
            HtmlBody = HtmlBody.Replace("##URL##", System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
            HtmlBody = HtmlBody.Replace("##MESSAGE##", Exc.Message);
            HtmlBody = HtmlBody.Replace("##STACKTRACE##", Exc.StackTrace);
            HtmlBody = HtmlBody.Replace("##IPADDRESS##", System.Web.HttpContext.Current.Request.UserHostAddress);
            //HtmlBody = HtmlBody.Replace("##USERNAME##", Fuji.Common.Sessions.FirstName + " " + Fuji.Common.Sessions.LastName + "(" + Fuji.Common.Sessions.Username + ")");

            //========== Send Email ==========//
            if (FujifilmUSA.Sync.Common.WebSettings.Domain != null || FujifilmUSA.Sync.Common.WebSettings.Domain != "")
            {
                SendEmail("", "", "", "", "website error from :- " + FujifilmUSA.Sync.Common.WebSettings.Domain + Exc.Message, HtmlBody, null);
            }
            else
            {
                SendEmail("", "", "", "", "[Taskero WebSite Error :- " + Exc.Message, HtmlBody, null);
            }
        }
        #endregion


        #region MailSend
        /// <summary>
        /// Used to send the email
        /// </summary>
        /// <param name="To">Email Address of recipient</param>
        /// <param name="From">Email Address of sender</param>
        /// <param name="CC">Other Email Address of recipient</param>
        /// <param name="BCC">Other Email Address of recipient</param>
        /// <param name="Subject">Subject of the email</param>
        /// <param name="Body">Email body</param>
        /// <param name="EmailAttechment">Attachment with the email address</param>
        public static void SendEmail(string To, string From, string CC, string BCC, string Subject, string Body, ArrayList EmailAttechment)
        {

            System.Net.Mail.Attachment attachFile = null;


            //========== Find the config application path ==========//



            System.Configuration.Configuration Config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath);

            //========== Find the mail setting from the web config file ==========//
            System.Net.Configuration.MailSettingsSectionGroup MailSettings = (System.Net.Configuration.MailSettingsSectionGroup)Config.GetSectionGroup("system.net/mailSettings");

            //========== Obtain the Network Credentials from the mailSettings section ==========//
            NetworkCredential Credential = new NetworkCredential(MailSettings.Smtp.Network.UserName, MailSettings.Smtp.Network.Password);

            //========== Create the SMTP Client ==========//
            System.Net.Mail.SmtpClient Client = new System.Net.Mail.SmtpClient();

            //========== Assign the host address ==========//
            Client.Host = MailSettings.Smtp.Network.Host;

            //========== Assign the network credentials to the smtp client ==========//
            Client.Credentials = Credential;

            //========== Create the mail object ==========//
            System.Net.Mail.MailMessage ObjEmail = new System.Net.Mail.MailMessage();

            //========== Assign the values to the mail object ==========//
            if (From == "") { ObjEmail.From = new System.Net.Mail.MailAddress(FujifilmUSA.Sync.Common.WebSettings.Email_FROM); } else { ObjEmail.From = new System.Net.Mail.MailAddress(From); }
            if (To == "") { ObjEmail.To.Add(FujifilmUSA.Sync.Common.WebSettings.Email); } else { ObjEmail.To.Add(To); }
            if (BCC.Length != 0)
                ObjEmail.Bcc.Add(BCC);
            if (CC.Length != 0)
                ObjEmail.CC.Add(CC);
            ObjEmail.Subject = Subject.Replace("\n", " ").Replace("\r", " ").Replace("\t", "");
            ObjEmail.IsBodyHtml = true;
            ObjEmail.Priority = System.Net.Mail.MailPriority.Normal;
            ObjEmail.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnFailure;
            ObjEmail.Body = Body;

            //========== Check the email attachment for the email ==========//
            if (EmailAttechment != null && EmailAttechment.Count != 0)
            {
                for (int i = 0; i <= EmailAttechment.Count - 1; i++)
                {
                    attachFile = new System.Net.Mail.Attachment(EmailAttechment[i].ToString());

                    ObjEmail.Attachments.Add(attachFile);
                }
            }
            Client.Send(ObjEmail);

            if (attachFile != null)
            {
                attachFile.Dispose();
                attachFile = null;
            }

        }
        #endregion

    }
}
