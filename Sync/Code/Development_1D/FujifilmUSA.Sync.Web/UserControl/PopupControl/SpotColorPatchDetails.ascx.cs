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
        decimal T_L, T_a, T_b, A_L, A_a, A_b;
        string htmlColor;

        protected void Page_Load(object sender, EventArgs e)
        {
            T_L = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["targetL"])) ? 0 : Convert.ToDecimal(Request.QueryString["targetL"]);
            T_a = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["targetA"])) ? 0 : Convert.ToDecimal(Request.QueryString["targetA"]);
            T_b = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["targetB"])) ? 0 : Convert.ToDecimal(Request.QueryString["targetB"]);
            A_L = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["actualL"])) ? 0 : Convert.ToDecimal(Request.QueryString["actualL"]);
            A_a = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["actualA"])) ? 0 : Convert.ToDecimal(Request.QueryString["actualA"]);
            A_b = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["actualB"])) ? 0 : Convert.ToDecimal(Request.QueryString["actualB"]);

            htmlColor = string.IsNullOrEmpty(Convert.ToString(Request.QueryString["htmlColor"])) ? "" : ("#" + Convert.ToString(Request.QueryString["htmlColor"]));
            BindValues();
        }


        public void BindValues()
        {
            decimal Delta_L, Delta_a, Delta_b;
            Delta_L = A_L - T_L;
            Delta_a = A_a - T_a;
            Delta_b = A_b - T_b;

            lblTargetL.Text = T_L.ToString();
            lblTargetA.Text = T_a.ToString();
            lblTargetB.Text = T_b.ToString();

            lblActualL.Text = A_L.ToString();
            lblActualA.Text = A_a.ToString();
            lblActualB.Text = A_b.ToString();
            lblDeltaL.Text = Delta_L.ToString();
            lblDeltaA.Text = Delta_a.ToString();
            lblDeltaB.Text = Delta_b.ToString();

            //lblTargetH.Text = "---";
            //lblTargetC.Text = "---";
            //lblTargetPC.Text = "---";
            //lblActualH.Text = "---";
            //lblActualC.Text = "---";
            //lblActualPC.Text = "---";


            //lblDeltaH.Text = "---";
            //lblDeltaC.Text = "---";
            //lblDeltaPC.Text = "---";

            divActual.Style.Add("background-color", htmlColor);
            divTarget.Style.Add("background-color", htmlColor);

            FillPaperChart(Delta_L, Delta_a, Delta_b);
        }

        #region Fill Paper Chart
        private void FillPaperChart(decimal deltaL, decimal deltaA, decimal deltaB)
        {
            string FileName;
            #region Fill Paper 1 Chart

            chartPaper1.Series["Green"].Points.Clear();
            chartPaper1.Series["Paper"].Points.Clear();

            chartPaper1.Series["Green"].Points.AddXY(Convert.ToDouble(0), Convert.ToDouble(0));
            chartPaper1.Series["Green"].ChartType = SeriesChartType.Point;
            chartPaper1.Series["Green"].Points[0].BorderWidth = 2;
            chartPaper1.Series["Green"].CustomProperties = "PolarDrawingStyle=Marker";

            StringBuilder toolTip = new StringBuilder();

            //#region Add Tooltip HTML
            toolTip.Append("<table>");
            toolTip.Append("<tr><td><b>" + "Paper Match Matrics" + "</b></td></tr>"); //Paper Patch Metrics
            toolTip.Append("<tr><td><table>");
            toolTip.Append("<tr><td>" + "Actual" + "</td><td>"); //Actual LAB
            toolTip.Append("L*" + A_L.ToString());
            toolTip.Append(" " + "a*" + A_a.ToString());
            toolTip.Append(" " + "b*" + A_b.ToString());
            toolTip.Append("</td></tr>");
            toolTip.Append("<tr><td>" + "Target" + "</td><td>"); //Target LAB
            toolTip.Append("L*" + T_L.ToString());
            toolTip.Append(" " + "a*" + T_a.ToString());
            toolTip.Append(" " + "b*" + T_b.ToString());
            toolTip.Append("</td></tr>");
            toolTip.Append("<tr><td>" + "ΔL*" + "</td><td>"); //ΔL*
            toolTip.Append(deltaL.ToString());
            toolTip.Append("</td></tr>");
            toolTip.Append("<tr><td>" + "Δa*" + "</td><td>"); //Δa*
            toolTip.Append(deltaA.ToString());
            toolTip.Append("</td></tr>");
            toolTip.Append("<tr><td>" + "Δb*" + "</td><td>"); //Δb*
            toolTip.Append(deltaB.ToString());
            toolTip.Append("</td></tr>");
            toolTip.Append("</table></td></tr>");
            toolTip.Append("</table>");
            //#endregion

            Double Paper_Δa = Convert.ToDouble(deltaA) == 0 ? 0.01 : Convert.ToDouble(deltaA); //-------If Δa = 0, then it makes problem in display chart, so just temporary assign 0.01 for negligible difference
            chartPaper1.Series["Paper"].Points.AddXY(Paper_Δa, Convert.ToDouble(deltaB));
            chartPaper1.Series["Paper"].ChartType = SeriesChartType.Point;
            chartPaper1.Series["Paper"].Points[0].BorderWidth = 4;
            chartPaper1.Series["Paper"].CustomProperties = "PolarDrawingStyle=Marker";
            //chartPaper1.Series["Paper"].Points[0].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";


            FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(chartPaper1.ImageLocation) + "." + chartPaper1.ImageType;
            chartPaper1.SaveImage(Server.MapPath(FileName));
            #endregion

            #region Fill Paper 2 chart

            chartPaper2.Series["Series1"].Points.Clear();
            chartPaper2.Series["Series2"].Points.Clear();

            #region Line Chart Series For Dispaly Line on 0 value
            chartPaper2.Series["Series2"].ChartType = SeriesChartType.Line;
            chartPaper2.Series["Series2"].XAxisType = AxisType.Secondary;
            chartPaper2.Series["Series2"].Points.AddXY(0, 0);
            chartPaper2.Series["Series2"].Points.AddXY(1, 0);
            #endregion

            chartPaper2.Series["Series1"].Points.AddY(Convert.ToDouble(deltaL));
            chartPaper2.Series["Series1"].Points[0].Color = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
            chartPaper2.Series["Series1"].CustomProperties = "DrawingStyle=Cylinder";
            //chartPaper2.Series["Series1"].Points[0].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";


            FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(chartPaper2.ImageLocation) + "." + chartPaper2.ImageType;
            chartPaper2.SaveImage(Server.MapPath(FileName));
            #endregion
        }
    }
    #endregion
}