#region System
using Dundas.Charting.WebControl;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using Taskero.Business;
using Taskero.Common;
using TaskeroWeb.Models;
#endregion

namespace TaskeroWeb
{

    public partial class ColorTrend : PageBase
    {

        #region Explanations
        /// <summary>
        /// LblError = Error Message.
        /// 
        /// CPDevicePrintTargetID = Gets the Print Target ID.
        /// 
        /// LTRL_CPDeviceName = The device name.
        /// 
        /// LTRL_CPDeviceID = The only device selected.
        /// LTRL_CPDeviceIDs = The comma sepearted list of devices.
        /// 
        /// LTRL_CPMeasurementGUID = The only GUID selected
        /// LTRL_CPMeasurementGUIDs = The Comma Seperated GUIDs selected on the search screen.        
        /// </summary>
        #endregion

        #region Page Load and Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //========== Clear Error ==========//
                LblError.Text = "";

                //========== Default Settings ==========//
                if (IsPostBack == false)
                {
                    //========== Fill From Request Querystring ==========//
                    FillLabels();

                    //========== Set Key ==========//
                    LTRL_KeyGuid.Text = LTRL_CPMeasurementGUID.Text + "-Color-DT-AllGuids";
                    LTRL_KeyDeltaEChartWithDensity.Text = LTRL_CPMeasurementGUID.Text + "-DSet-DeltaEChartWithDensity";

                    //========== Clear the Sessions ==========//
                    Sessions.SetSessions(LTRL_KeyGuid.Text, null);

                    //========== Check the form is for device or Job ==========//
                    if (Request.QueryString["ReportType"] == null)
                    { DDL_PrintTarget.Visible = false; LTRL_PrintTargetTitle.Visible = false; }
                    else
                    { DDL_PrintTarget.Visible = true; LTRL_PrintTargetTitle.Visible = true; FillPrintTarget(); }



                    //========== Fill Charts ==========//
                    GetAllGuids();
                    RefillCharts();

                    //========== Plot Legend At Bottom ==========//            
                    CHRT_DeltaE.Legends[0].Docking = LegendDocking.Bottom;
                    CHRT_DeltaE.Legends[0].Alignment = System.Drawing.StringAlignment.Center;
                }
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        protected void BTN_DeltaE_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Fills All Charts ==========//
                RefillCharts();
            }
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }
        protected void BTN_Density_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Fills All Charts ==========//
                RefillCharts();
            }
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }
        protected void BTN_DotGain_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Fills All Charts ==========//
                RefillCharts();
            }
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }
        protected void BTN_Ink_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Fills All Charts ==========//
                RefillCharts();
            }
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }
        protected void BTN_PrintContrast_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Fills All Charts ==========//
                RefillCharts();
            }
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }
        protected void DDL_PrintTarget_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LTRL_CPDevicePrintTargetID.Text = DDL_PrintTarget.SelectedValue;
                Sessions.SetSessions(LTRL_KeyGuid.Text, null);
                GetAllGuids();
                //RefillCharts();

                //========== Gets the querystring and Redirects to that page ==========//
                DataTable DT = (DataTable)Sessions.GetSession(LTRL_KeyGuid.Text);
                if (DT.Rows.Count > 0)
                {
                    string _QueryString = "CPMeasurementGUID=" + DT.Rows[DT.Rows.Count - 1]["CPMeasurementGUID"].ToString();
                    _QueryString += "&CPDevicePrintTargetID=" + LTRL_CPDevicePrintTargetID.Text;
                    _QueryString += "&CPDeviceID=" + LTRL_CPDeviceID.Text;
                    if (Request.QueryString["ModalDialog"] != null) { _QueryString += "&ModalDialog=Y"; }
                    if (Request.QueryString["ReportType"] != null) { _QueryString += "&ReportType=D"; }
                    CommonFunctions.PageRedirect("ColorTrend", _QueryString);
                }
                else
                {
                    LblError.Text = "No Measurement found for Print Target : " + DDL_PrintTarget.SelectedItem.Text + ".";
                    DDL_PrintTarget.SelectedIndex = 0;
                    DDL_PrintTarget_OnSelectedIndexChanged(null, null);
                }
            }
            catch (Exception Ex)
            {
                LblError.Text = HandleException(Ex);
            }
        }
        #endregion

        #region Methods and Functions

        #region FillCharts & FillForms
        private void FillPrintTarget()
        {
            try
            {
                //========== Fill Print Target ==========//
                CPDevicePrintTargetData objCPDevicePrintTarget = new CPDevicePrintTargetData();
                objCPDevicePrintTarget.DeviceID = Convert.ToInt32(LTRL_CPDeviceID.Text);
                DataTable DT = objCPDevicePrintTarget.uspGetPrintTargetByDeviceID();

                //========== Fill DDL ==========//
                DDL_PrintTarget.DataSource = DT;
                DDL_PrintTarget.DataTextField = "PrintTarget";
                DDL_PrintTarget.DataValueField = "CPDevicePrintTargetID";
                DDL_PrintTarget.DataBind();

                //========== Default ==========//
                DDL_PrintTarget.Items.Insert(0, (new ListItem("All", "0")));
                DDL_PrintTarget.SelectedIndex = 0;

                //========== Select From the List ==========//
                DDL_PrintTarget.SelectedValue = LTRL_CPDevicePrintTargetID.Text;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void RefillCharts()
        {
            try
            {
                FillChartDeltaEAndDensity();
                FillChartDotGain();
                FillPrintContrast();
                FillTemperatureHumidity();
                /// FillChartInkLevel();  By Chirag on 10/29 may be we have to drop it

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void FillLabels()
        {
            try
            {
                LTRL_CPDeviceID.Text = Request.QueryString["CPDeviceID"].ToString();
                LTRL_CPDevicePrintTargetID.Text = Request.QueryString["CPDevicePrintTargetID"].ToString();
                LTRL_CPMeasurementGUID.Text = Request.QueryString["CPMeasurementGUID"].ToString();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void GetAllGuids()
        {
            try
            {
                //========== Clear Old Labels ==========//
                LTRL_CPMeasurementGUIDs.Text = "";
                LTRL_StatuSs.Text = "";

                //========== First Get the Data Set ==========//
                if (Sessions.GetSession(LTRL_KeyGuid.Text) == null)
                {
                    CPMeasurementData objCPMeasurement = new CPMeasurementData();
                    if (DDL_PrintTarget.Visible == false || LTRL_CPDevicePrintTargetID.Text == "0")
                    { objCPMeasurement.CPDeviceID = Convert.ToInt32(LTRL_CPDeviceID.Text); Sessions.SetSessions(LTRL_KeyGuid.Text, objCPMeasurement.uspGetCPMeasurementGUIDByCPDeviceID(Sessions.UserID, Sessions.RoleID)); }
                    else
                    { Sessions.SetSessions(LTRL_KeyGuid.Text, objCPMeasurement.uspGetCPMeasurementGUIDByCPDevicePrintTargetID(Convert.ToInt32(LTRL_CPDevicePrintTargetID.Text))); }
                }
                DataTable DT = (DataTable)Sessions.GetSession(LTRL_KeyGuid.Text);

                #region Paging
                #region Paging Variables
                //========== Settings for paging ==========//
                int _PageSize = Convert.ToInt32(LTRL_PageSize.Text);
                int _CurrentPageIndex = Convert.ToInt32(LTRL_CurrentPage.Text);
                int _PageCount = DT.Rows.Count / _PageSize; if ((DT.Rows.Count % _PageSize) > 0) { _PageCount = _PageCount + 1; }
                int _StartIndex = 1;
                int _EndIndex = 0;
                int _CPMOnPage = 0;
                #endregion

                #region Fill DDL only On Page Load
                if (IsPostBack == false)
                {
                    //========== Filling the DropDown ==========//
                    FillDDLGoToPage(_PageCount);

                    //========== Setting On the Page ==========//
                    for (int i = 0; i < DT.Rows.Count; i = i + 1)
                    {
                        if (LTRL_CPMeasurementGUID.Text == DT.Rows[i]["CPMeasurementGUID"].ToString())
                        {
                            _CPMOnPage = i / _PageSize;
                            break;
                        }
                    }
                    DDL_GoToPage.SelectedIndex = _CPMOnPage;

                }
                _CurrentPageIndex = Convert.ToInt32(DDL_GoToPage.SelectedValue);
                #endregion

                #region Settings for Paging
                _CurrentPageIndex = Convert.ToInt32(DDL_GoToPage.SelectedValue);

                if (_CurrentPageIndex == 0) { _StartIndex = 0; } else { _StartIndex = (_CurrentPageIndex * _PageSize); }

                _EndIndex = _StartIndex + _PageSize;

                if (DT.Rows.Count < _EndIndex) { _EndIndex = DT.Rows.Count; }

                //========== Show Hide Settings and Setting Label Values ==========//        
                IBTN_FirstPage.Enabled = true; IBTN_FirstPage.ImageUrl = "Images/Nav_FirstPage.gif";
                IBTN_PreviousPage.Enabled = true; IBTN_PreviousPage.ImageUrl = "Images/Nav_PreviousPage.gif";

                IBTN_NextPage.Enabled = true; IBTN_NextPage.ImageUrl = "Images/Nav_NextPage.gif";
                IBTN_LastPage.Enabled = true; IBTN_LastPage.ImageUrl = "Images/Nav_LastPage.gif";

                //=========== Don't Show if Current Page = 0 =========//
                if (_CurrentPageIndex == 0)
                {
                    IBTN_FirstPage.Enabled = false;
                    IBTN_PreviousPage.Enabled = false;

                    IBTN_FirstPage.ImageUrl = "Images/Nav_FirstPage_off.gif";
                    IBTN_PreviousPage.ImageUrl = "Images/Nav_PreviousPage_off.gif";
                }
                //=========== Don't Show if Current Page = Last Page =========//
                if (_CurrentPageIndex == (_PageCount - 1))
                {
                    IBTN_NextPage.Enabled = false;
                    IBTN_LastPage.Enabled = false;

                    IBTN_NextPage.ImageUrl = "Images/Nav_NextPage_Off.gif";
                    IBTN_LastPage.ImageUrl = "Images/Nav_LastPage_Off.gif";
                }

                //=========== Don't Show if PageCount = 0 =========//
                if (_PageCount == 0)
                {

                    IBTN_FirstPage.Enabled = false;
                    IBTN_PreviousPage.Enabled = false;

                    IBTN_NextPage.Enabled = false;
                    IBTN_LastPage.Enabled = false;

                    IBTN_FirstPage.ImageUrl = "Images/Nav_FirstPage_off.gif";
                    IBTN_PreviousPage.ImageUrl = "Images/Nav_PreviousPage_off.gif";

                    IBTN_NextPage.ImageUrl = "Images/Nav_NextPage_Off.gif";
                    IBTN_LastPage.ImageUrl = "Images/Nav_LastPage_Off.gif";

                }

                //========== Set Values Back To Label ==========//
                LTRL_StartIndex.Text = _StartIndex.ToString();
                LTRL_CurrentPage.Text = _CurrentPageIndex.ToString();
                LTRL_StartRecord.Text = Convert.ToString(_StartIndex + 1);
                LTRL_EndRecord.Text = _EndIndex.ToString();
                LTRL_TotalRec.Text = DT.Rows.Count.ToString();
                #endregion
                #endregion

                LTRL_CPMeasurementGUIDs.Text = "";
                LTRL_StatuSs.Text = "";
                for (int i = _StartIndex; i < _EndIndex; i = i + 1)
                {
                    if (LTRL_CPMeasurementGUIDs.Text == "")
                    { LTRL_CPMeasurementGUIDs.Text = DT.Rows[i]["CPMeasurementGUID"].ToString(); LTRL_StatuSs.Text = DT.Rows[i]["StatuS"].ToString(); }
                    else
                    { LTRL_CPMeasurementGUIDs.Text += "," + DT.Rows[i]["CPMeasurementGUID"].ToString(); LTRL_StatuSs.Text += "," + DT.Rows[i]["StatuS"].ToString(); }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void FillChartDeltaEAndDensity()
        {
            try
            {
                //========== Get The Delta E Data ==========//        
                //if (Sessions.SetGet(LTRL_KeyDeltaEChartWithDensity.Text) == null)
                {
                    CPPatchDataLabData objCPPatchDataLab = new CPPatchDataLabData();
                    objCPPatchDataLab.MeasurementGUID = LTRL_CPMeasurementGUIDs.Text;
                    Sessions.SetSessions(LTRL_KeyDeltaEChartWithDensity.Text, objCPPatchDataLab.uspGetDTLTrendAveDeByMeasurementGUID1());
                }
                DataTable DT = (DataTable)Sessions.GetSession(LTRL_KeyDeltaEChartWithDensity.Text);

                #region Delta E
                //========== Get all the series ==========//
                Series objSCyan = (Series)CHRT_DeltaE.Series["Series1"]; // Cyan
                Series objSMagenta = (Series)CHRT_DeltaE.Series["Series2"]; // Magenta
                Series objSYellow = (Series)CHRT_DeltaE.Series["Series3"]; // Yellow
                Series objSBlack = (Series)CHRT_DeltaE.Series["Series4"]; // Black
                Series objSGray = (Series)CHRT_DeltaE.Series["Series5"]; // Gray
                Series objSdEPaperWhite = (Series)CHRT_DeltaE.Series["Series6"]; // de Paper White
                Series objSdEAverage = (Series)CHRT_DeltaE.Series["Series7"]; // de Average
                Series objSdEMaximum = (Series)CHRT_DeltaE.Series["Series8"]; // de Maximum
                Series objSdEPrimary = (Series)CHRT_DeltaE.Series["Series9"]; // de Primary

                //========== Assign Series Settings ==========//
                AssignSeriesSettings(objSCyan);
                AssignSeriesSettings(objSMagenta);
                AssignSeriesSettings(objSYellow);
                AssignSeriesSettings(objSBlack);
                AssignSeriesSettings(objSGray);
                AssignSeriesSettings(objSdEPaperWhite);
                AssignSeriesSettings(objSdEAverage);
                AssignSeriesSettings(objSdEMaximum);
                AssignSeriesSettings(objSdEPrimary);
                #endregion

                #region Density
                //========== Get all the series ==========//
                Series objSCyan1 = (Series)CHRT_Density.Series["Series1"]; // Cyan
                Series objSMagenta1 = (Series)CHRT_Density.Series["Series2"]; // Magenta
                Series objSYellow1 = (Series)CHRT_Density.Series["Series3"]; // Yellow
                Series objSBlack1 = (Series)CHRT_Density.Series["Series4"]; // Black

                //========== Assign Series Settings ==========//
                AssignSeriesSettings(objSCyan1);
                AssignSeriesSettings(objSMagenta1);
                AssignSeriesSettings(objSYellow1);
                AssignSeriesSettings(objSBlack1);
                #endregion

                #region Plot Values
                for (int i = 0; i < DT.Rows.Count; i = i + 1)
                {
                    #region CHRT_Date
                    CHRT_Date.Series[0].Points.AddXY(DT.Rows[i]["Created_Time"].ToString(), "0");
                    #endregion

                    #region Delta E
                    //========== Plot Values ==========//
                    //if (Convert.ToDecimal(DT.Rows[i]["Cyan_AVE_DE"]) > 2) { objSCyan.Points.AddXY(DT.Rows[i]["Created_Time"].ToString(), "2"); } else { objSCyan.Points.AddXY(DT.Rows[i]["Created_Time"].ToString(), DT.Rows[i]["Cyan_AVE_DE"].ToString()); }
                    //objSCyan.Points[i].ToolTip = " Color : Cyan, Delta E : " + Convert.ToDecimal(DT.Rows[i]["Cyan_AVE_DE"]).ToString("N2") + ", Created Date : " + DT.Rows[i]["CREATED_TIME"].ToString() + ", GUID : " + DT.Rows[i]["MeasurementGUID"].ToString();
                    //objSCyan.Points[i].MapAreaAttributes = "OnClick=\"javascript:PageRedirect( '" + DT.Rows[i]["MeasurementGUID"].ToString() + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' );\"";

                    //========== Cyan ==========//
                    PlotDeltaEValues(objSCyan, i, "Cyan", Convert.ToDecimal(DT.Rows[i]["Cyan_AVE_DE"]), DT.Rows[i]["MeasurementGUID"].ToString(), DT.Rows[i]["Created_Time"].ToString());
                    ChangeMarkerColor(i, DT.Rows[i]["MeasurementGUID"].ToString(), objSCyan);

                    //========== Magenta ==========//
                    PlotDeltaEValues(objSMagenta, i, "Magenta", Convert.ToDecimal(DT.Rows[i]["Magenta_AVE_DE"]), DT.Rows[i]["MeasurementGUID"].ToString(), DT.Rows[i]["Created_Time"].ToString());
                    ChangeMarkerColor(i, DT.Rows[i]["MeasurementGUID"].ToString(), objSMagenta);

                    //========== Yellow ==========//
                    PlotDeltaEValues(objSYellow, i, "Yellow", Convert.ToDecimal(DT.Rows[i]["Yellow_AVE_DE"]), DT.Rows[i]["MeasurementGUID"].ToString(), DT.Rows[i]["Created_Time"].ToString());
                    ChangeMarkerColor(i, DT.Rows[i]["MeasurementGUID"].ToString(), objSYellow);

                    //========== Black ==========//
                    PlotDeltaEValues(objSBlack, i, "Black", Convert.ToDecimal(DT.Rows[i]["Black_AVE_DE"]), DT.Rows[i]["MeasurementGUID"].ToString(), DT.Rows[i]["Created_Time"].ToString());
                    ChangeMarkerColor(i, DT.Rows[i]["MeasurementGUID"].ToString(), objSBlack);

                    //========== Gray ==========//
                    PlotDeltaEValues(objSGray, i, "Gray", Convert.ToDecimal(DT.Rows[i]["Gray_AVE_DE"]), DT.Rows[i]["MeasurementGUID"].ToString(), DT.Rows[i]["Created_Time"].ToString());
                    ChangeMarkerColor(i, DT.Rows[i]["MeasurementGUID"].ToString(), objSGray);

                    //========== de Paper White ==========//
                    PlotDeltaEValues(objSdEPaperWhite, i, "de Paper White", Convert.ToDecimal(DT.Rows[i]["dePaperWhite"]), DT.Rows[i]["MeasurementGUID"].ToString(), DT.Rows[i]["Created_Time"].ToString());
                    ChangeMarkerColor(i, DT.Rows[i]["MeasurementGUID"].ToString(), objSdEPaperWhite);

                    //========== de Average ==========//
                    PlotDeltaEValues(objSdEAverage, i, "de Average", Convert.ToDecimal(DT.Rows[i]["deAverage"]), DT.Rows[i]["MeasurementGUID"].ToString(), DT.Rows[i]["Created_Time"].ToString());
                    ChangeMarkerColor(i, DT.Rows[i]["MeasurementGUID"].ToString(), objSdEAverage);

                    //========== de Maximum ==========//
                    PlotDeltaEValues(objSdEMaximum, i, "de Maximum", Convert.ToDecimal(DT.Rows[i]["deMaximum"]), DT.Rows[i]["MeasurementGUID"].ToString(), DT.Rows[i]["Created_Time"].ToString());
                    ChangeMarkerColor(i, DT.Rows[i]["MeasurementGUID"].ToString(), objSdEMaximum);

                    //========== de Primary ==========//
                    PlotDeltaEValues(objSdEPrimary, i, "de Primary", Convert.ToDecimal(DT.Rows[i]["dePrimary"]), DT.Rows[i]["MeasurementGUID"].ToString(), DT.Rows[i]["Created_Time"].ToString());
                    ChangeMarkerColor(i, DT.Rows[i]["MeasurementGUID"].ToString(), objSdEPrimary);

                    //========== Hides if not available ==========//
                    if (CHK_DeltaE_Cyan.Checked == false) { objSCyan.Enabled = false; }
                    if (CHK_DeltaE_Magenta.Checked == false) { objSMagenta.Enabled = false; }
                    if (CHK_DeltaE_Yellow.Checked == false) { objSYellow.Enabled = false; }
                    if (CHK_DeltaE_Black.Checked == false) { objSBlack.Enabled = false; }
                    if (CHK_DeltaE_Gray.Checked == false) { objSGray.Enabled = false; }
                    if (CHK_DeltaE_PaperWhite.Checked == false) { objSdEPaperWhite.Enabled = false; }
                    if (CHK_DeltaE_Average.Checked == false) { objSdEAverage.Enabled = false; }
                    if (CHK_DeltaE_Maximum.Checked == false) { objSdEMaximum.Enabled = false; }
                    if (CHK_DeltaE_Primary.Checked == false) { objSdEPrimary.Enabled = false; }

                    #endregion

                    #region Density
                    //========== Cyan ==========//
                    PlotDensityValues(objSCyan1, i, "Cyan", Convert.ToDecimal(DT.Rows[i]["Cyan_Density"]), DT.Rows[i]["MeasurementGUID"].ToString(), DT.Rows[i]["Created_Time"].ToString());
                    ChangeMarkerColor(i, DT.Rows[i]["MeasurementGUID"].ToString(), objSCyan1);

                    //========== Magenta ==========//
                    PlotDensityValues(objSMagenta1, i, "Magenta", Convert.ToDecimal(DT.Rows[i]["Magenta_Density"]), DT.Rows[i]["MeasurementGUID"].ToString(), DT.Rows[i]["Created_Time"].ToString());
                    ChangeMarkerColor(i, DT.Rows[i]["MeasurementGUID"].ToString(), objSMagenta1);

                    //========== Yellow ==========//
                    PlotDensityValues(objSYellow1, i, "Yellow", Convert.ToDecimal(DT.Rows[i]["Yellow_Density"]), DT.Rows[i]["MeasurementGUID"].ToString(), DT.Rows[i]["Created_Time"].ToString());
                    ChangeMarkerColor(i, DT.Rows[i]["MeasurementGUID"].ToString(), objSYellow1);

                    //========== Black ==========//
                    PlotDensityValues(objSBlack1, i, "Black", Convert.ToDecimal(DT.Rows[i]["Black_Density"]), DT.Rows[i]["MeasurementGUID"].ToString(), DT.Rows[i]["Created_Time"].ToString());
                    ChangeMarkerColor(i, DT.Rows[i]["MeasurementGUID"].ToString(), objSBlack1);

                    //========== Hides if not available ==========//
                    if (CHK_Density_Cyan.Checked == false) { objSCyan1.Enabled = false; }
                    if (CHK_Density_Magenta.Checked == false) { objSMagenta1.Enabled = false; }
                    if (CHK_Density_Yellow.Checked == false) { objSYellow1.Enabled = false; }
                    if (CHK_Density_Black.Checked == false) { objSBlack1.Enabled = false; }
                    #endregion
                }
                #endregion

                #region Add A Blank Entry
                if (DT.Rows.Count == 0)
                {
                    AddBlankRow(CHRT_Date);
                    AddBlankRow(CHRT_DeltaE);
                    AddBlankRow(CHRT_Density);
                }
                #endregion
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void FillChartDotGain()
        {
            try
            {
                //========== Declarations ==========//
                CPPatchDataLabData objColorCharts;
                DataSet DSet;

                //========== Get The Dot Gain ==========//
                string[] _GUID = LTRL_CPMeasurementGUIDs.Text.Split(',');

                //========== Get to have Date Time ==========//
                DataTable DT = (DataTable)Sessions.GetSession(LTRL_KeyDeltaEChartWithDensity.Text);

                #region Fill DotGain
                for (int k = 0; k < _GUID.Length; k = k + 1)
                {
                    //========== Get the dataset ==========//
                    objColorCharts = new CPPatchDataLabData();
                    objColorCharts.MeasurementGUID = _GUID[k];
                    DSet = objColorCharts.uspGetColorCharDotGain();
                    //uspGetColorCharDotGain

                    //========== Loops to Plot the series ==========//
                    for (int i = 0; i < DSet.Tables.Count; i = i + 1)
                    {

                        if (DT.Rows.Count > 0)
                        {
                            for (int j = 0; j < DSet.Tables[i].Rows.Count; j = j + 1)
                            {
                                //========== Variables ==========//
                                string _ColorName = DSet.Tables[i].Rows[j]["ColorName"].ToString();

                                string _Created_Time = DT.Rows[k]["Created_Time"].ToString();

                                string _CMYK = Convert.ToInt32(DSet.Tables[i].Rows[j]["CMYK"]).ToString();
                                string _DotGain = DSet.Tables[i].Rows[j]["Dotgain"].ToString();
                                string _Tone = "";

                                if (i == 0)
                                { _Tone = "Quarter Tone"; }
                                else if (i == 1)
                                { _Tone = "Mid Tone"; }
                                else if (i == 2)
                                { _Tone = "Three Quarter Tone"; }

                                //========== Show Hide ==========//
                                if (_ColorName == "Cyan")
                                {
                                    if (CHK_DotGain_Cyan0.Checked == false && j == 0) { continue; }
                                    else if (CHK_DotGain_Cyan1.Checked == false && j == 1) { continue; }
                                    else if (CHK_DotGain_Cyan2.Checked == false && j == 2) { continue; }
                                }
                                else if (_ColorName == "Magenta")
                                {
                                    if (CHK_DotGain_Magenta0.Checked == false && j == 0) { continue; }
                                    else if (CHK_DotGain_Magenta1.Checked == false && j == 1) { continue; }
                                    else if (CHK_DotGain_Magenta2.Checked == false && j == 2) { continue; }
                                }
                                else if (_ColorName == "Yellow")
                                {
                                    if (CHK_DotGain_Yellow0.Checked == false && j == 0) { continue; }
                                    else if (CHK_DotGain_Yellow1.Checked == false && j == 1) { continue; }
                                    else if (CHK_DotGain_Yellow2.Checked == false && j == 2) { continue; }
                                }
                                else if (_ColorName == "Black")
                                {
                                    if (CHK_DotGain_Black0.Checked == false && j == 0) { continue; }
                                    else if (CHK_DotGain_Black1.Checked == false && j == 1) { continue; }
                                    else if (CHK_DotGain_Black2.Checked == false && j == 2) { continue; }
                                }

                                try
                                {
                                    Series objSeries = CHRT_DotGain.Series[_ColorName + j.ToString()];
                                    objSeries.Type = SeriesChartType.Spline;
                                    objSeries.XValueType = ChartValueTypes.DateTime;
                                    objSeries.YValueType = ChartValueTypes.Double;
                                    if (Convert.ToDecimal(_DotGain) > 25)
                                    { objSeries.Points.AddXY(_Created_Time, "25"); }
                                    else if (Convert.ToDecimal(_DotGain) < -25)
                                    { objSeries.Points.AddXY(_Created_Time, "-25"); }
                                    else
                                    {
                                        objSeries.Points.AddXY(_Created_Time, _DotGain);
                                    }
                                    if (objSeries.Points.Count > k && _GUID[k] != null)
                                    {
                                        objSeries.Points[k].ToolTip = "GUID : " + _GUID[k] + ", Created Date : " + _Created_Time + ", Color : " + _ColorName + " " + _Tone + " " + _CMYK + "%, Dotgain : " + Convert.ToDecimal(_DotGain).ToString("#.##");
                                        objSeries.Points[k].Href = "#";
                                        ChangeMarkerColor(k, _GUID[k], objSeries);
                                        objSeries.Points[k].MapAreaAttributes = "OnClick=\"javascript:PageRedirect( '" + _GUID[k] + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' );\"";
                                    }
                                }
                                catch
                                {
                                    continue;
                                }

                            }
                        }
                    }

                }
                #endregion

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void FillPrintContrast()
        {
            try
            {
                //========== Get The Print Contrast ==========//
                //if (Sessions.SetGet("DSet_GetPrintContrast") == null)
                {
                    CPPatchDataLabData objCPPatchDataLab = new CPPatchDataLabData();
                    objCPPatchDataLab.MeasurementGUID = LTRL_CPMeasurementGUIDs.Text;
                    Sessions.SetSessions("DSet_GetPrintContrast", objCPPatchDataLab.uspGetDTLTrendPrintContrast());
                }
                DataSet DSet = (DataSet)Sessions.GetSession("DSet_GetPrintContrast");

                //========== Instructions ==========//
                //Table[0] = Cyan
                //Table[1] = Magenta
                //Table[2] = Yellow
                //Table[3] = Black

                //========== Get all the tables working ==========//
                for (int i = 0; i < DSet.Tables.Count; i = i + 1)
                {
                    //========== Cyan ==========//
                    if (CHK_PrintContrast_Cyan.Checked == false && i == 0) { continue; }
                    //========== Magenta ==========//
                    else if (CHK_PrintContrast_Magenta.Checked == false && i == 1) { continue; }
                    //========== Yellow ==========//
                    else if (CHK_PrintContrast_Yellow.Checked == false && i == 2) { continue; }
                    //========== Black ==========//
                    else if (CHK_PrintContrast_Black.Checked == false && i == 3) { continue; }

                    //========== Charts ==========//
                    for (int j = 0; j < DSet.Tables[i].Rows.Count; j = j + 1)
                    {
                        //========== Calcualations and Variable Declarations ==========//
                        string _MeasurementGUID = DSet.Tables[i].Rows[j]["MeasurementGUID"].ToString();
                        string _Color = DSet.Tables[i].Rows[j]["Color"].ToString();
                        string _Created_Time = DSet.Tables[i].Rows[j]["Created_Time"].ToString();
                        decimal _D100 = Convert.ToDecimal(DSet.Tables[i].Rows[j]["D100"]);
                        decimal _D75 = Convert.ToDecimal(DSet.Tables[i].Rows[j]["D75"]);
                        decimal _Target_Value = Convert.ToDecimal(DSet.Tables[i].Rows[j]["Print_Contrast"]);

                        //========== Get Value ==========//
                        //decimal _Return_Value = Formulas.DotGain.CalcPrintContrast(_D100, _D75);
                        ////decimal _Deviation = _Target_Value - _Return_Value;
                        //decimal _Deviation = _Return_Value;

                        decimal _Deviation = 0;
                        if (_D100 == 0 && _D75 == 0)
                        { _Deviation = -1; }
                        else
                        { _Deviation = ((_D100 - _D75) / _D100) * 100; }

                        //========== Retrieve Series ==========//
                        Series objSeries = (Series)CHRT_PrintContrast.Series[i + 1];
                        objSeries.Type = SeriesChartType.Spline;
                        objSeries.XValueType = ChartValueTypes.DateTime;
                        objSeries.YValueType = ChartValueTypes.Double;

                        //========== Plot Area ==========//
                        if (_Deviation > 50) { objSeries.Points.AddXY(_Created_Time, "50"); }
                        else if (_Deviation < 0) { objSeries.Points.AddXY(_Created_Time, "0"); }
                        else { objSeries.Points.AddXY(_Created_Time, _Deviation.ToString()); }
                        objSeries.Points[j].ToolTip = "GUID : " + _MeasurementGUID + ", Created Date : " + _Created_Time + ", Color : " + _Color + ", Print Contrast : " + _Deviation.ToString("N2");
                        objSeries.Points[j].MapAreaAttributes = "OnClick=\"javascript:PageRedirect( '" + _MeasurementGUID + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' );\"";
                        ChangeMarkerColor(j, _MeasurementGUID, objSeries);
                        //========== Show yellow marker when CalcPrintContrast return -1 ==========//
                        if (_Deviation == -1)
                        {
                            objSeries.Points[j].MarkerImage = "Images/MarkerYellow.gif";
                            objSeries.Points[j].ToolTip = "Invalid data : -1";
                        }
                    }
                }

                #region Add A Blank Entry

                if (DSet.Tables[0].Rows.Count == 0)
                {
                    AddBlankRow(CHRT_PrintContrast);
                }
                #endregion
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void FillTemperatureHumidity()
        {
            try
            {
                //========== Fill Temperature Humidity ==========//
                //if (Sessions.SetGet("DT_TemperatureHumidity") == null)
                {
                    //TempRHDevice_CPDeviceData objTempRHDevice_CPDevice = new TempRHDevice_CPDeviceData();
                    //objTempRHDevice_CPDevice.CPDeviceID = Convert.ToInt32(LTRL_CPDeviceID.Text);
                    CPPatchDataLabData objCPPatchDataLab = new CPPatchDataLabData();
                    objCPPatchDataLab.MeasurementGUID = LTRL_CPMeasurementGUIDs.Text;
                    Sessions.SetSessions("DT_TemperatureHumidity", objCPPatchDataLab.uspGetDTLTrendTempRH());
                }
                DataTable DT = (DataTable)Sessions.GetSession("DT_TemperatureHumidity");
                DT.DefaultView.Sort = "CreatedDt ASC";

                if (DT.DefaultView.Count > 0)
                {
                    CHRT_TemperatureHumidity.Visible = true;

                    //========== Get Series ==========//
                    Series objSeries1 = (Series)CHRT_TemperatureHumidity.Series["Series1"];
                    objSeries1.Type = SeriesChartType.Spline;
                    objSeries1.XValueType = ChartValueTypes.DateTime;
                    objSeries1.YValueType = ChartValueTypes.Double;
                    //objSeries1.Label = "Fahrenheit";

                    Series objSeries2 = (Series)CHRT_TemperatureHumidity.Series["Series2"];
                    objSeries2.Type = SeriesChartType.Spline;
                    objSeries2.XValueType = ChartValueTypes.DateTime;
                    objSeries2.YValueType = ChartValueTypes.Double;
                    //objSeries1.Label = "Relative Humidity";

                    //========== Bind the Series ==========//
                    for (int i = 0; i < DT.DefaultView.Count; i = i + 1)
                    {
                        objSeries1.Points.AddXY(DT.DefaultView[i]["CreatedDt"].ToString(), DT.DefaultView[i]["TempF"].ToString());
                        objSeries1.Points[i].ToolTip = "Created Date : " + DT.DefaultView[i]["CreatedDt"].ToString() + ", Temp. Fahrenheit : " + DT.DefaultView[i]["TempF"].ToString();
                        objSeries1.Points[i].MarkerSize = 3;
                        objSeries1.Points[i].MarkerColor = Color.FromArgb(200, 25, 25, 25);
                        objSeries1.Points[i].MarkerStyle = MarkerStyle.Circle;

                        objSeries2.Points.AddXY(DT.DefaultView[i]["CreatedDt"].ToString(), DT.DefaultView[i]["RH"].ToString());
                        objSeries2.Points[i].ToolTip = "Created Date : " + DT.DefaultView[i]["CreatedDt"].ToString() + ", Relative Humidity : " + DT.DefaultView[i]["RH"].ToString();
                        objSeries2.Points[i].MarkerSize = 3;
                        objSeries2.Points[i].MarkerColor = Color.FromArgb(200, 25, 25, 25);
                        objSeries2.Points[i].MarkerStyle = MarkerStyle.Circle;
                    }

                    #region Add A Blank Row
                    if (DT.DefaultView.Count == 0)
                    {
                        AddBlankRow(CHRT_TemperatureHumidity);
                    }
                    #endregion
                }
                else
                { CHRT_TemperatureHumidity.Visible = false; }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void FillChartInkLevel()
        {
            try
            {

                #region Get All Series
                Series objSeries1 = (Series)CHRT_Ink.Series["Series1"];
                objSeries1.Type = SeriesChartType.Spline;
                objSeries1.XValueType = ChartValueTypes.DateTime;
                objSeries1.YValueType = ChartValueTypes.Double;

                Series objSeries2 = (Series)CHRT_Ink.Series["Series2"];
                objSeries2.Type = SeriesChartType.Spline;
                objSeries2.XValueType = ChartValueTypes.DateTime;
                objSeries2.YValueType = ChartValueTypes.Double;

                Series objSeries3 = (Series)CHRT_Ink.Series["Series3"];
                objSeries3.Type = SeriesChartType.Spline;
                objSeries3.XValueType = ChartValueTypes.DateTime;
                objSeries3.YValueType = ChartValueTypes.Double;

                Series objSeries4 = (Series)CHRT_Ink.Series["Series4"];
                objSeries4.Type = SeriesChartType.Spline;
                objSeries4.XValueType = ChartValueTypes.DateTime;
                objSeries4.YValueType = ChartValueTypes.Double;

                Series objSeries5 = (Series)CHRT_Ink.Series["Series5"];
                objSeries5.Type = SeriesChartType.Spline;
                objSeries5.XValueType = ChartValueTypes.DateTime;
                objSeries5.YValueType = ChartValueTypes.Double;

                Series objSeries6 = (Series)CHRT_Ink.Series["Series6"];
                objSeries6.Type = SeriesChartType.Spline;
                objSeries6.XValueType = ChartValueTypes.DateTime;
                objSeries6.YValueType = ChartValueTypes.Double;

                Series objSeries7 = (Series)CHRT_Ink.Series["Series7"];
                objSeries7.Type = SeriesChartType.Spline;
                objSeries7.XValueType = ChartValueTypes.DateTime;
                objSeries7.YValueType = ChartValueTypes.Double;

                Series objSeries8 = (Series)CHRT_Ink.Series["Series8"];
                objSeries8.Type = SeriesChartType.Spline;
                objSeries8.XValueType = ChartValueTypes.DateTime;
                objSeries8.YValueType = ChartValueTypes.Double;

                Series objSeries9 = (Series)CHRT_Ink.Series["Series9"];
                objSeries9.Type = SeriesChartType.Spline;
                objSeries9.XValueType = ChartValueTypes.DateTime;
                objSeries9.YValueType = ChartValueTypes.Double;
                #endregion

                #region Show Hide
                if (CHK_Ink_1.Checked == false) { objSeries1.Enabled = false; }
                if (CHK_Ink_2.Checked == false) { objSeries2.Enabled = false; }
                if (CHK_Ink_3.Checked == false) { objSeries3.Enabled = false; }
                if (CHK_Ink_4.Checked == false) { objSeries4.Enabled = false; }
                if (CHK_Ink_5.Checked == false) { objSeries5.Enabled = false; }
                if (CHK_Ink_6.Checked == false) { objSeries6.Enabled = false; }
                if (CHK_Ink_7.Checked == false) { objSeries7.Enabled = false; }
                if (CHK_Ink_8.Checked == false) { objSeries8.Enabled = false; }
                if (CHK_Ink_9.Checked == false) { objSeries9.Enabled = false; }
                #endregion

                //========== Get The Ink Level ==========//
                string[] _GUID = LTRL_CPMeasurementGUIDs.Text.Split(',');
                for (int k = 0; k < _GUID.Length; k = k + 1)
                {
                    CPMeasurement_PrinterData objCPMeasurement_Printer = new CPMeasurement_PrinterData();
                    objCPMeasurement_Printer.CPMeasurementGUID = _GUID[k];
                    Sessions.SetSessions("DT_GetInkLevel", objCPMeasurement_Printer.uspGetDTLTrendInk());

                    DataTable DT = (DataTable)Sessions.GetSession("DT_GetInkLevel");

                    #region Plot Point to Series
                    for (int i = 0; i < DT.DefaultView.Count; i++)
                    {
                        //========== Issue 149 Changed =========//
                        //Initialy Printer_InkValue_0 was checked as > 1.
                        if (i == 0)
                        {
                            if (CHK_Ink_1.Checked == true) { if (Convert.ToDecimal(DT.Rows[i]["InkLevel"]) > 0) { objSeries1.Points.AddXY(DT.Rows[i]["CreatedDt"].ToString(), "1"); } else { objSeries1.Points.AddXY(DT.Rows[i]["CreatedDt"].ToString(), DT.Rows[i]["InkLevel"].ToString()); } objSeries1.Points[k].ToolTip = "GUID : " + DT.Rows[i]["CPMeasurementGUID"].ToString() + ", Created Date : " + DT.Rows[i]["CreatedDt"].ToString() + " Ink : " + DT.Rows[i]["InkName"].ToString() + ", Value : " + DT.Rows[i]["InkLevel"].ToString(); ChangeMarkerColor(k, DT.Rows[i]["CPMeasurementGUID"].ToString(), objSeries1); objSeries1.Points[k].MapAreaAttributes = "OnClick=\"javascript:PageRedirect( '" + DT.Rows[i]["CPMeasurementGUID"].ToString() + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' );\""; if (Convert.ToDouble(DT.Rows[i]["InkLevel"]) <= 0) { ClearMarkerColor(k, objSeries1); } }
                        }
                        else if (i == 1)
                        {
                            if (CHK_Ink_2.Checked == true) { if (Convert.ToDecimal(DT.Rows[i]["InkLevel"]) > 0) { objSeries2.Points.AddXY(DT.Rows[i]["CreatedDt"].ToString(), "1"); } else { objSeries2.Points.AddXY(DT.Rows[i]["CreatedDt"].ToString(), DT.Rows[i]["InkLevel"].ToString()); } objSeries2.Points[k].ToolTip = "GUID : " + DT.Rows[i]["CPMeasurementGUID"].ToString() + ", Created Date : " + DT.Rows[i]["CreatedDt"].ToString() + " Ink : " + DT.Rows[i]["InkName"].ToString() + ", Value : " + DT.Rows[i]["InkLevel"].ToString(); ChangeMarkerColor(k, DT.Rows[i]["CPMeasurementGUID"].ToString(), objSeries2); objSeries2.Points[k].MapAreaAttributes = "OnClick=\"javascript:PageRedirect( '" + DT.Rows[i]["CPMeasurementGUID"].ToString() + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' );\""; if (Convert.ToDouble(DT.Rows[i]["InkLevel"]) <= 0) { ClearMarkerColor(k, objSeries2); } }
                        }

                        //if (CHK_Ink_3.Checked == true) { if (Convert.ToDecimal(DT.Rows[i+2]["InkLevel"]) > 0) { objSeries3.Points.AddXY(DT.Rows[i+2]["CreatedDt"].ToString(), "1"); } else { objSeries3.Points.AddXY(DT.Rows[i+2]["CreatedDt"].ToString(), DT.Rows[i]["InkLevel"].ToString()); } objSeries3.Points[i].ToolTip = "GUID : " + DT.Rows[i]["CPMeasurementGUID"].ToString() + ", Created Date : " + DT.Rows[i]["CreatedDt"].ToString() + " Ink : " + DT.Rows[i]["InkName"].ToString() + ", Value : " + DT.Rows[i]["InkLevel"].ToString(); ChangeMarkerColor(i, DT.Rows[i]["CPMeasurementGUID"].ToString(), objSeries3); objSeries3.Points[i].MapAreaAttributes = "OnClick=\"javascript:PageRedirect( '" + DT.Rows[i]["CPMeasurementGUID"].ToString() + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' );\""; if (Convert.ToDouble(DT.Rows[i]["InkLevel"]) <= 0) { ClearMarkerColor(i, objSeries3); } }
                        //if (CHK_Ink_4.Checked == true) { if (Convert.ToDecimal(DT.Rows[i+3]["InkLevel"]) > 0) { objSeries4.Points.AddXY(DT.Rows[i+3]["CreatedDt"].ToString(), "1"); } else { objSeries4.Points.AddXY(DT.Rows[i+3]["CreatedDt"].ToString(), DT.Rows[i]["InkLevel"].ToString()); } objSeries4.Points[i].ToolTip = "GUID : " + DT.Rows[i]["CPMeasurementGUID"].ToString() + ", Created Date : " + DT.Rows[i]["CreatedDt"].ToString() + " Ink : " + DT.Rows[i]["InkName"].ToString() + ", Value : " + DT.Rows[i]["InkLevel"].ToString(); ChangeMarkerColor(i, DT.Rows[i]["CPMeasurementGUID"].ToString(), objSeries4); objSeries4.Points[i].MapAreaAttributes = "OnClick=\"javascript:PageRedirect( '" + DT.Rows[i]["CPMeasurementGUID"].ToString() + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' );\""; if (Convert.ToDouble(DT.Rows[i]["InkLevel"]) <= 0) { ClearMarkerColor(i, objSeries4); } }
                        //if (CHK_Ink_5.Checked == true) { if (Convert.ToDecimal(DT.Rows[i+4]["InkLevel"]) > 0) { objSeries5.Points.AddXY(DT.Rows[i+4]["CreatedDt"].ToString(), "1"); } else { objSeries5.Points.AddXY(DT.Rows[i+4]["CreatedDt"].ToString(), DT.Rows[i]["InkLevel"].ToString()); } objSeries5.Points[i].ToolTip = "GUID : " + DT.Rows[i]["CPMeasurementGUID"].ToString() + ", Created Date : " + DT.Rows[i]["CreatedDt"].ToString() + " Ink : " + DT.Rows[i]["InkName"].ToString() + ", Value : " + DT.Rows[i]["InkLevel"].ToString(); ChangeMarkerColor(i, DT.Rows[i]["CPMeasurementGUID"].ToString(), objSeries5); objSeries5.Points[i].MapAreaAttributes = "OnClick=\"javascript:PageRedirect( '" + DT.Rows[i]["CPMeasurementGUID"].ToString() + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' );\""; if (Convert.ToDouble(DT.Rows[i]["InkLevel"]) <= 0) { ClearMarkerColor(i, objSeries5); } }
                        //if (CHK_Ink_6.Checked == true) { if (Convert.ToDecimal(DT.Rows[i+5]["InkLevel"]) > 0) { objSeries6.Points.AddXY(DT.Rows[i+5]["CreatedDt"].ToString(), "1"); } else { objSeries6.Points.AddXY(DT.Rows[i+5]["CreatedDt"].ToString(), DT.Rows[i]["InkLevel"].ToString()); } objSeries6.Points[i].ToolTip = "GUID : " + DT.Rows[i]["CPMeasurementGUID"].ToString() + ", Created Date : " + DT.Rows[i]["CreatedDt"].ToString() + " Ink : " + DT.Rows[i]["InkName"].ToString() + ", Value : " + DT.Rows[i]["InkLevel"].ToString(); ChangeMarkerColor(i, DT.Rows[i]["CPMeasurementGUID"].ToString(), objSeries6); objSeries6.Points[i].MapAreaAttributes = "OnClick=\"javascript:PageRedirect( '" + DT.Rows[i]["CPMeasurementGUID"].ToString() + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' );\""; if (Convert.ToDouble(DT.Rows[i]["InkLevel"]) <= 0) { ClearMarkerColor(i, objSeries6); } }
                        //if (CHK_Ink_7.Checked == true) { if (Convert.ToDecimal(DT.Rows[i+6]["InkLevel"]) > 0) { objSeries7.Points.AddXY(DT.Rows[i+6]["CreatedDt"].ToString(), "1"); } else { objSeries7.Points.AddXY(DT.Rows[i]["CreatedDt"].ToString(), DT.Rows[i]["InkLevel"].ToString()); } objSeries7.Points[i].ToolTip = "GUID : " + DT.Rows[i]["CPMeasurementGUID"].ToString() + ", Created Date : " + DT.Rows[i]["CreatedDt"].ToString() + " Ink : " + DT.Rows[i]["InkName"].ToString() + ", Value : " + DT.Rows[i]["InkLevel"].ToString(); ChangeMarkerColor(i, DT.Rows[i]["CPMeasurementGUID"].ToString(), objSeries7); objSeries7.Points[i].MapAreaAttributes = "OnClick=\"javascript:PageRedirect( '" + DT.Rows[i]["CPMeasurementGUID"].ToString() + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' );\""; if (Convert.ToDouble(DT.Rows[i]["InkLevel"]) <= 0) { ClearMarkerColor(i, objSeries7); } }
                        //if (CHK_Ink_8.Checked == true) { if (Convert.ToDecimal(DT.Rows[i+7]["InkLevel"]) > 0) { objSeries8.Points.AddXY(DT.Rows[i+7]["CreatedDt"].ToString(), "1"); } else { objSeries8.Points.AddXY(DT.Rows[i]["CreatedDt"].ToString(), DT.Rows[i]["InkLevel"].ToString()); } objSeries8.Points[i].ToolTip = "GUID : " + DT.Rows[i]["CPMeasurementGUID"].ToString() + ", Created Date : " + DT.Rows[i]["CreatedDt"].ToString() + " Ink : " + DT.Rows[i]["InkName"].ToString() + ", Value : " + DT.Rows[i]["InkLevel"].ToString(); ChangeMarkerColor(i, DT.Rows[i]["CPMeasurementGUID"].ToString(), objSeries8); objSeries8.Points[i].MapAreaAttributes = "OnClick=\"javascript:PageRedirect( '" + DT.Rows[i]["CPMeasurementGUID"].ToString() + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' );\""; if (Convert.ToDouble(DT.Rows[i]["InkLevel"]) <= 0) { ClearMarkerColor(i, objSeries8); } }
                        //if (CHK_Ink_9.Checked == true) { if (Convert.ToDecimal(DT.Rows[i+8]["InkLevel"]) > 0) { objSeries9.Points.AddXY(DT.Rows[i+8]["CreatedDt"].ToString(), "1"); } else { objSeries9.Points.AddXY(DT.Rows[i]["CreatedDt"].ToString(), DT.Rows[i]["InkLevel"].ToString()); } objSeries9.Points[i].ToolTip = "GUID : " + DT.Rows[i]["CPMeasurementGUID"].ToString() + ", Created Date : " + DT.Rows[i]["CreatedDt"].ToString() + " Ink : " + DT.Rows[i]["InkName"].ToString() + ", Value : " + DT.Rows[i]["InkLevel"].ToString(); ChangeMarkerColor(i, DT.Rows[i]["CPMeasurementGUID"].ToString(), objSeries9); objSeries9.Points[i].MapAreaAttributes = "OnClick=\"javascript:PageRedirect( '" + DT.Rows[i]["CPMeasurementGUID"].ToString() + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' );\""; if (Convert.ToDouble(DT.Rows[i]["InkLevel"]) <= 0) { ClearMarkerColor(i, objSeries9); } }
                    }
                    #endregion

                    #region Add A Blank Entry
                    if (DT.Rows.Count == 0)
                    {
                        AddBlankRow(CHRT_Ink);
                    }
                    #endregion

                }





            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Change Marker Color
        /// <summary>
        /// Give the green or red marker to the selected meaurement
        /// </summary>
        /// <param name="_Index"></param>
        /// <param name="_GUID"></param>
        /// <param name="_objS"></param>
        private void ChangeMarkerColor(int _Index, string _GUID, Series _objS)
        {
            try
            {
                int _Status = Convert.ToInt32(LTRL_StatuSs.Text.Split(',')[_Index]);
                _objS.Points[_Index].Href = "#";

                //========== Default ==========//
                {
                    _objS.Points[_Index].MarkerSize = 4;
                    _objS.Points[_Index].MarkerColor = Color.FromArgb(200, 25, 25, 25);
                    _objS.Points[_Index].MarkerStyle = MarkerStyle.Circle;
                }

                //========== Show The Red Marker for search criteria ==========//
                if (_Status == 0)
                {
                    _objS.Points[_Index].MarkerSize = 4;
                    _objS.Points[_Index].MarkerColor = Color.FromArgb(200, 255, 0, 0);
                    _objS.Points[_Index].MarkerImage = "Images/MarkerRed.gif";
                }
                else if (_Status == 1) //changed by chiarg on 06/05/2009
                {
                    _objS.Points[_Index].MarkerSize = 4;
                    _objS.Points[_Index].MarkerColor = Color.FromArgb(200, 255, 0, 0);
                    _objS.Points[_Index].MarkerImage = "Images/MarkerYellow.gif";


                }

                //========== Show The Green Marker for the one clicked ==========//
                if (_GUID == LTRL_CPMeasurementGUID.Text)
                {
                    if (_Status == 0)
                    { _objS.Points[_Index].MarkerImage = "Images/MarkerRed_Circle.gif"; }
                    else if (_Status == 1)
                    { _objS.Points[_Index].MarkerImage = "Images/MarkerYellow_Circle.gif"; }
                    else
                    { _objS.Points[_Index].MarkerImage = "Images/MarkerGreen.gif"; }
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void ClearMarkerColor(int _Index, Series _objS)
        {
            try
            {
                _objS.Points[_Index].MarkerSize = 1;
                _objS.Points[_Index].MarkerImage = "";
                _objS.Points[_Index].MarkerColor = Color.FromArgb(200, 25, 25, 25);
                _objS.Points[_Index].MarkerStyle = MarkerStyle.Circle;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Add Blank Entry
        private void AddBlankRow(Chart objChart)
        {
            try
            {
                Series objSeries0 = (Series)objChart.Series["Series0"];
                objSeries0.Type = SeriesChartType.Spline;
                objSeries0.XValueType = ChartValueTypes.DateTime;
                objSeries0.YValueType = ChartValueTypes.Double;

                objSeries0.Points.AddXY(System.DateTime.Now.ToString(), "1");
                objSeries0.Points.AddXY(System.DateTime.Now.AddDays(1).ToString(), "1");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region PlotDeltaEValues
        /// <summary>
        /// Plots the value for the series for CHRT_DeltaE
        /// </summary>
        private void PlotDeltaEValues(Series ObjSeries, int _Index, string _Title, decimal _AVE_DE, string _MeasurementGUID, string _Created_Time)
        {
            try
            {
                if (_AVE_DE > 10) { ObjSeries.Points.AddXY(_Created_Time, "10"); } else { ObjSeries.Points.AddXY(_Created_Time, _AVE_DE.ToString()); }
                //ObjSeries.Points[_Index].ToolTip = " Color : " + _Title + ", Delta E : " + _AVE_DE.ToString("N2") + ", Created Date : " + _Created_Time + ", GUID : " + _MeasurementGUID;
                ObjSeries.Points[_Index].ToolTip = "GUID : " + _MeasurementGUID + ", Created Date : " + _Created_Time + ", Color : " + _Title + ", Delta E : " + _AVE_DE.ToString("N2");
                ObjSeries.Points[_Index].MapAreaAttributes = "OnClick=\"javascript:PageRedirect( '" + _MeasurementGUID + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' );\"";
                ObjSeries.Points[_Index].Href = "#";
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region PlotDensityValues
        /// <summary>
        /// Plots the value for the series for CHRT_Density
        /// </summary>
        private void PlotDensityValues(Series ObjSeries, int _Index, string _Title, decimal _Density, string _MeasurementGUID, string _Created_Time)
        {
            try
            {
                if (_Density > 10) { ObjSeries.Points.AddXY(_Created_Time, "10"); } else { ObjSeries.Points.AddXY(_Created_Time, _Density.ToString()); }
                ObjSeries.Points[_Index].ToolTip = "GUID : " + _MeasurementGUID + ", Created Date : " + _Created_Time + ", Color : " + _Title + ", Density : " + _Density.ToString("N2");
                ObjSeries.Points[_Index].MapAreaAttributes = "OnClick=\"javascript:PageRedirect( '" + _MeasurementGUID + "' , '" + LTRL_CPDevicePrintTargetID.Text + "' , '" + LTRL_CPDeviceID.Text + "' );\"";
                ObjSeries.Points[_Index].Href = "#";
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Assign Series Settings
        /// <summary>
        /// Assign Default setting to a series
        /// </summary>
        private void AssignSeriesSettings(Series objSeries)
        {
            try
            {
                objSeries.Type = SeriesChartType.Spline;
                objSeries.XValueType = ChartValueTypes.DateTime;
                objSeries.YValueType = ChartValueTypes.Double;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Search Bar Events and Methods
        protected void DDL_PageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //========== Alter the page size ==========//
                LTRL_PageSize.Text = DDL_PageSize.SelectedValue;

                //========== Gets The Data Table ==========//
                DataTable DT = (DataTable)Sessions.GetSession(LTRL_KeyGuid.Text);
                int _PageSize = Convert.ToInt32(LTRL_PageSize.Text);
                int _PageCount = DT.Rows.Count / _PageSize; if ((DT.Rows.Count % _PageSize) > 0) { _PageCount = _PageCount + 1; }

                //========== Filling the DropDown ==========//
                DDL_GoToPage.Items.Clear();
                FillDDLGoToPage(_PageCount);

                GetAllGuids();
                RefillCharts();
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        protected void DDL_GoToPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //========== Directly to any page ==========//
                GetAllGuids();
                RefillCharts();
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        protected void IBTN_FirstPage_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Go To Page 1 ==========//
                DDL_GoToPage.SelectedIndex = 0;
                GetAllGuids();
                RefillCharts();
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        protected void IBTN_PreviousPage_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Go To previous page ==========//
                DDL_GoToPage.SelectedValue = Convert.ToString(Convert.ToInt32(DDL_GoToPage.SelectedValue) - 1);
                GetAllGuids();
                RefillCharts();
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        protected void IBTN_NextPage_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Go To next page ==========//
                DDL_GoToPage.SelectedValue = Convert.ToString(Convert.ToInt32(DDL_GoToPage.SelectedValue) + 1);
                GetAllGuids();
                RefillCharts();
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        protected void IBTN_LastPage_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Go To last page ==========//
                DDL_GoToPage.SelectedIndex = DDL_GoToPage.Items.Count - 1;
                GetAllGuids();
                RefillCharts();
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        private void FillDDLGoToPage(int _Count)
        {
            try
            {
                //========== Fills the Paging =========//
                for (int i = 0; i < _Count; i = i + 1)
                {
                    DDL_GoToPage.Items.Insert(i, (new ListItem((i + 1).ToString(), i.ToString())));
                }

                //========== Add A Def =========//
                if (_Count == 0) { DDL_GoToPage.Items.Insert(0, (new ListItem("--", "0"))); }

                //========== Sets Default =========//
                DDL_GoToPage.SelectedIndex = 0;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #endregion
    }
}