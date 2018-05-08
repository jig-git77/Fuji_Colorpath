using System;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;


namespace FujifilmUSA.Sync.Web.UserControl.PopupControl
{
    public partial class ReportCMYKDensity : BaseControl, IViewReportCMYKDensity
    {
        #region Variable Declarations
        private PresenterReportCMYKDensity _presenterReportCMYKDensity;
        public decimal DtELimit = 0;

        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterReportCMYKDensity = new PresenterReportCMYKDensity(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            FillDensity_Charts();
        }

        #endregion

        private void FillDensity_Charts()
        {
            //if (txt_OptimizeRunID.Text != null)
            //{
            //    OptimizeRunId = Convert.ToInt32(txt_OptimizeRunID.Text);

            //    DSDensity_C = new DataTable();
            //    DSDensity_M = new DataTable();
            //    DSDensity_Y = new DataTable();
            //    DSDensity_K = new DataTable();

            //    _presenterReportCMYKDensity.GetDensityData_C();
            //    if (!IsErrorOccured)
            //    {
            //        FillDensity_C_Chart();
            //        FillDensity_M_Chart();
            //        FillDensity_Y_Chart();
            //        FillDensity_K_Chart();
            //    }
            //}
        }

        //private void FillDensity_C_Chart()
        //{
        //    decimal minΔELevel = decimal.MaxValue;
        //    decimal maxΔELevel = decimal.MinValue;

        //    foreach (DataRow dr in DSDensity_C.Rows)
        //    {
        //        decimal accountLevel = dr.Field<decimal>("ΔE");
        //        minΔELevel = Math.Min(minΔELevel, accountLevel);
        //        maxΔELevel = Math.Max(maxΔELevel, accountLevel);
        //    }

        //    if (DSDensity_C != null && DSDensity_C.Rows.Count > 0)
        //    {
        //        #region Fill CMYKDensity_Cyan Chart
        //        Chart_CMYKDensity_C.Series["Series1"].Points.Clear();
        //        Chart_CMYKDensity_C.Series["Series1"]["TooltipOffset"] = "True";
        //        Chart_CMYKDensity_C.Series["Series1Point"].Points.Clear();
        //        Chart_CMYKDensity_C.Series["Series2"].Points.Clear();
        //        Chart_CMYKDensity_C.Series["Series2"]["TooltipOffset"] = "True";
        //        Chart_CMYKDensity_C.Series["Series2Point"].Points.Clear();
        //        Chart_CMYKDensity_C.Series["Series3"].Points.Clear();
        //        Chart_CMYKDensity_C.Series["Series3Point"].Points.Clear();


        //        if (DSDensity_C != null && DSDensity_C.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < DSDensity_C.Rows.Count; i++)
        //            {
        //                AddPointInChart(i, Chart_CMYKDensity_C.Series["Series1"], Chart_CMYKDensity_C.Series["Series1Point"], Convert.ToDecimal(DSDensity_C.Rows[i]["Density"]), Convert.ToDecimal(DSDensity_C.Rows[i]["ΔE"]), Color.Blue.Name.ToString(), true, "ΔE", minΔELevel);

        //                AddPointInChart(i, Chart_CMYKDensity_C.Series["Series3"], Chart_CMYKDensity_C.Series["Series3Point"], 1, Convert.ToDecimal(DSDensity_C.Rows[i]["ΔElimit"]), Color.Red.Name.ToString(), false, "ΔE", -1);
        //                AddPointInChart(i, Chart_CMYKDensity_C.Series["Series3"], Chart_CMYKDensity_C.Series["Series3Point"], 2, Convert.ToDecimal(DSDensity_C.Rows[i]["ΔElimit"]), Color.Red.Name.ToString(), false, "ΔE", -1);
        //            }
        //        }

        //        FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(Chart_CMYKDensity_C.ImageLocation) + "." + Chart_CMYKDensity_C.ImageType;
        //        Chart_CMYKDensity_C.SaveImage(Server.MapPath(FileName));

        //        #endregion

        //    }
        //}

        //private void FillDensity_M_Chart()
        //{
        //    decimal minΔELevel = decimal.MaxValue;
        //    decimal maxΔELevel = decimal.MinValue;

        //    foreach (DataRow dr in DSDensity_M.Rows)
        //    {
        //        decimal accountLevel = dr.Field<decimal>("ΔE");
        //        minΔELevel = Math.Min(minΔELevel, accountLevel);
        //        maxΔELevel = Math.Max(maxΔELevel, accountLevel);
        //    }

        //    if (DSDensity_M != null && DSDensity_M.Rows.Count > 0)
        //    {
        //        #region Fill CMYKDensity_Magenta Chart
        //        Chart_CMYKDensity_M.Series["Series1"].Points.Clear();
        //        Chart_CMYKDensity_M.Series["Series1"]["TooltipOffset"] = "True";
        //        Chart_CMYKDensity_M.Series["Series1Point"].Points.Clear();
        //        Chart_CMYKDensity_M.Series["Series2"].Points.Clear();
        //        Chart_CMYKDensity_M.Series["Series2"]["TooltipOffset"] = "True";
        //        Chart_CMYKDensity_M.Series["Series2Point"].Points.Clear();
        //        Chart_CMYKDensity_M.Series["Series3"].Points.Clear();
        //        Chart_CMYKDensity_M.Series["Series3Point"].Points.Clear();


