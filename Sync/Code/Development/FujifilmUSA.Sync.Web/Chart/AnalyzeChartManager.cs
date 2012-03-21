using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;
using System.Text;
using System.Drawing;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web
{
	public class AnalyzeChartManager
	{
        public AnalyzeChartManager(Chart analyzeChart,List<Entity.Measurement> lst)
        {
            AnalyzeChart = analyzeChart;
            lstMeasurement = lst;
        }
        public Chart AnalyzeChart { get; set; }
        public List<Entity.Measurement> lstMeasurement { get; set; }
        public int CurrentMeasurementID { get; set; }

        //======== Data Point Index ==========//
        int[] PatchTypeID =  new int[8];
        public void BindChart()
        {
            SetGraphWidth();
            SetLast();
            
            int i = 0;
            foreach (var measurement in lstMeasurement)
            {
                #region Bind point in chart in "PatchTypeID" wise
                switch (measurement.ReferenceChart.PatchTypeId)
                {
                    case  (int)CPPatchType.Cyan :
                        AddPoint(0,measurement);
                        break;
                    case (int)CPPatchType.Magenta:
                        AddPoint(1, measurement);
                        break;
                    case (int)CPPatchType.Yellow:
                        AddPoint(2, measurement);
                        break;
                    case (int)CPPatchType.Black:
                        AddPoint(3, measurement);
                        break;
                    case (int)CPPatchType.Overprint_Red:
                        AddPoint(4, measurement);
                        break;
                    case (int)CPPatchType.Overprint_Green:
                        AddPoint(5, measurement);
                        break;
                    case (int)CPPatchType.Overprint_Blue:
                        AddPoint(6, measurement);
                        break;
                    case (int)CPPatchType.Triplet_Solid_Gray:
                        AddPoint(7, measurement);
                        break;
                }
                #endregion
                AddCheckBox(i, measurement);
                i++;
            }

        }
        private void SetGraphWidth()
        {
            //========== Check Measurement Count ==========//
            if (lstMeasurement.Count < 240)
                AnalyzeChart.Width = 650;
            else
                AnalyzeChart.Width = (lstMeasurement.Count * 25);
        }
        private void SetLast()
        {
            //====== Selecte Default last point in trend line chart ========//
            if (lstMeasurement != null && lstMeasurement.Count > 0)
                if (CurrentMeasurementID == 0)
                    CurrentMeasurementID = lstMeasurement[lstMeasurement.Count - 1].MeasurementId;
        }
        private void AddPoint(int seriesId, Entity.Measurement measurement)
        {
            int index = PatchTypeID[seriesId];
            
            AnalyzeChart.Series["Series" + seriesId.ToString()].ChartType = SeriesChartType.Spline;  // Set series type 
            AnalyzeChart.Series["Series" + seriesId.ToString()].Points.AddXY(measurement.MeasurementNo.ToString(), measurement.Δe.ToString()); // Add XY point 
            AnalyzeChart.Series["Series" + seriesId.ToString()].Color = System.Drawing.ColorTranslator.FromHtml(measurement.ReferenceChart.HtmlColor); // Set HTML color to series         
            AnalyzeChart.Series["Series" + seriesId.ToString()].Points[index].ToolTip = measurement.MeasurementName;

            //====== Call javascript function for change current selected point and rebind chart ==================//
            AnalyzeChart.Series["Series" + seriesId.ToString()].Points[index].MapAreaAttributes = "OnClick=\"javascript:ClickOnPoint( '" + measurement.MeasurementId.ToString() + "');\"";

            if (CurrentMeasurementID != 0 && CurrentMeasurementID == measurement.MeasurementId) // Check current selected point
            {
                AnalyzeChart.Series["Series" + seriesId.ToString()].Points[index].MarkerSize = 10;
                AnalyzeChart.Series["Series" + seriesId.ToString()].Points[index].MarkerColor = Color.FromArgb(200, 255, 0, 0);
                AnalyzeChart.Series["Series" + seriesId.ToString()].Points[index].MarkerImage = "Images/MarkerRed_Circle.gif";  // Set red circle point image.
            }
            ////else if (Convert.ToDecimal(_YValue) > Convert.ToDecimal(_ELimit)) // Check limt of value
            ////{
            ////    AnalyzeChart.Series["Series" + seriesId.ToString()].Points[_Index].MarkerSize = 4;
            ////    AnalyzeChart.Series["Series" + seriesId.ToString()].Points[_Index].MarkerColor = Color.FromArgb(200, 255, 0, 0);
            ////    AnalyzeChart.Series["Series" + seriesId.ToString()].Points[_Index].MarkerImage = "Images/MarkerRed.gif"; // Set tringle image
            ////}
            //else
            {
                AnalyzeChart.Series["Series" + seriesId.ToString()].Points[index].MarkerSize = 10;
                AnalyzeChart.Series["Series" + seriesId.ToString()].Points[index].MarkerColor = Color.Gray;
                AnalyzeChart.Series["Series" + seriesId.ToString()].Points[index].MarkerStyle = MarkerStyle.Circle;
            }
            PatchTypeID[seriesId]++;
        }
        private void AddCheckBox(int i, Entity.Measurement measurement)
        {
            //========= Put check or uncheck check box and call javascript function for rebind chart ===/
            CustomLabel c = new CustomLabel(i + 1, i+ 1 + 0.001, "", 1, LabelMarkStyle.None);
            c.Image = measurement.IsUsed ? "Images/checked_checkbox.gif" : "Images/uncheck_checkbox.gif";
            c.ImageMapAreaAttributes = "onclick=\"ClickOnCheckBox('" + measurement.MeasurementId + "','" + !measurement.IsUsed + "');\""; // Bind javascript function 
            AnalyzeChart.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(c); // Add Custom Label

        }
      
	}

  
}