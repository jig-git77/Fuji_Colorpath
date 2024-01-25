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
    public partial class PressSheetTrend : PageBase
    {
        #region Page Load and Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //========== Clear Error ==========//
                LblError.Text = "";

                //========== Fill Form ==========//
                if (IsPostBack == false)
                {
                    #region " == Fill QueryString Values == "
                    //========== Assign Variables ==========//
                    LTRL_CPDeviceID.Text = Convert.ToString(Request.QueryString["CPDeviceID"]);
                    LTRL_JobID.Text = Convert.ToString(Request.QueryString["JobID"]);
                    LTRL_DongleID.Text = Convert.ToString(Request.QueryString["DongleID"]);
                    LTRL_SheetNumber.Text = Convert.ToString(Request.QueryString["SheetNumber"]);
                    #endregion
                    //========== Assign Variables ==========//
                    LTRL_KeyTrend.Text = LTRL_JobID.Text + LTRL_DongleID.Text + LTRL_SheetNumber.Text + "-Press-Trend-Sheet";
                    LTRL_KeyJobSheetSummary.Text = LTRL_JobID.Text + LTRL_DongleID.Text + LTRL_SheetNumber.Text + "-Press-DT-JobSheetSummary-Sheet";

                    //========== Clear Sessions ==========//                
                    Sessions.SetSessions(LTRL_KeyTrend.Text, null);
                    Sessions.SetSessions(LTRL_KeyJobSheetSummary.Text, null);

                    //========== Fills Chart ==========//
                    FillCharts();
                }
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        protected void BTN_TrendDeltaE_Click(object sender, EventArgs e)
        {
            try { FillCharts(); }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        protected void BTN_DeltaE_Click(object sender, EventArgs e)
        {
            try { FillCharts(); }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        protected void BTN_Density_Click(object sender, EventArgs e)
        {
            try { FillCharts(); }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        protected void BTN_Dotgain_Click(object sender, EventArgs e)
        {
            try { FillCharts(); }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        protected void BTN_PrintContrast_Click(object sender, EventArgs e)
        {
            try { FillCharts(); }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        #endregion

        #region Methods and Functions
        private void FillCharts()
        {
            try
            {
                if (Sessions.GetSession(LTRL_KeyJobSheetSummary.Text) == null)
                {
                    //========== Declaring Objects ==========//
                    PressJobsData objPressJobs = new PressJobsData();

                    //========== Get The Data Table ==========//
                    objPressJobs.JobID = Convert.ToInt32(LTRL_JobID.Text);
                    objPressJobs.DongleID = LTRL_DongleID.Text;
                    Sessions.SetSessions(LTRL_KeyJobSheetSummary.Text, objPressJobs.uspGetDeltaESummaryForSheet(Convert.ToInt32(LTRL_SheetNumber.Text)));
                }
                DataTable DT = (DataTable)Sessions.GetSession(LTRL_KeyJobSheetSummary.Text);

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

                #region Trend Delta E
                int j = 0;
                for (int i = _StartIndex; i < _EndIndex; i = i + 1)
                {
                    if (CHK_TrendDeltaE_AvgDe.Checked == true) { AddPointChart(j, CHRT_TrendDeltaE.Series["Series1"], "Zone Number", DT.Rows[i]["ZoneNumber"].ToString(), "Average Delta E", DT.Rows[i]["AvgDeltaE"].ToString()); } else { CHRT_TrendDeltaE.Series["Series1"].Enabled = false; }
                    if (CHK_TrendDeltaE_MaxDe.Checked == true) { AddPointChart(j, CHRT_TrendDeltaE.Series["Series2"], "Zone Number", DT.Rows[i]["ZoneNumber"].ToString(), "Maximum Delta E", DT.Rows[i]["MaxDeltaE"].ToString()); } else { CHRT_TrendDeltaE.Series["Series2"].Enabled = false; }
                    if (CHK_TrendDeltaE_StdDE.Checked == true) { AddPointChart(j, CHRT_TrendDeltaE.Series["Series3"], "Zone Number", DT.Rows[i]["ZoneNumber"].ToString(), "Standard Deviation", DT.Rows[i]["StdDevdeltaE"].ToString()); } else { CHRT_TrendDeltaE.Series["Series3"].Enabled = false; }

                    j = j + 1;
                }

                //========== Ads a blank row ==========//
                if (DT.Rows.Count == 0) { AddBlankRow(CHRT_TrendDeltaE); }
                #endregion

                #region Fill All Charts
                FillOtherCharts(_StartIndex, _EndIndex);
                //FillDotGainAndPrinctContrast();
                FillDotGainTrendLine();
                FillPrintContrastTrendLine();
                #endregion
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
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
        private void AddPointChart(int _Index, Series _objSeries, string _XTitle, string _XValue, string _YTitle, string _YValue)
        {
            try
            {
                _objSeries.Type = SeriesChartType.Spline;
                //if (Convert.ToDecimal(_YValue) > 30) { _objSeries.Points.AddXY(_XValue, "30"); } else { _objSeries.Points.AddXY(_XValue, _YValue); }
                _objSeries.Points.AddXY(_XValue, _YValue);

                _objSeries.Points[_Index].ToolTip = _XTitle + " : " + _XValue + ", " + _YTitle + " : " + _YValue;
                _objSeries.Points[_Index].Href = "#";

                _objSeries.Points[_Index].MarkerSize = 4;
                _objSeries.Points[_Index].MarkerColor = Color.FromArgb(200, 25, 25, 25);
                _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void AddPointChart(int _Index, Series _objSeries, string _XTitle, string _XValue, string _YTitle, string _YValue, string _ColorName)
        {
            try
            {
                _objSeries.Type = SeriesChartType.Spline;
                //if (Convert.ToDecimal(_YValue) > 30) { _objSeries.Points.AddXY(_XValue, "30"); } else { _objSeries.Points.AddXY(_XValue, _YValue); }
                _objSeries.Points.AddXY(_XValue, _YValue);

                _objSeries.Points[_Index].ToolTip = " Color Name : " + _ColorName + ", " + _XTitle + " : " + _XValue + ", " + _YTitle + " : " + _YValue;
                _objSeries.Points[_Index].Href = "#";

                _objSeries.Points[_Index].MarkerSize = 4;
                _objSeries.Points[_Index].MarkerColor = Color.FromArgb(200, 25, 25, 25);
                _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void FillOtherCharts(int _StartIndex, int _EndIndex)
        {
            try
            {
                if (Sessions.GetSession(LTRL_KeyTrend.Text) == null)
                {
                    //========== Declaring Objects ==========//
                    PressPatchCalculationsData objPressPatchCalculations = new PressPatchCalculationsData();

                    //========== Get The Data Table ==========//
                    objPressPatchCalculations.JobID = Convert.ToInt32(LTRL_JobID.Text);
                    objPressPatchCalculations.DongleID = LTRL_DongleID.Text;
                    Sessions.SetSessions(LTRL_KeyTrend.Text, objPressPatchCalculations.uspGetSheetTrendView(Convert.ToInt32(LTRL_SheetNumber.Text)));
                }
                DataSet DSet = (DataSet)Sessions.GetSession(LTRL_KeyTrend.Text);

                //========== GET All For Color = Cyan ==========//
                DSet.Tables[0].DefaultView.RowFilter = "PrimaryColorType = 1"; //Cyan
                DataTable DT_Cyan = DSet.Tables[0].DefaultView.ToTable("Cyan");

                //========== GET All For Color = Magenta ==========//
                DSet.Tables[0].DefaultView.RowFilter = "PrimaryColorType = 2"; //Magenta
                DataTable DT_Magenta = DSet.Tables[0].DefaultView.ToTable("Magenta");

                //========== GET All For Color = Yellow ==========//
                DSet.Tables[0].DefaultView.RowFilter = "PrimaryColorType = 3"; //Yellow
                DataTable DT_Yellow = DSet.Tables[0].DefaultView.ToTable("Yellow");

                //========== GET All For Color = Black ==========//
                DSet.Tables[0].DefaultView.RowFilter = "PrimaryColorType = 4";//Black
                DataTable DT_Black = DSet.Tables[0].DefaultView.ToTable("Black");

                int j = 0;
                for (int i = _StartIndex; i < _EndIndex; i = i + 1)
                {
                    #region Cyan
                    if (i < DT_Cyan.Rows.Count)
                    {
                        if (CHK_DeltaE_Cyan.Checked == true) { AddPointChart(j, CHRT_DeltaE.Series["Series1"], "Zone Number", DT_Cyan.Rows[i]["ZoneNumber"].ToString(), "Avg. Delta E", DT_Cyan.Rows[i]["AvgDeltaE"].ToString(), DT_Cyan.Rows[i]["ColorName"].ToString()); } else { CHRT_DeltaE.Series["Series1"].Enabled = false; }
                        if (CHK_Density_Cyan.Checked == true) { AddPointChart(j, CHRT_Density.Series["Series1"], "Zone Number", DT_Cyan.Rows[i]["ZoneNumber"].ToString(), "Avg. Density", DT_Cyan.Rows[i]["AvgDensity"].ToString(), DT_Cyan.Rows[i]["ColorName"].ToString()); } else { CHRT_Density.Series["Series1"].Enabled = false; }
                    }
                    #endregion

                    #region Magenta
                    if (i < DT_Magenta.Rows.Count)
                    {
                        if (CHK_DeltaE_Magenta.Checked == true) { AddPointChart(j, CHRT_DeltaE.Series["Series2"], "Zone Number", DT_Magenta.Rows[i]["ZoneNumber"].ToString(), "Avg. Delta E", DT_Magenta.Rows[i]["AvgDeltaE"].ToString(), DT_Magenta.Rows[i]["ColorName"].ToString()); } else { CHRT_DeltaE.Series["Series2"].Enabled = false; }
                        if (CHK_Density_Magenta.Checked == true) { AddPointChart(j, CHRT_Density.Series["Series2"], "Zone Number", DT_Magenta.Rows[i]["ZoneNumber"].ToString(), "Avg. Density", DT_Magenta.Rows[i]["AvgDensity"].ToString(), DT_Magenta.Rows[i]["ColorName"].ToString()); } else { CHRT_Density.Series["Series2"].Enabled = false; }
                    }
                    #endregion

                    #region Yellow
                    if (i < DT_Yellow.Rows.Count)
                    {
                        if (CHK_DeltaE_Yellow.Checked == true) { AddPointChart(j, CHRT_DeltaE.Series["Series3"], "Zone Number", DT_Yellow.Rows[i]["ZoneNumber"].ToString(), "Avg. Delta E", DT_Yellow.Rows[i]["AvgDeltaE"].ToString(), DT_Yellow.Rows[i]["ColorName"].ToString()); } else { CHRT_DeltaE.Series["Series3"].Enabled = false; }
                        if (CHK_Density_Yellow.Checked == true) { AddPointChart(j, CHRT_Density.Series["Series3"], "Zone Number", DT_Yellow.Rows[i]["ZoneNumber"].ToString(), "Avg. Density", DT_Yellow.Rows[i]["AvgDensity"].ToString(), DT_Yellow.Rows[i]["ColorName"].ToString()); } else { CHRT_Density.Series["Series3"].Enabled = false; }
                    }
                    #endregion

                    #region Black
                    if (i < DT_Black.Rows.Count)
                    {
                        if (CHK_DeltaE_Black.Checked == true) { AddPointChart(j, CHRT_DeltaE.Series["Series4"], "Zone Number", DT_Black.Rows[i]["ZoneNumber"].ToString(), "Avg. Delta E", DT_Black.Rows[i]["AvgDeltaE"].ToString(), DT_Black.Rows[i]["ColorName"].ToString()); } else { CHRT_DeltaE.Series["Series4"].Enabled = false; }
                        if (CHK_Density_Black.Checked == true) { AddPointChart(j, CHRT_Density.Series["Series4"], "Zone Number", DT_Black.Rows[i]["ZoneNumber"].ToString(), "Avg. Density", DT_Black.Rows[i]["AvgDensity"].ToString(), DT_Black.Rows[i]["ColorName"].ToString()); } else { CHRT_Density.Series["Series4"].Enabled = false; }
                    }
                    #endregion

                    j = j + 1;
                }

                //========== Ads a blank row ==========//
                if (DSet.Tables[0].Rows.Count == 0) { AddBlankRow(CHRT_DeltaE); AddBlankRow(CHRT_Density); }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        //private void FillDotGainAndPrinctContrast()
        //{     
        //    DataSet DSet = (DataSet)Sessions.GetSession(LTRL_KeyTrend.Text);

        //    //========== GET All For Color = Cyan ==========//
        //    DSet.Tables[1].DefaultView.RowFilter = "PrimaryColorType = 1"; //Cyan
        //    DataTable DT_Cyan = DSet.Tables[1].DefaultView.ToTable("Cyan");

        //    //========== GET All For Color = Magenta ==========//
        //    DSet.Tables[1].DefaultView.RowFilter = "PrimaryColorType = 2"; //Magenta
        //    DataTable DT_Magenta = DSet.Tables[1].DefaultView.ToTable("Magenta");

        //    //========== GET All For Color = Yellow ==========//
        //    DSet.Tables[1].DefaultView.RowFilter = "PrimaryColorType = 3"; //Yellow
        //    DataTable DT_Yellow = DSet.Tables[1].DefaultView.ToTable("Yellow");

        //    //========== GET All For Color = Black ==========//
        //    DSet.Tables[1].DefaultView.RowFilter = "PrimaryColorType = 4";//Black
        //    DataTable DT_Black = DSet.Tables[1].DefaultView.ToTable("Black");

        //    int j = 0;
        //    for (int i = 0; i < DSet.Tables[1].Rows.Count; i = i + 1)
        //    {
        //        #region Cyan
        //        if (i < DT_Cyan.Rows.Count)
        //        {
        //            if (CHK_Dotgain_Cyan.Checked == true) { AddPointChart(j, CHRT_Dotgain.Series["Series1"], "Scanned Date", DT_Cyan.Rows[i]["ScannedDateTime"].ToString(), "Avg. Dotgain", DT_Cyan.Rows[i]["AvgDotgain"].ToString(), DT_Cyan.Rows[i]["ColorName"].ToString()); } else { CHRT_Dotgain.Series["Series1"].Enabled = false; }
        //            if (CHK_PrintContrast_Cyan.Checked == true) { AddPointChart(j, CHRT_PrintContrast.Series["Series1"], "Scanned Date", DT_Cyan.Rows[i]["ScannedDateTime"].ToString(), "Avg. Print Contrast", DT_Cyan.Rows[i]["AvgPrintContrast"].ToString(), DT_Cyan.Rows[i]["ColorName"].ToString()); } else { CHRT_PrintContrast.Series["Series1"].Enabled = false; }
        //        }
        //        #endregion

        //        #region Magenta
        //        if (i < DT_Magenta.Rows.Count)
        //        {
        //            if (CHK_Dotgain_Magenta.Checked == true) { AddPointChart(j, CHRT_Dotgain.Series["Series2"], "Scanned Date", DT_Magenta.Rows[i]["ScannedDateTime"].ToString(), "Avg. Dotgain", DT_Magenta.Rows[i]["AvgDotgain"].ToString(), DT_Magenta.Rows[i]["ColorName"].ToString()); } else { CHRT_Dotgain.Series["Series2"].Enabled = false; }
        //            if (CHK_PrintContrast_Magenta.Checked == true) { AddPointChart(j, CHRT_PrintContrast.Series["Series2"], "Scanned Date", DT_Magenta.Rows[i]["ScannedDateTime"].ToString(), "Avg. Print Contrast", DT_Magenta.Rows[i]["AvgPrintContrast"].ToString(), DT_Magenta.Rows[i]["ColorName"].ToString()); } else { CHRT_PrintContrast.Series["Series2"].Enabled = false; }
        //        }
        //        #endregion

        //        #region Yellow
        //        if (i < DT_Yellow.Rows.Count)
        //        {
        //            if (CHK_Dotgain_Yellow.Checked == true) { AddPointChart(j, CHRT_Dotgain.Series["Series3"], "Scanned Date", DT_Yellow.Rows[i]["ScannedDateTime"].ToString(), "Avg. Dotgain", DT_Yellow.Rows[i]["AvgDotgain"].ToString(), DT_Yellow.Rows[i]["ColorName"].ToString()); } else { CHRT_Dotgain.Series["Series3"].Enabled = false; }
        //            if (CHK_PrintContrast_Yellow.Checked == true) { AddPointChart(j, CHRT_PrintContrast.Series["Series3"], "Scanned Date", DT_Yellow.Rows[i]["ScannedDateTime"].ToString(), "Avg. Print Contrast", DT_Yellow.Rows[i]["AvgPrintContrast"].ToString(), DT_Yellow.Rows[i]["ColorName"].ToString()); } else { CHRT_PrintContrast.Series["Series3"].Enabled = false; }
        //        }
        //        #endregion

        //        #region Black
        //        if (i < DT_Black.Rows.Count)
        //        {
        //            if (CHK_Dotgain_Black.Checked == true) { AddPointChart(j, CHRT_Dotgain.Series["Series4"], "Scanned Date", DT_Black.Rows[i]["ScannedDateTime"].ToString(), "Avg. Dotgain", DT_Black.Rows[i]["AvgDotgain"].ToString(), DT_Black.Rows[i]["ColorName"].ToString()); } else { CHRT_Dotgain.Series["Series4"].Enabled = false; }
        //            if (CHK_PrintContrast_Black.Checked == true) { AddPointChart(j, CHRT_PrintContrast.Series["Series4"], "Scanned Date", DT_Black.Rows[i]["ScannedDateTime"].ToString(), "Avg. Print Contrast", DT_Black.Rows[i]["AvgPrintContrast"].ToString(), DT_Black.Rows[i]["ColorName"].ToString()); } else { CHRT_PrintContrast.Series["Series4"].Enabled = false; }
        //        }
        //        #endregion

        //        j = j + 1;
        //    }

        //    if (DSet.Tables[1].Rows.Count == 0) { AddBlankRow(CHRT_Dotgain); AddBlankRow(CHRT_PrintContrast); }
        //}
        private void FillDotGainTrendLine()
        {
            try
            {
                DataSet DSet = (DataSet)Sessions.GetSession(LTRL_KeyTrend.Text);

                //========== GET All For Color = Cyan ==========//
                DSet.Tables[1].DefaultView.RowFilter = "PrimaryColorType = 1"; //Cyan
                DataTable DT_Cyan = DSet.Tables[1].DefaultView.ToTable("Cyan");

                //========== GET All For Color = Magenta ==========//
                DSet.Tables[1].DefaultView.RowFilter = "PrimaryColorType= 2"; //Magenta
                DataTable DT_Magenta = DSet.Tables[1].DefaultView.ToTable("Magenta");

                //========== GET All For Color = Yellow ==========//
                DSet.Tables[1].DefaultView.RowFilter = "PrimaryColorType = 3"; //Yellow
                DataTable DT_Yellow = DSet.Tables[1].DefaultView.ToTable("Yellow");

                //========== GET All For Color = Black ==========//
                DSet.Tables[1].DefaultView.RowFilter = "PrimaryColorType = 0";//Black
                DataTable DT_Black = DSet.Tables[1].DefaultView.ToTable("Black");

                int j = 0;
                for (int i = 0; i < DSet.Tables[1].Rows.Count; i = i + 1)
                {
                    #region Cyan
                    if (i < DT_Cyan.Rows.Count)
                    {
                        if (CHK_Dotgain_Cyan.Checked == true) { AddPointChart(j, CHRT_Dotgain.Series["Series1"], "Zone Number", DT_Cyan.Rows[i]["ZoneNumber"].ToString(), "Avg. Dotgain", DT_Cyan.Rows[i]["AvgDotgain"].ToString(), DT_Cyan.Rows[i]["ColorName"].ToString()); } else { CHRT_Dotgain.Series["Series1"].Enabled = false; }

                    }
                    #endregion

                    #region Magenta
                    if (i < DT_Magenta.Rows.Count)
                    {
                        if (CHK_Dotgain_Magenta.Checked == true) { AddPointChart(j, CHRT_Dotgain.Series["Series2"], "Zone Number", DT_Magenta.Rows[i]["ZoneNumber"].ToString(), "Avg. Dotgain", DT_Magenta.Rows[i]["AvgDotgain"].ToString(), DT_Magenta.Rows[i]["ColorName"].ToString()); } else { CHRT_Dotgain.Series["Series2"].Enabled = false; }

                    }
                    #endregion

                    #region Yellow
                    if (i < DT_Yellow.Rows.Count)
                    {
                        if (CHK_Dotgain_Yellow.Checked == true) { AddPointChart(j, CHRT_Dotgain.Series["Series3"], "Zone Number", DT_Yellow.Rows[i]["ZoneNumber"].ToString(), "Avg. Dotgain", DT_Yellow.Rows[i]["AvgDotgain"].ToString(), DT_Yellow.Rows[i]["ColorName"].ToString()); } else { CHRT_Dotgain.Series["Series3"].Enabled = false; }
                    }
                    #endregion

                    #region Black
                    if (i < DT_Black.Rows.Count)
                    {
                        if (CHK_Dotgain_Black.Checked == true) { AddPointChart(j, CHRT_Dotgain.Series["Series4"], "Zone Number", DT_Black.Rows[i]["ZoneNumber"].ToString(), "Avg. Dotgain", DT_Black.Rows[i]["AvgDotgain"].ToString(), DT_Black.Rows[i]["ColorName"].ToString()); } else { CHRT_Dotgain.Series["Series4"].Enabled = false; }

                    }
                    #endregion

                    j = j + 1;
                }

                if (DSet.Tables[1].Rows.Count == 0) { AddBlankRow(CHRT_Dotgain); }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void FillPrintContrastTrendLine()
        {
            try
            {
                DataSet DSet = (DataSet)Sessions.GetSession(LTRL_KeyTrend.Text);

                //========== GET All For Color = Cyan ==========//
                DSet.Tables[2].DefaultView.RowFilter = "PrimaryColorType= 1"; //Cyan
                DataTable DT_Cyan = DSet.Tables[2].DefaultView.ToTable("Cyan");

                //========== GET All For Color = Magenta ==========//
                DSet.Tables[2].DefaultView.RowFilter = "PrimaryColorType = 2"; //Magenta
                DataTable DT_Magenta = DSet.Tables[2].DefaultView.ToTable("Magenta");

                //========== GET All For Color = Yellow ==========//
                DSet.Tables[2].DefaultView.RowFilter = "PrimaryColorType = 3"; //Yellow
                DataTable DT_Yellow = DSet.Tables[2].DefaultView.ToTable("Yellow");

                //========== GET All For Color = Black ==========//
                DSet.Tables[2].DefaultView.RowFilter = "PrimaryColorType = 0";//Black
                DataTable DT_Black = DSet.Tables[2].DefaultView.ToTable("Black");

                int j = 0;
                for (int i = 0; i < DSet.Tables[2].Rows.Count; i = i + 1)
                {
                    #region Cyan
                    if (i < DT_Cyan.Rows.Count)
                    {
                        if (CHK_PrintContrast_Cyan.Checked == true) { AddPointChart(j, CHRT_PrintContrast.Series["Series1"], "Zone Number", DT_Cyan.Rows[i]["ZoneNumber"].ToString(), "Avg. Print Contrast", DT_Cyan.Rows[i]["AvgPrintContrast"].ToString(), DT_Cyan.Rows[i]["ColorName"].ToString()); } else { CHRT_PrintContrast.Series["Series1"].Enabled = false; }
                    }
                    #endregion

                    #region Magenta
                    if (i < DT_Magenta.Rows.Count)
                    {
                        if (CHK_PrintContrast_Magenta.Checked == true) { AddPointChart(j, CHRT_PrintContrast.Series["Series2"], "Zone Number", DT_Magenta.Rows[i]["ZoneNumber"].ToString(), "Avg. Print Contrast", DT_Magenta.Rows[i]["AvgPrintContrast"].ToString(), DT_Magenta.Rows[i]["ColorName"].ToString()); } else { CHRT_PrintContrast.Series["Series2"].Enabled = false; }
                    }
                    #endregion

                    #region Yellow
                    if (i < DT_Yellow.Rows.Count)
                    {
                        if (CHK_PrintContrast_Yellow.Checked == true) { AddPointChart(j, CHRT_PrintContrast.Series["Series3"], "Zone Number", DT_Yellow.Rows[i]["ZoneNumber"].ToString(), "Avg. Print Contrast", DT_Yellow.Rows[i]["AvgPrintContrast"].ToString(), DT_Yellow.Rows[i]["ColorName"].ToString()); } else { CHRT_PrintContrast.Series["Series3"].Enabled = false; }
                    }
                    #endregion

                    #region Black
                    if (i < DT_Black.Rows.Count)
                    {
                        if (CHK_PrintContrast_Black.Checked == true) { AddPointChart(j, CHRT_PrintContrast.Series["Series4"], "Zone Number", DT_Black.Rows[i]["ZoneNumber"].ToString(), "Avg. Print Contrast", DT_Black.Rows[i]["AvgPrintContrast"].ToString(), DT_Black.Rows[i]["ColorName"].ToString()); } else { CHRT_PrintContrast.Series["Series4"].Enabled = false; }
                    }
                    #endregion

                    j = j + 1;
                }

                if (DSet.Tables[2].Rows.Count == 0) { AddBlankRow(CHRT_PrintContrast); }
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
                DataTable DT = (DataTable)Sessions.GetSession(LTRL_KeyJobSheetSummary.Text);
                int _PageSize = Convert.ToInt32(LTRL_PageSize.Text);
                int _PageCount = DT.Rows.Count / _PageSize; if ((DT.Rows.Count % _PageSize) > 0) { _PageCount = _PageCount + 1; }

                //========== Filling the DropDown ==========//
                DDL_GoToPage.Items.Clear();
                FillDDLGoToPage(_PageCount);

                FillCharts();
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
                FillCharts();
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
                FillCharts();
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
                FillCharts();
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
                FillCharts();
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
                FillCharts();
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
    }
}