        //        if (DSDensity_M != null && DSDensity_M.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < DSDensity_M.Rows.Count; i++)
        //            {
        //                AddPointInChart(i, Chart_CMYKDensity_M.Series["Series1"], Chart_CMYKDensity_M.Series["Series1Point"], Convert.ToDecimal(DSDensity_M.Rows[i]["Density"]), Convert.ToDecimal(DSDensity_M.Rows[i]["ΔE"]), Color.Blue.Name.ToString(), true, "ΔE", minΔELevel);

        //                AddPointInChart(i, Chart_CMYKDensity_M.Series["Series3"], Chart_CMYKDensity_M.Series["Series3Point"], 1, Convert.ToDecimal(DSDensity_M.Rows[i]["ΔElimit"]), Color.Red.Name.ToString(), false, "ΔE", -1);
        //                AddPointInChart(i, Chart_CMYKDensity_M.Series["Series3"], Chart_CMYKDensity_M.Series["Series3Point"], 2, Convert.ToDecimal(DSDensity_M.Rows[i]["ΔElimit"]), Color.Red.Name.ToString(), false, "ΔE", -1);
        //            }
        //        }

        //        FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(Chart_CMYKDensity_M.ImageLocation) + "." + Chart_CMYKDensity_M.ImageType;
        //        Chart_CMYKDensity_M.SaveImage(Server.MapPath(FileName));

        //        #endregion

        //    }
        //}

        //private void FillDensity_Y_Chart()
        //{
        //    decimal minΔELevel = decimal.MaxValue;
        //    decimal maxΔELevel = decimal.MinValue;

        //    foreach (DataRow dr in DSDensity_Y.Rows)
        //    {
        //        decimal accountLevel = dr.Field<decimal>("ΔE");
        //        minΔELevel = Math.Min(minΔELevel, accountLevel);
        //        maxΔELevel = Math.Max(maxΔELevel, accountLevel);
        //    }

        //    if (DSDensity_Y != null && DSDensity_Y.Rows.Count > 0)
        //    {
        //        #region Fill CMYKDensity_Yellow Chart
        //        Chart_CMYKDensity_Y.Series["Series1"].Points.Clear();
        //        Chart_CMYKDensity_Y.Series["Series1"]["TooltipOffset"] = "True";
        //        Chart_CMYKDensity_Y.Series["Series1Point"].Points.Clear();
        //        Chart_CMYKDensity_Y.Series["Series2"].Points.Clear();
        //        Chart_CMYKDensity_Y.Series["Series2"]["TooltipOffset"] = "True";
        //        Chart_CMYKDensity_Y.Series["Series2Point"].Points.Clear();
        //        Chart_CMYKDensity_Y.Series["Series3"].Points.Clear();
        //        Chart_CMYKDensity_Y.Series["Series3Point"].Points.Clear();


        //        if (DSDensity_Y != null && DSDensity_Y.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < DSDensity_Y.Rows.Count; i++)
        //            {
        //                AddPointInChart(i, Chart_CMYKDensity_Y.Series["Series1"], Chart_CMYKDensity_Y.Series["Series1Point"], Convert.ToDecimal(DSDensity_Y.Rows[i]["Density"]), Convert.ToDecimal(DSDensity_Y.Rows[i]["ΔE"]), Color.Blue.Name.ToString(), true, "ΔE", minΔELevel);

        //                AddPointInChart(i, Chart_CMYKDensity_Y.Series["Series3"], Chart_CMYKDensity_Y.Series["Series3Point"], 1, Convert.ToDecimal(DSDensity_Y.Rows[i]["ΔElimit"]), Color.Red.Name.ToString(), false, "ΔE", -1);
        //                AddPointInChart(i, Chart_CMYKDensity_Y.Series["Series3"], Chart_CMYKDensity_Y.Series["Series3Point"], 2, Convert.ToDecimal(DSDensity_Y.Rows[i]["ΔElimit"]), Color.Red.Name.ToString(), false, "ΔE", -1);
        //            }
        //        }

        //        FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(Chart_CMYKDensity_Y.ImageLocation) + "." + Chart_CMYKDensity_Y.ImageType;
        //        Chart_CMYKDensity_Y.SaveImage(Server.MapPath(FileName));

        //        #endregion

        //    }
        //}

        //private void FillDensity_K_Chart()
        //{
        //    decimal minΔELevel = decimal.MaxValue;
        //    decimal maxΔELevel = decimal.MinValue;

        //    foreach (DataRow dr in DSDensity_K.Rows)
        //    {
        //        decimal accountLevel = dr.Field<decimal>("ΔE");
        //        minΔELevel = Math.Min(minΔELevel, accountLevel);
        //        maxΔELevel = Math.Max(maxΔELevel, accountLevel);
        //    }

