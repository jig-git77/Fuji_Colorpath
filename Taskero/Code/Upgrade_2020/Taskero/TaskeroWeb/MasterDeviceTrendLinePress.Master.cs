#region System
using System;
using System.Data;
using Taskero.Business;
using Taskero.Common;
using TaskeroWeb.Models;
#endregion

namespace TaskeroWeb
{
    public partial class MasterDeviceTrendLinePress : MasterBase
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
                    if (Sessions.Css != "") { LtrlCSS.Text = @"<link href='CSS/" + Sessions.Css + "' rel='stylesheet' type='text/css' />"; }

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
                    if (Request.Url.AbsoluteUri.ToLower().IndexOf("presstrend.aspx") > -1) { CSSTrend = " class = \"current\""; }
                    if (Request.Url.AbsoluteUri.ToLower().IndexOf("jobrunadmin.aspx") > -1) { CSSJobRunAdmin = " class = \"current\""; }

                    LTRL_Menu.Text = "<ul><li" + CSSDashboard + "><a title=\"Dashboard\" href=\"PressDashboard.aspx?" + QueryString + "\"><span>Dashboard</span></a></li>";
                    LTRL_Menu.Text += "<li" + CSSChart + "><a title=\"Chart\" href=\"PressCharts.aspx?" + QueryString + "\"><span>Chart</span></a></li>";
                    LTRL_Menu.Text += "<li" + CSSColorChars + "><a title=\"Color Characteristics\" href=\"PressColorCharacteristics.aspx?" + QueryString + "\"><span>Color Characteristics</span></a></li>";
                    LTRL_Menu.Text += "<li" + CSSTrend + "><a title=\"Trend\" href=\"PressTrend.aspx?" + QueryString + "\"><span>Trend</span></a></li>";
                    //LTRL_Menu.Text += "<li" + CSSZones + "><a title=\"Zones\" href=\"#\"><span>Zones</span></a></li>";
                    LTRL_Menu.Text += "<li" + CSSJobRunAdmin + "><a title=\"Job Run Admin\" href=\"JobRunAdmin.aspx?" + QueryString + "\"><span>Job Run Admin</span></a></li></ul>";
                    #endregion

                    //========== Check User Logged In ==========//
                    if (Request.QueryString["ModalDialog"] == null)
                    { Sessions.CheckUserLoginPopUp(); }
                    else
                    { Sessions.CheckUserLoginModelDialog(); }

                    //========== Fill Form ==========//
                    LTRL_JobID.Text = Request.QueryString["JobID"].ToString();
                    LTRL_CPDeviceID.Text = Request.QueryString["CPDeviceID"].ToString();
                    LTRL_DongleID.Text = Request.QueryString["DongleID"].ToString();

                    //========== Fill Form ==========//
                    FillJobInformation();
                    FillTempRH();
                }
                // =========Insert User Accessed Page Information To Database ========= //
                if (!this.Page.IsPostBack)
                {
                    TrackUserAccessedPagesData obj = new TrackUserAccessedPagesData();
                    obj.TrackUserAccessPage(Sessions.UserID);
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
        private void FillJobInformation()
        {
            try
            {
                //========== Declarations ==========//
                PressJobsData objPressJobs = new PressJobsData();

                //========== Get DT ==========//
                objPressJobs.JobID = Convert.ToInt32(LTRL_JobID.Text);
                objPressJobs.DongleID = LTRL_DongleID.Text;
                DataTable DT = objPressJobs.uspGetPressJobInformation();
                if (DT.Rows.Count > 0)
                {
                    LTRL_JobRunName.Text = DT.Rows[0]["JobName"].ToString();
                    LTRL_StartDate.Text = DT.Rows[0]["JobCreatedDt"].ToString();
                    LTRL_Company.Text = DT.Rows[0]["CompanyName"].ToString();
                    LTRL_Branch.Text = DT.Rows[0]["BranchName"].ToString();
                    LTRL_Workstation.Text = DT.Rows[0]["Workstation"].ToString();
                    LTRL_OutputDevice.Text = DT.Rows[0]["CPDeviceDesc"].ToString();
                    LTRL_DeltaEType.Text = DT.Rows[0]["DeltaETypeValue"].ToString();
                    LTRL_Substrate.Text = DT.Rows[0]["Substrate"].ToString();
                    LTRL_SpectroSettings.Text = DT.Rows[0]["IllumObsType"].ToString();

                    LTRL_Customer.Text = DT.Rows[0]["Customer"].ToString();
                    LTRL_SalesPerson.Text = DT.Rows[0]["SalesPerson"].ToString();
                    LTRL_Standard.Text = DT.Rows[0]["Standard"].ToString();
                    LTRL_Inks.Text = DT.Rows[0]["Inks"].ToString();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void FillTempRH()
        {
            try
            {
                //========== Gets the information on DeviceID ==========//
                DataTable DT = (new TempRHDataData()).uspGetTop1TempRHData(Convert.ToInt32(LTRL_CPDeviceID.Text));

                //========== Fill Label Values ==========//
                if (DT.Rows.Count > 0)
                {
                    LTRL_TempAct.Text = DT.Rows[0]["TempF"].ToString();
                    LTRL_TempTrg.Text = DT.Rows[0]["TargetTemp"].ToString();

                    LTRL_RHAct.Text = DT.Rows[0]["RH"].ToString();
                    LTRL_RHTrg.Text = DT.Rows[0]["TargetRH"].ToString();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion
    }
}