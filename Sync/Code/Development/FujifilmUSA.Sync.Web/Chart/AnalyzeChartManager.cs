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
        public AnalyzeChartManager(Chart analyzeChart, List<Entity.Measurement> lst)
        {
            AnalyzeChart = analyzeChart;
            Measurements = lst;
        }
        public Chart AnalyzeChart { get; set; }
        public List<Entity.Measurement> Measurements { get; set; }
        public int CurrentMeasurementID { get; set; }

        //======== Data Point Index ==========//
        int[] PatchTypeID = new int[8];
        public void BindChart()
        {
            SetGraphWidth();
            SetLast();

            int i = 0;
            foreach (Entity.Measurement measurement in Measurements)
            {
                #region Bind point in chart in "PatchTypeID" wise

                measurement.MeasurementPatches.ForEach(x =>
                                                           {
                                                               switch (x.MeasurementPatchId)
                                                               {
                                                                   case (int)CPPatchType.Cyan:
                                                                       AddPoint(0, x, i, measurement.MeasurementName);
                                                                       break;
                                                                   case (int)CPPatchType.Magenta:
                                                                       AddPoint(1, x, i, measurement.MeasurementName);
                                                                       break;
                                                                   case (int)CPPatchType.Yellow:
                                                                       AddPoint(2, x, i, measurement.MeasurementName);
                                                                       break;
                                                                   case (int)CPPatchType.Black:
                                                                       AddPoint(3, x, i, measurement.MeasurementName);
                                                                       break;
                                                                   case (int)CPPatchType.Overprint_Red:
                                                                       AddPoint(4, x, i, measurement.MeasurementName);
                                                                       break;
                                                                   case (int)CPPatchType.Overprint_Green:
                                                                       AddPoint(5, x, i, measurement.MeasurementName);
                                                                       break;
                                                                   case (int)CPPatchType.Overprint_Blue:
                                                                       AddPoint(6, x, i, measurement.MeasurementName);
                                                                       break;
                                                                   case (int)CPPatchType.Triplet_Solid_Gray:
                                                                       AddPoint(7, x, i, measurement.MeasurementName);
                                                                       break;
                                                               }
                                                           });
                #endregion
                AddCheckBox(i, measurement);
                i++;
            }

        }
        private void SetGraphWidth()
        {
            //========== Check Measurement Count ==========//
            if (Measurements.Count < 240)
                AnalyzeChart.Width = 650;
            else
                AnalyzeChart.Width = (Measurements.Count * 25);
        }
        private void SetLast()
        {
            //====== Selecte Default last point in trend line chart ========//
            if (Measurements != null && Measurements.Count > 0)
                if (CurrentMeasurementID == 0)
                    CurrentMeasurementID = Measurements[Measurements.Count - 1].MeasurementId;
        }
        private void AddPoint(int seriesId, Entity.MeasurementPatch measurementPatch, int measurementNo, string measurementName)
        {
            int index = PatchTypeID[seriesId];

            AnalyzeChart.Series["Series" + seriesId].ChartType = SeriesChartType.Spline;  // Set series type 
            AnalyzeChart.Series["Series" + seriesId].Points.AddXY(measurementNo + 1, measurementPatch.Δe.ToString()); // Add XY point
            AnalyzeChart.Series["Series" + seriesId].Color = ColorTranslator.FromHtml(measurementPatch.HtmlColor); // Set HTML color to series         
            AnalyzeChart.Series["Series" + seriesId].Points[index].ToolTip = measurementName;

            //====== Call javascript function for change current selected point and rebind chart ==================//
            AnalyzeChart.Series["Series" + seriesId.ToString()].Points[index].MapAreaAttributes = "OnClick=\"javascript:ClickOnPoint( '" + measurementPatch.MeasurementId + "');\"";

            if (CurrentMeasurementID != 0 && CurrentMeasurementID == measurementPatch.MeasurementId) // Check current selected point
            {
                AnalyzeChart.Series["Series" + seriesId].Points[index].MarkerSize = 10;
                AnalyzeChart.Series["Series" + seriesId].Points[index].MarkerColor = Color.FromArgb(200, 255, 0, 0);
                AnalyzeChart.Series["Series" + seriesId].Points[index].MarkerImage = "Images/MarkerRed_Circle.gif";  // Set red circle point image.
            }
            {
                AnalyzeChart.Series["Series" + seriesId].Points[index].MarkerSize = 10;
                AnalyzeChart.Series["Series" + seriesId].Points[index].MarkerColor = Color.Gray;
                AnalyzeChart.Series["Series" + seriesId].Points[index].MarkerStyle = MarkerStyle.Circle;
            }
            PatchTypeID[seriesId]++;
        }
        private void AddCheckBox(int i, Entity.Measurement measurement)
        {
            //========= Put check or uncheck check box and call javascript function for rebind chart ===/
            var c = new CustomLabel(i + 1, i + 1 + 0.001, "", 1, LabelMarkStyle.None)
                        {
                            Image = measurement.IsUsed ? "Images/checked_checkbox.gif" : "Images/uncheck_checkbox.gif",
                            ImageMapAreaAttributes = "onclick=\"ClickOnCheckBox('" + measurement.MeasurementId + "','" + !measurement.IsUsed + "');\""
                        };
            AnalyzeChart.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(c); // Add Custom Label

        }

    }


}