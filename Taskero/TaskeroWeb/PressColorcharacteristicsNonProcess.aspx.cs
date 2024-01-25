using Dundas.Charting.WebControl;
using System;
using System.Data;
using Taskero.Business;
using Taskero.Common;
using TaskeroWeb.Models;


namespace TaskeroWeb
{
    public partial class PressColorcharacteristicsNonProcess : PageBase
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
                    //========== Assign Variables ==========//

                    LTRL_JobID.Text = (Request.QueryString["JobID"] == null ? "0" : Request.QueryString["JobID"].ToString());
                    LTRL_DongleID.Text = (Request.QueryString["DongleID"] == null ? "0" : Request.QueryString["DongleID"].ToString());
                    LTRL_CPDeviceID.Text = (Request.QueryString["CPDeviceID"] == null ? "0" : Request.QueryString["CPDeviceID"].ToString());


                    //========== Set Keys ==========//
                    LTRL_KeyColorChars.Text = LTRL_JobID.Text + LTRL_DongleID.Text + "-Press-DSet-ColorChars";

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
                if (Sessions.GetSession(LTRL_KeyColorChars.Text) == null)
                {
                    PressPatchCalculationsData objPressPatchCalculations = new PressPatchCalculationsData
                    {
                        JobID = Convert.ToInt32(LTRL_JobID.Text),
                        DongleID = LTRL_DongleID.Text
                    };
                    Sessions.SetSessions(LTRL_KeyColorChars.Text, objPressPatchCalculations.uspGetPressColorChars());
                }
                DataSet DSet = (DataSet)Sessions.GetSession(LTRL_KeyColorChars.Text);


                //========== Variable to Change the table Index ==========//
                int _TableID = 0;

                #region Tables[0] = Gaumat
                FillGaumat(DSet.Tables[_TableID]);
                #endregion

                #region Tables[4] = Avg Density and Avg Print Contrast

                if (DSet.Tables[4].Rows.Count > 0)
                {
                    _TableID = 4;
                    for (int i = 0; i < DSet.Tables[_TableID].Rows.Count; i = i + 1)
                    {
                        #region "Begin Chart for Density"

                        CHRT_Density.Series[0].Points.AddXY(DSet.Tables[_TableID].Rows[i]["ColorName"].ToString(), DSet.Tables[_TableID].Rows[i]["AvgDensity"].ToString());
                        CHRT_Density.Series[0].Points[i].ToolTip = " Color Name:" + DSet.Tables[_TableID].Rows[i]["ColorName"].ToString() + ", Density: " + DSet.Tables[_TableID].Rows[i]["AvgDensity"].ToString();
                        CHRT_Density.Series[0].Points[i].Href = "#";

                        /* Maulik's Code
                        if (i == 0) { CHRT_Density.Series[0].Points[i].Color = Color.Black; }
                        else if (i == 1) { CHRT_Density.Series[0].Points[i].Color = Color.Cyan; }
                        else if (i == 2) { CHRT_Density.Series[0].Points[i].Color = Color.Magenta; }
                        else { CHRT_Density.Series[0].Points[i].Color = Color.Yellow; }
                        */

                        CHRT_Density.Series[0].Points[i].Color = System.Drawing.ColorTranslator.FromHtml(DSet.Tables[_TableID].Rows[i]["HTMLColor"].ToString());

                        #endregion "End  Chart for Density"
                    }
                }
                #endregion


                #region Tables[6] = Fill Color Summary
                _TableID = 6;

                #region Average De & Dh
                LTRL_ACT_AVGDE.Text = DSet.Tables[_TableID].Rows[0]["AvgDe"].ToString();
                LTRL_STD_AVGDE.Text = DSet.Tables[_TableID].Rows[0]["StdevDe"].ToString();
                LTRL_ACT_AVGDH.Text = DSet.Tables[_TableID].Rows[0]["AvgDH"].ToString();
                LTRL_STD_AVGDH.Text = DSet.Tables[_TableID].Rows[0]["StdevDH"].ToString();
                #endregion

                #region Cyan De & Dh
                if (DSet.Tables[_TableID].Rows.Count > 1)
                {
                    LTRL_Ink1_ColorName.Text = DSet.Tables[_TableID].Rows[1]["ColorName"].ToString();
                    LTRL_ACT_CyanDE.Text = DSet.Tables[_TableID].Rows[1]["AvgDe"].ToString();
                    LTRL_STD_CyanDE.Text = DSet.Tables[_TableID].Rows[1]["StdevDe"].ToString();

                    LTRL_Ink1DH_ColorName.Text = DSet.Tables[_TableID].Rows[1]["ColorName"].ToString();
                    LTRL_ACT_CyanDH.Text = DSet.Tables[_TableID].Rows[1]["AvgDH"].ToString();
                    LTRL_STD_CyanDH.Text = DSet.Tables[_TableID].Rows[1]["StdevDH"].ToString();
                }
                #endregion

