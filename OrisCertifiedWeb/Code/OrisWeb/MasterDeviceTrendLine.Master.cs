using Oris.Business;
using Oris.Common;
using OrisWeb.Models;
using System;
using System.Data;


namespace OrisWeb
{
    public partial class MasterDeviceTrendLine : MasterBase
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
                    //========== Declarations ==========//
                    string QueryString = "";
                    string CSSTrend = "";
                    string CSSChart = "";
                    string CSSDetails = "";
                    string CSSColorChars = "";

                    //========== The querystring ==========//
                    QueryString = Request.QueryString.ToString();

                    //========== Activate the tab ==========//
                    CSSTrend = ""; CSSChart = CSSDetails = CSSColorChars = "";
                    if (Request.Url.AbsoluteUri.ToLower().IndexOf("colortrend.aspx") > -1) { CSSTrend = " class = \"current\""; }
                    if (Request.Url.AbsoluteUri.ToLower().IndexOf("colorcharts.aspx") > -1) { CSSChart = " class = \"current\""; }
                    if (Request.Url.AbsoluteUri.ToLower().IndexOf("colordetails.aspx") > -1) { CSSDetails = " class = \"current\""; }
                    if (Request.Url.AbsoluteUri.ToLower().IndexOf("colorcolorcharacteristics.aspx") > -1) { CSSColorChars = " class = \"current\""; }

                    LTRL_Menu.Text = "<ul><li" + CSSTrend + "><a title=\"Trend\" href=\"ColorTrend.aspx?" + QueryString + "\"><span>Trend</span></a></li>";
                    LTRL_Menu.Text += "<li" + CSSChart + "><a title=\"Chart\" href=\"ColorCharts.aspx?" + QueryString + "\"><span>Chart</span></a></li>";
                    LTRL_Menu.Text += "<li" + CSSDetails + "><a title=\"Details\" href=\"ColorDetails.aspx?" + QueryString + "\"><span>Details</span></a></li>";
                    LTRL_Menu.Text += "<li" + CSSColorChars + "><a title=\"Color Characteristics\" href=\"ColorColorCharacteristics.aspx?" + QueryString + "\"><span>Color Characteristics</span></a></li></ul>";
                    #endregion

                    //========== Check User Logged In ==========//
                    if (Request.QueryString["ModalDialog"] == null)
                    { Sessions.CheckUserLoginPopUp(); }
                    else
                    { Sessions.CheckUserLoginModelDialog(); }

                    //========== Get The Variables ==========//
                    if (Request.QueryString["CPMeasurementGUID"] != null) { LTRL_CPMeasurementGUID.Text = Request.QueryString["CPMeasurementGUID"].ToString(); }
                    if (Request.QueryString["CPDevicePrintTargetID"] != null) { LTRL_CPDevicePrintTargetID.Text = Request.QueryString["CPDevicePrintTargetID"].ToString(); }

                    //========== Fill From Request Querystring ==========//
                    FillForm();

                    //========== Fill From Database ==========//
                    FillAveMaxStdDev();

                    //========== Checks the measurement Exists ==========//
                    CheckMeasurementExists();
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
        private void FillForm()
        {
            try
            {
                //========== Get all Nessessary Information ==========//
                DataTable Dt = (new CPMeasurementData()).uspGetJobReportDataOnGUID(LTRL_CPMeasurementGUID.Text);

                if (Dt.Rows.Count > 0)
                {
                    LTRL_Location.Text = Dt.Rows[0]["CompanyName"].ToString() + " - " + Dt.Rows[0]["BranchName"].ToString() + " - " + Dt.Rows[0]["DongleDescription"].ToString() + " - " + Dt.Rows[0]["CPDeviceDesc"].ToString() + " - " + Dt.Rows[0]["PrintTarget"].ToString();
                    LTRL_Job.Text = Dt.Rows[0]["Job"].ToString();
                    LTRL_Date.Text = Dt.Rows[0]["Date"].ToString();
                    LTRL_Customer.Text = Dt.Rows[0]["Customer"].ToString();
                    string Date = LTRL_Date.Text + " " + Dt.Rows[0]["Time"].ToString();
                    LTRL_Time.Text = Convert.ToDateTime(Date).ToLongTimeString();
                    LTRL_Device.Text = Dt.Rows[0]["CPDeviceDesc"].ToString();
                    LTRL_PrintTarget.Text = Dt.Rows[0]["PrintTarget"].ToString();


                    if (Convert.ToInt32(Dt.Rows[0]["Status"]) == 0) { LTRL_Status.Text = "<img src=\"Images/ProofFailed.gif\" alt=\"Measurement Out of Tolerance\"><br><b>Measurement Out <br> of Tolerance</b>"; }
                    else if (Convert.ToInt32(Dt.Rows[0]["Status"]) == 1) { LTRL_Status.Text = "<img src=\"Images/ProofWarning.gif\" alt=\"Measurement Warning\"><br><b>Measurement<br>Warning</b>"; }
                    else { LTRL_Status.Text = "<img src=\"Images/Proofok.gif\" alt=\"Measurement Passed\"><br><b>Measurement<br>Passed</b><div>"; }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void FillAveMaxStdDev()
        {
            try
            {
                //=========== Gets the Aver Density, Maximum Density, Standerd Deviation Density. ==========//
                CPPatchDataLabData objCPPatchDataLab = new CPPatchDataLabData();
                objCPPatchDataLab.MeasurementGUID = LTRL_CPMeasurementGUID.Text;
                DataTable DT = objCPPatchDataLab.uspGetAveMaxStdDevOnGUID();

                if (DT.Rows.Count > 0)
                {
                    LTRL_MaxDe.Text = DT.Rows[0]["MAX_DE"].ToString();
                    LTRL_AveDe.Text = DT.Rows[0]["AVE_DE"].ToString();
                    LTRL_StdDev.Text = DT.Rows[0]["STDEV_DE"].ToString();
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void CheckMeasurementExists()
        {
            try
            {
                //========== String Window Script ==========//
                string Script = "<script language=\"javascript\">window.location.href = \"MeasurementNotFound.aspx\";</script>";

                //========== Check the IsUsed ==========//
                CPMeasurementData objCPMeasurement = new CPMeasurementData();
                objCPMeasurement.CPMeasurementGUID = LTRL_CPMeasurementGUID.Text;
                DataTable DT = objCPMeasurement.uspGetCheckCPVStatus();

                if (DT.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(DT.Rows[0]["IsUsed"]) == true)
                    { Script = ""; }
                }

                //========== Write the script to close the window. ==========//
                Response.Write(Script);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

    }

}