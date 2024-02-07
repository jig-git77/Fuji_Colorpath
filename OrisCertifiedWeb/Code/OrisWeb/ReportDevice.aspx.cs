using Dundas.Charting.WebControl;
using Oris.Business;
using Oris.Common;
using Oris.DataAccess;
using OrisWeb.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

namespace OrisWeb
{
    public partial class ReportDevice : PageBase
    {
        #region Declarations
        string MeasuremrntTypeId = string.Empty;
        #endregion

        #region Page Load & Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //========== Clear Message ==========//
                LblError.Text = "";

                //========== Default Settings ==========//
                if (IsPostBack == false)
                {
                    //========== Check User Login ==========//
                    Sessions.CheckUserLogin();

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
        }
        protected void BTN_CreateReport_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Clear ViewState ==========//
                ViewState.Clear();

                //========== Validations ==========//
                if (DDL_Device.SelectedIndex == 0)
                { LblError.Text = "Please select a device!!!"; return; }
                if (TXT_FromDate.Text == "")
                { LblError.Text = "From Date is required!!!"; return; }
                if (TXT_ToDate.Text == "")
                { LblError.Text = "To Date is required!!!"; return; }

                LTRL_DateRange.Text = TXT_FromDate.Text + " - " + TXT_ToDate.Text;


                //========== Filling Label ==========//
                lblReportTitle.Text = "Device Summary Report";
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
                    //========== Hiding Search / displaying Result ==========//
                    PNL_Search.Style.Add("display", "none");
                    PNL_Result.Style.Add("display", "");

                    //========== Load Data ==========//
                    LoadData();
                    //========== display Data ==========//

                    // ======  Set Temp Table For PDF  ===========//
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
                lblReportTitle.Text = "Device Summary Report(Search Criteria)";
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        protected void BTN_MakePDF_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Create an object ==========//
                ReportPDF objReportPDF = new ReportPDF();

                //========== Assing type of Report ==========//
                objReportPDF.ReportType = (int)Enums.enumReportType.DeviceReport;

                //========== Path to save the file ==========//            
                objReportPDF.PDFPath = Server.MapPath("UploadFiles/Report_PDF/");
                objReportPDF.PDF_NAME = Sessions.UserID + "_DeviceReport.PDF";

