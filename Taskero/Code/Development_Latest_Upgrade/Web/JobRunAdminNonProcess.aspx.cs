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
using Fuji.Business;
using Dundas.Charting.WebControl;
using System.Drawing;
#endregion

public partial class JobRunAdminNonProcess : PageBase
{
    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            //========== Clear Error ==========//
            LblError.Text = "";

            #region Allow CHRT_TrendDeltaE scrolling
            //CHRT_TrendDeltaE.Series["Series1"].Type = SeriesChartType.Spline;
            //CHRT_TrendDeltaE.Series["Series2"].Type = SeriesChartType.Spline;
            //CHRT_TrendDeltaE.Series["Series3"].Type = SeriesChartType.Spline;

            //// Allow scrolling.
            //CHRT_TrendDeltaE.AJAXZoomEnabled = true;

            //// scrolling and zooming will force keeping of series data between callbacks.
            //CHRT_TrendDeltaE.ChartAreas["ChartArea1"].CursorX.UserEnabled = true;

            //// Set initial X axis zooming                        
            //CHRT_TrendDeltaE.ChartAreas["ChartArea1"].AxisY.View.MinSize = 10.0;
            //CHRT_TrendDeltaE.ChartAreas["ChartArea1"].AxisX.View.Position = 0.0;
            //CHRT_TrendDeltaE.ChartAreas["ChartArea1"].AxisX.View.Size = 50.0;
            #endregion

            //========== Fill Form ==========//

            //========== Assign Variables ==========//
            LTRL_CPDeviceID.Text = Request.QueryString["CPDeviceID"].ToString();
            LTRL_JobID.Text = Request.QueryString["JobID"].ToString();
            LTRL_DongleID.Text = Request.QueryString["DongleID"].ToString();

            //========== Clear Sessions ==========//

            LTRL_KeyJobSheetSummary.Text = LTRL_JobID.Text + LTRL_DongleID.Text + "-Press-DT-JobSheetSummary";
            LTRL_KeyJobZoneSummary.Text = LTRL_JobID.Text + LTRL_DongleID.Text + "-Press-DT-JobZoneSummary";


            Fuji.Common.Sessions.SetSessions(LTRL_KeyJobSheetSummary.Text, null);
            Fuji.Common.Sessions.SetSessions(LTRL_KeyJobZoneSummary.Text, null);