        //    if (DSDensity_K != null && DSDensity_K.Rows.Count > 0)
        //    {
        //        #region Fill CMYKDensity_Black Chart
        //        Chart_CMYKDensity_K.Series["Series1"].Points.Clear();
        //        Chart_CMYKDensity_K.Series["Series1"]["TooltipOffset"] = "True";
        //        Chart_CMYKDensity_K.Series["Series1Point"].Points.Clear();
        //        Chart_CMYKDensity_K.Series["Series2"].Points.Clear();
        //        Chart_CMYKDensity_K.Series["Series2"]["TooltipOffset"] = "True";
        //        Chart_CMYKDensity_K.Series["Series2Point"].Points.Clear();
        //        Chart_CMYKDensity_K.Series["Series3"].Points.Clear();
        //        Chart_CMYKDensity_K.Series["Series3Point"].Points.Clear();


        //        if (DSDensity_K != null && DSDensity_K.Rows.Count > 0)
        //        {
        //            for (int i = 0; i < DSDensity_K.Rows.Count; i++)
        //            {
        //                AddPointInChart(i, Chart_CMYKDensity_K.Series["Series1"], Chart_CMYKDensity_K.Series["Series1Point"], Convert.ToDecimal(DSDensity_K.Rows[i]["Density"]), Convert.ToDecimal(DSDensity_K.Rows[i]["ΔE"]), Color.Blue.Name.ToString(), true, "ΔE", minΔELevel);

        //                AddPointInChart(i, Chart_CMYKDensity_K.Series["Series3"], Chart_CMYKDensity_K.Series["Series3Point"], 1, Convert.ToDecimal(DSDensity_K.Rows[i]["ΔElimit"]), Color.Red.Name.ToString(), false, "ΔE", -1);
        //                AddPointInChart(i, Chart_CMYKDensity_K.Series["Series3"], Chart_CMYKDensity_K.Series["Series3Point"], 2, Convert.ToDecimal(DSDensity_K.Rows[i]["ΔElimit"]), Color.Red.Name.ToString(), false, "ΔE", -1);
        //            }
        //        }

        //        FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(Chart_CMYKDensity_K.ImageLocation) + "." + Chart_CMYKDensity_K.ImageType;
        //        Chart_CMYKDensity_K.SaveImage(Server.MapPath(FileName));

        //        #endregion

        //    }
        //}
        //private void AddPointInChart(int _Index, Series _objSeries, Series _objSeriesPoint, decimal _XValue, decimal _YValue, string ColorName, bool showCircle, string From_Delta, decimal BestmatchValue)
        //{
        //    _objSeries.ChartType = SeriesChartType.Spline;
        //    _objSeries.Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(_XValue)), Utilities.DecimalRound(Convert.ToDouble(_YValue)));
        //    _objSeries.Points[_Index].MarkerStyle = MarkerStyle.None;
        //    _objSeries.Points[_Index].BorderWidth = 1;


        //    _objSeriesPoint.ChartType = SeriesChartType.Point;
        //    _objSeriesPoint.Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(_XValue)), Utilities.DecimalRound(Convert.ToDouble(_YValue)));
        //    _objSeriesPoint.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
        //    _objSeriesPoint.Points[_Index].MarkerStyle = showCircle ? MarkerStyle.Circle : MarkerStyle.None;
            
        //    if (_YValue == BestmatchValue)
        //    {
        //        _objSeriesPoint.Points[_Index].MarkerSize = showCircle ? 10 : 0;
        //        _objSeriesPoint.Points[_Index].BorderWidth = 3;
        //        _objSeriesPoint.Points[_Index].MarkerBorderColor = Color.Aqua;
        //    }
        //    else
        //    {
        //        _objSeriesPoint.Points[_Index].MarkerSize = showCircle ? 5 : 0;
        //        _objSeriesPoint.Points[_Index].BorderWidth = 1;
        //        _objSeriesPoint.Points[_Index].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
        //    }


        //    #region Add Tooltip HTML
        //    StringBuilder toolTip = new StringBuilder();
        //    toolTip.Append("<table>");
        //    toolTip.Append("<tr><td><b>Density : </b>" + _XValue + "</td></tr>");
        //    toolTip.Append("<tr><td><table>");
        //    toolTip.Append("<tr><td><b>" + From_Delta + " : </b></td><td>");
        //    toolTip.Append(_YValue);
        //    toolTip.Append("</td></tr>");
        //    toolTip.Append("</table></td></tr>");
        //    toolTip.Append("</table>");
        //    #endregion
        //    _objSeriesPoint.Points[_Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";

        //}

        public string FileName;

        #region IViewReportCMYKDensity Members

        public int MeasurementId { get; set; }
        public int OptimizeRunId { get; set; }
        public int ProjectId { get; set; }
        public DataTable DSDensity_C { get; set; }
        public DataTable DSDensity_M { get; set; }
        public DataTable DSDensity_Y { get; set; }
        public DataTable DSDensity_K { get; set; }

        #endregion
    }
}