                #region Magenta De & Dh
                if (DSet.Tables[_TableID].Rows.Count > 2)
                {
                    LTRL_Ink2_ColorName.Text = DSet.Tables[_TableID].Rows[2]["ColorName"].ToString();
                    LTRL_ACT_MagentaDE.Text = DSet.Tables[_TableID].Rows[2]["AvgDe"].ToString();
                    LTRL_STD_MagentaDE.Text = DSet.Tables[_TableID].Rows[2]["StdevDe"].ToString();

                    LTRL_Ink2DH_ColorName.Text = DSet.Tables[_TableID].Rows[2]["ColorName"].ToString();
                    LTRL_ACT_MagentaDH.Text = DSet.Tables[_TableID].Rows[2]["AvgDH"].ToString();
                    LTRL_STD_MagentaDH.Text = DSet.Tables[_TableID].Rows[2]["StdevDH"].ToString();
                }
                #endregion

                #region Yellow De & Dh
                if (DSet.Tables[_TableID].Rows.Count > 3)
                {
                    LTRL_Ink3_ColorName.Text = DSet.Tables[_TableID].Rows[3]["ColorName"].ToString();
                    LTRL_ACT_YellowDE.Text = DSet.Tables[_TableID].Rows[3]["AvgDe"].ToString();
                    LTRL_STD_YellowDE.Text = DSet.Tables[_TableID].Rows[3]["StdevDe"].ToString();

                    LTRL_Ink3DH_ColorName.Text = DSet.Tables[_TableID].Rows[3]["ColorName"].ToString();
                    LTRL_ACT_YellowDH.Text = DSet.Tables[_TableID].Rows[3]["AvgDH"].ToString();
                    LTRL_STD_YellowDH.Text = DSet.Tables[_TableID].Rows[3]["StdevDH"].ToString();
                }
                #endregion

                #region Black De & Dh
                if (DSet.Tables[_TableID].Rows.Count > 4)
                {
                    LTRL_Ink4_ColorName.Text = DSet.Tables[_TableID].Rows[4]["ColorName"].ToString();
                    LTRL_ACT_BlackDE.Text = DSet.Tables[_TableID].Rows[4]["AvgDe"].ToString();
                    LTRL_STD_BlackDE.Text = DSet.Tables[_TableID].Rows[4]["StdevDe"].ToString();

                    LTRL_Ink4DH_ColorName.Text = DSet.Tables[_TableID].Rows[4]["ColorName"].ToString();
                    LTRL_ACT_BlackDH.Text = DSet.Tables[_TableID].Rows[4]["AvgDH"].ToString();
                    LTRL_STD_BlackDH.Text = DSet.Tables[_TableID].Rows[4]["StdevDH"].ToString();
                    #endregion
                }
                #region Paper De & Dh
                if (DSet.Tables[_TableID].Rows.Count > 5)
                {
                    LTRL_ACT_PaperDE.Text = DSet.Tables[_TableID].Rows[5]["AvgDe"].ToString();
                    LTRL_STD_PaperDE.Text = DSet.Tables[_TableID].Rows[5]["StdevDe"].ToString();
                    LTRL_ACT_PaperDH.Text = DSet.Tables[_TableID].Rows[5]["AvgDH"].ToString();
                    LTRL_STD_PaperDH.Text = DSet.Tables[_TableID].Rows[5]["StdevDH"].ToString();
                }
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
                #endregion
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
                //========== Plotting the Chart ==========//
                dtGamut.DefaultView.RowFilter = "Rank = 8";
                DataView DV = dtGamut.DefaultView;
                if (DV.Count > 0)
                {
                    //ImgShowGamut.Visible = true;
                    int i = 0;

                    CHRT_GAMUT.Series["Series3"].Points.AddXY(Convert.ToDouble(DV[i]["Actual_A"]), Convert.ToDouble(DV[i]["Actual_B"]));
                    CHRT_GAMUT.Series["Series3"].Type = SeriesChartType.Point;
                    CHRT_GAMUT.Series["Series3"]["PointDrawingStyle"] = "Marker";
                    CHRT_GAMUT.Series["Series3"]["AreaDrawingStyle"] = "Circle";
                    CHRT_GAMUT.Series["Series3"].Points[0].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Actual(a) : " + DV[i]["Actual_A"].ToString() + ", Actual(b) : " + DV[i]["Actual_B"].ToString();
                    CHRT_GAMUT.Series["Series3"].Points[0].Href = "#";


                    CHRT_GAMUT.Series["Series4"].Points.AddXY(Convert.ToDouble(DV[i]["Target_A"]), Convert.ToDouble(DV[i]["Target_B"]));
                    CHRT_GAMUT.Series["Series4"].Type = SeriesChartType.Point;
                    CHRT_GAMUT.Series["Series4"]["PointDrawingStyle"] = "Marker";
                    CHRT_GAMUT.Series["Series4"]["AreaDrawingStyle"] = "Circle";
                    CHRT_GAMUT.Series["Series4"].Points[0].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Target(a) : " + DV[i]["Target_A"].ToString() + ", Target(b) : " + DV[i]["Target_B"].ToString();
                    CHRT_GAMUT.Series["Series4"].Points[0].Href = "#";



                }
                else
                {
                    CHRT_GAMUT.ChartAreas["Default"].BackImage = "~/Images/FarbKreis_LAB2_150_Gray.gif";
                    //ImgShowGamut.Visible = false;
                }
                #endregion

