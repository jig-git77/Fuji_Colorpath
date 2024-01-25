using Formulas;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Taskero.Business;
using Taskero.Common;
using Taskero.DataAccess;
using TaskeroWeb.Models;


namespace TaskeroWeb
{
    public partial class ReportPatch : PageBase
    {
        #region Declarations
        protected string SEARCH = "";
        protected string RESULT = "";
        #endregion

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

                    //========== Fill Drop Down ==========//
                    SetStyle();
                }
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
            finally
            {
                SEARCH = LBL_TRSearch.Text;
                RESULT = LBL_TRResult.Text;
            }
        }
        protected void BTN_BackToSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //========== displaying Search / Hiding Result ==========//
                PNL_Search.Style.Add("display", "");
                PNL_Result.Style.Add("display", "none");
                lblReportTitle.Text = "Patch Detail Report(Search Criteria)";
            }
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }
        protected void BTN_CreateReport_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Validations ==========//
                if (DDL_Device.SelectedIndex == 0)
                { LblError.Text = "Please Select Device !!!"; return; }
                if (DDL_PrintTarget.SelectedIndex == 0)
                { LblError.Text = "Please Select Print Target !!!"; return; }
                if (TXT_FromDate.Text == "")
                { LblError.Text = "From Date is required!!!"; return; }
                if (TXT_ToDate.Text == "")
                { LblError.Text = "To Date is required!!!"; return; }

                LBL_DateRange.Text = TXT_FromDate.Text + " - " + TXT_ToDate.Text;
                LBL_DeviceName.Text = DDL_Device.SelectedItem.Text;
                if (DDL_PrintTarget.SelectedIndex != 0)
                { LBL_DeviceName.Text += " - " + DDL_PrintTarget.SelectedItem.Text; }
                LBL_Branch.Text = DDL_Branch.SelectedItem.Text;

                //========== Filling Label ==========//
                lblReportTitle.Text = "Patch Detail Report";
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

                //=============== Load Data ============================//
                GetPatchDetails();

                if (DGPatchDetail.Items.Count == 0)
                {
                    LblError.Text = "No measurements found for the search criteria selected!";
                }
                else
                {

                    //========== Hiding Search / displaying Result ==========//
                    PNL_Search.Style.Add("display", "none");
                    PNL_Result.Style.Add("display", "");

                    #region ====   Set Column of Data Tables ( BasicInfo ) ======

                    DataTable DT_BasicInfo = new DataTable("BasicInfo");

                    DataColumn DC_BasicInfo = new DataColumn();
                    DC_BasicInfo.ColumnName = "DeviceName_";
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
                    DR_BasicInfo["DeviceName_"] = DDL_Device.SelectedItem.Text;
                    //DR_BasicInfo["CompanyName_"] = LBL_CompanyName.Text.Trim();
                    DR_BasicInfo["BranchName_"] = LBL_Branch.Text.Trim();
                    DR_BasicInfo["DateRange_"] = LBL_DateRange.Text.Trim();

                    DT_BasicInfo.Rows.Add(DR_BasicInfo);

                    ViewState["DT_BasicInfo"] = DT_BasicInfo;

                    #endregion

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
                }
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        protected void BTN_MakePDF_Click(object sender, EventArgs e)
        {
            try
            {
                ReportPDF objReportPDF = new ReportPDF();
                objReportPDF.ReportType = (int)Enums.enumReportType.PatchDetailReport;
                objReportPDF.PDFPath = Server.MapPath("UploadFiles/Report_PDF/");
                objReportPDF.PDF_NAME = Sessions.UserID + "_PatchDetail.PDF";


                if (ViewState["DT_BasicInfo"] != null)
                { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_BasicInfo"]); }
                if (ViewState["DT_SearchCriteria"] != null)
                { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_SearchCriteria"]); }
                if (ViewState["DT_Patch_Details"] != null)
                { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_Patch_Details"]); }
                objReportPDF.MakePDF();

                CommonFunctions.DownlaodFile(objReportPDF.PDFPath, objReportPDF.PDF_NAME, objReportPDF.PDF_NAME);
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        protected void SortCommandPatchDetail_OnClick(object sender, DataGridSortCommandEventArgs e)
        {
            try
            {
                LBL_SortExpression1.Text = e.SortExpression;
                if (LBL_SortOrder1.Text == "")
                { LBL_SortOrder1.Text = "DESC"; }
                else { LBL_SortOrder1.Text = ""; }
                GetPatchDetails();
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        protected void DGPatchDetail_ItemDataBound1(object sender, DataGridItemEventArgs e)
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
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }
        #endregion

        #region Validations
        protected void TXT_DateText(object sender, EventArgs e)
        {
            if (Validations.IsDateTime(((TextBox)sender).Text) == false)
            { LblError.Text = "Please enter a valid date!"; ((TextBox)sender).Text = ""; return; }
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
                objUserBranch.TaskeroUserID = Sessions.UserID;
                objUserBranch.CompanyID = Convert.ToInt32(DDL_Company.SelectedValue);

                //==========  Get Data Table and Bind ==========//
                DataTable DT_Branch = new DataTable();
                DT_Branch = objUserBranch.uspGetBranchOfCompanyByUserID(Sessions.RoleID);

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
                objUserDongle.TaskeroUserID = Sessions.UserID;
                objUserDongle.UserDongleID = Convert.ToInt32(DDL_Branch.SelectedValue);
                DT_Dongle = objUserDongle.uspGetDongleByUserIDAndBranchID(Sessions.RoleID);

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
                objCPDevice.CPDongleID = Convert.ToInt32(DDL_DongleNo.SelectedValue);
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
                objCPPrintTarget.DeviceID = Convert.ToInt32(DDL_Device.SelectedValue);
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
                DataTable DT = (new Database()).GetDataTable(Cmd);

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

        #region Others
        private void SetStyle()
        {
            try
            {
                DDL_Company.Style.Add("width", "100%");
                DDL_Branch.Style.Add("width", "100%");
                DDL_DongleNo.Style.Add("width", "100%");
                DDL_Device.Style.Add("width", "100%");
                DDL_PrintTarget.Style.Add("width", "100%");

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

        #region Methods
        private void GetPatchDetails()
        {
            try
            {
                //========== JoinClause ==========//
                string JoinClause = "";
                if (DDL_OPT1_Field.SelectedIndex > 0 && DDL_OPT1_Operator.SelectedIndex > 0)
                { JoinClause += DDL_OPT1_Field.SelectedValue + " " + DDL_OPT1_Operator.SelectedValue.Replace("<<>>", TXT_OPT1_KeyWords.Text.Replace("'", "''")) + " "; }
                if (DDL_OPT2_Field.SelectedIndex > 0 && DDL_OPT2_Operator.SelectedIndex > 0)
                { JoinClause += DDL_OPT2_Field.SelectedValue + " " + DDL_OPT2_Operator.SelectedValue.Replace("<<>>", TXT_OPT2_KeyWords.Text.Replace("'", "''")) + " "; }
                if (DDL_OPT3_Field.SelectedIndex > 0 && DDL_OPT3_Operator.SelectedIndex > 0)
                { JoinClause += DDL_OPT3_Field.SelectedValue + " " + DDL_OPT3_Operator.SelectedValue.Replace("<<>>", TXT_OPT3_KeyWords.Text.Replace("'", "''")) + " "; }

                //========== Execute Dataset ==========//
                SqlCommand Cmd = new SqlCommand();

                Cmd.CommandText += "SELECT Sample_ID,Sample_Name , CMYK_C , CMYK_M ,  CMYK_Y ,  CMYK_K , Isnull(MIN(ABS(DL)) , 0) AS MinDL , Isnull(MAX(ABS(DL)) , 0) AS MaxDL , Isnull(AVG(ABS(DL)) , 0) AS AvgDL , Isnull(MIN(ABS(DA)) , 0) AS MinDA , Isnull(MAX(ABS(DA)) , 0)AS MaxDA , Isnull(MIN(ABS(DB)) , 0) AS MinDB , Isnull(MAX(ABS(DB)) , 0) AS MaxDB , IsNull(AVG(NULLIF(Density,-1)),0) As AveDensity ,  IsNull(STDEV(NULLIF(Density,-1)),0) As StdDevDensity , Isnull(MIN(ABS(DE)) , 0) AS MinDE , Isnull(MAX(ABS(DE)) , 0) AS MaxDE , Isnull(AVG(ABS(DE)) , 0) AS AvgDE , Isnull(STDEV(DE) , 0) AS STDDEV_DE , LAB_L1 ,  LAB_A1 ,  LAB_B1   ";// , Isnull(AVG(ABS(DA)) , 0) AS AvgDA , Isnull(AVG(ABS(DB)) , 0) AS AvgDB
                Cmd.CommandText += "FROM (SELECT Sample_ID,Sample_Name ,  CMYK_C ,  CMYK_M ,  CMYK_Y ,  CMYK_K ,  LAB_L1 ,  LAB_A1 ,  LAB_B1 ,  DE ,  DL ,  DA ,  DB , Density FROM CPPatchDataLab ";
                Cmd.CommandText += "WHERE (MeasurementGUID IN (SELECT PMD.CPMeasurementGUID FROM CPMeasurement AS PMD ";
                Cmd.CommandText += "INNER JOIN  CPMeasurement_PT AS PT ON PT.CPMeasurementGUID = PMD.CPMeasurementGUID ";
                Cmd.CommandText += JoinClause;
                Cmd.CommandText += "WHERE (PMD.CPDeviceID = " + DDL_Device.SelectedValue + ") AND (ISNULL( PMD.IsUsed , 1 ) = 1) AND (PT.CPDevicePrintTargetID = " + DDL_PrintTarget.SelectedValue + " AND ( PMD.CreatedDT BETWEEN  '" + TXT_FromDate.Text + "' and '" + Convert.ToDateTime(TXT_ToDate.Text).AddDays(1).ToShortDateString() + "'))))) AS CPD ";
                Cmd.CommandText += "GROUP BY Sample_Name ,  CMYK_C ,  CMYK_M ,  CMYK_Y ,  CMYK_K ,  LAB_L1 ,  LAB_A1 ,  LAB_B1, Sample_ID ";
                Cmd.CommandText += "Order By " + LBL_SortExpression1.Text + " " + LBL_SortOrder1.Text;
                DataTable DT = (new Database()).GetDataTable(Cmd);

                DT.TableName = "Patch_Details";
                ViewState["DT_Patch_Details"] = DT;

                //========== Show values ==========//
                DGPatchDetail.DataSource = DT;
                CommonFunctions.SetDataGridDefaultSettings(DGPatchDetail);
                DGPatchDetail.AllowPaging = false;
                DGPatchDetail.DataBind();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion
    }
}