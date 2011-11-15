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
#endregion

#region Others
using Fuji.Business;
using Fuji.Common;
using System.Drawing;
using Formulas;
using Dundas.Charting.WebControl;
using System.Text;
#endregion

public partial class SyncDetailMeasurementChart : PageBase
{
    //private string SessionName {
    //    get { return "SyncDetailMeasurementChart-" + Request.QueryString["MeasurementID"].ToString(); }
    //}
    private string SessionSummaryChart = "SummaryChart";
    private string SessionChartTable = "ChartTable";
    private string SessionG7Chart = "G7Chart";
    private string SessionLabChart = "LabChart";

    #region Page Load and Events
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            ////=========== Check Login ==========//
            Sessions.CheckUserLogin();

            //========== Clear Error ==========//
            LblError.Text = "";

            //========== Assign Css ==========//            
            LtrlCSS.Text = @"<link href='CSS/" + Fuji.Common.Sessions.Css + "' rel='stylesheet' type='text/css' />";

            TDEPrimary.Attributes.Add("onclick", "return  parent.OpenGamutWindow('SyncMeasurementChartPopup.aspx?MID=" + Request.QueryString["MeasurementID"].ToString() + "&PressRunID=" + Fuji.Common.Sessions.GetSession("SelectedPressRunID").ToString() + "&Type=Gamut','500px','600px')");
            TDDotgain.Attributes.Add("onclick", "return  parent.OpenGamutWindow('SyncMeasurementChartPopup.aspx?MID=" + Request.QueryString["MeasurementID"].ToString() + "&PressRunID=" + Fuji.Common.Sessions.GetSession("SelectedPressRunID").ToString() + "&Type=Dotgain','750px','570px')");

