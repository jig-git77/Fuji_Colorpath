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
        private const string _recTarget = "targetText";
        private const string _recDeltaL = "deltaLText";
        private const string _recDeltaA = "deltaAText";
        private const string _recDeltaB = "deltaBText";
        decimal T_L, T_a, T_b, P_L, P_a, P_b;
        string htmlColor;

        protected void Page_Load(object sender, EventArgs e)
        {
            T_L = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["targetL"])) ? 0 : Convert.ToDecimal(Request.QueryString["targetL"]);
            T_a = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["targetA"])) ? 0 : Convert.ToDecimal(Request.QueryString["targetA"]);
            T_b = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["targetB"])) ? 0 : Convert.ToDecimal(Request.QueryString["targetB"]);
            P_L = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["actualL"])) ? 0 : Convert.ToDecimal(Request.QueryString["actualL"]);
            P_a = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["actualA"])) ? 0 : Convert.ToDecimal(Request.QueryString["actualA"]);
            P_b = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["actualB"])) ? 0 : Convert.ToDecimal(Request.QueryString["actualB"]);

            htmlColor = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["htmlColor"])) ? "" : ("#" + Convert.ToString(Request.QueryString["htmlColor"]));
            BindValues();
            FillPaperChart(T_L,T_a,T_b,P_L,P_a,P_b);
        }


        public void BindValues()
        {
            decimal Delta_L, Delta_a, Delta_b;
            Delta_L = P_L - T_L;
            Delta_a = P_a - T_a;
            Delta_b = P_b - T_b;

            lblTargetL.Text = T_L.ToString();
            lblTargetA.Text = T_a.ToString();
            lblTargetB.Text = T_b.ToString();

            lblActualL.Text = P_L.ToString();
            lblActualA.Text = P_a.ToString();
            lblActualB.Text = P_b.ToString();
            lblDeltaL.Text = Delta_L.ToString();
            lblDeltaA.Text = Delta_a.ToString();
            lblDeltaB.Text = Delta_b.ToString();



            divActual.Style.Add("background-color", htmlColor);
            divTarget.Style.Add("background-color", htmlColor);

            //FillPaperChart(Delta_L, Delta_a, Delta_b);
        }

        #region Fill Paper Chart
        private void FillPaperChart(decimal Target_L, decimal Target_A, decimal Target_B, decimal Pre_L, decimal Pre_A, decimal Pre_B)
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
            
            // FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(chartPaper1.ImageLocation) + "." + chartPaper1.ImageType;
            // chartPaper1.SaveImage(Server.MapPath(FileName));
            #endregion

            #region Fill Paper 2 chart

            chartPaper2.Series["Red"].Points.Clear();
            chartPaper2.Series["Green"].Points.Clear();

            chartPaper2.Series["Red"].Points.AddXY(Convert.ToDouble(0), Convert.ToDouble(Target_L));
            //chartPaper2.Series["Red"].Points.AddXY(Convert.ToDouble(0), Convert.ToDouble(0));
            chartPaper2.Series["Red"].ChartType = SeriesChartType.Point;
            chartPaper2.Series["Red"].Points[0].BorderWidth = 4;
            chartPaper2.Series["Red"].CustomProperties = "PolarDrawingStyle=Marker";

            chartPaper2.Series["Green"].Points.AddXY(Convert.ToDouble(0), Convert.ToDouble(Pre_L));
            //chartPaper2.Series["Green"].Points.AddXY(Convert.ToDouble(0), Convert.ToDouble(0));
            chartPaper2.Series["Green"].ChartType = SeriesChartType.Point;
            chartPaper2.Series["Green"].Points[0].BorderWidth = 2;
            chartPaper2.Series["Green"].CustomProperties = "PolarDrawingStyle=Marker";
            

            //FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(chartPaper2.ImageLocation) + "." + chartPaper2.ImageType;
            //chartPaper2.SaveImage(Server.MapPath(FileName));
            #endregion
        }
    }
    #endregion
}