                #region Other Points
                //========== Other Points ==========//
                dtGamut.DefaultView.RowFilter = "Rank <> 8";
                DV = dtGamut.DefaultView;
                for (int i = 0; i < DV.Count; i = i + 1)
                {


                    CHRT_GAMUT.Series["Series1"].Points.AddXY(Convert.ToDouble(DV[i]["Actual_A"]), Convert.ToDouble(DV[i]["Actual_B"]));
                    CHRT_GAMUT.Series["Series1"].Type = SeriesChartType.Point;
                    CHRT_GAMUT.Series["Series1"]["PointDrawingStyle"] = "Marker";
                    CHRT_GAMUT.Series["Series1"]["AreaDrawingStyle"] = "Circle";
                    CHRT_GAMUT.Series["Series1"].Points[i].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Actual(a) : " + Convert.ToDouble(DV[i]["Actual_A"]).ToString("N2") + ", Actual(b) : " + Convert.ToDouble(DV[i]["Actual_B"]).ToString("N2");
                    CHRT_GAMUT.Series["Series1"].Points[i].Href = "#";

                    CHRT_GAMUT.Series["Series2"].Points.AddXY(Convert.ToDouble(DV[i]["Target_A"]), Convert.ToDouble(DV[i]["Target_B"]));
                    CHRT_GAMUT.Series["Series2"].Type = SeriesChartType.Point;
                    CHRT_GAMUT.Series["Series2"]["PointDrawingStyle"] = "Marker";
                    CHRT_GAMUT.Series["Series2"]["AreaDrawingStyle"] = "Circle";
                    CHRT_GAMUT.Series["Series2"].Points[i].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Target(a) : " + Convert.ToDouble(DV[i]["Target_A"]).ToString("N2") + ", Target(b) : " + Convert.ToDouble(DV[i]["Target_B"]).ToString("N2");
                    CHRT_GAMUT.Series["Series2"].Points[i].Href = "#";

                }
                if (DV.Count > 1)
                {
                    int i = 0;

                    CHRT_GAMUT.Series["Series1"].Points.AddXY(Convert.ToDouble(DV[i]["Actual_A"]), Convert.ToDouble(DV[i]["Actual_B"]));
                    CHRT_GAMUT.Series["Series1"].Type = SeriesChartType.Point;
                    CHRT_GAMUT.Series["Series1"]["PointDrawingStyle"] = "Marker";
                    CHRT_GAMUT.Series["Series1"]["AreaDrawingStyle"] = "Circle";
                    CHRT_GAMUT.Series["Series1"].Points[DV.Count].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Actual(a) : " + Convert.ToDouble(DV[i]["Actual_A"]).ToString("N2") + ", Actual(b) : " + Convert.ToDouble(DV[i]["Actual_B"]).ToString("N2");
                    CHRT_GAMUT.Series["Series1"].Points[DV.Count].Href = "#";


                    CHRT_GAMUT.Series["Series2"].Points.AddXY(Convert.ToDouble(DV[i]["Target_A"]), Convert.ToDouble(DV[i]["Target_B"]));
                    CHRT_GAMUT.Series["Series2"].Type = SeriesChartType.Point;
                    CHRT_GAMUT.Series["Series2"]["PointDrawingStyle"] = "Marker";
                    CHRT_GAMUT.Series["Series2"]["AreaDrawingStyle"] = "Circle";
                    CHRT_GAMUT.Series["Series2"].Points[DV.Count].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Target(a) : " + Convert.ToDouble(DV[i]["Target_A"]).ToString("N2") + ", Target(b) : " + Convert.ToDouble(DV[i]["Target_B"]).ToString("N2");
                    CHRT_GAMUT.Series["Series2"].Points[DV.Count].Href = "#";

                }
                #endregion
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion
    }

}