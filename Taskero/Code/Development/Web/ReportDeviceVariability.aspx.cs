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

public partial class ReportDeviceVariability : PageBase
{
    #region Page Load AND Events
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

                //========== Fill DDL ==========//
                FillCompany1(null, null);
                FillCompany2(null, null);

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
            if (DDL_Device1.SelectedIndex == 0)
            { LblError.Text = "Please select a device as device 1!!!"; return; }
            if (DDL_Device2.SelectedIndex == 0)
            { LblError.Text = "Please select a device as device 2!!!"; return; }
            if (TXT_FromDate.Text == "")
            { LblError.Text = "From Date is required!!!"; return; }
            if (TXT_ToDate.Text == "")
            { LblError.Text = "To Date is required!!!"; return; }

            //========== Filling Label ==========//
            lblReportTitle.Text = "Device Variability Report";
            TDReportTitle.Attributes.Add("class", "");
            LTRL_Field1.Text = DDL_OPT1_Field.SelectedItem.Text;
            LTRL_Field2.Text = DDL_OPT2_Field.SelectedItem.Text;
            LTRL_Field3.Text = DDL_OPT3_Field.SelectedItem.Text;

            LTRL_Operator1.Text = DDL_OPT1_Operator.SelectedItem.Text;
            LTRL_Operator2.Text = DDL_OPT2_Operator.SelectedItem.Text;
            LTRL_Operator3.Text = DDL_OPT3_Operator.SelectedItem.Text;

            LTRL_Criteria1.Text = TXT_OPT1_KeyWords.Text;
            LTRL_Criteria2.Text = TXT_OPT2_KeyWords.Text;
            LTRL_Criteria3.Text = TXT_OPT3_KeyWords.Text;

            //========== Fill Dates ==========//
            LTRL_DateRange.Text = TXT_FromDate.Text + " - " + TXT_ToDate.Text;

            //========== FILL Device Name ==========//
            LTRL_DeviceName1.Text = DDL_Device1.SelectedItem.Text;
            LTRL_DeviceName2.Text = DDL_Device2.SelectedItem.Text;

            //========== Fill Device Information ==========//
            LTRL_Company1.Text = DDL_Company1.SelectedItem.Text;
            LTRL_LocationName1.Text = DDL_Branch1.SelectedItem.Text;
            LTRL_DongleNumber1.Text = DDL_DongleNo1.SelectedItem.Text;
            LTRL_Device1.Text = DDL_Device1.SelectedItem.Text;

            LTRL_Company2.Text = DDL_Company2.SelectedItem.Text;
            LTRL_LocationName2.Text = DDL_Branch2.SelectedItem.Text;
            LTRL_DongleNumber2.Text = DDL_DongleNo2.SelectedItem.Text;
            LTRL_Device2.Text = DDL_Device2.SelectedItem.Text;

            //========== Fill All GUIDs ==========//
            GetAllGuids(DDL_Device1, LTRL_CPMeasurementGUID1);
            GetAllGuids(DDL_Device2, LTRL_CPMeasurementGUID2);