            //Fuji.Common.CommonFunctions.SetGridViewDefaultSettings(GV_MeasurementData);
            //========== Check And Set Labels ==========//
            if (IsPostBack == false)
            {                
                FillAllTabCharts(Convert.ToInt32(Request.QueryString["MeasurementID"]));             
            }
        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }
    #endregion

    #region Methods

    private void FillAllTabCharts(int MeasurementID)
    {
   
        try
        {

            #region Fill data table from database
            DataSet DS = null;

            if (Request.QueryString["PressRunID"] != null)
            {

                  // ===== Create Object of CPSyncMeasurementData  ======//
                 CPSyncMeasurementData objCPSyncMeasurementData = new CPSyncMeasurementData();

                // ===== Get Data from data base  ==================//
                 objCPSyncMeasurementData.PressRunID = Convert.ToInt32(Request.QueryString["PressRunID"]);
                 DS = objCPSyncMeasurementData.uspGetChartsDataAvg();            
            }
            else
	        {
                 // ===== Create Object of CPSyncMeasurementData  ======//
                 CPSyncMeasurementData objCPSyncMeasurementData = new CPSyncMeasurementData();

                // ===== Get Data from data base  ==================//
                objCPSyncMeasurementData.MeasurementID = MeasurementID;
                DS = objCPSyncMeasurementData.uspGetAnalyzeMeasurementG7ChartsData();	        
            }

            if (DS != null && DS.Tables.Count > 0)
            {
                FillInkPaperChart(DS.Tables[0], DS.Tables[1], DS.Tables[2]);
                FillG7Chart(DS.Tables[3], DS.Tables[4], DS.Tables[5], DS.Tables[6]);
                FillChartTable(DS.Tables[7],DS.Tables[8]);  //if FileFound = 1 show download icon
                FillLabCharts(DS.Tables[9], DS.Tables[10], DS.Tables[11]);
                FillTVIChart(DS.Tables[12]);
            }                
            #endregion

        } catch (Exception Exc)
        {
            HandleException(Exc);
        }
    }

    #region Fill InkPaper Tab    
    private void FillInkPaperChart(DataTable DTDeltaEPrimaryPatches, DataTable DTDensity, DataTable Paper)
    {
        try
        {           
            if (DTDeltaEPrimaryPatches != null)
                FillDeltaEPrimaryPatchesChart(DTDeltaEPrimaryPatches);
            if (DTDensity != null)
                FillDensityChart(DTDensity);
            if (Paper != null)
                FillPaperChart(Paper);          
            
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }

    #region Fill DeltaE Primary Patches Chart
    private void FillDeltaEPrimaryPatchesChart(DataTable DT)
    {
        try
        {
            ChartSummary_DeltaE.BorderColor = Color.White;
            ChartSummary_DeltaE.BorderWidth = 0;
            ChartSummary_DeltaE.BackColor = Color.White;

            //========== Clear Points and Show Chart ==========//
            ChartSummary_DeltaE.Visible = true;
            ChartSummary_DeltaE.Series[0].Points.Clear();
            ChartSummary_DeltaE.Series[1].Points.Clear();
            ChartSummary_DeltaE.Series[2].Points.Clear();

            #region //========== Declarations ==========//
            // === Series0 to display Thresold value === //
            Series objSeries0 = (Series)ChartSummary_DeltaE.Series[0];
            objSeries0.XValueType = ChartValueTypes.Int;
            objSeries0.YValueType = ChartValueTypes.String;
            objSeries0.ShowInLegend = false;
            objSeries0.Type = SeriesChartType.Bar;
            objSeries0.BackGradientType = GradientType.DiagonalRight;

            // === Series1 to display Actual value === //
            Series objSeries1 = (Series)ChartSummary_DeltaE.Series[1];
            objSeries1.XValueType = ChartValueTypes.Int;
            objSeries1.YValueType = ChartValueTypes.String;
            objSeries1.ShowInLegend = false;
            objSeries1.Type = SeriesChartType.Bar;
      

            // === Series2 to display red marker on Thresold value === //
            Series objSeries2 = (Series)ChartSummary_DeltaE.Series[2];
            objSeries2.XValueType = ChartValueTypes.Int;
            objSeries2.YValueType = ChartValueTypes.String;
            objSeries2.ShowInLegend = false;
            objSeries2.Type = SeriesChartType.Bar;

            // === Make Chart area look like 2D even it is 3D === //
            ChartSummary_DeltaE.ChartAreas["ChartArea1"].Area3DStyle.RightAngleAxes = true;
            ChartSummary_DeltaE.ChartAreas["ChartArea1"].Area3DStyle.Clustered = true;
            ChartSummary_DeltaE.ChartAreas["ChartArea1"].Area3DStyle.XAngle = 0;
            ChartSummary_DeltaE.ChartAreas["ChartArea1"].Area3DStyle.YAngle = 0;
            ChartSummary_DeltaE.ChartAreas["ChartArea1"].BackColor = Color.White;
            #endregion

            #region //========== ChartArea Settings ==========//
            ChartSummary_DeltaE.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
            ChartSummary_DeltaE.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;

            ChartSummary_DeltaE.ChartAreas["ChartArea1"].Position.Auto = true;
            ChartSummary_DeltaE.ChartAreas["ChartArea1"].Position.X = float.Parse("0");
            ChartSummary_DeltaE.ChartAreas["ChartArea1"].Position.Y = float.Parse("5");
            ChartSummary_DeltaE.ChartAreas["ChartArea1"].Position.Width = float.Parse("99");
            ChartSummary_DeltaE.ChartAreas["ChartArea1"].Position.Height = float.Parse("97");

            ChartSummary_DeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.Auto = true;
            ChartSummary_DeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.X = float.Parse("0");
            ChartSummary_DeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.Y = float.Parse("2");
            ChartSummary_DeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.Width = float.Parse("98");
            ChartSummary_DeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.Height = float.Parse("97");

            ChartSummary_DeltaE.ChartAreas["ChartArea1"].ShadowOffset = 0;
            ChartSummary_DeltaE.BorderSkin.SkinStyle = BorderSkinStyle.None;
            ChartSummary_DeltaE.ChartAreas["ChartArea1"].BorderWidth = 0;
            ChartSummary_DeltaE.BorderSkin.FrameBorderWidth = 0;



            #endregion

            #region " === Fill Series === "
            // === Limit with red marker === //
            StringBuilder SBDeltaEPatches = new StringBuilder();
            string TooltipTitle = string.Empty;
            if (DT != null && DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    SBDeltaEPatches.Append("<tr style='height: 10px'>");
                    if (Convert.ToDecimal(DT.Rows[i]["ΔE"]) < Convert.ToDecimal(DT.Rows[i]["ΔELimit"]))
                        SBDeltaEPatches.Append("<td valign='middle' align='center' style='padding-right: 5px' class='SyncBottomBorder'><img src=\"Images/Proofok.gif\" width='19px' height='19px' ></td>");
                    else
                        SBDeltaEPatches.Append("<td valign='middle' align='center' style='padding-right: 5px' class='SyncBottomBorder'><img src=\"Images/ProofFailed.gif\" width='19px' height='19px' ></td>");

                    if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString())
                    {
                        SBDeltaEPatches.Append("<td valign='middle' class='SyncBottomBorder' style='padding-top:10px; font-family:Tahoma; font-size:13px; padding-bottom:7px;'>Cyan</td>");
                        TooltipTitle = "Cyan Patch Metrics";
                    }
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString())
                    {
                        SBDeltaEPatches.Append("<td valign='middle' class='SyncBottomBorder' style='padding-top:10px; font-family:Tahoma; font-size:13px; padding-bottom:5px;'>Magenta</td>");
                        TooltipTitle = "Magenta Patch Metrics";
                    }
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString())
                    {
                        SBDeltaEPatches.Append("<td valign='middle' class='SyncBottomBorder' style='padding-top:10px; font-family:Tahoma; font-size:13px; padding-bottom:7px;'>Yellow</td>");
                        TooltipTitle = "Yellow Patch Metrics";
                    }
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString())
                    {
                        SBDeltaEPatches.Append("<td valign='middle' class='SyncBottomBorder' style='padding-top:10px; font-family:Tahoma; font-size:13px; padding-bottom:5px;'>Black</td>");
                        TooltipTitle = "Black Patch Metrics";
                    }
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString())
                    {
                        SBDeltaEPatches.Append("<td valign='middle' class='SyncBottomBorder' style='padding-top:10px; font-family:Tahoma; font-size:13px; padding-bottom:5px;'>Red</td>");
                        TooltipTitle = "Red Patch Metrics";
                    }
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString())
                    {
                        SBDeltaEPatches.Append("<td valign='middle' class='SyncBottomBorder' style='padding-top:10px; font-family:Tahoma; font-size:13px; padding-bottom:5px;'>Green</td>");
                        TooltipTitle = "Green Patch Metrics";
                    }
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString())
                    {
                        SBDeltaEPatches.Append("<td valign='middle' class='SyncBottomBorder' style='padding-top:10px; font-family:Tahoma; font-size:13px; padding-bottom:7px;'>Blue</td>");
                        TooltipTitle = "Blue Patch Metrics";
                    }
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString())
                    {
                        SBDeltaEPatches.Append("<td valign='middle' class='SyncBottomBorder' style='padding-top:10px; font-family:Tahoma; font-size:13px; padding-bottom:6px;'>Paper</td>");
                        TooltipTitle = "Paper Patch Metrics";
                    }
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString())
                    {
                        SBDeltaEPatches.Append("<td valign='middle' class='SyncBottomBorder' style='padding-top:10px; font-family:Tahoma; font-size:13px; padding-bottom:6px;'>3-C</td>");
                        TooltipTitle = "3-C Patch Metrics";
                    }

                    SBDeltaEPatches.Append("</tr>");
                    //BindDeltaEValueACTTHValue(objSeries0, objSeries1, objSeries2, Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["ΔE"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["ΔELimit"].ToString())), i, DT.Rows[i]["HTMLColor"].ToString());
                    BindDeltaEValueACTTHValue(objSeries0, objSeries1, objSeries2, DT.Rows[i], i, TooltipTitle);
                }
                LitDeltaEPatches.Text = SBDeltaEPatches.ToString();
            }
            #endregion
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }    
    private void BindDeltaEValueACTTHValue(Series objSeries0, Series objSeries1, Series objSeries2, DataRow DR, int Index, string TooltipTitle)
    {
        try
        {

            Double Y1, Y2;
            Y1 = Y2 = 0;

            Y1 = (Convert.ToString(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DR["ΔELimit"].ToString()))) == "" ? 0 : Convert.ToDouble(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DR["ΔELimit"].ToString()))));
            Y2 = (Convert.ToString(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DR["ΔE"].ToString()))) == "" ? 0 : Convert.ToDouble(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DR["ΔE"].ToString()))));

            // === When Actual value is not available then no series will display and no marker will display
            if (Y2 == 0)
                Y1 = Y2;


            Series SeriesActual = objSeries1;
            objSeries0.Points.AddXY((Double)0, Y1);
            if (Y1 > Y2)
            {
                //objSeries0.Points[Index].ToolTip = (Y1 - Y2).ToString();
            }
            objSeries0.Points[Index]["PointWidth"] = "1.0";
            objSeries1.Points.AddXY((Double)0, Y2);

            #region Add Tooltip HTML
            StringBuilder toolTip = new StringBuilder();
            toolTip.Append("<table>");
            toolTip.Append("<tr><td><b>" + TooltipTitle + "</b></td></tr>");
            toolTip.Append("<tr><td><table>");
            toolTip.Append("<tr><td>Actual</td><td>");
            toolTip.Append("L*" + DR["CIELAB_L*"].ToString());
            toolTip.Append(" a*" + DR["CIELAB_a*"].ToString());
            toolTip.Append(" b*" + DR["CIELAB_b*"].ToString());
            toolTip.Append("</td></tr>");
            toolTip.Append("<tr><td>Target</td><td>");
            toolTip.Append("L*" + DR["TargetL*"].ToString());
            toolTip.Append(" a*" + DR["Targeta*"].ToString());
            toolTip.Append(" b*" + DR["Targetb*"].ToString());
            toolTip.Append("</td></tr>");
            toolTip.Append("<tr><td>&Delta;E</td><td>");
            toolTip.Append(DR["ΔE"].ToString());
            toolTip.Append("(Thr." + DR["ΔELimit"].ToString() + ")");
            toolTip.Append("</td></tr>");
            toolTip.Append("<tr><td>&Delta;H</td><td>");
            toolTip.Append(DR["ΔH"].ToString());
            //toolTip.Append("(Thr." + Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DR["ΔELimit"].ToString())).ToString() + ")");
            toolTip.Append("</td></tr>");
            toolTip.Append("</table></td></tr>");
            toolTip.Append("</table>");
            #endregion

            objSeries1.Points[Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";

            //objSeries1.Points[Index].ToolTip = Y2.ToString();
            objSeries2.Points.AddXY((Double)0, Y1);
            objSeries0.Points[Index].BackGradientType = GradientType.DiagonalRight;

            SeriesActual.Points[Index].Color = System.Drawing.ColorTranslator.FromHtml(DR["HTMLColor"].ToString());
            SeriesActual.Points[Index]["PointWidth"] = "1.0";
            //SeriesActual.Points[PointIndex].Href = "#";

            if (Y1 == 0 && Y2 == 0)
            {
                objSeries0.Points[Index].MarkerImage = "";
                objSeries2.Points[Index].MarkerImage = "";
            }
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Fill Density Chart
    private void FillDensityChart(DataTable DT)
    {
        try
        {
            Double CValue = 0;
            

            //========== Clear Points and Show Chart ==========//
            CHRT_SummaryDensity.Visible = true;
            CHRT_SummaryDensity.Series[0].Points.Clear();
            CHRT_SummaryDensity.Series[1].Points.Clear();
            CHRT_SummaryDensity.Series[2].Points.Clear();

            #region //========== Declarations ==========//
            // === Series0 to display Thresold value === //
            Series objSeries0 = (Series)CHRT_SummaryDensity.Series[0];
            objSeries0.XValueType = ChartValueTypes.String;
            objSeries0.YValueType = ChartValueTypes.String;
            objSeries0.ShowInLegend = false;
            objSeries0.Type = SeriesChartType.Column;
            objSeries0.BackGradientType = GradientType.DiagonalRight;

            // === Series1 to display Actual value === //
            Series objSeries1 = (Series)CHRT_SummaryDensity.Series[1];
            objSeries1.XValueType = ChartValueTypes.String;
            objSeries1.YValueType = ChartValueTypes.String;
            objSeries1.ShowInLegend = false;
            objSeries1.Type = SeriesChartType.Column;


            // === Series2 to display red marker on Thresold value === //
            Series objSeries2 = (Series)CHRT_SummaryDensity.Series[2];
            objSeries2.XValueType = ChartValueTypes.String;
            objSeries2.YValueType = ChartValueTypes.String;
            objSeries2.ShowInLegend = false;
            objSeries2.Type = SeriesChartType.Column;

            // === Make Chart area look like 2D even it is 3D === //
            CHRT_SummaryDensity.ChartAreas["ChartArea1"].Area3DStyle.RightAngleAxes = true;
            CHRT_SummaryDensity.ChartAreas["ChartArea1"].Area3DStyle.Clustered = true;
            CHRT_SummaryDensity.ChartAreas["ChartArea1"].Area3DStyle.XAngle = 0;
            CHRT_SummaryDensity.ChartAreas["ChartArea1"].Area3DStyle.YAngle = 0;
            #endregion

            if (DT != null && DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    CValue = Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["Density"]));
                    if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString())
                        AddDensityPoint(i, objSeries0, objSeries1, objSeries2, "C", CValue.ToString(), DT.Rows[i]["HTMLColor"].ToString(), DT.Rows[i]["Density"].ToString(), DT.Rows[i]["TargetDens"].ToString(), "Cyan", true);
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString())
                        AddDensityPoint(i, objSeries0, objSeries1, objSeries2, "M", CValue.ToString(), DT.Rows[i]["HTMLColor"].ToString(), DT.Rows[i]["Density"].ToString(), DT.Rows[i]["TargetDens"].ToString(), "Magenta", true);
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString())
                        AddDensityPoint(i, objSeries0, objSeries1, objSeries2, "Y", CValue.ToString(), DT.Rows[i]["HTMLColor"].ToString(), DT.Rows[i]["Density"].ToString(), DT.Rows[i]["TargetDens"].ToString(), "Yellow", true);
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString())
                        AddDensityPoint(i, objSeries0, objSeries1, objSeries2, "K", CValue.ToString(), DT.Rows[i]["HTMLColor"].ToString(), DT.Rows[i]["Density"].ToString(), DT.Rows[i]["TargetDens"].ToString(), "Black", true);
                }

            }
            else
            {
                CHRT_SummaryDensity.ChartAreas["Default"].AxisY.Minimum = -10;
                CHRT_SummaryDensity.ChartAreas["Default"].AxisY.Maximum = 10;

                AddDensityPoint(0, objSeries0, objSeries1, objSeries2, "C", "0", "#E9E9E9", "0", "0", "", false);
                AddDensityPoint(1, objSeries0, objSeries1, objSeries2, "M", "0", "#E9E9E9", "0", "0", "", false);
                AddDensityPoint(2, objSeries0, objSeries1, objSeries2, "Y", "0", "#E9E9E9", "0", "0", "", false);
                AddDensityPoint(3, objSeries0, objSeries1, objSeries2, "K", "0", "#E9E9E9", "0", "0", "", false);
            }


        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }    
    private void AddDensityPoint(int Index, Series objSeries0, Series objSeries1, Series objSeries2, string _XValue, string _YValue, string HTMLColor, string _Actual, string _Target, string ColorName, bool isTooltip)
    {
        try
        {
            objSeries0.Points.AddXY(_XValue, _Target);
            objSeries1.Points.AddXY(_XValue, _YValue);
            StringBuilder toolTip = new StringBuilder();
            if (isTooltip)
            {
                #region Add Tooltip HTML
                toolTip.Append("<table>");
                toolTip.Append("<tr><td><b>" + ColorName + " Patch Metrics</b></td></tr>");
                toolTip.Append("<tr><td><table>");
                toolTip.Append("<tr><td>Actual</td><td>");
                toolTip.Append(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_Actual)).ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>Target</td><td>");
                toolTip.Append(_Target);
                toolTip.Append("</td></tr>");
                toolTip.Append("</table></td></tr>");
                toolTip.Append("</table>");
                #endregion
            }
            objSeries1.Points[Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
            objSeries2.Points.AddXY(_XValue, _Target);
            objSeries1.Points[Index].Color = System.Drawing.ColorTranslator.FromHtml(HTMLColor);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Fill Papaer Chart
    private void FillPaperChart(DataTable DT)
    {
        try
        {
            if (DT != null && DT.Rows.Count > 0)
            {
                #region Fill Paper 1 Chart
                //CHRT_ISOPaper1.Series["Green"].Points.AddXY(Convert.ToDouble(DT.Rows[0]["Targeta*"]), Convert.ToDouble(DT.Rows[0]["Targetb*"]));
                CHRT_Paper1.Series["Green"].Points.AddXY(Convert.ToDouble(0), Convert.ToDouble(0));
                CHRT_Paper1.Series["Green"].Type = SeriesChartType.Point;
                CHRT_Paper1.Series["Green"].Points[0].BorderWidth = 2;                

                StringBuilder toolTip = new StringBuilder();
                #region Add Tooltip HTML
                toolTip.Append("<table>");
                toolTip.Append("<tr><td><b>Paper Patch Metrics</b></td></tr>");
                toolTip.Append("<tr><td><table>");
                toolTip.Append("<tr><td>Actual</td><td>");
                toolTip.Append("L*" + DT.Rows[0]["CIELAB_L*"].ToString());
                toolTip.Append(" a*" + DT.Rows[0]["CIELAB_a*"].ToString());
                toolTip.Append(" b*" + DT.Rows[0]["CIELAB_b*"].ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>Target</td><td>");
                toolTip.Append("L*" + DT.Rows[0]["TargetL*"].ToString());
                toolTip.Append(" a*" + DT.Rows[0]["Targeta*"].ToString());
                toolTip.Append(" b*" + DT.Rows[0]["Targetb*"].ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>&Delta;L*</td><td>");
                toolTip.Append(DT.Rows[0]["ΔL*"].ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>&Delta;a*</td><td>");
                toolTip.Append(DT.Rows[0]["Δa*"].ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>&Delta;b*</td><td>");
                toolTip.Append(DT.Rows[0]["Δb*"].ToString());
                toolTip.Append("</td></tr>");

                toolTip.Append("</table></td></tr>");
                toolTip.Append("</table>");
                #endregion

                CHRT_Paper1.Series["Paper"].Points.AddXY(Convert.ToDouble(DT.Rows[0]["Δa*"]), Convert.ToDouble(DT.Rows[0]["Δb*"]));
                CHRT_Paper1.Series["Paper"].Type = SeriesChartType.Point;
                CHRT_Paper1.Series["Paper"].Points[0].BorderWidth = 4;
                CHRT_Paper1.Series["Paper"].Points[0].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                #endregion

                #region Fill Paper 2 chart

                #region Line Chart Series For Dispaly Line on 0 value
                CHRT_Paper2.Series["Series2"].Type = SeriesChartType.Line;
                CHRT_Paper2.Series["Series2"].XAxisType = AxisType.Secondary;
                CHRT_Paper2.Series["Series2"].Points.AddXY(0, 0);
                CHRT_Paper2.Series["Series2"].Points.AddXY(1, 0);
                #endregion

                CHRT_Paper2.Series["Series1"].Points.AddY(Convert.ToDouble(DT.Rows[0]["ΔL*"]));                
                CHRT_Paper2.Series["Series1"].Points[0].Color = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                CHRT_Paper2.Series["Series1"].Points[0].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                #endregion
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #endregion

    #region G7 Chart
    private void FillG7Chart(DataTable GrayFChart, DataTable GrayQuartileSummary, DataTable NPDChart1, DataTable NPDChart2)
    {
        try
        {
            if (GrayFChart != null)
                FillGrayFChart(GrayFChart);
            if (GrayQuartileSummary != null)
                FillGrayQuartileSummary(GrayQuartileSummary);
            if (NPDChart1 != null && NPDChart2 != null)
                FillNPDChart(NPDChart1,NPDChart2);


        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }

    #region Fill Gray F Chart
    private void FillGrayFChart(DataTable DT)
    {
        try
        {
            Chart_GrayF.Series["Series1"].Points.Clear();
            Chart_GrayF.Series["Series1"]["TooltipOffset"] = "True";
            if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    AddPointChart(i, Chart_GrayF.Series["Series1"], Chart_GrayF.Series["Series1Point"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["ΔF"].ToString(), Color.Black.Name.ToString(), true);
                    AddPointChart(i, Chart_GrayF.Series["Series2"], Chart_GrayF.Series["Series2Point"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["ΔFLimit"].ToString(), Color.Red.Name.ToString(), false);
                }
            }
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }

    #endregion

    #region Fill Gray Quartile Summary

    private void FillGrayQuartileSummary(DataTable Dt)
    {
        rptGrayQuartileSummary.DataSource = Dt;
        rptGrayQuartileSummary.DataBind();
    }

    #endregion

    #region Fill NPD Chart
    private void FillNPDChart(DataTable DTBlack, DataTable DTGray)
    {
        try
        {
            CHRT_BlackNPD.Series["Series0"].Points.Clear();
            CHRT_BlackNPD.Series["Series1"].Points.Clear();

            // There is an issue with tooltip on spline chart, by default it ignores 
            //http://support2.dundas.com/OnlineDocumentation/WebChart2003/CustomAttributes_TooltipOffset.html
            CHRT_BlackNPD.Series["Series0"]["TooltipOffset"]   = "True";
            CHRT_BlackNPD.Series["Series1"]["TooltipOffset"] = "True";
            if (DTBlack != null && DTBlack.Rows.Count > 0 && DTBlack.Rows[0][0].ToString() != "NO DATA")
            {
                for (int i = 0; i < DTBlack.Rows.Count; i++)
                {
                    AddNPDPointChart(i, CHRT_BlackNPD.Series["Series0"], Convert.ToInt64(DTBlack.Rows[i]["CMYK_K"]).ToString(), DTBlack.Rows[i]["Density"].ToString(), Color.Black.Name.ToString(),"Actual NPD","Density");
                    if (DTBlack.Rows[i]["G7NPDC"] != DBNull.Value)
                    {
                        AddNPDPointChart(i, CHRT_BlackNPD.Series["Series1"], Convert.ToInt64(DTBlack.Rows[i]["CMYK_K"]).ToString(), DTBlack.Rows[i]["G7NPDC"].ToString(), Color.Green.Name.ToString(),"Target NPD","G7NPDC");
                    }

                }
            }

            CHRT_GrayNPD.Series["Series0"].Points.Clear();
            CHRT_GrayNPD.Series["Series1"].Points.Clear();

            CHRT_GrayNPD.Series["Series0"]["TooltipOffset"] = "True";
            CHRT_GrayNPD.Series["Series1"]["TooltipOffset"] = "True";
            if (DTGray != null && DTGray.Rows.Count > 0 && DTGray.Rows[0][0].ToString() != "NO DATA")
            {
                for (int i = 0; i < DTGray.Rows.Count; i++)
                {
                    AddNPDPointChart(i, CHRT_GrayNPD.Series["Series0"],  Convert.ToInt64(DTGray.Rows[i]["CMYK_C"]).ToString(), DTGray.Rows[i]["Density"].ToString(), Color.Black.Name.ToString(),"Gray NPD","Density");
                    if (DTGray.Rows[i]["G7NPDC"] != DBNull.Value)
                    {
                        AddNPDPointChart(i, CHRT_GrayNPD.Series["Series1"], Convert.ToInt64(DTGray.Rows[i]["CMYK_C"]).ToString(), DTGray.Rows[i]["G7NPDC"].ToString(), Color.Green.Name.ToString(),"Gray Target NPD","G7NPDC");
                    }
                }
            }
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }

    #endregion

    private void AddPointChart(int _Index, Series _objSeries, Series _objSeriesPoint, string _XValue, string _YValue, string ColorName, bool showCircle)
    {
        try
        {
            _objSeries.Type = SeriesChartType.Spline;
            _objSeries.Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.None;
            _objSeries.Points[_Index].BorderWidth = 1;

             
            _objSeriesPoint.Type = SeriesChartType.Point;
            _objSeriesPoint.Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));            
            _objSeriesPoint.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
            _objSeriesPoint.Points[_Index].MarkerStyle = showCircle ? MarkerStyle.Circle : MarkerStyle.None;
            _objSeriesPoint.Points[_Index].MarkerSize = showCircle ?5 : 0;
            _objSeriesPoint.Points[_Index].BorderWidth = 1;
            _objSeriesPoint.Points[_Index].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
            

            #region Add Tooltip HTML
            StringBuilder toolTip = new StringBuilder();
            toolTip.Append("<table>");
            toolTip.Append("<tr><td><b>" + _XValue + "% Patch Metrics</b></td></tr>");
            toolTip.Append("<tr><td><table>");
            if (!showCircle) { toolTip.Append("<tr><td>ΔF* Threshold : </td><td>"); }
            else { toolTip.Append("<tr><td>ΔF* : </td><td>"); };
            toolTip.Append(_YValue);
            toolTip.Append("</td></tr>");
            toolTip.Append("</table></td></tr>");
            toolTip.Append("</table>");
            #endregion            
            _objSeriesPoint.Points[_Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    private void AddNPDPointChart(int _Index, Series _objSeries, string _XValue, string _YValue, string ColorName,string TitleName,string ValueName)
    {
        try
        {
            _objSeries.Type = SeriesChartType.Spline;
          
            _objSeries.Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));            
            _objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.None;
            _objSeries.Points[_Index].MarkerSize = 1;  //changed 0 by chirag on 10/23/10 as we will drawing more points and make smooth curve 
            _objSeries.Points[_Index].BorderWidth = 1;
            _objSeries.Points[_Index].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(ColorName);

            #region Add Tooltip HTML
            StringBuilder toolTip = new StringBuilder();
            toolTip.Append("<table>");
            toolTip.Append("<tr><td><b> "+TitleName+" " + _XValue + "% Patch Metrics</b></td></tr>");
            toolTip.Append("<tr><td><table>");
            toolTip.Append("<tr><td>"+ValueName+" : </td><td>");
            toolTip.Append(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)).ToString());
            toolTip.Append("</td></tr>");
            toolTip.Append("</table></td></tr>");
            toolTip.Append("</table>");
            #endregion            
            _objSeries.Points[_Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";

        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }

    #endregion

    #region Chart Table   
    private void FillChartTable(DataTable DTShowDownloadIcon, DataTable DTMeasurementData)
    {
        try
        {         
            //add by chirag on 03/30/2011  only show download icon if measurement file (file with actual spectral data is available) FileFound = "1"
            if (DTShowDownloadIcon.Rows[0]["FileFound"].ToString() == "0") btn_DownloadFile.Visible = false;  
            GV_MeasurementData.DataSource = DTMeasurementData;           
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }

    protected void GV_MeasurementData_SortCommand(object sender, Telerik.WebControls.GridSortCommandEventArgs e)
    {
        //========== Check Sort expression exist or not ===========//
        if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
        {
            //========== Declare the sort expression object ===========//
            Telerik.WebControls.GridSortExpression SortExpr = new Telerik.WebControls.GridSortExpression();

            //========== Set the sort expression value  ===========//
            SortExpr.FieldName = e.SortExpression;

            //========== Add sort expression value ===========//
            e.Item.OwnerTableView.SortExpressions.AddSortExpression(SortExpr);
        }
    }

    protected void GV_MeasurementData_ItemDataBound(object sender, Telerik.WebControls.GridItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == Telerik.WebControls.GridItemType.AlternatingItem || e.Item.ItemType == Telerik.WebControls.GridItemType.Item)
            {
                DataRowView DRV = (DataRowView)e.Item.DataItem;
                System.Web.UI.WebControls.Label LBL = (System.Web.UI.WebControls.Label)e.Item.FindControl("LBL_Color");
                LBL.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                LBL.Style.Add("background-color", DRV["HTMLColor"].ToString());
            }
        }
        catch (Exception Exc)
        { LblError.Text = HandleException(Exc); }
    }

    protected void GV_MeasurementData_NeedDataSource(object source, Telerik.WebControls.GridNeedDataSourceEventArgs e)
    {
       
            /*** Changed By Chirag Shah on 10th Sept - 2010 **/
            FillAllTabCharts(Convert.ToInt32(Request.QueryString["MeasurementID"]));
            TabContainer1.ActiveTabIndex = 2;
    }

    protected void btn_Excel_Click(object sender, EventArgs e)
    {


        try
        {
        //Response.Clear();

        //Response.AddHeader("content-disposition", "attachment;filename=MeasurementData.xls");

        //Response.Charset = "";

        //// If you want the option to open the Excel file without saving than
        //// comment out the line below
        //// Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //Response.ContentType = "application/vnd.xls";

        //System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        //System.Web.UI.HtmlTextWriter htmlWrite =
        //new HtmlTextWriter(stringWrite);

        //GV_MeasurementData.RenderControl(htmlWrite);

        //Response.Write(stringWrite.ToString());

        //Response.End();        
        //GV_MeasurementData.ExportSettings.IgnorePaging = true;
        //GV_MeasurementData.ExportSettings.OpenInNewWindow = true;
        //GV_MeasurementData.MasterTableView.ExportToExcel("vnd");

        /*** implement logic to create a memoery stream and write file with data and download it ***/
        //get the measurment data Convert.ToInt32(Request.QueryString["MeasurementID"])

        // ===== Create Object of CPSyncMeasurementData  ======//
        CPSyncMeasurementData objCPSyncMeasurementData = new CPSyncMeasurementData();

        int _MsmtID = Convert.ToInt32(Request.QueryString["MeasurementID"]);

        // ===== Get Data from data base  ==================//
        objCPSyncMeasurementData.MeasurementID = _MsmtID;
        DataTable DT = objCPSyncMeasurementData.uspGetMeasurementData();

        if (DT.Rows.Count > 0)  {
            
                System.IO.StreamWriter sw;
                sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + _MsmtID.ToString() + "_MeasurementFile.txt");
                sw.WriteLine(@"KEYWORD	""SampleID""");
                sw.WriteLine(@"KEYWORD	""SAMPLE_NAME""");
                sw.WriteLine("NUMBER_OF_FIELDS" + "\t" +"16");
                sw.WriteLine("BEGIN_DATA_FORMAT");
                sw.WriteLine("SampleID\tSAMPLE_NAME\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K\tXYZ_X\tXYZ_Y\tXYZ_Z\tLAB_L\tLAB_A\tLAB_B\tD_RED\tD_GREEN\tD_BLUE\tD_VIS");
                sw.WriteLine("END_DATA_FORMAT");
                sw.WriteLine("NUMBER_OF_SETS" + "\t" +	DT.Rows.Count.ToString());
                sw.WriteLine("BEGIN_DATA");

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    sw.WriteLine(DT.Rows[i]["SampleID"].ToString() + "\t" + DT.Rows[i]["SAMPLE_NAME"].ToString() + "\t" + DT.Rows[i]["CMYK_C"].ToString() + "\t" + DT.Rows[i]["CMYK_M"].ToString() + "\t" + DT.Rows[i]["CMYK_Y"].ToString() + "\t" + DT.Rows[i]["CMYK_K"].ToString() + "\t" + DT.Rows[i]["CIEXYZ_X"].ToString() + "\t" + DT.Rows[i]["CIEXYZ_Y"].ToString() + "\t" + DT.Rows[i]["CIEXYZ_Z"].ToString() + "\t" + DT.Rows[i]["LAB_L"].ToString() + "\t" + DT.Rows[i]["LAB_A"].ToString() + "\t" + DT.Rows[i]["LAB_B"].ToString() + "\t" + DT.Rows[i]["D_Red"].ToString() + "\t" + DT.Rows[i]["D_Green"].ToString() + "\t" + DT.Rows[i]["D_Blue"].ToString() + "\t" + DT.Rows[i]["D_Vis"].ToString());
                }
 
                sw.WriteLine("END_DATA");
                sw.Close();

                Fuji.Common.CommonFunctions.DownlaodFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), _MsmtID.ToString() + "_MeasurementFile.txt", _MsmtID.ToString() + "_MeasurementFile.txt");
            }
        }
        catch (Exception Exc)
        {
            HandleException(Exc);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Confirms that an HtmlForm control is rendered for the
        //specified ASP.NET server control at run time.
    }

    protected void btn_DownloadFile_Click(object sender, EventArgs e)
    {
        try
        {
            // ===== Create Object of CPSyncMeasurementData  ======//
            CPSyncMeasurementData objCPSyncMeasurementData = new CPSyncMeasurementData();

            // ===== Get Data from data base  ==================//
            objCPSyncMeasurementData.MeasurementID = Convert.ToInt32(Request.QueryString["MeasurementID"]);
            DataTable DT = objCPSyncMeasurementData.uspGetMeasurementFile();

            if (DT.Rows.Count > 0)
            {

                Response.AddHeader("Content-disposition", "attachment; filename=" + DT.Rows[0]["MeasurementName"] + "");
                Response.ContentType = "application/octet-stream";

                Response.Write(DT.Rows[0]["MeasurementFile"].ToString());
                Response.End();  //By Chirag why not to use HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            else
            {

            }

        }
        catch (Exception Exc)
        {
            LblError.Text = HandleException(Exc);
        }
    }

    #endregion

    #region Fill Lab Tab
    private void FillLabCharts(DataTable DTLABChart, DataTable DTGaumatChart1, DataTable DTGaumatChart2)
    {
        try
        {          
            if (DTLABChart != null)
                FillLABChart(DTLABChart);
            if (DTGaumatChart1 != null && DTGaumatChart2 != null)
                FillGaumatChart(DTGaumatChart1, DTGaumatChart2);
            
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }

    #region Fill L* a* b* Chart
    private void FillLABChart(DataTable DT)
    {
        try
        {
            Chart_L.Series["Series1"].Points.Clear();
            Chart_a.Series["Series1"].Points.Clear();
            Chart_b.Series["Series1"].Points.Clear();
            Chart_L.Series["Series2"].Points.Clear();
            Chart_a.Series["Series2"].Points.Clear();
            Chart_b.Series["Series2"].Points.Clear();

            if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    AddPointLABChart(i, Chart_L.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["CIELAB_L*"].ToString(), Color.Black.Name.ToString(), DT.Rows[i], true);
                    AddPointLABChart(i, Chart_a.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["CIELAB_a*"].ToString(), Color.Black.Name.ToString(), DT.Rows[i], true);
                    AddPointLABChart(i, Chart_b.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["CIELAB_b*"].ToString(), Color.Black.Name.ToString(), DT.Rows[i], true);

                    if (DT.Rows[i]["Target_L*"] != DBNull.Value)
                    {
                        AddPointLABChart(i, Chart_L.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["Target_L*"].ToString(), Color.Green.Name.ToString(), DT.Rows[i], false);
                    }
                    AddPointLABChart(i, Chart_a.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["Target_a*"].ToString(), Color.Green.Name.ToString(), DT.Rows[i], false);
                    AddPointLABChart(i, Chart_b.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["Target_b*"].ToString(), Color.Green.Name.ToString(), DT.Rows[i], false);
                }
            }
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    private void AddPointLABChart(int _Index, Series _objSeries, string _XValue, string _YValue, string ColorName, DataRow DR, bool isActual)
    {
        try
        {
            StringBuilder toolTip = new StringBuilder();
            if (isActual)
            {
                #region Add Tooltip HTML
                toolTip.Append("<table>");
                toolTip.Append("<tr><td><b>" + _XValue + "% GB Patch Metrics</b></td></tr>");
                toolTip.Append("<tr><td><table>");
                toolTip.Append("<tr><td>Actual</td><td>");
                toolTip.Append("L*" + DR["CIELAB_L*"].ToString());
                toolTip.Append(" a*" + DR["CIELAB_a*"].ToString());
                toolTip.Append(" b*" + DR["CIELAB_b*"].ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>Target</td><td>");
                toolTip.Append("L*" + DR["Target_L*"].ToString());
                toolTip.Append(" a*" + DR["Target_a*"].ToString());
                toolTip.Append(" b*" + DR["Target_b*"].ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>&Delta;E</td><td>");
                toolTip.Append(DR["ΔE"].ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>&Delta;H</td><td>");
                toolTip.Append(DR["ΔH"].ToString());
                toolTip.Append("</td></tr>");

                toolTip.Append("<tr><td>&Delta;L*</td><td>");
                toolTip.Append(DR["ΔL*"].ToString());
                toolTip.Append("</td></tr>");

                toolTip.Append("<tr><td>&Delta;a*</td><td>");
                toolTip.Append(DR["Δa*"].ToString());
                toolTip.Append("</td></tr>");

                toolTip.Append("<tr><td>&Delta;b*</td><td>");
                toolTip.Append(DR["Δb*"].ToString());
                toolTip.Append("</td></tr>");

                toolTip.Append("</table></td></tr>");
                toolTip.Append("</table>");
                #endregion
            }

            _objSeries.Type = SeriesChartType.Spline;
            _objSeries.Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));
            //_objSeries.Points[_Index].ToolTip = "[" + Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)).ToString() + "," + Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)).ToString() + "]";
            if (isActual)
                _objSeries.Points[_Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";

            _objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
            _objSeries.Points[_Index].MarkerSize = 5;
            _objSeries.Points[_Index].BorderWidth = 1;
            _objSeries.Points[_Index].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Fill Gaumat Chart
    private void FillGaumatChart(DataTable DTTarget, DataTable DTColors)
    {
        DTTarget.DefaultView.RowFilter = "PatchType = '" + Enums.CPSyncPatchType.Paper.ToString() + "'";
        DataView dv = DTTarget.DefaultView;
        Double Papera = 0;
        Double Paperb = 0;
        if (dv.Count > 0)
        {
            Papera = Convert.ToDouble(dv[0]["CIELAB_a*"]);
            Paperb = Convert.ToDouble(dv[0]["CIELAB_b*"]);

        }
        //Set the colors first
        BindLABComparision("Cyan", DTColors, Papera, Paperb, 5, false, false);
        BindLABComparision("Magenta", DTColors, Papera, Paperb, 5, false, false);
        BindLABComparision("Yellow", DTColors, Papera, Paperb, 5, false, false);
        
        BindLABComparision("Cyan", DTTarget,0,0, 9, true, false);
        BindLABComparision("Magenta", DTTarget,0,0, 9, true, false);
        BindLABComparision("Yellow", DTTarget, 0, 0, 9, true, false);
        BindLABComparision("Red", DTTarget, 0, 0, 9, true, false);
        BindLABComparision("Green", DTTarget, 0, 0, 9, true, false);
        BindLABComparision("Blue", DTTarget, 0, 0, 9, true, false);
        BindLABComparision("Paper", DTTarget, 0, 0, 9, true, false);

        BindLABComparision("Cyan", DTTarget, 0, 0, 6, false, true);
        BindLABComparision("Magenta", DTTarget, 0, 0, 6, false, true);
        BindLABComparision("Yellow", DTTarget, 0, 0, 6, false, true);
        BindLABComparision("Red", DTTarget, 0, 0, 6, false, true);
        BindLABComparision("Green", DTTarget, 0, 0, 6, false, true);
        BindLABComparision("Blue", DTTarget, 0, 0, 6, false, true);
        BindLABComparision("Paper", DTTarget, 0, 0, 6, false, true);
    }

    private void BindLABComparision(string ColorName, DataTable DT, Double Papera, Double Paperb, int MarkerSize, bool isTarget, bool isActual)
    {
        StringBuilder toolTip = new StringBuilder();
        DT.DefaultView.RowFilter = "PatchType = '" + ColorName + "'";
        DataView dv = DT.DefaultView;
        for (int i = 0; i < dv.Count; i = i + 1)
        {
            #region Bind AB Comparison
            if (i == 0 && !isTarget && !isActual)
            {
                CHART_ABComparison.Series[ColorName].Type = SeriesChartType.Line;
                CHART_ABComparison.Series[ColorName].Points.AddXY(Papera, Paperb);
            }

            int pointCount = ((isTarget) ? CHART_ABComparison.Series["Targets"].Points.Count : CHART_ABComparison.Series[ColorName].Points.Count);
        

            if (isTarget || isActual)
            {
                #region Add Tooltip HTML
                toolTip.Append("<table>");
                toolTip.Append("<tr><td><b>" + ColorName + " Patch Metrics</b></td></tr>");
                toolTip.Append("<tr><td><table>");
                toolTip.Append("<tr><td>Actual</td><td>");
                toolTip.Append("L*" + dv[i]["CIELAB_L*"].ToString());
                toolTip.Append(" a*" + dv[i]["CIELAB_a*"].ToString());
                toolTip.Append(" b*" + dv[i]["CIELAB_b*"].ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>Target</td><td>");
                toolTip.Append("L*" + dv[i]["TargetL*"].ToString());
                toolTip.Append(" a*" + dv[i]["Targeta*"].ToString());
                toolTip.Append(" b*" + dv[i]["Targetb*"].ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>&Delta;E</td><td>");
                toolTip.Append(dv[i]["ΔE"].ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>&Delta;H</td><td>");
                toolTip.Append(dv[i]["ΔH"].ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>&Delta;L*</td><td>");
                toolTip.Append(dv[i]["L*Deviation"].ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>&Delta;a*</td><td>");
                toolTip.Append(dv[i]["Δa*"].ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("<tr><td>&Delta;b*</td><td>");
                toolTip.Append(dv[i]["Δb*"].ToString());
                toolTip.Append("</td></tr>");
                toolTip.Append("</table></td></tr>");
                toolTip.Append("</table>");
                #endregion
            }

            
            if (isTarget)
            {
                CHART_ABComparison.Series["Targets"].Type = SeriesChartType.FastPoint;
                CHART_ABComparison.Series["Targets"].Points.AddXY(Convert.ToDouble(dv[i]["Targeta*"]), Convert.ToDouble(dv[i]["Targetb*"]));
                CHART_ABComparison.Series["Targets"].MarkerSize = MarkerSize;
                CHART_ABComparison.Series["Targets"].MarkerColor = Color.Green;
            }
            else
            {
                CHART_ABComparison.Series[ColorName].Type = SeriesChartType.Line;
                CHART_ABComparison.Series[ColorName].Points.AddXY(Convert.ToDouble(dv[i]["CIELAB_a*"]), Convert.ToDouble(dv[i]["CIELAB_b*"]));
            }

            
            if (i == dv.Count - 1 && (isTarget || isActual))
            {
                if (isActual)
                {
                    CHART_ABComparison.Series[ColorName].Points[pointCount].BorderWidth = 1;
                    CHART_ABComparison.Series[ColorName].Points[pointCount].MarkerSize = MarkerSize;
                    CHART_ABComparison.Series[ColorName].Points[pointCount].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";                
                    CHART_ABComparison.Series[ColorName].Points[pointCount].Color = (Color)new ColorConverter().ConvertFromString(ColorName.Equals("paper", StringComparison.CurrentCultureIgnoreCase) ? "White" : ColorName);
                }
            }
            else
            {
                CHART_ABComparison.Series[ColorName].Points[pointCount].BorderWidth = 1;
                CHART_ABComparison.Series[ColorName].Points[pointCount].MarkerSize = 0;
                CHART_ABComparison.Series[ColorName].Points[pointCount].Color = (Color)new ColorConverter().ConvertFromString(ColorName.Equals("paper", StringComparison.CurrentCultureIgnoreCase) ? "White" : ColorName);                
            }
            #endregion

            //Bind L Comparision
            #region Bind L Comparision
            if (isTarget)
            {
                int series = 0;
                if (Convert.ToString(dv[0]["PatchType"]).ToLower() == "paper")
                {
                    series = 2;
                }

                pointCount = CHART_LComparison.Series[series].Points.Count;

                CHART_LComparison.Series[series].Points.AddXY(Convert.ToDouble(dv[0]["CIELAB_a*"]), Convert.ToDouble(0));
                CHART_LComparison.Series[series].Type = SeriesChartType.Point;
                CHART_LComparison.Series[series].Points[i].BorderWidth = 2;
                CHART_LComparison.Series[series].Points[pointCount].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                

                CHART_LComparison.Series[series + 1].Points.AddXY(Convert.ToDouble(dv[0]["CIELAB_a*"]), Convert.ToDouble(dv[0]["L*Deviation"]));
                CHART_LComparison.Series[series + 1].Type = SeriesChartType.Point;
                CHART_LComparison.Series[series + 1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                CHART_LComparison.Series[series + 1].Points[i].BorderWidth = 2;
                CHART_LComparison.Series[series].Points[pointCount].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
                

                if (Convert.ToString(dv[0]["PatchType"]).ToLower() == "paper")
                {
                    CHART_LComparison.Series[series].Points[pointCount].MarkerColor = Color.White;
                    CHART_LComparison.Series[series + 1].Points[pointCount].MarkerColor = Color.White;
                }
                else
                {
                    CHART_LComparison.Series[series].Points[pointCount].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(dv[0]["PatchType"]));
                    CHART_LComparison.Series[series + 1].Points[pointCount].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(dv[0]["PatchType"]));
                }
                CHART_LComparison.Series[series].Points[pointCount].MarkerSize = 9;
                CHART_LComparison.Series[series + 1].Points[pointCount].MarkerSize = 5;
            }
            #endregion
        }
    }

    #endregion

    #endregion

    #region Fill TVI Tab
    private void FillTVIChart(DataTable DT)
    {
        try
        {
            CHRT_SummaryDotgain.Series["Series1"].Points.Clear();
            CHRT_SummaryDotgain.Series["Series2"].Points.Clear();
            CHRT_SummaryDotgain.Series["Series3"].Points.Clear();
            CHRT_SummaryDotgain.Series["Series4"].Points.Clear();
            //Double CValue = 0;
            //Double MaxValue = 0;
            //Double MinValue = 0;

            if (DT != null && DT.Rows.Count > 0)
            {
                int C = 0;
                int M = 0;
                int Y = 0;
                int K = 0;


                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (Convert.ToInt16(DT.Rows[i]["CMYK_C"]) == 0 && Convert.ToInt16(DT.Rows[i]["CMYK_M"]) == 0 && Convert.ToInt16(DT.Rows[i]["CMYK_Y"]) == 0 && Convert.ToInt16(DT.Rows[i]["CMYK_K"]) == 0)
                    {
                        AddPointChartWithColor(C, CHRT_SummaryDotgain.Series["Series1"], CHRT_SummaryDotgain.Series["Series1Point"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Cyan.Name.ToString());
                        AddPointChartWithColor(M, CHRT_SummaryDotgain.Series["Series2"], CHRT_SummaryDotgain.Series["Series2Point"], Convert.ToInt64(DT.Rows[i]["CMYK_M"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Magenta.Name.ToString());
                        AddPointChartWithColor(Y, CHRT_SummaryDotgain.Series["Series3"], CHRT_SummaryDotgain.Series["Series3Point"], Convert.ToInt64(DT.Rows[i]["CMYK_Y"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Yellow.Name.ToString());
                        AddPointChartWithColor(K, CHRT_SummaryDotgain.Series["Series4"], CHRT_SummaryDotgain.Series["Series4Point"], Convert.ToInt64(DT.Rows[i]["CMYK_K"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Black.Name.ToString());
                        C++; M++; Y++; K++;
                    }
                    if (Convert.ToInt16(DT.Rows[i]["CMYK_C"]) != 0)
                    {
                        AddPointChartWithColor(C, CHRT_SummaryDotgain.Series["Series1"], CHRT_SummaryDotgain.Series["Series1Point"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Cyan.Name.ToString());
                        C++;
                    }
                    if (Convert.ToInt16(DT.Rows[i]["CMYK_M"]) != 0)
                    {
                        AddPointChartWithColor(M, CHRT_SummaryDotgain.Series["Series2"], CHRT_SummaryDotgain.Series["Series2Point"], Convert.ToInt64(DT.Rows[i]["CMYK_M"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Magenta.Name.ToString());
                        M++;
                    }
                    if (Convert.ToInt16(DT.Rows[i]["CMYK_Y"]) != 0)
                    {
                        AddPointChartWithColor(Y, CHRT_SummaryDotgain.Series["Series3"], CHRT_SummaryDotgain.Series["Series3Point"], Convert.ToInt64(DT.Rows[i]["CMYK_Y"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Yellow.Name.ToString());
                        Y++;
                    }
                    if (Convert.ToInt16(DT.Rows[i]["CMYK_K"]) != 0)
                    {
                        AddPointChartWithColor(K, CHRT_SummaryDotgain.Series["Series4"], CHRT_SummaryDotgain.Series["Series4Point"], Convert.ToInt64(DT.Rows[i]["CMYK_K"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Black.Name.ToString());
                        K++;
                    }
                }
            }
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    //Add Point with color
    private void AddPointChartWithColor(int _Index, Series _objSeries, Series _objSeriesPoint, string _XValue, string _YValue, string ColorName)
    {
        try
        {
            _objSeries.Type = SeriesChartType.Spline;
            _objSeries.Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));            
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.None;
            _objSeries.Points[_Index].BorderWidth = 1;

            _objSeriesPoint.Type = SeriesChartType.Point;
            _objSeriesPoint.Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));
            _objSeriesPoint.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
            _objSeriesPoint.Points[_Index].MarkerStyle = MarkerStyle.Circle;
            _objSeriesPoint.Points[_Index].MarkerSize = 5;
            _objSeriesPoint.Points[_Index].BorderWidth = 1;
            _objSeriesPoint.Points[_Index].MarkerBorderColor = Color.Black;

            if (Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)) > 0)
            {
                #region Add Tooltip HTML
                StringBuilder toolTip = new StringBuilder();
                toolTip.Append("<table>");
                toolTip.Append("<tr><td><b>" + ColorName + " " + _XValue +"% Patch Metrics</b></td></tr>");
                toolTip.Append("<tr><td><table>");                
                toolTip.Append("<tr><td>Dot Gain : </td><td>");
                toolTip.Append(_YValue);
                toolTip.Append("</td></tr>");
                toolTip.Append("</table></td></tr>");
                toolTip.Append("</table>");
                #endregion

                //_objSeries.Points[_Index].ToolTip = "(" + Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)).ToString() + "," + Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)).ToString() + ")";
                _objSeriesPoint.Points[_Index].MapAreaAttributes = "onmouseover=\"DisplayTooltip('" + toolTip.ToString() + "');\" onmouseout=\"DisplayTooltip('');\"";
            }
            //_objSeries.Points[_Index].ToolTip = "(" + Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)).ToString() + "," + Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)).ToString() + ")";
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #endregion
}
