#region System
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
#endregion

#region Others
using System.Data.SqlClient;
using Fuji.Common;
using Fuji.Business;
using Fuji.DataAccess;
using Dundas.Charting.WebControl;
#endregion

public partial class ReportShiftVariability : PageBase
{
    #region Page Load & Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Clear Message ==========//
            LblError.Text = "";

            //========== Check User Login ==========//
            Sessions.CheckUserLogin();

            //========== Default Settings ==========//
            if (IsPostBack == false)
            {
                //========== Assign Style ==========//
                SetStyle();

                //========== Hide the Section ==========//
                PNL_Search.Style.Add("display", "");
                PNL_Result.Style.Add("display", "none");

                //========== Fill Search View ==========//
                FillCompany(null, null);


                //========== Fill Drop Down ==========//
                SetStyle();
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void BTN_CreateReport_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Validations ==========//
            if (DDL_Device.SelectedIndex == 0)
            { LblError.Text = "Please select a device!!!"; return; }
            if (TXT_FromDate.Text == "")
            { LblError.Text = "From Date is required!!!"; return; }
            if (TXT_ToDate.Text == "")
            { LblError.Text = "To Date is required!!!"; return; }

            //========== Shift ==========//                        
            LBL_Shift1Start.Text = DDL_Shift1FromHours.SelectedItem.Text + ":" + DDL_Shift1FromMins.SelectedValue + " " + RBTN_Shift1FromAMPM.SelectedItem.Text;
            LBL_Shift1End.Text = DDL_Shift1ToHours.SelectedItem.Text + ":" + DDL_Shift1ToMins.SelectedValue + " " + RBTN_Shift1ToAMPM.SelectedItem.Text;
            LBL_Shift2Start.Text = DDL_Shift2FromHours.SelectedItem.Text + ":" + DDL_Shift2FromMins.SelectedValue + " " + RBTN_Shift2FromAMPM.SelectedItem.Text;
            LBL_Shift2End.Text = DDL_Shift2ToHours.SelectedItem.Text + ":" + DDL_Shift2ToMins.SelectedValue + " " + RBTN_Shift2ToAMPM.SelectedItem.Text;

            //========== Filling Label ==========//
            lblReportTitle.Text = "Shift Variability Report";
            TDReportTitle.Attributes.Add("class", "");

            LBL_Field1.Text = DDL_OPT1_Field.SelectedItem.Text;
            LBL_Field2.Text = DDL_OPT2_Field.SelectedItem.Text;
            LBL_Field3.Text = DDL_OPT3_Field.SelectedItem.Text;

            LBL_Operator1.Text = DDL_OPT1_Operator.SelectedItem.Text;
            LBL_Operator2.Text = DDL_OPT2_Operator.SelectedItem.Text;
            LBL_Operator3.Text = DDL_OPT3_Operator.SelectedItem.Text;

            LBL_Criteria1.Text = TXT_OPT1_KeyWords.Text;
            LBL_Criteria2.Text = TXT_OPT2_KeyWords.Text;
            LBL_Criteria3.Text = TXT_OPT3_KeyWords.Text;

            //========== Filling Form ==========//
            LBL_DateRange.Text = TXT_FromDate.Text + " - " + TXT_ToDate.Text;
            LBL_DeviceName.Text = DDL_Device.SelectedItem.Text;
            LBL_DongleNumber.Text = DDL_DongleNo.SelectedItem.Text;
            LBL_BranchName.Text = DDL_Branch.SelectedItem.Text;
            LBL_CompanyName.Text = DDL_Company.SelectedItem.Text;

            //========== Get All GUIDs ==========//
            GetAllGuids
                (
                    LBL_CPMeasurementGUID1
                    , DDL_Shift1FromHours, DDL_Shift1FromMins, RBTN_Shift1FromAMPM
                    , DDL_Shift1ToHours, DDL_Shift1ToMins, RBTN_Shift1ToAMPM
                );
            GetAllGuids
                (
                      LBL_CPMeasurementGUID2
                    , DDL_Shift2FromHours, DDL_Shift2FromMins, RBTN_Shift2FromAMPM
                    , DDL_Shift2ToHours, DDL_Shift2ToMins, RBTN_Shift2ToAMPM
                );

            if (LBL_CPMeasurementGUID1.Text == "" && LBL_CPMeasurementGUID2.Text == "")
            { LblError.Text = "No measurements found for the search criteria selected!"; }
            else
            {
                //========== Fills if No GUID ==========//
                //if (LBL_CPMeasurementGUID1.Text == "") { LBL_CPMeasurementGUID1.Text = "'B3BFE5CD-679A-4E28-A9D4-E005FA7D3519'"; }
                //if (LBL_CPMeasurementGUID2.Text == "") { LBL_CPMeasurementGUID2.Text = "'B3BFE5CD-679A-4E28-A9D4-E005FA7D3519'"; }

                //========== Fill Data ==========//        
                FillForm();

                TempTable();

                //========== Hiding Search / displaying Result ==========//
                PNL_Search.Style.Add("display", "none");
                PNL_Result.Style.Add("display", "");
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void BTN_BackToSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //========== displaying Search / Hiding Result ==========//
            PNL_Search.Style.Add("display", "");
            PNL_Result.Style.Add("display", "none");
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
        }
    }
    protected void BTN_MakePDF_Click(object sender, EventArgs e)
    {
        try
        {
            Fuji.Common.ReportPDF objReportPDF = new Fuji.Common.ReportPDF();
            objReportPDF.ReportType = (int)Fuji.Common.Enums.enumReportType.ShiftVariabilityReport;
            objReportPDF.PDFPath = Server.MapPath("UploadFiles/Report_PDF/");
            objReportPDF.PDF_NAME = Sessions.UserID + "_ShiftVariabilityReport.PDF";

            if (ViewState["DT_BasicInfo"] != null)
            { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_BasicInfo"]); }
            if (ViewState["DT_SearchCriteria"] != null)
            { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_SearchCriteria"]); }
            if (ViewState["DT_Shift1"] != null)
            { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_Shift1"]); }
            if (ViewState["DT_Shift2"] != null)
            { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_Shift2"]); }

            objReportPDF.MakePDF();

            Fuji.Common.CommonFunctions.DownlaodFile(objReportPDF.PDFPath, objReportPDF.PDF_NAME, objReportPDF.PDF_NAME);

            //Response.Write("window.open(" + Server.MapPath(objReportPDF.PDFPath + objReportPDF.PDF_NAME) + ")");
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void BTN_Clear_Click(object sender, EventArgs e)
    {
        try
        {
            int Ind = Convert.ToInt32(((Button)sender).Attributes["Ind"]);
            if (Ind == 1)
            {
                DDL_OPT1_Field.SelectedIndex = 0;
                DDL_OPT1_Operator.SelectedIndex = 0;
                TXT_OPT1_KeyWords.Text = "";
            }
            else if (Ind == 2)
            {
                DDL_OPT2_Field.SelectedIndex = 0;
                DDL_OPT2_Operator.SelectedIndex = 0;
                TXT_OPT2_KeyWords.Text = "";
            }
            else
            {
                DDL_OPT3_Field.SelectedIndex = 0;
                DDL_OPT3_Operator.SelectedIndex = 0;
                TXT_OPT3_KeyWords.Text = "";
            }
        }
        catch (Exception Ex)
        {
            LblError.Text = HandleException(Ex);
        }
    }
    #endregion

    #region Search Criteria
    protected void FillCompany(object sender, EventArgs e)
    {
        try
        {
            //==========  Declaring Objects ==========//
            UserCompanyData objUserCompany = new UserCompanyData();
            objUserCompany.TaskeroUserID = Sessions.UserID;
            objUserCompany.CompanyID = Sessions.RoleID;

            //==========  Get Data Table and Bind ==========//spCompanySelectByUserID
            DataTable DT_Company = new DataTable();
            DT_Company = objUserCompany.uspGetCompanyByUserID();

            //========== Clearing all the values ==========//
            DDL_Company.Items.Clear();

            //========== Fill DDL ==========//
            if (DT_Company != null && DT_Company.Rows.Count > 0)
            {
                DDL_Company.DataSource = DT_Company;
                DDL_Company.DataTextField = "CompanyName";
                DDL_Company.DataValueField = "CompanyID";
                DDL_Company.DataBind();
            }
            else
            { DDL_Company.DataSource = null; DDL_Company.DataBind(); }

            //========== Insert Items DropDownList in Company ===========//
            DDL_Company.Items.Insert(0, new ListItem("--- Select Company ---", "0"));
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void FillBranch(object sender, EventArgs e)
    {
        try
        {
            //========== Declare the Object of the Branch ===========//
            UserBranchData objUserBranch = new UserBranchData();
            objUserBranch.TaskeroUserID = Fuji.Common.Sessions.UserID;
            objUserBranch.CompanyID = Convert.ToInt32(DDL_Company.SelectedValue );

            //==========  Get Data Table and Bind ==========//
            DataTable DT_Branch = new DataTable();
            DT_Branch = objUserBranch.uspGetBranchOfCompanyByUserID(Fuji.Common.Sessions.RoleID);

            //========== Clearing all the values ==========//
            DDL_Branch.Items.Clear();

            //========== Fill DDL ==========//
            if (DT_Branch != null && DT_Branch.Rows.Count > 0)
            {
                DDL_Branch.DataSource = DT_Branch;
                DDL_Branch.DataTextField = "BranchName";
                DDL_Branch.DataValueField = "BranchID";
                DDL_Branch.DataBind();
            }
            else
            { DDL_Branch.DataSource = null; DDL_Branch.DataBind(); }

            //========== Insert Items DropDownList in Branch ===========//
            DDL_Branch.Items.Insert(0, new ListItem("--- Select Branch ---", "0"));
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void FillDongle(object sender, EventArgs e)
    {
        //========== Clearing all the values ==========//
        DDL_DongleNo.Items.Clear();

        try
        {
            //========== Declare the Object of the Dongle ===========//
            UserDongleData objUserDongle = new UserDongleData();

            //==========  Get Data Table and Fill ==========//        
            DataTable DT_Dongle = new DataTable();
            objUserDongle.TaskeroUserID = Fuji.Common.Sessions.UserID;
            objUserDongle.UserDongleID = Convert.ToInt32(DDL_Branch.SelectedValue );
            DT_Dongle = objUserDongle.uspGetDongleByUserIDAndBranchID(Fuji.Common.Sessions.RoleID);

            //========== Fill DDL ==========//
            if (DT_Dongle != null && DT_Dongle.Rows.Count > 0)
            {
                DDL_DongleNo.DataSource = DT_Dongle;
                DDL_DongleNo.DataTextField = "DongleSerial";//CPSerialNum
                DDL_DongleNo.DataValueField = "CPDongleID";
                DDL_DongleNo.DataBind();
            }
            else
            { DDL_DongleNo.DataSource = null; DDL_DongleNo.DataBind(); }

            //========== Insert Items DropDownList in Dongle ===========//
            DDL_DongleNo.Items.Insert(0, new ListItem("--- Select Dongle ---", "0"));
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void FillDevice(object sender, EventArgs e)
    {
        //========== Clearing all the values ==========//
        DDL_Device.Items.Clear();

        try
        {
            //========== Declare the Object of the Dongle ===========//
            CPDeviceData objCPDevice = new CPDeviceData();

            //==========  Get Data Table and Fill ==========//        
            DataTable DT_Device = new DataTable();
            objCPDevice.CPDongleID = Convert.ToInt32(DDL_DongleNo.SelectedValue );
            DT_Device = objCPDevice.uspGetDeviceByDongleIDColors();

            //========== Fill DDL ==========//
            if (DT_Device != null && DT_Device.Rows.Count > 0)
            {
                DDL_Device.DataSource = DT_Device;
                DDL_Device.DataTextField = "CPDeviceDesc";
                DDL_Device.DataValueField = "CPDeviceID";
                DDL_Device.DataBind();
            }
            else
            { DDL_Device.DataSource = null; DDL_Device.DataBind(); }

            //========== Insert Items DropDownList in Dongle ===========//
            DDL_Device.Items.Insert(0, new ListItem("--- Select Device ---", "0"));
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }

    }
    protected void FillDates(object sender, EventArgs e)
    {
        try
        {
            //========== If No device selected clear ==========//
            if (DDL_Device.SelectedIndex == 0)
            { TXT_ToDate.Text = ""; TXT_FromDate.Text = ""; }

            //========== Declarations ==========//
            SqlCommand Cmd = new SqlCommand();

            //========== Execute Query ==========//
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = "Select CONVERT( VARCHAR, MIN(CreatedDT), 101 ) AS MINDATE ,  CONVERT( VARCHAR, MAX(CreatedDT), 101 ) AS MAXDATE from CPMeasurement WHERE CPDeviceID = " + DDL_Device.SelectedValue;
            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);

            //========== Check and Fill ==========//                        
            if (Convert.IsDBNull(DT.Rows[0]["MINDATE"]) == false)
            { TXT_FromDate.Text = DT.Rows[0]["MINDATE"].ToString(); }
            if (Convert.IsDBNull(DT.Rows[0]["MAXDATE"]) == false)
            { TXT_ToDate.Text = DT.Rows[0]["MAXDATE"].ToString(); }
            if (Convert.IsDBNull(DT.Rows[0]["MINDATE"]) == false && Convert.IsDBNull(DT.Rows[0]["MAXDATE"]) == false)
            { TXT_FromDate.Text = Convert.ToDateTime(DT.Rows[0]["MAXDATE"]).AddMonths(-3).ToShortDateString(); }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    #endregion

    #region Validations
    protected void TXT_DateText(object sender, EventArgs e)
    {
        if (Fuji.Common.Validations.IsDateTime(((TextBox)sender).Text) == false)
        { LblError.Text = "Please enter a valid date!"; ((TextBox)sender).Text = ""; return; }
    }
    #endregion

    #region Others
    private void SetStyle()
    {
        try
        {
            DDL_Company.Style.Add("width", "100%");
            DDL_Branch.Style.Add("width", "100%");
            DDL_DongleNo.Style.Add("width", "100%");
            DDL_Device.Style.Add("width", "100%");

            DDL_OPT1_Field.Style.Add("width", "100%");
            DDL_OPT1_Operator.Style.Add("width", "100%");
            TXT_OPT1_KeyWords.Style.Add("width", "80%");

            DDL_OPT2_Field.Style.Add("width", "100%");
            DDL_OPT2_Operator.Style.Add("width", "100%");
            TXT_OPT2_KeyWords.Style.Add("width", "80%");

            DDL_OPT3_Field.Style.Add("width", "100%");
            DDL_OPT3_Operator.Style.Add("width", "100%");
            TXT_OPT3_KeyWords.Style.Add("width", "80%");

            DDL_Shift1FromHours.Style.Add("width", "40px");
            DDL_Shift1FromMins.Style.Add("width", "40px");
            DDL_Shift2FromHours.Style.Add("width", "40px");
            DDL_Shift2FromMins.Style.Add("width", "40px");

            DDL_Shift1ToHours.Style.Add("width", "40px");
            DDL_Shift1ToMins.Style.Add("width", "40px");
            DDL_Shift2ToHours.Style.Add("width", "40px");
            DDL_Shift2ToMins.Style.Add("width", "40px");
        }
        catch (Exception Ex) { throw Ex; }
    }
    #endregion

    #region Fill Form
    private void GetAllGuids(   System.Web.UI.WebControls.Label LBL_CPMeasurementGUID
                                , DropDownList DDL_ShiftFromHours, DropDownList DDL_ShiftFromMins, RadioButtonList RBTN_ShiftFromAMPM
                                , DropDownList DDL_ShiftToHours, DropDownList DDL_ShiftToMins, RadioButtonList RBTN_ShiftToAMPM
                            )
    {
        try
        {
            //========== Create Join Clause ==========//
            string JoinClause = "";
            if (DDL_OPT1_Field.SelectedIndex > 0 && DDL_OPT1_Operator.SelectedIndex > 0)
            { JoinClause += DDL_OPT1_Field.SelectedValue + " " + DDL_OPT1_Operator.SelectedValue.Replace("<<>>", TXT_OPT1_KeyWords.Text.Replace("'", "''")) + " "; }
            if (DDL_OPT2_Field.SelectedIndex > 0 && DDL_OPT2_Operator.SelectedIndex > 0)
            { JoinClause += DDL_OPT2_Field.SelectedValue + " " + DDL_OPT2_Operator.SelectedValue.Replace("<<>>", TXT_OPT2_KeyWords.Text.Replace("'", "''")) + " "; }
            if (DDL_OPT3_Field.SelectedIndex > 0 && DDL_OPT3_Operator.SelectedIndex > 0)
            { JoinClause += DDL_OPT3_Field.SelectedValue + " " + DDL_OPT3_Operator.SelectedValue.Replace("<<>>", TXT_OPT3_KeyWords.Text.Replace("'", "''")) + " "; }

            //========== Query ==========//
            SqlCommand Cmd = new SqlCommand();

            if (Fuji.Common.Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin)
            {
                Cmd.CommandText = "SELECT ";
                Cmd.CommandText += "DISTINCT(PMD.CPMeasurementGUID) as CPMeasurementGUID ";
                Cmd.CommandText += "FROM  CPMeasurement PMD ";
                Cmd.CommandText += JoinClause;
                Cmd.CommandText += "WHERE PMD.CPMeasurementTypeID = 1 ";
                Cmd.CommandText += "AND PMD.CPDeviceID = " + DDL_Device.SelectedValue + " ";
                Cmd.CommandText += "AND ISNULL( PMD.IsUsed , 1 ) = 1 ";
                Cmd.CommandText += "AND ( PMD.CreatedDT BETWEEN  '" + TXT_FromDate.Text + "' and '" + Convert.ToDateTime(TXT_ToDate.Text).AddDays(1).ToShortDateString() + "') ";
                Cmd.CommandText += "AND ( DatePart( HH , PMD.CreatedDT ) BETWEEN ( " + DDL_ShiftFromHours.SelectedValue + " + " + RBTN_ShiftFromAMPM.SelectedValue + " ) AND ( " + DDL_ShiftToHours.SelectedValue + " + " + RBTN_ShiftToAMPM.SelectedValue + " + 1) ) ";
                Cmd.CommandText += "AND ( DatePart( MI , PMD.CreatedDT ) BETWEEN " + DDL_ShiftFromMins.SelectedValue + "  AND (" + DDL_ShiftToMins.SelectedValue + " + 1) ) ";
            }
            else
            {
                Cmd.CommandText = "SELECT ";
                Cmd.CommandText += "DISTINCT(PMD.CPMeasurementGUID) as CPMeasurementGUID ";
                Cmd.CommandText += "FROM  CPMeasurement PMD ";
                Cmd.CommandText += "INNER JOIN CPV.vwGetAllDevicesPerUsers vw1 ON vw1.CPDeviceID = PMD.CPDeviceID ";
                Cmd.CommandText += JoinClause;
                Cmd.CommandText += "WHERE vw1.TaskeroUserID = " + Fuji.Common.Sessions.UserID + " AND vw1.CPDeviceID = " + DDL_Device.SelectedValue + " ";
                Cmd.CommandText += "AND ISNULL( PMD.IsUsed , 1 ) = 1 ";
                Cmd.CommandText += "AND ( PMD.CreatedDT BETWEEN  '" + TXT_FromDate.Text + "' and '" + Convert.ToDateTime(TXT_ToDate.Text).AddDays(1).ToShortDateString() + "') ";
                //========== Query that will check the Customer field if the user is Print Buyer ==========//
                Cmd.CommandText += "AND 1 = ( CASE vw1.RoleID WHEN 5 THEN CASE WHEN vw1.Customer = PMD.Customer THEN 1 ELSE 0 END ELSE  1 END ) ";
            }
            Cmd.CommandType = CommandType.Text;

            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);

            //========== FILL THE GUIDs ==========//
            LBL_CPMeasurementGUID.Text = "";
            for (int i = 0; i < DT.Rows.Count; i = i + 1)
            {
                if (i == 0)
                { LBL_CPMeasurementGUID.Text = "'" + DT.Rows[i]["CPMeasurementGUID"].ToString() + "'"; }
                else
                { LBL_CPMeasurementGUID.Text += ",'" + DT.Rows[i]["CPMeasurementGUID"].ToString() + "'"; }
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillForm()
    {
        try
        {
            //========== Displaying errors and Measurements range ==========//
            NoOfReads
                (
                    LBL_CPMeasurementGUID1
                    , LBL_Shift1ErrorRatio, LBL_Shift1NoOfReads, LBL_Shift1TotalErrors
                    , LBL_Shift1Start, LBL_Shift1End
                );
            NoOfReads
                (
                    LBL_CPMeasurementGUID2
                    , LBL_Shift2ErrorRatio, LBL_Shift2NoOfReads, LBL_Shift2TotalErrors
                    , LBL_Shift2Start, LBL_Shift2End
                );

            //========== FILL Maximum Delta E Ave ==========//
            FillMaxDeltaE(LBL_CPMeasurementGUID1, CHRT_MaxDeltaE1);
            FillMaxDeltaE(LBL_CPMeasurementGUID2, CHRT_MaxDeltaE2);

            //========== FILL Density ==========//
            FillDensity(LBL_CPMeasurementGUID1, CHRT_Density1);
            FillDensity(LBL_CPMeasurementGUID2, CHRT_Density2);

            //========== Dot Gain ==========//
            FillDotgain(LBL_CPMeasurementGUID1, CHRT_DotGain1);
            FillDotgain(LBL_CPMeasurementGUID2, CHRT_DotGain2);

            //========== Paper White ==========//
            FillPaperWhite(LBL_CPMeasurementGUID1, CHRT_PaperWhite1);
            FillPaperWhite(LBL_CPMeasurementGUID2, CHRT_PaperWhite2);

            //========== Temp Density ==========//
            FillTempHumidity
                (
                    DDL_Shift1FromHours, DDL_Shift1FromMins, RBTN_Shift1FromAMPM
                    , DDL_Shift1ToHours, DDL_Shift1ToMins, RBTN_Shift1ToAMPM
                    , CHRT_TempHumidity1
                );
            FillTempHumidity
                (
                    DDL_Shift2FromHours, DDL_Shift2FromMins, RBTN_Shift2FromAMPM
                    , DDL_Shift2ToHours, DDL_Shift2ToMins, RBTN_Shift2ToAMPM
                    , CHRT_TempHumidity2
                );
        }
        catch (Exception Ex)
        {
            throw Ex;
        }

    }
    private void NoOfReads
        (
            System.Web.UI.WebControls.Label LBL_CPMeasurementGUID
            , System.Web.UI.WebControls.Label LBL_ShiftErrorRatio, System.Web.UI.WebControls.Label LBL_ShiftNoOfReads, System.Web.UI.WebControls.Label LBL_ShiftTotalErrors
            , System.Web.UI.WebControls.Label LBL_ShiftStart, System.Web.UI.WebControls.Label LBL_ShiftEnd
        )
    {
        try
        {
            //========== Declare DataTable ==========//
            DataTable DT = new DataTable();

            if (LBL_CPMeasurementGUID.Text != "")
            {
                //========== Sql Command ==========//
                SqlCommand Cmd = new SqlCommand();

                Cmd.CommandText = "SELECT ";
                Cmd.CommandText += " COUNT(PMD.CPMeasurementGUID) as TotalReads ";
                Cmd.CommandText += ", SUM(CASE PMD.STATUS WHEN 0 THEN 1 ELSE 0 END) AS TotalErrors ";
                Cmd.CommandText += ", ( CONVERT( DECIMAL(18,2) , SUM(CASE PMD.STATUS WHEN 0 THEN 1 ELSE 0 END) ) / COUNT(PMD.CPMeasurementGUID) ) * CONVERT( DECIMAL(18,2) , 100 ) AS ErrorRate ";
                Cmd.CommandText += "FROM CPMeasurement PMD  ";
                Cmd.CommandText += "WHERE PMD.CPMeasurementGUID IN (" + LBL_CPMeasurementGUID.Text + ")";
                Cmd.CommandType = CommandType.Text;

                DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);
            }

            if (DT.Rows.Count > 0)
            {
                LBL_ShiftErrorRatio.Text = Convert.ToDecimal(DT.Rows[0]["ErrorRate"]).ToString("N2") + "%";
                LBL_ShiftNoOfReads.Text = DT.Rows[0]["TotalReads"].ToString();
                LBL_ShiftTotalErrors.Text = DT.Rows[0]["TotalErrors"].ToString();

                //LBL_ShiftStart.Text += "     ," + DT.Rows[0]["Min_ID"].ToString();
                //LBL_ShiftEnd.Text += "     ," + DT.Rows[0]["Max_ID"].ToString();
            }
            else
            {
                LBL_ShiftErrorRatio.Text = "00.00%";
                LBL_ShiftNoOfReads.Text = "0";
                LBL_ShiftTotalErrors.Text = "0";
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    private void FillMaxDeltaE(System.Web.UI.WebControls.Label LBL_CPMeasurementGUID, Chart _Chart)
    {
        try
        {
            //========== Declare DataTable ==========//
            DataTable DT = new DataTable();

            if (LBL_CPMeasurementGUID.Text != "")
            {
                //========== Get Data Table ==========//
                SqlCommand Cmd = new SqlCommand();

                Cmd.CommandText = "SELECT ";
                Cmd.CommandText += "(PMD.CreatedDt) as CreatedDt ";
                Cmd.CommandText += ", ISNULL( ACT.Average , 0 ) as AVG_DE ";
                Cmd.CommandText += ", ISNULL( ACT.PatchdEValue , 0 ) as MAX_DE ";
                Cmd.CommandText += "FROM CPMeasurement_ACT ACT ";
                Cmd.CommandText += "INNER JOIN CPMeasurement PMD ON PMD.CPMeasurementGUID = ACT.CPMeasurementGUID ";
                Cmd.CommandText += "WHERE PMD.CPMeasurementGUID IN ( " + LBL_CPMeasurementGUID.Text + " ) ";
                Cmd.CommandText += "ORDER BY PMD.CreatedDt";

                Cmd.CommandType = CommandType.Text;
                DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);
            }

            if (DT.Rows.Count == 0)
            { _Chart.Visible = false; }
            else
            {
                _Chart.Visible = true;

                //========== Clear series ==========//
                _Chart.Series.Clear();

                SetCommonChartStyle(_Chart);

                #region Add Series
                Series objSeries1 = new Series();
                objSeries1.Name = "Ave. Delta E";
                objSeries1.Type = SeriesChartType.Spline;
                //objSeries1.MarkerStyle = MarkerStyle.Triangle;
                objSeries1.XValueType = ChartValueTypes.Date;
                objSeries1.YValueType = ChartValueTypes.Double;
                objSeries1.ValueMemberX = "CreatedDT";
                objSeries1.ValueMembersY = "AVG_DE";
                objSeries1.Color = System.Drawing.Color.Green;
                _Chart.Series.Add(objSeries1);

                Series objSeries2 = new Series();
                objSeries2.Name = "Max Delta E";
                objSeries2.Type = SeriesChartType.Spline;
                //objSeries2.MarkerStyle = MarkerStyle.Triangle;
                objSeries2.XValueType = ChartValueTypes.Date;
                objSeries2.YValueType = ChartValueTypes.Double;
                objSeries2.ValueMemberX = "CreatedDT";
                objSeries2.ValueMembersY = "Max_DE";
                //objSeries2.ChartArea.
                objSeries2.Color = System.Drawing.Color.Red;

                _Chart.Series.Add(objSeries2);

                #endregion

                //========== Show it in the chart ==========//
                for (int i = 0; i < DT.Rows.Count; i = i + 1)
                {
                    _Chart.Series[0].Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), DT.Rows[i]["AVG_DE"].ToString());
                    _Chart.Series[0].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + "\nAve. Delta E : " + DT.Rows[i]["AVG_DE"].ToString();

                    _Chart.Series[1].Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), (DT.Rows[i]["Max_DE"].ToString()));
                    _Chart.Series[1].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + "\nMax. Delta E : " + DT.Rows[i]["MAX_DE"].ToString() + "";
                }

                string FileName = "UploadFiles/temp/" + Sessions.UserID + "_" + Path.GetFileName(_Chart.ImageUrl) + "." + _Chart.ImageType;
                _Chart.Save(Server.MapPath(FileName));

                objSeries1.MarkerStyle = MarkerStyle.Triangle;
                objSeries2.MarkerStyle = MarkerStyle.Triangle;

                #region Give Scrolling Effect
                _Chart.AJAXZoomEnabled = true;


                // scrolling and zooming will force keeping of series data between callbacks.
                _Chart.ChartAreas["Default"].CursorX.UserEnabled = true;

                // Set initial X axis zooming                        
                _Chart.ChartAreas["Default"].AxisY.View.MinSize = 10.0;
                _Chart.ChartAreas["Default"].AxisX.View.Position = 0.0;
                _Chart.ChartAreas["Default"].AxisX.View.Size = 50.0;
                #endregion
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillDensity(System.Web.UI.WebControls.Label LBL_CPMeasurementGUID, Chart _Chart)
    {
        try
        {
            //========== Declare DataTable ==========//
            DataSet DSet = new DataSet();

            if (LBL_CPMeasurementGUID.Text != "")
            {
                //========== Get Data Table ==========//
                SqlCommand Cmd = new SqlCommand();

                Cmd.CommandText = "";

                #region Cyan
                Cmd.CommandText += "SELECT MAX(PMD.CREATEDDT) AS CREATEDDT , ISNULL(MAX(PPD.DENSITY),0) AS MAX_DENSITY ";
                Cmd.CommandText += "FROM CPMeasurement PMD ";
                Cmd.CommandText += "Inner Join CPPatchDataLab PPD ON PPD.MEASUREMENTGUID = PMD.CPMEASUREMENTGUID ";
                Cmd.CommandText += "WHERE PMD.CPMEASUREMENTGUID IN (" + LBL_CPMeasurementGUID.Text + ") ";
                Cmd.CommandText += "AND (PPD.CMYK_C = 100 AND PPD.CMYK_M = 0 AND PPD.CMYK_Y = 0 AND PPD.CMYK_K = 0) ";
                Cmd.CommandText += "GROUP BY PMD.CPMEASUREMENTGUID ";
                Cmd.CommandText += "ORDER BY CREATEDDT ";
                #endregion

                #region Magenta
                Cmd.CommandText += "SELECT MAX(PMD.CREATEDDT) AS CREATEDDT , ISNULL(MAX(PPD.DENSITY),0) AS MAX_DENSITY ";
                Cmd.CommandText += "FROM CPMeasurement PMD ";
                Cmd.CommandText += "Inner Join CPPatchDataLab PPD ON PPD.MEASUREMENTGUID = PMD.CPMEASUREMENTGUID ";
                Cmd.CommandText += "WHERE PMD.CPMEASUREMENTGUID IN (" + LBL_CPMeasurementGUID.Text + ") ";
                Cmd.CommandText += "AND (PPD.CMYK_C = 0 AND PPD.CMYK_M = 100 AND PPD.CMYK_Y = 0 AND PPD.CMYK_K = 0) ";
                Cmd.CommandText += "GROUP BY PMD.CPMEASUREMENTGUID ";
                Cmd.CommandText += "ORDER BY CREATEDDT ";
                #endregion

                #region Yellow
                Cmd.CommandText += "SELECT MAX(PMD.CREATEDDT) AS CREATEDDT , ISNULL(MAX(PPD.DENSITY),0) AS MAX_DENSITY ";
                Cmd.CommandText += "FROM CPMeasurement PMD ";
                Cmd.CommandText += "Inner Join CPPatchDataLab PPD ON PPD.MEASUREMENTGUID = PMD.CPMEASUREMENTGUID ";
                Cmd.CommandText += "WHERE PMD.CPMEASUREMENTGUID IN (" + LBL_CPMeasurementGUID.Text + ") ";
                Cmd.CommandText += "AND (PPD.CMYK_C = 0 AND PPD.CMYK_M = 0 AND PPD.CMYK_Y = 100 AND PPD.CMYK_K = 0) ";
                Cmd.CommandText += "GROUP BY PMD.CPMEASUREMENTGUID ";
                Cmd.CommandText += "ORDER BY CREATEDDT ";
                #endregion

                #region Black
                Cmd.CommandText += "SELECT MAX(PMD.CREATEDDT) AS CREATEDDT , ISNULL(MAX(PPD.DENSITY),0) AS MAX_DENSITY ";
                Cmd.CommandText += "FROM CPMeasurement PMD ";
                Cmd.CommandText += "Inner Join CPPatchDataLab PPD ON PPD.MEASUREMENTGUID = PMD.CPMEASUREMENTGUID ";
                Cmd.CommandText += "WHERE PMD.CPMEASUREMENTGUID IN (" + LBL_CPMeasurementGUID.Text + ") ";
                Cmd.CommandText += "AND (PPD.CMYK_C = 0 AND PPD.CMYK_M = 0 AND PPD.CMYK_Y = 0 AND PPD.CMYK_K = 100) ";
                Cmd.CommandText += "GROUP BY PMD.CPMEASUREMENTGUID ";
                Cmd.CommandText += "ORDER BY CREATEDDT ";
                #endregion

                Cmd.CommandType = CommandType.Text;
                DSet = (new Fuji.DataAccess.Database()).GetDataSet(Cmd);
            }

            if (DSet.Tables.Count == 0)
            { _Chart.Visible = false; }
            else
            {
                _Chart.Visible = true;

                //========== Fill CHart ==========//
                _Chart.Series.Clear();

                SetCommonChartStyle(_Chart);

                #region Add Series
                Series objSeries1 = new Series();
                objSeries1.Name = "Cyan";
                objSeries1.Type = SeriesChartType.Spline;
                //objSeries1.MarkerStyle = MarkerStyle.Triangle;
                objSeries1.XValueType = ChartValueTypes.Date;
                objSeries1.YValueType = ChartValueTypes.Double;
                objSeries1.ValueMemberX = "CreatedDT";
                objSeries1.ValueMembersY = "MAX_DENSITY";
                objSeries1.Color = System.Drawing.Color.Cyan;
                _Chart.Series.Add(objSeries1);

                Series objSeries2 = new Series();
                objSeries2.Name = "Magenta";
                objSeries2.Type = SeriesChartType.Spline;
                //objSeries2.MarkerStyle = MarkerStyle.Triangle;
                objSeries2.XValueType = ChartValueTypes.Date;
                objSeries2.YValueType = ChartValueTypes.Double;
                objSeries2.ValueMemberX = "CreatedDT";
                objSeries2.ValueMembersY = "MAX_DENSITY";
                objSeries2.Color = System.Drawing.Color.Magenta;
                _Chart.Series.Add(objSeries2);

                Series objSeries3 = new Series();
                objSeries3.Name = "Yellow";
                objSeries3.Type = SeriesChartType.Spline;
                //objSeries3.MarkerStyle = MarkerStyle.Triangle;
                objSeries3.XValueType = ChartValueTypes.Date;
                objSeries3.YValueType = ChartValueTypes.Double;
                objSeries3.ValueMemberX = "CreatedDT";
                objSeries3.ValueMembersY = "MAX_DENSITY";
                objSeries3.Color = System.Drawing.Color.Yellow;
                _Chart.Series.Add(objSeries3);

                Series objSeries4 = new Series();
                objSeries4.Name = "Black";
                objSeries4.Type = SeriesChartType.Spline;
                //objSeries4.MarkerStyle = MarkerStyle.Triangle;
                objSeries4.XValueType = ChartValueTypes.Date;
                objSeries4.YValueType = ChartValueTypes.Double;
                objSeries4.ValueMemberX = "CreatedDT";
                objSeries4.ValueMembersY = "MAX_DENSITY";
                objSeries4.Color = System.Drawing.Color.Black;
                _Chart.Series.Add(objSeries4);
                #endregion

                for (int i = 0; i < DSet.Tables.Count; i = i + 1)
                {
                    //========== Add Values ==========//
                    for (int j = 0; j < DSet.Tables[i].Rows.Count; j = j + 1)
                    {
                        //_Chart.Series[i].Color = System.Drawing.Color.FromName(DSet.Tables[i].Rows[0]["TableName"].ToString());
                        _Chart.Series[i].Points.AddXY(DSet.Tables[i].Rows[j]["CreatedDT"].ToString(), DSet.Tables[i].Rows[j]["MAX_DENSITY"].ToString());
                        _Chart.Series[i].Points[j].ToolTip = "Date : " + Convert.ToDateTime(DSet.Tables[i].Rows[j]["CreatedDT"]).ToString() + "\nDensity : " + DSet.Tables[i].Rows[j]["MAX_DENSITY"].ToString() + "";
                    }
                }

                string FileName = "UploadFiles/temp/" + Sessions.UserID + "_" + Path.GetFileName(_Chart.ImageUrl) + "." + _Chart.ImageType;
                _Chart.Save(Server.MapPath(FileName));

                objSeries1.MarkerStyle = MarkerStyle.Triangle;
                objSeries2.MarkerStyle = MarkerStyle.Triangle;
                objSeries3.MarkerStyle = MarkerStyle.Triangle;
                objSeries4.MarkerStyle = MarkerStyle.Triangle;

                #region Give Scrolling Effect
                _Chart.AJAXZoomEnabled = true;


                // scrolling and zooming will force keeping of series data between callbacks.
                _Chart.ChartAreas["Default"].CursorX.UserEnabled = true;

                // Set initial X axis zooming                        
                _Chart.ChartAreas["Default"].AxisY.View.MinSize = 10.0;
                _Chart.ChartAreas["Default"].AxisX.View.Position = 0.0;
                _Chart.ChartAreas["Default"].AxisX.View.Size = 50.0;
                #endregion
            }
        }
        catch (Exception Ex) { throw Ex; }
    }
    private void FillDotgain(System.Web.UI.WebControls.Label LBL_CPMeasurementGUID, Chart _Chart)
    {
        try
        {
            //========== Declarations ==========//
            Fuji.Business.CPPatchDataLabData objColorCharts;
            string _ColorName = "", _Created_Time = "", _CMYK = "", _DotGain = "", _Tone = "";
            DataSet DSet;
            int indPlot = 0;

            //========== Get The Dot Gain ==========//
            string[] _GUID = LBL_CPMeasurementGUID.Text.Replace("'", "").Split(',');

            for (int k = 0; k < _GUID.Length; k = k + 1)
            {
                //========== Get the dataset ==========//
                objColorCharts = new CPPatchDataLabData();
                objColorCharts.MeasurementGUID = _GUID[k];
                DSet = objColorCharts.uspGetColorCharDotGain();

                for (int i = 0; i < DSet.Tables.Count; i = i + 1)
                {
                    for (int j = 0; j < DSet.Tables[i].Rows.Count; j = j + 1)
                    {
                        //========== Variables ==========//
                        _ColorName = DSet.Tables[i].Rows[j]["ColorName"].ToString();
                        _Created_Time = System.DateTime.Now.ToString();
                        _CMYK = Convert.ToInt32(DSet.Tables[i].Rows[j]["CMYK"]).ToString();
                        _DotGain = DSet.Tables[i].Rows[j]["Dotgain"].ToString();
                        _Tone = "";

                        if (i == 0)
                        { _Tone = "Quarter Tone"; }
                        else if (i == 1)
                        { _Tone = "Mid Tone"; }
                        else if (i == 2)
                        { _Tone = "Three Quarter Tone"; }

                        try
                        {
                            Series objSeries = _Chart.Series[_ColorName + j.ToString()];
                            objSeries.Type = SeriesChartType.Spline;
                            objSeries.XValueType = ChartValueTypes.DateTime;
                            objSeries.YValueType = ChartValueTypes.Double;

                            if (Convert.ToDecimal(_DotGain) > 25)
                            { objSeries.Points.AddXY(_Created_Time, "25"); }
                            else if (Convert.ToDecimal(_DotGain) < -25)
                            { objSeries.Points.AddXY(_Created_Time, "-25"); }
                            else
                            { objSeries.Points.AddXY(_Created_Time, _DotGain); }

                            //Changes done by Maulik Dhorajia
                            //Issue with the index mentioned.
                            indPlot = objSeries.Points.Count - 1;

                            objSeries.Points[indPlot].ToolTip = "GUID : " + _GUID[k] + ", Created Date : " + _Created_Time + ", Color : " + _ColorName + " " + _Tone + " " + _CMYK + "%, Dotgain : " + _DotGain;
                            objSeries.Points[indPlot].Href = "#";
                        }
                        catch
                        {
                            continue;
                        }

                    }

                }
            }
            if (indPlot > 0)
            {
                _Chart.Visible = true;
                if (_GUID.Length > 0)
                {
                    _Chart.Visible = true;

                    string FileName = "UploadFiles/temp/" + Sessions.UserID + "_" + Path.GetFileName(_Chart.ImageUrl) + "." + _Chart.ImageType;
                    _Chart.Save(Server.MapPath(FileName));

                    for (int i = 0; i < _Chart.Series.Count; i = i + 1)
                    {
                        _Chart.Series[i].MarkerStyle = MarkerStyle.Triangle;
                    }

                    #region Give Scrolling Effect
                    _Chart.AJAXZoomEnabled = true;

                    //// scrolling and zooming will force keeping of series data between callbacks.
                    _Chart.ChartAreas["ChartArea1"].CursorX.UserEnabled = true;

                    //// Set initial X axis zooming                        
                    _Chart.ChartAreas["ChartArea1"].AxisY.View.MinSize = 10.0;
                    _Chart.ChartAreas["ChartArea1"].AxisX.View.Position = 0.0;
                    _Chart.ChartAreas["ChartArea1"].AxisX.View.Size = 50.0;
                    #endregion
                }
                else
                { _Chart.Visible = false; }
            }
            else
            { _Chart.Visible = false; }
        }
        catch (Exception Ex) { throw Ex; }
    }
    private void FillPaperWhite(System.Web.UI.WebControls.Label LBL_CPMeasurementGUID, Chart _Chart)
    {
        try
        {
            //========== Declare DataTable ==========//
            DataTable DT = new DataTable();

            if (LBL_CPMeasurementGUID.Text != "")
            {
                //========== Get Data Table ==========//
                SqlCommand Cmd = new SqlCommand();

                Cmd.CommandText = "SELECT (PMD.CREATEDDT) As CreatedDT , (ACT.PAPER) As Paper ";
                Cmd.CommandText += "FROM CPMeasurement PMD ";
                Cmd.CommandText += "Inner Join CPMeasurement_ACT ACT ON ACT.CPMeasurementGUID = PMD.CPMeasurementGUID ";
                Cmd.CommandText += "WHERE PMD.CPMeasurementGUID IN (" + LBL_CPMeasurementGUID.Text + ") ";
                Cmd.CommandText += "ORDER BY CREATEDDT ";

                Cmd.CommandType = CommandType.Text;
                DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);
            }

            if (DT.Rows.Count == 0)
            { _Chart.Visible = false; }
            else
            {
                _Chart.Visible = true;
                //========== Clear series ==========//
                _Chart.Series.Clear();

                SetCommonChartStyle(_Chart);

                #region Add Series
                Series objSeries1 = new Series();
                objSeries1.Name = "Paper";
                objSeries1.Type = SeriesChartType.Spline;
                //objSeries1.MarkerStyle = MarkerStyle.Triangle;
                objSeries1.XValueType = ChartValueTypes.Date;
                objSeries1.YValueType = ChartValueTypes.Double;
                objSeries1.ValueMemberX = "CreatedDT";
                objSeries1.ValueMembersY = "Paper";
                objSeries1.Color = System.Drawing.Color.Blue;
                _Chart.Series.Add(objSeries1);
                #endregion

                //========== Show it in the chart ==========//
                for (int i = 0; i < DT.Rows.Count; i = i + 1)
                {
                    _Chart.Series[0].Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), DT.Rows[i]["Paper"].ToString());
                    _Chart.Series[0].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + " \nPaper : " + DT.Rows[i]["Paper"].ToString() + "";
                }

                string FileName = "UploadFiles/temp/" + Sessions.UserID + "_" + Path.GetFileName(_Chart.ImageUrl) + "." + _Chart.ImageType;
                _Chart.Save(Server.MapPath(FileName));

                objSeries1.MarkerStyle = MarkerStyle.Triangle;

                #region Give Scrolling Effect
                _Chart.AJAXZoomEnabled = true;

                // scrolling and zooming will force keeping of series data between callbacks.
                _Chart.ChartAreas["Default"].CursorX.UserEnabled = true;

                // Set initial X axis zooming                        
                _Chart.ChartAreas["Default"].AxisY.View.MinSize = 10.0;
                _Chart.ChartAreas["Default"].AxisX.View.Position = 0.0;
                _Chart.ChartAreas["Default"].AxisX.View.Size = 50.0;
                #endregion
            }
        }
        catch (Exception Ex) { throw Ex; }
    }
    private void FillTempHumidity
        (
           DropDownList DDL_ShiftFromHours, DropDownList DDL_ShiftFromMins, RadioButtonList RBTN_ShiftFromAMPM
        , DropDownList DDL_ShiftToHours, DropDownList DDL_ShiftToMins, RadioButtonList RBTN_ShiftToAMPM
        , Chart _Chart
        )
    {
        try
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();

            Cmd.CommandText = "SELECT (TRHDATA.CREATEDDT) AS CREATEDDT , ISNULL(MAX(TRHDATA.TempC),0) AS TempC , ISNULL(MAX(TRHDATA.TempF),0) AS TempF , ISNULL(MAX(TRHDATA.RH),0) AS RH ";
            Cmd.CommandText += "FROM TempRHData As TRHDATA ";
            Cmd.CommandText += "INNER JOIN TempRHDevice_CPDevice TCPD ON TRHDATA.TempRHDeviceID = TRHDATA.TempRHDeviceID ";
            Cmd.CommandText += "WHERE TCPD.CPDeviceID = " + DDL_Device.SelectedValue + " ";
            Cmd.CommandText += "AND (TRHDATA.CreatedDT Between '" + TXT_FromDate.Text + "' AND '" + Convert.ToDateTime(TXT_ToDate.Text).AddDays(1).ToShortDateString() + "') ";
            Cmd.CommandText += "AND ( DatePart( HH , TRHDATA.CreatedDT ) BETWEEN ( " + DDL_ShiftFromHours.SelectedValue + " + " + RBTN_ShiftFromAMPM.SelectedValue + " ) AND ( " + DDL_ShiftToHours.SelectedValue + " + " + RBTN_ShiftToAMPM.SelectedValue + " + 1) ) ";
            Cmd.CommandText += "AND ( DatePart( MI , TRHDATA.CreatedDT ) BETWEEN " + DDL_ShiftFromMins.SelectedValue + "  AND (" + DDL_ShiftToMins.SelectedValue + " + 1) ) ";
            Cmd.CommandText += "GROUP BY TRHDATA.CREATEDDT ";

            Cmd.CommandType = CommandType.Text;
            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);
            if (DT.Rows.Count == 0)
            { _Chart.Visible = false; }
            else
            {
                _Chart.Visible = true;

                //========== Clear series ==========//
                _Chart.Series.Clear();

                SetCommonChartStyle(_Chart);

                #region Add Series
                Series objSeries1 = new Series();
                objSeries1.Name = "Temp. Celcius";
                objSeries1.Type = SeriesChartType.Spline;
                //objSeries1.MarkerStyle = MarkerStyle.Triangle;
                objSeries1.XValueType = ChartValueTypes.Date;
                objSeries1.YValueType = ChartValueTypes.Double;
                objSeries1.ValueMemberX = "CreatedDT";
                objSeries1.ValueMembersY = "TempC";
                objSeries1.Color = System.Drawing.Color.Red;
                _Chart.Series.Add(objSeries1);

                Series objSeries2 = new Series();
                objSeries2.Name = "Temp. Fehrenite";
                objSeries2.Type = SeriesChartType.Spline;
                //objSeries2.MarkerStyle = MarkerStyle.Triangle;
                objSeries2.XValueType = ChartValueTypes.Date;
                objSeries2.YValueType = ChartValueTypes.Double;
                objSeries2.ValueMemberX = "CreatedDT";
                objSeries2.ValueMembersY = "TempF";
                objSeries2.Color = System.Drawing.Color.Blue;
                _Chart.Series.Add(objSeries2);

                Series objSeries3 = new Series();
                objSeries3.Name = "Rel. Humidity";
                objSeries3.Type = SeriesChartType.Spline;
                //objSeries3.MarkerStyle = MarkerStyle.Triangle;
                objSeries3.XValueType = ChartValueTypes.Date;
                objSeries3.YValueType = ChartValueTypes.Double;
                objSeries3.ValueMemberX = "CreatedDT";
                objSeries3.ValueMembersY = "RH";
                objSeries3.Color = System.Drawing.Color.Green;
                _Chart.Series.Add(objSeries3);
                #endregion

                //========== Show it in the chart ==========//
                for (int i = 0; i < DT.Rows.Count; i = i + 1)
                {
                    _Chart.Series[0].Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), DT.Rows[i]["Tempc"].ToString());
                    _Chart.Series[0].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + " \nTemp. Celcius : " + DT.Rows[i]["TempC"].ToString() + "";

                    _Chart.Series[1].Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), DT.Rows[i]["TempF"].ToString());
                    _Chart.Series[1].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + " \nTemp. Fehrenite : " + DT.Rows[i]["TempF"].ToString() + "";

                    _Chart.Series[2].Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), DT.Rows[i]["RH"].ToString());
                    _Chart.Series[2].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + " \nRel. Humidity : " + DT.Rows[i]["RH"].ToString() + "";
                }

                string FileName = "UploadFiles/temp/" + Sessions.UserID + "_" + Path.GetFileName(_Chart.ImageUrl) + "." + _Chart.ImageType;
                _Chart.Save(Server.MapPath(FileName));

                objSeries1.MarkerStyle = MarkerStyle.Triangle;
                objSeries2.MarkerStyle = MarkerStyle.Triangle;
                objSeries3.MarkerStyle = MarkerStyle.Triangle;

                #region Give Scrolling Effect
                _Chart.AJAXZoomEnabled = true;

                // scrolling and zooming will force keeping of series data between callbacks.
                _Chart.ChartAreas["Default"].CursorX.UserEnabled = true;

                // Set initial X axis zooming                        
                _Chart.ChartAreas["Default"].AxisY.View.MinSize = 10.0;
                _Chart.ChartAreas["Default"].AxisX.View.Position = 0.0;
                _Chart.ChartAreas["Default"].AxisX.View.Size = 50.0;
                #endregion
            }
        }catch(Exception Ex){throw Ex;}
    }
    private void SetCommonChartStyle(Dundas.Charting.WebControl.Chart _Chart)
    {
        try
        {
            //========== Setting Legend Position ==========//
            _Chart.Legends[0].BorderColor = System.Drawing.Color.Gray;
            _Chart.Legends[0].BackColor = System.Drawing.Color.Transparent;
            _Chart.Legends[0].Docking = LegendDocking.Bottom;
            _Chart.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillEmptyChart(Dundas.Charting.WebControl.Chart _Chart)
    {
        try
        {
            _Chart.Series[0].Color = System.Drawing.Color.Transparent;
            _Chart.Series[0].Points.AddXY(Convert.ToDateTime(TXT_FromDate.Text).ToShortDateString(), "8");
            _Chart.Series[0].Points.AddXY(Convert.ToDateTime(TXT_ToDate.Text).ToShortDateString(), "8");
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Create Temp Table For PDF
    private void TempTable()
    {
        try
        {
            #region BasicInfo

            DataTable DT_BasicInfo = new DataTable("BasicInfo");

            DataColumn DC_BasicInfo = new DataColumn();
            DC_BasicInfo.ColumnName = "CompanyName_";
            DC_BasicInfo.DataType = System.Type.GetType("System.String");
            DT_BasicInfo.Columns.Add(DC_BasicInfo);

            DC_BasicInfo = new DataColumn();
            DC_BasicInfo.ColumnName = "Device_";
            DC_BasicInfo.DataType = System.Type.GetType("System.String");
            DT_BasicInfo.Columns.Add(DC_BasicInfo);

            DC_BasicInfo = new DataColumn();
            DC_BasicInfo.ColumnName = "DongleNumber_";
            DC_BasicInfo.DataType = System.Type.GetType("System.String");
            DT_BasicInfo.Columns.Add(DC_BasicInfo);

            DC_BasicInfo = new DataColumn();
            DC_BasicInfo.ColumnName = "Location_";
            DC_BasicInfo.DataType = System.Type.GetType("System.String");
            DT_BasicInfo.Columns.Add(DC_BasicInfo);

            DC_BasicInfo = new DataColumn();
            DC_BasicInfo.ColumnName = "DateRange_";
            DC_BasicInfo.DataType = System.Type.GetType("System.String");
            DT_BasicInfo.Columns.Add(DC_BasicInfo);

            DataRow DR_BasicInfo = DT_BasicInfo.NewRow();

            DR_BasicInfo["CompanyName_"] = LBL_CompanyName.Text.Trim();
            DR_BasicInfo["Device_"] = LBL_DeviceName.Text.Trim();
            DR_BasicInfo["DongleNumber_"] = LBL_DongleNumber.Text.Trim();
            DR_BasicInfo["Location_"] = LBL_BranchName.Text.Trim();
            DR_BasicInfo["DateRange_"] = LBL_DateRange.Text.Trim();

            DT_BasicInfo.Rows.Add(DR_BasicInfo);

            ViewState["DT_BasicInfo"] = DT_BasicInfo;

            #endregion

            #region Search Criteria

            DataTable DT_SearchCriteria = new DataTable("SearchCriteria");

            DataColumn DC_SearchCriteria = new DataColumn();
            DC_SearchCriteria.ColumnName = "SearchField_";
            DC_SearchCriteria.DataType = System.Type.GetType("System.String");
            DT_SearchCriteria.Columns.Add(DC_SearchCriteria);

            DC_SearchCriteria = new DataColumn();
            DC_SearchCriteria.ColumnName = "Operator_";
            DC_SearchCriteria.DataType = System.Type.GetType("System.String");
            DT_SearchCriteria.Columns.Add(DC_SearchCriteria);

            DC_SearchCriteria = new DataColumn();
            DC_SearchCriteria.ColumnName = "Criteria_";
            DC_SearchCriteria.DataType = System.Type.GetType("System.String");
            DT_SearchCriteria.Columns.Add(DC_SearchCriteria);

            DataRow DR_SearchCriteria = DT_SearchCriteria.NewRow();

            DR_SearchCriteria["SearchField_"] = LBL_Field1.Text.Trim();
            DR_SearchCriteria["Operator_"] = LBL_Operator1.Text.Trim();
            DR_SearchCriteria["Criteria_"] = LBL_Criteria1.Text.Trim();

            DT_SearchCriteria.Rows.Add(DR_SearchCriteria);

            DR_SearchCriteria = DT_SearchCriteria.NewRow();

            DR_SearchCriteria["SearchField_"] = LBL_Field2.Text.Trim();
            DR_SearchCriteria["Operator_"] = LBL_Operator2.Text.Trim();
            DR_SearchCriteria["Criteria_"] = LBL_Criteria2.Text.Trim();

            DT_SearchCriteria.Rows.Add(DR_SearchCriteria);

            DR_SearchCriteria = DT_SearchCriteria.NewRow();

            DR_SearchCriteria["SearchField_"] = LBL_Field3.Text.Trim();
            DR_SearchCriteria["Operator_"] = LBL_Operator3.Text.Trim();
            DR_SearchCriteria["Criteria_"] = LBL_Criteria3.Text.Trim();

            DT_SearchCriteria.Rows.Add(DR_SearchCriteria);

            ViewState["DT_SearchCriteria"] = DT_SearchCriteria;

            #endregion

            #region Shift 1

            DataTable DT_Shift1 = new DataTable("Shift1");

            DataColumn DC_Shift1 = new DataColumn();
            DC_Shift1.ColumnName = "Start_";
            DC_Shift1.DataType = System.Type.GetType("System.String");
            DT_Shift1.Columns.Add(DC_Shift1);

            DC_Shift1 = new DataColumn();
            DC_Shift1.ColumnName = "End_";
            DC_Shift1.DataType = System.Type.GetType("System.String");
            DT_Shift1.Columns.Add(DC_Shift1);

            DC_Shift1 = new DataColumn();
            DC_Shift1.ColumnName = "ErrorRatio_";
            DC_Shift1.DataType = System.Type.GetType("System.String");
            DT_Shift1.Columns.Add(DC_Shift1);

            DC_Shift1 = new DataColumn();
            DC_Shift1.ColumnName = "NumberOfReads_";
            DC_Shift1.DataType = System.Type.GetType("System.String");
            DT_Shift1.Columns.Add(DC_Shift1);

            DC_Shift1 = new DataColumn();
            DC_Shift1.ColumnName = "TotalErrors_";
            DC_Shift1.DataType = System.Type.GetType("System.String");
            DT_Shift1.Columns.Add(DC_Shift1);


            DataRow DR_Shift1 = DT_Shift1.NewRow();

            DR_Shift1["Start_"] = LBL_Shift1Start.Text.Trim();
            DR_Shift1["End_"] = LBL_Shift1End.Text.Trim();
            DR_Shift1["ErrorRatio_"] = LBL_Shift1ErrorRatio.Text.Trim();
            DR_Shift1["NumberOfReads_"] = LBL_Shift1NoOfReads.Text.Trim();
            DR_Shift1["TotalErrors_"] = LBL_Shift1TotalErrors.Text.Trim();

            DT_Shift1.Rows.Add(DR_Shift1);

            ViewState["DT_Shift1"] = DT_Shift1;

            #endregion

            #region Shift 2

            DataTable DT_Shift2 = new DataTable("Shift2");

            DataColumn DC_Shift2 = new DataColumn();
            DC_Shift2.ColumnName = "Start_";
            DC_Shift2.DataType = System.Type.GetType("System.String");
            DT_Shift2.Columns.Add(DC_Shift2);

            DC_Shift2 = new DataColumn();
            DC_Shift2.ColumnName = "End_";
            DC_Shift2.DataType = System.Type.GetType("System.String");
            DT_Shift2.Columns.Add(DC_Shift2);

            DC_Shift2 = new DataColumn();
            DC_Shift2.ColumnName = "ErrorRatio_";
            DC_Shift2.DataType = System.Type.GetType("System.String");
            DT_Shift2.Columns.Add(DC_Shift2);

            DC_Shift2 = new DataColumn();
            DC_Shift2.ColumnName = "NumberOfReads_";
            DC_Shift2.DataType = System.Type.GetType("System.String");
            DT_Shift2.Columns.Add(DC_Shift2);

            DC_Shift2 = new DataColumn();
            DC_Shift2.ColumnName = "TotalErrors_";
            DC_Shift2.DataType = System.Type.GetType("System.String");
            DT_Shift2.Columns.Add(DC_Shift2);

            DataRow DR_Shift2 = DT_Shift2.NewRow();

            DR_Shift2["Start_"] = LBL_Shift2Start.Text.Trim();
            DR_Shift2["End_"] = LBL_Shift2End.Text.Trim();
            DR_Shift2["ErrorRatio_"] = LBL_Shift2ErrorRatio.Text.Trim();
            DR_Shift2["NumberOfReads_"] = LBL_Shift2NoOfReads.Text.Trim();
            DR_Shift2["TotalErrors_"] = LBL_Shift1TotalErrors.Text.Trim();

            DT_Shift2.Rows.Add(DR_Shift2);

            ViewState["DT_Shift2"] = DT_Shift2;

            #endregion

            #region Charts Visible
            Session["CHRT_MaxDeltaE1"] = CHRT_MaxDeltaE1.Visible;
            Session["CHRT_Density1"] = CHRT_Density1.Visible;
            Session["CHRT_DotGain1"] = CHRT_DotGain1.Visible;
            Session["CHRT_PaperWhite1"] = CHRT_PaperWhite1.Visible;
            Session["CHRT_TempHumidity1"] = CHRT_TempHumidity1.Visible;

            Session["CHRT_MaxDeltaE2"] = CHRT_MaxDeltaE2.Visible;
            Session["CHRT_Density2"] = CHRT_Density2.Visible;
            Session["CHRT_DotGain2"] = CHRT_DotGain2.Visible;
            Session["CHRT_PaperWhite2"] = CHRT_PaperWhite2.Visible;
            Session["CHRT_TempHumidity2"] = CHRT_TempHumidity2.Visible;
            #endregion

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion
}

/*
 //========== Declare DataTable ==========//
        DataTable DT = new DataTable();

        if (LBL_CPMeasurementGUID.Text != "")
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();

            Cmd.CommandText = "";
            Cmd.CommandText += "SELECT ISNULL(CDOt.[DotGain_C50%],0) as Cyan , ISNULL( CDOt.[DotGain_M50%] , 0 ) AS Magenta , ISNULL( CDOt.[DotGain_Y50%] , 0 ) AS Yellow , ISNULL( CDOt.[DotGain_K50%] , 0 ) as Black , PMD.CreatedDT ";
            Cmd.CommandText += "FROM CPMeasurement_DotGain AS CDOt ";
            Cmd.CommandText += "INNER JOIN CPMeasurement AS PMD ON CDOt.CPMeasurementGUID = PMD.CPMeasurementGUID  ";
            Cmd.CommandText += "AND PMD.CPMeasurementGUID IN (" + LBL_CPMeasurementGUID.Text + ") ";
            Cmd.CommandText += "ORDER BY CREATEDDT ";

            Cmd.CommandType = CommandType.Text;
            DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);
        }

        if (DT.Rows.Count == 0)
        { _Chart.Visible = false; }
        else
        {
            _Chart.Visible = true;

            //========== Fill CHart ==========//
            _Chart.Series.Clear();

            SetCommonChartStyle(_Chart);

            #region Add Series
            Series objSeries1 = new Series();
            objSeries1.Name = "Cyan";
            objSeries1.Type = SeriesChartType.Spline;
            objSeries1.XValueType = ChartValueTypes.Date;
            objSeries1.YValueType = ChartValueTypes.Double;
            objSeries1.ValueMemberX = "CreatedDT";
            objSeries1.ValueMembersY = "Cyan";
            objSeries1.Color = System.Drawing.Color.Cyan;
            _Chart.Series.Add(objSeries1);

            Series objSeries2 = new Series();
            objSeries2.Name = "Magenta";
            objSeries2.Type = SeriesChartType.Spline;
            objSeries2.XValueType = ChartValueTypes.Date;
            objSeries2.YValueType = ChartValueTypes.Double;
            objSeries2.ValueMemberX = "CreatedDT";
            objSeries2.ValueMembersY = "Magenta";
            objSeries2.Color = System.Drawing.Color.Magenta;
            _Chart.Series.Add(objSeries2);

            Series objSeries3 = new Series();
            objSeries3.Name = "Yellow";
            objSeries3.Type = SeriesChartType.Spline;
            objSeries3.XValueType = ChartValueTypes.Date;
            objSeries3.YValueType = ChartValueTypes.Double;
            objSeries3.ValueMemberX = "CreatedDT";
            objSeries3.ValueMembersY = "Yellow";
            objSeries3.Color = System.Drawing.Color.Yellow;
            _Chart.Series.Add(objSeries3);

            Series objSeries4 = new Series();
            objSeries4.Name = "Black";
            objSeries4.Type = SeriesChartType.Spline;
            objSeries4.XValueType = ChartValueTypes.Date;
            objSeries4.YValueType = ChartValueTypes.Double;
            objSeries4.ValueMemberX = "CreatedDT";
            objSeries4.ValueMembersY = "Black";
            objSeries4.Color = System.Drawing.Color.Black;
            _Chart.Series.Add(objSeries4);
            #endregion

            for (int i = 0; i < DT.Rows.Count; i = i + 1)
            {
                _Chart.Series[0].Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), DT.Rows[i]["Cyan"].ToString());
                _Chart.Series[0].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + " \nDot Gain Cyan : " + DT.Rows[i]["Cyan"].ToString() + "";

                _Chart.Series[1].Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), DT.Rows[i]["Magenta"].ToString());
                _Chart.Series[1].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + " \nDot Gain Magenta : " + DT.Rows[i]["Magenta"].ToString() + "";

                _Chart.Series[2].Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), DT.Rows[i]["Yellow"].ToString());
                _Chart.Series[2].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + " \nDot Gain Yellow : " + DT.Rows[i]["Yellow"].ToString() + "";

                _Chart.Series[3].Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), DT.Rows[i]["Black"].ToString());
                _Chart.Series[3].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + " \nDot Gain Black : " + DT.Rows[i]["Black"].ToString() + "";
            }

            string FileName = "UploadFiles/temp/" + Sessions.UserID + "_" + Path.GetFileName(_Chart.ImageUrl) + "." + _Chart.ImageType;
            _Chart.Save(Server.MapPath(FileName));

            objSeries1.MarkerStyle = MarkerStyle.Triangle;
            objSeries2.MarkerStyle = MarkerStyle.Triangle;
            objSeries3.MarkerStyle = MarkerStyle.Triangle;
            objSeries4.MarkerStyle = MarkerStyle.Triangle;

            #region Give Scrolling Effect
            _Chart.AJAXZoomEnabled = true;


            // scrolling and zooming will force keeping of series data between callbacks.
            _Chart.ChartAreas["Default"].CursorX.UserEnabled = true;

            // Set initial X axis zooming                        
            _Chart.ChartAreas["Default"].AxisY.View.MinSize = 10.0;
            _Chart.ChartAreas["Default"].AxisX.View.Position = 0.0;
            _Chart.ChartAreas["Default"].AxisX.View.Size = 50.0;
            #endregion
        }
 */