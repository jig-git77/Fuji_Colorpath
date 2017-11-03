using FujifilmUSA.Sync.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl.PopupControl
{
    public partial class SpotColorPatchDetails : BaseControl
    {
        private const string _recActual = "actualText";
        private const string _recPaperPathMetrics = "paperPathMetrics";
        private const string _recL = "lText";
        private const string _recA = "aText";
        private const string _recB = "bText";
        private const string _recTarget = "targe39tText";
        private const string _recDeltaL = "deltaLText";
        private const string _recDeltaA = "deltaAText";
        private const string _recDeltaB = "deltaBText";
        decimal T_L, T_a, T_b, P_L, P_a, P_b, M_L, M_a, M_b;
        string htmlColor;
        int MeasurementID;

        protected void Page_Load(object sender, EventArgs e)
        {
            T_L = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["targetL"])) ? 0 : Convert.ToDecimal(Request.QueryString["targetL"]);
            T_a = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["targetA"])) ? 0 : Convert.ToDecimal(Request.QueryString["targetA"]);
            T_b = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["targetB"])) ? 0 : Convert.ToDecimal(Request.QueryString["targetB"]);

            P_L = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["actualL"])) ? 0 : Convert.ToDecimal(Request.QueryString["actualL"]);
            P_a = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["actualA"])) ? 0 : Convert.ToDecimal(Request.QueryString["actualA"]);
            P_b = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["actualB"])) ? 0 : Convert.ToDecimal(Request.QueryString["actualB"]);

            M_L = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["measuredL"])) ? 0 : Convert.ToDecimal(Request.QueryString["measuredL"]);
            M_a = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["measuredA"])) ? 0 : Convert.ToDecimal(Request.QueryString["measuredA"]);
            M_b = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["measuredB"])) ? 0 : Convert.ToDecimal(Request.QueryString["measuredB"]);

            MeasurementID = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["measurementID"])) ? 0 : Convert.ToInt32(Request.QueryString["measurementID"]);

            htmlColor = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["htmlColor"])) ? "" : ("#" + Convert.ToString(Request.QueryString["htmlColor"]));
            BindValues();
            FillPaperChart(T_L, T_a, T_b, P_L, P_a, P_b, M_L, M_a, M_b, MeasurementID);
        }


        public void BindValues()
        {
            if (MeasurementID > 0) // if optimization-0
            {
                divPatchDetailsRun.Visible = true;
                decimal Delta_Target_PredictedL, Delta_Measured_TargetL, Delta_Measured_PredictedL,
                    Delta_Target_PredictedA, Delta_Measured_TargetA, Delta_Measured_PredictedA,
                    Delta_Target_PredictedB, Delta_Measured_TargetB, Delta_Measured_PredictedB;

                Delta_Target_PredictedL = T_L - P_L;
                Delta_Target_PredictedA = T_a - P_a;
                Delta_Target_PredictedB = T_b - P_b;

                Delta_Measured_TargetL = M_L - T_L;
                Delta_Measured_TargetA = M_a - T_a;
                Delta_Measured_TargetB = M_b - T_b;

                Delta_Measured_PredictedL = M_L - P_L;
                Delta_Measured_PredictedA = M_a - P_a;
                Delta_Measured_PredictedB = M_b - P_b;

                lblTargetL.Text = T_L.ToString();
                lblTargetA.Text = T_a.ToString();
                lblTargetB.Text = T_b.ToString();

                lblActualL.Text = P_L.ToString();
                lblActualA.Text = P_a.ToString();
                lblActualB.Text = P_b.ToString();

                lblMeasuredL.Text = M_L.ToString();
                lblMeasuredA.Text = M_a.ToString();
                lblMeasuredB.Text = M_b.ToString();

                lblMeasuredL.Text = M_L.ToString();
                lblMeasuredA.Text = M_a.ToString();
                lblMeasuredB.Text = M_b.ToString();

                lblTarget_PredictedL.Text = Delta_Target_PredictedL.ToString();
                lblTarget_PredictedA.Text = Delta_Target_PredictedA.ToString();
                lblTarget_PredictedB.Text = Delta_Target_PredictedB.ToString();

                lblMeasured_TargetL.Text = Delta_Measured_TargetL.ToString();
                lblMeasured_TargetA.Text = Delta_Measured_TargetA.ToString();
                lblMeasured_TargetB.Text = Delta_Measured_TargetB.ToString();

                lblMeasured_PredictedL.Text = Delta_Measured_PredictedL.ToString();
                lblMeasured_PredictedA.Text = Delta_Measured_PredictedA.ToString();
                lblMeasured_PredictedB.Text = Delta_Measured_PredictedB.ToString();

                divActual.Style.Add("background-color", htmlColor);
                divTarget.Style.Add("background-color", htmlColor);
                divMeasured.Style.Add("background-color", htmlColor);
            }
            else
            {
                divPatchDetailsRun0.Visible = true;
                decimal Delta_L, Delta_a, Delta_b;
                Delta_L = P_L - T_L;
                Delta_a = P_a - T_a;
                Delta_b = P_b - T_b;

                lblTargetLRun0.Text = T_L.ToString();
                lblTargetARun0.Text = T_a.ToString();
                lblTargetBRun0.Text = T_b.ToString();

                lblActualLRun0.Text = P_L.ToString();
                lblActualARun0.Text = P_a.ToString();
                lblActualBRun0.Text = P_b.ToString();
                lblDeltaL.Text = Delta_L.ToString();
                lblDeltaA.Text = Delta_a.ToString();
                lblDeltaB.Text = Delta_b.ToString();

                divActualRun0.Style.Add("background-color", htmlColor);
                divTargetRun0.Style.Add("background-color", htmlColor);
            }
            //FillPaperChart(Delta_L, Delta_a, Delta_b);
        }

        #region Fill Paper Chart
        private void FillPaperChart(decimal Target_L, decimal Target_A, decimal Target_B, decimal Pre_L, decimal Pre_A, decimal Pre_B, decimal Measured_L, decimal Measured_A, decimal Measured_B, int measurementID)
        {
            //string FileName;
            #region Fill Paper 1 Chart

            chartPaper1.Series["Red"].Points.Clear();
            chartPaper1.Series["Green"].Points.Clear();

            chartPaper1.Series["Red"].Points.AddXY(Convert.ToDouble(Target_A), Convert.ToDouble(Target_B));
            //chartPaper1.Series["Red"].Points.AddXY(Convert.ToDouble(0), Convert.ToDouble(0));
            chartPaper1.Series["Red"].ChartType = SeriesChartType.Point;
            chartPaper1.Series["Red"].Points[0].BorderWidth = 4;
            chartPaper1.Series["Red"].CustomProperties = "PolarDrawingStyle=Marker";

            chartPaper1.Series["Green"].Points.AddXY(Convert.ToDouble(Pre_A), Convert.ToDouble(Pre_B));
            //chartPaper1.Series["Green"].Points.AddXY(Convert.ToDouble(0), Convert.ToDouble(0));
            chartPaper1.Series["Green"].ChartType = SeriesChartType.Point;
            chartPaper1.Series["Green"].Points[0].BorderWidth = 2;
            chartPaper1.Series["Green"].CustomProperties = "PolarDrawingStyle=Marker";

            if (measurementID > 0)
            {
                chartPaper1.Series["Blue"].Points.AddXY(Convert.ToDouble(Measured_A), Convert.ToDouble(Measured_B));
                chartPaper1.Series["Blue"].ChartType = SeriesChartType.Point;
                chartPaper1.Series["Blue"].Points[0].BorderWidth = 2;
                chartPaper1.Series["Blue"].CustomProperties = "PolarDrawingStyle=Marker";
            }
            else
            {
                chartPaper1.Series["Blue"].Enabled = false;
            }

            #endregion

            #region Fill Paper 2 chart

            chartPaper2.Series["Red"].Points.Clear();
            chartPaper2.Series["Green"].Points.Clear();

            chartPaper2.Series["Red"].Points.AddXY(Convert.ToDouble(0), Convert.ToDouble(Target_L));
            chartPaper2.Series["Red"].ChartType = SeriesChartType.Point;
            chartPaper2.Series["Red"].Points[0].BorderWidth = 4;
            chartPaper2.Series["Red"].CustomProperties = "PolarDrawingStyle=Marker";

            chartPaper2.Series["Green"].Points.AddXY(Convert.ToDouble(0), Convert.ToDouble(Pre_L));
            chartPaper2.Series["Green"].ChartType = SeriesChartType.Point;
            chartPaper2.Series["Green"].Points[0].BorderWidth = 2;
            chartPaper2.Series["Green"].CustomProperties = "PolarDrawingStyle=Marker";

            if (measurementID > 0)
            {
                chartPaper2.Series["Blue"].Points.AddXY(Convert.ToDouble(0), Convert.ToDouble(Measured_L));
                chartPaper2.Series["Blue"].ChartType = SeriesChartType.Point;
                chartPaper2.Series["Blue"].Points[0].BorderWidth = 2;
                chartPaper2.Series["Blue"].CustomProperties = "PolarDrawingStyle=Marker";
            }
            else
            {
                chartPaper1.Series["Blue"].Enabled = false;
            }
            #endregion
        }
    }
    #endregion
}