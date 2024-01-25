#region System
using Dundas.Charting.WebControl;
using System;
using System.Data;
using System.Drawing;
using Taskero.Business;
using Taskero.Common;
using TaskeroWeb.Models;
#endregion


namespace TaskeroWeb
{
    public partial class PressSheetColorCharacteristicsNonProcess : PageBase
    {
        #region Page Load and Events
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //========== Clear Error ==========//
                LblError.Text = "";

                //========== Default settings ==========//
                if (IsPostBack == false)
                {
                    #region " == Fill QueryString Values == "
                    //========== Assign Variables ==========//
                    LTRL_JobID.Text = Convert.ToString(Request.QueryString["JobID"]);
                    LTRL_DongleID.Text = Convert.ToString(Request.QueryString["DongleID"]);
                    LTRL_SheetNumber.Text = Convert.ToString(Request.QueryString["SheetNumber"]);
                    LTRL_CPDeviceID.Text = Convert.ToString(Request.QueryString["CPDeviceID"]);
                    #endregion
                    //========== Set Keys ==========//
                    LTRL_KeyColorChars.Text = LTRL_JobID.Text + LTRL_DongleID.Text + LTRL_SheetNumber.Text + "-Press-Sheet-DSet-ColorChars";

                    //========== Clear Session ==========//
                    Sessions.SetSessions(LTRL_KeyColorChars.Text, null);

                    //========== Fill Form ==========//
                    FillCharts();
                }
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        protected void BTN_TVI_Click(object sender, EventArgs e)
        {
            try
            {
                //========== Fills All Charts ==========//
                FillCharts();
            }
            catch (Exception Exc)
            {
                LblError.Text = HandleException(Exc);
            }
        }
        #endregion

