using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Fuji.Business;
using System.Data;
using Dundas.Charting.WebControl;
using Fuji.Common;
using System.Drawing;
using System.IO;
using System.Text;

public partial class Common_GenerateSyncReport : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //the logic currently disables report creation functionality for the ISO projects.
        if (Fuji.Common.Sessions.GetSession("SelectedProjectTypeID").ToString() == Convert.ToInt16(Enums.ProjectType.G7).ToString())
        {
            IbtnGenerateReport.Visible = true;
        }
        else
        {
            IbtnGenerateReport.Visible = false;
        }
    }
    protected void IbtnGenerateReport_Click(object sender, EventArgs e)
    {
        try
        {
            // ===== Create Object of ReportPrepressSingleMeasurementData ======//
            ReportCPSPrintStandardsData objReportCPSPrintStandardsData = new ReportCPSPrintStandardsData();

            // ===== Get Data from data base  ==================//
            objReportCPSPrintStandardsData.PressRunId = Convert.ToInt32(Fuji.Common.Sessions.GetSession("SelectedPressRunID"));
            DataSet dsReportCPSPrintStandardsData = objReportCPSPrintStandardsData.GetPrintStandardReport();

            if (dsReportCPSPrintStandardsData != null && dsReportCPSPrintStandardsData.Tables.Count > 0)
            {
                CreateReportPDF(dsReportCPSPrintStandardsData); // Generates PDF of Reports.
            }
        }
        catch (Exception Ex)
        {
            //throw Ex;
            (new ErrorLogData()).HandleException(Ex);
        }
    }

    #region Report Generation
    private void CreateReportPDF(DataSet ds)
    {
        
            // === Set all parameters to generate pdf file === //

            Fuji.Common.ReportPDF objReportPDF = new Fuji.Common.ReportPDF();
            objReportPDF.ReportType = (int)Fuji.Common.Enums.enumReportType.CPSPrintStandardsReport;
            objReportPDF.PDFPath = Server.MapPath("UploadFiles/Report_PDF/");
            objReportPDF.PDF_NAME = Sessions.UserID + "_CPSPrintStandardReport.htm.PDF";
            objReportPDF.DS_Common = ds;
            GenerateReportCharts(ds);
            objReportPDF.MakePDF();

            // === After generat and save PDF file, Download it at a time === //
            Fuji.Common.CommonFunctions.DownlaodFile(objReportPDF.PDFPath, objReportPDF.PDF_NAME, objReportPDF.PDF_NAME);
        
        
    }
    private void GenerateReportCharts(DataSet ds)
    {
        if (ds.Tables.Count >= 3)
        {
            FillGrayBalanceFChart(ds.Tables[2]);
        }
        if (ds.Tables.Count >= 6)
        {
            FillNPDChart(ds.Tables[4], ds.Tables[5]);
        }
        if (ds.Tables.Count >= 7)
        {
            FillDeltaEPrimaryPatchesChart(ds.Tables[6]);
        }
        if (ds.Tables.Count >= 9)
        {
            FillGaumatChart(ds.Tables[7], ds.Tables[8]);
        }
        if (ds.Tables.Count >= 10)
        {
            FillLABChart(ds.Tables[9]);
        }
        if (ds.Tables.Count >= 11)
        {
            FillDensityChart(ds.Tables[10]);
        }
        if (ds.Tables.Count >= 12)
        {
            FillDotgainChart(ds.Tables[11]);
        }
    }
    private void FillGrayBalanceFChart(DataTable DT)
    {
        try
        {
            Chart_GrayBalanceF.Series["Series1"].Points.Clear();
            if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    AddPointChart(i, Chart_GrayBalanceF.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["ΔF*"].ToString(), Color.Black.Name.ToString(), true);
                    AddPointChart(i, Chart_GrayBalanceF.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["Limit"].ToString(), Color.Red.Name.ToString(), false);
                }
            }
            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(Chart_GrayBalanceF.ImageUrl) + "." + Chart_GrayBalanceF.ImageType;
            Chart_GrayBalanceF.Save(Server.MapPath(FileName));
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    private void FillNPDChart(DataTable DTBlack, DataTable DTGray)
    {
        try
        {
            CHRT_BlackNPD.Series["Series0"].Points.Clear();
            CHRT_BlackNPD.Series["Series1"].Points.Clear();
            if (DTBlack != null && DTBlack.Rows.Count > 0 && DTBlack.Rows[0][0].ToString() != "NO DATA")
            {
                for (int i = 0; i < DTBlack.Rows.Count; i++)
                {
                    AddPointChart(i, CHRT_BlackNPD.Series["Series0"], Convert.ToInt64(DTBlack.Rows[i]["CMYK_K"]).ToString(), DTBlack.Rows[i]["Density"].ToString(), Color.Black.Name.ToString(), true);
                    if (DTBlack.Rows[i]["G7NPDC"] != DBNull.Value)
                    {
                        AddPointChart(i, CHRT_BlackNPD.Series["Series1"], Convert.ToInt64(DTBlack.Rows[i]["CMYK_K"]).ToString(), DTBlack.Rows[i]["G7NPDC"].ToString(), Color.Green.Name.ToString(), true);
                    }
                }
            }
            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_BlackNPD.ImageUrl) + "." + CHRT_BlackNPD.ImageType;
            CHRT_BlackNPD.Save(Server.MapPath(FileName));

            CHRT_GrayNPD.Series["Series0"].Points.Clear();
            CHRT_GrayNPD.Series["Series1"].Points.Clear();
            if (DTGray != null && DTGray.Rows.Count > 0 && DTGray.Rows[0][0].ToString() != "NO DATA")
            {
                for (int i = 0; i < DTGray.Rows.Count; i++)
                {
                    AddPointChart(i, CHRT_GrayNPD.Series["Series0"], Convert.ToInt64(DTGray.Rows[i]["CMYK_C"]).ToString(), DTGray.Rows[i]["Density"].ToString(), Color.Black.Name.ToString(), true);
                    if (DTGray.Rows[i]["G7NPDC"] != DBNull.Value)
                    {
                        AddPointChart(i, CHRT_GrayNPD.Series["Series1"], Convert.ToInt64(DTGray.Rows[i]["CMYK_C"]).ToString(), DTGray.Rows[i]["G7NPDC"].ToString(), Color.Green.Name.ToString(), true);
                    }
                }
            }
            FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_GrayNPD.ImageUrl) + "." + CHRT_GrayNPD.ImageType;
            CHRT_GrayNPD.Save(Server.MapPath(FileName));
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    private void AddPointChart(int _Index, Series _objSeries, string _XValue, string _YValue, string ColorName, bool showCircle)
    {
        try
        {
            _objSeries.Type = SeriesChartType.Spline;
            _objSeries.Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));
            _objSeries.Points[_Index].ToolTip = "[" + Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)).ToString() + "," + Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)).ToString() + "]";
            _objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
            _objSeries.Points[_Index].MarkerStyle = showCircle ? MarkerStyle.Circle : MarkerStyle.None;
            _objSeries.Points[_Index].MarkerSize = 5;
            _objSeries.Points[_Index].BorderWidth = 1;
            _objSeries.Points[_Index].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
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
            if (DT != null && DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    SBDeltaEPatches.Append("<tr style='height: 10px'>");
                    if (Convert.ToDecimal(DT.Rows[i]["ΔE"]) < Convert.ToDecimal(DT.Rows[i]["ΔELimit"]))
                        SBDeltaEPatches.Append("<td valign='middle' align='center' style='padding-right: 5px' class='SyncBottomBorder'><img src=\"Images/Proofok.gif\" width='19px' height='19px' ></td>");
                    else
                        SBDeltaEPatches.Append("<td valign='middle' align='center' style='padding-right: 5px' class='SyncBottomBorder'><img src=\"Images/ProofFailed.gif\" width='19px' height='19px' ></td>");

                    if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString())// "1")
                        SBDeltaEPatches.Append("<td valign='middle' class='SyncBottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:4px;'>Cyan</td>");
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString())// "2")
                        SBDeltaEPatches.Append("<td valign='middle' class='SyncBottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:4px;'>Magenta</td>");
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString())// "3")
                        SBDeltaEPatches.Append("<td valign='middle' class='SyncBottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:4px;'>Yellow</td>");
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString())// "4")
                        SBDeltaEPatches.Append("<td valign='middle' class='SyncBottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:4px;'>Black</td>");
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString())//  "5")
                        SBDeltaEPatches.Append("<td valign='middle' class='SyncBottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:4px;'>Red</td>");
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString())// "6")
                        SBDeltaEPatches.Append("<td valign='middle' class='SyncBottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:4px;'>Green</td>");
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString())// "7")
                        SBDeltaEPatches.Append("<td valign='middle' class='SyncBottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:4px;'>Blue</td>");
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString())// "8")
                        SBDeltaEPatches.Append("<td valign='middle' class='SyncBottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:4px;'>Paper</td>");
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString())// "9")
                        SBDeltaEPatches.Append("<td valign='middle' class='SyncBottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:4px;'>3-C</td>");

                    SBDeltaEPatches.Append("</tr>");
                    BindDeltaEValueACTTHValue(objSeries0, objSeries1, objSeries2, Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["ΔE"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["ΔELimit"].ToString())), i, DT.Rows[i]["HTMLColor"].ToString());
                }
                string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(ChartSummary_DeltaE.ImageUrl) + "." + ChartSummary_DeltaE.ImageType;
                ChartSummary_DeltaE.Save(Server.MapPath(FileName));
                //LitDeltaEPatches.Text = SBDeltaEPatches.ToString();
            }
            #endregion
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    private void BindDeltaEValueACTTHValue(Series objSeries0, Series objSeries1, Series objSeries2, double ACT, double TH, int Index, string HTMLColor)
    {
        try
        {

            Double Y1, Y2;
            Y1 = Y2 = 0;

            Y1 = (Convert.ToString(TH) == "" ? 0 : Convert.ToDouble(TH.ToString()));
            Y2 = (Convert.ToString(ACT) == "" ? 0 : Convert.ToDouble(ACT.ToString()));

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
            objSeries1.Points[Index].ToolTip = Y2.ToString();
            objSeries2.Points.AddXY((Double)0, Y1);
            objSeries0.Points[Index].BackGradientType = GradientType.DiagonalRight;

            SeriesActual.Points[Index].Color = System.Drawing.ColorTranslator.FromHtml(HTMLColor);
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


        BindLABComparision("Cyan", DTTarget, 0, 0, 9, true, false);
        BindLABComparision("Magenta", DTTarget, 0, 0, 9, true, false);
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

        string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHART_ABComparison.ImageUrl) + "." + CHART_ABComparison.ImageType;
        CHART_ABComparison.Save(Server.MapPath(FileName));
    }
    private void BindLABComparision(string ColorName, DataTable DT, Double Papera, Double Paperb, int MarkerSize, bool isTarget, bool isActual)
    {
        DT.DefaultView.RowFilter = "PatchType = '" + ColorName + "'";
        DataView dv = DT.DefaultView;
        for (int i = 0; i < dv.Count; i = i + 1)
        {
            
            if (i == 0 && !isTarget && !isActual)
            {
                CHART_ABComparison.Series[ColorName].Type = SeriesChartType.Line;
                CHART_ABComparison.Series[ColorName].Points.AddXY(Papera, Paperb);
            }

            int pointCount = ((isTarget) ? CHART_ABComparison.Series["Targets"].Points.Count : CHART_ABComparison.Series[ColorName].Points.Count);
                                
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
                    CHART_ABComparison.Series[ColorName].Points[pointCount].ToolTip = (isTarget ? "Target " : "") + (isActual ? "Actual " : "") + "a*:" + dv[i]["CIELAB_a*"].ToString() + " ,b* : " + dv[i]["CIELAB_b*"].ToString();
                    CHART_ABComparison.Series[ColorName].Points[pointCount].Color = (Color)new ColorConverter().ConvertFromString(ColorName.Equals("paper", StringComparison.CurrentCultureIgnoreCase) ? "White" : ColorName);
                }
            }
            else
            {
                CHART_ABComparison.Series[ColorName].Points[pointCount].BorderWidth = 1;
                CHART_ABComparison.Series[ColorName].Points[pointCount].MarkerSize = 0;
                CHART_ABComparison.Series[ColorName].Points[pointCount].Color = (Color)new ColorConverter().ConvertFromString(ColorName.Equals("paper", StringComparison.CurrentCultureIgnoreCase) ? "White" : ColorName);
            }
        }
    }
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
                    AddPointChart(i, Chart_L.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["CIELAB_L*"].ToString(), Color.Black.Name.ToString(), true);
                    AddPointChart(i, Chart_a.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["CIELAB_a*"].ToString(), Color.Black.Name.ToString(), true);
                    AddPointChart(i, Chart_b.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["CIELAB_b*"].ToString(), Color.Black.Name.ToString(), true);

                    if (DT.Rows[i]["Target_L*"] != DBNull.Value)
                    {
                        AddPointChart(i, Chart_L.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["Target_L*"].ToString(), Color.Green.Name.ToString(), true);
                    }
                    AddPointChart(i, Chart_a.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["Target_a*"].ToString(), Color.Green.Name.ToString(), true);
                    AddPointChart(i, Chart_b.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["Target_b*"].ToString(), Color.Green.Name.ToString(), true);
                }
            }
            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(Chart_a.ImageUrl) + "." + Chart_a.ImageType;
            Chart_a.Save(Server.MapPath(FileName));

            FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(Chart_b.ImageUrl) + "." + Chart_b.ImageType;
            Chart_b.Save(Server.MapPath(FileName));

            FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(Chart_L.ImageUrl) + "." + Chart_L.ImageType;
            Chart_L.Save(Server.MapPath(FileName));
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    private void FillDensityChart(DataTable DT)
    {
        try
        {
            Double CValue = 0;
            CHRT_SummaryDensity.Series["Series1"].Points.Clear();

            #region Threshold Value for Positive
            CHRT_SummaryDensity.Series["Series3"].Type = SeriesChartType.Line;
            #endregion

            #region Threshold Value for Nagative
            CHRT_SummaryDensity.Series["Series4"].Type = SeriesChartType.Line;
            #endregion

            if (DT != null && DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    CValue = Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["Density"]));
                    if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString())// "1")
                    {
                        AddPoint(i, CHRT_SummaryDensity.Series["Series1"], "C", CValue.ToString(), DT.Rows[i]["HTMLColor"].ToString());
                    }
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString())// "2")
                    {
                        AddPoint(i, CHRT_SummaryDensity.Series["Series1"], "M", CValue.ToString(), DT.Rows[i]["HTMLColor"].ToString());
                    }
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString())// "3")
                    {
                        AddPoint(i, CHRT_SummaryDensity.Series["Series1"], "Y", CValue.ToString(), DT.Rows[i]["HTMLColor"].ToString());
                    }
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString())// "4")
                    {
                        AddPoint(i, CHRT_SummaryDensity.Series["Series1"], "K", CValue.ToString(), DT.Rows[i]["HTMLColor"].ToString());
                    }
                }

            }
            else
            {
                CHRT_SummaryDensity.ChartAreas["Default"].AxisY.Minimum = -10;
                CHRT_SummaryDensity.ChartAreas["Default"].AxisY.Maximum = 10;

                AddPoint(0, CHRT_SummaryDensity.Series["Series1"], "C", "0", "#E9E9E9");
                AddPoint(1, CHRT_SummaryDensity.Series["Series1"], "M", "0", "#E9E9E9");
                AddPoint(2, CHRT_SummaryDensity.Series["Series1"], "Y", "0", "#E9E9E9");
                AddPoint(3, CHRT_SummaryDensity.Series["Series1"], "K", "0", "#E9E9E9");
            }

            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_SummaryDensity.ImageUrl) + "." + CHRT_SummaryDensity.ImageType;
            CHRT_SummaryDensity.Save(Server.MapPath(FileName));
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    private void AddPoint(int _Index, Series _objSeries, string _XValue, string _YValue, string ColorName)
    {
        try
        {
            _objSeries.Points.AddXY(_XValue, _YValue);
            _objSeries.Points[_Index].ToolTip = _YValue.ToString();
            _objSeries.Points[_Index].Color = System.Drawing.ColorTranslator.FromHtml(ColorName);
            _objSeries.Points[_Index].Href = "#";
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void FillDotgainChart(DataTable DT)
    {
        try
        {
            CHRT_SummaryDotgain.Series["Series1"].Points.Clear();
            CHRT_SummaryDotgain.Series["Series2"].Points.Clear();
            CHRT_SummaryDotgain.Series["Series3"].Points.Clear();
            CHRT_SummaryDotgain.Series["Series4"].Points.Clear();

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
                        AddPointChartWithClr(C, CHRT_SummaryDotgain.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Cyan.Name.ToString());
                        AddPointChartWithClr(M, CHRT_SummaryDotgain.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_M"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Magenta.Name.ToString());
                        AddPointChartWithClr(Y, CHRT_SummaryDotgain.Series["Series3"], Convert.ToInt64(DT.Rows[i]["CMYK_Y"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Yellow.Name.ToString());
                        AddPointChartWithClr(K, CHRT_SummaryDotgain.Series["Series4"], Convert.ToInt64(DT.Rows[i]["CMYK_K"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Black.Name.ToString());
                        C++; M++; Y++; K++;
                    }
                    if (Convert.ToInt16(DT.Rows[i]["CMYK_C"]) != 0)
                    {
                        AddPointChartWithClr(C, CHRT_SummaryDotgain.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Cyan.Name.ToString());
                        C++;
                    }
                    if (Convert.ToInt16(DT.Rows[i]["CMYK_M"]) != 0)
                    {
                        AddPointChartWithClr(M, CHRT_SummaryDotgain.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_M"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Magenta.Name.ToString());
                        M++;
                    }
                    if (Convert.ToInt16(DT.Rows[i]["CMYK_Y"]) != 0)
                    {
                        AddPointChartWithClr(Y, CHRT_SummaryDotgain.Series["Series3"], Convert.ToInt64(DT.Rows[i]["CMYK_Y"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Yellow.Name.ToString());
                        Y++;
                    }
                    if (Convert.ToInt16(DT.Rows[i]["CMYK_K"]) != 0)
                    {
                        AddPointChartWithClr(K, CHRT_SummaryDotgain.Series["Series4"], Convert.ToInt64(DT.Rows[i]["CMYK_K"]).ToString(), DT.Rows[i]["DotGain"].ToString(), Color.Black.Name.ToString());
                        K++;
                    }
                }
            }
            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_SummaryDotgain.ImageUrl) + "." + CHRT_SummaryDotgain.ImageType;
            CHRT_SummaryDotgain.Save(Server.MapPath(FileName));
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    private void AddPointChartWithClr(int _Index, Series _objSeries, string _XValue, string _YValue, string ColorName)
    {
        try
        {
            _objSeries.Type = SeriesChartType.Spline;
            _objSeries.Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));
            _objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
            _objSeries.Points[_Index].MarkerSize = 5;
            _objSeries.Points[_Index].BorderWidth = 1;
            _objSeries.Points[_Index].MarkerBorderColor = Color.Black;
            _objSeries.Points[_Index].ToolTip = "(" + Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)).ToString() + "," + Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)).ToString() + ")";
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion
}
