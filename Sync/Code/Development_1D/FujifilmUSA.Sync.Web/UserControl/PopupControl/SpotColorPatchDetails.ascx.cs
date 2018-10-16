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
        decimal T_L, T_a, T_b, P_L, P_a, P_b, M_L, M_a, M_b, deltaE, deltaTP, deltaMT, deltaMP;
        string htmlColor;
        string Predicated_htmlColor, Measured_htmlColor;
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

            deltaE = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["deltaE"])) ? 0 : Convert.ToDecimal(Request.QueryString["deltaE"]);
            deltaTP = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["deltaTP"])) ? 0 : Convert.ToDecimal(Request.QueryString["deltaTP"]);
            deltaMT = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["deltaMT"])) ? 0 : Convert.ToDecimal(Request.QueryString["deltaMT"]);
            deltaMP = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["deltaMP"])) ? 0 : Convert.ToDecimal(Request.QueryString["deltaMP"]);

            MeasurementID = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["measurementID"])) ? 0 : Convert.ToInt32(Request.QueryString["measurementID"]);
            hdnMeasurementID.Value = MeasurementID.ToString();
            htmlColor = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["htmlColor"])) ? "" : ("#" + Convert.ToString(Request.QueryString["htmlColor"]));
            Predicated_htmlColor = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["P_htmlColor"])) ? "" : ("#" + Convert.ToString(Request.QueryString["P_htmlColor"]));
            Measured_htmlColor = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["M_htmlColor"])) ? "" : ("#" + Convert.ToString(Request.QueryString["M_htmlColor"]));

            BindValues();
            //BindValuesNew();
            //FillPaperChart(T_L, T_a, T_b, P_L, P_a, P_b, M_L, M_a, M_b, MeasurementID);
        }


        public void BindValues()
        {
            if (MeasurementID > 0) // if optimization run !=0
            {
                //divPatchDetailsRun.Visible = true;
                tblDetailsMoreRun.Visible = true;
                tblColorMoreRun.Visible = true;
                //decimal Delta_Target_PredictedL, Delta_Measured_TargetL, Delta_Measured_PredictedL,
                //    Delta_Target_PredictedA, Delta_Measured_TargetA, Delta_Measured_PredictedA,
                //    Delta_Target_PredictedB, Delta_Measured_TargetB, Delta_Measured_PredictedB;

                decimal delta_Predicted_Target_L, delta_Measured_Target_L,
                    delta_Predicted_Target_A, delta_Measured_Target_A,
                    delta_Predicted_Target_B, delta_Measured_Target_B;

                //Delta_Target_PredictedL = T_L - P_L;
                //Delta_Target_PredictedA = T_a - P_a;
                //Delta_Target_PredictedB = T_b - P_b;

                //Delta_Measured_TargetL = M_L - T_L;
                //Delta_Measured_TargetA = M_a - T_a;
                //Delta_Measured_TargetB = M_b - T_b;

                //Delta_Measured_PredictedL = M_L - P_L;
                //Delta_Measured_PredictedA = M_a - P_a;
                //Delta_Measured_PredictedB = M_b - P_b;

                delta_Predicted_Target_L = P_L - T_L;
                delta_Measured_Target_L = M_L - T_L;

                delta_Predicted_Target_A = P_a - T_a;
                delta_Measured_Target_A = M_a - T_a;

                delta_Predicted_Target_B = P_b - T_b;
                delta_Measured_Target_B = M_b - T_b;

                lblTargetL.Text = string.Format("{0:0.00}", T_L);
                lblTargetA.Text = string.Format("{0:0.00}", T_a);
                lblTargetB.Text = string.Format("{0:0.00}", T_b);

                lblActualL.Text = string.Format("{0:0.00}", P_L);
                lblActualA.Text = string.Format("{0:0.00}", P_a);
                lblActualB.Text = string.Format("{0:0.00}", P_b);

                lblMeasuredL.Text = string.Format("{0:0.00}", M_L);
                lblMeasuredA.Text = string.Format("{0:0.00}", M_a);
                lblMeasuredB.Text = string.Format("{0:0.00}", M_b);

                lblMeasuredL.Text = string.Format("{0:0.00}", M_L);
                lblMeasuredA.Text = string.Format("{0:0.00}", M_a);
                lblMeasuredB.Text = string.Format("{0:0.00}", M_b);





                lblPredicted_Target_L.Text = getDecimalSign(delta_Predicted_Target_L) + string.Format("{0:0.00}", Math.Abs(delta_Predicted_Target_L));
                lblMeasured_Target_L.Text = getDecimalSign(delta_Measured_Target_L) + string.Format("{0:0.00}", Math.Abs(delta_Measured_Target_L));

                lblPredicted_Target_a.Text = getDecimalSign(delta_Predicted_Target_A) + string.Format("{0:0.00}", Math.Abs(delta_Predicted_Target_A));
                lblMeasured_Target_a.Text = getDecimalSign(delta_Measured_Target_A) + string.Format("{0:0.00}", Math.Abs(delta_Measured_Target_A));

                lblPredicted_Target_b.Text = getDecimalSign(delta_Predicted_Target_B) + string.Format("{0:0.00}", Math.Abs(delta_Predicted_Target_B));
                lblMeasured_Target_b.Text = getDecimalSign(delta_Measured_Target_B) + string.Format("{0:0.00}", Math.Abs(delta_Measured_Target_B));

                //lblTarget_PredictedL.Text = string.Format("{0:0.00}", Delta_Target_PredictedL);
                //lblTarget_PredictedA.Text = string.Format("{0:0.00}", Delta_Target_PredictedA);
                //lblTarget_PredictedB.Text = string.Format("{0:0.00}", Delta_Target_PredictedB);

                //lblMeasured_TargetL.Text = string.Format("{0:0.00}", Delta_Measured_TargetL);
                //lblMeasured_TargetA.Text = string.Format("{0:0.00}", Delta_Measured_TargetA);
                //lblMeasured_TargetB.Text = string.Format("{0:0.00}", Delta_Measured_TargetB);

                //lblMeasured_PredictedL.Text = string.Format("{0:0.00}", Delta_Measured_PredictedL);
                //lblMeasured_PredictedA.Text = string.Format("{0:0.00}", Delta_Measured_PredictedA);
                //lblMeasured_PredictedB.Text = string.Format("{0:0.00}", Delta_Measured_PredictedB);

                divActual.Style.Add("background-color", Predicated_htmlColor);
                divTarget.Style.Add("background-color", htmlColor);
                divMeasured.Style.Add("background-color", Measured_htmlColor);

                lblDeltaTargetPrediction.Text = string.Format("{0:0.00}", deltaTP);
                lblDeltaTargetMeasured.Text = string.Format("{0:0.00}", deltaMT);
                lblDeltaPredictionMeasured.Text = string.Format("{0:0.00}", deltaMP);
            }
            else                  //--------------For Optimization Run-0---------------//
            {
                //divPatchDetailsRun0.Visible = true;
                tblDetailsFirstRun.Visible = true;
                tblColorFirstRun.Visible = true;
                decimal Delta_L, Delta_a, Delta_b;
                Delta_L = T_L - P_L;
                Delta_a = T_a - P_a;
                Delta_b = T_b - P_b;

                lblTargetLRun0.Text = T_L.ToString();
                lblTargetARun0.Text = T_a.ToString();
                lblTargetBRun0.Text = T_b.ToString();

                lblActualLRun0.Text = P_L.ToString();
                lblActualARun0.Text = P_a.ToString();
                lblActualBRun0.Text = P_b.ToString();
                lblDeltaL.Text = Delta_L.ToString();
                lblDeltaA.Text = Delta_a.ToString();
                lblDeltaB.Text = Delta_b.ToString();

                lblDeltaTargetPerdictionRun0.Text = string.Format("{0:0.00}", deltaE);
                divActualRun0.Style.Add("background-color", Predicated_htmlColor);
                divTargetRun0.Style.Add("background-color", htmlColor);
            }
        }

        public string getDecimalSign(decimal number)
        {
            return number == 0 ? "" : number <= 0 ? "-" : "+";
        }

        //public void BindValuesNew()
        //{
        //    if (MeasurementID > 0) // if optimization run !=0
        //    {
        //        tblDetailsMoreRun.Visible = true;
        //        tblColorMoreRun.Visible = true;
        //        decimal Delta_Target_PredictedL, Delta_Measured_TargetL, Delta_Measured_PredictedL,
        //            Delta_Target_PredictedA, Delta_Measured_TargetA, Delta_Measured_PredictedA,
        //            Delta_Target_PredictedB, Delta_Measured_TargetB, Delta_Measured_PredictedB;

        //        Delta_Target_PredictedL = T_L - P_L;
        //        Delta_Target_PredictedA = T_a - P_a;
        //        Delta_Target_PredictedB = T_b - P_b;

        //        Delta_Measured_TargetL = M_L - T_L;
        //        Delta_Measured_TargetA = M_a - T_a;
        //        Delta_Measured_TargetB = M_b - T_b;

        //        Delta_Measured_PredictedL = M_L - P_L;
        //        Delta_Measured_PredictedA = M_a - P_a;
        //        Delta_Measured_PredictedB = M_b - P_b;

        //        lblTargetLNew.Text = T_L.ToString();
        //        lblTargetANew.Text = T_a.ToString();
        //        lblTargetBNew.Text = T_b.ToString();

        //        lblActualLNew.Text = P_L.ToString();
        //        lblActualANew.Text = P_a.ToString();
        //        lblActualBNew.Text = P_b.ToString();

        //        lblMeasuredLNew.Text = M_L.ToString();
        //        lblMeasuredANew.Text = M_a.ToString();
        //        lblMeasuredBNew.Text = M_b.ToString();

        //        lblMeasuredLNew.Text = M_L.ToString();
        //        lblMeasuredANew.Text = M_a.ToString();
        //        lblMeasuredBNew.Text = M_b.ToString();

        //        lblTarget_PredictedLNew.Text = Delta_Target_PredictedL.ToString();
        //        lblTarget_PredictedANew.Text = Delta_Target_PredictedA.ToString();
        //        lblTarget_PredictedBNew.Text = Delta_Target_PredictedB.ToString();

        //        lblMeasured_TargetLNew.Text = Delta_Measured_TargetL.ToString();
        //        lblMeasured_TargetANew.Text = Delta_Measured_TargetA.ToString();
        //        lblMeasured_TargetBNew.Text = Delta_Measured_TargetB.ToString();

        //        lblMeasured_PredictedLNew.Text = Delta_Measured_PredictedL.ToString();
        //        lblMeasured_PredictedANew.Text = Delta_Measured_PredictedA.ToString();
        //        lblMeasured_PredictedBNew.Text = Delta_Measured_PredictedB.ToString();

        //        divActualNew.Style.Add("background-color", Predicated_htmlColor);
        //        divTargetNew.Style.Add("background-color", htmlColor);
        //        divMeasuredNew.Style.Add("background-color", Measured_htmlColor);
        //    }
        //    else
        //    {
        //        tblDetailsFirstRun.Visible = true;
        //        tblColorFirstRun.Visible = true;
        //        decimal Delta_L, Delta_a, Delta_b;
        //        Delta_L = P_L - T_L;
        //        Delta_a = P_a - T_a;
        //        Delta_b = P_b - T_b;

        //        lblTargetLRun0New.Text = T_L.ToString();
        //        lblTargetARun0New.Text = T_a.ToString();
        //        lblTargetBRun0New.Text = T_b.ToString();

        //        lblActualLRun0New.Text = P_L.ToString();
        //        lblActualARun0New.Text = P_a.ToString();
        //        lblActualBRun0New.Text = P_b.ToString();
        //        lblDeltaLNew.Text = Delta_L.ToString();
        //        lblDeltaANew.Text = Delta_a.ToString();
        //        lblDeltaBNew.Text = Delta_b.ToString();

        //        divActualRun0New.Style.Add("background-color", Predicated_htmlColor);
        //        divTargetRun0New.Style.Add("background-color", htmlColor);
        //    }
        //}
        #region Fill Paper Chart
        //private void FillPaperChart(decimal Target_L, decimal Target_A, decimal Target_B, decimal Pre_L, decimal Pre_A, decimal Pre_B, decimal Measured_L, decimal Measured_A, decimal Measured_B, int measurementID)
        //{
        //    //string FileName;
        //    #region Fill Paper 1 Chart

        //    chartPaper1.Series["Red"].Points.Clear();
        //    chartPaper1.Series["Green"].Points.Clear();

        //    chartPaper1.Series["Red"].Points.AddXY(Convert.ToDouble(Target_A), Convert.ToDouble(Target_B));
        //    //chartPaper1.Series["Red"].Points.AddXY(Convert.ToDouble(0), Convert.ToDouble(0));
        //    chartPaper1.Series["Red"].ChartType = SeriesChartType.Point;
        //    chartPaper1.Series["Red"].Points[0].BorderWidth = 4;
        //    chartPaper1.Series["Red"].CustomProperties = "PolarDrawingStyle=Marker";

        //    chartPaper1.Series["Green"].Points.AddXY(Convert.ToDouble(Pre_A), Convert.ToDouble(Pre_B));
        //    //chartPaper1.Series["Green"].Points.AddXY(Convert.ToDouble(0), Convert.ToDouble(0));
        //    chartPaper1.Series["Green"].ChartType = SeriesChartType.Point;
        //    chartPaper1.Series["Green"].Points[0].BorderWidth = 2;
        //    chartPaper1.Series["Green"].CustomProperties = "PolarDrawingStyle=Marker";

        //    if (measurementID > 0)
        //    {
        //        chartPaper1.Series["Blue"].Points.AddXY(Convert.ToDouble(Measured_A), Convert.ToDouble(Measured_B));
        //        chartPaper1.Series["Blue"].ChartType = SeriesChartType.Point;
        //        chartPaper1.Series["Blue"].Points[0].BorderWidth = 2;
        //        chartPaper1.Series["Blue"].CustomProperties = "PolarDrawingStyle=Marker";
        //    }
        //    else
        //    {
        //        chartPaper1.Series["Blue"].Enabled = false;
        //    }

        //    #endregion

        //    #region Fill Paper 2 chart

        //    chartPaper2.Series["Red"].Points.Clear();
        //    chartPaper2.Series["Green"].Points.Clear();

        //    chartPaper2.Series["Red"].Points.AddXY(Convert.ToDouble(0), Convert.ToDouble(Target_L));
        //    chartPaper2.Series["Red"].ChartType = SeriesChartType.Point;
        //    chartPaper2.Series["Red"].Points[0].BorderWidth = 4;
        //    chartPaper2.Series["Red"].CustomProperties = "PolarDrawingStyle=Marker";

        //    chartPaper2.Series["Green"].Points.AddXY(Convert.ToDouble(0), Convert.ToDouble(Pre_L));
        //    chartPaper2.Series["Green"].ChartType = SeriesChartType.Point;
        //    chartPaper2.Series["Green"].Points[0].BorderWidth = 2;
        //    chartPaper2.Series["Green"].CustomProperties = "PolarDrawingStyle=Marker";

        //    if (measurementID > 0)
        //    {
        //        chartPaper2.Series["Blue"].Points.AddXY(Convert.ToDouble(0), Convert.ToDouble(Measured_L));
        //        chartPaper2.Series["Blue"].ChartType = SeriesChartType.Point;
        //        chartPaper2.Series["Blue"].Points[0].BorderWidth = 2;
        //        chartPaper2.Series["Blue"].CustomProperties = "PolarDrawingStyle=Marker";
        //    }
        //    else
        //    {
        //        chartPaper1.Series["Blue"].Enabled = false;
        //    }
        //    #endregion
        //}
    }
    #endregion
}