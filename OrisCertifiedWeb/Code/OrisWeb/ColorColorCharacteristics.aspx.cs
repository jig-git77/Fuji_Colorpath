using Dundas.Charting.WebControl;
using Oris.Business;
using Oris.Common;
using OrisWeb.Models;
using System;
using System.Data;
using System.Drawing;

namespace OrisWeb
{
    public partial class ColorColorCharacteristics : PageBase
    {
        #region Declaration
        private enum enumColorChartsTable
        {
            L_AB_25 = 0, L_AB_50 = 1, L_AB_75 = 2, Gamut = 3, Density = 4, PrintContrast = 6
        }
        #endregion

        #region Page Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //========== Clear Error ==========//
                LblError.Text = "";

                //========== Default Settings ==========//
                if (IsPostBack == false)
                {
                    //========== Fill From Request Querystring ==========//
                    FillLabels();

                    //========== Set Key ==========//
                    LTRL_KeyColorChars.Text = LTRL_CPMeasurementGUID.Text + "-Color-DSet-ColorChars";
                    LTRL_KeyColorCharsDotgain.Text = LTRL_CPMeasurementGUID.Text + "-Color-DSet-ColorCharsDotgain";

                    //========== Clear the Sessions ==========//
                    Sessions.SetSessions(LTRL_KeyColorChars.Text, null);
                    Sessions.SetSessions(LTRL_KeyColorCharsDotgain.Text, null);

                    //========== Fill Charts ==========//
                    FillCharts();

                }

            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        protected void CHK_TVI_CheckedChanged(object sender, EventArgs e)
        {
            //========== Fills All Charts ==========//
            FillCharts();
        }
        #endregion

