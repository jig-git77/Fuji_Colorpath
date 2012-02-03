#region System
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
#endregion

public partial class MasterProcessor : System.Web.UI.MasterPage
{
    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Clear Message ==========//
            LblError.Text = "";

            if (IsPostBack == false)
            {
                //========== Assign Css ==========//            
                if (Fuji.Common.Sessions.Css != "") { LtrlCSS.Text = @"<link href='CSS/" + Fuji.Common.Sessions.Css + "' rel='stylesheet' type='text/css' />"; }

                #region Menu Settings
                string QueryString = "";
                string CSSDashboard = "";
                string CSSChart = "";
                string CSSTrend = "";
                string CSSColorChars = "";
                string CSSZones = "";
                string CSSJobRunAdmin = "";

                //========== The querystring ==========//
                QueryString = Request.QueryString.ToString();

                //========== Activate the tab ==========//
                if (Request.Url.AbsoluteUri.ToLower().IndexOf("pressdashboard.aspx") > -1) { CSSDashboard = " class = \"current\""; }
                if (Request.Url.AbsoluteUri.ToLower().IndexOf("presscharts.aspx") > -1) { CSSChart = " class = \"current\""; }
                if (Request.Url.AbsoluteUri.ToLower().IndexOf("presscolorcharacteristics.aspx") > -1) { CSSColorChars = " class = \"current\""; }
                if (Request.Url.AbsoluteUri.ToLower().IndexOf("processortrend.aspx") > -1) { CSSTrend = " class = \"current\""; }
                if (Request.Url.AbsoluteUri.ToLower().IndexOf("jobrunadmin.aspx") > -1) { CSSJobRunAdmin = " class = \"current\""; }

                LTRL_Menu.Text = "<ul>";
                //LTRL_Menu.Text += "<li" + CSSDashboard + "><a title=\"Dashboard\" href=\"PressDashboard.aspx?" + QueryString + "\"><span>Dashboard</span></a></li>";
                //LTRL_Menu.Text += "<li" + CSSChart + "><a title=\"Chart\" href=\"PressCharts.aspx?" + QueryString + "\"><span>Chart</span></a></li>";
                //LTRL_Menu.Text += "<li" + CSSColorChars + "><a title=\"Color Characteristics\" href=\"PressColorCharacteristics.aspx?" + QueryString + "\"><span>Color Characteristics</span></a></li>";
                LTRL_Menu.Text += "<li" + CSSTrend + "><a title=\"Trend\" href=\"ProcessorTrend.aspx?" + QueryString + "\"><span>Trend</span></a></li>";
                //LTRL_Menu.Text += "<li" + CSSZones + "><a title=\"Zones\" href=\"#\"><span>Zones</span></a></li>";
                //LTRL_Menu.Text += "<li" + CSSJobRunAdmin + "><a title=\"Job Run Admin\" href=\"JobRunAdmin.aspx?" + QueryString + "\"><span>Job Run Admin</span></a></li>";
                LTRL_Menu.Text += "</ul>";
                #endregion

                //========== Check User Logged In ==========//
                if (Request.QueryString["ModalDialog"] == null)
                { Fuji.Common.Sessions.CheckUserLoginPopUp(); }
                else
                { Fuji.Common.Sessions.CheckUserLoginModelDialog(); }

                //========== Fill Form ==========//
                LTRL_ZProcID.Text = Convert.ToString(Request.QueryString["ZProcID"]);
                LTRL_DongleID.Text = Convert.ToString(Request.QueryString["DongleID"]);

                ltrlLocation.Text = Convert.ToString(Request.QueryString["Location"]) + " - " + LTRL_DongleID.Text;
                ltrlDevice.Text = Convert.ToString(Request.QueryString["Device"]);
                //========== Fill Form ==========//
                
                //FillTempRH();
            }
            // =========Insert User Accessed Page Information To Database ========= //
            if (!this.Page.IsPostBack)
            {
                Fuji.Business.TrackUserAccessedPagesData obj = new Fuji.Business.TrackUserAccessedPagesData();
                obj.TrackUserAccessPage(Fuji.Common.Sessions.UserID);
            }
            // ===========================  END ================================
        }
        catch (Exception Exc)
        {
            LblError.Text = Exc.Message;
        }
    }
    #endregion

    #region Mehtods and Functions
    
    //private void FillTempRH()
    //{
    //    try
    //    {
    //        //========== Gets the information on DeviceID ==========//
    //        DataTable DT = (new TempRHDataData()).uspGetTop1TempRHData(Convert.ToInt32(LTRL_CPDeviceID.Text));

    //        //========== Fill Label Values ==========//
    //        if (DT.Rows.Count > 0)
    //        {
    //            LTRL_TempAct.Text = DT.Rows[0]["TempF"].ToString();
    //            LTRL_TempTrg.Text = DT.Rows[0]["TargetTemp"].ToString();

    //            LTRL_RHAct.Text = DT.Rows[0]["RH"].ToString();
    //            LTRL_RHTrg.Text = DT.Rows[0]["TargetRH"].ToString();
    //        }
    //    }
    //    catch (Exception Ex)
    //    {
    //        throw Ex;
    //    }
    //}
    #endregion
}
