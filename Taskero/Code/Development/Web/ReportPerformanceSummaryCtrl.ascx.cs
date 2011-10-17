#region System
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
#endregion

#region Others
using System.Data.SqlClient;
using Fuji.Common;
using Fuji.Business;
using Fuji.DataAccess;
#endregion

public partial class ReportPerformanceSummaryCtrl : ControlBase
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
                //========== See If Print Buyer ==========//
                if (Fuji.Common.Sessions.RoleDesc == Enums.RoleType.PrintBuyer)
                {
                    //========== The Print Buyers ==========//
                    TD_PBTitle.Visible = true;
                    TD_PBText.Visible = true;
                    TD_PBText.InnerHtml = Sessions.FirstName + " " + Sessions.LastName;

                    ClearAndAddCorrectOption(DDL_OPT1_Field, 1);
                    ClearAndAddCorrectOption(DDL_OPT2_Field, 2);
                    ClearAndAddCorrectOption(DDL_OPT3_Field, 3);
                }

                //========== Fill Variables to Form ==========//
                SetLabels();

                //========== Hide the Section ==========//
                PNL_Search.Style.Add("display", "");
                PNL_Result.Style.Add("display", "none");


                //========== Fill Search View ==========//
                FillCompany(null, null);
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

            //========== Clear Main DSet ==========//
            Fuji.Common.Sessions.SetSessions("DSet_PerformanceReport", null);

            //========== Validations ==========//
            if (DDL_Company.SelectedIndex == 0)
            { LblError.Text = "Company is required."; return; }
            //if (DDL_Branch.SelectedIndex == 0)
            //{ LblError.Text = "Branch is required."; return; }
            if (TXT_FromDate.Text == "")
            { LblError.Text = "From Date is required."; return; }
            if (TXT_ToDate.Text == "")
            { LblError.Text = "To Date is required."; return; }
            LTRL_DateRange.Text = TXT_FromDate.Text + " - " + TXT_ToDate.Text;

            //========== Filling Label ==========//
            lblReportTitle.Text = "Color Specification Adherence Report";
            LTRL_CompanyName.Text = DDL_Company.SelectedItem.Text;
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

                //========== Sets Everything for PDF Generation ==========//
                PDFSettings();
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
            lblReportTitle.Text = "Performance Summary Report(Search Criteria)";
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
            objReportPDF.ReportType = (int)Fuji.Common.Enums.enumReportType.ReportPerformanceSummary;
            objReportPDF.PDFPath = Server.MapPath("UploadFiles/Report_PDF/");
            objReportPDF.PDF_NAME = Sessions.UserID + "_ReportPerformanceSummary.PDF";//LoginID

            if (ViewState["DT_BasicInfo"] != null) { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_BasicInfo"]); }
            if (ViewState["DT_SearchCriteria"] != null) { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_SearchCriteria"]); }

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
    protected void RPT_Company_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //==========  Default View ==========//
                DataRowView DRV = (DataRowView)e.Item.DataItem;

                //========== Get The Objects ==========//
                DataGrid DG_ColorSpec = (DataGrid)e.Item.FindControl("DG_ColorSpec");
                SetDataGridDefaultSettings(DG_ColorSpec);

                //==========  Get The Default View ==========//                
                DataSet DSet = (DataSet)Fuji.Common.Sessions.GetSession("DSet_PerformanceReport");

                //==========  Fill Color Specs ==========//
                string  _SortExpression = "";
                string _SortOrder = "";
                if( ViewState[DG_ColorSpec.ClientID + "-SortExpression"] != null){ _SortExpression = ViewState[DG_ColorSpec.ClientID + "-SortExpression"].ToString(); }
                if (ViewState[DG_ColorSpec.ClientID + "-SortOrder"] != null) { _SortOrder = ViewState[DG_ColorSpec.ClientID + "-SortOrder"].ToString(); }

                DSet.Tables[1].DefaultView.RowFilter = "BranchID = " + DRV["BranchID"];
                if (_SortExpression.Length > 0) { DSet.Tables[1].DefaultView.Sort = _SortExpression + " " + _SortOrder; }
                
                DataView DV_Color = DSet.Tables[1].DefaultView;
                if (DV_Color.Count > 0)
                {
                    DG_ColorSpec.ItemCreated += new DataGridItemEventHandler(DG_ColorSpec_ItemCreated);
                    DG_ColorSpec.AllowSorting = false;
                    //DG_ColorSpec.SortCommand +=new DataGridSortCommandEventHandler(DG_ColorSpec_SortCommand);

                    DG_ColorSpec.DataSource = DV_Color;
                    DG_ColorSpec.DataBind();
                }
                else { DG_ColorSpec.Visible = false; }
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void DG_ColorSpec_ItemCreated(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                //========== Declaring Objects ==========//
                DataGridItem objDataGridItem = new DataGridItem(0, 0, ListItemType.Header);

                //Add Fist Sell
                TableCell objTableCell = new TableCell();
                objTableCell.ColumnSpan = 6;
                objTableCell.Text = "&nbsp;";
                objDataGridItem.Cells.Add(objTableCell);
                ((DataGrid)sender).Controls[0].Controls.AddAt(0, objDataGridItem);


                //Add Fist Substrate
                objTableCell = new TableCell();
                objTableCell.ColumnSpan = 2;
                objTableCell.Text = "Substrate";
                objDataGridItem.Cells.Add(objTableCell);
                ((DataGrid)sender).Controls[0].Controls.AddAt(0, objDataGridItem);

                //Add Fist Cyan
                objTableCell = new TableCell();
                objTableCell.ColumnSpan = 2;
                objTableCell.Text = "Cyan";
                objDataGridItem.Cells.Add(objTableCell);
                ((DataGrid)sender).Controls[0].Controls.AddAt(0, objDataGridItem);

                //Add Fist Magenta
                objTableCell = new TableCell();
                objTableCell.ColumnSpan = 2;
                objTableCell.Text = "Magenta";
                objDataGridItem.Cells.Add(objTableCell);
                ((DataGrid)sender).Controls[0].Controls.AddAt(0, objDataGridItem);

                //Add Fist Yellow
                objTableCell = new TableCell();
                objTableCell.ColumnSpan = 2;
                objTableCell.Text = "Yellow";
                objDataGridItem.Cells.Add(objTableCell);
                ((DataGrid)sender).Controls[0].Controls.AddAt(0, objDataGridItem);

                //Add Fist Black
                objTableCell = new TableCell();
                objTableCell.ColumnSpan = 2;
                objTableCell.Text = "Black";
                objDataGridItem.Cells.Add(objTableCell);
                ((DataGrid)sender).Controls[0].Controls.AddAt(0, objDataGridItem);

                //Add Fist Gray
                objTableCell = new TableCell();
                objTableCell.ColumnSpan = 2;
                objTableCell.Text = "Gray";
                objDataGridItem.Cells.Add(objTableCell);
                ((DataGrid)sender).Controls[0].Controls.AddAt(0, objDataGridItem);

                //Add Fist Humidity
                objTableCell = new TableCell();
                objTableCell.ColumnSpan = 2;
                objTableCell.Text = "Humidity";
                objDataGridItem.Cells.Add(objTableCell);
                ((DataGrid)sender).Controls[0].Controls.AddAt(0, objDataGridItem);

                //Add Fist Temp
                objTableCell = new TableCell();
                objTableCell.ColumnSpan = 2;
                objTableCell.Text = "Temp";
                objDataGridItem.Cells.Add(objTableCell);
                ((DataGrid)sender).Controls[0].Controls.AddAt(0, objDataGridItem);
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void DG_Branch_ItemCreated(object sender, DataGridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                //========== Declaring Objects ==========//
                DataGridItem objDataGridItem = new DataGridItem(0, 0, ListItemType.Header);

                //Add Fist Sell
                TableCell objTableCell = new TableCell();
                objTableCell.ColumnSpan = 6;
                objTableCell.Text = "&nbsp;";
                objDataGridItem.Cells.Add(objTableCell);
                ((DataGrid)sender).Controls[0].Controls.AddAt(0, objDataGridItem);


                //Add Fist Substrate
                objTableCell = new TableCell();
                objTableCell.ColumnSpan = 2;
                objTableCell.Text = "Substrate";
                objDataGridItem.Cells.Add(objTableCell);
                ((DataGrid)sender).Controls[0].Controls.AddAt(0, objDataGridItem);

                //Add Fist Cyan
                objTableCell = new TableCell();
                objTableCell.ColumnSpan = 2;
                objTableCell.Text = "Cyan";
                objDataGridItem.Cells.Add(objTableCell);
                ((DataGrid)sender).Controls[0].Controls.AddAt(0, objDataGridItem);

                //Add Fist Magenta
                objTableCell = new TableCell();
                objTableCell.ColumnSpan = 2;
                objTableCell.Text = "Magenta";
                objDataGridItem.Cells.Add(objTableCell);
                ((DataGrid)sender).Controls[0].Controls.AddAt(0, objDataGridItem);

                //Add Fist Yellow
                objTableCell = new TableCell();
                objTableCell.ColumnSpan = 2;
                objTableCell.Text = "Yellow";
                objDataGridItem.Cells.Add(objTableCell);
                ((DataGrid)sender).Controls[0].Controls.AddAt(0, objDataGridItem);

                //Add Fist Black
                objTableCell = new TableCell();
                objTableCell.ColumnSpan = 2;
                objTableCell.Text = "Black";
                objDataGridItem.Cells.Add(objTableCell);
                ((DataGrid)sender).Controls[0].Controls.AddAt(0, objDataGridItem);

                //Add Fist Gray
                objTableCell = new TableCell();
                objTableCell.ColumnSpan = 2;
                objTableCell.Text = "Gray";
                objDataGridItem.Cells.Add(objTableCell);
                ((DataGrid)sender).Controls[0].Controls.AddAt(0, objDataGridItem);

                //Add Fist Humidity
                objTableCell = new TableCell();
                objTableCell.ColumnSpan = 2;
                objTableCell.Text = "Humidity";
                objDataGridItem.Cells.Add(objTableCell);
                ((DataGrid)sender).Controls[0].Controls.AddAt(0, objDataGridItem);

                //Add Fist Temp
                objTableCell = new TableCell();
                objTableCell.ColumnSpan = 2;
                objTableCell.Text = "Temp";
                objDataGridItem.Cells.Add(objTableCell);
                ((DataGrid)sender).Controls[0].Controls.AddAt(0, objDataGridItem);
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void DG_Brnach_SortCommand(object source, DataGridSortCommandEventArgs e)
    {
        try
        {
            //========== Set Sort Expression ==========//
            LTRL_Branch_SortExpression.Text = e.SortExpression;
            if (LTRL_Branch_SortExpression.Text == e.SortExpression)
            {
                if (LTRL_Branch_SortOrder.Text == "")
                { LTRL_Branch_SortOrder.Text = "DESC"; }
                else
                { LTRL_Branch_SortOrder.Text = ""; }
            }
            else
            { LTRL_Branch_SortOrder.Text = ""; }


            //========== Get The DataSet ==========//
            DataSet DSet = (DataSet)Fuji.Common.Sessions.GetSession("DSet_PerformanceReport");
            DSet.Tables[0].DefaultView.Sort = LTRL_Branch_SortExpression.Text + " " + LTRL_Branch_SortOrder.Text;

            //========== Bind Grid ==========//
            SetDataGridDefaultSettings(DG_Brnach);
            DG_Brnach.DataSource = DSet.Tables[0].DefaultView;
            DG_Brnach.DataBind();

            //========== Bind Repeater ==========//
            RPT_Company.DataSource = DSet.Tables[0].DefaultView;
            RPT_Company.DataBind();

            //========== Set the PDF ==========//
            PDFSettings();
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void DG_ColorSpec_SortCommand(object source, DataGridSortCommandEventArgs e)
    {
        try
        {
           //========== Get The Object ==========//
            DataGrid DG_ColorSpec = ((DataGrid)source);

            //========== Sort Expression ==========//
            string _SortExpression = e.SortExpression;
            string _SortOrder = "";
            if (ViewState[DG_ColorSpec.ClientID + "-SortOrder"] != null){ _SortOrder = ViewState[DG_ColorSpec.ClientID + "-SortOrder"].ToString(); }
            if (_SortExpression == e.SortExpression)
            {
                if (_SortOrder == "")
                { _SortOrder = "DESC"; }
                else
                { _SortOrder = ""; }
            }
            else
            { _SortOrder = ""; }

            ViewState[DG_ColorSpec.ClientID + "-SortExpression"] = _SortExpression;
            ViewState[DG_ColorSpec.ClientID + "-SortOrder"] = _SortOrder;

            //========== Get The DataSet ==========//
            DataSet DSet = (DataSet)Fuji.Common.Sessions.GetSession("DSet_PerformanceReport");
            DSet.Tables[0].DefaultView.Sort = LTRL_Branch_SortExpression.Text + " " + LTRL_Branch_SortOrder.Text;

            //========== Bind Grid ==========//
            SetDataGridDefaultSettings(DG_Brnach);
            DG_Brnach.DataSource = DSet.Tables[0].DefaultView;
            DG_Brnach.DataBind();

            //========== Bind Repeater ==========//
            RPT_Company.DataSource = DSet.Tables[0].DefaultView;
            RPT_Company.DataBind();

            //========== Set the PDF ==========//
            PDFSettings();

        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    #endregion

    #region Set Datagrid Style
    private void SetDataGridDefaultSettings(DataGrid DGrid)
    {
        try
        {
            DGrid.AllowPaging = false;
            DGrid.AllowSorting = true;
            DGrid.AutoGenerateColumns = false;
            DGrid.PageSize = 10;
            DGrid.PagerStyle.Mode = System.Web.UI.WebControls.PagerMode.NumericPages;
            DGrid.CssClass = "DATAGRID";
            DGrid.EditItemStyle.CssClass = "EDITITEMSTYLE";
            DGrid.FooterStyle.CssClass = "FOOTERSTYLE";
            DGrid.HeaderStyle.CssClass = "HEADERSTYLE";
            DGrid.SelectedItemStyle.CssClass = "SELECTEDITEMSTYLE";
            DGrid.AlternatingItemStyle.CssClass = "ALTERNATINGITEMSTYLE";
            DGrid.ItemStyle.CssClass = "ITEMSTYLE";
            DGrid.PagerStyle.CssClass = "PAGERSTYLE";
            DGrid.AlternatingItemStyle.CssClass = "ALTERNATINGITEMSTYLE";
            DGrid.Style.Add("width", "100%");
            DGrid.Attributes.Add("align", "right");
        }
        catch (Exception Ex)
        {
            throw Ex;
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
            objUserBranch.CompanyID = Convert.ToInt32(DDL_Company.SelectedValue);

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

            //========== Fill Dates By Company ===========//
            FillDates(null, null);
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }
    protected void FillDates(object sender, EventArgs e)
    {
        try
        {
            //==========  Declaring Objects ==========//
            DataTable DT;

            if (DDL_Branch.SelectedIndex == 0)
            {
                //==========  Get Data Table and Bind ==========//
                CompanyData objCompany = new CompanyData();
                objCompany.CompanyID = Convert.ToInt32(DDL_Company.SelectedValue);
                DT = objCompany.uspGetDateByCompanyID();
            }
            else
            {
                //==========  Get Data Table and Bind ==========//
                BranchData objBranch = new BranchData();
                objBranch.BranchID = Convert.ToInt32(DDL_Branch.SelectedValue);
                DT = objBranch.uspGetDateByBranchID();
            }

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
                if (DDL_Branch.SelectedIndex == 0)
                { Cmd.CommandText += "WHERE PMD.CPDeviceID IN ( SELECT CPDeviceID from CPDevice WHERE CPDongleID IN (SELECT CPDongleID FROM CPDongle WHERE BranchID IN ( Select BranchID FROM Branch WHERE CompanyID = " + DDL_Company.SelectedValue + " ) ) ) "; }
                else
                { Cmd.CommandText += "WHERE PMD.CPDeviceID IN ( SELECT CPDeviceID from CPDevice WHERE CPDongleID IN (SELECT CPDongleID FROM CPDongle WHERE BranchID = " + DDL_Branch.SelectedValue + " ) ) "; }
                Cmd.CommandText += " AND ( PMD.CreatedDT BETWEEN  '" + TXT_FromDate.Text + "' and '" + Convert.ToDateTime(TXT_ToDate.Text).AddDays(1).ToShortDateString() + "') ";
                Cmd.CommandText += " AND ISNULL( PMD.IsUsed , 1 ) = 1 ";
                Cmd.CommandText += " AND PMD.CPMeasurementTypeID = 1 ";
            }
            else //========== Query Only For Other User type ==========//
            {
                Cmd.CommandText = "SELECT ";
                Cmd.CommandText += "DISTINCT(PMD.CPMeasurementGUID) as CPMeasurementGUID ";
                Cmd.CommandText += "FROM  CPMeasurement PMD ";
                Cmd.CommandText += "INNER JOIN CPV.vwGetAllDevicesPerUsers vw1 ON vw1.CPDeviceID = PMD.CPDeviceID ";
                Cmd.CommandText += JoinClause;
                if (DDL_Branch.SelectedIndex == 0)
                { Cmd.CommandText += "WHERE vw1.TaskeroUserID = " + Fuji.Common.Sessions.UserID + " AND vw1.CompanyID = " + DDL_Company.SelectedValue + " "; }
                else
                { Cmd.CommandText += "WHERE vw1.TaskeroUserID = " + Fuji.Common.Sessions.UserID + " AND vw1.BranchID = " + DDL_Branch.SelectedValue + " "; }
                Cmd.CommandText += "AND ISNULL( PMD.IsUsed , 1 ) = 1 ";
                Cmd.CommandText += "AND PMD.CPMeasurementTypeID = 1 ";
                Cmd.CommandText += "AND 1 = ( CASE vw1.RoleID WHEN 5 THEN CASE WHEN vw1.Customer = PMD.Customer THEN 1 ELSE 0 END ELSE  1 END ) ";
                Cmd.CommandText += "AND ( PMD.CreatedDT BETWEEN  '" + TXT_FromDate.Text + "' and '" + Convert.ToDateTime(TXT_ToDate.Text).AddDays(1).ToShortDateString() + "') ";
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
            AverageDeltaE();
            AveragePlateDaviation();
            ErrorRate();
            FillSearchResult();
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
    private void FillSearchResult()
    {
        try
        {
            //========== Fill Company Repeater ==========//
            if (Fuji.Common.Sessions.GetSession("DSet_PerformanceReport") == null)
            {
                Server.ScriptTimeout = 56000;

                //========== Get GUIDs and comma separated values ==========//
                string strGUIDs = LTRL_CPMeasurementGUID.Text.Replace("'", String.Empty);

                //========== Declarations ==========//
                SqlCommand Cmd = new SqlCommand();

                if (Fuji.Common.Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin)
                { Cmd.CommandText = "CPV.uspColorSpecificationAdherenceReport_Admin"; }
                else
                {
                    Cmd.CommandText = "CPV.uspColorSpecificationAdherenceReport";
                    Cmd.Parameters.AddWithValue("@TaskeroUserID", Fuji.Common.Sessions.UserID);
                }
                Cmd.Parameters.AddWithValue("@CompanyID", DDL_Company.SelectedValue);
                Cmd.Parameters.AddWithValue("@BranchID", DDL_Branch.SelectedValue);
                Cmd.Parameters.AddWithValue("@StartDate", TXT_FromDate.Text);
                Cmd.Parameters.AddWithValue("@EndDate", TXT_ToDate.Text);
                Cmd.Parameters.AddWithValue("@CPMeasurementGUIDs", strGUIDs);
                Cmd.CommandType = CommandType.StoredProcedure;

                Fuji.Common.Sessions.SetSessions("DSet_PerformanceReport", (new Fuji.DataAccess.Database()).GetDataSetExtendedTimeout(Cmd));
            }
            DataSet DSet = (DataSet)Fuji.Common.Sessions.GetSession("DSet_PerformanceReport");

            //========== Bind Grid ==========//
            SetDataGridDefaultSettings(DG_Brnach);
            DG_Brnach.DataSource = DSet.Tables[0];
            DG_Brnach.DataBind();

            //========== Fills The details for the comapany ==========//
            FillCompanyDetails();

            //========== Bind Repeater ==========//
            RPT_Company.DataSource = DSet.Tables[0];
            RPT_Company.DataBind();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillCompanyDetails()
    {
        try
        {
            //========== Declarations ===========//
            int _Reads = 0; int _Total_Errors = 0;
            decimal _AVE_DE = 0, _MAX_DE = 0;
            decimal _SUBSTRATE_DE = 0, _SUBSTRATE_DH = 0;
            decimal _CYAN_DE = 0, _CYAN_DH = 0;
            decimal _MAGENTA_DE = 0, _MAGENTA_DH = 0;
            decimal _YELLOW_DE = 0, _YELLOW_DH = 0;
            decimal _BLACK_DE = 0, _BLACK_DH = 0;
            decimal _GRAY_DE = 0, _GRAY_DH = 0;
            decimal _HUMIDITY_AVE = 0, _HUMIDITY_STD = 0;
            decimal _TEMP_AVE = 0, _TEMP_STD = 0;

            for (int i = 0; i < DG_Brnach.Items.Count; i = i + 1)
            {
                _Reads += Convert.ToInt32(DG_Brnach.Items[i].Cells[1].Text);
                _Total_Errors += Convert.ToInt32(DG_Brnach.Items[i].Cells[3].Text);

                _AVE_DE += Convert.ToDecimal(DG_Brnach.Items[i].Cells[4].Text);
                _MAX_DE += Convert.ToDecimal(DG_Brnach.Items[i].Cells[5].Text);

                _SUBSTRATE_DE += Convert.ToDecimal(DG_Brnach.Items[i].Cells[6].Text);
                _SUBSTRATE_DH += Convert.ToDecimal(DG_Brnach.Items[i].Cells[7].Text);
                _CYAN_DE += Convert.ToDecimal(DG_Brnach.Items[i].Cells[8].Text);
                _CYAN_DH += Convert.ToDecimal(DG_Brnach.Items[i].Cells[9].Text);
                _MAGENTA_DE += Convert.ToDecimal(DG_Brnach.Items[i].Cells[10].Text);
                _MAGENTA_DH += Convert.ToDecimal(DG_Brnach.Items[i].Cells[11].Text);
                _YELLOW_DE += Convert.ToDecimal(DG_Brnach.Items[i].Cells[12].Text);
                _YELLOW_DH += Convert.ToDecimal(DG_Brnach.Items[i].Cells[13].Text);
                _BLACK_DE += Convert.ToDecimal(DG_Brnach.Items[i].Cells[14].Text);
                _BLACK_DH += Convert.ToDecimal(DG_Brnach.Items[i].Cells[15].Text);
                _GRAY_DE += Convert.ToDecimal(DG_Brnach.Items[i].Cells[16].Text);
                _GRAY_DH += Convert.ToDecimal(DG_Brnach.Items[i].Cells[17].Text);
                _HUMIDITY_AVE += Convert.ToDecimal(DG_Brnach.Items[i].Cells[18].Text);
                _HUMIDITY_STD += Convert.ToDecimal(DG_Brnach.Items[i].Cells[19].Text);
                _TEMP_AVE += Convert.ToDecimal(DG_Brnach.Items[i].Cells[20].Text);
                _TEMP_STD += Convert.ToDecimal(DG_Brnach.Items[i].Cells[21].Text);
            }

            //========== Assign Values ===========//
            LTRL_CD_CompanyName.Text = DDL_Company.SelectedItem.Text;

            LTRL_CD_READS.Text = _Reads.ToString();
            LTRL_CD_ERRORRATE.Text = Convert.ToDecimal((Convert.ToDecimal(_Total_Errors) / Convert.ToDecimal(_Reads)) * 100).ToString("N2");
            LTRL_CD_TOTAL_ERRORS.Text = _Total_Errors.ToString();

            LTRL_CD_AVG_DE.Text = CalculateAvg(_AVE_DE); LTRL_CD_MAX_DE.Text = CalculateAvg(_MAX_DE);

            LTRL_CD_SUBSTRATE_DE.Text = CalculateAvg(_SUBSTRATE_DE); LTRL_CD_SUBSTRATE_DH.Text = CalculateAvg(_SUBSTRATE_DH);
            LTRL_CD_CYAN_DE.Text = CalculateAvg(_CYAN_DE); LTRL_CD_CYAN_DH.Text = CalculateAvg(_CYAN_DH);
            LTRL_CD_MAGENTA_DE.Text = CalculateAvg(_MAGENTA_DH); LTRL_CD_MAGENTA_DH.Text = CalculateAvg(_MAGENTA_DH);
            LTRL_CD_YELLOW_DE.Text = CalculateAvg(_YELLOW_DE); LTRL_CD_YELLOW_DH.Text = CalculateAvg(_YELLOW_DH);
            LTRL_CD_BLACK_DE.Text = CalculateAvg(_BLACK_DE); LTRL_CD_BLACK_DH.Text = CalculateAvg(_BLACK_DH);
            LTRL_CD_GRAY_DE.Text = CalculateAvg(_GRAY_DE); LTRL_CD_GRAY_DH.Text = CalculateAvg(_GRAY_DH);
            LTRL_CD_HUMIDITY_AVE.Text = CalculateAvg(_HUMIDITY_AVE); LTRL_CD_HUMIDITY_STD.Text = CalculateAvg(_HUMIDITY_STD);
            LTRL_CD_TEMP_AVE.Text = CalculateAvg(_TEMP_AVE); LTRL_CD_TEMP_STD.Text = CalculateAvg(_TEMP_STD);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private string CalculateAvg(decimal _Val)
    {
        try
        {
            if (_Val == 0)  //changed by chirag on 06/16/2009 if 0 display no data available "-"
            {
                return "-";   
            }
            else
            {
                int _Count = DG_Brnach.Items.Count;
                return Convert.ToDecimal(_Val / _Count).ToString("N2");
            }
        }
        catch (Exception)
        {
            return "";
        }
    }
    private void ClearAndAddCorrectOption(DropDownList DDL_OPT_Field, int _DDL_Index)
    {
        try
        {
            DDL_OPT_Field.Items.Clear();
            DDL_OPT_Field.Items.Insert(0, new ListItem("---", ""));
            DDL_OPT_Field.Items.Insert(1, new ListItem("Job", "INNER JOIN CPMeasurement CP" + _DDL_Index + " ON CP" + _DDL_Index + ".CPMeasurementGUID = PMD.CPMeasurementGUID AND CP" + _DDL_Index + ".Job"));
            DDL_OPT_Field.Items.Insert(2, new ListItem("Info Text", "INNER JOIN CPMeasurement CP" + _DDL_Index + " ON CP" + _DDL_Index + ".CPMeasurementGUID = PMD.CPMeasurementGUID AND CP" + _DDL_Index + ".Info_Text"));
            DDL_OPT_Field.SelectedIndex = 0;
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
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
    private void PDFSettings()
    {
        try
        {
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
            DR_BasicInfo["DateRange_"] = LTRL_DateRange.Text.Trim();
            DR_BasicInfo["CompanyName_"] = DDL_Company.SelectedItem.Text;
            DR_BasicInfo["BranchName_"] = DDL_Branch.SelectedItem.Text;
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

            #region === Set the Tables for printing the branch === (##REPLACE_THE_BRACHES##)

            string Css = "GRIDEVEN";

            //==========  Get The Default View ==========//                
            DataSet DSet = (DataSet)Fuji.Common.Sessions.GetSession("DSet_PerformanceReport");

            string BranchHtml = "";

            #region Company Summary
            BranchHtml += "<tr><td style=\"font-size:15px\" align=\"center\">---------- Company Summary ----------</td></tr>";
            BranchHtml += "<tr>";
            BranchHtml += "<td style=\"padding:3px;background-color:#efefef;color:#333;border:1px solid #808080\"><b style=\"font-size:10px\">Company Summary</b><br />";
            BranchHtml += "<table style=\"width: 100%\" cellspacing=\"1\" cellpadding=\"0\" border=\"0\" align=\"center\" style=\"border:1px solid #888\">";
            BranchHtml += "<tr class=\"GRIDTITLE\"><td style=\"width: 40%\" class=\"GRIDTITLE\" align=\"left\" rowspan=\"2\">Company Name</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" rowspan=\"2\">Reads</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" rowspan=\"2\">Error<br />Rate</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" rowspan=\"2\">Total<br />Errors</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" rowspan=\"2\">Ave<br />DE</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" rowspan=\"2\">Max<br />DE</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Substrate</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Cyan</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Magenta</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Yellow</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Black</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Gray</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Humidity</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Temp</td></tr>";
            BranchHtml += "<tr class=\"GRIDTITLE\"><td class=\"GRIDTITLE\">DE</td><td class=\"GRIDTITLE\">DH</td><td class=\"GRIDTITLE\">DE</td><td class=\"GRIDTITLE\">DH</td><td class=\"GRIDTITLE\">DE</td><td class=\"GRIDTITLE\">DH</td><td class=\"GRIDTITLE\">DE</td><td class=\"GRIDTITLE\">DH</td><td class=\"GRIDTITLE\">DE</td><td class=\"GRIDTITLE\">DH</td><td class=\"GRIDTITLE\">DE</td><td class=\"GRIDTITLE\">DH</td><td class=\"GRIDTITLE\">AVE</td><td class=\"GRIDTITLE\">STD</td><td class=\"GRIDTITLE\">AVE</td><td class=\"GRIDTITLE\">STD</td></tr>";
            BranchHtml += "<tr class=\"" + Css + "\">";
            BranchHtml += "<td>" + LTRL_CD_CompanyName.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_READS.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_ERRORRATE.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_TOTAL_ERRORS.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_AVG_DE.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_MAX_DE.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_SUBSTRATE_DE.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_SUBSTRATE_DH.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_CYAN_DE.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_CYAN_DH.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_MAGENTA_DE.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_MAGENTA_DH.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_YELLOW_DE.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_YELLOW_DH.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_BLACK_DE.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_BLACK_DH.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_GRAY_DE.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_GRAY_DH.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_HUMIDITY_AVE.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_HUMIDITY_STD.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_TEMP_AVE.Text + "</td>";
            BranchHtml += "<td>" + LTRL_CD_TEMP_STD.Text + "</td>";
            BranchHtml += "</tr>";
            BranchHtml += "</table>";
            BranchHtml += "</td>";
            BranchHtml += "</tr>";
            BranchHtml += "<tr><td style=\"height: 5px\"></td></tr>";
            #endregion

            #region Branch Summary
            BranchHtml += "<tr><td style=\"padding:3px;background-color:#efefef;color:#333;border:1px solid #808080\"><b style=\"font-size:10px\">Branch Summary</b><br />";
            BranchHtml += "<table style=\"width: 100%\" cellspacing=\"1\" cellpadding=\"0\" border=\"0\" align=\"center\" style=\"border:1px solid #888\">";
            BranchHtml += "<tr class=\"GRIDTITLE\"><td style=\"width: 40%\" class=\"GRIDTITLE\" align=\"left\" rowspan=\"2\">Branch Name</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" rowspan=\"2\">Reads</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" rowspan=\"2\">Error<br />Rate</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" rowspan=\"2\">Total<br />Errors</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" rowspan=\"2\">Ave<br />DE</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" rowspan=\"2\">Max<br />DE</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Substrate</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Cyan</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Magenta</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Yellow</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Black</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Gray</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Humidity</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Temp</td></tr>";
            BranchHtml += "<tr class=\"GRIDTITLE\"><td class=\"GRIDTITLE\">DE</td><td class=\"GRIDTITLE\">DH</td><td class=\"GRIDTITLE\">DE</td><td class=\"GRIDTITLE\">DH</td><td class=\"GRIDTITLE\">DE</td><td class=\"GRIDTITLE\">DH</td><td class=\"GRIDTITLE\">DE</td><td class=\"GRIDTITLE\">DH</td><td class=\"GRIDTITLE\">DE</td><td class=\"GRIDTITLE\">DH</td><td class=\"GRIDTITLE\">DE</td><td class=\"GRIDTITLE\">DH</td><td class=\"GRIDTITLE\">AVE</td><td class=\"GRIDTITLE\">STD</td><td class=\"GRIDTITLE\">AVE</td><td class=\"GRIDTITLE\">STD</td></tr>";

            for (int i = 0; i < DG_Brnach.Items.Count; i = i + 1)
            {
                if (((i + 1) % 2) == 0)
                { Css = "GRIDEVEN"; }
                else
                { Css = "GRIDODD"; }

                BranchHtml += "<tr class=\"" + Css + "\">";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[0].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[1].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[2].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[3].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[4].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[5].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[6].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[7].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[8].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[9].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[10].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[11].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[12].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[13].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[14].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[15].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[16].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[17].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[18].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[19].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[20].Text + "</td>";
                BranchHtml += "<td>" + DG_Brnach.Items[i].Cells[21].Text + "</td>";
                BranchHtml += "</tr>";

            }
            BranchHtml += "</table>";
            BranchHtml += "<br />";
            #endregion

            #region Branch Details
            BranchHtml += "<tr><td style=\"font-size:15px\" align=\"center\">---------- Branch Detail ----------</td></tr>";
            BranchHtml += "<tr><td style=\"height: 5px\"></td></tr>";
            for (int i = 0; i < RPT_Company.Items.Count; i = i + 1)
            {
                //==========  Set The Titles ==========//
                BranchHtml += "<tr><td style=\"padding:3px;background-color:#efefef;color:#333;border:1px solid #808080\"><b style=\"font-size:10px\">" + DG_Brnach.Items[i].Cells[0].Text + "</b><br />";

                //==========  Find the Controls ==========//
                DataGrid DG_ColorSpec = (DataGrid)RPT_Company.Items[i].FindControl("DG_ColorSpec");

                //==========  Fill Color Specs ==========//
                if (DG_ColorSpec.Items.Count > 0)
                {
                    BranchHtml += "<table style=\"width: 100%\" cellspacing=\"1\" cellpadding=\"0\" border=\"0\" align=\"right\" style=\"border:1px solid #888\">";
                    BranchHtml += "<tr class=\"GRIDTITLE\"><td style=\"width: 40%\" class=\"GRIDTITLE\" align=\"left\" rowspan=\"2\">Device Name</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" rowspan=\"2\">Reads</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" rowspan=\"2\">Error<br />Rate</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" rowspan=\"2\">Total<br />Errors</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" rowspan=\"2\">Ave<br />DE</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" rowspan=\"2\">Max<br />DE</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Substrate</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Cyan</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Magenta</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Yellow</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Black</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Gray</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Humidity</td><td style=\"width: 10%\" class=\"GRIDTITLE\" align=\"center\" colspan=\"2\">Temp</td></tr>";
                    BranchHtml += "<tr class=\"GRIDTITLE\"><td class=\"GRIDTITLE\">DE</td><td class=\"GRIDTITLE\">DH</td><td class=\"GRIDTITLE\">DE</td><td class=\"GRIDTITLE\">DH</td><td class=\"GRIDTITLE\">DE</td><td class=\"GRIDTITLE\">DH</td><td class=\"GRIDTITLE\">DE</td><td class=\"GRIDTITLE\">DH</td><td class=\"GRIDTITLE\">DE</td><td class=\"GRIDTITLE\">DH</td><td class=\"GRIDTITLE\">DE</td><td class=\"GRIDTITLE\">DH</td><td class=\"GRIDTITLE\">AVE</td><td class=\"GRIDTITLE\">STD</td><td class=\"GRIDTITLE\">AVE</td><td class=\"GRIDTITLE\">STD</td></tr>";

                    string Css1 = "GRIDEVEN";
                    for (int j = 0; j < DG_ColorSpec.Items.Count; j = j + 1)
                    {
                        if (((j + 1) % 2) == 0)
                        { Css1 = "GRIDEVEN"; }
                        else
                        { Css1 = "GRIDODD"; }

                        BranchHtml += "<tr class=\"" + Css1 + "\">";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[0].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[1].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[2].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[3].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[4].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[5].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[6].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[7].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[8].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[9].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[10].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[11].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[12].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[13].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[14].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[15].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[16].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[17].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[18].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[19].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[20].Text + "</td>";
                        BranchHtml += "<td>" + DG_ColorSpec.Items[j].Cells[21].Text + "</td>";
                        BranchHtml += "</tr>";
                    }
                    BranchHtml += "</table>";
                    BranchHtml += "</td></tr><tr><td style=\"height: 5px\"></td></tr>";
                }

            }
            #endregion

            Sessions.SetSessions("RPT-8-BranchInfo", BranchHtml);
            #endregion
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion
}
