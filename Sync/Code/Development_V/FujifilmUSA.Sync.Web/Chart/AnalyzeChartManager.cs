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
        public AnalyzeChartManager(Chart analyzeChart, List<Entity.Measurement> lst, bool _isValidation)
        {
            AnalyzeChart = analyzeChart;
            Measurements = lst;
            IsValidation = _isValidation;
        }
        public Chart AnalyzeChart { get; set; }
        public List<Entity.Measurement> Measurements { get; set; }
        public int CurrentMeasurementID { get; set; }
        public string MeasurementName { get; set; }
        public bool IsValidation { get; set; }

        //======== Data Point Index ==========//
        int[] PatchTypeID = new int[8];
        public void BindChart()
        {
            //SetGraphWidth();
            SetLast();

            foreach (Series series in AnalyzeChart.Series)
            {
                series.Points.Clear();
            }
            int i = 0;
            Measurements.ForEach(p =>
            {
                p.MeasurementPatches.ForEach(x =>
                {
                    switch (x.MeasurementPatchId)
                    {
                        case (int)CPPatchType.Cyan:
                            x.HtmlColor = "#00FFFF";
                            AddPoint(0, x, i, p.MeasurementDate, p.MeasurementName, p.IsUsed, IsValidation);
                            break;
                        case (int)CPPatchType.Magenta:
                            x.HtmlColor = "#FF00FF";
                            AddPoint(1, x, i, p.MeasurementDate, p.MeasurementName, p.IsUsed, IsValidation);
                            break;
                        case (int)CPPatchType.Yellow:
                            x.HtmlColor = "#FFFF00";
                            AddPoint(2, x, i, p.MeasurementDate, p.MeasurementName, p.IsUsed, IsValidation);
                            break;
                        case (int)CPPatchType.Black:
                            x.HtmlColor = "#000000";
                            AddPoint(3, x, i, p.MeasurementDate, p.MeasurementName, p.IsUsed, IsValidation);
                            break;
                        case (int)CPPatchType.Overprint_Red:
                            x.HtmlColor = "#FF0000";
                            AddPoint(4, x, i, p.MeasurementDate, p.MeasurementName, p.IsUsed, IsValidation);
                            break;
                        case (int)CPPatchType.Overprint_Green:
                            x.HtmlColor = "#008000";
                            AddPoint(5, x, i, p.MeasurementDate, p.MeasurementName, p.IsUsed, IsValidation);
                            break;
                        case (int)CPPatchType.Overprint_Blue:
                            x.HtmlColor = "#0000FF";
                            AddPoint(6, x, i, p.MeasurementDate, p.MeasurementName, p.IsUsed, IsValidation);
                            break;
                        case (int)CPPatchType.Paper:
                            x.HtmlColor = "#FFFFFF";
                            AddPoint(7, x, i, p.MeasurementDate, p.MeasurementName, p.IsUsed, IsValidation);
                            break;
                    }
                });
                if (!IsValidation) //if Optimization, then only add checkboxes
                {
                    AddCheckBox(i, p, Measurements.Where(x => x.IsUsed).Count());
                }
                i++;
            });

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
        private void AddPoint(int seriesId, Entity.MeasurementPatch measurementPatch, int measurementno, DateTime MeasurementDate, string measurementName, bool IsUsed, bool _isValidation)
        {
            int index = PatchTypeID[seriesId];

            AnalyzeChart.Series["Series" + seriesId].ChartType = SeriesChartType.Spline;  // Set series type 
            if (_isValidation) //if Validation
            {
                AnalyzeChart.Series["Series" + seriesId].Points.AddXY(MeasurementDate.ToString(), measurementPatch.Δe2000.ToString()); // Add XY point
            }
            else   //if Optimization
            {
                AnalyzeChart.Series["Series" + seriesId].Points.AddXY(measurementno + 1, measurementPatch.Δe2000.ToString()); // Add XY point
            }
            AnalyzeChart.Series["Series" + seriesId].Color = ColorTranslator.FromHtml(measurementPatch.HtmlColor); // Set HTML color to series         
            AnalyzeChart.Series["Series" + seriesId].Points[index].ToolTip = measurementName;

            //====== Call javascript function for change current selected point and rebind chart ==================//
            AnalyzeChart.Series["Series" + seriesId.ToString()].Points[index].MapAreaAttributes = "OnClick=\"javascript:ClickOnPoint( '" + measurementPatch.MeasurementId + "');\"";

            if (CurrentMeasurementID != 0 && CurrentMeasurementID == measurementPatch.MeasurementId) // Check current selected point
            {
                AnalyzeChart.Series["Series" + seriesId].Points[index].MarkerSize = 10;
                AnalyzeChart.Series["Series" + seriesId].Points[index].MarkerColor = ColorTranslator.FromHtml(measurementPatch.HtmlColor);
                AnalyzeChart.Series["Series" + seriesId].Points[index].MarkerStyle = MarkerStyle.Circle;

                // property for bind measurement name in header chart
                MeasurementName = measurementName;
            }
            else
            {
                AnalyzeChart.Series["Series" + seriesId].Points[index].MarkerSize = 10;
                AnalyzeChart.Series["Series" + seriesId].Points[index].MarkerColor = Color.Gray;
                AnalyzeChart.Series["Series" + seriesId].Points[index].MarkerStyle = MarkerStyle.Circle;
            }
            PatchTypeID[seriesId]++;
        }
        private void AddCheckBox(int i, Entity.Measurement measurement, int isUsedCount)
        {
            //========= Put check or uncheck check box and call javascript function for rebind chart ===/
            var c = new CustomLabel(i + 1, i + 1 + 0.001, "", 1, LabelMarkStyle.None)
                        {
                            Image = measurement.IsUsed ? "Images/checked_checkbox.gif" : "Images/uncheck_checkbox.gif",
                            ImageMapAreaAttributes = "onclick=\"ClickOnCheckBox('" + measurement.MeasurementId + "','" + !measurement.IsUsed + "'," + isUsedCount + ");\""
                        };
            AnalyzeChart.ChartAreas["ChartArea1"].AxisX.CustomLabels.Add(c); // Add Custom Label

        }

    }


}