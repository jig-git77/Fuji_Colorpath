#region System
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Fuji.Business;
using Dundas.Charting.WebControl;
using System.Drawing;
#endregion

public partial class PressSheetColorCharacteristics : PageBase
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

                #region " == Fill QueryString Value == "
                //========== Assign Variables ==========//
                LTRL_JobID.Text = Convert.ToString(Request.QueryString["JobID"]);
                LTRL_DongleID.Text = Convert.ToString(Request.QueryString["DongleID"]);
                LTRL_SheetNumber.Text = Convert.ToString(Request.QueryString["SheetNumber"]);
                LTRL_CPDeviceID.Text = Convert.ToString(Request.QueryString["CPDeviceID"]);
                #endregion
                //========== Set Keys ==========//
                LTRL_KeyColorChars.Text = LTRL_JobID.Text + LTRL_DongleID.Text + LTRL_SheetNumber.Text + "-Press-Sheet-DSet-ColorChars";

                //========== Clear Session ==========//
                Fuji.Common.Sessions.SetSessions(LTRL_KeyColorChars.Text, null);

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
            if (Fuji.Common.Sessions.GetSession(LTRL_KeyColorChars.Text) == null)
            {
                PressPatchCalculationsData objPressPatchCalculations = new PressPatchCalculationsData();
                objPressPatchCalculations.JobID = Convert.ToInt32(LTRL_JobID.Text);
                objPressPatchCalculations.DongleID = LTRL_DongleID.Text;
                Fuji.Common.Sessions.SetSessions(LTRL_KeyColorChars.Text, objPressPatchCalculations.uspGetSheetColorChars(Convert.ToInt32(LTRL_SheetNumber.Text)));
            }
            DataSet DSet = (DataSet)Fuji.Common.Sessions.GetSession(LTRL_KeyColorChars.Text);
            #endregion

            //========== Variable to Change the table Index ==========//
            int _TableID = 0;

            #region Tables[0] = Gaumat
            FillGaumat(DSet.Tables[_TableID]);
            #endregion

            #region Tables[1] = L A B 25%
            _TableID = 1;
            if (DSet.Tables[_TableID] != null && DSet.Tables[_TableID].Rows.Count > 0)
            {
                FillABCharts(CHRT_AB25
                        , Convert.ToDouble(DSet.Tables[_TableID].Rows[0]["Target_A"]), Convert.ToDouble(DSet.Tables[_TableID].Rows[0]["Target_B"])
                        , Convert.ToDouble(DSet.Tables[_TableID].Rows[0]["Actual_A"]), Convert.ToDouble(DSet.Tables[_TableID].Rows[0]["Actual_B"]));
                FillLCharts(CHRT_L25
                        , Convert.ToDouble(DSet.Tables[_TableID].Rows[0]["Target_L"])
                        , Convert.ToDouble(DSet.Tables[_TableID].Rows[0]["ACTUAL_L"]));
            }
            else
            {
                CHRT_AB25.BackImage = "~/Images/FarbKreis_LAB3_150_Gray.gif";
                CHRT_L25.ChartAreas["Default"].BackColor = Color.Gray;
                CHRT_L25.BackColor = Color.Gray;
                FillLCharts(CHRT_L25, Convert.ToDouble(-50), Convert.ToDouble(-50));
            }
            #endregion

            #region Tables[2] = L A B 50%
            _TableID = 2;
            if (DSet.Tables[_TableID] != null && DSet.Tables[_TableID].Rows.Count > 0)
            {
                FillABCharts(CHRT_AB50
                        , Convert.ToDouble(DSet.Tables[_TableID].Rows[0]["Target_A"]), Convert.ToDouble(DSet.Tables[_TableID].Rows[0]["Target_B"])
                        , Convert.ToDouble(DSet.Tables[_TableID].Rows[0]["Actual_A"]), Convert.ToDouble(DSet.Tables[_TableID].Rows[0]["Actual_B"]));
                FillLCharts(CHRT_L50
                        , Convert.ToDouble(DSet.Tables[_TableID].Rows[0]["Target_L"])
                        , Convert.ToDouble(DSet.Tables[_TableID].Rows[0]["ACTUAL_L"]));
            }
            else
            {
                CHRT_AB50.BackImage = "~/Images/FarbKreis_LAB3_150_Gray.gif";
                CHRT_L50.ChartAreas["Default"].BackColor = Color.Gray;
                CHRT_L50.BackColor = Color.Gray;
                FillLCharts(CHRT_L50, Convert.ToDouble(-50), Convert.ToDouble(-50));
            }
            #endregion

            #region Tables[3] = L A B 75%
            _TableID = 3;
            if (DSet.Tables[_TableID] != null && DSet.Tables[_TableID].Rows.Count > 0)
            {
                FillABCharts(CHRT_AB75
                        , Convert.ToDouble(DSet.Tables[_TableID].Rows[0]["Target_A"]), Convert.ToDouble(DSet.Tables[_TableID].Rows[0]["Target_B"])
                        , Convert.ToDouble(DSet.Tables[_TableID].Rows[0]["Actual_A"]), Convert.ToDouble(DSet.Tables[_TableID].Rows[0]["Actual_B"]));
                FillLCharts(CHRT_L75
                        , Convert.ToDouble(DSet.Tables[_TableID].Rows[0]["Target_L"])
                        , Convert.ToDouble(DSet.Tables[_TableID].Rows[0]["ACTUAL_L"]));
            }
            else
            {
                CHRT_AB75.BackImage = "~/Images/FarbKreis_LAB3_150_Gray.gif";
                CHRT_L75.ChartAreas["Default"].BackColor = Color.Gray;
                CHRT_L75.BackColor = Color.Gray;
                FillLCharts(CHRT_L75, Convert.ToDouble(-50), Convert.ToDouble(-50));
            }
            #endregion

            #region Tables[4] = Avg Density and Avg Print Contrast

            if (DSet.Tables[4] != null && DSet.Tables[4].Rows.Count > 0)
            {
                _TableID = 4;
                for (int i = 0; i < DSet.Tables[_TableID].Rows.Count; i = i + 1)
                {
                    #region "Begin Chart for Density"

                    //CHRT_Density.Series[0].Points.AddXY(DSet.Tables[_TableID].Rows[i]["ColorName"].ToString(), DSet.Tables[_TableID].Rows[i]["AvgDensity"].ToString());
                    //CHRT_Density.Series[0].Points[i].ToolTip = " Color Name :" + DSet.Tables[_TableID].Rows[i]["ColorName"].ToString() + ", Density : " + DSet.Tables[_TableID].Rows[i]["AvgDensity"].ToString();
                    //CHRT_Density.Series[0].Points[i].Href = "#";

                    CHRT_Density.Series[0].Points.AddXY(DSet.Tables[_TableID].Rows[i]["ShortColorName"].ToString(), DSet.Tables[_TableID].Rows[i]["AvgDensity"].ToString());
                    CHRT_Density.Series[0].Points[i].ToolTip = " Color Name :" + DSet.Tables[_TableID].Rows[i]["ColorName"].ToString() + ", Density : " + DSet.Tables[_TableID].Rows[i]["AvgDensity"].ToString();
                    CHRT_Density.Series[0].Points[i].Href = "#";

                    if (i == 0) { CHRT_Density.Series[0].Points[i].Color = Color.Cyan; }
                    else if (i == 1) { CHRT_Density.Series[0].Points[i].Color = Color.Magenta; }
                    else if (i == 2) { CHRT_Density.Series[0].Points[i].Color = Color.Yellow; }
                    else { CHRT_Density.Series[0].Points[i].Color = Color.Black; }

                    #endregion
                }
            }
            #endregion

            #region Tables[5] = Dotgain
            _TableID = 5;

            //if (DSet.Tables[_TableID] != null)
            //{
            //========== Gets the series ==========//
            DSet.Tables[_TableID].DefaultView.RowFilter = "ColorName LIKE 'Cya%'";
            DataView DV_Cyan = (DataView)DSet.Tables[_TableID].DefaultView;
            if (DV_Cyan.Count > 0 && CHK_TVI_C.Checked == true) { FillDotGain(CHRT_TVI.Series["Series1"], DV_Cyan); } else { CHRT_TVI.Series["Series1"].Enabled = false; }

            DSet.Tables[_TableID].DefaultView.RowFilter = "ColorName LIKE 'Mag%'";
            DataView DV_Magenta = (DataView)DSet.Tables[_TableID].DefaultView;
            if (DV_Magenta.Count > 0 && CHK_TVI_M.Checked == true) { FillDotGain(CHRT_TVI.Series["Series2"], DV_Magenta); } else { CHRT_TVI.Series["Series2"].Enabled = false; }

            DSet.Tables[_TableID].DefaultView.RowFilter = "ColorName LIKE 'Yel%'";
            DataView DV_Yellow = (DataView)DSet.Tables[_TableID].DefaultView;
            if (DV_Yellow.Count > 0 && CHK_TVI_Y.Checked == true) { FillDotGain(CHRT_TVI.Series["Series3"], DV_Yellow); } else { CHRT_TVI.Series["Series3"].Enabled = false; }

            DSet.Tables[_TableID].DefaultView.RowFilter = "ColorName LIKE 'Bla%'";
            DataView DV_Black = (DataView)DSet.Tables[_TableID].DefaultView;
            if (DV_Black.Count > 0 && CHK_TVI_K.Checked == true) { FillDotGain(CHRT_TVI.Series["Series4"], DV_Black); } else { CHRT_TVI.Series["Series4"].Enabled = false; }
            //}
            //========== Adds a hidden Blank Row ==========//
            {
                Series objSeries = (Series)CHRT_TVI.Series["Series0"];
                objSeries.Type = SeriesChartType.Spline;
                objSeries.XValueType = ChartValueTypes.DateTime;
                objSeries.YValueType = ChartValueTypes.Double;
                objSeries.MarkerSize = 0;

                /* for dynamic axis label */
                if (DV_Cyan.Count > 0)
                {
                    for (int i = 0; i < DV_Cyan.Count; i = i + 1)
                    {
                        objSeries.Points.AddXY(DV_Cyan[i]["XValue"].ToString(), 0.0);
                    }

                }
                else
                {
                    objSeries.Points.AddXY("25%", 0.0);
                    objSeries.Points.AddXY("50%", 0.0);
                    objSeries.Points.AddXY("75%", 0.0);
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
                LTRL_ACT_CyanDE.Text = DSet.Tables[_TableID].Rows[1]["AvgDe"].ToString();
                LTRL_STD_CyanDE.Text = DSet.Tables[_TableID].Rows[1]["StdevDe"].ToString();
                LTRL_ACT_CyanDH.Text = DSet.Tables[_TableID].Rows[1]["AvgDH"].ToString();
                LTRL_STD_CyanDH.Text = DSet.Tables[_TableID].Rows[1]["StdevDH"].ToString();
                #endregion

                #region Magenta De & Dh
                LTRL_ACT_MagentaDE.Text = DSet.Tables[_TableID].Rows[2]["AvgDe"].ToString();
                LTRL_STD_MagentaDE.Text = DSet.Tables[_TableID].Rows[2]["StdevDe"].ToString();
                LTRL_ACT_MagentaDH.Text = DSet.Tables[_TableID].Rows[2]["AvgDH"].ToString();
                LTRL_STD_MagentaDH.Text = DSet.Tables[_TableID].Rows[2]["StdevDH"].ToString();
                #endregion

                #region Yellow De & Dh
                LTRL_ACT_YellowDE.Text = DSet.Tables[_TableID].Rows[3]["AvgDe"].ToString();
                LTRL_STD_YellowDE.Text = DSet.Tables[_TableID].Rows[3]["StdevDe"].ToString();
                LTRL_ACT_YellowDH.Text = DSet.Tables[_TableID].Rows[3]["AvgDH"].ToString();
                LTRL_STD_YellowDH.Text = DSet.Tables[_TableID].Rows[3]["StdevDH"].ToString();
                #endregion

                #region Black De & Dh
                LTRL_ACT_BlackDE.Text = DSet.Tables[_TableID].Rows[4]["AvgDe"].ToString();
                LTRL_STD_BlackDE.Text = DSet.Tables[_TableID].Rows[4]["StdevDe"].ToString();
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
                    LTRL_Spot1DE_ColorName.Text = DSet.Tables[_TableID].Rows[6]["ColorName"].ToString();
                    LTRL_Spot1DH_ColorName.Text = DSet.Tables[_TableID].Rows[6]["ColorName"].ToString();
                    LTRL_ACT_Spot1DE.Text = DSet.Tables[_TableID].Rows[6]["AvgDe"].ToString();
                    LTRL_STD_Spot1DE.Text = DSet.Tables[_TableID].Rows[6]["StdevDe"].ToString();
                    LTRL_ACT_Spot1DH.Text = DSet.Tables[_TableID].Rows[6]["AvgDH"].ToString();
                    LTRL_STD_Spot1DH.Text = DSet.Tables[_TableID].Rows[6]["StdevDH"].ToString();

                }
                #endregion

                #region Spot 2 De & Dh
                if (DSet.Tables[_TableID].Rows.Count > 7)
                {
                    LTRL_Spot2DE_ColorName.Text = DSet.Tables[_TableID].Rows[7]["ColorName"].ToString();
                    LTRL_Spot2DH_ColorName.Text = DSet.Tables[_TableID].Rows[7]["ColorName"].ToString();

                    LTRL_ACT_Spot2DE.Text = DSet.Tables[_TableID].Rows[7]["AvgDe"].ToString();
                    LTRL_STD_Spot2DE.Text = DSet.Tables[_TableID].Rows[7]["StdevDe"].ToString();
                    LTRL_ACT_Spot2DH.Text = DSet.Tables[_TableID].Rows[7]["AvgDH"].ToString();
                    LTRL_STD_Spot2DH.Text = DSet.Tables[_TableID].Rows[7]["StdevDH"].ToString();
                }
                #endregion
            }
            #endregion

            #region Tables[7] = Print Contrast
            _TableID = 7;
            if (DSet.Tables[_TableID] != null)
            {
                for (int i = 0; i < DSet.Tables[_TableID].Rows.Count; i = i + 1)
                {
                    CHRT_PrintContrast.Series[0].Points.AddXY(DSet.Tables[_TableID].Rows[i]["ColorName1"].ToString(), DSet.Tables[_TableID].Rows[i]["AvgPrintContrast"].ToString());
                    CHRT_PrintContrast.Series[0].Points[i].ToolTip = " Color Name :" + DSet.Tables[_TableID].Rows[i]["ColorName"].ToString() + ", Print Contrast : " + DSet.Tables[_TableID].Rows[i]["AvgPrintContrast"].ToString();
                    CHRT_PrintContrast.Series[0].Points[i].Href = "#";


                    if (i == 0) { CHRT_PrintContrast.Series[0].Points[i].Color = Color.Cyan; }
                    else if (i == 1) { CHRT_PrintContrast.Series[0].Points[i].Color = Color.Magenta; }
                    else if (i == 2) { CHRT_PrintContrast.Series[0].Points[i].Color = Color.Yellow; }
                    else { CHRT_PrintContrast.Series[0].Points[i].Color = Color.Black; }

                }
            }
            #endregion

            #region Tables[8] = K NPD WHERE 'Press.InkWells.PrimaryColorType = 0
           _TableID = 8;

            //=========== Updated On : 21Jan2009, Updated By : Softweb Start ===========//
            CHRT_KYPD.Series[0].Type = SeriesChartType.Spline;// SeriesChartType.FastLine;
            CHRT_KYPD.Series[1].Type = SeriesChartType.Spline;// SeriesChartType.FastLine;
            //=========== Updated On : 21Jan2009, Updated By : Softweb End ===========//

            CHRT_KYPD.Series[0].MarkerSize = 5;
            CHRT_KYPD.Series[1].MarkerSize = 5;

           //DSet.Tables[_TableID].DefaultView.RowFilter = "PrimaryColorType = 0";

            DataView DV8 = DSet.Tables[_TableID].DefaultView;
            if (DV8.Count > 0)
            {
                for (int i = 0; i < DV8.Count; i = i + 1)
                {

                    CHRT_KYPD.Series[0].Points.AddXY(DV8[i]["XValue"].ToString(), DV8[i]["Density"].ToString());
                    CHRT_KYPD.Series[0].Points[i].ToolTip = "Color: " + DV8[i]["ColorName"].ToString() + ", Density: " + DV8[i]["Density"].ToString();
                    CHRT_KYPD.Series[0].Points[i].Href = "#";


                    CHRT_KYPD.Series[1].Points.AddXY(DV8[i]["XValue"].ToString(), DV8[i]["TargetDensity"].ToString());
                    CHRT_KYPD.Series[1].Points[i].ToolTip = "Color: " + DV8[i]["ColorName"].ToString() + ", Target Density:" + DV8[i]["TargetDensity"].ToString();
                    CHRT_KYPD.Series[1].Points[i].Href = "#";
                }
            }
            else
            {
                CHRT_KYPD.Visible = false;
            }
            #endregion

            #region Tables[9] =  CMY NPD Gray NPD
            /* only show if job is G7 */
            _TableID = 9;

            //=========== Updated On : 21Jan2009, Updated By : Softweb Start ===========//
            CHRT_CMYNPD.Series[0].Type = SeriesChartType.Spline;// SeriesChartType.FastLine;
            CHRT_CMYNPD.Series[1].Type = SeriesChartType.Spline;// SeriesChartType.FastLine;
            //=========== Updated On : 21Jan2009, Updated By : Softweb End ===========//

            CHRT_CMYNPD.Series[0].MarkerSize = 5;
            CHRT_CMYNPD.Series[1].MarkerSize = 5;

            //changed by CSS 05/14/2009 can't find table 9 error
            if (DSet.Tables.Count > 9)
            {
                DataView DV9 = DSet.Tables[_TableID].DefaultView;
                if (DV9.Count > 1)
                {
                    CHRT_TRAP.Visible = false;
                    for (int i = 0; i < DV9.Count; i = i + 1)
                    {

                        CHRT_CMYNPD.Series[0].Points.AddXY(DV9[i]["XValue"].ToString(), DV9[i]["Density"].ToString());
                        CHRT_CMYNPD.Series[0].Points[i].ToolTip = "Color: " + DV9[i]["ColorName"].ToString() + ", Density: " + DV9[i]["Density"].ToString();
                        CHRT_CMYNPD.Series[0].Points[i].Href = "#";

                        //if value is not zero then plot the target value
                        if (Convert.ToDouble(DV9[i]["TargetDensity"]) != 0.0)
                        {
                            CHRT_CMYNPD.Series[1].Points.AddXY(DV9[i]["XValue"].ToString(), DV9[i]["TargetDensity"].ToString());
                            CHRT_CMYNPD.Series[1].Points[i].ToolTip = "Color: " + DV9[i]["ColorName"].ToString() + ", Neutral Density:" + DV9[i]["TargetDensity"].ToString();
                            CHRT_CMYNPD.Series[1].Points[i].Href = "#";
                        }
                    }
                }
                else
                {
                    CHRT_CMYNPD.Visible = false;

                    if (DSet.Tables[10].Rows.Count > 0)
                    {
                        _TableID = 10;
                        for (int i = 0; i < DSet.Tables[_TableID].Rows.Count; i = i + 1)
                        {
                            #region "Begin Chart for Trap"

                            CHRT_TRAP.Series[0].Points.AddXY(DSet.Tables[_TableID].Rows[i]["ShortColorName"].ToString(), DSet.Tables[_TableID].Rows[i]["Trap"].ToString());
                            CHRT_TRAP.Series[0].Points[i].ToolTip = " Color Name:" + DSet.Tables[_TableID].Rows[i]["ColorName"].ToString() + ", Trap: " + DSet.Tables[_TableID].Rows[i]["Trap"].ToString();
                            CHRT_TRAP.Series[0].Points[i].Href = "#";

                            CHRT_TRAP.Series[0].Points[i].Color = System.Drawing.ColorTranslator.FromHtml(DSet.Tables[_TableID].Rows[i]["HTMLColor"].ToString());

                            #endregion "End  Chart for Density"
                        }
                    }
                    else
                    {
                        CHRT_TRAP.Visible = false;
                    }
                }
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

            //====  L1  --target
            CHRT_L.Series[2].Type = SeriesChartType.Point;
            CHRT_L.Series[2].Points.AddXY(1, L1);

            //====  L2 --actual
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

                CHRT_GAMUT.Series[2].Points.AddXY(Convert.ToDouble(DV[0]["Target_A"]), Convert.ToDouble(DV[i]["Target_B"]));
                CHRT_GAMUT.Series[2].Type = SeriesChartType.FastPoint;
                CHRT_GAMUT.Series[2].Points[0].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Target(a) : " + DV[i]["Target_A"].ToString() + ", Target(b) : " + DV[i]["Target_B"].ToString();
                CHRT_GAMUT.Series[2].Points[0].Href = "#";

                CHRT_GAMUT.Series[3].Points.AddXY(Convert.ToDouble(DV[i]["Actual_A"]), Convert.ToDouble(DV[i]["Actual_B"]));
                CHRT_GAMUT.Series[3].Type = SeriesChartType.FastPoint;
                CHRT_GAMUT.Series[3].Points[0].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Actual(a) : " + DV[i]["Actual_A"].ToString() + ", Actual(b) : " + DV[i]["Actual_B"].ToString();
                CHRT_GAMUT.Series[3].Points[0].Href = "#";

                
            }
            #endregion

            #region Other Points
            dtGamut.DefaultView.RowFilter = "Rank <> 8";
            DV = dtGamut.DefaultView;
            for (int i = 0; i < DV.Count; i = i + 1)
            {
                CHRT_GAMUT.Series[0].Points.AddXY(Convert.ToDouble(DV[i]["Target_A"]), Convert.ToDouble(DV[i]["Target_B"]));
                CHRT_GAMUT.Series[0].Type = SeriesChartType.Line;
                CHRT_GAMUT.Series[0].Points[i].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Target(a) : " + Convert.ToDouble(DV[i]["Target_A"]).ToString("N2") + ", Target(b) : " + Convert.ToDouble(DV[i]["Target_B"]).ToString("N2");
                CHRT_GAMUT.Series[0].Points[i].Href = "#";

                CHRT_GAMUT.Series[1].Points.AddXY(Convert.ToDouble(DV[i]["Actual_A"]), Convert.ToDouble(DV[i]["Actual_B"]));
                CHRT_GAMUT.Series[1].Type = SeriesChartType.Line;
                CHRT_GAMUT.Series[1].Points[i].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Actual(a) : " + Convert.ToDouble(DV[i]["Actual_A"]).ToString("N2") + ", Actual(b) : " + Convert.ToDouble(DV[i]["Actual_B"]).ToString("N2");
                CHRT_GAMUT.Series[1].Points[i].Href = "#";
            }
            if (DV.Count > 1)
            {
                int i = 0;
                CHRT_GAMUT.Series[0].Points.AddXY(Convert.ToDouble(DV[i]["Target_A"]), Convert.ToDouble(DV[i]["Target_B"]));
                CHRT_GAMUT.Series[0].Type = SeriesChartType.Line;
                CHRT_GAMUT.Series[0].Points[DV.Count].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Target(a) : " + Convert.ToDouble(DV[i]["Target_A"]).ToString("N2") + ", Target(b) : " + Convert.ToDouble(DV[i]["Target_B"]).ToString("N2");
                CHRT_GAMUT.Series[0].Points[DV.Count].Href = "#";

                CHRT_GAMUT.Series[1].Points.AddXY(Convert.ToDouble(DV[i]["Target_A"]), Convert.ToDouble(DV[i]["Target_B"]));
                CHRT_GAMUT.Series[1].Type = SeriesChartType.Line;
                CHRT_GAMUT.Series[1].Points[DV.Count].ToolTip = "Color : " + DV[i]["ColorName"].ToString() + ", Actual(a) : " + Convert.ToDouble(DV[i]["Actual_A"]).ToString("N2") + ", Actual(b) : " + Convert.ToDouble(DV[i]["Actual_B"]).ToString("N2");
                CHRT_GAMUT.Series[1].Points[DV.Count].Href = "#";
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