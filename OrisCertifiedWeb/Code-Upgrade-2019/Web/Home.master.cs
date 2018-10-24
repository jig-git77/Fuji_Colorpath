#region Using
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
using Fuji.Business;
using Fuji.Common;
#endregion

public partial class Home1 : MasterBase
{
    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Check User Login ==========//
            Sessions.CheckUserLogin();

            //========== Assign Css ==========//            
            IMG_LogoImage.Src = "Branding/HeaderImage/" + Fuji.Common.Sessions.HeaderLogo.ToString();
            if (Fuji.Common.Sessions.Css != "") { LtrlCSS.Text = @"<link href='CSS/" + Fuji.Common.Sessions.Css + "' rel='stylesheet' type='text/css' />"; }

            //********* Calling the Method for Applying the Page Right ************//
            CheckRights();

            if (!this.Page.IsPostBack)
            {
                Fuji.Business.TrackUserAccessedPagesData obj = new Fuji.Business.TrackUserAccessedPagesData();
                obj.TrackUserAccessPage(Fuji.Common.Sessions.UserID);
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
            //if (Fuji.Common.Sessions.RoleLevel >= 5 || Fuji.Common.Sessions.RoleLevel == 0)
            //{ TD_Admin.Visible = false; TD_Admin_Spacer.Visible = false; }
            //else
            //{ TD_Admin.Visible = true; TD_Admin_Spacer.Visible = true; }

            if (
                Fuji.Common.Sessions.Access_Branding == true ||
                Fuji.Common.Sessions.Add_Branch == true ||
                Fuji.Common.Sessions.Add_Company == true ||
                Fuji.Common.Sessions.Add_Device == true ||
                Fuji.Common.Sessions.Add_Dongle == true ||
                Fuji.Common.Sessions.Add_Support_User == true ||
                Fuji.Common.Sessions.Add_Temp_RH_Device == true ||
                Fuji.Common.Sessions.Add_User == true ||
                Fuji.Common.Sessions.Delete_Branch == true ||
                Fuji.Common.Sessions.Delete_Company == true ||
                Fuji.Common.Sessions.Delete_Device == true ||
                Fuji.Common.Sessions.Delete_Dongle == true ||
                Fuji.Common.Sessions.Delete_Print_Target == true ||
                Fuji.Common.Sessions.Delete_Temp_RH_Device == true ||
                Fuji.Common.Sessions.Delete_User == true ||
                Fuji.Common.Sessions.Remove_Support_User == true ||
                Fuji.Common.Sessions.Update_Branch == true ||
                Fuji.Common.Sessions.Update_Device == true ||
                Fuji.Common.Sessions.Update_Dongle == true ||
                Fuji.Common.Sessions.Update_Temp_RH_Device == true ||
                Fuji.Common.Sessions.Update_User == true ||
                Fuji.Common.Sessions.Access_Video == true ||
                Fuji.Common.Sessions.Access_ProcessorView == true
                )
            { TD_Admin.Visible = true; TD_Admin_Spacer.Visible = true; }
            else
            { TD_Admin.Visible = false; TD_Admin_Spacer.Visible = false; }

            //if (Fuji.Common.Sessions.Access_VideoView)
            //{ TD_Videos.Visible = true; TD_Videos_Spacer.Visible = true; }
            //else
            //{ TD_Videos.Visible = false; TD_Videos_Spacer.Visible = false; }

            //// === Processor Tab Hide/Show Based on Permission === //
            //if (Fuji.Common.Sessions.Access_ProcessorView)
            //{ TD_Processor.Visible = true; TD_Processor_Spacer.Visible = true; }
            //else
            //{ TD_Processor.Visible = false; TD_Processor_Spacer.Visible = false; }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #endregion
}
