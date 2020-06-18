
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Taskero.Business;
using Taskero.Common;
using TaskeroWeb.Models;

namespace TaskeroWeb
{
    public partial class Home : MasterBase
    {
        #region Page Load and Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //========== Check User Login ==========//
                Sessions.CheckUserLogin();

                //========== Assign Css ==========//            
                IMG_LogoImage.Src = "Branding/HeaderImage/" + Sessions.HeaderLogo.ToString();
                if (Sessions.Css != "") { LtrlCSS.Text = @"<link href='CSS/" + Sessions.Css + "' rel='stylesheet' type='text/css' />"; }

                //********* Calling the Method for Applying the Page Right ************//
                CheckRights();

                if (!this.Page.IsPostBack)
                {
                    TrackUserAccessedPagesData obj = new TrackUserAccessedPagesData();
                    obj.TrackUserAccessPage(Sessions.UserID);
                }
            }
            catch (Exception exc)
            {
                exc.Message.ToString();
            }
        }
        #endregion

        #region Methods & Functions

        #region Check Rights
        /// <summary>
        /// Method for the Page Rights
        /// </summary>
        private void CheckRights()
        {
            try
            {
                //========== Change Done before granullar permission ==========//
                //if (Sessions.RoleLevel >= 5 || Sessions.RoleLevel == 0)
                //{ TD_Admin.Visible = false; TD_Admin_Spacer.Visible = false; }
                //else
                //{ TD_Admin.Visible = true; TD_Admin_Spacer.Visible = true; }

                if (
                    Sessions.Access_Branding == true ||
                    Sessions.Add_Branch == true ||
                    Sessions.Add_Company == true ||
                    Sessions.Add_Device == true ||
                    Sessions.Add_Dongle == true ||
                    Sessions.Add_Support_User == true ||
                    Sessions.Add_Temp_RH_Device == true ||
                    Sessions.Add_User == true ||
                    Sessions.Delete_Branch == true ||
                    Sessions.Delete_Company == true ||
                    Sessions.Delete_Device == true ||
                    Sessions.Delete_Dongle == true ||
                    Sessions.Delete_Print_Target == true ||
                    Sessions.Delete_Temp_RH_Device == true ||
                    Sessions.Delete_User == true ||
                    Sessions.Remove_Support_User == true ||
                    Sessions.Update_Branch == true ||
                    Sessions.Update_Device == true ||
                    Sessions.Update_Dongle == true ||
                    Sessions.Update_Temp_RH_Device == true ||
                    Sessions.Update_User == true ||
                    Sessions.Access_Video == true ||
                    Sessions.Access_SyncView == true
                    )
                { TD_Admin.Visible = true; TD_Admin_Spacer.Visible = true; }
                else
                { TD_Admin.Visible = false; TD_Admin_Spacer.Visible = false; }

                if (Sessions.Access_VideoView)
                { TD_Videos.Visible = true; TD_Videos_Spacer.Visible = true; }
                else
                { TD_Videos.Visible = false; TD_Videos_Spacer.Visible = false; }

                // === Sync Tab Hide/Show Based on Permission === //
                if (Sessions.Access_SyncView)
                { TD_Processor.Visible = true; TD_Processor_Spacer.Visible = true; }
                else
                { TD_Processor.Visible = false; TD_Processor_Spacer.Visible = false; }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #endregion
    }
}