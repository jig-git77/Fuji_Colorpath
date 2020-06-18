#region 
using Dundas.Charting.WebControl;
using System;
using System.Data;
using Taskero.Business;
using Taskero.Common;
using TaskeroWeb.Models;
#endregion

namespace TaskeroWeb
{
    public partial class ColorPopUpGuamat : PageBase
    {
        #region Declaration

        private enum enumColorChartsTable
        {
            L_AB_25 = 0, L_AB_50 = 1, L_AB_75 = 2, Gamut = 3, Density = 4, TVI = 5, PrintContrast = 6
        }

        #endregion

        #region Page Load and Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //========== Clear Error ==========//
                LblError.Text = "";

                //========== Default Settings ==========//
                if (IsPostBack == false)
                {
                    //========== Set Keys ==========//
                    LTRL_KeyColorChars.Text = Convert.ToString(Request.QueryString["CPMeasurementGUID"]) + "-Color-DSet-ColorChars";

                    //========== Fill Charts ==========//
                    FillCharts();
                }
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        private void FillCharts()
        {
            try
            {
                //========== Get The Delta E Data ==========//        
                if (Sessions.GetSession(LTRL_KeyColorChars.Text) == null)
                {
                    CPPatchDataLabData objColorCharts = new CPPatchDataLabData();
                    objColorCharts.MeasurementGUID = Convert.ToString(Request.QueryString["CPMeasurementGUID"]).Trim();
                    Sessions.SetSessions(LTRL_KeyColorChars.Text, objColorCharts.uspGetDTLColorChartsByGUID());
                }

                DataSet DS_ColorCharts = (DataSet)Sessions.GetSession(LTRL_KeyColorChars.Text);

                #region For Gamut

                if (DS_ColorCharts.Tables.Count > Convert.ToInt32(enumColorChartsTable.Gamut) && DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.Gamut)].Rows.Count > 0)
                {
                    //========== Bind GAMUT CHART ==========//
                    Bind_GAMUT_CHART(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.Gamut)]);
                }

                #endregion
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        private void Bind_GAMUT_CHART(DataTable dtGamut)
        {
            try
            {
                #region Old Plotting
                /*
                for (int j = 0; j < dtGamut.Rows.Count; j++)
                {
                    #region For Plotting other colors
                    int i = j;
                    if (i == (dtGamut.Rows.Count - 1))
                    { i = 0; }

                    CHART_GAMUT.Series[0].Points.AddXY(Convert.ToDouble(dtGamut.Rows[i]["LAB_A1"].ToString()), Convert.ToDouble(dtGamut.Rows[i]["LAB_B1"].ToString()));
                    CHART_GAMUT.Series[0].Type = SeriesChartType.Line;
                    CHART_GAMUT.Series[0].Points[i].ToolTip = "Color : " + dtGamut.Rows[i]["ColorType"].ToString() + ", Target(a) : " + dtGamut.Rows[i]["LAB_A1"].ToString() + ", Target(b) : " + dtGamut.Rows[i]["LAB_B1"].ToString();
                    CHART_GAMUT.Series[0].Points[i].Href = "#";

                    CHART_GAMUT.Series[1].Points.AddXY(Convert.ToDouble(dtGamut.Rows[i]["LAB_A2"].ToString()), Convert.ToDouble(dtGamut.Rows[i]["LAB_B2"].ToString()));
                    CHART_GAMUT.Series[1].Type = SeriesChartType.Line;
                    CHART_GAMUT.Series[1].Points[i].ToolTip = "Color : " + dtGamut.Rows[i]["ColorType"].ToString() + ", Actual(a) : " + dtGamut.Rows[i]["LAB_A2"].ToString() + " , Actual(b) : " + dtGamut.Rows[i]["LAB_B2"].ToString();
                    CHART_GAMUT.Series[1].Points[i].Href = "#";
                    #endregion

                    #region For Plotting paper white
                    if (j == 6)
                    {
                        i = j;

                        CHART_GAMUT.Series[2].Points.AddXY(Convert.ToDouble(dtGamut.Rows[i]["LAB_A1"].ToString()), Convert.ToDouble(dtGamut.Rows[i]["LAB_B1"].ToString()));
                        CHART_GAMUT.Series[2].Type = SeriesChartType.FastPoint;
                        CHART_GAMUT.Series[2].Points[0].ToolTip = "Color : " + dtGamut.Rows[i]["ColorType"].ToString() + ", Target(a) : " + dtGamut.Rows[i]["LAB_A1"].ToString() + ", Target(b) : " + dtGamut.Rows[i]["LAB_B1"].ToString();
                        CHART_GAMUT.Series[2].Points[0].Href = "#";

                        CHART_GAMUT.Series[3].Points.AddXY(Convert.ToDouble(dtGamut.Rows[i]["LAB_A2"].ToString()), Convert.ToDouble(dtGamut.Rows[i]["LAB_B2"].ToString()));
                        CHART_GAMUT.Series[3].Type = SeriesChartType.FastPoint;
                        CHART_GAMUT.Series[3].Points[0].ToolTip = "Color : " + dtGamut.Rows[i]["ColorType"].ToString() + ", Actual(a) : " + dtGamut.Rows[i]["LAB_A2"].ToString() + ", Actual(b) : " + dtGamut.Rows[i]["LAB_B2"].ToString();
                        CHART_GAMUT.Series[3].Points[0].Href = "#";
                    }
                    #endregion
                }
                 */
                #endregion

                #region Plotting the Chart
                dtGamut.DefaultView.RowFilter = "(CMYK_C = 0 AND CMYK_M = 0  AND CMYK_Y = 0  AND CMYK_K = 0)";
                DataView DV = dtGamut.DefaultView;
                if (DV.Count > 0)
                {
                    int i = 0;
                    CHART_GAMUT.Series[2].Points.AddXY(Convert.ToDouble(DV[0]["LAB_A1"]), Convert.ToDouble(DV[i]["LAB_B1"]));
                    CHART_GAMUT.Series[2].Type = SeriesChartType.FastPoint;
                    CHART_GAMUT.Series[2].Points[0].ToolTip = "Color : " + DV[i]["ColorType"].ToString() + ", Target(a) : " + DV[i]["LAB_A1"].ToString() + ", Target(b) : " + DV[i]["LAB_B1"].ToString();
                    CHART_GAMUT.Series[2].Points[0].Href = "#";

                    CHART_GAMUT.Series[3].Points.AddXY(Convert.ToDouble(DV[i]["LAB_A2"]), Convert.ToDouble(DV[i]["LAB_B2"]));
                    CHART_GAMUT.Series[3].Type = SeriesChartType.FastPoint;
                    CHART_GAMUT.Series[3].Points[0].ToolTip = "Color : " + DV[i]["ColorType"].ToString() + ", Actual(a) : " + DV[i]["LAB_A2"].ToString() + ", Actual(b) : " + DV[i]["LAB_B2"].ToString();
                    CHART_GAMUT.Series[3].Points[0].Href = "#";
                }
                #endregion

                #region Other Points
                dtGamut.DefaultView.RowFilter = "(ColorType <> 'WHITE') AND (LAB_A1 <> 0 AND LAB_B1 <> 0 AND LAB_A2 <> 0 AND LAB_B2 <> 0)";
                DV = dtGamut.DefaultView;
                for (int i = 0; i < DV.Count; i = i + 1)
                {
                    CHART_GAMUT.Series[0].Points.AddXY(Convert.ToDouble(DV[i]["LAB_A1"]), Convert.ToDouble(DV[i]["LAB_B1"]));
                    CHART_GAMUT.Series[0].Type = SeriesChartType.Line;
                    CHART_GAMUT.Series[0].Points[i].ToolTip = "Color : " + DV[i]["ColorType"].ToString() + ", Target(a) : " + Convert.ToDouble(DV[i]["LAB_A1"]).ToString("N2") + ", Target(b) : " + Convert.ToDouble(DV[i]["LAB_B1"]).ToString("N2");
                    CHART_GAMUT.Series[0].Points[i].Href = "#";

                    CHART_GAMUT.Series[1].Points.AddXY(Convert.ToDouble(DV[i]["LAB_A2"]), Convert.ToDouble(DV[i]["LAB_B2"]));
                    CHART_GAMUT.Series[1].Type = SeriesChartType.Line;
                    CHART_GAMUT.Series[1].Points[i].ToolTip = "Color : " + DV[i]["ColorType"].ToString() + ", Actual(a) : " + Convert.ToDouble(DV[i]["LAB_A2"]).ToString("N2") + ", Actual(b) : " + Convert.ToDouble(DV[i]["LAB_B2"]).ToString("N2");
                    CHART_GAMUT.Series[1].Points[i].Href = "#";
                }
                if (DV.Count > 1)
                {
                    int i = 0;
                    CHART_GAMUT.Series[0].Points.AddXY(Convert.ToDouble(DV[i]["LAB_A1"]), Convert.ToDouble(DV[i]["LAB_B1"]));
                    CHART_GAMUT.Series[0].Type = SeriesChartType.Line;
                    CHART_GAMUT.Series[0].Points[DV.Count].ToolTip = "Color : " + DV[i]["ColorType"].ToString() + ", Target(a) : " + Convert.ToDouble(DV[i]["LAB_A1"]).ToString("N2") + ", Target(b) : " + Convert.ToDouble(DV[i]["LAB_B1"]).ToString("N2");
                    CHART_GAMUT.Series[0].Points[DV.Count].Href = "#";

                    CHART_GAMUT.Series[1].Points.AddXY(Convert.ToDouble(DV[i]["LAB_A2"]), Convert.ToDouble(DV[i]["LAB_B2"]));
                    CHART_GAMUT.Series[1].Type = SeriesChartType.Line;
                    CHART_GAMUT.Series[1].Points[DV.Count].ToolTip = "Color : " + DV[i]["ColorType"].ToString() + ", Actual(a) : " + Convert.ToDouble(DV[i]["LAB_A2"]).ToString("N2") + ", Actual(b) : " + Convert.ToDouble(DV[i]["LAB_B2"]).ToString("N2");
                    CHART_GAMUT.Series[1].Points[DV.Count].Href = "#";
                }
                #endregion
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        #endregion
    }

}