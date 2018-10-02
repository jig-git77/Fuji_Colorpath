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
#endregion

#region Others
using System.Data.SqlClient;
using Fuji.Common;
using Fuji.Business;
using Fuji.DataAccess;
#endregion

public partial class ReportBranchSummary : PageBase
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

                //========== Fill Variables to Form ==========//
                SetLabels();

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
        { 
            LblError.Text = HandleException(Exc); 
        }

    }
    protected void BTN_CreateReport_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Clear ViewState ==========//
            ViewState.Clear();

            //========== Validations ==========//
            if (DDL_Branch.SelectedIndex == 0)
            { LblError.Text = "Branch is required!!!"; return; }
            if (TXT_FromDate.Text == "")
            { LblError.Text = "From Date is required!!!"; return; }
            if (TXT_ToDate.Text == "")
            { LblError.Text = "To Date is required!!!"; return; }
            //LTRL_DateRange.Text = TXT_FromDate.Text + " - " + TXT_ToDate.Text;
            LTRL_DateRange.Text = TXT_FromDate.Text + " - " + TXT_ToDate.Text;
            LTRL_Branch.Text = Convert.ToString(DDL_Branch.SelectedItem);
            LTRL_CompanyName.Text = Convert.ToString(DDL_Company.SelectedItem);

            //========== Filling Label ==========//
            lblReportTitle.Text = "Site(Branch) Summary Report";
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

            //========== Retrieve GUID ==========//
            GetAllGuids();

            if (LTRL_CPMeasurementGUID.Text == "")
            {
                LblError.Text = "No measurements found for the search criteria selected!";
            }
            else
            {
                //========== Load Data ==========//
                LoadData();

                //========== Hiding Search / displaying Result ==========//
                PNL_Search.Style.Add("display", "none");
                PNL_Result.Style.Add("display", "");

                #region ====   Set Column of Data Tables ( BasicInfo ) ======

                DataTable DT_BasicInfo = new DataTable("BasicInfo");

                DataColumn DC_BasicInfo = new DataColumn();
                DC_BasicInfo.ColumnName = "CompanyName_";
                DC_BasicInfo.DataType = System.Type.GetType("System.String");
                DT_BasicInfo.Columns.Add(DC_BasicInfo);

                DC_BasicInfo = new DataColumn();
                DC_BasicInfo.ColumnName = "BranchName_";
                DC_BasicInfo.DataType = System.Type.GetType("System.String");
                DT_BasicInfo.Columns.Add(DC_BasicInfo);

                DC_BasicInfo = new DataColumn();
                DC_BasicInfo.ColumnName = "DateRange_";
                DC_BasicInfo.DataType = System.Type.GetType("System.String");
                DT_BasicInfo.Columns.Add(DC_BasicInfo);

                DataRow DR_BasicInfo = DT_BasicInfo.NewRow();

                DR_BasicInfo["CompanyName_"] = LTRL_CompanyName.Text.Trim();
                DR_BasicInfo["BranchName_"] = LTRL_Branch.Text.Trim();
                DR_BasicInfo["DateRange_"] = LTRL_DateRange.Text.Trim();

                DT_BasicInfo.Rows.Add(DR_BasicInfo);

                ViewState["DT_BasicInfo"] = DT_BasicInfo;

                #endregion

                #region ===== Set Data in Temp Datatable (SearchCriteria)  ===== //

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

                DR_SearchCriteria["Parameter_"] = "Average Delta E";
                DR_SearchCriteria["Result_"] = LTRL_AvgDeltaE.Text.Trim();
                DR_SearchCriteria["SearchField_"] = LTRL_Field1.Text.Trim();
                DR_SearchCriteria["Operator_"] = LTRL_Operator1.Text.Trim();
                DR_SearchCriteria["Criteria_"] = LTRL_Criteria1.Text.Trim();

                DT_SearchCriteria.Rows.Add(DR_SearchCriteria);

                DR_SearchCriteria = DT_SearchCriteria.NewRow();

                DR_SearchCriteria["Parameter_"] = "Average Plate Deviation";
                DR_SearchCriteria["Result_"] = LTRL_AvgPlateDeviation.Text.Trim();
                DR_SearchCriteria["SearchField_"] = LTRL_Field2.Text.Trim();
                DR_SearchCriteria["Operator_"] = LTRL_Operator2.Text.Trim();
                DR_SearchCriteria["Criteria_"] = LTRL_Criteria2.Text.Trim();

                DT_SearchCriteria.Rows.Add(DR_SearchCriteria);

                DR_SearchCriteria = DT_SearchCriteria.NewRow();

                DR_SearchCriteria["Parameter_"] = "Error Rate";
                DR_SearchCriteria["Result_"] = LTRL_AvgErrorRate.Text.Trim();
                DR_SearchCriteria["SearchField_"] = LTRL_Field3.Text.Trim();
                DR_SearchCriteria["Operator_"] = LTRL_Operator3.Text.Trim();
                DR_SearchCriteria["Criteria_"] = LTRL_Criteria3.Text.Trim();

                DT_SearchCriteria.Rows.Add(DR_SearchCriteria);

                ViewState["DT_SearchCriteria"] = DT_SearchCriteria;

                #endregion
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void BTN_BackToSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //========== displaying Search / Hiding Result ==========//
            PNL_Search.Style.Add("display", "");
            PNL_Result.Style.Add("display", "none");
            lblReportTitle.Text = "Site(Branch) Summary Report(Search Criteria)";
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void SortCommandColorSpe_OnClick(object sender, DataGridSortCommandEventArgs e)
    {
        try
        {
            LTRL_SortExpression1.Text = e.SortExpression;

            if (LTRL_SortOrder1.Text == "ASC")
            { LTRL_SortOrder1.Text = "DESC"; }
            else { LTRL_SortOrder1.Text = "ASC"; }

            int i = 0;
            foreach (DataGridColumn col in DG_ColorSpe.Columns)
            {
                if (col.SortExpression == e.SortExpression)
                    DG_ColorSpe.Columns[i].HeaderStyle.CssClass = "gridHeaderSort" + LTRL_SortOrder1.Text;
                else
                    DG_ColorSpe.Columns[i].HeaderStyle.CssClass = "HEADERSTYLE";
                i++;
            }
            ColorSpeAdherence();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void SortCommandDensitySpe_OnClick(object sender, DataGridSortCommandEventArgs e)
    {
        try
        {
            LTRL_SortExpression2.Text = e.SortExpression;
            if (LTRL_SortOrder2.Text == "ASC")
            { LTRL_SortOrder2.Text = "DESC"; }
            else { LTRL_SortOrder2.Text = "ASC"; }

            int i = 0;
            foreach (DataGridColumn col in DG_DensitySpe.Columns)
            {
                if (col.SortExpression == e.SortExpression)
                    DG_DensitySpe.Columns[i].HeaderStyle.CssClass = "gridHeaderSort" + LTRL_SortOrder2.Text;
                else
                    DG_DensitySpe.Columns[i].HeaderStyle.CssClass = "HEADERSTYLE";
                i++;
            }
            DencitySpeAdherence();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void SortCommandPlateSpe_OnClick(object sender, DataGridSortCommandEventArgs e)
    {
        try
        {
            LTRL_SortExpression3.Text = e.SortExpression;
            if (LTRL_SortOrder3.Text == "ASC")
            { LTRL_SortOrder3.Text = "DESC"; }
            else { LTRL_SortOrder3.Text = "ASC"; }

            int i = 0;
            foreach (DataGridColumn col in DG_PlateSpe.Columns)
            {
                if (col.SortExpression == e.SortExpression)
                    DG_PlateSpe.Columns[i].HeaderStyle.CssClass = "gridHeaderSort" + LTRL_SortOrder3.Text;
                else
                    DG_PlateSpe.Columns[i].HeaderStyle.CssClass = "HEADERSTYLE";
                i++;
            }
            PlateSpeAdherence();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void BTN_MakePDF_Click(object sender, EventArgs e)
    {
        try
        {
            Fuji.Common.ReportPDF objReportPDF = new Fuji.Common.ReportPDF();
            objReportPDF.ReportType = (int)Fuji.Common.Enums.enumReportType.BranchSummaryReport;
            objReportPDF.PDFPath = Server.MapPath("UploadFiles/Report_PDF/");
            objReportPDF.PDF_NAME = Sessions.UserID + "_BranchSummaryReport.PDF";//LoginID

            if (ViewState["DT_BasicInfo"] != null)
            { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_BasicInfo"]); }
            if (ViewState["DT_SearchCriteria"] != null)
            { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_SearchCriteria"]); }
            if (ViewState["DT_ColorSpecification"] != null)
            { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_ColorSpecification"]); }
            if (ViewState["DT_DensitySpecification"] != null)
            { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_DensitySpecification"]); }
            if (ViewState["DT_PlateSpecification"] != null)
            { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_PlateSpecification"]); }

            objReportPDF.MakePDF();

            Fuji.Common.CommonFunctions.DownlaodFile(objReportPDF.PDFPath, objReportPDF.PDF_NAME, objReportPDF.PDF_NAME);
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
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
        {
            LblError.Text = HandleException(Exc);
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
    protected void FillDates(object sender, EventArgs e)
    {
        try
        {
            //==========  Declaring Objects ==========//
            BranchData objBranch = new BranchData();
            objBranch.BranchID = Convert.ToInt32(DDL_Branch.SelectedValue );

            //==========  Get Data Table and Bind ==========//
            DataTable DT = new DataTable();
            DT = objBranch.uspGetDateByBranchID();

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
            //========== Set Style ==========//
            DG_ColorSpe.Style.Add("width", "100%");
            DG_ColorSpe.Attributes.Add("cellspacing", "1px");
            DG_ColorSpe.Attributes.Add("cellspadding", "0px");
            DG_ColorSpe.Attributes.Add("border", "0px");

            DG_DensitySpe.Style.Add("width", "100%");
            DG_DensitySpe.Attributes.Add("cellspacing", "1px");
            DG_DensitySpe.Attributes.Add("cellspadding", "0px");
            DG_DensitySpe.Attributes.Add("border", "0px");

            DG_PlateSpe.Style.Add("width", "100%");
            DG_PlateSpe.Attributes.Add("cellspacing", "1px");
            DG_PlateSpe.Attributes.Add("cellspadding", "0px");
            DG_PlateSpe.Attributes.Add("border", "0px");

            DDL_Company.Style.Add("width", "100%");
            DDL_Branch.Style.Add("width", "100%");

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

            if (Fuji.Common.Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin) //========== Query Only For Admin ==========//
            {
                Cmd.CommandText = "SELECT ";
                Cmd.CommandText += "DISTINCT(PMD.CPMeasurementGUID) as CPMeasurementGUID ";
                Cmd.CommandText += "FROM  CPMeasurement PMD ";
                Cmd.CommandText += JoinClause;
                Cmd.CommandText += "WHERE PMD.CPDeviceID IN ( SELECT CPDeviceID from CPDevice WHERE CPDongleID IN (SELECT CPDongleID FROM CPDongle WHERE BranchID = " + DDL_Branch.SelectedValue + " ) ) ";
                Cmd.CommandText += " AND ( PMD.CreatedDT BETWEEN  '" + TXT_FromDate.Text + "' and '" + Convert.ToDateTime(TXT_ToDate.Text).AddDays(1).ToShortDateString() + "') ";
                Cmd.CommandText += " AND ISNULL( PMD.IsUsed , 1 ) = 1";
            }
            else //========== Query Only For Other User type ==========//
            {
                Cmd.CommandText = "SELECT ";
                Cmd.CommandText += "DISTINCT(PMD.CPMeasurementGUID) as CPMeasurementGUID ";
                Cmd.CommandText += "FROM  CPMeasurement PMD ";
                Cmd.CommandText += "INNER JOIN CPV.vwGetAllDevicesPerUsers vw1 ON vw1.CPDeviceID = PMD.CPDeviceID ";
                Cmd.CommandText += JoinClause;
                Cmd.CommandText += "WHERE vw1.TaskeroUserID = " + Fuji.Common.Sessions.UserID + " AND vw1.BranchID = " + DDL_Branch.SelectedValue + " ";
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
    private void LoadData()
    {
        try
        {
            //========== LoadData ==========//
            AverageDeltaE();
            AveragePlateDaviation();
            ErrorRate();
            ColorSpeAdherence();
            DencitySpeAdherence();
            PlateSpeAdherence();
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
            Cmd.CommandText += " ISNULL( AVG(Average) , 0 ) AS AVG_DE ";
            Cmd.CommandText += "FROM CPMeasurement_ACT ACT ";
            Cmd.CommandText += "WHERE ACT.CPMeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ")";

            Cmd.CommandType = CommandType.Text;

            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);

            //========== Show values ==========//
            if (DT.Rows.Count > 0)
            { LTRL_AvgDeltaE.Text = Convert.ToDecimal(DT.Rows[0]["AVG_DE"]).ToString("N2"); }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void AveragePlateDaviation()
    {
        try
        {
            //========== Query ==========//            
            SqlCommand Cmd = new SqlCommand();

            Cmd.CommandText = "SELECT ";
            Cmd.CommandText += " ISNUll( Avg(ABS(PPD.DEVIATION)) , 0 ) AS AVG_DEVIATION ";
            Cmd.CommandText += "FROM CPPatchDataPlate PPD ";
            Cmd.CommandText += "WHERE PPD.MeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ")";

            Cmd.CommandType = CommandType.Text;

            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);
            //========== Show values ==========//
            if (DT.Rows.Count > 0)
            { LTRL_AvgPlateDeviation.Text = Convert.ToDecimal(DT.Rows[0]["AVG_DEVIATION"]).ToString("N2"); }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void ErrorRate()
    {
        try
        {
            //========== Query ==========//
            SqlCommand Cmd = new SqlCommand();

            Cmd.CommandText = "SELECT ";
            Cmd.CommandText += " COUNT(PMD.CPMeasurementGUID) as TotalReads ";
            Cmd.CommandText += ", ( CONVERT( DECIMAL(18,2) , SUM(CASE PMD.STATUS WHEN 0 THEN 1 ELSE 0 END) ) / COUNT(PMD.CPMeasurementGUID) ) * CONVERT( DECIMAL(18,2) , 100 ) AS ErrorRate ";
            //Cmd.CommandText += ", ( CONVERT( DECIMAL(18,2) , SUM(CASE PMD.STATUS WHEN 1 THEN 1 ELSE 0 END) ) / COUNT(PMD.CPMeasurementGUID) ) * CONVERT( DECIMAL(18,2) , 100 ) AS Proof_Caution ";
            //Cmd.CommandText += ", ( CONVERT( DECIMAL(18,2) , SUM(CASE PMD.STATUS WHEN 2 THEN 1 ELSE 0 END) ) / COUNT(PMD.CPMeasurementGUID) ) * CONVERT( DECIMAL(18,2) , 100 ) AS Proof_Passed ";
            Cmd.CommandText += "FROM CPMeasurement PMD  ";
            Cmd.CommandText += "WHERE PMD.CPMeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ")";

            Cmd.CommandType = CommandType.Text;

            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);
            //========== Show values ==========//
            if (DT.Rows.Count > 0)
            { LTRL_AvgErrorRate.Text = Convert.ToDecimal(DT.Rows[0]["ErrorRate"]).ToString("N2") + "%"; }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void ColorSpeAdherence()
    {
        try
        {
            //========== Query ==========//            
            SqlCommand Cmd = new SqlCommand();

            Cmd.CommandText = "SELECT ";
            Cmd.CommandText += "MAX(CPD.CPDeviceDesc) AS CPDeviceDesc ";
            Cmd.CommandText += ", SUM(CASE WHEN PMD.[Status] = 0 THEN 1 ELSE 0 END) AS Total_Errors ";
            Cmd.CommandText += ",(CASE COUNT(PMD.CPMeasurementID) WHEN 0 THEN 00.00 ELSE ( convert(Decimal(18,2) ,(SUM(CASE WHEN PMD.[Status] = 0 THEN 1 ELSE 0 END))) / convert(Decimal(18,2) , COUNT(PMD.CPMeasurementID)) * 100 ) END ) AS ErrorRate ";
            Cmd.CommandText += ", ISNULL(AVG(ACT.AVERAGE) , 0) AS AVG_DE  ";
            Cmd.CommandText += ", ISNULL(MAX(ACT.PatchdEValue) , 0) AS MAX_DE ";
            Cmd.CommandText += "FROM CPMeasurement PMD ";
            Cmd.CommandText += "INNER JOIN CPDevice CPD ON CPD.CPDeviceID = PMD.CPDeviceID ";
            Cmd.CommandText += "INNER JOIN CPMeasurement_ACT ACT ON PMD.CPMeasurementGUID = ACT.CPMeasurementGUID ";
            Cmd.CommandText += "WHERE PMD.CPMeasurementTypeID = 1 ";
            Cmd.CommandText += "AND PMD.CPMeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";
            Cmd.CommandText += "GROUP BY PMD.CPDeviceID ";
            Cmd.CommandText += "Order By " + LTRL_SortExpression1.Text + " " + LTRL_SortOrder1.Text;

            Cmd.CommandType = CommandType.Text;

            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);
            DT.TableName = "ColorSpecification";

            if (DT.Rows.Count > 0)
            {
                Fuji.Common.CommonFunctions.SetDataGridDefaultSettings(DG_ColorSpe);
                DG_ColorSpe.AllowPaging = false;
                DG_ColorSpe.DataSource = DT;
                DG_ColorSpe.DataBind();

                ViewState["DT_ColorSpecification"] = DT;

                LBL_ColorSpeNotFound.Visible = true;
                DG_ColorSpe.Visible = true;
            }
            else
            {
                LBL_ColorSpeNotFound.Visible = false;
                DG_ColorSpe.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void DencitySpeAdherence()
    {
        try
        {
            //========== Query ==========//            
            SqlCommand Cmd = new SqlCommand();

            Cmd.CommandText = "SELECT ";
            Cmd.CommandText += "MAX(CPD.CPDeviceDesc) AS CPDeviceDesc ";
            Cmd.CommandText += ", SUM(CASE WHEN PMD.[Status] = 0 THEN 1 ELSE 0 END) AS Total_Errors ";
            Cmd.CommandText += ",(CASE COUNT(PMD.CPMeasurementID) WHEN 0 THEN 00.00 ELSE ( convert(Decimal(18,2) ,(SUM(CASE WHEN PMD.[Status] = 0 THEN 1 ELSE 0 END))) / convert(Decimal(18,2) , COUNT(PMD.CPMeasurementID)) * 100 ) END ) AS ErrorRate ";
            Cmd.CommandText += ", ISNULL(AVG(ACT.AVERAGE) , 0) AS AVG_DE  ";
            Cmd.CommandText += ", ISNULL(MAX(ACT.PatchdEValue) , 0) AS MAX_DE ";
            Cmd.CommandText += "FROM CPMeasurement PMD ";
            Cmd.CommandText += "INNER JOIN CPDevice CPD ON CPD.CPDeviceID = PMD.CPDeviceID ";
            Cmd.CommandText += "INNER JOIN CPMeasurement_ACT ACT ON PMD.CPMeasurementGUID = ACT.CPMeasurementGUID ";
            Cmd.CommandText += "WHERE PMD.CPMeasurementTypeID = 2 ";
            Cmd.CommandText += "AND PMD.CPMeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";
            Cmd.CommandText += "GROUP BY PMD.CPDeviceID ";
            Cmd.CommandText += "Order By " + LTRL_SortExpression2.Text + " " + LTRL_SortOrder2.Text;

            Cmd.CommandType = CommandType.Text;
            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);
            DT.TableName = "DensitySpecification";

            if (DT.Rows.Count > 0)
            {
                Fuji.Common.CommonFunctions.SetDataGridDefaultSettings(DG_DensitySpe);
                DG_DensitySpe.AllowPaging = false;
                DG_DensitySpe.DataSource = DT;
                DG_DensitySpe.DataBind();

                ViewState["DT_DensitySpecification"] = DT;

                LBL_DensityNotFound.Visible = true;
                DG_DensitySpe.Visible = true;
            }
            else
            {
                LBL_DensityNotFound.Visible = false;
                DG_DensitySpe.Visible = false;

            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void PlateSpeAdherence()
    {
        try
        {
            //========== Query ==========//            
            SqlCommand Cmd = new SqlCommand();


            Cmd.CommandText = "SELECT ";
            Cmd.CommandText += "MAX(CPD.CPDeviceDesc) AS CPDeviceDesc ";
            Cmd.CommandText += ", SUM(CASE WHEN PMD.[Status] = 0 THEN 1 ELSE 0 END) AS Total_Errors ";
            Cmd.CommandText += ",(CASE COUNT(PMD.CPMeasurementID) WHEN 0 THEN 00.00 ELSE ( convert(Decimal(18,2) ,(SUM(CASE WHEN PMD.[Status] = 0 THEN 1 ELSE 0 END))) / convert(Decimal(18,2) , COUNT(PMD.CPMeasurementID)) * 100 ) END ) AS ErrorRate ";
            Cmd.CommandText += ", ISNULL(AVG(ACT.AvgNeg_DotArea), 0) AS AVG_Delta_NEG ";
            Cmd.CommandText += ",ISNULL(AVG(ACT.AvgPos_DotArea),0) AS AVG_Delta_POS ";
            Cmd.CommandText += ", ISNULL(MIN(ACT.MaxNeg_DotArea) , 0) AS MAX_Delta_NEG ";
            Cmd.CommandText += ",ISNULL(MAX(ACT.MaxPos_DotArea) , 0) AS MAX_Delta_POS ";
            Cmd.CommandText += "FROM CPMeasurement PMD ";
            Cmd.CommandText += "INNER JOIN CPDevice CPD ON CPD.CPDeviceID = PMD.CPDeviceID ";
            Cmd.CommandText += "INNER JOIN CPMeasurement_ACT_Plate ACT ON PMD.CPMeasurementGUID = ACT.CPMeasurementGUID ";
            Cmd.CommandText += "WHERE PMD.CPMeasurementTypeID = 3 ";
            Cmd.CommandText += "AND PMD.CPMeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";
            Cmd.CommandText += "GROUP BY PMD.CPDeviceID ";
            Cmd.CommandText += "Order By " + LTRL_SortExpression3.Text + " " + LTRL_SortOrder3.Text;

            Cmd.CommandType = CommandType.Text;

            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);
            DT.TableName = "PlateSpecification";

            if (DT.Rows.Count > 0)
            {
                Fuji.Common.CommonFunctions.SetDataGridDefaultSettings(DG_PlateSpe);
                DG_PlateSpe.AllowPaging = false;
                DG_PlateSpe.DataSource = DT;
                DG_PlateSpe.DataBind();

                ViewState["DT_PlateSpecification"] = DT;

                LBL_PlateSpeNotFound.Visible = true;
                DG_PlateSpe.Visible = true;
            }
            else
            {
                LBL_PlateSpeNotFound.Visible = false;
                DG_PlateSpe.Visible = false;
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    #region Set Labels
    /// <summary>
    /// Used to set the labels of the form
    /// </summary>
    private void SetLabels()
    {
        try
        {
            //========== Set Culture ==========//
            Resources.Message.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));
            Resources.BranchSummaryReport.Culture = (new System.Globalization.CultureInfo(Fuji.Common.Sessions.Language));

            //========== Set Values ==========//
            LTRL_Branch1.Text = Resources.BranchSummaryReport.LBL_Branch_Text;
            LTRL_Company.Text = Resources.BranchSummaryReport.LBL_Company_Text;
            LTRL_SearchByDate.Text = Resources.BranchSummaryReport.LBL_SearchByDate_Text;
            LTRL_From.Text = Resources.BranchSummaryReport.LBL_From_Text;
            LTRL_To.Text = Resources.BranchSummaryReport.LBL_To_Text;
            LTRL_Field.Text = Resources.BranchSummaryReport.LBL_Field_Text;
            LTRL_Operator.Text = Resources.BranchSummaryReport.LBL_Operator_Text;
            LTRL_Criteria.Text = Resources.BranchSummaryReport.LBL_Criteria_Text;
            LTRL_SearchOptionOne.Text = Resources.BranchSummaryReport.LBL_SearchOptionOne_Text;
            LTRL_SearchOptionTwo.Text = Resources.BranchSummaryReport.LBL_SearchOptionTwo_Text;
            LTRL_SearchOptionThree.Text = Resources.BranchSummaryReport.LBL_SearchOptionThree_Text;

            BTN_CreateReport.Text = Resources.BranchSummaryReport.BTN_CreateReport_Text;
            BTN_MakePDF.Text = Resources.BranchSummaryReport.BTN_MakePDF_Text;
            BTN_BackToSearch.Text = Resources.BranchSummaryReport.BTN_BackToSearch_Text;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #endregion
}