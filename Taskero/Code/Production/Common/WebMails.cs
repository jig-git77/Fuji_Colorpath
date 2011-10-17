using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mail;
using System.Net;
using System.Collections;

namespace Fuji.Common
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
            HtmlBody = HtmlBody.Replace("##USERNAME##", Fuji.Common.Sessions.FirstName + " " + Fuji.Common.Sessions.LastName + "(" + Fuji.Common.Sessions.Username + ")");

            //========== Send Email ==========//
            if (Fuji.Common.WebSettings.Domain != null || Fuji.Common.WebSettings.Domain != "")
            {
                SendEmail("", "", "", "", "website error from :- " + Fuji.Common.WebSettings.Domain + Exc.Message, HtmlBody, null);
            }
            else  {
             SendEmail("", "", "","", "[Taskero WebSite Error :- " + Exc.Message, HtmlBody, null);
            }
        }
        #endregion

       
        #region Send Error Email With Attachment
        //
        public static void SendErrorEmailWithAttachment(Exception Exc, String AttachMessage)
        {

            try
            {

            //========== Get The Template ==========//
            string FilePath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/UploadFiles/temp/");
            string FileName = FilePath + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + ".txt";

            System.IO.File.WriteAllText(FileName, AttachMessage);

            string EmailBodyPath = System.Web.HttpContext.Current.Server.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath + "/MailTemplates/ErrorMessage.htm");
            System.IO.StreamReader SR = new System.IO.StreamReader(EmailBodyPath);


            if (! System.IO.File.Exists(FileName)) throw new Exception ("file not exists for attachement");
            //========== Replace With Variables ==========//
            string HtmlBody = SR.ReadToEnd();
            HtmlBody = HtmlBody.Replace("##URL##", System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
            HtmlBody = HtmlBody.Replace("##MESSAGE##", Exc.Message);
            HtmlBody = HtmlBody.Replace("##STACKTRACE##", Exc.StackTrace);
            HtmlBody = HtmlBody.Replace("##IPADDRESS##", System.Web.HttpContext.Current.Request.UserHostAddress);
            HtmlBody = HtmlBody.Replace("##USERNAME##", Fuji.Common.Sessions.FirstName + " " + Fuji.Common.Sessions.LastName + "(" + Fuji.Common.Sessions.Username + ")");

            //========== Send Email ==========//
            if (Fuji.Common.WebSettings.Domain != null || Fuji.Common.WebSettings.Domain != "")
            {
                if (AttachMessage != null && AttachMessage != String.Empty)
                {
                    //string uploadPath = System.Web.HttpContext.Current.Server.MapPath("../UploadFiles/temp/");


                    System.Collections.ArrayList Attachment = new System.Collections.ArrayList();
                    Attachment.Add(FileName);
                    SendEmail("", "", "", "", "website error from :- " + Fuji.Common.WebSettings.Domain + "  " + " Error uploading Sync Measurement", HtmlBody, Attachment);

                }
                else { SendEmail("", "", "", "", "website error from :- " + Fuji.Common.WebSettings.Domain + Exc.Message, HtmlBody, null); };
            }
            else
            {
                SendEmail("", "", "", "", "[Taskero WebSite Error :- " + Exc.Message, HtmlBody, null);
            }

            if (System.IO.File.Exists(FileName)) System.IO.File.Delete(FileName);

            } catch (Exception exc)
            {
                throw exc;
            }
        }
        #endregion


        #region Send Retrieve Password
        public static void SendRetrievePassword(string Email, string Username, string Password, string Name)
        {
            //========== Get The Template ==========//
            string FilePath = System.Web.HttpContext.Current.Server.MapPath("MailTemplates/ForgotPassword.htm");
            System.IO.StreamReader SR = new System.IO.StreamReader(FilePath);

            //========== Replace With Variables ==========//
            string HtmlBody = SR.ReadToEnd();
            HtmlBody = HtmlBody.Replace("##NAME##", Name);
            HtmlBody = HtmlBody.Replace("##USERNAME##", Username);
            HtmlBody = HtmlBody.Replace("##PASSWORD##", Password);

            //========== Send Email ==========//
            SendEmail(Email, Fuji.Common.WebSettings.Email_FROM, "","", "FujiFilm Retrieve Password :- " + Name, HtmlBody, null);
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
        public static void SendEmail(string To, string From,string CC, string BCC, string Subject, string Body, ArrayList EmailAttechment)
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
            if (From == "") { ObjEmail.From = new System.Net.Mail.MailAddress(Fuji.Common.WebSettings.Email_FROM); } else { ObjEmail.From = new System.Net.Mail.MailAddress(From); }
            if (To == "") { ObjEmail.To.Add(Fuji.Common.WebSettings.Email); } else { ObjEmail.To.Add(To); }
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
