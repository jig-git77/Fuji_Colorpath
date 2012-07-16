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

public partial class ReportProductivity : PageBase
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
                FillBranch(null, null);
                FillDongle(null, null);
                FillDates(null, null);

                //========== Fill Drop Down ==========//
                SetStyle();
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void SortCommandProduction_OnClick(object sender, DataGridSortCommandEventArgs e)
    {
        try
        {
            //========== Set Sort Expression ==========//
            LBL_SortExpression1.Text = e.SortExpression;
            if (LBL_SortOrder1.Text == "ASC")
            { LBL_SortOrder1.Text = "DESC"; }
            else { LBL_SortOrder1.Text = "ASC"; }

            int i = 0;
            foreach (DataGridColumn col in DGProductivityDetail.Columns)
            {
                if (col.SortExpression == e.SortExpression)
                    DGProductivityDetail.Columns[i].HeaderStyle.CssClass = "gridHeaderSort" + LBL_SortOrder1.Text;
                else
                    DGProductivityDetail.Columns[i].HeaderStyle.CssClass = "HEADERSTYLE";
                i++;
            }
            FillProductionDetail();
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
            lblReportTitle.Text = "Device Variability Report(Search Criteria)";
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
            //========== Create ReportPDF Object ==========//
            Fuji.Common.ReportPDF objReportPDF = new Fuji.Common.ReportPDF();
            objReportPDF.ReportType = (int)Fuji.Common.Enums.enumReportType.ProductivityReport;
            objReportPDF.PDFPath = Server.MapPath("UploadFiles/Report_PDF/");
            objReportPDF.PDF_NAME = Sessions.UserID + "_ProductivityReport.pdf";

            if (ViewState["DT_BasicInfo"] != null)
            { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_BasicInfo"]); }
            if (ViewState["DT_SearchCriteria"] != null)
            { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_SearchCriteria"]); }


            objReportPDF.Dg = (DataGrid)DGProductivityDetail;

            objReportPDF.MakePDF();

            Fuji.Common.CommonFunctions.DownlaodFile(objReportPDF.PDFPath, objReportPDF.PDF_NAME, objReportPDF.PDF_NAME);
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void BTN_CreateReport_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Validations ==========//
            if (TXT_FromDate.Text == "")
            { LblError.Text = "From Date is required!!!"; return; }
            if (TXT_ToDate.Text == "")
            { LblError.Text = "To Date is required!!!"; return; }

            lblReportTitle.Text = "Productivity Report";
            TDReportTitle.Attributes.Add("class", "");

            LBL_DateRange.Text = TXT_FromDate.Text + " - " + TXT_ToDate.Text;
            if (DDL_DongleNo.SelectedValue != "0")
            { LBL_DongleNo.Text = Convert.ToString(DDL_DongleNo.SelectedItem); }
            else
            { LBL_DongleNo.Text = ""; }

            if (DDL_Branch.SelectedValue != "0")
            { LBL_Branch.Text = "- " + Convert.ToString(DDL_Branch.SelectedItem); }
            else
            { LBL_Branch.Text = ""; }
            LBL_CompanyName.Text = Convert.ToString(DDL_Company.SelectedItem);

            //========== Filling Label ==========//

            LBL_Field1.Text = DDL_OPT1_Field.SelectedItem.Text;
            LBL_Field2.Text = DDL_OPT2_Field.SelectedItem.Text;
            LBL_Field3.Text = DDL_OPT3_Field.SelectedItem.Text;

            LBL_Operator1.Text = DDL_OPT1_Operator.SelectedItem.Text;
            LBL_Operator2.Text = DDL_OPT2_Operator.SelectedItem.Text;
            LBL_Operator3.Text = DDL_OPT3_Operator.SelectedItem.Text;

            LBL_Criteria1.Text = TXT_OPT1_KeyWords.Text;
            LBL_Criteria2.Text = TXT_OPT2_KeyWords.Text;
            LBL_Criteria3.Text = TXT_OPT3_KeyWords.Text;

            #region ===== Set Data in Temp Datatable (SearchCriteria)  ===== //

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

            #region ====   Set Column of Data Tables ( BasicInfo ) ======

            DataTable DT_BasicInfo = new DataTable("BasicInfo");

            DataColumn DC_BasicInfo = new DataColumn();
            DC_BasicInfo.ColumnName = "DongleName_";
            DC_BasicInfo.DataType = System.Type.GetType("System.String");
            DT_BasicInfo.Columns.Add(DC_BasicInfo);

            DC_BasicInfo = new DataColumn();
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
            DR_BasicInfo["DongleName_"] = LBL_DongleNo.Text.Trim();
            DR_BasicInfo["CompanyName_"] = LBL_CompanyName.Text.Trim();
            DR_BasicInfo["BranchName_"] = LBL_Branch.Text.Trim();
            DR_BasicInfo["DateRange_"] = LBL_DateRange.Text.Trim();

            DT_BasicInfo.Rows.Add(DR_BasicInfo);

            ViewState["DT_BasicInfo"] = DT_BasicInfo;

            #endregion

            //============= Fill Grid =============//
            FillProductionDetail();

            if (DGProductivityDetail.Items.Count > 0)
            {
                //========== Hiding Search / displaying Result ==========//
                PNL_Search.Style.Add("display", "none");
                PNL_Result.Style.Add("display", "");
            }
            else
            {
                LblError.Text = "No measurements found for the search criteria selected!";
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void DGProductivityDetail_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //========== Criteria ==========//
                string JoinClause = "";
                if (DDL_OPT1_Field.SelectedIndex > 0 && DDL_OPT1_Operator.SelectedIndex > 0)
                { JoinClause += DDL_OPT1_Field.SelectedValue + " " + DDL_OPT1_Operator.SelectedValue.Replace("<<>>", TXT_OPT1_KeyWords.Text.Replace("'", "''")) + " "; }
                if (DDL_OPT2_Field.SelectedIndex > 0 && DDL_OPT2_Operator.SelectedIndex > 0)
                { JoinClause += DDL_OPT2_Field.SelectedValue + " " + DDL_OPT2_Operator.SelectedValue.Replace("<<>>", TXT_OPT2_KeyWords.Text.Replace("'", "''")) + " "; }
                if (DDL_OPT3_Field.SelectedIndex > 0 && DDL_OPT3_Operator.SelectedIndex > 0)
                { JoinClause += DDL_OPT3_Field.SelectedValue + " " + DDL_OPT3_Operator.SelectedValue.Replace("<<>>", TXT_OPT3_KeyWords.Text.Replace("'", "''")) + " "; }

                //========== Declarions ==========//
                Label Lbl_Reruns = (Label)e.Item.FindControl("Lbl_Reruns");
                Label Lbl_RedState = (Label)e.Item.FindControl("Lbl_RedState");
                Label Lbl_RedStatePer100 = (Label)e.Item.FindControl("Lbl_RedStatePer100");
                Label Lbl_UniqueJobs = (Label)e.Item.FindControl("Lbl_UniqueJobs");

                DataRowView DRV = (DataRowView)e.Item.DataItem;

                #region Calculations

                //================ Initialzing SqlCommand ============//
                SqlCommand Cmd = new SqlCommand();

                //================ Get Red State ============//
                Cmd.CommandText = "SELECT ";
                Cmd.CommandText += "COUNT(CPDStat.CPDeviceStatusID) as RedState ";
                if (DRV["Measurements"].ToString() == "0")
                { Cmd.CommandText += ", 0 "; }
                else
                { Cmd.CommandText += ", (( CONVERT(DECIMAL(18,2),COUNT(CPDStat.CPDeviceStatusID)) / " + DRV["Measurements"].ToString() + " ) * 100) as RedStatePercentage "; }
                Cmd.CommandText += "FROM CPDeviceStatus CPDStat ";
                Cmd.CommandText += "WHERE CPDStat.CPDeviceID IN (SELECT CPD.CPDeviceID FROM CPDevice CPD WHERE CPD.CPDongleID = " + DRV["CPDongleID"].ToString() + " ";
                if (DRV["MeasurementTypeID"].ToString() == "") { Cmd.CommandText += "AND CPD.MeasurementTypeID = 0 "; } else { Cmd.CommandText += "AND CPD.MeasurementTypeID = " + DRV["MeasurementTypeID"].ToString() + " "; }
                Cmd.CommandText += ") AND CPDStat.CPDeviceStatusTypeID = 4 ";
                Cmd.CommandText += "AND ( CPDStat.StatusDT BETWEEN  '" + TXT_FromDate.Text + "' and '" + Convert.ToDateTime(TXT_ToDate.Text).AddDays(1).ToShortDateString() + "') ";
                Cmd.CommandText += "; ";


                Cmd.CommandText += "SELECT ISNULL(SUM(ReRuns) , 0) as ReRuns FROM ( ";
                Cmd.CommandText += "SELECT ";
                Cmd.CommandText += "ISNULL(COUNT(PMD.JOB) - 1 ,0) as ReRuns ";
                Cmd.CommandText += "FROM CPMeasurement PMD ";
                Cmd.CommandText += JoinClause;
                Cmd.CommandText += "WHERE PMD.CPDongleID = " + DRV["CPDongleID"].ToString() + " ";
                Cmd.CommandText += "AND ( PMD.CreatedDT BETWEEN  '" + TXT_FromDate.Text + "' and '" + Convert.ToDateTime(TXT_ToDate.Text).AddDays(1).ToShortDateString() + "') ";
                Cmd.CommandText += "AND ( ISNULL(PMD.JOB,'') != '') ";
                if (DRV["MeasurementTypeID"].ToString() == "") { Cmd.CommandText += "AND PMD.CPMeasurementTypeID = 0 "; } else { Cmd.CommandText += "AND PMD.CPMeasurementTypeID = " + DRV["MeasurementTypeID"].ToString() + " "; }
                Cmd.CommandText += "GROUP BY PMD.JOB ";
                Cmd.CommandText += "HAVING COUNT(PMD.JOB) > 1 ";
                Cmd.CommandText += ") AS A; ";

                Cmd.CommandText += "SELECT ";
                Cmd.CommandText += "COUNT(DISTINCT(PMD.Job)) AS UniqueJobs ";
                Cmd.CommandText += "FROM CPMeasurement PMD ";
                Cmd.CommandText += "WHERE PMD.CPDongleID = " + DRV["CPDongleID"].ToString() + " ";
                Cmd.CommandText += "AND ( PMD.CreatedDT BETWEEN  '" + TXT_FromDate.Text + "' and '" + Convert.ToDateTime(TXT_ToDate.Text).AddDays(1).ToShortDateString() + "') ";
                Cmd.CommandText += "AND ( ISNULL(PMD.JOB,'') != '') ";
                if (DRV["MeasurementTypeID"].ToString() == "") { Cmd.CommandText += "AND PMD.CPMeasurementTypeID = 0 "; } else { Cmd.CommandText += "AND PMD.CPMeasurementTypeID = " + DRV["MeasurementTypeID"].ToString() + " "; }
                Cmd.CommandText += "; ";

                Cmd.CommandType = CommandType.Text;

                DataSet Dset = (new Fuji.DataAccess.Database()).GetDataSet(Cmd);
                #endregion

                Lbl_RedState.Text = Dset.Tables[0].Rows[0]["RedState"].ToString();
                Lbl_RedStatePer100.Text = Convert.ToInt32(Dset.Tables[0].Rows[0]["RedStatePercentage"]).ToString();
                Lbl_Reruns.Text = Dset.Tables[1].Rows[0]["Reruns"].ToString();
                Lbl_UniqueJobs.Text = Dset.Tables[2].Rows[0]["UniqueJobs"].ToString();
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
            DDL_Branch.Items.Insert(0, new ListItem("--- All ---", "0"));
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
            DDL_DongleNo.Items.Insert(0, new ListItem("--- All ---", "0"));
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void FillDates(object sender, EventArgs e)
    {
        try
        {
            //========== If No device selected clear ==========//
            if (DDL_Branch.SelectedIndex == 0)
            { TXT_ToDate.Text = ""; TXT_FromDate.Text = ""; }

            //========== Declarations ==========//
            SqlCommand Cmd = new SqlCommand();

            if (DDL_Company.SelectedValue != "0" && DDL_Branch.SelectedValue == "0" && DDL_DongleNo.SelectedValue == "0")
            {
                Cmd.CommandText = "SELECT CONVERT(VARCHAR, MIN(PMD.CreatedDT), 101) AS MINDATE, CONVERT(VARCHAR, MAX(PMD.CreatedDT), 101)  AS MAXDATE ";
                Cmd.CommandText += "FROM CPMeasurement PMD ";
                Cmd.CommandText += "INNER JOIN CPDongle CPD ON PMD.CPDongleID = CPD.CPDongleID ";
                Cmd.CommandText += "INNER JOIN Branch B ON CPD.BranchID = B.BranchID ";
                Cmd.CommandText += "INNER JOIN Company C ON B.CompanyID = C.CompanyID ";
                Cmd.CommandText += "WHERE (B.CompanyID = " + DDL_Company.SelectedValue + ") ";

            }
            else if (DDL_Company.SelectedValue != "0" && DDL_Branch.SelectedValue != "0" && DDL_DongleNo.SelectedValue == "0")
            {
                Cmd.CommandText = "SELECT CONVERT(VARCHAR, MIN(CPMeasurement.CreatedDT), 101) AS MINDATE, CONVERT(VARCHAR, MAX(CPMeasurement.CreatedDT), 101)" +
               "AS MAXDATE FROM CPMeasurement INNER JOIN CPDongle ON CPMeasurement.CPDongleID = CPDongle.CPDongleID INNER JOIN " +
                         "Branch ON CPDongle.BranchID = " + DDL_Branch.SelectedValue;
            }
            else
            {
                Cmd.CommandText = "SELECT CONVERT(VARCHAR, MIN(CPMeasurement.CreatedDT), 101) AS MINDATE, CONVERT(VARCHAR, MAX(CPMeasurement.CreatedDT), 101) AS MAXDATE  ";
                Cmd.CommandText += "FROM CPMeasurement ";
                Cmd.CommandText += "INNER JOIN CPDongle AS CPD ON CPMeasurement.CPDongleID = CPD.CPDongleID ";
                Cmd.CommandText += "WHERE (CPD.CPDongleID = " + DDL_DongleNo.SelectedValue + ") ";
            }

            //========== Execute Query ==========//
            Cmd.CommandType = CommandType.Text;

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
    protected void FillDateBranch(object sender, EventArgs e)
    {
        try
        {
            FillBranch(null, null);
            FillDates(null, null);
        }
        catch (Exception Ex){throw Ex;}
    }
    protected void FillDateDongle(object sender, EventArgs e)
    {
        try
        {
            FillDongle(null, null);
            FillDates(null, null);
        }catch(Exception Ex){throw Ex;}
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

            DDL_OPT1_Field.Style.Add("width", "100%");
            DDL_OPT1_Operator.Style.Add("width", "100%");
            TXT_OPT1_KeyWords.Style.Add("width", "80%");

            DDL_OPT2_Field.Style.Add("width", "100%");
            DDL_OPT2_Operator.Style.Add("width", "100%");
            TXT_OPT2_KeyWords.Style.Add("width", "80%");

            DDL_OPT3_Field.Style.Add("width", "100%");
            DDL_OPT3_Operator.Style.Add("width", "100%");
            TXT_OPT3_KeyWords.Style.Add("width", "80%");

            DGProductivityDetail.Style.Add("width", "100%");
        }
        catch (Exception Ex) { throw Ex; }
    }
    #endregion

    #region Methods and Function
    protected void FillProductionDetail()
    {
        try
        {
            //========== Validations ==========//
            //if (DDL_Branch.SelectedIndex == 0)
            //{ LblError.Text = "Branch is required."; return; }

            //========== Join Clause ==========//
            string JoinClause = "";
            if (DDL_OPT1_Field.SelectedIndex > 0 && DDL_OPT1_Operator.SelectedIndex > 0)
            { JoinClause += DDL_OPT1_Field.SelectedValue + " " + DDL_OPT1_Operator.SelectedValue.Replace("<<>>", TXT_OPT1_KeyWords.Text.Replace("'", "''")) + " "; }
            if (DDL_OPT2_Field.SelectedIndex > 0 && DDL_OPT2_Operator.SelectedIndex > 0)
            { JoinClause += DDL_OPT2_Field.SelectedValue + " " + DDL_OPT2_Operator.SelectedValue.Replace("<<>>", TXT_OPT2_KeyWords.Text.Replace("'", "''")) + " "; }
            if (DDL_OPT3_Field.SelectedIndex > 0 && DDL_OPT3_Operator.SelectedIndex > 0)
            { JoinClause += DDL_OPT3_Field.SelectedValue + " " + DDL_OPT3_Operator.SelectedValue.Replace("<<>>", TXT_OPT3_KeyWords.Text.Replace("'", "''")) + " "; }

            SqlCommand Cmd = new SqlCommand();

            if (Fuji.Common.Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin)
            {
                Cmd.CommandText = "SELECT ";
                Cmd.CommandText += "B.BranchName ";
                Cmd.CommandText += ", (DO.CPSerialNUM +' '+ DO.DongleDescription) as DongleDescription ";
                Cmd.CommandText += ", DO.CPDongleID ";
                Cmd.CommandText += ", DO.CPSerialNUM AS Dongle ";
                Cmd.CommandText += ", CPD.MeasurementTypeID ";
                Cmd.CommandText += ", (CASE CPD.MeasurementTypeID WHEN 1 THEN 'Color' WHEN 2 THEN 'Density' WHEN 3 THEN 'Plate' END) AS Category ";
                Cmd.CommandText += ",  COUNT(PMD.CPMeasurementTypeID) AS Measurements ";
                //Cmd.CommandText += ", COUNT(DISTINCT(PMD.Job)) AS UniqueJobs ";
                Cmd.CommandText += ", SUM(CASE WHEN ISNULL(PMD.JOB , '') = '' THEN 1 ELSE 0 END) AS NoJob ";
                Cmd.CommandText += ", CONVERT(Decimal(18, 2), CONVERT(Decimal, SUM(CASE ISNULL(PMD.Status, 0) WHEN 0 THEN 1 ELSE 0 END)) / CONVERT(Decimal, COUNT(PMD.CPMeasurementID)) * 100)  AS ErrorRate  ";
                Cmd.CommandText += ", CONVERT( VARCHAR , CONVERT(Decimal(18, 2), CONVERT(Decimal, SUM(CASE ISNULL(PMD.Status, 0) WHEN 0 THEN 1 ELSE 0 END)) / CONVERT(Decimal, COUNT(PMD.CPMeasurementID)) * 100) ) + '%' AS ErrorRate1  ";
                //Cmd.CommandText += ", Count(PMD.JOB) as Reruns ";
                Cmd.CommandText += "FROM Company AS C ";
                Cmd.CommandText += "INNER JOIN  Branch AS B ON C.CompanyID = B.CompanyID  ";
                Cmd.CommandText += "INNER JOIN CPDongle AS DO ON DO.BranchID = B.BranchID ";
                Cmd.CommandText += "INNER JOIN CPDevice AS CPD ON CPD.CPDongleID = DO.CPDongleID ";
                Cmd.CommandText += "INNER JOIN CPMeasurement AS PMD ON CPD.CPDeviceID = PMD.CPDeviceID AND ISNULL( PMD.IsUsed , 1 ) = 1 ";
                Cmd.CommandText += JoinClause;
                Cmd.CommandText += "WHERE B.CompanyID = " + DDL_Company.SelectedValue + " ";
                if (DDL_Branch.SelectedIndex > 0) { Cmd.CommandText += "AND B.BranchID = " + DDL_Branch.SelectedValue + " "; }
                if (DDL_DongleNo.SelectedIndex > 0) { Cmd.CommandText += "AND DO.CPDongleID = " + DDL_DongleNo.SelectedValue + " "; }
                Cmd.CommandText += "AND ( PMD.CreatedDT BETWEEN  '" + TXT_FromDate.Text + "' and '" + Convert.ToDateTime(TXT_ToDate.Text).AddDays(1).ToShortDateString() + "') ";
                Cmd.CommandText += "AND ISNULL( PMD.IsUsed , 1 ) = 1 ";
                Cmd.CommandText += "GROUP BY DO.CPDongleID, B.BranchName, DO.DongleDescription, DO.CPSerialNUM, CPD.MeasurementTypeID  ";
                Cmd.CommandText += "ORDER By " + LBL_SortExpression1.Text + " " + LBL_SortOrder1.Text + ";";
            }
            else
            {
                Cmd.CommandText = "SELECT ";
                Cmd.CommandText += "vw1.BranchName  ";
                Cmd.CommandText += ", (vw1.CPSerialNUM +' '+ DongleDescription) AS DongleDescription  ";
                Cmd.CommandText += ", vw1.CPDongleID   ";
                Cmd.CommandText += ", vw1.CPSerialNUM AS Dongle  ";
                Cmd.CommandText += ", vw1.MeasurementTypeID   ";
                Cmd.CommandText += ", vw1.MeasurementTypeIDDesc AS Category  ";
                Cmd.CommandText += ", COUNT(vw1.MeasurementTypeID) AS Measurements  ";
                Cmd.CommandText += ", SUM(CASE WHEN ISNULL(PMD.JOB , '') = '' THEN 1 ELSE 0 END) AS NoJob  ";
                Cmd.CommandText += ", CONVERT(Decimal(18, 2), CONVERT(Decimal, SUM(CASE ISNULL(PMD.Status, 0) WHEN 0 THEN 1 ELSE 0 END)) / CONVERT(Decimal, COUNT(PMD.CPMeasurementID)) * 100)  AS ErrorRate  ";
                Cmd.CommandText += ", CONVERT( VARCHAR , CONVERT(Decimal(18, 2), CONVERT(Decimal, SUM(CASE ISNULL(PMD.Status, 0) WHEN 0 THEN 1 ELSE 0 END)) / CONVERT(Decimal, COUNT(PMD.CPMeasurementID)) * 100) ) + '%' AS ErrorRate1  ";
                Cmd.CommandText += "FROM CPV.vwGetAllDevicesPerUsers vw1  ";
                Cmd.CommandText += "INNER JOIN CPMeasurement AS PMD ON PMD.CPDeviceID = vw1.CPDeviceID  ";
                Cmd.CommandText += JoinClause;
                Cmd.CommandText += "WHERE vw1.TaskeroUserID = " + Fuji.Common.Sessions.UserID + "  AND vw1.CompanyID = " + DDL_Company.SelectedValue + " ";
                if (DDL_Branch.SelectedIndex > 0)
                { Cmd.CommandText += "AND vw1.BranchID = " + DDL_Branch.SelectedValue + " "; }
                if (DDL_DongleNo.SelectedIndex > 0) { Cmd.CommandText += "AND vw1.CPDongleID = " + DDL_DongleNo.SelectedValue + " "; }
                Cmd.CommandText += "AND ( PMD.CreatedDT BETWEEN  '" + TXT_FromDate.Text + "' and '" + Convert.ToDateTime(TXT_ToDate.Text).AddDays(1).ToShortDateString() + "') ";
                Cmd.CommandText += "GROUP BY vw1.CPDongleID, vw1.BranchName, vw1.DongleDescription, vw1.CPSerialNUM, vw1.MeasurementTypeID , vw1.MeasurementTypeIDDesc  ";
                Cmd.CommandText += "ORDER By " + LBL_SortExpression1.Text + " " + LBL_SortOrder1.Text + ";";

            }
            Cmd.CommandType = CommandType.Text;

            DataTable DT = (new Fuji.DataAccess.Database()).GetDataTable(Cmd);
            DT.TableName = "ProductivityDetail";

            DGProductivityDetail.DataSource = DT;
            Fuji.Common.CommonFunctions.SetDataGridDefaultSettings(DGProductivityDetail);
            DGProductivityDetail.AllowPaging = false;
            DGProductivityDetail.DataBind();

            ViewState["DT_ProductionDetail"] = DT;
        }
        catch (Exception Ex) { throw Ex; }
    }
    #endregion
}