        #region User Functions
        private void FillLabels()
        {
            try
            {
                LTRL_CPDeviceID.Text = Convert.ToString(Request.QueryString["CPDeviceID"]);
                LTRL_CPDevicePrintTargetID.Text = Convert.ToString(Request.QueryString["CPDevicePrintTargetID"]);
                LTRL_CPMeasurementGUID.Text = Convert.ToString(Request.QueryString["CPMeasurementGUID"]);
            }
            catch (Exception Ex)
            {
                throw Ex;
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
                    objColorCharts.MeasurementGUID = LTRL_CPMeasurementGUID.Text.Trim();
                    Sessions.SetSessions(LTRL_KeyColorChars.Text, objColorCharts.uspGetDTLColorChartsByGUID());
                }
                DataSet DS_ColorCharts = (DataSet)Sessions.GetSession(LTRL_KeyColorChars.Text);

                #region For Gamut

                if (DS_ColorCharts.Tables.Count > Convert.ToInt32(enumColorChartsTable.Gamut) && DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.Gamut)].Rows.Count > 0)
                {
                    Bind_GAMUT_CHART(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.Gamut)]);
                }

                #endregion

                #region For AB & L 25%

                if (DS_ColorCharts.Tables.Count > Convert.ToInt32(enumColorChartsTable.L_AB_25) && DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_25)].Rows.Count > 0)
                {
                    Bind_AB_CHART(CHART_AB25, Convert.ToDouble(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_25)].Rows[0]["LAB_A1"].ToString()),
                                              Convert.ToDouble(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_25)].Rows[0]["LAB_B1"].ToString()),
                                              Convert.ToDouble(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_25)].Rows[0]["LAB_A2"].ToString()),
                                              Convert.ToDouble(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_25)].Rows[0]["LAB_B2"].ToString()));

                    Bind_L_CHART(CHART_L25, Convert.ToDouble(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_25)].Rows[0]["LAB_L1"].ToString()),
                                            Convert.ToDouble(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_25)].Rows[0]["LAB_L2"].ToString()));
                }
                else
                {
                    Bind_AB_CHART(CHART_AB25, 0, 0, 0, 0);
                    Bind_L_CHART(CHART_L25, 0, 0);
                }

                #endregion

                #region For AB & L 50%

                if (DS_ColorCharts.Tables.Count > Convert.ToInt32(enumColorChartsTable.L_AB_50) && DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_50)].Rows.Count > 0)
                {
                    Bind_AB_CHART(CHART_AB50, Convert.ToDouble(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_50)].Rows[0]["LAB_A1"].ToString()),
                                              Convert.ToDouble(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_50)].Rows[0]["LAB_B1"].ToString()),
                                              Convert.ToDouble(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_50)].Rows[0]["LAB_A2"].ToString()),
                                              Convert.ToDouble(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_50)].Rows[0]["LAB_B2"].ToString()));

                    Bind_L_CHART(CHART_L50, Convert.ToDouble(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_50)].Rows[0]["LAB_L1"].ToString()),
                                            Convert.ToDouble(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_50)].Rows[0]["LAB_L2"].ToString()));
                }
                else
                {
                    Bind_AB_CHART(CHART_AB50, 0, 0, 0, 0);
                    Bind_L_CHART(CHART_L50, 0, 0);
                }

                #endregion

                #region For AB & L 75%

                if (DS_ColorCharts.Tables.Count > Convert.ToInt32(enumColorChartsTable.L_AB_75) && DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_75)].Rows.Count > 0)
                {
                    Bind_AB_CHART(CHART_AB75, Convert.ToDouble(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_75)].Rows[0]["LAB_A1"].ToString()),
                                              Convert.ToDouble(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_75)].Rows[0]["LAB_B1"].ToString()),
                                              Convert.ToDouble(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_75)].Rows[0]["LAB_A2"].ToString()),
                                              Convert.ToDouble(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_75)].Rows[0]["LAB_B2"].ToString()));

                    Bind_L_CHART(CHART_L75, Convert.ToDouble(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_75)].Rows[0]["LAB_L1"].ToString()),
                                            Convert.ToDouble(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.L_AB_75)].Rows[0]["LAB_L2"].ToString()));
                }
                else
                {
                    Bind_AB_CHART(CHART_AB75, 0, 0, 0, 0);
                    Bind_L_CHART(CHART_L75, 0, 0);
                }

                #endregion

                #region For Density

                if (DS_ColorCharts.Tables.Count > Convert.ToInt32(enumColorChartsTable.Density) && DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.Density)].Rows.Count > 0)
                {
                    Bind_Density_CHART(DS_ColorCharts.Tables[Convert.ToInt32(enumColorChartsTable.Density)]);
                }

                #endregion

                #region For Print Contrast
                int _IsPrintDtailShow = (int)Enums.enumYesNo.Yes;
                _IsPrintDtailShow = Bind_PrintContrast_CHART(DS_ColorCharts);
                if (_IsPrintDtailShow == (int)Enums.enumYesNo.No)
                { TBLPrintDetails.Visible = false; }
                #endregion

                #region Dotgain
                FillDotgain();
                #endregion
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }
        }
        #endregion

        #region For Gaumat Chart
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
            { LblError.Text = HandleException(Exc); }

        }
        #endregion

        #region For ab 25% Chart, ab 50% Chart, ab 75% Chart
        private void Bind_AB_CHART(Dundas.Charting.WebControl.Chart Bind_AB_CHART, Double A1, Double B1, Double A2, Double B2)
        {
            try
            {
                Bind_AB_CHART.Series[0].ToolTip = "Target(a) : " + A1 + " | Target(b) : " + B1;
                Bind_AB_CHART.Series[1].ToolTip = "Actual(a) : " + A2 + " | Actual(b) : " + B2;

                A1 = (A1 * 100) / 10;
                B1 = (B1 * 100) / 10;
                A2 = (A2 * 100) / 10;
                B2 = (B2 * 100) / 10;

                Bind_AB_CHART.Series[0].Points.AddXY(A1, B1);
                Bind_AB_CHART.Series[0].Type = SeriesChartType.Point;
                Bind_AB_CHART.Series[0].Href = "#";

                Bind_AB_CHART.Series[1].Points.AddXY(A2, B2);
                Bind_AB_CHART.Series[1].Type = SeriesChartType.Point;
                Bind_AB_CHART.Series[1].Href = "#";
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region For L25% Chart, L50% Chart, L75% Chart
        private void Bind_L_CHART(Dundas.Charting.WebControl.Chart Bind_L_CHART, Double L1, Double L2)
        {
            try
            {
                //====  Static Dimond Top & Buttom
                Bind_L_CHART.Series[0].Type = SeriesChartType.Point;
                Bind_L_CHART.Series[0].Points.AddXY(1, 0);

                Bind_L_CHART.Series[1].Type = SeriesChartType.Point;
                Bind_L_CHART.Series[1].Points.AddXY(1, 100);
                //============================================

                //====  L1
                Bind_L_CHART.Series[2].Type = SeriesChartType.Point;
                Bind_L_CHART.Series[2].Points.AddXY(1, L1);

                //====  L2
                Bind_L_CHART.Series[3].Type = SeriesChartType.Point;
                Bind_L_CHART.Series[3].Points.AddXY(1, L2);

                //==== Tooltip  
                Bind_L_CHART.Series[2].ToolTip = "Target(L) : " + L1;
                Bind_L_CHART.Series[3].ToolTip = "Actual(L) : " + L2;

                Bind_L_CHART.Series[2].Href = "#";
                Bind_L_CHART.Series[3].Href = "#";
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Dotgain Or TVI
        private void FillDotgain()
        {
            try
            {
                //========== Get The Delta E Data ==========//        
                if (Sessions.GetSession(LTRL_KeyColorCharsDotgain.Text) == null)
                {
                    CPPatchDataLabData objColorCharts = new CPPatchDataLabData();
                    objColorCharts.MeasurementGUID = LTRL_CPMeasurementGUID.Text.Trim();
                    Sessions.SetSessions(LTRL_KeyColorCharsDotgain.Text, objColorCharts.uspGetColorCharDotGain());
                }
                DataSet DSet = (DataSet)Sessions.GetSession(LTRL_KeyColorCharsDotgain.Text);

                //========== Get The Dotgain Data ==========//        
                for (int i = 0; i < DSet.Tables.Count; i = i + 1)
                {
                    //========== Show Hide ==========//
                    if (i == 0 && CHK_TVI_C.Checked == false) { continue; }
                    if (i == 1 && CHK_TVI_M.Checked == false) { continue; }
                    if (i == 2 && CHK_TVI_Y.Checked == false) { continue; }
                    if (i == 3 && CHK_TVI_K.Checked == false) { continue; }

                    //========== Loops to Plot the series ==========//
                    for (int j = 0; j < DSet.Tables[i].Rows.Count; j = j + 1)
                    {
                        //========== Get The series ==========//
                        Series objSeries = (Series)CHART_TVI.Series[i + 1];

                        //========== Plot the series ==========//
                        string _ColorName = DSet.Tables[i].Rows[j]["ColorName"].ToString();
                        string _CMYK = Convert.ToInt32(DSet.Tables[i].Rows[j]["CMYK"]).ToString() + "%";
                        string _DotGain = DSet.Tables[i].Rows[j]["DotGain"].ToString();

                        if (Convert.ToDecimal(_DotGain) > 25)
                        { objSeries.Points.AddXY(_CMYK, "25"); }
                        else if (Convert.ToDecimal(_DotGain) < -25)
                        { objSeries.Points.AddXY(_CMYK, "-25"); }
                        else
                        { objSeries.Points.AddXY(_CMYK, _DotGain); }

                        objSeries.Points[j].ToolTip = " Color : " + _ColorName + ", CMKY : " + _CMYK + ", Dotgain : " + Convert.ToDecimal(_DotGain).ToString("#.##");
                        objSeries.Points[j].Href = "#";
                    }
                }

                if (
                    (
                        CHK_TVI_C.Checked == false
                        && CHK_TVI_M.Checked == false
                        && CHK_TVI_Y.Checked == false
                        && CHK_TVI_K.Checked == false
                    )
                    ||
                    (
                        DSet.Tables[0].Rows.Count == 0
                        && DSet.Tables[1].Rows.Count == 0
                        && DSet.Tables[2].Rows.Count == 0
                        && DSet.Tables[3].Rows.Count == 0
                    )
                    )
                {
                    Series objSeries = (Series)CHART_TVI.Series[0];
                    objSeries.Points.AddXY("25%", "0");
                    objSeries.Points.AddXY("50%", "0");
                    objSeries.Points.AddXY("75%", "0");
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region For Density Chart
        private void Bind_Density_CHART(DataTable dtDensity)
        {
            try
            {
                for (int i = 0; i < dtDensity.Rows.Count; i++)
                {
                    CHART_Density.Series[0].Points.AddXY(dtDensity.Rows[i]["ColorType"].ToString(), Convert.ToDouble(dtDensity.Rows[i]["DENSITY"].ToString()));
                    CHART_Density.Series[0].Points[i].Color = Color.FromName(dtDensity.Rows[i]["ColorType"].ToString());
                    CHART_Density.Series[0].Points[i].ToolTip = "Density : " + dtDensity.Rows[i]["DENSITY"].ToString();
                    CHART_Density.Series[0].Points[i].Href = "#";
                }
            }
            catch (Exception Exc)
            { LblError.Text = HandleException(Exc); }

        }
        #endregion

        #region For Print Contrast Chart
        private int Bind_PrintContrast_CHART(DataSet dsPrintContrast)
        {
            try
            {
                if (dsPrintContrast.Tables.Count > Convert.ToInt32(enumColorChartsTable.PrintContrast))
                {
                    decimal _D100;
                    decimal _D75;
                    decimal _Print_Contrast;
                    decimal _Print_Contrast_TH;

                    //========== Get Value ==========//                
                    decimal _Deviation;
                    decimal _Max;
                    decimal _Min;
                    string _Color;

                    #region Series Declaration
                    Series objSeries0 = (Series)CHART_PrintContrast.Series[0];
                    objSeries0.XValueType = ChartValueTypes.Double;
                    objSeries0.YValueType = ChartValueTypes.String;
                    objSeries0.ShowInLegend = false;
                    objSeries0.Type = SeriesChartType.Column;

                    Series objSeries1 = (Series)CHART_PrintContrast.Series[1];
                    objSeries1.XValueType = ChartValueTypes.Double;
                    objSeries1.YValueType = ChartValueTypes.String;
                    objSeries1.ShowInLegend = false;
                    objSeries1.Type = SeriesChartType.StackedColumn;

                    Series objSeries2 = (Series)CHART_PrintContrast.Series[2];
                    objSeries2.XValueType = ChartValueTypes.Double;
                    objSeries2.YValueType = ChartValueTypes.String;
                    objSeries2.ShowInLegend = false;
                    objSeries2.Type = SeriesChartType.Point;

                    Series objSeries3 = (Series)CHART_PrintContrast.Series[3];
                    objSeries3.XValueType = ChartValueTypes.Double;
                    objSeries3.YValueType = ChartValueTypes.String;
                    objSeries3.ShowInLegend = false;
                    objSeries3.Type = SeriesChartType.Point;

                    #endregion

                    #region " === Fill Chart === "
                    for (int i = 5, j = 0; i < dsPrintContrast.Tables.Count; i++, j++)
                    {
                        //========== Calcualations and Variable Declarations ==========//
                        _D100 = Convert.ToDecimal(dsPrintContrast.Tables[i].Rows[0]["D100"].ToString());
                        _D75 = Convert.ToDecimal(dsPrintContrast.Tables[i].Rows[0]["D75"].ToString());
                        _Print_Contrast = Convert.ToDecimal(dsPrintContrast.Tables[i].Rows[0]["Print_Contrast"].ToString());
                        _Print_Contrast_TH = Convert.ToDecimal(dsPrintContrast.Tables[i].Rows[0]["Print_Contrast_TH"].ToString());

                        //========== Get Value ==========//
                        _Deviation = 0;
                        if (_D100 == 0 && _D75 == 0)
                        { _Deviation = -1; }
                        else
                        { _Deviation = ((_D100 - _D75) / _D100) * 100; }

                        if (_Deviation <= 0)
                        { return (int)Enums.enumYesNo.No; }

                        _Max = _Print_Contrast + _Print_Contrast_TH;
                        _Min = _Print_Contrast - _Print_Contrast_TH;
                        _Color = dsPrintContrast.Tables[i].Rows[0]["Color"].ToString();

                        objSeries0.Points.AddXY(_Color, _Max);
                        //objSeries0.Points[j].Color = Color.FromArgb(255, R1, G1, B1);
                        objSeries0.Points[j]["PointWidth"] = "0.4";
                        objSeries0.Points[j].ToolTip = " Print Contrast : " + _Deviation.ToString("#0.00") + ", Min : " + _Min + ", Max: " + _Max;
                        objSeries0.Points[j].Href = "#";

                        objSeries1.Points.AddXY(_Color, _Deviation);
                        objSeries1.Points[j].Color = Color.FromName(dsPrintContrast.Tables[i].Rows[0]["Color"].ToString());
                        objSeries1.Points[j]["PointWidth"] = "0.4";
                        objSeries1.Points[j].ToolTip = " Print Contrast : " + _Deviation.ToString("#0.00") + ", Min : " + _Min + ", Max: " + _Max;
                        objSeries1.Points[j].Href = "#";

                        objSeries2.Points.AddXY(_Color, _Max);
                        objSeries2.Points[j].Color = Color.Transparent;
                        objSeries2.Points[j]["PointWidth"] = "0.4";
                        objSeries2.Points[j].ToolTip = " Print Contrast : " + _Deviation.ToString("#0.00") + ", Min : " + _Min + ", Max: " + _Max;
                        objSeries2.Points[j].Href = "#";

                        objSeries3.Points.AddXY(_Color, _Min);
                        objSeries3.Points[j].Color = Color.Transparent;
                        objSeries3.Points[j]["PointWidth"] = "0.4";
                        objSeries3.Points[j].ToolTip = " Print Contrast : " + _Deviation.ToString("#0.00") + ", Min : " + _Min + ", Max: " + _Max;
                        objSeries3.Points[j].Href = "#";

                        if (_Print_Contrast_TH >= 100 || _Print_Contrast_TH <= 0)
                        {
                            objSeries0.Points[j].Empty = true;
                            objSeries2.Points[j].Empty = true;
                            objSeries3.Points[j].Empty = true;
                        }
                        else
                        {
                            if (_Max >= 100 || _Max <= 0)
                            {
                                objSeries0.Points[j].Empty = true;
                                objSeries2.Points[j].Empty = true;
                            }

                            if (_Min >= 100 || _Min <= 0)
                            {
                                objSeries3.Points[j].Empty = true;
                            }
                        }

                        //CHART_PrintContrast.Series[0].Points.AddXY(dsPrintContrast.Tables[i].Rows[0]["Color"].ToString(), _Deviation);
                        //CHART_PrintContrast.Series[0].Points[j].Color = Color.FromName(dsPrintContrast.Tables[i].Rows[0]["Color"].ToString());
                    }
                    #endregion
                }
                else
                {
                    Series objSeries0 = (Series)CHART_PrintContrast.Series[0];
                    objSeries0.XValueType = ChartValueTypes.Double;
                    objSeries0.YValueType = ChartValueTypes.String;
                    objSeries0.ShowInLegend = false;
                    objSeries0.Type = SeriesChartType.Column;
                    objSeries0.Points.AddXY("No Measurement", "10");
                    objSeries0.Points[0].Color = Color.Transparent;

                }
                return (int)Enums.enumYesNo.Yes;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region Add Blank Entry
        private void AddBlankRow()
        {
            try
            {
                Series objSeries = (Series)CHART_TVI.Series["Series0"];
                objSeries.Type = SeriesChartType.Spline;
                objSeries.XValueType = ChartValueTypes.DateTime;
                objSeries.YValueType = ChartValueTypes.Double;
                objSeries.MarkerSize = 0;

                objSeries.Points.AddXY("25%", 0.0);
                objSeries.Points.AddXY("50%", 0.0);
                objSeries.Points.AddXY("75%", 0.0);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

    }
}