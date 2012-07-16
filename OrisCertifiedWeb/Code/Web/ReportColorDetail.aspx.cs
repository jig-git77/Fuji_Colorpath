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
using Formulas;
#endregion

public partial class ReportColorDetail : PageBase
{
    #region Page Load and Events
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
            //========== Clear Main Things ==========//
            ViewState.Clear();
            LTRL_CPMeasurementGUID.Text = "";

            //========== Validations ==========//
            if (DDL_Device.SelectedIndex == 0)
            { LblError.Text = "Please select a device!!!"; return; }
            if (DDL_PrintTarget.SelectedIndex == 0)
            { LblError.Text = "Please Select Print Target!!"; return; }
            if (TXT_FromDate.Text == "")
            { LblError.Text = "From Date is required!!!"; return; }
            if (TXT_ToDate.Text == "")
            { LblError.Text = "To Date is required!!!"; return; }

            //========== Filling Label ==========//
            lblReportTitle.Text = "Color Detail Report";
            TDReportTitle.Attributes.Add("class", "");
            LTRL_DateRange.Text = TXT_FromDate.Text + " - " + TXT_ToDate.Text;
            LTRL_Branch.Text = Convert.ToString(DDL_Branch.SelectedItem);
            LTRL_CompanyName.Text = Convert.ToString(DDL_Company.SelectedItem);
            LTRL_DeviceName.Text = Convert.ToString(DDL_Device.SelectedItem);
            if (DDL_PrintTarget.SelectedIndex != 0)
            { LTRL_DeviceName.Text += " - " + DDL_PrintTarget.SelectedItem.Text; }
            //LTRL_DeviceName.Text += " Report";

            LTRL_Field1.Text = DDL_OPT1_Field.SelectedItem.Text;
            LTRL_Field2.Text = DDL_OPT2_Field.SelectedItem.Text;
            LTRL_Field3.Text = DDL_OPT3_Field.SelectedItem.Text;

            LTRL_Operator1.Text = DDL_OPT1_Operator.SelectedItem.Text;
            LTRL_Operator2.Text = DDL_OPT2_Operator.SelectedItem.Text;
            LTRL_Operator3.Text = DDL_OPT3_Operator.SelectedItem.Text;

            LTRL_Criteria1.Text = TXT_OPT1_KeyWords.Text;
            LTRL_Criteria2.Text = TXT_OPT2_KeyWords.Text;
            LTRL_Criteria3.Text = TXT_OPT3_KeyWords.Text;

            //========== Load Data ==========//
            GetAllGuids();

