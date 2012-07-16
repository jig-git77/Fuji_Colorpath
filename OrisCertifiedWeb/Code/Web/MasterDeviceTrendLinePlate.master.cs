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

public partial class MasterDeviceTrendLinePlate : MasterBase
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

                #region Menu Settings
                string QueryString = "";
                string CSSTrend = "";
                string CSSChart = "";
                string CSSDetails = "";

                //========== Assign Css ==========//            
                if (Fuji.Common.Sessions.Css != "") { LtrlCSS.Text = @"<link href='CSS/" + Fuji.Common.Sessions.Css + "' rel='stylesheet' type='text/css' />"; }

                //========== The querystring ==========//
                QueryString = Request.QueryString.ToString();

                //========== Activate the tab ==========//
                CSSTrend = ""; CSSChart = CSSDetails = "";
                if (Request.Url.AbsoluteUri.ToLower().IndexOf("platetrend.aspx") > -1) { CSSTrend = " class = \"current\""; }
                if (Request.Url.AbsoluteUri.ToLower().IndexOf("platecharts.aspx") > -1) { CSSChart = " class = \"current\""; }
                if (Request.Url.AbsoluteUri.ToLower().IndexOf("platedetails.aspx") > -1) { CSSDetails = " class = \"current\""; }

                LTRL_Menu.Text = "<ul><li" + CSSTrend + "><a title=\"Trend\" href=\"PlateTrend.aspx?" + QueryString + "\"><span>Trend</span></a></li>";
                LTRL_Menu.Text += "<li" + CSSChart + "><a title=\"Chart\" href=\"PlateCharts.aspx?" + QueryString + "\"><span>Chart</span></a></li>";
                LTRL_Menu.Text += "<li" + CSSDetails + "><a title=\"Details\" href=\"PlateDetails.aspx?" + QueryString + "\"><span>Details</span></a></li></ul>";
                #endregion

                              
                //========== Check User Logged In ==========//
                if (Request.QueryString["ModalDialog"] == null)
                { Fuji.Common.Sessions.CheckUserLoginPopUp(); }
                else
                { Fuji.Common.Sessions.CheckUserLoginModelDialog(); }

                //========== Get The Variables ==========//
                if (Request.QueryString["CPMeasurementGUID"] != null) { LTRL_CPMeasurementGUID.Text = Request.QueryString["CPMeasurementGUID"].ToString(); }
                if (Request.QueryString["CPDevicePrintTargetID"] != null) { LTRL_CPDevicePrintTargetID.Text = Request.QueryString["CPDevicePrintTargetID"].ToString(); }

                //========== Fill From Request Querystring ==========//
                FillForm();

                //========== Fill From Database ==========//
                FillAveMaxMin();

                //========== Checks the measurement Exists ==========//
                CheckMeasurementExists();
               
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

    #region Methods and Functions
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
                LTRL_Time.Text = Dt.Rows[0]["Time"].ToString();
                LTRL_Device.Text = Dt.Rows[0]["CPDeviceDesc"].ToString();
                LTRL_PrintTarget.Text = Dt.Rows[0]["PrintTarget"].ToString();

                if (Convert.ToInt32(Dt.Rows[0]["Status"]) == 0) { LTRL_Status.Text = "<img src=\"Images/ProofFailed.gif\" alt=\"Measurement Out of Tolerance\"><br><b>Measurement Out <br> of Tolerance</b>"; }
                else if (Convert.ToInt32(Dt.Rows[0]["Status"]) == 1) { LTRL_Status.Text = "<img src=\"Images/ProofWarning.gif\" alt=\"Measurement Warning\"><br><b>Measurement<br>Warning</b>"; }
                else { LTRL_Status.Text = "<img src=\"Images/Proofok.gif\" alt=\"Measurement Passed\"><br><b>Measurement<br>Passed</b>"; }
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillAveMaxMin()
    {
        try
        {
            //=========== Gets the Aver Density, Maximum Density, Standerd Deviation Density. ==========//
            CPPatchDataPlateData objCPPatchDataPlate = new CPPatchDataPlateData();
            objCPPatchDataPlate.MeasurementGUID = LTRL_CPMeasurementGUID.Text;
            DataTable DT = objCPPatchDataPlate.uspGetAveMaxMinDeviation();

            if (DT.Rows.Count > 0)
            {
                LTRL_AveDe.Text = DT.Rows[0]["AveD"].ToString();
                LTRL_MaxPos.Text = DT.Rows[0]["MaxD"].ToString();
                LTRL_MaxNeg.Text = DT.Rows[0]["MinD"].ToString();
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
