#region SYSTEM
using System;
using System.Data;
using System.Data.SqlClient;
#endregion

#region Others
using Fuji.DataAccess;
#endregion

namespace Fuji.Business
{
    public class TrackUserAccessedPagesData : TrackUserAccessedPages
    {
        public void TrackUserAccessPage(int intTaskeroUserId)
        {
            try
            {
                // === If Setting in web.config to enter data in database : Manish(10/04/2009) === //
                if (!this.IsTrackUserAccessedPages)
                    return;

                if (intTaskeroUserId == 0)
                    return;

                this.TaskeroUserId = intTaskeroUserId;
                this.PageName = System.Web.HttpContext.Current.Request.Path.Substring(System.Web.HttpContext.Current.Request.Path.LastIndexOf('/') + 1);
                this.WebPageURL = System.Web.HttpContext.Current.Request.Url.OriginalString;
                this.AccessedDateTime = DateTime.Now;
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                    this.AccessedFromIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                else
                    this.AccessedFromIP = System.Web.HttpContext.Current.Request.UserHostAddress;

                this.BrowserInfo = System.Web.HttpContext.Current.Request.Browser.Id + " - " + System.Web.HttpContext.Current.Request.Browser.Type + " - Version : " + System.Web.HttpContext.Current.Request.Browser.Version;

                string[] str = System.Web.HttpContext.Current.Request.UserAgent.Replace(" ", "").Replace(';', '~').Replace('(', '~').Replace(')', '~').Split('~');


                string strOSInfo = "";
                string strOS = "";
                for (int i = 0; i < str.Length; i++)
                {
                    strOS = str[i];
                    bool flag = false;
                    switch (strOS)
                    {
                        case "WindowsNT6.0":
                            strOSInfo = "Windows Vista";
                            flag = true;
                            break;
                        case "WindowsNT5.2":
                            strOSInfo = "Windows Server 2003/Windows x64";
                            flag = true;
                            break;
                        case "WindowsNT5.1":
                            strOSInfo = "Windows XP";
                            flag = true;
                            break;
                        case "WindowsNT5.0":
                            strOSInfo = "Windows 2000";
                            flag = true;
                            break;
                        case "WindowsNT4.0":
                            strOSInfo = "Windows NT";
                            flag = true;
                            break;
                        case "WindowsME":
                            strOSInfo = "Windows ME";
                            flag = true;
                            break;
                        case "Windows98":
                            strOSInfo = "Windows98";
                            flag = true;
                            break;
                        case "Windows95":
                            strOSInfo = "Windows95";
                            flag = true;
                            break;
                        default:
                            strOSInfo = "";
                            break;
                    }
                    if (flag)
                        break;
                }

                if (strOSInfo == "")
                    strOSInfo = System.Web.HttpContext.Current.Request.UserAgent.Split(';')[2];

                this.OS = strOSInfo;

                this.Insert();

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #region uspInsertCompany
        /// <summary>
        /// Inserts User Accessed Pages Information
        /// </summary>
        public void Insert()
        {
            try
            {
                // === If Setting in web.config to enter data in database : Manish(10/04/2009) === //
                if (!this.IsTrackUserAccessedPages)
                    return;

                //========== Execute Command ==========//
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandText = "[dbo].[uspInsertTrackUserAccessedPages]";
                Cmd.CommandType = CommandType.StoredProcedure;
                Cmd.Parameters.AddWithValue("@TaskeroUserId", this.TaskeroUserId);
                Cmd.Parameters.AddWithValue("@PageName", this.PageName);
                Cmd.Parameters.AddWithValue("@WebPageURL", this.WebPageURL);
                Cmd.Parameters.AddWithValue("@AccessedDateTime", this.AccessedDateTime);
                Cmd.Parameters.AddWithValue("@AccessedFromIP", this.AccessedFromIP);
                Cmd.Parameters.AddWithValue("@BrowserInfo", this.BrowserInfo);
                Cmd.Parameters.AddWithValue("@OS", this.OS);

                (new Fuji.DataAccess.Database()).ExecuteNonQuery(Cmd);

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion
    }
}