            if (LTRL_CPMeasurementGUID.Text == "")
            {
                LblError.Text = "No measurements found for the search criteria selected!";
            }
            else
            {
                //========== Hiding Search / displaying Result ==========//
                PNL_Search.Style.Add("display", "none");
                PNL_Result.Style.Add("display", "");

                //========== Shows the data on the screen ==========//
                LoadData();

                //========== Updates the data for PDF ==========//
                TempTable();
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
            lblReportTitle.Text = "Color Detail Report(Search Criteria)";
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void BTN_MakePDF_Click(object sender, EventArgs e)
    {
        try
        {
            Fuji.Common.ReportPDF objReportPDF = new Fuji.Common.ReportPDF();
            objReportPDF.ReportType = (int)Fuji.Common.Enums.enumReportType.ColorDetailReport;
            objReportPDF.PDFPath = Server.MapPath("UploadFiles/Report_PDF/");
            objReportPDF.PDF_NAME = Sessions.UserID + "_ColorDetailReport.PDF";

            if (ViewState["DT_BasicInfo"] != null)
            { 
                objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_BasicInfo"]); 
            }
            if (ViewState["DT_SearchCriteria"] != null)
            { 
                objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_SearchCriteria"]); 
            }
            if (ViewState["DT_Consumables"] != null)
            { 
                objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_Consumables"]); 
            }
            if (ViewState["DT_WorstPatches"] != null)
            { 
                objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_WorstPatches"]); 
            }
            if (ViewState["DT_Environmental"] != null)
            { 
                objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_Environmental"]); 
            }
            if (ViewState["DT_SkuInfo"] != null)
            { 
                objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_SkuInfo"]); 
            }
            if (ViewState["DT_InkSkuInfo"] != null)
            { 
                objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_InkSkuInfo"]); 
            }
            if (ViewState["DT_BatchInfo"] != null)
            { 
                objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_BatchInfo"]); 
            }
            if (ViewState["DT_InkBatchInfo"] != null)
            { 
                objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_InkBatchInfo"]); 
            }
            if (ViewState["DT_PatchDetails"] != null)
            { 
                objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_PatchDetails"]); 
            }

            objReportPDF.MakePDF();

            Fuji.Common.CommonFunctions.DownlaodFile(objReportPDF.PDFPath, objReportPDF.PDF_NAME, objReportPDF.PDF_NAME);

        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void Lnk_Sample_Click(object sender, EventArgs e)
    {
        try
        {
            ClearImage();
            //LTRL_SortExpression1.Text = "Substring(Sample_Name,1,1) , Convert( int , Substring( Sample_Name , 2 , Len(Sample_Name) - 1 ) )";
            LTRL_SortExpression1.Text = "Sample_ID";
            if (LTRL_SortOrder1.Text == "ASC")
            { LTRL_SortOrder1.Text = "DESC"; TD_Sample.Attributes.Add("Class", "gridHeaderSortDESC"); }
            else { LTRL_SortOrder1.Text = "ASC"; TD_Sample.Attributes.Add("Class", "gridHeaderSortASC"); }

            FillPatches();
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void Lnk_Cyan_Click(object sender, EventArgs e)
    {
        try
        {
            ClearImage();
            LTRL_SortExpression1.Text = "CMYK_C";
            if (LTRL_SortOrder1.Text == "ASC")
            { LTRL_SortOrder1.Text = "DESC"; TD_C.Attributes.Add("Class", "gridHeaderSortDESC"); }
            else { LTRL_SortOrder1.Text = "ASC"; TD_C.Attributes.Add("Class", "gridHeaderSortASC"); }
            FillPatches();
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void Lnk_Margenta_Click(object sender, EventArgs e)
    {
        try
        {
            ClearImage();
            LTRL_SortExpression1.Text = "CMYK_M";
            if (LTRL_SortOrder1.Text == "ASC")
            { LTRL_SortOrder1.Text = "DESC"; TD_M.Attributes.Add("Class", "gridHeaderSortDESC"); }
            else { LTRL_SortOrder1.Text = "ASC"; TD_M.Attributes.Add("Class", "gridHeaderSortASC"); }
            FillPatches();
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void Lnk_Yellow_Click(object sender, EventArgs e)
    {
        try
        {
            ClearImage();
            LTRL_SortExpression1.Text = "CMYK_Y";
            if (LTRL_SortOrder1.Text == "ASC")
            { LTRL_SortOrder1.Text = "DESC"; TD_Y.Attributes.Add("Class", "gridHeaderSortDESC"); }
            else { LTRL_SortOrder1.Text = "ASC"; TD_Y.Attributes.Add("Class", "gridHeaderSortASC"); }
            FillPatches();
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void Lnk_Black_Click(object sender, EventArgs e)
    {
        try
        {
            ClearImage();
            LTRL_SortExpression1.Text = "CMYK_K";
            if (LTRL_SortOrder1.Text == "ASC")
            { LTRL_SortOrder1.Text = "DESC"; Td_B.Attributes.Add("Class", "gridHeaderSortDESC"); }
            else { LTRL_SortOrder1.Text = "ASC"; Td_B.Attributes.Add("Class", "gridHeaderSortASC"); }
            FillPatches();
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void Lnk_Avg_De_Click(object sender, EventArgs e)
    {
        try
        {
            ClearImage();
            LTRL_SortExpression1.Text = "AVG_DE";
            if (LTRL_SortOrder1.Text == "ASC")
            { LTRL_SortOrder1.Text = "DESC"; TD_Avg_De.Attributes.Add("Class", "gridHeaderSortDESC"); }
            else { LTRL_SortOrder1.Text = "ASC"; TD_Avg_De.Attributes.Add("Class", "gridHeaderSortASC"); }
            FillPatches();
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void Lnk_Std_Dev_Click(object sender, EventArgs e)
    {
        try
        {
            ClearImage();
            LTRL_SortExpression1.Text = "STDEV_DE";
            if (LTRL_SortOrder1.Text == "ASC")
            { LTRL_SortOrder1.Text = "DESC"; TD_Std.Attributes.Add("Class", "gridHeaderSortDESC"); }
            else { LTRL_SortOrder1.Text = "ASC"; TD_Std.Attributes.Add("Class", "gridHeaderSortASC"); }
            FillPatches();
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void DGPatches_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView DRV = (DataRowView)e.Item.DataItem;
                System.Web.UI.WebControls.Label LBL = (System.Web.UI.WebControls.Label)e.Item.FindControl("LBL_Color");
                LBL.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                LBL.Style.Add("background-color", ColorCalcuations.LabtoRGB(Convert.ToInt16(DRV["LAB_L1"]), Convert.ToInt16(DRV["LAB_A1"]), Convert.ToInt16(DRV["LAB_B1"])));
            }
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
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
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
    protected void FillPrintTarget(object sender, EventArgs e)
    {

        //========== Clearing all the values ==========//
        DDL_PrintTarget.Items.Clear();

        try
        {
            //========== Declare the Object of the Dongle ===========//
            CPDevicePrintTargetData objCPPrintTarget = new CPDevicePrintTargetData();

            //==========  Get Data Table and Fill ==========//        
            DataTable DT_PrintTarget = new DataTable();
            objCPPrintTarget.DeviceID = Convert.ToInt32(DDL_Device.SelectedValue );
            DT_PrintTarget = objCPPrintTarget.uspGetPrintTargetByDeviceID();

            //========== Fill DDL ==========//
            if (DT_PrintTarget != null && DT_PrintTarget.Rows.Count > 0)
            {
                DDL_PrintTarget.DataSource = DT_PrintTarget;
                DDL_PrintTarget.DataTextField = "PrintTarget";
                DDL_PrintTarget.DataValueField = "CPDevicePrintTargetId";
                DDL_PrintTarget.DataBind();
            }
            else
            { DDL_PrintTarget.DataSource = null; DDL_PrintTarget.DataBind(); }

            //========== Insert Items DropDownList in Dongle ===========//
            DDL_PrintTarget.Items.Insert(0, new ListItem("--- Select Print Target ---", "0"));
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
            DDL_PrintTarget.Style.Add("WIDTH", "100%");

            DDL_OPT1_Field.Style.Add("width", "100%");
            DDL_OPT1_Operator.Style.Add("width", "100%");
            TXT_OPT1_KeyWords.Style.Add("width", "80%");

            DDL_OPT2_Field.Style.Add("width", "100%");
            DDL_OPT2_Operator.Style.Add("width", "100%");
            TXT_OPT2_KeyWords.Style.Add("width", "80%");

            DDL_OPT3_Field.Style.Add("width", "100%");
            DDL_OPT3_Operator.Style.Add("width", "100%");
            TXT_OPT3_KeyWords.Style.Add("width", "80%");

            DGConSkuInfo.Style.Add("width", "100%");
            DGConSkuInfo.Attributes.Add("cellspacing", "1px");
            DGConSkuInfo.Attributes.Add("cellspadding", "0px");
            DGConSkuInfo.Attributes.Add("border", "0px");

            DGInkSku.Style.Add("width", "100%");
            DGInkSku.Attributes.Add("cellspacing", "1px");
            DGInkSku.Attributes.Add("cellspadding", "0px");
            DGInkSku.Attributes.Add("border", "0px");

            DGConBatchInfo.Style.Add("width", "100%");
            DGConBatchInfo.Attributes.Add("cellspacing", "1px");
            DGConBatchInfo.Attributes.Add("cellspadding", "0px");
            DGConBatchInfo.Attributes.Add("border", "0px");

            DGInkBatch.Style.Add("width", "100%");
            DGInkBatch.Attributes.Add("cellspacing", "1px");
            DGInkBatch.Attributes.Add("cellspadding", "0px");
            DGInkBatch.Attributes.Add("border", "0px");


            DGPatches.Style.Add("width", "97%");
            DGPatches.Attributes.Add("cellspacing", "1px");
            DGPatches.Attributes.Add("cellspadding", "0px");
            DGPatches.Attributes.Add("border", "0px");
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Methods and Function
    private void GetAllGuids()
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

            Cmd.CommandText = "SELECT ";
            Cmd.CommandText += "DISTINCT(PMD.CPMeasurementGUID) as CPMeasurementGUID ";
            Cmd.CommandText += "FROM  CPMeasurement PMD ";
            Cmd.CommandText += "INNER JOIN CPMeasurement_PT AS CPM_PT ON PMD.CPMeasurementGUID = CPM_PT.CPMeasurementGUID ";
            Cmd.CommandText += "INNER JOIN CPDevicePrintTarget AS CPD_PRINT ON CPM_PT.CPDevicePrintTargetID = CPD_PRINT.CPDevicePrintTargetID ";
            Cmd.CommandText += JoinClause;
            Cmd.CommandText += "WHERE (CPD_PRINT.CPDevicePrintTargetID = " + DDL_PrintTarget.SelectedValue + ") ";
            Cmd.CommandText += " AND ( PMD.CreatedDT BETWEEN  '" + TXT_FromDate.Text + "' and '" + Convert.ToDateTime(TXT_ToDate.Text).AddDays(1).ToShortDateString() + "') ";
            Cmd.CommandText += " AND ISNULL( PMD.IsUsed , 1 ) = 1 ";
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
    private void LoadData()
    {
        try
        {
            AverageDeltaE();
            GetConsumableSkuInfo();
            GetConsumableInkSkuInfo();
            GetDeviceTRHData();
            GetDeviceConsumableStability();
            GetConsumableBatchInfo();
            GetInkBatchInfo();
            FillPatches();
            FillLineChart();

            //========== Show Hide The Tr ==========//
            if (DGConSkuInfo.Items.Count == 0) { DGConSkuInfo.Visible = false; } else { DGConSkuInfo.Visible = true; }
            if (DGInkSku.Items.Count == 0) { DGInkSku.Visible = false; } else { DGInkSku.Visible = true; }
            if (DGConSkuInfo.Items.Count == 0 && DGInkSku.Items.Count == 0) { TR_ConSKUInfo.Visible = false; } else { TR_ConSKUInfo.Visible = true; }

            if (DGConBatchInfo.Items.Count == 0) { DGConBatchInfo.Visible = false; } else { DGConBatchInfo.Visible = true; }
            if (DGInkBatch.Items.Count == 0) { DGInkBatch.Visible = false; } else { DGInkBatch.Visible = true; }
            if (DGConBatchInfo.Items.Count == 0 && DGInkBatch.Items.Count == 0) { TR_ConBatchInfo.Visible = false; } else { TR_ConBatchInfo.Visible = true; }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void NoOfReads()
    {
        try
        {
            //========== Query ==========//
            SqlCommand Cmd = new SqlCommand();

            Cmd.CommandText = "SELECT ";
            Cmd.CommandText += " COUNT(PMD.CPMeasurementGUID) as TotalReads ";
            Cmd.CommandText += ", ( CONVERT( DECIMAL(18,2) , SUM(CASE PMD.STATUS WHEN 0 THEN 1 ELSE 0 END) ) / COUNT(PMD.CPMeasurementGUID) ) * CONVERT( DECIMAL(18,2) , 100 ) AS ErrorRate ";
            Cmd.CommandText += ", ( CONVERT( DECIMAL(18,2) , SUM(CASE PMD.STATUS WHEN 1 THEN 1 ELSE 0 END) ) / COUNT(PMD.CPMeasurementGUID) ) * CONVERT( DECIMAL(18,2) , 100 ) AS Proof_Caution ";
            Cmd.CommandText += ", ( CONVERT( DECIMAL(18,2) , SUM(CASE PMD.STATUS WHEN 2 THEN 1 ELSE 0 END) ) / COUNT(PMD.CPMeasurementGUID) ) * CONVERT( DECIMAL(18,2) , 100 ) AS Proof_Passed ";
            Cmd.CommandText += "FROM CPMeasurement PMD  ";
            Cmd.CommandText += "WHERE PMD.CPMeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ")";

            Cmd.CommandType = CommandType.Text;
            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);

            //========== Show values ==========//
            if (DT.Rows.Count > 0)
            {
                LTRL_NoOfReads.Text = (DT.Rows[0]["TotalReads"]).ToString();
                LTRL_AvgErrorRate.Text = Convert.ToDecimal(DT.Rows[0]["ErrorRate"]).ToString("N2");
            }

            //========== Fill Charts ==========//
            FillPieChart(DT);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void AverageDeltaE()
    {
        try
        {
            //========== Query ==========//
            SqlCommand Cmd = new SqlCommand();

            Cmd.CommandText = "SELECT ";
            Cmd.CommandText += "ISNULL( AVG(ACT.Average) , 0.00 ) as AVG_DELTA_E ";
            Cmd.CommandText += ", ISNULL( AVG(ACT.PatchdEValue) , 0.00 )  as AVG_MAX_DELTA_E ";
            Cmd.CommandText += "FROM CPMeasurement_ACT ACT ";
            Cmd.CommandText += "WHERE ACT.CPMeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ")";

            Cmd.CommandType = CommandType.Text;

            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);
            //========== Show values ==========//
            if (DT.Rows.Count > 0)
            {
                LTRL_AvgDeltaE.Text = Convert.ToDecimal(DT.Rows[0]["AVG_DELTA_E"]).ToString("N2");
                LTRL_AvgMaxDeltaE.Text = Convert.ToDecimal(DT.Rows[0]["AVG_MAX_DELTA_E"]).ToString("N2");
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void GetConsumableSkuInfo()
    {
        try
        {
            //========== Query ==========//
            SqlCommand Cmd = new SqlCommand();

            Cmd.CommandText = "SELECT ";
            Cmd.CommandText += "(DP.Substrate_SKU) AS PAPER_SKU ";
            Cmd.CommandText += ", COUNT(PMD.CPMeasurementGUID) as Measurements	";
            Cmd.CommandText += ", ( CONVERT( DECIMAL(18,2) , SUM(CASE PMD.STATUS WHEN 0 THEN 1 ELSE 0 END) ) / COUNT(PMD.CPMeasurementGUID) ) * CONVERT( DECIMAL(18,2) , 100 ) AS ErrorRate ";
            Cmd.CommandText += ", ISNULL( AVG(ACT.Average) , 0 ) AS AVG_DE ";
            Cmd.CommandText += "FROM CPMeasurement PMD ";
            Cmd.CommandText += "INNER JOIN CPMeasurement_DP DP ON PMD.CPMeasurementGUID = DP.CPMeasurementGUID ";
            Cmd.CommandText += "INNER JOIN CPMeasurement_ACT ACT ON PMD.CPMeasurementGUID = ACT.CPMeasurementGUID ";
            Cmd.CommandText += "WHERE PMD.CPMeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";
            Cmd.CommandText += "AND LTRIM(ISNULL(DP.Substrate_SKU,'')) <> '' ";
            Cmd.CommandText += "GROUP BY DP.Substrate_SKU ";

            Cmd.CommandType = CommandType.Text;

            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);

            DT.TableName = "SkuInfo";
            ViewState["DT_SkuInfo"] = DT;

            DGConSkuInfo.DataSource = DT;
            Fuji.Common.CommonFunctions.SetDataGridDefaultSettings(DGConSkuInfo);
            DGConSkuInfo.AllowPaging = false;
            DGConSkuInfo.DataBind();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void GetConsumableInkSkuInfo()
    {
        try
        {
            //========== Query ==========//
            SqlCommand Cmd = new SqlCommand();

            Cmd.CommandText = "SELECT ";
            Cmd.CommandText += "(DP.Ink_SKU) AS Ink_SKU ";
            Cmd.CommandText += ", COUNT(PMD.CPMeasurementGUID) as Measurements	";
            Cmd.CommandText += ", ( CONVERT( DECIMAL(18,2) , SUM(CASE PMD.STATUS WHEN 0 THEN 1 ELSE 0 END) ) / COUNT(PMD.CPMeasurementGUID) ) * CONVERT( DECIMAL(18,2) , 100 ) AS ErrorRate ";
            Cmd.CommandText += ", ISNULL( AVG(ACT.Average) , 0 ) AS AVG_DE ";
            Cmd.CommandText += "FROM CPMeasurement PMD ";
            Cmd.CommandText += "INNER JOIN CPMeasurement_DP DP ON PMD.CPMeasurementGUID = DP.CPMeasurementGUID ";
            Cmd.CommandText += "INNER JOIN CPMeasurement_ACT ACT ON PMD.CPMeasurementGUID = ACT.CPMeasurementGUID ";
            Cmd.CommandText += "WHERE PMD.CPMeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";
            Cmd.CommandText += "AND LTRIM(ISNULL(DP.Ink_SKU,'')) <> '' ";
            Cmd.CommandText += "GROUP BY DP.Ink_SKU ";

            Cmd.CommandType = CommandType.Text;
            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);

            DT.TableName = "InkSkuInfo";
            ViewState["DT_InkSkuInfo"] = DT;

            DGInkSku.DataSource = DT;
            Fuji.Common.CommonFunctions.SetDataGridDefaultSettings(DGInkSku);
            DGInkSku.AllowPaging = false;
            DGInkSku.DataBind();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void GetDeviceTRHData()
    {
        try
        {
            //========== Query ==========//
            SqlCommand Cmd = new SqlCommand();

            Cmd.CommandText = "SELECT ";
            Cmd.CommandText += "ISNULL( AVG(TempRHData.TempF) , 0 ) As AvgTempF ";
            Cmd.CommandText += ", ISNULL( AVG(TempRHData.RH) , 0 ) As AvgRH ";
            Cmd.CommandText += ", ISNULL( MIN(TempRHData.TempF) , 0) As MinTempF ";
            Cmd.CommandText += ", ISNULL( MIN(TempRHData.RH) , 0 ) As MinRH  ";
            Cmd.CommandText += ", ISNULL( STDEV(TempRHData.TempF) , 0) As StdevTempF  ";
            Cmd.CommandText += ", ISNULL( STDEV(TempRHData.RH) , 0) As StdevRH ";
            Cmd.CommandText += "FROM CPMeasurement As CPM ";
            Cmd.CommandText += "INNER JOIN CPMeasurement_TempRH  As TempRHData ON TempRHData.CPMeasurementGUID = CPM.CPMeasurementGUID ";
            Cmd.CommandText += "LEFT JOIN TempRHDevice_CPDevice ON TempRHDevice_CPDevice.CPDeviceID = CPM.CPDeviceID ";
            Cmd.CommandText += "LEFT JOIN TempRHDevice ON	TempRHDevice.TempRHDeviceID = TempRHDevice_CPDevice.TempRHDeviceID ";
            Cmd.CommandText += "WHERE TempRHData.CPMeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ")";
           

            Cmd.CommandType = CommandType.Text;
            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);

            //========== Show values ==========//
            if (DT.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(DT.Rows[0]["AvgTempF"])) { LTRL_TempAvg.Text = Convert.ToDecimal(DT.Rows[0]["AvgTempF"]).ToString("N2"); }
                if (!Convert.IsDBNull(DT.Rows[0]["AvgRH"])) { LTRL_RhAvg.Text = Convert.ToDecimal(DT.Rows[0]["AvgRH"]).ToString("N2"); }
                if (!Convert.IsDBNull(DT.Rows[0]["MinTempF"])) { LTRL_TempMin.Text = Convert.ToDecimal(DT.Rows[0]["MinTempF"]).ToString("N2"); }
                if (!Convert.IsDBNull(DT.Rows[0]["MinRH"])) { LTRL_RhMin.Text = Convert.ToDecimal(DT.Rows[0]["MinRH"]).ToString("N2"); }
                if (!Convert.IsDBNull(DT.Rows[0]["StdevTempF"])) { LTRL_TempStandard.Text = Convert.ToDecimal(DT.Rows[0]["StdevTempF"]).ToString("N2"); }
                if (!Convert.IsDBNull(DT.Rows[0]["StdevRH"])) { LTRL_RhStandard.Text = Convert.ToDecimal(DT.Rows[0]["StdevRH"]).ToString("N2"); }
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void GetDeviceConsumableStability()
    {
        try
        {
            //========== Get Data Table ==========//
            SqlCommand Cmd = new SqlCommand();

            Cmd.CommandText = "";

            /*===== Cyan =====*/
            Cmd.CommandText += "SELECT ";
            Cmd.CommandText += "ISNULL( AVG(PPD.DE) , 0) AS AVE_DELTA_E ";
            Cmd.CommandText += ", ISNULL( AVG( ABS(PPD.DH) ) , 0 ) AS AVE_HUE_DELTA ";
            Cmd.CommandText += ", ISNULL( AVG( ABS(PPD.DL) ) , 0 ) AS AVE_L_DELTA  ";
            Cmd.CommandText += "FROM CPPatchDataLab PPD ";
            Cmd.CommandText += "WHERE ( PPD.CMYK_C = 100 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 0 ) ";
            Cmd.CommandText += "AND PPD.MeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ")";

            Cmd.CommandText += " UNION ALL ";

            /*===== Magenta =====*/
            Cmd.CommandText += "SELECT ";
            Cmd.CommandText += "ISNULL( AVG(PPD.DE) , 0) AS AVE_DELTA_E ";
            Cmd.CommandText += ", ISNULL( AVG( ABS(PPD.DH) ) , 0 ) AS AVE_HUE_DELTA ";
            Cmd.CommandText += ", ISNULL( AVG( ABS(PPD.DL) ) , 0 ) AS AVE_L_DELTA  ";
            Cmd.CommandText += "FROM CPPatchDataLab PPD ";
            Cmd.CommandText += "WHERE ( PPD.CMYK_C = 0 AND CMYK_M = 100 AND CMYK_Y = 0 AND CMYK_K = 0 ) ";
            Cmd.CommandText += "AND PPD.MeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ")";

            Cmd.CommandText += " UNION ALL ";

            /*===== Yellow =====*/
            Cmd.CommandText += "SELECT ";
            Cmd.CommandText += "ISNULL( AVG(PPD.DE) , 0) AS AVE_DELTA_E ";
            Cmd.CommandText += ", ISNULL( AVG( ABS(PPD.DH) ) , 0 ) AS AVE_HUE_DELTA ";
            Cmd.CommandText += ", ISNULL( AVG( ABS(PPD.DL) ) , 0 ) AS AVE_L_DELTA  ";
            Cmd.CommandText += "FROM CPPatchDataLab PPD ";
            Cmd.CommandText += "WHERE ( PPD.CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 100 AND CMYK_K = 0 ) ";
            Cmd.CommandText += "AND PPD.MeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ")";

            Cmd.CommandText += " UNION ALL ";

            /*===== Black =====*/
            Cmd.CommandText += "SELECT ";
            Cmd.CommandText += "ISNULL( AVG(PPD.DE) , 0) AS AVE_DELTA_E ";
            Cmd.CommandText += ", ISNULL( AVG( ABS(PPD.DH) ) , 0 ) AS AVE_HUE_DELTA ";
            Cmd.CommandText += ", ISNULL( AVG( ABS(PPD.DL) ) , 0 ) AS AVE_L_DELTA  ";
            Cmd.CommandText += "FROM CPPatchDataLab PPD ";
            Cmd.CommandText += "WHERE ( PPD.CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 100 ) ";
            Cmd.CommandText += "AND PPD.MeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ")";

            Cmd.CommandText += " UNION ALL ";

            /*===== Paper =====*/
            Cmd.CommandText += "SELECT ";
            Cmd.CommandText += "ISNULL( AVG(PPD.DE) , 0) AS AVE_DELTA_E ";
            Cmd.CommandText += ", ISNULL( AVG( ABS(PPD.DH) ) , 0 ) AS AVE_HUE_DELTA ";
            Cmd.CommandText += ", ISNULL( AVG( ABS(PPD.DL) ) , 0 ) AS AVE_L_DELTA  ";
            Cmd.CommandText += "FROM CPPatchDataLab PPD  ";
            Cmd.CommandText += "WHERE ( PPD.CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 0 ) ";
            Cmd.CommandText += "AND PPD.MeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ")";

            Cmd.CommandType = CommandType.Text;
            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);

            if (DT.Rows.Count > 0)
            {
                //========Ave_Delta_E========//
                LTRL_CyanAvgDeltaE.Text = Convert.ToDecimal(DT.Rows[0]["AVE_DELTA_E"]).ToString("N2");
                LTRL_MagentaAvgDeltaE.Text = Convert.ToDecimal(DT.Rows[1]["AVE_DELTA_E"]).ToString("N2");
                LTRL_YellowAvgDeltaE.Text = Convert.ToDecimal(DT.Rows[2]["AVE_DELTA_E"]).ToString("N2");
                LTRL_BlackAvgDeltaE.Text = Convert.ToDecimal(DT.Rows[3]["AVE_DELTA_E"]).ToString("N2");
                LTRL_PaperAveDelta.Text = Convert.ToDecimal(DT.Rows[4]["AVE_DELTA_E"]).ToString("N2");
                //========Ave Hue Delta========//
                LTRL_CAveHueDelta.Text = Convert.ToDecimal(DT.Rows[0]["AVE_HUE_DELTA"]).ToString("N2");
                LTRL_MAveHueDelta.Text = Convert.ToDecimal(DT.Rows[1]["AVE_HUE_DELTA"]).ToString("N2");
                LTRL_YAveHueDelta.Text = Convert.ToDecimal(DT.Rows[2]["AVE_HUE_DELTA"]).ToString("N2");
                LTRL_BAveHueDelta.Text = Convert.ToDecimal(DT.Rows[3]["AVE_HUE_DELTA"]).ToString("N2");
                LTRL_PaperAveHueDelta.Text = Convert.ToDecimal(DT.Rows[4]["AVE_HUE_DELTA"]).ToString("N2");
                //========Ave L* Delta========//
                LTRL_CAVELDelta.Text = Convert.ToDecimal(DT.Rows[0]["AVE_L_DELTA"]).ToString("N2");
                LTRL_MAveLDelta.Text = Convert.ToDecimal(DT.Rows[1]["AVE_L_DELTA"]).ToString("N2");
                LTRL_YAveLDelta.Text = Convert.ToDecimal(DT.Rows[2]["AVE_L_DELTA"]).ToString("N2");
                LTRL_BAveLDelta.Text = Convert.ToDecimal(DT.Rows[3]["AVE_L_DELTA"]).ToString("N2");
                LTRL_PaperAveLDelta.Text = Convert.ToDecimal(DT.Rows[4]["AVE_L_DELTA"]).ToString("N2");
            }

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void GetConsumableBatchInfo()
    {
        try
        {
            //========== Query ==========//
            SqlCommand Cmd = new SqlCommand();

            Cmd.CommandText = "SELECT ";
            Cmd.CommandText += "(DP.Substrate_Batch) AS Substrate_Batch ";
            Cmd.CommandText += ", COUNT(PMD.CPMeasurementGUID) as Measurements	";
            Cmd.CommandText += ", ( CONVERT( DECIMAL(18,2) , SUM(CASE PMD.STATUS WHEN 0 THEN 1 ELSE 0 END) ) / COUNT(PMD.CPMeasurementGUID) ) * CONVERT( DECIMAL(18,2) , 100 ) AS ErrorRate ";
            Cmd.CommandText += ", ISNULL( AVG(ACT.Average) , 0 ) AS AVG_DE ";
            Cmd.CommandText += "FROM CPMeasurement PMD ";
            Cmd.CommandText += "INNER JOIN CPMeasurement_DP DP ON PMD.CPMeasurementGUID = DP.CPMeasurementGUID ";
            Cmd.CommandText += "INNER JOIN CPMeasurement_ACT ACT ON PMD.CPMeasurementGUID = ACT.CPMeasurementGUID ";
            Cmd.CommandText += "WHERE PMD.CPMeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";
            Cmd.CommandText += "AND LTRIM(ISNULL(DP.Substrate_Batch,'')) <> '' ";
            Cmd.CommandText += "GROUP BY DP.Substrate_Batch ";

            Cmd.CommandType = CommandType.Text;
            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);

            DT.TableName = "BatchInfo";
            ViewState["DT_BatchInfo"] = DT;

            DGConBatchInfo.DataSource = DT;
            Fuji.Common.CommonFunctions.SetDataGridDefaultSettings(DGConBatchInfo);
            DGConBatchInfo.AllowPaging = false;
            DGConBatchInfo.DataBind();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void GetInkBatchInfo()
    {
        try
        {
            //========== ==========//
            SqlCommand Cmd = new SqlCommand();

            Cmd.CommandText = "SELECT ";
            Cmd.CommandText += "(DP.Ink_Batch) AS Ink_Batch ";
            Cmd.CommandText += ", COUNT(PMD.CPMeasurementGUID) as Measurements	";
            Cmd.CommandText += ", ( CONVERT( DECIMAL(18,2) , SUM(CASE PMD.STATUS WHEN 0 THEN 1 ELSE 0 END) ) / COUNT(PMD.CPMeasurementGUID) ) * CONVERT( DECIMAL(18,2) , 100 ) AS ErrorRate ";
            Cmd.CommandText += ", ISNULL( AVG(ACT.Average) , 0 ) AS AVG_DE ";
            Cmd.CommandText += "FROM CPMeasurement PMD ";
            Cmd.CommandText += "INNER JOIN CPMeasurement_DP DP ON PMD.CPMeasurementGUID = DP.CPMeasurementGUID ";
            Cmd.CommandText += "INNER JOIN CPMeasurement_ACT ACT ON PMD.CPMeasurementGUID = ACT.CPMeasurementGUID ";
            Cmd.CommandText += "WHERE PMD.CPMeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";
            Cmd.CommandText += "AND LTRIM(ISNULL(DP.INK_Batch,'')) <> '' ";
            Cmd.CommandText += "GROUP BY DP.Ink_Batch ";

            Cmd.CommandType = CommandType.Text;
            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);

            DT.TableName = "InkBatchInfo";
            ViewState["DT_InkBatchInfo"] = DT;

            DGInkBatch.DataSource = DT;
            Fuji.Common.CommonFunctions.SetDataGridDefaultSettings(DGInkBatch);
            DGInkBatch.AllowPaging = false;
            DGInkBatch.DataBind();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillPieChart(DataTable DT)
    {
        try
        {
            //========= Set 3D chart flag =========//
            ChartPie1.ChartAreas["Default"].Area3DStyle.Enable3D = true;

            //========= Declarations =========//
            Series ObjSeries = new Series();
            DataPoint objDP;

            #region Passed
            objDP = new DataPoint();

            decimal PROOF_PASSED;
            if (!Convert.IsDBNull(DT.Rows[0]["PROOF_PASSED"]))
            {
                PROOF_PASSED = Convert.ToDecimal(DT.Rows[0]["PROOF_PASSED"]);
                double[] yVal1 = { Convert.ToDouble(PROOF_PASSED) };
                objDP.Label = "Passed";
                objDP.YValues = yVal1;
                objDP.Label = objDP.LabelToolTip = objDP.ToolTip = "Passed : " + PROOF_PASSED.ToString("N2") + "%";
                ObjSeries.ChartType = "Pie";
                objDP.Color = ObjSeries.Color = System.Drawing.Color.Green;
                ObjSeries["PieLabelStyle"] = "Outside";
                ObjSeries["PieLineArrowType"] = "Triangle";
                ObjSeries.Color = System.Drawing.Color.Green;
                ObjSeries.Points.Add(objDP);
            }
            #endregion

            #region Caution
            objDP = new DataPoint();
            decimal PROOF_CAUTION;
            if (!Convert.IsDBNull(DT.Rows[0]["PROOF_PASSED"]))
            {
                PROOF_CAUTION = Convert.ToDecimal(DT.Rows[0]["PROOF_CAUTION"]);
                double[] yVal2 = { Convert.ToDouble(PROOF_CAUTION) };
                objDP.Label = "Caution";
                objDP.YValues = yVal2;
                objDP.Label = objDP.LabelToolTip = objDP.ToolTip = "Caution : " + PROOF_CAUTION.ToString("N2") + "%";
                ObjSeries.ChartType = "Pie";
                objDP.Color = ObjSeries.Color = System.Drawing.Color.Yellow;
                ObjSeries["PieLabelStyle"] = "Outside";
                ObjSeries["PieLineArrowType"] = "Triangle";
                ObjSeries.Points.Add(objDP);
            }
            #endregion

            #region ErrorRatio
            objDP = new DataPoint();
            objDP = new DataPoint();
            decimal ErrorRate;
            if (!Convert.IsDBNull(DT.Rows[0]["PROOF_PASSED"]))
            {
                ErrorRate = Convert.ToDecimal(DT.Rows[0]["ErrorRate"]);
                double[] yVal3 = { Convert.ToDouble(ErrorRate) };
                objDP.Label = "Error Rate";
                objDP.YValues = yVal3;
                objDP.Label = objDP.LabelToolTip = objDP.ToolTip = "Error : " + ErrorRate.ToString("N2") + "%";
                ObjSeries.ChartType = "Pie";
                objDP.Color = ObjSeries.Color = System.Drawing.Color.Red;
                ObjSeries["PieLabelStyle"] = "Outside";
                ObjSeries["PieLineArrowType"] = "Triangle";
                ObjSeries.Points.Add(objDP);
            }
            #endregion

            ChartPie1.Series.Add(ObjSeries);

            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(ChartPie1.ImageUrl) + "." + ChartPie1.ImageType;
            ChartPie1.Save(Server.MapPath(FileName));


            //========= Set animation theme =========//
            ChartPie1.AnimationTheme = AnimationTheme.GrowingOneByOne;
            ChartPie1.AnimationDuration = 5;

            //========= Set Image Type =========//
            ChartPie1.ImageType = ChartImageType.Flash;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillPatches()
    {
        try
        {
            //========== Query ==========//
            SqlCommand Cmd = new SqlCommand();
            Cmd.CommandText = "WITH PatchDATA AS (";
            Cmd.CommandText += " SELECT TOP (100) ";
            Cmd.CommandText += " PERCENT CPPatchDataLabID";
            Cmd.CommandText += " , MeasurementGUID , ID_N , Sample_ID , Sample_Name , CMYK_C , CMYK_M , CMYK_Y , CMYK_K ";
            Cmd.CommandText += " , LAB_L1 , LAB_A1 , LAB_B1 , LAB_L2 , LAB_A2 , LAB_B2 , DE , DL , DA , DB , DH , DC  ";
            Cmd.CommandText += " , DENSITY , TOL_STRICT , TOL_STAND ";
            Cmd.CommandText += " FROM CPPatchDataLab ";
            Cmd.CommandText += " WHERE MeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ")";
            Cmd.CommandText += " ORDER BY SUBSTRING(Sample_Name, 1, 1), CAST(SUBSTRING(Sample_Name, 2, LEN(Sample_Name)) AS Integer) ";
            Cmd.CommandText += ") ";
            Cmd.CommandText += "SELECT ";
            Cmd.CommandText += "PD1.Sample_ID,PD1.Sample_Name , PD1.CMYK_C , PD1.CMYK_M , PD1.CMYK_Y , PD1.CMYK_K ";
            Cmd.CommandText += ", PD1.LAB_L1 ,PD1.LAB_A1 , PD1.LAB_B1 ";
            Cmd.CommandText += ", ISNULL(AVG(PD1.DE), 0) AS AVG_DE ";
            Cmd.CommandText += ", ISNULL(STDEV(PD1.DE), 0) AS STDEV_DE ";
            Cmd.CommandText += "FROM PatchDATA AS PD1  ";
            //Cmd.CommandText += "INNER JOIN CPMeasurement_PT AS CPM_PT ON PD1.MeasurementGUID = CPM_PT.CPMeasurementGUID  ";
            //Cmd.CommandText += "INNER JOIN CPDevicePrintTarget AS CPD_PRINT ON CPM_PT.CPDevicePrintTargetID = CPD_PRINT.CPDevicePrintTargetID ";
            //Cmd.CommandText += "WHERE (CPD_PRINT.CPDevicePrintTargetID = "+ DDL_PrintTarget.SelectedValue +") ";
            Cmd.CommandText += "GROUP BY PD1.Sample_Name, PD1.CMYK_C, PD1.CMYK_M, PD1.CMYK_Y, PD1.CMYK_K,PD1.LAB_L1, PD1.LAB_A1, PD1.LAB_B1,PD1.Sample_ID  ";
            //Cmd.CommandText += "Order By PD1.Sample_Name ";
            Cmd.CommandText += "ORDER BY " + LTRL_SortExpression1.Text + " " + LTRL_SortOrder1.Text;

            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);

            DT.TableName = "PatchDetails";
            ViewState["DT_PatchDetails"] = DT;

            DGPatches.DataSource = DT;
            Fuji.Common.CommonFunctions.SetDataGridDefaultSettings(DGPatches);
            DGPatches.AllowPaging = false;
            DGPatches.DataBind();
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
        finally
        {
            NoOfReads();
        }
    }
    private void FillLineChart()
    {
        try
        {
            //========== Sql Command ==========//
            SqlCommand Cmd = new SqlCommand();

            Cmd.CommandText = "SELECT ";
            Cmd.CommandText += " (PMD.CPMeasurementId) AS CPMeasurementId  ";
            Cmd.CommandText += ", (ACT.Average) AS AVG_DE ";
            Cmd.CommandText += ", (ACT.PatchdEValue) as MAX_DE	";
            Cmd.CommandText += "FROM CPMeasurement_ACT ACT ";
            Cmd.CommandText += "INNER JOIN CPMeasurement PMD ON ACT.CPMeasurementGUID = PMD.CPMeasurementGUID ";
            Cmd.CommandText += "WHERE ACT.CPMeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ")";
            Cmd.CommandText += "ORDER BY PMD.CPMeasurementID";

            Cmd.CommandType = CommandType.Text;
            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);

            ChartLine1.Series.Clear();
            ChartLine1.CallbackStateContent = CallbackStateContent.All;

            //========== Add Series AVE_DELTA ==========//
            Series objSeries = new Series();
            objSeries.Type = SeriesChartType.Spline;
            //objSeries.MarkerStyle = MarkerStyle.Triangle;       

            objSeries.Name = "Maximum Delta E";
            objSeries.ValueMembersY = "MAX_DE";
            objSeries.Color = System.Drawing.Color.Red;
            objSeries.XValueType = ChartValueTypes.Double;
            objSeries.YValueType = ChartValueTypes.Double;
            objSeries.ValueMemberX = "CPMeasurementId";

            ChartLine1.Series.Add(objSeries);

            //========== Add Series MAX_DELTA ==========//
            Series objSeries1 = new Series();
            objSeries1.Type = SeriesChartType.Spline;
            //objSeries1.MarkerStyle = MarkerStyle.Triangle;
            objSeries1.Name = "Average Delta E";
            objSeries1.ValueMembersY = "AVG_DE";
            objSeries1.Color = System.Drawing.Color.Green;
            objSeries1.XValueType = ChartValueTypes.Double;
            objSeries1.YValueType = ChartValueTypes.Double;
            objSeries1.ValueMemberX = "CPMeasurementId";

            ChartLine1.Series.Add(objSeries1);

            for (int i = 0; i < DT.Rows.Count; i = i + 1)
            {
                ChartLine1.Series[0].Points.AddXY(DT.Rows[i]["CPMeasurementId"].ToString(), (DT.Rows[i]["Max_DE"].ToString()));
                ChartLine1.Series[0].Points[i].ToolTip = "Measurement ID : " + DT.Rows[i]["CPMeasurementId"].ToString() + "\nMaximum Delta E :" + DT.Rows[i]["MAX_DE"].ToString() + "";

                ChartLine1.Series[1].Points.AddXY(DT.Rows[i]["CPMeasurementId"].ToString(), DT.Rows[i]["AVG_DE"].ToString());
                ChartLine1.Series[1].Points[i].ToolTip = "Measurement ID : " + DT.Rows[i]["CPMeasurementId"].ToString() + "\nAverage Delta E :" + DT.Rows[i]["AVG_DE"].ToString() + "";
            }

            //========== Align Legend At bottom ==========//
            ChartLine1.Legends[0].Docking = LegendDocking.Bottom;
            ChartLine1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

            string FileName = "UploadFiles/temp/" + Sessions.UserID + "_" + Path.GetFileName(ChartLine1.ImageUrl) + "." + ChartLine1.ImageType;
            ChartLine1.Save(Server.MapPath(FileName));

            //========== Other CHart Settings ==========//
            ChartLine1.AJAXZoomEnabled = true;

            // scrolling and zooming will force keeping of series data between callbacks.
            this.ChartLine1.ChartAreas["Default"].CursorX.UserEnabled = true;

            // Set initial X axis zooming                        
            this.ChartLine1.ChartAreas["Default"].AxisY.View.MinSize = 10.0;
            this.ChartLine1.ChartAreas["Default"].AxisX.View.Position = 0.0;
            this.ChartLine1.ChartAreas["Default"].AxisX.View.Size = 50.0;

            objSeries.MarkerStyle = MarkerStyle.Triangle;
            objSeries1.MarkerStyle = MarkerStyle.Triangle;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void ClearImage()
    {
        try
        {
            TD_Sample.Attributes.Add("Class", "HEADERSTYLE");
            TD_C.Attributes.Add("Class", "HEADERSTYLE");
            TD_M.Attributes.Add("Class", "HEADERSTYLE");
            TD_Y.Attributes.Add("Class", "HEADERSTYLE");
            Td_B.Attributes.Add("Class", "HEADERSTYLE");
            TD_Avg_De.Attributes.Add("Class", "HEADERSTYLE");
            TD_Std.Attributes.Add("Class", "HEADERSTYLE");
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
            #region ====  BasicInfo  ====

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
            DC_BasicInfo.ColumnName = "Location_";
            DC_BasicInfo.DataType = System.Type.GetType("System.String");
            DT_BasicInfo.Columns.Add(DC_BasicInfo);

            DC_BasicInfo = new DataColumn();
            DC_BasicInfo.ColumnName = "DateRange_";
            DC_BasicInfo.DataType = System.Type.GetType("System.String");
            DT_BasicInfo.Columns.Add(DC_BasicInfo);

            DataRow DR_BasicInfo = DT_BasicInfo.NewRow();

            DR_BasicInfo["CompanyName_"] = LTRL_CompanyName.Text.Trim();
            DR_BasicInfo["Device_"] = LTRL_DeviceName.Text.Trim();
            DR_BasicInfo["Location_"] = LTRL_Branch.Text.Trim();
            DR_BasicInfo["DateRange_"] = LTRL_DateRange.Text.Trim();

            DT_BasicInfo.Rows.Add(DR_BasicInfo);

            ViewState["DT_BasicInfo"] = DT_BasicInfo;

            #endregion

            #region ===== Search Criteria  =====

            DataTable DT_SearchCriteria = new DataTable("SearchCriteria");

            DataColumn DC_SearchCriteria = new DataColumn();
            DC_SearchCriteria.ColumnName = "Parameter_";
            DC_SearchCriteria.DataType = System.Type.GetType("System.String");
            DT_SearchCriteria.Columns.Add(DC_SearchCriteria);

            DC_SearchCriteria = new DataColumn();
            DC_SearchCriteria.ColumnName = "Result_";
            DC_SearchCriteria.DataType = System.Type.GetType("System.String");
            DT_SearchCriteria.Columns.Add(DC_SearchCriteria);

            DC_SearchCriteria = new DataColumn();
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

            DR_SearchCriteria["Parameter_"] = "No Of Reads";
            DR_SearchCriteria["Result_"] = LTRL_NoOfReads.Text.Trim();
            DR_SearchCriteria["SearchField_"] = LTRL_Field1.Text.Trim();
            DR_SearchCriteria["Operator_"] = LTRL_Operator1.Text.Trim();
            DR_SearchCriteria["Criteria_"] = LTRL_Criteria1.Text.Trim();

            DT_SearchCriteria.Rows.Add(DR_SearchCriteria);

            DR_SearchCriteria = DT_SearchCriteria.NewRow();

            DR_SearchCriteria["Parameter_"] = "Avg Delta E";
            DR_SearchCriteria["Result_"] = LTRL_AvgDeltaE.Text.Trim();
            DR_SearchCriteria["SearchField_"] = LTRL_Field2.Text.Trim();
            DR_SearchCriteria["Operator_"] = LTRL_Operator2.Text.Trim();
            DR_SearchCriteria["Criteria_"] = LTRL_Criteria2.Text.Trim();

            DT_SearchCriteria.Rows.Add(DR_SearchCriteria);

            DR_SearchCriteria = DT_SearchCriteria.NewRow();

            DR_SearchCriteria["Parameter_"] = "Avg Max Delta E";
            DR_SearchCriteria["Result_"] = LTRL_AvgMaxDeltaE.Text.Trim();
            DR_SearchCriteria["SearchField_"] = LTRL_Field3.Text.Trim();
            DR_SearchCriteria["Operator_"] = LTRL_Operator3.Text.Trim();
            DR_SearchCriteria["Criteria_"] = LTRL_Criteria3.Text.Trim();

            DT_SearchCriteria.Rows.Add(DR_SearchCriteria);

            DR_SearchCriteria = DT_SearchCriteria.NewRow();

            DR_SearchCriteria["Parameter_"] = "Error Ratio";
            DR_SearchCriteria["Result_"] = LTRL_AvgErrorRate.Text.Trim();
            DR_SearchCriteria["SearchField_"] = "---";
            DR_SearchCriteria["Operator_"] = "---";
            DR_SearchCriteria["Criteria_"] = "---";

            DT_SearchCriteria.Rows.Add(DR_SearchCriteria);

            ViewState["DT_SearchCriteria"] = DT_SearchCriteria;

            #endregion

            #region ====  Consumables  =====

            DataTable DT_Consumables = new DataTable("Consumables");

            DataColumn DC_Consumables = new DataColumn();
            DC_Consumables.ColumnName = "AveDeltaE_";
            DC_Consumables.DataType = System.Type.GetType("System.String");
            DT_Consumables.Columns.Add(DC_Consumables);

            DC_Consumables = new DataColumn();
            DC_Consumables.ColumnName = "AveHueDelta_";
            DC_Consumables.DataType = System.Type.GetType("System.String");
            DT_Consumables.Columns.Add(DC_Consumables);

            DC_Consumables = new DataColumn();
            DC_Consumables.ColumnName = "AveLDelta_";
            DC_Consumables.DataType = System.Type.GetType("System.String");
            DT_Consumables.Columns.Add(DC_Consumables);

            DataRow DR_Consumables = DT_Consumables.NewRow();

            DR_Consumables["AveDeltaE_"] = LTRL_CyanAvgDeltaE.Text.Trim();
            DR_Consumables["AveHueDelta_"] = LTRL_CAveHueDelta.Text.Trim();
            DR_Consumables["AveLDelta_"] = LTRL_CAVELDelta.Text.Trim();

            DT_Consumables.Rows.Add(DR_Consumables);

            DR_Consumables = DT_Consumables.NewRow();

            DR_Consumables["AveDeltaE_"] = LTRL_MagentaAvgDeltaE.Text.Trim();
            DR_Consumables["AveHueDelta_"] = LTRL_MAveHueDelta.Text.Trim();
            DR_Consumables["AveLDelta_"] = LTRL_MAveLDelta.Text.Trim();

            DT_Consumables.Rows.Add(DR_Consumables);

            DR_Consumables = DT_Consumables.NewRow();

            DR_Consumables["AveDeltaE_"] = LTRL_YellowAvgDeltaE.Text.Trim();
            DR_Consumables["AveHueDelta_"] = LTRL_YAveHueDelta.Text.Trim();
            DR_Consumables["AveLDelta_"] = LTRL_YAveLDelta.Text.Trim();

            DT_Consumables.Rows.Add(DR_Consumables);

            DR_Consumables = DT_Consumables.NewRow();

            DR_Consumables["AveDeltaE_"] = LTRL_BlackAvgDeltaE.Text.Trim();
            DR_Consumables["AveHueDelta_"] = LTRL_BAveHueDelta.Text.Trim();
            DR_Consumables["AveLDelta_"] = LTRL_BAveLDelta.Text.Trim();

            DT_Consumables.Rows.Add(DR_Consumables);

            DR_Consumables = DT_Consumables.NewRow();

            DR_Consumables["AveDeltaE_"] = LTRL_PaperAveDelta.Text.Trim();
            DR_Consumables["AveHueDelta_"] = LTRL_PaperAveHueDelta.Text.Trim();
            DR_Consumables["AveLDelta_"] = LTRL_PaperAveLDelta.Text.Trim();

            DT_Consumables.Rows.Add(DR_Consumables);


            ViewState["DT_Consumables"] = DT_Consumables;

            #endregion

            #region ====  Environmental  ====

            DataTable DT_Environmental = new DataTable("Environmental");

            DataColumn DC_Environmental = new DataColumn();
            DC_Environmental.ColumnName = "Average_";
            DC_Environmental.DataType = System.Type.GetType("System.String");
            DT_Environmental.Columns.Add(DC_Environmental);

            DC_Environmental = new DataColumn();
            DC_Environmental.ColumnName = "Standarad_";
            DC_Environmental.DataType = System.Type.GetType("System.String");
            DT_Environmental.Columns.Add(DC_Environmental);

            DC_Environmental = new DataColumn();
            DC_Environmental.ColumnName = "Min_";
            DC_Environmental.DataType = System.Type.GetType("System.String");
            DT_Environmental.Columns.Add(DC_Environmental);

            DataRow DR_Environmental = DT_Environmental.NewRow();

            DR_Environmental["Average_"] = LTRL_TempAvg.Text.Trim();
            DR_Environmental["Standarad_"] = LTRL_TempStandard.Text.Trim();
            DR_Environmental["Min_"] = LTRL_TempMin.Text.Trim();

            DT_Environmental.Rows.Add(DR_Environmental);

            DR_Environmental = DT_Environmental.NewRow();

            DR_Environmental["Average_"] = LTRL_RhAvg.Text.Trim();
            DR_Environmental["Standarad_"] = LTRL_RhStandard.Text.Trim();
            DR_Environmental["Min_"] = LTRL_RhMin.Text.Trim();

            DT_Environmental.Rows.Add(DR_Environmental);

            ViewState["DT_Environmental"] = DT_Environmental;

            #endregion
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion
}