            if (LTRL_CPMeasurementGUID1.Text == "" && LTRL_CPMeasurementGUID2.Text == "")
            { LblError.Text = "No measurements found for the search criteria selected!"; }
            else
            {
                //========== Fills if No GUID ==========//
                //if (LTRL_CPMeasurementGUID1.Text == "") { LTRL_CPMeasurementGUID1.Text = "'B3BFE5CD-679A-4E28-A9D4-E005FA7D3519'"; }
                //if (LTRL_CPMeasurementGUID2.Text == "") { LTRL_CPMeasurementGUID2.Text = "'B3BFE5CD-679A-4E28-A9D4-E005FA7D3519'"; }

                //========== Hiding Search / displaying Result ==========//
                PNL_Search.Style.Add("display", "none");
                PNL_Result.Style.Add("display", "");

                //========== Fill Data ==========//        
                FillForm();

                TempTable();
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }

    }
    protected void BTN_BackToSearch_Click(object sender, EventArgs e)
    {
        //========== displaying Search / Hiding Result ==========//
        PNL_Search.Style.Add("display", "");
        PNL_Result.Style.Add("display", "none");
        lblReportTitle.Text = "Device Variability Report(Search Criteria)";
    }
    protected void BTN_MakePDF_Click(object sender, EventArgs e)
    {
        try
        {
            Fuji.Common.ReportPDF objReportPDF = new Fuji.Common.ReportPDF();
            objReportPDF.ReportType = (int)Fuji.Common.Enums.enumReportType.DeviceVariabilityReport;
            objReportPDF.PDFPath = Server.MapPath("UploadFiles/Report_PDF/");
            objReportPDF.PDF_NAME = Sessions.UserID + "_DeviceVariabilityReport.PDF";

            if (ViewState["DT_BasicInfo"] != null)
            {
                objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_BasicInfo"]);
            }
            if (ViewState["DT_SearchCriteria"] != null)
            {
                objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_SearchCriteria"]);
            }
            if (ViewState["DT_Measurement"] != null)
            {
                objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_Measurement"]);
            }
            objReportPDF.MakePDF();
            Fuji.Common.CommonFunctions.DownlaodFile(objReportPDF.PDFPath, objReportPDF.PDF_NAME, objReportPDF.PDF_NAME);
            //Response.Write("window.open(" + Server.MapPath( objReportPDF.PDFPath + objReportPDF.PDF_NAME ) + ")");
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
    protected void FillCompany1(object sender, EventArgs e)
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
            DDL_Company1.Items.Clear();

            //========== Fill DDL ==========//
            if (DT_Company != null && DT_Company.Rows.Count > 0)
            {
                DDL_Company1.DataSource = DT_Company;
                DDL_Company1.DataTextField = "CompanyName";
                DDL_Company1.DataValueField = "CompanyID";
                DDL_Company1.DataBind();
            }
            else
            { DDL_Company1.DataSource = null; DDL_Company1.DataBind(); }

            //========== Insert Items DropDownList in Company ===========//
            DDL_Company1.Items.Insert(0, new ListItem("--- Select Company ---", "0"));
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void FillBranch1(object sender, EventArgs e)
    {
        try
        {
            //========== Declare the Object of the Branch ===========//
            UserBranchData objUserBranch = new UserBranchData();
            objUserBranch.TaskeroUserID = Fuji.Common.Sessions.UserID;
            objUserBranch.CompanyID = Convert.ToInt32(DDL_Company1.SelectedValue);

            //==========  Get Data Table and Bind ==========//
            DataTable DT_Branch = new DataTable();
            DT_Branch = objUserBranch.uspGetBranchOfCompanyByUserID(Fuji.Common.Sessions.RoleID);

            //========== Clearing all the values ==========//
            DDL_Branch1.Items.Clear();

            //========== Fill DDL ==========//
            if (DT_Branch != null && DT_Branch.Rows.Count > 0)
            {
                DDL_Branch1.DataSource = DT_Branch;
                DDL_Branch1.DataTextField = "BranchName";
                DDL_Branch1.DataValueField = "BranchID";
                DDL_Branch1.DataBind();
            }
            else
            { DDL_Branch1.DataSource = null; DDL_Branch1.DataBind(); }

            //========== Insert Items DropDownList in Branch ===========//
            DDL_Branch1.Items.Insert(0, new ListItem("--- Select Branch ---", "0"));
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void FillDongle1(object sender, EventArgs e)
    {
        //========== Clearing all the values ==========//
        DDL_DongleNo1.Items.Clear();

        try
        {
            //========== Declare the Object of the Dongle ===========//
            UserDongleData objUserDongle = new UserDongleData();

            //==========  Get Data Table and Fill ==========//        
            DataTable DT_Dongle = new DataTable();
            objUserDongle.TaskeroUserID = Fuji.Common.Sessions.UserID;
            objUserDongle.UserDongleID = Convert.ToInt32(DDL_Branch1.SelectedValue);
            DT_Dongle = objUserDongle.uspGetDongleByUserIDAndBranchID(Fuji.Common.Sessions.RoleID);

            //========== Fill DDL ==========//
            if (DT_Dongle != null && DT_Dongle.Rows.Count > 0)
            {
                DDL_DongleNo1.DataSource = DT_Dongle;
                DDL_DongleNo1.DataTextField = "DongleSerial";//CPSerialNum
                DDL_DongleNo1.DataValueField = "CPDongleID";
                DDL_DongleNo1.DataBind();
            }
            else
            { DDL_DongleNo1.DataSource = null; DDL_DongleNo1.DataBind(); }

            //========== Insert Items DropDownList in Dongle ===========//
            DDL_DongleNo1.Items.Insert(0, new ListItem("--- Select Dongle ---", "0"));
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void FillDevice1(object sender, EventArgs e)
    {
        //========== Clearing all the values ==========//
        DDL_Device1.Items.Clear();

        try
        {
            //========== Declare the Object of the Dongle ===========//
            CPDeviceData objCPDevice = new CPDeviceData();

            //==========  Get Data Table and Fill ==========//        
            DataTable DT_Device = new DataTable();
            objCPDevice.CPDongleID = Convert.ToInt32(DDL_DongleNo1.SelectedValue);
            DT_Device = objCPDevice.uspGetDeviceByDongleIDColors();

            //========== Fill DDL ==========//
            if (DT_Device != null && DT_Device.Rows.Count > 0)
            {
                DDL_Device1.DataSource = DT_Device;
                DDL_Device1.DataTextField = "CPDeviceDesc";
                DDL_Device1.DataValueField = "CPDeviceID";
                DDL_Device1.DataBind();
            }
            else
            { DDL_Device1.DataSource = null; DDL_Device1.DataBind(); }

            //========== Insert Items DropDownList in Dongle ===========//
            DDL_Device1.Items.Insert(0, new ListItem("--- Select Device ---", "0"));
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void FillCompany2(object sender, EventArgs e)
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
            DDL_Company2.Items.Clear();

            //========== Fill DDL ==========//
            if (DT_Company != null && DT_Company.Rows.Count > 0)
            {
                DDL_Company2.DataSource = DT_Company;
                DDL_Company2.DataTextField = "CompanyName";
                DDL_Company2.DataValueField = "CompanyID";
                DDL_Company2.DataBind();
            }
            else
            { DDL_Company2.DataSource = null; DDL_Company2.DataBind(); }

            //========== Insert Items DropDownList in Company ===========//
            DDL_Company2.Items.Insert(0, new ListItem("--- Select Company ---", "0"));
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void FillBranch2(object sender, EventArgs e)
    {
        try
        {
            //========== Declare the Object of the Branch ===========//
            UserBranchData objUserBranch = new UserBranchData();
            objUserBranch.TaskeroUserID = Fuji.Common.Sessions.UserID;
            objUserBranch.CompanyID = Convert.ToInt32(DDL_Company2.SelectedValue);

            //==========  Get Data Table and Bind ==========//
            DataTable DT_Branch = new DataTable();
            DT_Branch = objUserBranch.uspGetBranchOfCompanyByUserID(Fuji.Common.Sessions.RoleID);

            //========== Clearing all the values ==========//
            DDL_Branch2.Items.Clear();

            //========== Fill DDL ==========//
            if (DT_Branch != null && DT_Branch.Rows.Count > 0)
            {
                DDL_Branch2.DataSource = DT_Branch;
                DDL_Branch2.DataTextField = "BranchName";
                DDL_Branch2.DataValueField = "BranchID";
                DDL_Branch2.DataBind();
            }
            else
            { DDL_Branch2.DataSource = null; DDL_Branch2.DataBind(); }

            //========== Insert Items DropDownList in Branch ===========//
            DDL_Branch2.Items.Insert(0, new ListItem("--- All ---", "0"));
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void FillDongle2(object sender, EventArgs e)
    {
        //========== Clearing all the values ==========//
        DDL_DongleNo2.Items.Clear();

        try
        {
            //========== Declare the Object of the Dongle ===========//
            UserDongleData objUserDongle = new UserDongleData();

            //==========  Get Data Table and Fill ==========//        
            DataTable DT_Dongle = new DataTable();
            objUserDongle.TaskeroUserID = Fuji.Common.Sessions.UserID;
            objUserDongle.UserDongleID = Convert.ToInt32(DDL_Branch2.SelectedValue);
            DT_Dongle = objUserDongle.uspGetDongleByUserIDAndBranchID(Fuji.Common.Sessions.RoleID);

            //========== Fill DDL ==========//
            if (DT_Dongle != null && DT_Dongle.Rows.Count > 0)
            {
                DDL_DongleNo2.DataSource = DT_Dongle;
                DDL_DongleNo2.DataTextField = "DongleSerial";//CPSerialNum
                DDL_DongleNo2.DataValueField = "CPDongleID";
                DDL_DongleNo2.DataBind();
            }
            else
            { DDL_DongleNo2.DataSource = null; DDL_DongleNo2.DataBind(); }

            //========== Insert Items DropDownList in Dongle ===========//
            DDL_DongleNo2.Items.Insert(0, new ListItem("--- All ---", "0"));
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void FillDevice2(object sender, EventArgs e)
    {
        //========== Clearing all the values ==========//
        DDL_Device2.Items.Clear();

        try
        {
            //========== Declare the Object of the Dongle ===========//
            CPDeviceData objCPDevice = new CPDeviceData();

            //==========  Get Data Table and Fill ==========//        
            DataTable DT_Device = new DataTable();
            objCPDevice.CPDongleID = Convert.ToInt32(DDL_DongleNo2.SelectedValue);
            DT_Device = objCPDevice.uspGetDeviceByDongleIDColors();

            //========== Fill DDL ==========//
            if (DT_Device != null && DT_Device.Rows.Count > 0)
            {
                DDL_Device2.DataSource = DT_Device;
                DDL_Device2.DataTextField = "CPDeviceDesc";
                DDL_Device2.DataValueField = "CPDeviceID";
                DDL_Device2.DataBind();
            }
            else
            { DDL_Device2.DataSource = null; DDL_Device2.DataBind(); }

            //========== Insert Items DropDownList in Dongle ===========//
            DDL_Device2.Items.Insert(0, new ListItem("--- All ---", "0"));
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void FillDates(object sender, EventArgs e)
    {
        try
        {
            //========== If No device selected clear ==========//
            if (DDL_Device1.SelectedIndex == 0)
            { TXT_ToDate.Text = ""; TXT_FromDate.Text = ""; }

            //========== Declarations ==========//
            SqlCommand Cmd = new SqlCommand();

            //========== Execute Query ==========//
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandText = "Select CONVERT( VARCHAR, MIN(CreatedDT), 101 ) AS MINDATE ,  CONVERT( VARCHAR, MAX(CreatedDT), 101 ) AS MAXDATE from CPMeasurement WHERE CPDeviceID = " + DDL_Device1.SelectedValue;
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
            DDL_Company1.Style.Add("width", "100%");
            DDL_Branch1.Style.Add("width", "100%");
            DDL_DongleNo1.Style.Add("width", "100%");
            DDL_Device1.Style.Add("width", "100%");

            DDL_Company2.Style.Add("width", "100%");
            DDL_Branch2.Style.Add("width", "100%");
            DDL_DongleNo2.Style.Add("width", "100%");
            DDL_Device2.Style.Add("width", "100%");

            DDL_OPT1_Field.Style.Add("width", "100%");
            DDL_OPT1_Operator.Style.Add("width", "100%");
            TXT_OPT1_KeyWords.Style.Add("width", "80%");

            DDL_OPT2_Field.Style.Add("width", "100%");
            DDL_OPT2_Operator.Style.Add("width", "100%");
            TXT_OPT2_KeyWords.Style.Add("width", "80%");

            DDL_OPT3_Field.Style.Add("width", "100%");
            DDL_OPT3_Operator.Style.Add("width", "100%");
            TXT_OPT3_KeyWords.Style.Add("width", "80%");
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Fill Form
    private void GetAllGuids(DropDownList DDL_Device, System.Web.UI.WebControls.Literal LTRL_CPMeasurementGUID)
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
                Cmd.CommandText += "AND ( PMD.CreatedDT BETWEEN  '" + TXT_FromDate.Text + "' and '" + Convert.ToDateTime(TXT_ToDate.Text).AddDays(1).ToShortDateString() + "') ";
                Cmd.CommandText += "AND ISNULL( PMD.IsUsed , 1 ) = 1 ";
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
            LTRL_CPMeasurementGUID.Text = "";
            for (int i = 0; i < DT.Rows.Count; i = i + 1)
            {
                if (i == 0)
                { LTRL_CPMeasurementGUID.Text = "'" + DT.Rows[i]["CPMeasurementGUID"].ToString() + "'"; }
                else
                { LTRL_CPMeasurementGUID.Text += ",'" + DT.Rows[i]["CPMeasurementGUID"].ToString() + "'"; }
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
            //========== Fill Errors ==========//
            NoOfReads(LTRL_CPMeasurementGUID1, LTRL_NoOfReads1, LTRL_TotalErrors1, LTRL_ErrorRate1);
            NoOfReads(LTRL_CPMeasurementGUID2, LTRL_NoOfReads2, LTRL_TotalErrors2, LTRL_ErrorRate2);

            //========== FILL Measurement ==========//
            FillMeasurementID(LTRL_CPMeasurementGUID1, LTRL_MeasurementID1);
            FillMeasurementID(LTRL_CPMeasurementGUID2, LTRL_MeasurementID2);

            //========== FILL Maximum Delta E Ave ==========//
            FillMaxDeltaE(LTRL_CPMeasurementGUID1, CHRT_MaxDeltaE1);
            FillMaxDeltaE(LTRL_CPMeasurementGUID2, CHRT_MaxDeltaE2);

            //========== FILL Density ==========//
            FillDensity(LTRL_CPMeasurementGUID1, CHRT_Density1);
            FillDensity(LTRL_CPMeasurementGUID2, CHRT_Density2);

            //========== Dot Gain ==========//            
            FillDotgain(LTRL_CPMeasurementGUID1, CHRT_DotGain1);
            FillDotgain(LTRL_CPMeasurementGUID2, CHRT_DotGain2);

            //========== Paper White ==========//
            FillPaperWhite(LTRL_CPMeasurementGUID1, CHRT_PaperWhite1);
            FillPaperWhite(LTRL_CPMeasurementGUID2, CHRT_PaperWhite2);

            //========== Temp Density ==========//
            FillTempHumidity(DDL_Device1, CHRT_TempHumidity1);
            FillTempHumidity(DDL_Device2, CHRT_TempHumidity2);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void NoOfReads(System.Web.UI.WebControls.Literal LTRL_CPMeasurementGUID, System.Web.UI.WebControls.Literal LTRL_NoOfReads, System.Web.UI.WebControls.Literal LTRL_TotalErrors, System.Web.UI.WebControls.Literal LTRL_ErrorRate)
    {
        try
        {
            //========== Declare DataTable ==========//
            DataTable DT = new DataTable();

            if (LTRL_CPMeasurementGUID.Text != "")
            {
                //========== Query ==========//
                SqlCommand Cmd = new SqlCommand();

                Cmd.CommandText = "SELECT ";
                Cmd.CommandText += " COUNT(PMD.CPMeasurementGUID) as TotalReads ";
                Cmd.CommandText += ", SUM(CASE PMD.STATUS WHEN 0 THEN 1 ELSE 0 END) AS TotalErrors ";
                Cmd.CommandText += ", ( CONVERT( DECIMAL(18,2) , SUM(CASE PMD.STATUS WHEN 0 THEN 1 ELSE 0 END) ) / COUNT(PMD.CPMeasurementGUID) ) * CONVERT( DECIMAL(18,2) , 100 ) AS ErrorRate ";
                //Cmd.CommandText += ", ( CONVERT( DECIMAL(18,2) , SUM(CASE PMD.STATUS WHEN 1 THEN 1 ELSE 0 END) ) / COUNT(PMD.CPMeasurementGUID) ) * CONVERT( DECIMAL(18,2) , 100 ) AS Proof_Caution ";
                //Cmd.CommandText += ", ( CONVERT( DECIMAL(18,2) , SUM(CASE PMD.STATUS WHEN 2 THEN 1 ELSE 0 END) ) / COUNT(PMD.CPMeasurementGUID) ) * CONVERT( DECIMAL(18,2) , 100 ) AS Proof_Passed ";
                Cmd.CommandText += "FROM CPMeasurement PMD  ";
                Cmd.CommandText += "WHERE PMD.CPMeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ")";

                Cmd.CommandType = CommandType.Text;
                DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);
            }

            //========== Show values ==========//
            if (DT.Rows.Count > 0)
            {
                LTRL_NoOfReads.Text = (DT.Rows[0]["TotalReads"]).ToString();
                LTRL_TotalErrors.Text = (DT.Rows[0]["TotalErrors"]).ToString();
                LTRL_ErrorRate.Text = Convert.ToDecimal(DT.Rows[0]["ErrorRate"]).ToString("N2") + "%";
            }
            else
            {
                LTRL_NoOfReads.Text = "0";
                LTRL_TotalErrors.Text = "0";
                LTRL_ErrorRate.Text = "00.00%";
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillMeasurementID(System.Web.UI.WebControls.Literal LTRL_CPMeasurementGUID, System.Web.UI.WebControls.Literal LTRL_MeasurementID)
    {
        try
        {
            //========== Declare DataTable ==========//
            DataTable DT = new DataTable();

            if (LTRL_CPMeasurementGUID.Text != "")
            {
                //========== Get Data Table ==========//
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandText = "SELECT ISNULL( MIN(PMD.CPMeasurementID) , 0 ) AS Min_MID , ISNULL( MAX(PMD.CPMeasurementID) , 0 ) AS Max_MID  ";
                Cmd.CommandText += "FROM CPMeasurement PMD ";
                Cmd.CommandText += "WHERE  PMD.CPMeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";
                Cmd.CommandType = CommandType.Text;
                DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);
            }

            if (DT.Rows.Count > 0)
            { LTRL_MeasurementID.Text = DT.Rows[0]["Min_MID"].ToString() + " - " + DT.Rows[0]["Max_MID"].ToString(); }
            else
            { LTRL_MeasurementID.Text = ""; }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillMaxDeltaE(System.Web.UI.WebControls.Literal LTRL_CPMeasurementGUID, Chart _Chart)
    {
        try
        {
            //========== Declare DataTable ==========//
            DataTable DT = new DataTable();

            if (LTRL_CPMeasurementGUID.Text != "")
            {
                //========== Get Data Table ==========//
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandText = "SELECT MAX(PMD.CREATEDDT) AS CREATEDDT , ISNULL( CONVERT( Decimal(18,2) , AVG(DE) ) , 0 ) AS AVG_DE , ISNULL( MAX(DE) , 0 ) AS MAX_DE ";
                Cmd.CommandText += "FROM CPMeasurement PMD ";
                Cmd.CommandText += "Inner Join CPPatchDataLab PPD ON PMD.CPMeasurementGUID = PPD.MeasurementGUID ";
                Cmd.CommandText += "WHERE PMD.CPMeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";
                Cmd.CommandText += "GROUP BY PMD.CPMeasurementGUID ";
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
                objSeries1.Name = "Ave. Delta E";
                objSeries1.Type = SeriesChartType.Spline;
                objSeries1.XValueType = ChartValueTypes.Date;
                objSeries1.YValueType = ChartValueTypes.Double;
                objSeries1.ValueMemberX = "CreatedDT";
                objSeries1.ValueMembersY = "AVG_DE";
                objSeries1.Color = System.Drawing.Color.Green;
                _Chart.Series.Add(objSeries1);

                Series objSeries2 = new Series();
                objSeries2.Name = "Max Delta E";
                objSeries2.Type = SeriesChartType.Spline;
                objSeries2.XValueType = ChartValueTypes.Date;
                objSeries2.YValueType = ChartValueTypes.Double;
                objSeries2.ValueMemberX = "CreatedDT";
                objSeries2.ValueMembersY = "Max_DE";
                objSeries2.Color = System.Drawing.Color.Red;
                _Chart.Series.Add(objSeries2);
                #endregion


                //========== Show it in the chart ==========//
                for (int i = 0; i < DT.Rows.Count; i = i + 1)
                {
                    _Chart.Series[0].Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), DT.Rows[i]["AVG_DE"].ToString());
                    _Chart.Series[0].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + " \nAve. Delta E : " + DT.Rows[i]["AVG_DE"].ToString() + "";

                    _Chart.Series[1].Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), (DT.Rows[i]["Max_DE"].ToString()));
                    _Chart.Series[1].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + " \nMax Delta E : " + DT.Rows[i]["MAX_DE"].ToString() + "";
                }

                string FileName = "UploadFiles/temp/" + Sessions.UserID + "_" + Path.GetFileName(_Chart.ImageUrl) + "." + _Chart.ImageType;
                _Chart.Save(Server.MapPath(FileName));

                objSeries1.MarkerStyle = MarkerStyle.Triangle;
                objSeries2.MarkerStyle = MarkerStyle.Triangle;

                #region Give Scrolling Effect
                _Chart.AJAXZoomEnabled = true;

                //// scrolling and zooming will force keeping of series data between callbacks.
                _Chart.ChartAreas["Default"].CursorX.UserEnabled = true;

                //// Set initial X axis zooming                        
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
    private void FillDensity(System.Web.UI.WebControls.Literal LTRL_CPMeasurementGUID, Chart _Chart)
    {
        try
        {
            //========== Declare DataTable ==========//
            DataSet DSet = new DataSet();

            if (LTRL_CPMeasurementGUID.Text != "")
            {
                //========== Get Data Table ==========//
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandText = "";

                #region Cyan
                Cmd.CommandText += "SELECT MAX(PMD.CREATEDDT) AS CREATEDDT , ISNULL(MAX(PPD.DENSITY),0) AS MAX_DENSITY ";
                Cmd.CommandText += "FROM CPMeasurement PMD ";
                Cmd.CommandText += "Inner Join CPPatchDataLab PPD ON PPD.MEASUREMENTGUID = PMD.CPMEASUREMENTGUID ";
                Cmd.CommandText += "WHERE PMD.CPMEASUREMENTGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";
                Cmd.CommandText += "AND (PPD.CMYK_C = 100 AND PPD.CMYK_M = 0 AND PPD.CMYK_Y = 0 AND PPD.CMYK_K = 0) ";
                Cmd.CommandText += "GROUP BY PMD.CPMEASUREMENTGUID ";
                Cmd.CommandText += "ORDER BY CREATEDDT ";
                #endregion

                #region Magenta
                Cmd.CommandText += "SELECT MAX(PMD.CREATEDDT) AS CREATEDDT , ISNULL(MAX(PPD.DENSITY),0) AS MAX_DENSITY ";
                Cmd.CommandText += "FROM CPMeasurement PMD ";
                Cmd.CommandText += "Inner Join CPPatchDataLab PPD ON PPD.MEASUREMENTGUID = PMD.CPMEASUREMENTGUID ";
                Cmd.CommandText += "WHERE PMD.CPMEASUREMENTGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";
                Cmd.CommandText += "AND (PPD.CMYK_C = 0 AND PPD.CMYK_M = 100 AND PPD.CMYK_Y = 0 AND PPD.CMYK_K = 0) ";
                Cmd.CommandText += "GROUP BY PMD.CPMEASUREMENTGUID ";
                Cmd.CommandText += "ORDER BY CREATEDDT ";
                #endregion

                #region Yellow
                Cmd.CommandText += "SELECT MAX(PMD.CREATEDDT) AS CREATEDDT , ISNULL(MAX(PPD.DENSITY),0) AS MAX_DENSITY ";
                Cmd.CommandText += "FROM CPMeasurement PMD ";
                Cmd.CommandText += "Inner Join CPPatchDataLab PPD ON PPD.MEASUREMENTGUID = PMD.CPMEASUREMENTGUID ";
                Cmd.CommandText += "WHERE PMD.CPMEASUREMENTGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";
                Cmd.CommandText += "AND (PPD.CMYK_C = 0 AND PPD.CMYK_M = 0 AND PPD.CMYK_Y = 100 AND PPD.CMYK_K = 0) ";
                Cmd.CommandText += "GROUP BY PMD.CPMEASUREMENTGUID ";
                Cmd.CommandText += "ORDER BY CREATEDDT ";
                #endregion

                #region Black
                Cmd.CommandText += "SELECT MAX(PMD.CREATEDDT) AS CREATEDDT , ISNULL(MAX(PPD.DENSITY),0) AS MAX_DENSITY ";
                Cmd.CommandText += "FROM CPMeasurement PMD ";
                Cmd.CommandText += "Inner Join CPPatchDataLab PPD ON PPD.MEASUREMENTGUID = PMD.CPMEASUREMENTGUID ";
                Cmd.CommandText += "WHERE PMD.CPMEASUREMENTGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";
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

                //// scrolling and zooming will force keeping of series data between callbacks.
                _Chart.ChartAreas["Default"].CursorX.UserEnabled = true;

                //// Set initial X axis zooming                        
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
    private void FillDotgain(System.Web.UI.WebControls.Literal LTRL_CPMeasurementGUID, Chart _Chart)
    {
        try
        {
            //========== Declarations ==========//
            Fuji.Business.CPPatchDataLabData objColorCharts;
            string _ColorName = "", _Created_Time = "", _CMYK = "", _DotGain = "", _Tone = "";
            DataSet DSet;
            int indPlot = 0;


            if (LTRL_CPMeasurementGUID.Text != "")
            {

                //========== Get The Dot Gain ==========//
                string[] _GUID = LTRL_CPMeasurementGUID.Text.Replace("'", "").Split(',');


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
            else
            { _Chart.Visible = false; }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillPaperWhite(System.Web.UI.WebControls.Literal LTRL_CPMeasurementGUID, Chart _Chart)
    {
        try
        {
            //========== Declare DataTable ==========//
            DataTable DT = new DataTable();

            if (LTRL_CPMeasurementGUID.Text != "")
            {
                //========== Get Data Table ==========//
                SqlCommand Cmd = new SqlCommand();

                Cmd.CommandText = "SELECT (PMD.CREATEDDT) As CreatedDT , ISNULL( ACT.PAPER , 0 ) As Paper ";
                Cmd.CommandText += "FROM CPMeasurement PMD ";
                Cmd.CommandText += "Inner Join CPMeasurement_ACT ACT ON ACT.CPMeasurementGUID = PMD.CPMeasurementGUID ";
                Cmd.CommandText += "WHERE PMD.CPMeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";
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
                    _Chart.Series[0].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + "\nPaper : " + DT.Rows[i]["Paper"].ToString() + "";
                }

                string FileName = "UploadFiles/temp/" + Sessions.UserID + "_" + Path.GetFileName(_Chart.ImageUrl) + "." + _Chart.ImageType;
                _Chart.Save(Server.MapPath(FileName));

                objSeries1.MarkerStyle = MarkerStyle.Triangle;

                #region Give Scrolling Effect
                _Chart.AJAXZoomEnabled = true;

                //// scrolling and zooming will force keeping of series data between callbacks.
                _Chart.ChartAreas["Default"].CursorX.UserEnabled = true;

                //// Set initial X axis zooming                        
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
    private void FillTempHumidity(DropDownList DDL_Device, Chart _Chart)
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
                    _Chart.Series[0].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + "\nTemp. Celcius : " + DT.Rows[i]["TempC"].ToString() + "";

                    _Chart.Series[1].Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), DT.Rows[i]["TempF"].ToString());
                    _Chart.Series[1].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + "\nTemp. Fehrenite : " + DT.Rows[i]["TempF"].ToString() + "";

                    _Chart.Series[2].Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), DT.Rows[i]["RH"].ToString());
                    _Chart.Series[2].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + "\nRel. Humidity : " + DT.Rows[i]["RH"].ToString() + "";
                }

                string FileName = "UploadFiles/temp/" + Sessions.UserID + "_" + Path.GetFileName(_Chart.ImageUrl) + "." + _Chart.ImageType;
                _Chart.Save(System.Web.HttpContext.Current.Server.MapPath(FileName));

                objSeries1.MarkerStyle = MarkerStyle.Triangle;
                objSeries2.MarkerStyle = MarkerStyle.Triangle;
                objSeries3.MarkerStyle = MarkerStyle.Triangle;

                #region Give Scrolling Effect
                _Chart.AJAXZoomEnabled = true;

                //// scrolling and zooming will force keeping of series data between callbacks.
                _Chart.ChartAreas["Default"].CursorX.UserEnabled = true;

                //// Set initial X axis zooming                        
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
        //_Chart.Series[0].Color = System.Drawing.Color.Transparent;
        //_Chart.Series[0].Points.AddXY(Convert.ToDateTime(TXT_FromDate.Text).ToShortDateString(), "8");
        //_Chart.Series[0].Points.AddXY(Convert.ToDateTime(TXT_ToDate.Text).ToShortDateString(), "8");
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

        DC_BasicInfo = new DataColumn();
        DC_BasicInfo.ColumnName = "MeasurementID_";
        DC_BasicInfo.DataType = System.Type.GetType("System.String");
        DT_BasicInfo.Columns.Add(DC_BasicInfo);

        DC_BasicInfo = new DataColumn();
        DC_BasicInfo.ColumnName = "NoOfReads_";
        DC_BasicInfo.DataType = System.Type.GetType("System.String");
        DT_BasicInfo.Columns.Add(DC_BasicInfo);

        DC_BasicInfo = new DataColumn();
        DC_BasicInfo.ColumnName = "TotalErrors_";
        DC_BasicInfo.DataType = System.Type.GetType("System.String");
        DT_BasicInfo.Columns.Add(DC_BasicInfo);

        DC_BasicInfo = new DataColumn();
        DC_BasicInfo.ColumnName = "ErrorRate_";
        DC_BasicInfo.DataType = System.Type.GetType("System.String");
        DT_BasicInfo.Columns.Add(DC_BasicInfo);

        DataRow DR_BasicInfo = DT_BasicInfo.NewRow();
        DR_BasicInfo["CompanyName_"] = LTRL_Company1.Text.Trim();
        DR_BasicInfo["Device_"] = LTRL_DeviceName1.Text.Trim();
        DR_BasicInfo["DongleNumber_"] = LTRL_DongleNumber1.Text.Trim();
        DR_BasicInfo["Location_"] = LTRL_LocationName1.Text.Trim();
        DR_BasicInfo["DateRange_"] = LTRL_DateRange.Text.Trim();
        DR_BasicInfo["MeasurementID_"] = LTRL_MeasurementID1.Text.Trim();
        DR_BasicInfo["NoOfReads_"] = LTRL_NoOfReads1.Text.Trim();
        DR_BasicInfo["TotalErrors_"] = LTRL_TotalErrors1.Text.Trim();
        DR_BasicInfo["ErrorRate_"] = LTRL_ErrorRate1.Text.Trim();
        DT_BasicInfo.Rows.Add(DR_BasicInfo);

        DR_BasicInfo = DT_BasicInfo.NewRow();
        DR_BasicInfo["CompanyName_"] = LTRL_Company2.Text.Trim();
        DR_BasicInfo["Device_"] = LTRL_DeviceName2.Text.Trim();
        DR_BasicInfo["DongleNumber_"] = LTRL_DongleNumber2.Text.Trim();
        DR_BasicInfo["Location_"] = LTRL_LocationName2.Text.Trim();
        DR_BasicInfo["DateRange_"] = LTRL_DateRange.Text.Trim();
        DR_BasicInfo["MeasurementID_"] = LTRL_MeasurementID2.Text.Trim();
        DR_BasicInfo["NoOfReads_"] = LTRL_NoOfReads2.Text.Trim();
        DR_BasicInfo["TotalErrors_"] = LTRL_TotalErrors2.Text.Trim();
        DR_BasicInfo["ErrorRate_"] = LTRL_ErrorRate2.Text.Trim();
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

        DR_SearchCriteria["SearchField_"] = LTRL_Field1.Text.Trim();
        DR_SearchCriteria["Operator_"] = LTRL_Operator1.Text.Trim();
        DR_SearchCriteria["Criteria_"] = LTRL_Criteria1.Text.Trim();

        DT_SearchCriteria.Rows.Add(DR_SearchCriteria);

        DR_SearchCriteria = DT_SearchCriteria.NewRow();

        DR_SearchCriteria["SearchField_"] = LTRL_Field2.Text.Trim();
        DR_SearchCriteria["Operator_"] = LTRL_Operator2.Text.Trim();
        DR_SearchCriteria["Criteria_"] = LTRL_Criteria2.Text.Trim();

        DT_SearchCriteria.Rows.Add(DR_SearchCriteria);

        DR_SearchCriteria = DT_SearchCriteria.NewRow();

        DR_SearchCriteria["SearchField_"] = LTRL_Field3.Text.Trim();
        DR_SearchCriteria["Operator_"] = LTRL_Operator3.Text.Trim();
        DR_SearchCriteria["Criteria_"] = LTRL_Criteria3.Text.Trim();

        DT_SearchCriteria.Rows.Add(DR_SearchCriteria);

        ViewState["DT_SearchCriteria"] = DT_SearchCriteria;

        #endregion

        #region Measurement
        DataTable DT_Measurement = new DataTable("Measurement");

        DataColumn DC_Measurement = new DataColumn();
        DC_Measurement.ColumnName = "ErrorRate";
        DC_Measurement.DataType = System.Type.GetType("System.String");
        DT_Measurement.Columns.Add(DC_Measurement);

        DC_Measurement = new DataColumn();
        DC_Measurement.ColumnName = "NoOfReads";
        DC_Measurement.DataType = System.Type.GetType("System.String");
        DT_Measurement.Columns.Add(DC_Measurement);

        DC_Measurement = new DataColumn();
        DC_Measurement.ColumnName = "TotalErrors";
        DC_Measurement.DataType = System.Type.GetType("System.String");
        DT_Measurement.Columns.Add(DC_Measurement);

        DataRow DR_Measurement = DT_Measurement.NewRow();
        DR_Measurement["ErrorRate"] = LTRL_ErrorRate1.Text.Trim();
        DR_Measurement["NoOfReads"] = LTRL_NoOfReads1.Text.Trim();
        DR_Measurement["TotalErrors"] = LTRL_TotalErrors1.Text.Trim();
        DT_Measurement.Rows.Add(DR_Measurement);

        DR_Measurement = DT_Measurement.NewRow();
        DR_Measurement["ErrorRate"] = LTRL_ErrorRate2.Text.Trim();
        DR_Measurement["NoOfReads"] = LTRL_NoOfReads2.Text.Trim();
        DR_Measurement["TotalErrors"] = LTRL_TotalErrors2.Text.Trim();
        DT_Measurement.Rows.Add(DR_Measurement);

        ViewState["DT_Measurement"] = DT_Measurement;
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

        if (LTRL_CPMeasurementGUID.Text != "")
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();

            Cmd.CommandText = "";

            Cmd.CommandText += "SELECT ISNULL(CDOt.[DotGain_C50%],0) as Cyan , ISNULL( CDOt.[DotGain_M50%] , 0 ) AS Magenta , ISNULL( CDOt.[DotGain_Y50%] , 0 ) AS Yellow , ISNULL( CDOt.[DotGain_K50%] , 0 ) as Black , PMD.CreatedDT ";
            Cmd.CommandText += "FROM CPMeasurement_DotGain AS CDOt ";
            Cmd.CommandText += "INNER JOIN CPMeasurement AS PMD ON CDOt.CPMeasurementGUID = PMD.CPMeasurementGUID  ";
            Cmd.CommandText += "AND PMD.CPMeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";
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

            //========== Show it in the chart ==========//
            for (int i = 0; i < DT.Rows.Count; i = i + 1)
            {
                _Chart.Series[0].Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), DT.Rows[i]["Cyan"].ToString());
                _Chart.Series[0].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + "\nDot Gain Cyan : " + DT.Rows[i]["Cyan"].ToString() + "";

                _Chart.Series[1].Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), DT.Rows[i]["Magenta"].ToString());
                _Chart.Series[1].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + "\nDot Gain Magenta : " + DT.Rows[i]["Magenta"].ToString() + "";

                _Chart.Series[2].Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), DT.Rows[i]["Yellow"].ToString());
                _Chart.Series[2].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + "\nDot Gain Yellow : " + DT.Rows[i]["Yellow"].ToString() + "";

                _Chart.Series[3].Points.AddXY(DT.Rows[i]["CreatedDT"].ToString(), DT.Rows[i]["Black"].ToString());
                _Chart.Series[3].Points[i].ToolTip = "Date : " + Convert.ToDateTime(DT.Rows[i]["CreatedDT"]).ToString() + "\nDot Gain Black : " + DT.Rows[i]["Black"].ToString() + "";
            }

            string FileName = "UploadFiles/temp/" + Sessions.UserID + "_" + Path.GetFileName(_Chart.ImageUrl) + "." + _Chart.ImageType;
            _Chart.Save(Server.MapPath(FileName));

            objSeries1.MarkerStyle = MarkerStyle.Triangle;
            objSeries2.MarkerStyle = MarkerStyle.Triangle;
            objSeries3.MarkerStyle = MarkerStyle.Triangle;
            objSeries4.MarkerStyle = MarkerStyle.Triangle;

            #region Give Scrolling Effect
            _Chart.AJAXZoomEnabled = true;

            //// scrolling and zooming will force keeping of series data between callbacks.
            _Chart.ChartAreas["Default"].CursorX.UserEnabled = true;

            //// Set initial X axis zooming                        
            _Chart.ChartAreas["Default"].AxisY.View.MinSize = 10.0;
            _Chart.ChartAreas["Default"].AxisX.View.Position = 0.0;
            _Chart.ChartAreas["Default"].AxisX.View.Size = 50.0;
            #endregion
        }
 
 */