        #region Methods and Functions
        private void FillCharts()
        {
            try
            {
                //========== Declarations ==========//
                #region " == Get DataSet For charts === "
                if (Sessions.GetSession(LTRL_KeyColorChars.Text) == null)
                {
                    PressPatchCalculationsData objPressPatchCalculations = new PressPatchCalculationsData();
                    objPressPatchCalculations.JobID = Convert.ToInt32(LTRL_JobID.Text);
                    objPressPatchCalculations.DongleID = LTRL_DongleID.Text;
                    Sessions.SetSessions(LTRL_KeyColorChars.Text, objPressPatchCalculations.uspGetSheetColorChars(Convert.ToInt32(LTRL_SheetNumber.Text)));
                }
                DataSet DSet = (DataSet)Sessions.GetSession(LTRL_KeyColorChars.Text);
                #endregion

                //========== Variable to Change the table Index ==========//
                int _TableID = 0;

                #region Tables[0] = Gaumat
                FillGaumat(DSet.Tables[_TableID]);
                #endregion

                #region Tables[4] = Avg Density and Avg Print Contrast
                _TableID = 4;
                if (DSet.Tables[_TableID] != null && DSet.Tables[_TableID].Rows.Count > 0)
                {
                    for (int i = 0; i < DSet.Tables[_TableID].Rows.Count; i = i + 1)
                    {
                        #region "Begin Chart for Density"
                        CHRT_Density.Series[0].Points.AddXY(DSet.Tables[_TableID].Rows[i]["ColorName"].ToString(), DSet.Tables[_TableID].Rows[i]["AvgDensity"].ToString());
                        CHRT_Density.Series[0].Points[i].ToolTip = " Color Name :" + DSet.Tables[_TableID].Rows[i]["ColorName"].ToString() + ", Density : " + DSet.Tables[_TableID].Rows[i]["AvgDensity"].ToString();
                        CHRT_Density.Series[0].Points[i].Href = "#";

                        //CHRT_Density.Series[0].Points.AddXY(DSet.Tables[_TableID].Rows[i]["ShortColorName"].ToString(), DSet.Tables[_TableID].Rows[i]["AvgDensity"].ToString());
                        //CHRT_Density.Series[0].Points[i].ToolTip = " Color Name :" + DSet.Tables[_TableID].Rows[i]["ColorName"].ToString() + ", Density : " + DSet.Tables[_TableID].Rows[i]["AvgDensity"].ToString();
                        //CHRT_Density.Series[0].Points[i].Href = "#";

                        if (i == 0) { CHRT_Density.Series[0].Points[i].Color = Color.Cyan; }
                        else if (i == 1) { CHRT_Density.Series[0].Points[i].Color = Color.Magenta; }
                        else if (i == 2) { CHRT_Density.Series[0].Points[i].Color = Color.Yellow; }
                        else { CHRT_Density.Series[0].Points[i].Color = Color.Black; }

                        #endregion
                    }
                }
                #endregion

                #region Tables[6] = Fill Color Summary
                _TableID = 6;

                if (DSet.Tables[_TableID] != null)
                {
                    #region Average De & Dh
                    LTRL_ACT_AVGDE.Text = DSet.Tables[_TableID].Rows[0]["AvgDe"].ToString();
                    LTRL_STD_AVGDE.Text = DSet.Tables[_TableID].Rows[0]["StdevDe"].ToString();
                    LTRL_ACT_AVGDH.Text = DSet.Tables[_TableID].Rows[0]["AvgDH"].ToString();
                    LTRL_STD_AVGDH.Text = DSet.Tables[_TableID].Rows[0]["StdevDH"].ToString();
                    #endregion

                    #region Cyan De & Dh
                    LTRL_Ink1_ColorName.Text = DSet.Tables[_TableID].Rows[1]["ColorName"].ToString();
                    LTRL_ACT_CyanDE.Text = DSet.Tables[_TableID].Rows[1]["AvgDe"].ToString();
                    LTRL_STD_CyanDE.Text = DSet.Tables[_TableID].Rows[1]["StdevDe"].ToString();

                    LTRL_Ink1DH_ColorName.Text = DSet.Tables[_TableID].Rows[1]["ColorName"].ToString();
                    LTRL_ACT_CyanDH.Text = DSet.Tables[_TableID].Rows[1]["AvgDH"].ToString();
                    LTRL_STD_CyanDH.Text = DSet.Tables[_TableID].Rows[1]["StdevDH"].ToString();
                    #endregion

                    #region Magenta De & Dh
                    LTRL_Ink2_ColorName.Text = DSet.Tables[_TableID].Rows[2]["ColorName"].ToString();
                    LTRL_ACT_MagentaDE.Text = DSet.Tables[_TableID].Rows[2]["AvgDe"].ToString();
                    LTRL_STD_MagentaDE.Text = DSet.Tables[_TableID].Rows[2]["StdevDe"].ToString();

                    LTRL_Ink2DH_ColorName.Text = DSet.Tables[_TableID].Rows[2]["ColorName"].ToString();
                    LTRL_ACT_MagentaDH.Text = DSet.Tables[_TableID].Rows[2]["AvgDH"].ToString();
                    LTRL_STD_MagentaDH.Text = DSet.Tables[_TableID].Rows[2]["StdevDH"].ToString();
                    #endregion

                    #region Yellow De & Dh
                    LTRL_Ink3_ColorName.Text = DSet.Tables[_TableID].Rows[3]["ColorName"].ToString();
                    LTRL_ACT_YellowDE.Text = DSet.Tables[_TableID].Rows[3]["AvgDe"].ToString();
                    LTRL_STD_YellowDE.Text = DSet.Tables[_TableID].Rows[3]["StdevDe"].ToString();

                    LTRL_Ink3DH_ColorName.Text = DSet.Tables[_TableID].Rows[3]["ColorName"].ToString();
                    LTRL_ACT_YellowDH.Text = DSet.Tables[_TableID].Rows[3]["AvgDH"].ToString();
                    LTRL_STD_YellowDH.Text = DSet.Tables[_TableID].Rows[3]["StdevDH"].ToString();
                    #endregion

                    #region Black De & Dh
                    LTRL_Ink4_ColorName.Text = DSet.Tables[_TableID].Rows[4]["ColorName"].ToString();
                    LTRL_ACT_BlackDE.Text = DSet.Tables[_TableID].Rows[4]["AvgDe"].ToString();
                    LTRL_STD_BlackDE.Text = DSet.Tables[_TableID].Rows[4]["StdevDe"].ToString();

                    LTRL_Ink4DH_ColorName.Text = DSet.Tables[_TableID].Rows[4]["ColorName"].ToString();
                    LTRL_ACT_BlackDH.Text = DSet.Tables[_TableID].Rows[4]["AvgDH"].ToString();
                    LTRL_STD_BlackDH.Text = DSet.Tables[_TableID].Rows[4]["StdevDH"].ToString();
                    #endregion

                    #region Paper De & Dh
                    LTRL_ACT_PaperDE.Text = DSet.Tables[_TableID].Rows[5]["AvgDe"].ToString();
                    LTRL_STD_PaperDE.Text = DSet.Tables[_TableID].Rows[5]["StdevDe"].ToString();
                    LTRL_ACT_PaperDH.Text = DSet.Tables[_TableID].Rows[5]["AvgDH"].ToString();
                    LTRL_STD_PaperDH.Text = DSet.Tables[_TableID].Rows[5]["StdevDH"].ToString();
                    #endregion

                    #region Spot 1 De & Dh
                    if (DSet.Tables[_TableID].Rows.Count > 6)
                    {
                        LTRL_Ink5_ColorName.Text = DSet.Tables[_TableID].Rows[6]["ColorName"].ToString();
                        LTRL_ACT_Spot1DE.Text = DSet.Tables[_TableID].Rows[6]["AvgDe"].ToString();
                        LTRL_STD_Spot1DE.Text = DSet.Tables[_TableID].Rows[6]["StdevDe"].ToString();

                        LTRL_Ink5DH_ColorName.Text = DSet.Tables[_TableID].Rows[6]["ColorName"].ToString();
                        LTRL_ACT_Spot1DH.Text = DSet.Tables[_TableID].Rows[6]["AvgDH"].ToString();
                        LTRL_STD_Spot1DH.Text = DSet.Tables[_TableID].Rows[6]["StdevDH"].ToString();

                    }
                    #endregion

                    #region Spot 2 De & Dh
                    if (DSet.Tables[_TableID].Rows.Count > 7)
                    {
                        LTRL_Ink6_ColorName.Text = DSet.Tables[_TableID].Rows[7]["ColorName"].ToString();
                        LTRL_ACT_Spot2DE.Text = DSet.Tables[_TableID].Rows[7]["AvgDe"].ToString();
                        LTRL_STD_Spot2DE.Text = DSet.Tables[_TableID].Rows[7]["StdevDe"].ToString();

                        LTRL_Ink6DH_ColorName.Text = DSet.Tables[_TableID].Rows[7]["ColorName"].ToString();
                        LTRL_ACT_Spot2DH.Text = DSet.Tables[_TableID].Rows[7]["AvgDH"].ToString();
                        LTRL_STD_Spot2DH.Text = DSet.Tables[_TableID].Rows[7]["StdevDH"].ToString();
                    }
                    #endregion
                }
                #endregion

            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void FillABCharts(Chart CHRT_AB, Double A1, Double B1, Double A2, Double B2)
        {
            try
            {
                CHRT_AB.Series[0].ToolTip = "Target(a) : " + A1 + " | Target(b) : " + B1;
                CHRT_AB.Series[1].ToolTip = "Actual(a) : " + A2 + " | Actual(b) : " + B2;

                CHRT_AB.Series[0].Points.AddXY(A1, B1);
                CHRT_AB.Series[0].Type = SeriesChartType.Point;
                CHRT_AB.Series[0].Href = "#";

                CHRT_AB.Series[1].Points.AddXY(A2, B2);
                CHRT_AB.Series[1].Type = SeriesChartType.Point;
                CHRT_AB.Series[1].Href = "#";
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void FillLCharts(Chart CHRT_L, Double L1, Double L2)
        {
            try
            {
                //====  Static Dimond Top & Buttom
                CHRT_L.Series[0].Type = SeriesChartType.Point;
                CHRT_L.Series[0].Points.AddXY(1, 0);

                CHRT_L.Series[1].Type = SeriesChartType.Point;
                CHRT_L.Series[1].Points.AddXY(1, 100);
                //============================================

                //====  L1
                CHRT_L.Series[2].Type = SeriesChartType.Point;
                CHRT_L.Series[2].Points.AddXY(1, L1);

                //====  L2
                CHRT_L.Series[3].Type = SeriesChartType.Point;
                CHRT_L.Series[3].Points.AddXY(1, L2);

                //==== Tooltip  
                CHRT_L.Series[2].ToolTip = "Target(L) : " + L1;
                CHRT_L.Series[3].ToolTip = "Actual(L) : " + L2;

                CHRT_L.Series[2].Href = "#";
                CHRT_L.Series[3].Href = "#";
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void FillGaumat(DataTable dtGamut)
        {
            try
            {
                #region Plotting the Chart
                dtGamut.DefaultView.RowFilter = "Rank = 8";
                DataView DV = dtGamut.DefaultView;
                if (DV.Count > 0)
                {
                    int i = 0;
                    #region " == Gamut Chart Series 2 & 3 == "



                    CHRT_GAMUT.Series[2].Points.AddXY(Convert.ToDouble(DV[0]["Actual_A"]), Convert.ToDouble(DV[i]["Actual_B"]));
                    CHRT_GAMUT.Series[2].Type = SeriesChartType.Radar;
                    CHRT_GAMUT.Series[2]["AreaDrawingStyle"] = "Circle";
                    CHRT_GAMUT.Series[2].Points[0].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Actual(a) : " + DV[i]["Actual_A"].ToString() + ", Actual(b) : " + DV[i]["Actual_B"].ToString();
                    CHRT_GAMUT.Series[2].Points[0].Href = "#";

                    CHRT_GAMUT.Series[3].Points.AddXY(Convert.ToDouble(DV[i]["Target_A"]), Convert.ToDouble(DV[i]["Target_B"]));
                    CHRT_GAMUT.Series[3].Type = SeriesChartType.Radar;
                    CHRT_GAMUT.Series[3]["AreaDrawingStyle"] = "Circle";
                    CHRT_GAMUT.Series[3].Points[0].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Target(a) : " + DV[i]["Target_A"].ToString() + ", Target(b) : " + DV[i]["Target_B"].ToString();
                    CHRT_GAMUT.Series[3].Points[0].Href = "#";
                    #endregion
                }
                #endregion

                #region Other Points
                dtGamut.DefaultView.RowFilter = "Rank <> 8";
                DV = dtGamut.DefaultView;
                for (int i = 0; i < DV.Count; i = i + 1)
                {
                    #region " == Gamut Chart Series 0 & 1 == "

                    CHRT_GAMUT.Series[0].Points.AddXY(Convert.ToDouble(DV[i]["Actual_A"]), Convert.ToDouble(DV[i]["Actual_B"]));
                    CHRT_GAMUT.Series[0].Type = SeriesChartType.Radar;
                    CHRT_GAMUT.Series[0]["AreaDrawingStyle"] = "Circle";
                    CHRT_GAMUT.Series[0].Points[i].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Actual(a) : " + Convert.ToDouble(DV[i]["Actual_A"]).ToString("N2") + ", Actual(b) : " + Convert.ToDouble(DV[i]["Actual_B"]).ToString("N2");
                    CHRT_GAMUT.Series[0].Points[i].Href = "#";

                    CHRT_GAMUT.Series[1].Points.AddXY(Convert.ToDouble(DV[i]["Target_A"]), Convert.ToDouble(DV[i]["Target_B"]));
                    CHRT_GAMUT.Series[1].Type = SeriesChartType.Radar;
                    CHRT_GAMUT.Series[1]["AreaDrawingStyle"] = "Circle";
                    CHRT_GAMUT.Series[1].Points[i].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Target(a) : " + Convert.ToDouble(DV[i]["Target_A"]).ToString("N2") + ", Target(b) : " + Convert.ToDouble(DV[i]["Target_B"]).ToString("N2");
                    CHRT_GAMUT.Series[1].Points[i].Href = "#";
                    #endregion
                }
                if (DV.Count > 1)
                {
                    int i = 0;
                    #region " == Gamut Chart Series 0 & 1 == "



                    CHRT_GAMUT.Series[0].Points.AddXY(Convert.ToDouble(DV[i]["Actual_A"]), Convert.ToDouble(DV[i]["Actual_B"]));
                    CHRT_GAMUT.Series[0].Type = SeriesChartType.Radar;
                    CHRT_GAMUT.Series[0]["AreaDrawingStyle"] = "Circle";
                    CHRT_GAMUT.Series[0].Points[DV.Count].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Actual(a) : " + Convert.ToDouble(DV[i]["Actual_A"]).ToString("N2") + ", Actual(b) : " + Convert.ToDouble(DV[i]["Actual_B"]).ToString("N2");
                    CHRT_GAMUT.Series[0].Points[DV.Count].Href = "#";

                    CHRT_GAMUT.Series[1].Points.AddXY(Convert.ToDouble(DV[i]["Target_A"]), Convert.ToDouble(DV[i]["Target_B"]));
                    CHRT_GAMUT.Series[1].Type = SeriesChartType.Radar;
                    CHRT_GAMUT.Series[1]["AreaDrawingStyle"] = "Circle";
                    CHRT_GAMUT.Series[1].Points[DV.Count].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Target(a) : " + Convert.ToDouble(DV[i]["Target_A"]).ToString("N2") + ", Target(b) : " + Convert.ToDouble(DV[i]["Target_B"]).ToString("N2");
                    CHRT_GAMUT.Series[1].Points[DV.Count].Href = "#";
                    #endregion
                }
                #endregion
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void FillDotGain(Series objSeries, DataView DV)
        {
            try
            {
                //========== Assign Type to Series ==========//
                objSeries.Type = SeriesChartType.Spline;
                objSeries.XValueType = ChartValueTypes.String;
                objSeries.YValueType = ChartValueTypes.Double;

                //========== Plot The Series ==========//
                for (int i = 0; i < DV.Count; i = i + 1)
                {
                    objSeries.Points.AddXY(DV[i]["XValue"].ToString(), DV[i]["AvgDotgain"].ToString());
                    objSeries.Points[i].ToolTip = "Color Name : " + DV[i]["ColorName"].ToString() + ", Dotgain : " + DV[i]["AvgDotgain"].ToString();
                    objSeries.Points[i].Href = "#";
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion
    }

}