                if (ViewState["DT_BasicInfo"] != null)
                { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_BasicInfo"]); }
                if (ViewState["DT_SearchCriteria"] != null)
                { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_SearchCriteria"]); }
                if (ViewState["DT_FailureRate"] != null)
                { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_FailureRate"]); }
                if (ViewState["DT_Environmental"] != null)
                { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_Environmental"]); }
                if (ViewState["DT_Consumables"] != null)
                { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_Consumables"]); }
                if (ViewState["DT_Consumable"] != null)
                { objReportPDF.DS_Common.Tables.Add((DataTable)ViewState["DT_Consumable"]); }
                objReportPDF.MakePDF();

                CommonFunctions.DownlaodFile(objReportPDF.PDFPath, objReportPDF.PDF_NAME, objReportPDF.PDF_NAME);

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
                DT_Device = objCPDevice.uspGetDeviceByDongleID();

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

        #region Validations
        protected void TXT_DateText(object sender, EventArgs e)
        {
            if (Validations.IsDateTime(((TextBox)sender).Text) == false)
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
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Load Data
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

                if (Sessions.RoleDesc == Enums.RoleType.TaskeroAdmin)
                {
                    Cmd.CommandText = "SELECT ";
                    Cmd.CommandText += "DISTINCT(PMD.CPMeasurementGUID) as CPMeasurementGUID ";
                    Cmd.CommandText += "FROM  CPMeasurement PMD ";
                    Cmd.CommandText += JoinClause;
                    Cmd.CommandText += "WHERE PMD.CPDeviceID = " + DDL_Device.SelectedValue + " ";
                    Cmd.CommandText += " AND ( PMD.CreatedDT BETWEEN  '" + TXT_FromDate.Text + "' and '" + Convert.ToDateTime(TXT_ToDate.Text).AddDays(1).ToShortDateString() + "') ";
                    Cmd.CommandText += " AND ISNULL( PMD.IsUsed , 1 ) = 1 ";
                }
                else
                {
                    Cmd.CommandText = "SELECT ";
                    Cmd.CommandText += "DISTINCT(PMD.CPMeasurementGUID) as CPMeasurementGUID ";
                    Cmd.CommandText += "FROM  CPMeasurement PMD ";
                    Cmd.CommandText += "INNER JOIN CPV.vwGetAllDevicesPerUsers vw1 ON vw1.CPDeviceID = PMD.CPDeviceID ";
                    Cmd.CommandText += JoinClause;
                    Cmd.CommandText += "WHERE vw1.TaskeroUserID = " + Sessions.UserID + " AND vw1.CPDeviceID = " + DDL_Device.SelectedValue + " ";
                    Cmd.CommandText += "AND ISNULL( PMD.IsUsed , 1 ) = 1 ";
                    Cmd.CommandText += "AND ( PMD.CreatedDT BETWEEN  '" + TXT_FromDate.Text + "' and '" + Convert.ToDateTime(TXT_ToDate.Text).AddDays(1).ToShortDateString() + "') ";
                    //========== Query that will check the Customer field if the user is Print Buyer ==========//
                    Cmd.CommandText += "AND 1 = ( CASE vw1.RoleID WHEN 5 THEN CASE WHEN vw1.Customer = PMD.Customer THEN 1 ELSE 0 END ELSE  1 END ) ";
                }
                Cmd.CommandType = CommandType.Text;
                DataTable DT = (new Database()).GetDataTable(Cmd);

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
                GetDeviceData();
                GetDeviceTRHData();
                GetDeviceMeasurementStatistics();
                GetTypeId();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void GetTypeId()
        {
            try
            {
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandText = "Select CPDeviceID , CPDeviceDesc, MeasurementTypeID  from CPDevice  WHERE CPDeviceID = " + DDL_Device.SelectedValue;
                Cmd.CommandType = CommandType.Text;
                DataTable DT = (new Database()).GetDataTable(Cmd);
                if (DT.Rows.Count > 0)
                {
                    MeasuremrntTypeId = DT.Rows[0]["MeasurementTypeID"].ToString();
                }

                if (MeasuremrntTypeId == "3")
                {
                    GetConsumable();
                    LBL_InkConsumables.Visible = false;
                    LBL_Consumables.Visible = true;
                }
                else
                {
                    GetDeviceInkData();
                    LBL_Consumables.Visible = false;
                    LBL_InkConsumables.Visible = true;
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void GetDeviceData()
        {
            try
            {
                //========== Get Data Table ==========//
                SqlCommand Cmd = new SqlCommand();
                Cmd.CommandText = "SELECT Top 1 C.CompanyName ,B.BranchName , CPDo.CPSerialNum , CPD.CPDeviceDesc ";
                Cmd.CommandText += "From Company C ";
                Cmd.CommandText += "INNER JOIN Branch B ON C.CompanyID = B.CompanyID ";
                Cmd.CommandText += "INNER JOIN CPDongle CPDo ON B.BranchID = CPDo.BranchID ";
                Cmd.CommandText += "INNER JOIN CPDevice CPD ON CPDo.CPDongleID = CPD.CPDongleID ";
                Cmd.CommandText += "INNER JOIN CPMeasurement PMD ON CPD.CPDeviceID = PMD.CPDeviceID ";
                Cmd.CommandText += "WHERE PMD.CPMeasurementGUID IN ( " + LTRL_CPMeasurementGUID.Text + " ) ";

                Cmd.CommandType = CommandType.Text;
                DataTable DT = (new Database()).GetDataTable(Cmd);

                //========== Show values ==========//
                if (DT.Rows.Count > 0)
                {

                    LTRL_DeviceName.Text = DT.Rows[0]["CPDeviceDesc"].ToString();
                    LTRL_DongleNumber.Text = DT.Rows[0]["CPSerialNum"].ToString();
                    LTRL_BranchName.Text = DT.Rows[0]["BranchName"].ToString();
                    LTRL_CompanyName.Text = DT.Rows[0]["CompanyName"].ToString();
                }
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
                //========== Build an And Clause ==========//
                string JoinClause = "";
                if (DDL_OPT1_Field.SelectedIndex > 0 && DDL_OPT1_Operator.SelectedIndex > 0)
                { JoinClause += DDL_OPT1_Field.SelectedValue + " " + DDL_OPT1_Operator.SelectedValue.Replace("<<>>", TXT_OPT1_KeyWords.Text.Replace("'", "''")) + " "; }
                if (DDL_OPT2_Field.SelectedIndex > 0 && DDL_OPT2_Operator.SelectedIndex > 0)
                { JoinClause += DDL_OPT2_Field.SelectedValue + " " + DDL_OPT2_Operator.SelectedValue.Replace("<<>>", TXT_OPT2_KeyWords.Text.Replace("'", "''")) + " "; }
                if (DDL_OPT3_Field.SelectedIndex > 0 && DDL_OPT3_Operator.SelectedIndex > 0)
                { JoinClause += DDL_OPT3_Field.SelectedValue + " " + DDL_OPT3_Operator.SelectedValue.Replace("<<>>", TXT_OPT3_KeyWords.Text.Replace("'", "''")) + " "; }


                //========== Get Data Table ==========//
                SqlCommand Cmd = new SqlCommand();

                Cmd.CommandText = "SELECT ISNULL(COUNT(TempRHData.CPMeasurementGUID),0) AS TotalRec, ISNULL(AVG(TempRHData.TempF),0) As AvgTempF, ISNULL(AVG(TempRHData.RH),0) As AvgRH,ISNULL(MIN(TempRHData.TempF),0) As MinTempF,ISNULL(MAX(TempRHData.TempF),0) As MaxTempF,ISNULL(MIN(TempRHData.RH),0) As MinRH, ISNULL(MAX(TempRHData.RH),0) As MaxRH , ISNULL( MAX(TempRHDevice.TargetTemp) , 0 ) as TargetTemp  ";
                Cmd.CommandText += "FROM CPMeasurement AS CPM ";
                Cmd.CommandText += "INNER JOIN CPMeasurement_TempRH AS TempRHData ON CPM.CPMeasurementGUID = TempRHData.CPMeasurementGUID ";
                Cmd.CommandText += "LEFT JOIN TempRHDevice_CPDevice ON TempRHDevice_CPDevice.CPDeviceID = CPM.CPDeviceID ";
                Cmd.CommandText += "LEFT JOIN TempRHDevice ON	TempRHDevice.TempRHDeviceID = TempRHDevice_CPDevice.TempRHDeviceID ";
                Cmd.CommandText += "WHERE TempRHData.CPMeasurementGUID IN ( " + LTRL_CPMeasurementGUID.Text + " ) ";
                Cmd.CommandType = CommandType.Text;
                DataTable DT = (new Database()).GetDataTable(Cmd);

                //========== Show values ==========//
                if (DT.Rows.Count > 0)
                {
                    if (Convert.ToDecimal(DT.Rows[0]["AvgTempF"]) == 0
                        && Convert.ToDecimal(DT.Rows[0]["TargetTemp"]) == 0
                        && Convert.ToDecimal(DT.Rows[0]["MinTempF"]) == 0
                        && Convert.ToDecimal(DT.Rows[0]["MaxTempF"]) == 0
                        && Convert.ToDecimal(DT.Rows[0]["MinRH"]) == 0
                        && Convert.ToDecimal(DT.Rows[0]["MaxRH"]) == 0
                        )
                    {
                        TR_Env.Visible = false;
                        LTRL_AveTemp.Text = "0.00";
                        LTRL_TargetTemp.Text = "0.00";
                        LTRL_MinTemp.Text = "0.00";
                        LTRL_MaxTemp.Text = "0.00";
                        LTRL_MinRH.Text = "0.00";
                        LTRL_MaxRH.Text = "0.00";
                    }
                    else
                    {
                        TR_Env.Visible = true;
                        if (!Convert.IsDBNull(DT.Rows[0]["AvgTempF"])) { LTRL_AveTemp.Text = Convert.ToDecimal(DT.Rows[0]["AvgTempF"]).ToString("N2"); }
                        if (!Convert.IsDBNull(DT.Rows[0]["TargetTemp"])) { LTRL_TargetTemp.Text = Convert.ToDecimal(DT.Rows[0]["TargetTemp"]).ToString("N2"); }
                        if (!Convert.IsDBNull(DT.Rows[0]["MinTempF"])) { LTRL_MinTemp.Text = Convert.ToDecimal(DT.Rows[0]["MinTempF"]).ToString("N2"); }
                        if (!Convert.IsDBNull(DT.Rows[0]["MaxTempF"])) { LTRL_MaxTemp.Text = Convert.ToDecimal(DT.Rows[0]["MaxTempF"]).ToString("N2"); }
                        if (!Convert.IsDBNull(DT.Rows[0]["MinRH"])) { LTRL_MinRH.Text = Convert.ToDecimal(DT.Rows[0]["MinRH"]).ToString("N2"); }
                        if (!Convert.IsDBNull(DT.Rows[0]["MaxRH"])) { LTRL_MaxRH.Text = Convert.ToDecimal(DT.Rows[0]["MaxRH"]).ToString("N2"); }
                    }
                }
                else
                {
                    TR_Env.Visible = false;
                    LTRL_AveTemp.Text = "0.00";
                    LTRL_TargetTemp.Text = "0.00";
                    LTRL_MinTemp.Text = "0.00";
                    LTRL_MaxTemp.Text = "0.00";
                    LTRL_MinRH.Text = "0.00";
                    LTRL_MaxRH.Text = "0.00";
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void GetDeviceMeasurementStatistics()
        {
            try
            {
                //========== Get Data Table ==========//
                SqlCommand Cmd = new SqlCommand();

                Cmd.CommandText = "SELECT COUNT(PMD.CPMeasurementID)AS TotalRec,SUM(CASE WHEN PMD.[Status] = 0 THEN 1 ELSE 0 END) AS PROOF_FAILED,SUM(CASE WHEN PMD.[Status] = 1 THEN 1 ELSE 0 END) AS PROOF_CAUTION, SUM(CASE WHEN PMD.[Status] = 2 THEN 1 ELSE 0 END) AS PROOF_PASSED ";
                //Cmd.CommandText = "SELECT (100) AS TotalRec , (0) AS PROOF_FAILED , (0) AS PROOF_CAUTION, (100) AS PROOF_PASSED ";
                Cmd.CommandText += "From CPMeasurement PMD ";
                Cmd.CommandText += "WHERE PMD.CPMeasurementGUID IN ( " + LTRL_CPMeasurementGUID.Text + " ) ";

                Cmd.CommandType = CommandType.Text;
                DataTable DT = (new Database()).GetDataTable(Cmd);

                //========== Show values ==========//
                if (DT.Rows.Count > 0)
                {
                    LTRL_NumofMeasurements.Text = (DT.Rows[0]["TotalRec"]).ToString();
                    LTRL_Passed.Text = (DT.Rows[0]["Proof_Passed"]).ToString(); ;
                    LTRL_Failed.Text = (DT.Rows[0]["Proof_Failed"]).ToString();
                    LTRL_Cautions.Text = (DT.Rows[0]["Proof_Caution"]).ToString();

                    #region Fill Pie Chart
                    FillPieChart1(DT);
                    #endregion
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void GetDeviceInkData()
        {
            try
            {
                //========== Get Data Table ==========//           
                SqlCommand Cmd = new SqlCommand();

                Cmd.CommandText += "";

                /*===== Cyan =====*/
                Cmd.CommandText += "";
                Cmd.CommandText += "SELECT COUNT(PPD.MeasurementGUID) AS Measurement,ISNULL(AVG(PPD.DE),0) AS AVG_DELTA, ISNULL(MAX(PPD.DE),0) As MEX_DELTA,ISNULL(AVG(PPD.DENSITY),0) As Delta_Den_Ave , ISNULL( STDEV(PPD.DENSITY) , 0 ) as Density_StdDev ";
                Cmd.CommandText += "FROM CPPatchDataLab PPD ";
                Cmd.CommandText += "WHERE PPD.CMYK_C = 100 AND PPD.CMYK_M = 0 AND PPD.CMYK_Y = 0 AND PPD.CMYK_K = 0 ";
                Cmd.CommandText += "AND PPD.MeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";

                Cmd.CommandText += "  UNION ALL  ";

                /*===== Magenta =====*/
                Cmd.CommandText += "SELECT COUNT(PPD.MeasurementGUID) AS Measurement,ISNULL(AVG(PPD.DE),0) AS AVG_DELTA, ISNULL(MAX(PPD.DE),0) As MEX_DELTA,ISNULL(AVG(PPD.DENSITY),0) As Delta_Den_Ave , ISNULL( STDEV(PPD.DENSITY) , 0 ) as Density_StdDev ";
                Cmd.CommandText += "FROM CPPatchDataLab PPD ";
                Cmd.CommandText += "WHERE PPD.CMYK_C = 0 AND PPD.CMYK_M = 100 AND PPD.CMYK_Y = 0 AND PPD.CMYK_K = 0 ";
                Cmd.CommandText += "AND PPD.MeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";

                Cmd.CommandText += "  UNION ALL  ";

                /*===== Yellow =====*/
                Cmd.CommandText += "SELECT COUNT(PPD.MeasurementGUID) AS Measurement,ISNULL(AVG(PPD.DE),0) AS AVG_DELTA, ISNULL(MAX(PPD.DE),0) As MEX_DELTA,ISNULL(AVG(PPD.DENSITY),0) As Delta_Den_Ave , ISNULL( STDEV(PPD.DENSITY) , 0 ) as Density_StdDev ";
                Cmd.CommandText += "FROM CPPatchDataLab PPD ";
                Cmd.CommandText += "WHERE PPD.CMYK_C = 0 AND PPD.CMYK_M = 0 AND PPD.CMYK_Y = 100 AND PPD.CMYK_K = 0 ";
                Cmd.CommandText += "AND PPD.MeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";

                Cmd.CommandText += "  UNION ALL  ";

                /*===== Black =====*/
                Cmd.CommandText += "SELECT COUNT(PPD.MeasurementGUID) AS Measurement,ISNULL(AVG(PPD.DE),0) AS AVG_DELTA, ISNULL(MAX(PPD.DE),0) As MEX_DELTA,ISNULL(AVG(PPD.DENSITY),0) As Delta_Den_Ave , ISNULL( STDEV(PPD.DENSITY) , 0 ) as Density_StdDev ";
                Cmd.CommandText += "FROM CPPatchDataLab PPD ";
                Cmd.CommandText += "WHERE PPD.CMYK_C = 0 AND PPD.CMYK_M = 0 AND PPD.CMYK_Y = 0 AND PPD.CMYK_K = 100 ";
                Cmd.CommandText += "AND PPD.MeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";

                Cmd.CommandText += " UNION ALL  ";

                /*==== Paper ==== */
                Cmd.CommandText += "SELECT COUNT(PPD.MeasurementGUID) AS Measurement,ISNULL(AVG(PPD.DE),0) AS AVG_DELTA, ISNULL(MAX(PPD.DE),0) As MEX_DELTA,ISNULL(AVG(PPD.DENSITY),0) As Delta_Den_Ave , ISNULL( STDEV(PPD.DENSITY) , 0 ) as Density_StdDev ";
                Cmd.CommandText += "FROM CPPatchDataLab PPD ";
                Cmd.CommandText += "WHERE PPD.CMYK_C = 0 AND PPD.CMYK_M = 0 AND PPD.CMYK_Y = 0 AND PPD.CMYK_K = 0 ";
                Cmd.CommandText += "AND PPD.MeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";

                Cmd.CommandType = CommandType.Text;
                DataTable DT = (new Database()).GetDataTable(Cmd);

                //========== Show values ==========//
                if (DT.Rows.Count > 0)
                {
                    //Cyan
                    LTRL_InkCyanMeasurements.Text = Convert.ToDecimal(DT.Rows[0]["Measurement"]).ToString();
                    LTRL_InkCyanDeltaEAve.Text = Convert.ToDecimal(DT.Rows[0]["AVG_DELTA"]).ToString("N2");
                    LTRL_InkCyanDeltaMax.Text = Convert.ToDecimal(DT.Rows[0]["MEX_DELTA"]).ToString("N2");
                    LTRL_InkCyanDeltaDensityAve.Text = Convert.ToDecimal(DT.Rows[0]["Delta_Den_Ave"]).ToString("N2");
                    LTRL_InkCyanStdDev.Text = Convert.ToDecimal(DT.Rows[0]["Density_StdDev"]).ToString("N2");
                    //'Magenta
                    LTRL_InkMagentaMeasurements.Text = Convert.ToDecimal(DT.Rows[1]["Measurement"]).ToString();
                    LTRL_InkMagentaDeltaEAve.Text = Convert.ToDecimal(DT.Rows[1]["AVG_DELTA"]).ToString("N2");
                    LTRL_InkMagentaDeltaMax.Text = Convert.ToDecimal(DT.Rows[1]["MEX_DELTA"]).ToString("N2");
                    LTRL_InkMagentaDeltaDensityAve.Text = Convert.ToDecimal(DT.Rows[1]["Delta_Den_Ave"]).ToString("N2");
                    LTRL_InkMagentaStdDev.Text = Convert.ToDecimal(DT.Rows[1]["Density_StdDev"]).ToString("N2");
                    //Yellow
                    LTRL_InkYellowMeasurements.Text = Convert.ToDecimal(DT.Rows[2]["Measurement"]).ToString();
                    LTRL_InkYellowDeltaEAve.Text = Convert.ToDecimal(DT.Rows[2]["AVG_DELTA"]).ToString("N2");
                    LTRL_InkYellowDeltaMax.Text = Convert.ToDecimal(DT.Rows[2]["MEX_DELTA"]).ToString("N2");
                    LTRL_InkYellowDeltaDensityAve.Text = Convert.ToDecimal(DT.Rows[2]["Delta_Den_Ave"]).ToString("N2");
                    LTRL_InkYellowStdDev.Text = Convert.ToDecimal(DT.Rows[2]["Density_StdDev"]).ToString("N2");
                    //Black
                    LTRL_InkBlackMeasurements.Text = Convert.ToDecimal(DT.Rows[3]["Measurement"]).ToString();
                    LTRL_InkBlackDeltaEAve.Text = Convert.ToDecimal(DT.Rows[3]["AVG_DELTA"]).ToString("N2");
                    LTRL_InkBlackDeltaMax.Text = Convert.ToDecimal(DT.Rows[3]["MEX_DELTA"]).ToString("N2");
                    LTRL_InkBlackDeltaDensityAve.Text = Convert.ToDecimal(DT.Rows[3]["Delta_Den_Ave"]).ToString("N2");
                    LTRL_InkBlackStdDev.Text = Convert.ToDecimal(DT.Rows[3]["Density_StdDev"]).ToString("N2");
                    //Paper
                    LTRL_PaperMeasurements.Text = Convert.ToDecimal(DT.Rows[4]["Measurement"]).ToString();
                    LTRL_PaperDeltaEAve.Text = Convert.ToDecimal(DT.Rows[4]["AVG_DELTA"]).ToString("N2");
                    LTRL_PaperDeltaMax.Text = Convert.ToDecimal(DT.Rows[4]["MEX_DELTA"]).ToString("N2");
                    LTRL_PaperDeltaDensityAve.Text = Convert.ToDecimal(DT.Rows[4]["Delta_Den_Ave"]).ToString("N2");
                    LTRL_PaperStdDev.Text = Convert.ToDecimal(DT.Rows[4]["Density_StdDev"]).ToString("N2");
                }
                FillLineChart1();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void GetConsumable()
        {
            try
            {
                //========== Get Data Table ==========//
                SqlCommand Cmd = new SqlCommand();

                Cmd.CommandText += "SELECT REPLACE(DP.Substrate_SKU, '', 'NO Plate SKU') AS Plate, COUNT(PMD.CPMeasurementGUID) AS TotalReads , AVG(ACT.AvgNeg_DotArea) As AvgNegDev, AVG(ACT.AvgPos_DotArea) As AvgPosDev, MIN(ACT.MaxNeg_DotArea) As MaxNegDev , MAX(ACT.MaxPos_DotArea) as MaxPosDev ";// , AVG(ACT.Average) AS AvgDE,  MAX(ACT.PatchdEValue) AS AvgMaxdE
                Cmd.CommandText += "FROM CPMeasurement PMD ";
                Cmd.CommandText += "INNER JOIN CPMeasurement_ACT_Plate ACT ON PMD.CPMeasurementGUID = ACT.CPMeasurementGUID ";
                Cmd.CommandText += "INNER JOIN CPMeasurement_DP DP ON PMD.CPMeasurementGUID = DP.CPMeasurementGUID  ";
                Cmd.CommandText += "WHERE PMD.CPMeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ";
                Cmd.CommandText += "GROUP BY DP.Substrate_SKU ";

                Cmd.CommandType = CommandType.Text;
                DataTable DT = (new Database()).GetDataTable(Cmd);
                DT.TableName = "Consumabel";

                DGConsumable.DataSource = DT;
                CommonFunctions.SetDataGridDefaultSettings(DGConsumable);
                DGConsumable.AllowPaging = false;
                DGConsumable.DataBind();
                ViewState["DT_Consumable"] = DT;

                FillLineChart2();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Fill Chart
        private void FillPieChart1(DataTable DT)
        {
            try
            {
                //========= Declarations =========//
                Series ObjSeries = new Series();
                DataPoint objDP;

                #region Passed
                objDP = new DataPoint();
                decimal PROOF_PASSED;
                if (!Convert.IsDBNull(DT.Rows[0]["PROOF_PASSED"]))
                {
                    PROOF_PASSED = Convert.ToDecimal(DT.Rows[0]["Proof_Passed"]);
                    double[] yVal1 = { Convert.ToDouble(PROOF_PASSED) };
                    //objDP.Label = "Passed";
                    objDP.YValues = yVal1;
                    objDP.Label = objDP.LabelToolTip = objDP.ToolTip = "Passed : " + PROOF_PASSED.ToString();
                    ObjSeries.ChartType = "Pie";
                    objDP.Color = ObjSeries.Color = System.Drawing.Color.Green;
                    ObjSeries["PieLabelStyle"] = "Outside";
                    ObjSeries["PieLineArrowType"] = "Triangle";
                    ObjSeries.Color = System.Drawing.Color.Green;
                    ObjSeries.Points.Add(objDP);
                }
                #endregion

                #region Failed
                objDP = new DataPoint();
                decimal PROOF_FAILED;
                if (!Convert.IsDBNull(DT.Rows[0]["Proof_Failed"]))
                {
                    PROOF_FAILED = Convert.ToDecimal(DT.Rows[0]["Proof_Failed"]);
                    double[] yVal2 = { Convert.ToDouble(PROOF_FAILED) };
                    //objDP.Label = "Failed";
                    objDP.YValues = yVal2;
                    objDP.Label = objDP.LabelToolTip = objDP.ToolTip = "Failed : " + PROOF_FAILED.ToString();
                    objDP.Color = ObjSeries.Color = System.Drawing.Color.Red;
                    ObjSeries.ChartType = "Pie";
                    ObjSeries["PieLabelStyle"] = "Outside";
                    ObjSeries["PieLineArrowType"] = "Triangle";
                    ObjSeries.Points.Add(objDP);
                }
                #endregion

                #region Caution
                objDP = new DataPoint();
                decimal PROOF_CAUTION;
                if (!Convert.IsDBNull(DT.Rows[0]["Proof_Caution"]))
                {
                    PROOF_CAUTION = Convert.ToDecimal(DT.Rows[0]["Proof_Caution"]);
                    double[] yVal3 = { Convert.ToDouble(PROOF_CAUTION) };
                    //objDP.Label = "Caution";
                    objDP.YValues = yVal3;
                    objDP.Label = objDP.LabelToolTip = objDP.ToolTip = "Caution : " + PROOF_CAUTION.ToString();
                    ObjSeries.ChartType = "Pie";
                    objDP.Color = ObjSeries.Color = System.Drawing.Color.Orange;
                    ObjSeries["PieLabelStyle"] = "Outside";
                    ObjSeries["PieLineArrowType"] = "Triangle";
                    ObjSeries.Points.Add(objDP);
                }
                #endregion

                ChartPie1.Series.Add(ObjSeries);

                //========= Set 3D chart flag =========//
                ChartPie1.ChartAreas["Default"].Area3DStyle.Enable3D = true;

                string FileName = "UploadFiles/temp/" + Sessions.UserID + "_" + Path.GetFileName(ChartPie1.ImageUrl) + "." + ChartPie1.ImageType;
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
        private void FillLineChart1()
        {
            try
            {
                //========== DSet ==========//
                SqlCommand Cmd = new SqlCommand();

                Cmd.CommandText = "";
                /*===== Cyan =====*/
                Cmd.CommandText += "SELECT ('Cyan') AS TableName , ISNULL(AVG(PPD.DE),0) as DE ,PMD.CreatedDT  ";
                Cmd.CommandText += "From CPPatchDataLab PPD ";
                Cmd.CommandText += "INNER JOIN CPMeasurement PMD ON PPD.MeasurementGUID = PMD.CPMeasurementGUID ";
                Cmd.CommandText += "WHERE PPD.CMYK_C = 100 AND PPD.CMYK_M = 0   AND PPD.CMYK_Y = 0 AND PPD.CMYK_K = 0  ";
                Cmd.CommandText += "AND PMD.CPMeasurementGUID IN ( " + LTRL_CPMeasurementGUID.Text + " ) ";
                Cmd.CommandText += "GROUP BY PMD.CreatedDT  ";
                Cmd.CommandText += "Order By PMD.CreatedDT ";

                /*===== Magenta =====*/
                Cmd.CommandText += "SELECT ('Magenta') AS TableName , ISNULL(AVG(PPD.DE),0) as DE ,PMD.CreatedDT  ";
                Cmd.CommandText += " From CPPatchDataLab PPD ";
                Cmd.CommandText += "INNER JOIN CPMeasurement PMD ON PPD.MeasurementGUID = PMD.CPMeasurementGUID ";
                Cmd.CommandText += "WHERE PPD.CMYK_C = 0   AND PPD.CMYK_M = 100 AND PPD.CMYK_Y = 0 AND PPD.CMYK_K = 0 ";
                Cmd.CommandText += "AND PMD.CPMeasurementGUID IN ( " + LTRL_CPMeasurementGUID.Text + " ) ";
                Cmd.CommandText += "GROUP BY PMD.CreatedDT ";
                Cmd.CommandText += "Order By PMD.CreatedDT ";

                /*===== Yellow =====*/
                Cmd.CommandText += "SELECT ('Yellow') AS TableName , ISNULL(AVG(PPD.DE),0) as DE ,PMD.CreatedDT  ";
                Cmd.CommandText += " From CPPatchDataLab PPD ";
                Cmd.CommandText += "INNER JOIN CPMeasurement PMD ON PPD.MeasurementGUID = PMD.CPMeasurementGUID ";
                Cmd.CommandText += "WHERE PPD.CMYK_C = 0   AND PPD.CMYK_M = 0 AND PPD.CMYK_Y = 100 AND PPD.CMYK_K = 0  ";
                Cmd.CommandText += "AND PMD.CPMeasurementGUID IN ( " + LTRL_CPMeasurementGUID.Text + " ) ";
                Cmd.CommandText += "GROUP BY PMD.CreatedDT ";
                Cmd.CommandText += "Order By PMD.CreatedDT ";

                /*===== Black =====*/
                Cmd.CommandText += "SELECT ('Black') AS TableName  , ISNULL(AVG(PPD.DE),0) as DE ,PMD.CreatedDT  ";
                Cmd.CommandText += " From CPPatchDataLab PPD ";
                Cmd.CommandText += "INNER JOIN CPMeasurement PMD ON PPD.MeasurementGUID = PMD.CPMeasurementGUID ";
                Cmd.CommandText += "WHERE PPD.CMYK_C = 0   AND PPD.CMYK_M = 0 AND PPD.CMYK_Y = 0 AND PPD.CMYK_K = 100  ";
                Cmd.CommandText += "AND PMD.CPMeasurementGUID IN ( " + LTRL_CPMeasurementGUID.Text + " ) ";
                Cmd.CommandText += "GROUP BY PMD.CreatedDT ";
                Cmd.CommandText += "Order By PMD.CreatedDT ";

                Cmd.CommandType = CommandType.Text;
                DataSet DSet = (new Database()).GetDataSet(Cmd);

                //========== Fill CHart ==========//
                ChartLine1.Titles.Clear();
                ChartLine1.Titles.Add("Delta E");
                ChartLine1.Series.Clear();
                ChartLine1.CallbackStateContent = CallbackStateContent.All;

                for (int i = 0; i < DSet.Tables.Count; i = i + 1)
                {

                    //========== Add Series ==========//
                    Series objSeries = new Series();

                    objSeries.Type = SeriesChartType.Spline;

                    if (i == 0) { objSeries.Name = "Cyan"; }
                    else if (i == 1) { objSeries.Name = "Magenta"; }
                    else if (i == 2) { objSeries.Name = "Yellow"; }
                    else { objSeries.Name = "Black"; }

                    objSeries.XValueType = ChartValueTypes.DateTime;
                    objSeries.YValueType = ChartValueTypes.Double;

                    objSeries.ValueMemberX = "CreatedDT";
                    objSeries.ValueMembersY = "DE";
                    //ChartLine1.Series[objSeries].ToolTip = "DE";

                    ChartLine1.Series.Add(objSeries);

                    //========== Add Values ==========//
                    for (int j = 0; j < DSet.Tables[i].Rows.Count; j = j + 1)
                    {
                        ChartLine1.Series[i].Color = System.Drawing.Color.FromName(DSet.Tables[i].Rows[0]["TableName"].ToString());
                        if (Convert.ToDecimal(DSet.Tables[i].Rows[j]["DE"]) >= 25)
                        { ChartLine1.Series[i].Points.AddXY(DSet.Tables[i].Rows[j]["CreatedDT"].ToString(), "25"); }
                        else
                        { ChartLine1.Series[i].Points.AddXY(DSet.Tables[i].Rows[j]["CreatedDT"].ToString(), DSet.Tables[i].Rows[j]["DE"].ToString()); }

                        ChartLine1.Series[i].Points[j].ToolTip = "Color : " + DSet.Tables[i].Rows[j]["TableName"].ToString() + " \nDate : " + Convert.ToDateTime(DSet.Tables[i].Rows[j]["CreatedDT"]).ToString() + "\nDelta E Ave : " + DSet.Tables[i].Rows[j]["DE"].ToString() + "";
                    }
                }

                //========== Chart to Bottom ==========//
                ChartLine1.Legends[0].Docking = LegendDocking.Bottom;
                ChartLine1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                string FileName = "UploadFiles/temp/" + Sessions.UserID + "_" + Path.GetFileName(ChartLine1.ImageUrl) + "." + ChartLine1.ImageType;
                ChartLine1.Save(Server.MapPath(FileName));

                foreach (Dundas.Charting.WebControl.Series objS in ChartLine1.Series)
                {
                    objS.Type = SeriesChartType.Spline;
                    objS.MarkerStyle = MarkerStyle.Triangle;
                }

                //========== Other CHart Settings ==========//
                ChartLine1.AJAXZoomEnabled = true;

                // scrolling and zooming will force keeping of series data between callbacks.
                ChartLine1.ChartAreas["Default"].CursorX.UserEnabled = true;

                // Set initial X axis zooming                        
                ChartLine1.ChartAreas["Default"].AxisY.View.MinSize = 10.0;
                ChartLine1.ChartAreas["Default"].AxisX.View.Position = 0.0;
                ChartLine1.ChartAreas["Default"].AxisX.View.Size = 50.0;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }
        private void FillLineChart2()
        {
            try
            {
                //========== DSet ==========//
                SqlCommand Cmd = new SqlCommand();

                Cmd.CommandText = "";
                //========== IC 5 ==========//
                Cmd.CommandText += "SELECT 'IC5' as IC , Deviation AS Value , CPM.CreatedDT AS Created_time FROM CPMeasurement  AS CPM INNER JOIN CPPatchDataPlate As PlateData ON PlateData.MeasurementGUID = CPM.CPMeasurementGUID  WHERE DOT_AREA_TARGET = 5  AND MeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ORDER BY Created_time;";
                //========== IC 25 ==========//
                Cmd.CommandText += "SELECT 'IC25' as IC , Deviation AS Value , CPM.CreatedDT AS Created_time FROM CPMeasurement  AS CPM INNER JOIN CPPatchDataPlate  As PlateData ON PlateData.MeasurementGUID = CPM.CPMeasurementGUID WHERE DOT_AREA_TARGET = 25  AND MeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ORDER BY Created_time;";
                //========== IC 50 ==========//
                Cmd.CommandText += "SELECT 'IC50' as IC , Deviation AS Value , CPM.CreatedDT AS Created_time FROM CPMeasurement  AS CPM INNER JOIN CPPatchDataPlate  As PlateData ON PlateData.MeasurementGUID = CPM.CPMeasurementGUID WHERE DOT_AREA_TARGET = 50  AND MeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ORDER BY Created_time;";
                //========== IC 75 ==========//
                Cmd.CommandText += "SELECT 'IC75' as IC , Deviation AS Value , CPM.CreatedDT AS Created_time FROM CPMeasurement  AS CPM INNER JOIN CPPatchDataPlate  As PlateData ON PlateData.MeasurementGUID = CPM.CPMeasurementGUID WHERE DOT_AREA_TARGET = 75  AND MeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ORDER BY Created_time;";
                //========== IC 95 ==========//        
                Cmd.CommandText += "SELECT 'IC95' as IC , Deviation AS Value , CPM.CreatedDT AS Created_time FROM CPMeasurement  AS CPM INNER JOIN CPPatchDataPlate  As PlateData ON PlateData.MeasurementGUID = CPM.CPMeasurementGUID WHERE DOT_AREA_TARGET = 95  AND MeasurementGUID IN (" + LTRL_CPMeasurementGUID.Text + ") ORDER BY Created_time;";

                //========== Execute The Dataset ==========//
                Cmd.CommandType = CommandType.Text;
                DataSet DSet = (new Database()).GetDataSet(Cmd);

                //========== Fill Chart ==========//
                ChartLine1.Titles.Clear();
                ChartLine1.Titles.Add("Std. Deviation Density");
                ChartLine1.Series.Clear();
                ChartLine1.CallbackStateContent = CallbackStateContent.All;

                for (int i = 0; i < DSet.Tables.Count; i = i + 1)
                {
                    //========== Add Series ==========//
                    Series objSeries = new Series();

                    objSeries.Type = SeriesChartType.Spline;

                    if (i == 0) { objSeries.Name = "IC5"; }
                    else if (i == 1) { objSeries.Name = "IC25"; }
                    else if (i == 2) { objSeries.Name = "IC50"; }
                    else if (i == 3) { objSeries.Name = "IC75"; }
                    else { objSeries.Name = "IC95"; }

                    objSeries.XValueType = ChartValueTypes.DateTime;
                    objSeries.YValueType = ChartValueTypes.Double;

                    objSeries.ValueMemberX = "Created_time";
                    objSeries.ValueMembersY = "IC";

                    ChartLine1.Series.Add(objSeries);

                    //========== Add Values ==========//
                    for (int j = 0; j < DSet.Tables[i].Rows.Count; j = j + 1)
                    {
                        //ChartLine1.Series[i].Color = System.Drawing.Color.FromName(DSet.Tables[i].Rows[0]["TableName"].ToString());
                        if (Convert.ToDecimal(DSet.Tables[i].Rows[j]["Value"]) >= 12)
                        { ChartLine1.Series[i].Points.AddXY(DSet.Tables[i].Rows[j]["Created_time"].ToString(), "12"); }
                        else if (Convert.ToDecimal(DSet.Tables[i].Rows[j]["Value"]) <= -12)
                        { ChartLine1.Series[i].Points.AddXY(DSet.Tables[i].Rows[j]["Value"].ToString(), "-12"); }
                        else
                        { ChartLine1.Series[i].Points.AddXY(DSet.Tables[i].Rows[j]["Created_time"].ToString(), DSet.Tables[i].Rows[j]["Value"].ToString()); }
                        ChartLine1.Series[i].Points[j].ToolTip = "Date : " + Convert.ToDateTime(DSet.Tables[i].Rows[j]["Created_time"]).ToString() + "\n" + DSet.Tables[i].Rows[j]["IC"].ToString() + " : " + DSet.Tables[i].Rows[j]["Value"].ToString() + "";
                    }
                }

                //========== Chart to Bottom ==========//
                ChartLine1.Legends[0].Docking = LegendDocking.Bottom;
                ChartLine1.Legends[0].Alignment = System.Drawing.StringAlignment.Center;

                string FileName = "UploadFiles/temp/" + Sessions.UserID + "_" + Path.GetFileName(ChartLine1.ImageUrl) + "." + ChartLine1.ImageType;
                ChartLine1.Save(Server.MapPath(FileName));

                foreach (Dundas.Charting.WebControl.Series objS in ChartLine1.Series)
                {
                    objS.Type = SeriesChartType.Spline;
                    objS.MarkerStyle = MarkerStyle.Triangle;
                }

                //========== Other CHart Settings ==========//
                ChartLine1.AJAXZoomEnabled = true;

                // scrolling and zooming will force keeping of series data between callbacks.
                ChartLine1.ChartAreas["Default"].CursorX.UserEnabled = true;

                // Set initial X axis zooming                        
                ChartLine1.ChartAreas["Default"].AxisY.View.MinSize = 10.0;
                ChartLine1.ChartAreas["Default"].AxisX.View.Position = 0.0;
                ChartLine1.ChartAreas["Default"].AxisX.View.Size = 50.0;
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

                DR_BasicInfo["CompanyName_"] = LTRL_CompanyName.Text.Trim();
                DR_BasicInfo["Device_"] = LTRL_DeviceName.Text.Trim();
                DR_BasicInfo["DongleNumber_"] = LTRL_DongleNumber.Text.Trim();
                DR_BasicInfo["Location_"] = LTRL_BranchName.Text.Trim();
                DR_BasicInfo["DateRange_"] = LTRL_DateRange.Text.Trim();

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

                #region Failure Rate

                DataTable DT_FailureRate = new DataTable("FailureRate");

                DataColumn DC_FailureRate = new DataColumn();
                DC_FailureRate.ColumnName = "NumofMeasurements_";
                DC_FailureRate.DataType = System.Type.GetType("System.String");
                DT_FailureRate.Columns.Add(DC_FailureRate);

                DC_FailureRate = new DataColumn();
                DC_FailureRate.ColumnName = "Passed_";
                DC_FailureRate.DataType = System.Type.GetType("System.String");
                DT_FailureRate.Columns.Add(DC_FailureRate);

                DC_FailureRate = new DataColumn();
                DC_FailureRate.ColumnName = "Failed_";
                DC_FailureRate.DataType = System.Type.GetType("System.String");
                DT_FailureRate.Columns.Add(DC_FailureRate);

                DC_FailureRate = new DataColumn();
                DC_FailureRate.ColumnName = "Cautions_";
                DC_FailureRate.DataType = System.Type.GetType("System.String");
                DT_FailureRate.Columns.Add(DC_FailureRate);

                DataRow DR_FailureRate = DT_FailureRate.NewRow();

                DR_FailureRate["NumofMeasurements_"] = LTRL_NumofMeasurements.Text.Trim();
                DR_FailureRate["Passed_"] = LTRL_Passed.Text.Trim();
                DR_FailureRate["Failed_"] = LTRL_Failed.Text.Trim();
                DR_FailureRate["Cautions_"] = LTRL_Cautions.Text.Trim();

                DT_FailureRate.Rows.Add(DR_FailureRate);

                ViewState["DT_FailureRate"] = DT_FailureRate;

                #endregion

                #region Environmental

                DataTable DT_Environmental = new DataTable("Environmental");

                DataColumn DC_Environmental = new DataColumn();
                DC_Environmental.ColumnName = "AveTemp_";
                DC_Environmental.DataType = System.Type.GetType("System.String");
                DT_Environmental.Columns.Add(DC_Environmental);

                DC_Environmental = new DataColumn();
                DC_Environmental.ColumnName = "AveRH_";
                DC_Environmental.DataType = System.Type.GetType("System.String");
                DT_Environmental.Columns.Add(DC_Environmental);

                DC_Environmental = new DataColumn();
                DC_Environmental.ColumnName = "MinTemp_";
                DC_Environmental.DataType = System.Type.GetType("System.String");
                DT_Environmental.Columns.Add(DC_Environmental);

                DC_Environmental = new DataColumn();
                DC_Environmental.ColumnName = "MaxTemp_";
                DC_Environmental.DataType = System.Type.GetType("System.String");
                DT_Environmental.Columns.Add(DC_Environmental);

                DC_Environmental = new DataColumn();
                DC_Environmental.ColumnName = "MinRH_";
                DC_Environmental.DataType = System.Type.GetType("System.String");
                DT_Environmental.Columns.Add(DC_Environmental);

                DC_Environmental = new DataColumn();
                DC_Environmental.ColumnName = "MaxRH_";
                DC_Environmental.DataType = System.Type.GetType("System.String");
                DT_Environmental.Columns.Add(DC_Environmental);

                DataRow DR_Environmental = DT_Environmental.NewRow();

                DR_Environmental["AveTemp_"] = LTRL_AveTemp.Text.Trim();
                DR_Environmental["AveRH_"] = LTRL_TargetTemp.Text.Trim();
                DR_Environmental["MinTemp_"] = LTRL_MinTemp.Text.Trim();
                DR_Environmental["MaxTemp_"] = LTRL_MaxTemp.Text.Trim();
                DR_Environmental["MinRH_"] = LTRL_MinRH.Text.Trim();
                DR_Environmental["MaxRH_"] = LTRL_MaxRH.Text.Trim();

                DT_Environmental.Rows.Add(DR_Environmental);

                ViewState["DT_Environmental"] = DT_Environmental;

                #endregion

                #region Consumables

                DataTable DT_Consumables = new DataTable("Consumables");

                DataColumn DC_Consumables = new DataColumn();
                DC_Consumables.ColumnName = "Measurements_";
                DC_Consumables.DataType = System.Type.GetType("System.String");
                DT_Consumables.Columns.Add(DC_Consumables);

                DC_Consumables = new DataColumn();
                DC_Consumables.ColumnName = "DeltaEAve_";
                DC_Consumables.DataType = System.Type.GetType("System.String");
                DT_Consumables.Columns.Add(DC_Consumables);

                DC_Consumables = new DataColumn();
                DC_Consumables.ColumnName = "DeltaMax_";
                DC_Consumables.DataType = System.Type.GetType("System.String");
                DT_Consumables.Columns.Add(DC_Consumables);

                DC_Consumables = new DataColumn();
                DC_Consumables.ColumnName = "DeltaDensityAve_";
                DC_Consumables.DataType = System.Type.GetType("System.String");
                DT_Consumables.Columns.Add(DC_Consumables);

                DC_Consumables = new DataColumn();
                DC_Consumables.ColumnName = "StdDev_";
                DC_Consumables.DataType = System.Type.GetType("System.String");
                DT_Consumables.Columns.Add(DC_Consumables);

                DataRow DR_Consumables = DT_Consumables.NewRow();
                DR_Consumables["Measurements_"] = LTRL_PaperMeasurements.Text.Trim();
                DR_Consumables["DeltaEAve_"] = LTRL_PaperDeltaEAve.Text.Trim();
                DR_Consumables["DeltaMax_"] = LTRL_PaperDeltaMax.Text.Trim();
                DR_Consumables["DeltaDensityAve_"] = LTRL_PaperDeltaDensityAve.Text.Trim();
                DR_Consumables["StdDev_"] = LTRL_PaperStdDev.Text.Trim();
                DT_Consumables.Rows.Add(DR_Consumables);

                DR_Consumables = DT_Consumables.NewRow();
                DR_Consumables["Measurements_"] = LTRL_InkCyanMeasurements.Text.Trim();
                DR_Consumables["DeltaEAve_"] = LTRL_InkCyanDeltaEAve.Text.Trim();
                DR_Consumables["DeltaMax_"] = LTRL_InkCyanDeltaMax.Text.Trim();
                DR_Consumables["DeltaDensityAve_"] = LTRL_InkCyanDeltaDensityAve.Text.Trim();
                DR_Consumables["StdDev_"] = LTRL_InkCyanStdDev.Text.Trim();
                DT_Consumables.Rows.Add(DR_Consumables);

                DR_Consumables = DT_Consumables.NewRow();
                DR_Consumables["Measurements_"] = LTRL_InkMagentaMeasurements.Text.Trim();
                DR_Consumables["DeltaEAve_"] = LTRL_InkMagentaDeltaEAve.Text.Trim();
                DR_Consumables["DeltaMax_"] = LTRL_InkMagentaDeltaMax.Text.Trim();
                DR_Consumables["DeltaDensityAve_"] = LTRL_InkMagentaDeltaDensityAve.Text.Trim();
                DR_Consumables["StdDev_"] = LTRL_InkMagentaStdDev.Text.Trim();
                DT_Consumables.Rows.Add(DR_Consumables);

                DR_Consumables = DT_Consumables.NewRow();
                DR_Consumables["Measurements_"] = LTRL_InkYellowMeasurements.Text.Trim();
                DR_Consumables["DeltaEAve_"] = LTRL_InkYellowDeltaEAve.Text.Trim();
                DR_Consumables["DeltaMax_"] = LTRL_InkYellowDeltaMax.Text.Trim();
                DR_Consumables["DeltaDensityAve_"] = LTRL_InkYellowDeltaDensityAve.Text.Trim();
                DR_Consumables["StdDev_"] = LTRL_InkYellowStdDev.Text.Trim();
                DT_Consumables.Rows.Add(DR_Consumables);

                DR_Consumables = DT_Consumables.NewRow();
                DR_Consumables["Measurements_"] = LTRL_InkBlackMeasurements.Text.Trim();
                DR_Consumables["DeltaEAve_"] = LTRL_InkBlackDeltaEAve.Text.Trim();
                DR_Consumables["DeltaMax_"] = LTRL_InkBlackDeltaMax.Text.Trim();
                DR_Consumables["DeltaDensityAve_"] = LTRL_InkBlackDeltaDensityAve.Text.Trim();
                DR_Consumables["StdDev_"] = LTRL_InkBlackStdDev.Text.Trim();
                DT_Consumables.Rows.Add(DR_Consumables);

                ViewState["DT_Consumables"] = DT_Consumables;

                #endregion
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion



    }
}