            //========== Fills Chart ==========//
            if (IsPostBack == false)
            {
                RefillCharts();
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    protected void BTN_TrendDeltaE_Click(object sender, EventArgs e)
    { RefillCharts(); }
    #endregion

    #region Methods and Events
    private void RefillCharts()
    {
        try
        {
            //---------- Fills Job Sheet Summary -----------//
            FillJobSheetSummary();

            //---------- Fills Job Zone Summary -----------//
            FillZoneSummary();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    private void FillJobSheetSummary()
    {
        try
        {
            if (Fuji.Common.Sessions.GetSession(LTRL_KeyJobSheetSummary.Text) == null)
            {
                //========== Declaring Objects ==========//
                PressJobsData objPressJobs = new PressJobsData();

                //========== Get The Data Table ==========//
                objPressJobs.JobID = Convert.ToInt32(LTRL_JobID.Text);
                objPressJobs.DongleID = LTRL_DongleID.Text;
                Fuji.Common.Sessions.SetSessions(LTRL_KeyJobSheetSummary.Text, objPressJobs.uspGetPressJobRunAdminSheetSummary());
            }
            DataTable DT = (DataTable)Fuji.Common.Sessions.GetSession(LTRL_KeyJobSheetSummary.Text);

            if (DT.Rows.Count < 35)
                CHRT_TrendDeltaE.Width = 900;
            else
                CHRT_TrendDeltaE.Width = (DT.Rows.Count * 25);


            //========== Bind the Chart ==========//        
            for (int i = 0; i < DT.Rows.Count; i = i + 1)
            {
                if (CHK_TrendDeltaE_AvgDe.Checked == true)
                {
                    AddPointToTrendDeltaE(i, CHRT_TrendDeltaE.Series["Series1"], "Sheet Number", DT.Rows[i]["SheetNumber"].ToString(), "Average Delta E", DT.Rows[i]["AvgDeltaE"].ToString(), Convert.ToBoolean(DT.Rows[i]["IsUsed"].ToString()));
                }
                else
                {
                    CHRT_TrendDeltaE.Series["Series1"].Enabled = false;
                }
                if (CHK_TrendDeltaE_MaxDe.Checked == true) { AddPointToTrendDeltaE(i, CHRT_TrendDeltaE.Series["Series2"], "Sheet Number", DT.Rows[i]["SheetNumber"].ToString(), "Maximum Delta E", DT.Rows[i]["MaxDeltaE"].ToString(), Convert.ToBoolean(DT.Rows[i]["IsUsed"].ToString())); } else { CHRT_TrendDeltaE.Series["Series2"].Enabled = false; }
                if (CHK_TrendDeltaE_StdDE.Checked == true) { AddPointToTrendDeltaE(i, CHRT_TrendDeltaE.Series["Series3"], "Sheet Number", DT.Rows[i]["SheetNumber"].ToString(), "Standard Deviation", DT.Rows[i]["StdDevdeltaE"].ToString(), Convert.ToBoolean(DT.Rows[i]["IsUsed"].ToString())); } else { CHRT_TrendDeltaE.Series["Series3"].Enabled = false; }

                CustomLabel c = new CustomLabel(i + 1, i + 1 + 0.001, "", 1, LabelMark.None);
                if (Convert.ToBoolean(DT.Rows[i]["IsUsed"].ToString()))
                    c.Image = "Images/checkbox_2.gif";
                else
                    c.Image = "Images/checkbox_1.gif";

                c.ImageMapAreaAttributes = "onclick=\"SetValueTrendDeltaSummary('" + DT.Rows[i]["SheetNumber"].ToString() + "');\"";
                CHRT_TrendDeltaE.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(c);
            }
            //========== Ads a blank row ==========//
            if (DT.Rows.Count == 0) { AddBlankRow(CHRT_TrendDeltaE); }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }

    }
    private void FillZoneSummary()
    {
        try
        {
            if (Fuji.Common.Sessions.GetSession(LTRL_KeyJobZoneSummary.Text) == null)
            {
                //========== Declaring Objects ==========//
                PressJobsData objPressJobs = new PressJobsData();

                //========== Get The Data Table ==========//
                objPressJobs.JobID = Convert.ToInt32(LTRL_JobID.Text);
                objPressJobs.DongleID = LTRL_DongleID.Text;
                Fuji.Common.Sessions.SetSessions(LTRL_KeyJobZoneSummary.Text, objPressJobs.uspGetPressJobRunAdminZoneSummary());
            }
            DataTable DT = (DataTable)Fuji.Common.Sessions.GetSession(LTRL_KeyJobZoneSummary.Text);
            //========== Bind the Chart ==========//        

            if (DT.Rows.Count < 23)
                CHRT_ZoneDeltaE.Width = 900;
            else
                CHRT_ZoneDeltaE.Width = DT.Rows.Count * 40;

            for (int i = 0; i < DT.Rows.Count; i = i + 1)
            {
                AddPointToZoneDeltaE(i, CHRT_ZoneDeltaE.Series["Series1"], "Zone Number", DT.Rows[i]["ZoneNumber"].ToString(), "Average Delta E", DT.Rows[i]["AvgDeltaE"].ToString(), DT.Rows[i]["ZoneColor"].ToString());

                CustomLabel c = new CustomLabel(i + 1, i + 1 + 0.001, "", 1, LabelMark.None);
                if (Convert.ToBoolean(DT.Rows[i]["IsUsed"].ToString()))
                    c.Image = "Images/checkbox_2.gif";
                else
                {
                    c.Image = "Images/checkbox_1.gif";
                    CHRT_ZoneDeltaE.Series["Series1"].Points[i].Color = Color.Gray;
                }
                c.ImageMapAreaAttributes = "onclick=\"SetValueZoneSummary('" + DT.Rows[i]["ZoneNumber"].ToString() + "');\"";
                CHRT_ZoneDeltaE.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(c);
            }
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
    private void AddPointToTrendDeltaE(int _Index, Series _objSeries, string _XTitle, string _XValue, string _YTitle, string _YValue, Boolean IsUsed)
    {
        try
        {
            _objSeries.Type = SeriesChartType.Spline;
            _objSeries.Points.AddXY(_XValue, _YValue);

            // _objSeries.Points[_Index].MapAreaAttributes = "onclick = \"OpenMeasurementWindow( 'PressSheetTrend.aspx?JobID=" + LTRL_JobID.Text + "&DongleID=" + LTRL_DongleID.Text + "&CPDeviceID=" + LTRL_CPDeviceID.Text + "&SheetNumber=" + _XValue + "' , '" + _XValue + "' );\"";
            _objSeries.Points[_Index].ToolTip = _XTitle + " : " + _XValue + ", " + _YTitle + " : " + _YValue;
            //_objSeries.Points[_Index].Href = "#";
            _objSeries.Points[_Index].MarkerColor = Color.FromArgb(200, 25, 25, 25);
            if (IsUsed)
            {
                _objSeries.Points[_Index].MarkerSize = 4;
                _objSeries.Points[_Index].MarkerColor = Color.FromArgb(200, 25, 25, 25);
                _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
            }
            else
            {
                _objSeries.Points[_Index].MarkerSize = 15;
                _objSeries.Points[_Index].MarkerColor = Color.Gray;
                _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Triangle;
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void AddPointToZoneDeltaE(int _Index, Series _objSeries, string _XTitle, string _XValue, string _YTitle, string _YValue, string ZoneColor)
    {
        try
        {
            //if (Convert.ToDecimal(_YValue) > 20) { _objSeries.Points.AddXY(_XValue, "20"); } else { _objSeries.Points.AddXY(_XValue, _YValue); }
            _objSeries.Points.AddXY(_XValue, _YValue);
            _objSeries.Points[_Index].ToolTip = _XTitle + " : " + _XValue + ", " + _YTitle + " : " + _YValue;

            //'**********Changed By Chirag on 1/23/2008
            string ColorName = null;
            switch (ZoneColor)
            {
                case "1": //Red
                    ColorName = "Red";
                    break;
                case "2": //Yellow
                    ColorName = "Yellow";
                    break;
                case "3": //Green
                    ColorName = "Green";
                    break;
            }

            _objSeries.Points[_Index].Color = Color.FromName(ColorName);
            _objSeries.Points[_Index].Href = "#";
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    protected void HDButZoneSummary_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Declaring Objects ==========//
            PressJobsData objPressJobs = new PressJobsData();

            //========== Updateing IsUsed Value ==========//
            objPressJobs.JobID = Convert.ToInt32(LTRL_JobID.Text);
            objPressJobs.DongleID = LTRL_DongleID.Text;
            objPressJobs.uspUpdatePressJobRunAdminIsUsed(Convert.ToInt16(HDZoneSummary.Value.Trim()));
            RefillCharts();
        }
        catch (Exception Ex)
        {
           throw Ex;
        } 
    }
    protected void HDButTrendDeltaSummary_Click(object sender, EventArgs e)
    {
        try
        {
            //========== Declaring Objects ==========//
            PressJobsData objPressJobs = new PressJobsData();

            //========== Updateing IsUsed Value ==========//
            objPressJobs.JobID = Convert.ToInt32(LTRL_JobID.Text);
            objPressJobs.DongleID = LTRL_DongleID.Text;
            objPressJobs.uspUpdatePressSheetesRunAdminIsUsed(Convert.ToInt16(HDTrendDeltaSummary.Value.Trim()));

            RefillCharts();
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion
}
