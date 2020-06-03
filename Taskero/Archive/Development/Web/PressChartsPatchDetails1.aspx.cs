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
using System.Drawing;
using Fuji.Business;
#endregion

public partial class PressChartsPatchDetails1 : PageBase
{
    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //========== Clear Error ==========//
            LblError.Text = "";

            //========== Assign Css ==========//            
            if (Fuji.Common.Sessions.Css != "") { LtrlCSS.Text = @"<link href='CSS/" + Fuji.Common.Sessions.Css + "' rel='stylesheet' type='text/css' />"; }

            //========== Default Settings ==========//
            if (IsPostBack == false)
            {
                //========== Fill Form ==========//
                LTRL_JobID.Text = Convert.ToString(Request.QueryString["JobID"]);
                LTRL_DongleID.Text = Convert.ToString(Request.QueryString["DongleID"]);
                LTRL_CPDeviceID.Text = Convert.ToString(Request.QueryString["CPDeviceID"]);
                LTRL_ColorID.Text = Convert.ToString(Request.QueryString["ColorID"]);
                LTRL_ZoneNumber.Text = Convert.ToString(Request.QueryString["ZoneNumber"]);
                if (Request.QueryString["SheetNumber"] != null) { LTRL_SheetNumber.Text = Request.QueryString["SheetNumber"].ToString(); }

                //========== Set Key ==========//
                LTRL_KeyJobZoneSummary.Text = LTRL_JobID.Text + LTRL_DongleID.Text + "-Press-DSet-ZoneSummary1";
                LTRL_KeyJobZoneSummaryPerSheet.Text = LTRL_JobID.Text + LTRL_DongleID.Text + LTRL_SheetNumber.Text + "-Press-DSet-ZoneSummaryPerSheet";

                //========== FillForm ==========//
                FillForm();
                FillBottomChart();
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    #endregion

    #region Methods and Events
    private void FillForm()
    {
        try
        {
            //========== Gets Data If Empty ==========//
            DataSet DSet;
            if (LTRL_SheetNumber.Text == "")
            {
                if (Fuji.Common.Sessions.GetSession(LTRL_KeyJobZoneSummary.Text) == null)
                {
                    //========== Declaring Objects ==========//
                    PressPatchCalculationsData objPressPatchCalculations = new PressPatchCalculationsData();
                    objPressPatchCalculations.JobID = Convert.ToInt32(LTRL_JobID.Text);
                    objPressPatchCalculations.DongleID = LTRL_DongleID.Text;
                    Fuji.Common.Sessions.SetSessions(LTRL_KeyJobZoneSummary.Text, objPressPatchCalculations.uspGetPressChartZoneSelection());
                }
                DSet = (DataSet)Fuji.Common.Sessions.GetSession(LTRL_KeyJobZoneSummary.Text);
            }
            else
            {
                if (Fuji.Common.Sessions.GetSession(LTRL_KeyJobZoneSummaryPerSheet.Text) == null)
                {
                    //========== Declaring Objects ==========//
                    PressPatchCalculationsData objPressPatchCalculations = new PressPatchCalculationsData();
                    objPressPatchCalculations.JobID = Convert.ToInt32(LTRL_JobID.Text);
                    objPressPatchCalculations.DongleID = LTRL_DongleID.Text;
                    Fuji.Common.Sessions.SetSessions(LTRL_KeyJobZoneSummaryPerSheet.Text, objPressPatchCalculations.uspGetSheetChartZoneSelection(Convert.ToInt32(LTRL_SheetNumber.Text)));
                }
                DSet = (DataSet)Fuji.Common.Sessions.GetSession(LTRL_KeyJobZoneSummaryPerSheet.Text);
            }

            //========== Filter With Zone Number ==========//
            DSet.Tables[1].DefaultView.RowFilter = " ZoneNumber = " + LTRL_ZoneNumber.Text + " AND ColorID = " + LTRL_ColorID.Text;
            DataView DV = DSet.Tables[1].DefaultView;

            //========== Fill Form  ==========//
            if (DV.Count > 0)
            {
                #region Fill Chart A , B
                CHART_GAMUT.Series[0].ToolTip = "Target(A) : " + DV[0]["Target_A"].ToString() + ", Target(B) : " + DV[0]["Target_B"].ToString();
                CHART_GAMUT.Series[0].Points.AddXY(Double.Parse(DV[0]["Target_A"].ToString()), Double.Parse(DV[0]["Target_B"].ToString()));
                CHART_GAMUT.Series[0].Type = Dundas.Charting.WebControl.SeriesChartType.Point;
                CHART_GAMUT.Series[0].Href = "#";

                CHART_GAMUT.Series[1].ToolTip = "Actual(A) : " + DV[0]["Actual_A"].ToString() + ", Actual(B) : " + DV[0]["Actual_B"].ToString();
                CHART_GAMUT.Series[1].Points.AddXY(Double.Parse(DV[0]["Actual_A"].ToString()), Double.Parse(DV[0]["Actual_B"].ToString()));
                CHART_GAMUT.Series[1].Type = Dundas.Charting.WebControl.SeriesChartType.Point;
                CHART_GAMUT.Series[1].Href = "#";
                #endregion

                #region Fill Chart L
                //========== Static Dimond Top & Buttom ==========//
                CHART_L.Series[0].Type = Dundas.Charting.WebControl.SeriesChartType.Point;
                CHART_L.Series[0].Points.AddXY(1, 0);

                CHART_L.Series[1].Type = Dundas.Charting.WebControl.SeriesChartType.Point;
                CHART_L.Series[1].Points.AddXY(1, 100);

                //========== Bind Chart ==========//
                CHART_L.Series[2].Type = Dundas.Charting.WebControl.SeriesChartType.Point;
                CHART_L.Series[2].Points.AddXY(1, DV[0]["Target_L"].ToString());
                CHART_L.Series[2].ToolTip = "Target(L) : " + DV[0]["Target_L"].ToString();
                CHART_L.Series[2].Href = "#";

                CHART_L.Series[3].Type = Dundas.Charting.WebControl.SeriesChartType.Point;
                CHART_L.Series[3].Points.AddXY(1, DV[0]["Actual_L"].ToString());
                CHART_L.Series[3].ToolTip = "Actual(L) : " + DV[0]["Actual_L"].ToString();
                CHART_L.Series[3].Href = "#";
                #endregion

                #region Fill Colors
                string _ActualColor = Formulas.ColorCalcuations.LabtoRGB(Convert.ToDouble(DV[0]["ACTUAL_L"]), Convert.ToDouble(DV[0]["ACTUAL_A"]), Convert.ToDouble(DV[0]["ACTUAL_B"]));
                TD_TargetColor.Style.Add("background-color", DV[0]["HTMLColor"].ToString());
                TD_ActualColor.Style.Add("background-color", _ActualColor);
                #endregion

                #region Fill Labels - Target
                LTRL_TARGET_L.Text = DV[0]["TARGET_L"].ToString();
                LTRL_TARGET_A.Text = DV[0]["TARGET_A"].ToString();
                LTRL_TARGET_B.Text = DV[0]["TARGET_B"].ToString();
                LTRL_TARGET_Hue.Text = DV[0]["TARGET_Hue"].ToString();
                LTRL_TARGET_Chorma.Text = DV[0]["TARGET_Chroma"].ToString();
                LTRL_TARGET_DeltaH.Text = DV[0]["TARGET_DeltaH"].ToString();
                LTRL_TARGET_Density.Text = DV[0]["TARGET_Density"].ToString();
                LTRL_TARGET_Trap.Text = DV[0]["TARGET_Trap"].ToString();
                LTRL_TARGET_Dotgain.Text = DV[0]["TARGET_Dotgain"].ToString();
                LTRL_TARGET_BRIGHTNESS.Text = DV[0]["Target_Brightness"].ToString();
                LTRL_TARGET_G7DELTAAB.Text = DV[0]["Target_G7DeltaEAB"].ToString();
                #endregion

                #region Fill Labels - Actual
                LTRL_ACTUAL_L.Text = DV[0]["ACTUAL_L"].ToString();
                LTRL_ACTUAL_A.Text = DV[0]["ACTUAL_A"].ToString();
                LTRL_ACTUAL_B.Text = DV[0]["ACTUAL_B"].ToString();
                LTRL_ACTUAL_Hue.Text = DV[0]["ACTUAL_Hue"].ToString();
                LTRL_ACTUAL_Chorma.Text = DV[0]["ACTUAL_Chroma"].ToString();
                LTRL_ACTUAL_DeltaH.Text = DV[0]["ACTUAL_DeltaH"].ToString();
                LTRL_ACTUAL_Density.Text = DV[0]["ACTUAL_Density"].ToString();
                LTRL_ACTUAL_Trap.Text = DV[0]["ACTUAL_Trap"].ToString();
                LTRL_ACTUAL_Dotgain.Text = DV[0]["ACTUAL_Dotgain"].ToString();
                LTRL_ACTUAL_BRIGHTNESS.Text = DV[0]["ACTUAL_Brightness"].ToString();
                LTRL_ACTUAL_G7DELTAAB.Text = DV[0]["ACTUAL_G7DeltaEAB"].ToString();
                #endregion
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillBottomChart()
    {
        try
        {
            //========== Declarations ==========//
            PressPatchCalculationsData objPressPatchCalculations = new PressPatchCalculationsData();

            //========== Retrieve Data ==========//
            DataTable DT;
            objPressPatchCalculations.ZoneNumber = Convert.ToInt32(LTRL_ZoneNumber.Text);
            objPressPatchCalculations.ColorID = Convert.ToInt32(LTRL_ColorID.Text);
            objPressPatchCalculations.JobID = Convert.ToInt32(LTRL_JobID.Text);
            objPressPatchCalculations.DongleID = LTRL_DongleID.Text;

            if (LTRL_SheetNumber.Text == "")
            { DT = objPressPatchCalculations.uspGetPressPatchTrendChartPerZoneNumber(); }
            else
            { DT = objPressPatchCalculations.uspGetSheetPatchTrendChartPerZoneNumber(Convert.ToInt32(LTRL_SheetNumber.Text)); }

            //========== Plot The Chart ==========//
            Dundas.Charting.WebControl.Series objSeries = CHRT_PatchZoneDetails.Series[1];
            objSeries.Points.Clear();
            objSeries.Type = Dundas.Charting.WebControl.SeriesChartType.Spline;
            objSeries.Color = Color.Black;
            CHRT_PatchZoneDetails.Titles[0].Alignment = ContentAlignment.BottomCenter;

            if (LTRL_SheetNumber.Text == "")
            {
                CHRT_PatchZoneDetails.Titles[0].Text = "Sheet Number";
                for (int i = 0; i < DT.Rows.Count; i = i + 1)
                {
                    objSeries.Points.AddXY(DT.Rows[i]["SheetNumber"].ToString(), DT.Rows[i]["AvgDeltaE"].ToString());
                    objSeries.Points[i].ToolTip = "Sheet Number : " + DT.Rows[i]["SheetNumber"].ToString() + ", Avg. Delta E : " + DT.Rows[i]["AvgDeltaE"].ToString();
                    objSeries.Points[i].MarkerStyle = Dundas.Charting.WebControl.MarkerStyle.Diamond;                    
                }
            }
            else
            {
                CHRT_PatchZoneDetails.Titles[0].Text = "Zone Number";
                for (int i = 0; i < DT.Rows.Count; i = i + 1)
                {
                    objSeries.Points.AddXY(DT.Rows[i]["ZoneNumber"].ToString(), DT.Rows[i]["AvgDeltaE"].ToString());
                    objSeries.Points[i].ToolTip = "Zone Number : " + DT.Rows[i]["ZoneNumber"].ToString() + ", Avg. Delta E : " + DT.Rows[i]["AvgDeltaE"].ToString();
                    objSeries.Points[i].MarkerStyle = Dundas.Charting.WebControl.MarkerStyle.Diamond;
                }
            }

        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    #endregion
}
