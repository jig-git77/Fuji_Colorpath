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
        //if (Fuji.Common.Sessions.GetSession("SelectedProjectTypeID").ToString() == Convert.ToInt16(Enums.ProjectType.G7).ToString())
        //{
        //    IbtnGenerateReport.Visible = true;
        //}
        //else
        //{
        //    IbtnGenerateReport.Visible = false;
        //}
    }
    protected void IbtnGenerateReport_Click(object sender, EventArgs e)
    {
        try
        {
            // ===== Create Object of ReportPrepressSingleMeasurementData ======//
            ReportCPSPrintStandardsData objReportCPSPrintStandardsData = new ReportCPSPrintStandardsData();

            // ===== Get Data from data base  ==================//
            objReportCPSPrintStandardsData.PressRunId = Convert.ToInt32(Fuji.Common.Sessions.GetSession("SelectedPressRunID"));
            if (Fuji.Common.Sessions.GetSession("SelectedProjectTypeID").ToString() == Convert.ToInt16(Enums.ProjectType.G7).ToString())
            {
                DataSet dsReportCPSPrintStandardsData = objReportCPSPrintStandardsData.GetPrintStandardReport();

                if (dsReportCPSPrintStandardsData != null && dsReportCPSPrintStandardsData.Tables.Count > 0)
                {
                    CreateReportPDF(dsReportCPSPrintStandardsData); // Generates PDF of Reports.
                }
            }
            else if (Fuji.Common.Sessions.GetSession("SelectedProjectTypeID").ToString() == Convert.ToInt16(Enums.ProjectType.ISO).ToString())
            {
                DataSet dsReportISOCPSPrintStandardsData = objReportCPSPrintStandardsData.GetPrintStandardReportISO();

                if (dsReportISOCPSPrintStandardsData != null && dsReportISOCPSPrintStandardsData.Tables.Count > 0)
                {
                    CreateReportPDFISO(dsReportISOCPSPrintStandardsData); // Generates PDF of Reports.
                }
            }
        }
        catch (Exception Ex)
        {
            //throw Ex;
            (new ErrorLogData()).HandleException(Ex);
        }
    }

    #region Report Generation For G7
    private void CreateReportPDF(DataSet ds)
    {
        
            // === Set all parameters to generate pdf file === //

            Fuji.Common.ReportPDF objReportPDF = new Fuji.Common.ReportPDF();
            objReportPDF.ReportType = (int)Fuji.Common.Enums.enumReportType.CPSPrintStandardsReport;
            objReportPDF.PDFPath = Server.MapPath("UploadFiles/Report_PDF/");
            objReportPDF.PDF_NAME = Sessions.UserID + "_CPSPrintStandardReportG7.PDF";
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

    #region Report Generation For ISO
    private void CreateReportPDFISO(DataSet ds)
    {
        // === Set all parameters to generate pdf file === //

        Fuji.Common.ReportPDF objReportPDF = new Fuji.Common.ReportPDF();
        objReportPDF.ReportType = (int)Fuji.Common.Enums.enumReportType.CPSPrintStandardsReportISO;
        objReportPDF.PDFPath = Server.MapPath("UploadFiles/Report_PDF/");
        objReportPDF.PDF_NAME = Sessions.UserID + "_CPSPrintStandardReportISO.PDF";
        objReportPDF.DS_Common = ds;
        GenerateReportChartsISO(ds);
        objReportPDF.MakePDF();

        // === After generat and save PDF file, Download it at a time === //
        Fuji.Common.CommonFunctions.DownlaodFile(objReportPDF.PDFPath, objReportPDF.PDF_NAME, objReportPDF.PDF_NAME);
    }
    private void GenerateReportChartsISO(DataSet ds)
    {
        if (ds.Tables.Count >= 4)
        {
            FillGaumatChartISO(ds.Tables[2], ds.Tables[3]);
        }
        if (ds.Tables.Count >= 5)
        {
            FillDeltaEPrimaryPatchesChartISO(ds.Tables[4]);
        }
        if (ds.Tables.Count >= 6)
        {
            FillPaperChartISO(ds.Tables[5]);
        }
        if (ds.Tables.Count >= 7)
        {
            FillDensityChartISO(ds.Tables[6]);
        }
        if (ds.Tables.Count >= 9)
        {

            FillSpreadChartISO(ds.Tables[7], ds.Tables[8]);
        }

        if(ds.Tables.Count >= 11)
        {
            FillTVIChartISO(ds.Tables[10]);
        }
        if (ds.Tables.Count >= 12)
        {
            FillGryaBalanceChartISO(ds.Tables[11]);
        }

        if (ds.Tables.Count >= 13)
        {
            FillLABChartISO(ds.Tables[12]);
        }

    }

    #region Fill Gaumat Chart
    private void FillGaumatChartISO(DataTable DTTarget, DataTable DTColors)
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
        BindLABComparisionISO("Cyan", DTColors, Papera, Paperb, 5, false, false);
        BindLABComparisionISO("Magenta", DTColors, Papera, Paperb, 5, false, false);
        BindLABComparisionISO("Yellow", DTColors, Papera, Paperb, 5, false, false);

        BindLABComparisionISO("Cyan", DTTarget, 0, 0, 6, false, true);
        BindLABComparisionISO("Magenta", DTTarget, 0, 0, 6, false, true);
        BindLABComparisionISO("Yellow", DTTarget, 0, 0, 6, false, true);
        BindLABComparisionISO("Red", DTTarget, 0, 0, 6, false, true);
        BindLABComparisionISO("Green", DTTarget, 0, 0, 6, false, true);
        BindLABComparisionISO("Blue", DTTarget, 0, 0, 6, false, true);
        BindLABComparisionISO("Paper", DTTarget, 0, 0, 6, false, true);

        BindLABComparisionISO("Cyan", DTTarget, 0, 0, 9, true, false);
        BindLABComparisionISO("Magenta", DTTarget, 0, 0, 9, true, false);
        BindLABComparisionISO("Yellow", DTTarget, 0, 0, 9, true, false);
        BindLABComparisionISO("Red", DTTarget, 0, 0, 9, true, false);
        BindLABComparisionISO("Green", DTTarget, 0, 0, 9, true, false);
        BindLABComparisionISO("Blue", DTTarget, 0, 0, 9, true, false);
        BindLABComparisionISO("Paper", DTTarget, 0, 0, 9, true, false);

    }
    private void BindLABComparisionISO(string ColorName, DataTable DT, Double Papera, Double Paperb, int MarkerSize, bool isTarget, bool isActual)
    {        
        DT.DefaultView.RowFilter = "PatchType = '" + ColorName + "'";
        DataView dv = DT.DefaultView;
        for (int i = 0; i < dv.Count; i = i + 1)
        {
            #region Bind AB Comparison
            if (i == 0 && !isTarget && !isActual)
            {
                CHART_ABISOComparison.Series[ColorName].Type = SeriesChartType.Line;
                CHART_ABISOComparison.Series[ColorName].Points.AddXY(Papera, Paperb);
            }

            int pointCount = ((isTarget) ? CHART_ABISOComparison.Series["Targets"].Points.Count : CHART_ABISOComparison.Series[ColorName].Points.Count);

            

            if (isTarget)
            {
                CHART_ABISOComparison.Series["Targets"].Type = SeriesChartType.FastPoint;
                CHART_ABISOComparison.Series["Targets"].Points.AddXY(Convert.ToDouble(dv[i]["Targeta*"]), Convert.ToDouble(dv[i]["Targetb*"]));
                CHART_ABISOComparison.Series["Targets"].MarkerSize = MarkerSize;
                CHART_ABISOComparison.Series["Targets"].MarkerColor = Color.Green;
            }
            else
            {
                CHART_ABISOComparison.Series[ColorName].Type = SeriesChartType.Line;
                CHART_ABISOComparison.Series[ColorName].Points.AddXY(Convert.ToDouble(dv[i]["CIELAB_a*"]), Convert.ToDouble(dv[i]["CIELAB_b*"]));
            }
            if (i == dv.Count - 1 && (isTarget || isActual))
            {
                if (isActual)
                {
                    CHART_ABISOComparison.Series[ColorName].Points[pointCount].BorderWidth = 1;
                    CHART_ABISOComparison.Series[ColorName].Points[pointCount].MarkerSize = MarkerSize;                    
                    CHART_ABISOComparison.Series[ColorName].Points[pointCount].Color = (Color)new ColorConverter().ConvertFromString(ColorName.Equals("paper", StringComparison.CurrentCultureIgnoreCase) ? "White" : ColorName);
                }
            }
            else
            {
                CHART_ABISOComparison.Series[ColorName].Points[pointCount].BorderWidth = 1;
                CHART_ABISOComparison.Series[ColorName].Points[pointCount].MarkerSize = 0;
                CHART_ABISOComparison.Series[ColorName].Points[pointCount].Color = (Color)new ColorConverter().ConvertFromString(ColorName.Equals("paper", StringComparison.CurrentCultureIgnoreCase) ? "White" : ColorName);

            }

            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHART_ABISOComparison.ImageUrl) + "." + CHART_ABISOComparison.ImageType;
            CHART_ABISOComparison.Save(Server.MapPath(FileName));

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

                pointCount = CHART_LISOComparison.Series[series].Points.Count;

                CHART_LISOComparison.Series[series].Points.AddXY(Convert.ToDouble(dv[0]["CIELAB_a*"]), Convert.ToDouble(0));
                CHART_LISOComparison.Series[series].Type = SeriesChartType.Point;
                CHART_LISOComparison.Series[series].Points[i].BorderWidth = 2;

                CHART_LISOComparison.Series[series + 1].Points.AddXY(Convert.ToDouble(dv[0]["CIELAB_a*"]), Convert.ToDouble(dv[0]["L*Deviation"]));
                CHART_LISOComparison.Series[series + 1].Type = SeriesChartType.Point;
                CHART_LISOComparison.Series[series + 1].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                CHART_LISOComparison.Series[series + 1].Points[i].BorderWidth = 2;


                if (Convert.ToString(dv[0]["PatchType"]).ToLower() == "paper")
                {
                    CHART_LISOComparison.Series[series].Points[pointCount].MarkerColor = Color.White;
                    CHART_LISOComparison.Series[series + 1].Points[pointCount].MarkerColor = Color.White;
                }
                else
                {
                    CHART_LISOComparison.Series[series].Points[pointCount].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(dv[0]["PatchType"]));
                    CHART_LISOComparison.Series[series + 1].Points[pointCount].MarkerColor = System.Drawing.ColorTranslator.FromHtml(Convert.ToString(dv[0]["PatchType"]));
                }
                CHART_LISOComparison.Series[series].Points[pointCount].MarkerSize = 9;
                CHART_LISOComparison.Series[series + 1].Points[pointCount].MarkerSize = 5;
            }
            FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHART_LISOComparison.ImageUrl) + "." + CHART_LISOComparison.ImageType;
            CHART_LISOComparison.Save(Server.MapPath(FileName));
            #endregion

        }
    }
    #endregion

    #region Fill DeltaE Primary Patches Chart
    private void FillDeltaEPrimaryPatchesChartISO(DataTable DT)
    {
        try
        {
            ChartISOSummary_DeltaE.BorderColor = Color.White;
            ChartISOSummary_DeltaE.BorderWidth = 0;
            ChartISOSummary_DeltaE.BackColor = Color.White;

            //========== Clear Points and Show Chart ==========//
            ChartISOSummary_DeltaE.Visible = true;
            ChartISOSummary_DeltaE.Series[0].Points.Clear();
            ChartISOSummary_DeltaE.Series[1].Points.Clear();
            ChartISOSummary_DeltaE.Series[2].Points.Clear();

            #region //========== Declarations ==========//
            // === Series0 to display Thresold value === //
            Series objSeries0 = (Series)ChartISOSummary_DeltaE.Series[0];
            objSeries0.XValueType = ChartValueTypes.Int;
            objSeries0.YValueType = ChartValueTypes.String;
            objSeries0.ShowInLegend = false;
            objSeries0.Type = SeriesChartType.Bar;
            objSeries0.BackGradientType = GradientType.DiagonalRight;

            // === Series1 to display Actual value === //
            Series objSeries1 = (Series)ChartISOSummary_DeltaE.Series[1];
            objSeries1.XValueType = ChartValueTypes.Int;
            objSeries1.YValueType = ChartValueTypes.String;
            objSeries1.ShowInLegend = false;
            objSeries1.Type = SeriesChartType.Bar;


            // === Series2 to display red marker on Thresold value === //
            Series objSeries2 = (Series)ChartISOSummary_DeltaE.Series[2];
            objSeries2.XValueType = ChartValueTypes.Int;
            objSeries2.YValueType = ChartValueTypes.String;
            objSeries2.ShowInLegend = false;
            objSeries2.Type = SeriesChartType.Bar;

            // === Make Chart area look like 2D even it is 3D === //
            ChartISOSummary_DeltaE.ChartAreas["ChartArea1"].Area3DStyle.RightAngleAxes = true;
            ChartISOSummary_DeltaE.ChartAreas["ChartArea1"].Area3DStyle.Clustered = true;
            ChartISOSummary_DeltaE.ChartAreas["ChartArea1"].Area3DStyle.XAngle = 0;
            ChartISOSummary_DeltaE.ChartAreas["ChartArea1"].Area3DStyle.YAngle = 0;
            ChartISOSummary_DeltaE.ChartAreas["ChartArea1"].BackColor = Color.White;
            #endregion

            #region //========== ChartArea Settings ==========//
            ChartISOSummary_DeltaE.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
            ChartISOSummary_DeltaE.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;

            ChartISOSummary_DeltaE.ChartAreas["ChartArea1"].Position.Auto = true;
            ChartISOSummary_DeltaE.ChartAreas["ChartArea1"].Position.X = float.Parse("0");
            ChartISOSummary_DeltaE.ChartAreas["ChartArea1"].Position.Y = float.Parse("5");
            ChartISOSummary_DeltaE.ChartAreas["ChartArea1"].Position.Width = float.Parse("99");
            ChartISOSummary_DeltaE.ChartAreas["ChartArea1"].Position.Height = float.Parse("97");

            ChartISOSummary_DeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.Auto = true;
            ChartISOSummary_DeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.X = float.Parse("0");
            ChartISOSummary_DeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.Y = float.Parse("12");
            ChartISOSummary_DeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.Width = float.Parse("98");
            ChartISOSummary_DeltaE.ChartAreas["ChartArea1"].InnerPlotPosition.Height = float.Parse("97");

            ChartISOSummary_DeltaE.ChartAreas["ChartArea1"].ShadowOffset = 0;
            ChartISOSummary_DeltaE.BorderSkin.SkinStyle = BorderSkinStyle.None;
            ChartISOSummary_DeltaE.ChartAreas["ChartArea1"].BorderWidth = 0;
            ChartISOSummary_DeltaE.BorderSkin.FrameBorderWidth = 0;



            #endregion

            #region " === Fill Series === "
            // === Limit with red marker === //           
            
            if (DT != null && DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    BindDeltaEValueACTTHValueISO(objSeries0, objSeries1, objSeries2, DT.Rows[i], i);
                }                
            }
            #endregion
            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(ChartISOSummary_DeltaE.ImageUrl) + "." + ChartISOSummary_DeltaE.ImageType;
            ChartISOSummary_DeltaE.Save(Server.MapPath(FileName));
            
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    private void BindDeltaEValueACTTHValueISO(Series objSeries0, Series objSeries1, Series objSeries2, DataRow DR, int Index)
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
            objSeries0.Points[Index]["PointWidth"] = "1.0";
            objSeries1.Points.AddXY((Double)0, Y2);            
            objSeries2.Points.AddXY((Double)0, Y1);
            objSeries0.Points[Index].BackGradientType = GradientType.DiagonalRight;
            SeriesActual.Points[Index].Color = System.Drawing.ColorTranslator.FromHtml(DR["HTMLColor"].ToString());
            SeriesActual.Points[Index]["PointWidth"] = "1.0";           
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

    #region Fill Papaer Chart
    private void FillPaperChartISO(DataTable DT)
    {
        try
        {
            if (DT != null && DT.Rows.Count > 0)
            {
                #region Fill ISO Paper 1 Chart                
                CHRT_ISOPaper1.Series["Green"].Points.AddXY(Convert.ToDouble(0), Convert.ToDouble(0));
                CHRT_ISOPaper1.Series["Green"].Type = SeriesChartType.Point;
                CHRT_ISOPaper1.Series["Green"].Points[0].BorderWidth = 2;
                
                CHRT_ISOPaper1.Series["Paper"].Points.AddXY(Convert.ToDouble(DT.Rows[0]["Δa*"]), Convert.ToDouble(DT.Rows[0]["Δb*"]));
                CHRT_ISOPaper1.Series["Paper"].Type = SeriesChartType.Point;
                CHRT_ISOPaper1.Series["Paper"].Points[0].BorderWidth = 4;

                #region Add Target Box
                Int64 PositiveA = Convert.ToInt64(DT.Rows[0]["Δa*Th"]);
                Int64 PositiveB = Convert.ToInt64(DT.Rows[0]["Δb*Th"]);

                Int64 NegativeL = 0;
                Int64 NegativeA = 0;
                Int64 NegativeB = 0;

                if (PositiveA < 0)
                    PositiveA = (PositiveA * -1);
                if (PositiveB < 0)
                    PositiveB = (PositiveB * -1);

                NegativeA = (PositiveA * -1);
                NegativeB = (PositiveB * -1);

                CHRT_ISOPaper1.Series["SeriesTop"].Points.AddXY(NegativeA, PositiveB, NegativeB);
                CHRT_ISOPaper1.Series["SeriesTop"].Points.AddXY(PositiveA, PositiveB, NegativeB);

                CHRT_ISOPaper1.Series["SeriesRed1"].Points.AddXY(NegativeA, NegativeB);
                CHRT_ISOPaper1.Series["SeriesRed1"].Points.AddXY(NegativeA, PositiveB);

                CHRT_ISOPaper1.Series["SeriesRed2"].Points.AddXY(NegativeA, NegativeB);
                CHRT_ISOPaper1.Series["SeriesRed2"].Points.AddXY(PositiveA, NegativeB);

                CHRT_ISOPaper1.Series["SeriesRed3"].Points.AddXY(NegativeA, PositiveB);
                CHRT_ISOPaper1.Series["SeriesRed3"].Points.AddXY(PositiveA, PositiveB);

                CHRT_ISOPaper1.Series["SeriesRed4"].Points.AddXY(PositiveA, PositiveB);
                CHRT_ISOPaper1.Series["SeriesRed4"].Points.AddXY(PositiveA, NegativeB);
                #endregion

                string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_ISOPaper1.ImageUrl) + "." + CHRT_ISOPaper1.ImageType;
                CHRT_ISOPaper1.Save(Server.MapPath(FileName));

                #endregion

                #region Fill ISO Paper 2 chart

                #region Line Chart Series For Dispaly Line on 0 value
                CHRT_ISOPaper2.Series["Series2"].Type = SeriesChartType.Line;
                CHRT_ISOPaper2.Series["Series2"].XAxisType = AxisType.Secondary;
                CHRT_ISOPaper2.Series["Series2"].Points.AddXY(0, 0);
                CHRT_ISOPaper2.Series["Series2"].Points.AddXY(1, 0);
                #endregion

                #region Add Target Box
                Int64 PositiveL = Convert.ToInt64(DT.Rows[0]["ΔL*Th"]);
                if (PositiveL < 0)
                    PositiveL = (PositiveL * -1);
                NegativeL = (PositiveL * -1);
                CHRT_ISOPaper2.Series["SeriesTop"].XAxisType = AxisType.Secondary;
                CHRT_ISOPaper2.Series["SeriesTop"].Points.AddXY(0, PositiveL, NegativeL);
                CHRT_ISOPaper2.Series["SeriesTop"].Points.AddXY(1, PositiveL, NegativeL);
                #endregion

                CHRT_ISOPaper2.Series["Series1"].Points.AddY(Convert.ToDouble(DT.Rows[0]["ΔL*"]));
                CHRT_ISOPaper2.Series["Series1"].Points[0].Color = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");

                FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_ISOPaper2.ImageUrl) + "." + CHRT_ISOPaper2.ImageType;
                CHRT_ISOPaper2.Save(Server.MapPath(FileName));
                #endregion
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
    #endregion

    #region Fill Density Chart
    private void FillDensityChartISO(DataTable DT)
    {
        try
        {

            Double CValue = 0;


            //========== Clear Points and Show Chart ==========//
            CHRT_SummaryISODensity.Visible = true;
            CHRT_SummaryISODensity.Series[0].Points.Clear();
            CHRT_SummaryISODensity.Series[1].Points.Clear();
            CHRT_SummaryISODensity.Series[2].Points.Clear();

            #region //========== Declarations ==========//
            // === Series0 to display Thresold value === //
            Series objSeries0 = (Series)CHRT_SummaryISODensity.Series[0];
            objSeries0.XValueType = ChartValueTypes.String;
            objSeries0.YValueType = ChartValueTypes.String;
            objSeries0.ShowInLegend = false;
            objSeries0.Type = SeriesChartType.Column;
            objSeries0.BackGradientType = GradientType.DiagonalRight;

            // === Series1 to display Actual value === //
            Series objSeries1 = (Series)CHRT_SummaryISODensity.Series[1];
            objSeries1.XValueType = ChartValueTypes.String;
            objSeries1.YValueType = ChartValueTypes.String;
            objSeries1.ShowInLegend = false;
            objSeries1.Type = SeriesChartType.Column;


            // === Series2 to display red marker on Thresold value === //
            Series objSeries2 = (Series)CHRT_SummaryISODensity.Series[2];
            objSeries2.XValueType = ChartValueTypes.String;
            objSeries2.YValueType = ChartValueTypes.String;
            objSeries2.ShowInLegend = false;
            objSeries2.Type = SeriesChartType.Column;

            // === Make Chart area look like 2D even it is 3D === //
            CHRT_SummaryISODensity.ChartAreas["ChartArea1"].Area3DStyle.RightAngleAxes = true;
            CHRT_SummaryISODensity.ChartAreas["ChartArea1"].Area3DStyle.Clustered = true;
            CHRT_SummaryISODensity.ChartAreas["ChartArea1"].Area3DStyle.XAngle = 0;
            CHRT_SummaryISODensity.ChartAreas["ChartArea1"].Area3DStyle.YAngle = 0;
            #endregion


            if (DT != null && DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    CValue = Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["Density"]));

                    if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString())
                        AddDensityPointISO(i, objSeries0, objSeries1, objSeries2, "C", CValue.ToString(), DT.Rows[i]["HTMLColor"].ToString(), DT.Rows[i]["Density"].ToString(), DT.Rows[i]["TargetDens"].ToString(), "Cyan");
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString())
                        AddDensityPointISO(i, objSeries0, objSeries1, objSeries2, "M", CValue.ToString(), DT.Rows[i]["HTMLColor"].ToString(), DT.Rows[i]["Density"].ToString(), DT.Rows[i]["TargetDens"].ToString(), "Magenta");
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString())
                        AddDensityPointISO(i, objSeries0, objSeries1, objSeries2, "Y", CValue.ToString(), DT.Rows[i]["HTMLColor"].ToString(), DT.Rows[i]["Density"].ToString(), DT.Rows[i]["TargetDens"].ToString(), "Yellow");
                    else if (DT.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString())
                        AddDensityPointISO(i, objSeries0, objSeries1, objSeries2, "K", CValue.ToString(), DT.Rows[i]["HTMLColor"].ToString(), DT.Rows[i]["Density"].ToString(), DT.Rows[i]["TargetDens"].ToString(), "Black");
                }
            }
            else
            {
                CHRT_SummaryISODensity.ChartAreas["Default"].AxisY.Minimum = -10;
                CHRT_SummaryISODensity.ChartAreas["Default"].AxisY.Maximum = 10;

                AddDensityPointISO(0, objSeries0, objSeries1, objSeries2, "C", "0", "#E9E9E9", "0", "0", "");
                AddDensityPointISO(1, objSeries0, objSeries1, objSeries2, "M", "0", "#E9E9E9", "0", "0", "");
                AddDensityPointISO(2, objSeries0, objSeries1, objSeries2, "Y", "0", "#E9E9E9", "0", "0", "");
                AddDensityPointISO(3, objSeries0, objSeries1, objSeries2, "K", "0", "#E9E9E9", "0", "0", "");
            }

            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_SummaryISODensity.ImageUrl) + "." + CHRT_SummaryISODensity.ImageType;
            CHRT_SummaryISODensity.Save(Server.MapPath(FileName));
            
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    private void AddDensityPointISO(int Index, Series objSeries0, Series objSeries1, Series objSeries2, string _XValue, string _YValue, string HTMLColor, string _Actual, string _Target, string ColorName)
    {
        try
        {
            objSeries0.Points.AddXY(_XValue, _Target);
            objSeries1.Points.AddXY(_XValue, _YValue);                        
            objSeries2.Points.AddXY(_XValue, _Target);
            objSeries1.Points[Index].Color = System.Drawing.ColorTranslator.FromHtml(HTMLColor);
            objSeries1["PointWidth"] = "0.8";
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Fill Spread
    private void FillSpreadChartISO(DataTable DTSpread, DataTable DTSpreadSplineRange)
    {
        try
        {
            DataView DV = new DataView();

            CHRT_SummaryISODotgain.Series["Series1"].Points.Clear();
            CHRT_SummaryISODotgain.Series["Series2"].Points.Clear();
            CHRT_SummaryISODotgain.Series["Series3"].Points.Clear();

            CHRT_SummaryISODotgain.Series["Series1Point"].Points.Clear();
            CHRT_SummaryISODotgain.Series["Series2Point"].Points.Clear();
            CHRT_SummaryISODotgain.Series["Series3Point"].Points.Clear();

            #region Dotgain Spread Chart
            if (DTSpread != null && DTSpread.Rows.Count > 0)
            {
                int C = 0;
                int M = 0;
                int Y = 0;
                Int16 CyanValue = 0;
                Int16 MagentaValue = 0;
                Int16 YellowValue = 0;
                Int16 DiffrentOne = 0;
                Int16 DiffrentTwo = 0;
                String DiffrentTitleOne = string.Empty;
                String DiffrentTitleTwo = string.Empty;


                for (int i = 0; i < DTSpread.Rows.Count; i++)
                {

                    if (Convert.ToInt16(DTSpread.Rows[i]["CMYK_C"]) == 0 && Convert.ToInt16(DTSpread.Rows[i]["CMYK_M"]) == 0 && Convert.ToInt16(DTSpread.Rows[i]["CMYK_Y"]) == 0)
                    {

                        AddPointChartWithColorISO(C, CHRT_SummaryISODotgain.Series["Series1"], CHRT_SummaryISODotgain.Series["Series1Point"], Convert.ToInt64(DTSpread.Rows[i]["CMYK_C"]).ToString(), DTSpread.Rows[i]["DotGain"].ToString(), Color.Cyan.Name.ToString(), DTSpread.Rows[i]["DotGainSpreadTh"].ToString(), "", "", "", "");
                        AddPointChartWithColorISO(M, CHRT_SummaryISODotgain.Series["Series2"], CHRT_SummaryISODotgain.Series["Series2Point"], Convert.ToInt64(DTSpread.Rows[i]["CMYK_M"]).ToString(), DTSpread.Rows[i]["DotGain"].ToString(), Color.Magenta.Name.ToString(), DTSpread.Rows[i]["DotGainSpreadTh"].ToString(), "", "", "", "");
                        AddPointChartWithColorISO(Y, CHRT_SummaryISODotgain.Series["Series3"], CHRT_SummaryISODotgain.Series["Series3Point"], Convert.ToInt64(DTSpread.Rows[i]["CMYK_Y"]).ToString(), DTSpread.Rows[i]["DotGain"].ToString(), Color.Yellow.Name.ToString(), DTSpread.Rows[i]["DotGainSpreadTh"].ToString(), "", "", "", "");
                        C++; M++; Y++;

                    }
                    if (Convert.ToInt16(DTSpread.Rows[i]["CMYK_C"]) != 0)
                    {
                        DTSpread.DefaultView.RowFilter = "(CMYK_M = " + DTSpread.Rows[i]["CMYK_C"].ToString() + ")";
                        MagentaValue = Convert.ToInt16(DTSpread.DefaultView[0]["DotGain"]);

                        DTSpread.DefaultView.RowFilter = "(CMYK_Y = " + DTSpread.Rows[i]["CMYK_C"].ToString() + ")";
                        YellowValue = Convert.ToInt16(DTSpread.DefaultView[0]["DotGain"]);

                        DiffrentOne = Convert.ToInt16(Convert.ToInt16(DTSpread.Rows[i]["DotGain"]) - MagentaValue);
                        DiffrentTwo = Convert.ToInt16(Convert.ToInt16(DTSpread.Rows[i]["DotGain"]) - YellowValue);

                        DiffrentTitleOne = "C - M";
                        DiffrentTitleTwo = "C - Y";

                        AddPointChartWithColorISO(C, CHRT_SummaryISODotgain.Series["Series1"], CHRT_SummaryISODotgain.Series["Series1Point"], Convert.ToInt64(DTSpread.Rows[i]["CMYK_C"]).ToString(), DTSpread.Rows[i]["DotGain"].ToString(), Color.Cyan.Name.ToString(), DTSpread.Rows[i]["DotGainSpreadTh"].ToString(), DiffrentTitleOne, DiffrentTitleTwo, Math.Abs(DiffrentOne).ToString(), Math.Abs(DiffrentTwo).ToString());
                        C++;
                    }
                    if (Convert.ToInt16(DTSpread.Rows[i]["CMYK_M"]) != 0)
                    {
                        DTSpread.DefaultView.RowFilter = "(CMYK_C = " + DTSpread.Rows[i]["CMYK_M"].ToString() + ")";
                        CyanValue = Convert.ToInt16(DTSpread.DefaultView[0]["DotGain"]);

                        DTSpread.DefaultView.RowFilter = "(CMYK_Y = " + DTSpread.Rows[i]["CMYK_M"].ToString() + ")";
                        YellowValue = Convert.ToInt16(DTSpread.DefaultView[0]["DotGain"]);

                        DiffrentOne = Convert.ToInt16(Convert.ToInt16(DTSpread.Rows[i]["DotGain"]) - CyanValue);
                        DiffrentTwo = Convert.ToInt16(Convert.ToInt16(DTSpread.Rows[i]["DotGain"]) - YellowValue);

                        DiffrentTitleOne = "M - C";
                        DiffrentTitleTwo = "M - Y";


                        AddPointChartWithColorISO(M, CHRT_SummaryISODotgain.Series["Series2"], CHRT_SummaryISODotgain.Series["Series2Point"], Convert.ToInt64(DTSpread.Rows[i]["CMYK_M"]).ToString(), DTSpread.Rows[i]["DotGain"].ToString(), Color.Magenta.Name.ToString(), DTSpread.Rows[i]["DotGainSpreadTh"].ToString(), DiffrentTitleOne, DiffrentTitleTwo, Math.Abs(DiffrentOne).ToString(), Math.Abs(DiffrentTwo).ToString());
                        M++;
                    }
                    if (Convert.ToInt16(DTSpread.Rows[i]["CMYK_Y"]) != 0)
                    {
                        DTSpread.DefaultView.RowFilter = "(CMYK_C = " + DTSpread.Rows[i]["CMYK_Y"].ToString() + ")";
                        CyanValue = Convert.ToInt16(DTSpread.DefaultView[0]["DotGain"]);

                        DTSpread.DefaultView.RowFilter = "(CMYK_M = " + DTSpread.Rows[i]["CMYK_Y"].ToString() + ")";
                        MagentaValue = Convert.ToInt16(DTSpread.DefaultView[0]["DotGain"]);

                        DiffrentOne = Convert.ToInt16(Convert.ToInt16(DTSpread.Rows[i]["DotGain"]) - CyanValue);
                        DiffrentTwo = Convert.ToInt16(Convert.ToInt16(DTSpread.Rows[i]["DotGain"]) - MagentaValue);

                        DiffrentTitleOne = "Y - C";
                        DiffrentTitleTwo = "Y - M";

                        AddPointChartWithColorISO(Y, CHRT_SummaryISODotgain.Series["Series3"], CHRT_SummaryISODotgain.Series["Series3Point"], Convert.ToInt64(DTSpread.Rows[i]["CMYK_Y"]).ToString(), DTSpread.Rows[i]["DotGain"].ToString(), Color.Yellow.Name.ToString(), DTSpread.Rows[i]["DotGainSpreadTh"].ToString(), DiffrentTitleOne, DiffrentTitleTwo, Math.Abs(DiffrentOne).ToString(), Math.Abs(DiffrentTwo).ToString());
                        Y++;
                    }
                }
            }
            #endregion

            #region Spread Spline Range (green boundries)
            if (DTSpreadSplineRange != null && DTSpreadSplineRange.Rows.Count > 0)
            {

                CHRT_SummaryISODotgain.Series["SeriesTop"].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(0), Fuji.Common.CommonFunctions.DecimalRound(0), Fuji.Common.CommonFunctions.DecimalRound(0));
                for (int i = 0; i < DTSpreadSplineRange.Rows.Count; i++)
                {
                    CHRT_SummaryISODotgain.Series["SeriesTop"].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DTSpreadSplineRange.Rows[i]["TVI"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DTSpreadSplineRange.Rows[i]["UpperLimit"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DTSpreadSplineRange.Rows[i]["LowerLimit"].ToString())));

                }
                CHRT_SummaryISODotgain.Series["SeriesTop"].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(100), Fuji.Common.CommonFunctions.DecimalRound(0), Fuji.Common.CommonFunctions.DecimalRound(0));
            }
            #endregion

            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_SummaryISODotgain.ImageUrl) + "." + CHRT_SummaryISODotgain.ImageType;
            CHRT_SummaryISODotgain.Save(Server.MapPath(FileName));

        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    private void AddPointChartWithColorISO(int _Index, Series _objSeries, Series _objSeriesPoint, string _XValue, string _YValue, string ColorName, string _TVISpreadTh, string DiffrentTitleOne, string DiffrentTitleTwo, string DiffrentOne, string DiffrentTwo)
    {
        try
        {
            _objSeries.Type = SeriesChartType.Spline;
            _objSeries.Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));
            //_objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.None;

            _objSeriesPoint.Type = SeriesChartType.Point;
            _objSeriesPoint.Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));
            _objSeriesPoint.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
            _objSeriesPoint.Points[_Index].MarkerStyle = MarkerStyle.Circle;
            _objSeriesPoint.Points[_Index].MarkerSize = 5;
            _objSeriesPoint.Points[_Index].BorderWidth = 1;
            _objSeriesPoint.Points[_Index].MarkerBorderColor = Color.Black;


        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Fill TVI Tab
    private void FillTVIChartISO(DataTable DTTVI)
    {
        DataView DV = new DataView();
        double ThresholdValue = 0;
        double ThresholdTop = 0;
        double ThresholdBottom = 0;
        if (DTTVI != null)
        {

            #region Cyan            
            DTTVI.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Three_Quarter_Tone).ToString() + "))";
            DV = DTTVI.DefaultView;
            DV.Sort = "CMYK_C";
            if (DV.Count > 0)
            {
                for (int i = 0; i < DV.Count; i++)
                {
                    AddTVIPointChartISO(i, CHRT_DotgainISOCyan.Series["Series0"], DV[i]["CMYK_C"].ToString(), DV[i]["Dotgain"].ToString(), DV[i]["HTMLColor"].ToString(), true);
                    if (Convert.ToInt16(DV[i]["Threshold"]) == 0)
                        ThresholdValue = 0;
                    else
                        ThresholdValue = Convert.ToDouble(DV[i]["Threshold"]);

                    ThresholdTop = Convert.ToDouble(DV[i]["TargetDotGain"]) + ThresholdValue;
                    ThresholdBottom = Convert.ToDouble(DV[i]["TargetDotGain"]) - ThresholdValue;


                    CHRT_DotgainISOCyan.Series["SeriesTop"].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["CMYK_C"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(ThresholdTop), Fuji.Common.CommonFunctions.DecimalRound(ThresholdBottom));
                    AddTVIPointChartISO(i, CHRT_DotgainISOCyan.Series["Series1"], DV[i]["CMYK_C"].ToString(), DV[i]["TargetDotGain"].ToString(), Color.Green.Name.ToString(), true);
                }


            }         
            #endregion

            #region Magenta
            ThresholdValue = 0;
            ThresholdTop = 0;
            ThresholdBottom = 0;
            //DTTVI.DefaultView.RowFilter = "(PatchTypeID in (2,8,19,20,21))";
            DTTVI.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Three_Quarter_Tone).ToString() + "))";
            DV = DTTVI.DefaultView;
            DV.Sort = "CMYK_M";
            if (DV.Count > 0)
            {
                for (int i = 0; i < DV.Count; i++)
                {
                    AddTVIPointChartISO(i, CHRT_DotgainISOMeganta.Series["Series0"], DV[i]["CMYK_M"].ToString(), DV[i]["Dotgain"].ToString(), DV[i]["HTMLColor"].ToString(), true);

                    if (Convert.ToInt16(DV[i]["Threshold"]) == 0)
                        ThresholdValue = 0;
                    else
                        ThresholdValue = Convert.ToDouble(DV[i]["Threshold"]);

                    ThresholdTop = Convert.ToDouble(DV[i]["TargetDotGain"]) + ThresholdValue;
                    ThresholdBottom = Convert.ToDouble(DV[i]["TargetDotGain"]) - ThresholdValue;


                    CHRT_DotgainISOMeganta.Series["SeriesTop"].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["CMYK_M"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(ThresholdTop), Fuji.Common.CommonFunctions.DecimalRound(ThresholdBottom));

                    AddTVIPointChartISO(i, CHRT_DotgainISOMeganta.Series["Series1"], DV[i]["CMYK_M"].ToString(), DV[i]["TargetDotGain"].ToString(), Color.Green.Name.ToString(), true);
                }

            }
           
            #endregion

            #region Yellow
            ThresholdValue = 0;
            ThresholdTop = 0;
            ThresholdBottom = 0;            
            DTTVI.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Three_Quarter_Tone).ToString() + "))";

            DV = DTTVI.DefaultView;
            DV.Sort = "CMYK_Y";
            if (DV.Count > 0)
            {
                for (int i = 0; i < DV.Count; i++)
                {
                    AddTVIPointChartISO(i, CHRT_DotgainISOYellow.Series["Series0"], DV[i]["CMYK_Y"].ToString(), DV[i]["Dotgain"].ToString(), DV[i]["HTMLColor"].ToString(), true);

                    if (Convert.ToInt16(DV[i]["Threshold"]) == 0)
                        ThresholdValue = 0;
                    else
                        ThresholdValue = Convert.ToDouble(DV[i]["Threshold"]);

                    ThresholdTop = Convert.ToDouble(DV[i]["TargetDotGain"]) + ThresholdValue;
                    ThresholdBottom = Convert.ToDouble(DV[i]["TargetDotGain"]) - ThresholdValue;


                    CHRT_DotgainISOYellow.Series["SeriesTop"].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["CMYK_Y"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(ThresholdTop), Fuji.Common.CommonFunctions.DecimalRound(ThresholdBottom));

                    AddTVIPointChartISO(i, CHRT_DotgainISOYellow.Series["Series1"], DV[i]["CMYK_Y"].ToString(), DV[i]["TargetDotGain"].ToString(), Color.Green.Name.ToString(), true);
                }

            }
           
            #endregion

            #region Black
            ThresholdValue = 0;
            ThresholdTop = 0;
            ThresholdBottom = 0;
            
            DTTVI.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Black_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Black_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Black_Three_Quarter_Tone).ToString() + "))";
            DV = DTTVI.DefaultView;
            DV.Sort = "CMYK_K";
            if (DV.Count > 0)
            {
                for (int i = 0; i < DV.Count; i++)
                {
                    AddTVIPointChartISO(i, CHRT_DotgainISOBlack.Series["Series0"], DV[i]["CMYK_K"].ToString(), DV[i]["Dotgain"].ToString(), DV[i]["HTMLColor"].ToString(), true);

                    if (Convert.ToInt16(DV[i]["Threshold"]) == 0)
                        ThresholdValue = 0;
                    else
                        ThresholdValue = Convert.ToDouble(DV[i]["Threshold"]);

                    ThresholdTop = Convert.ToDouble(DV[i]["TargetDotGain"]) + ThresholdValue;
                    ThresholdBottom = Convert.ToDouble(DV[i]["TargetDotGain"]) - ThresholdValue;

                    CHRT_DotgainISOBlack.Series["SeriesTop"].Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DV[i]["CMYK_K"].ToString())), Fuji.Common.CommonFunctions.DecimalRound(ThresholdTop), Fuji.Common.CommonFunctions.DecimalRound(ThresholdBottom));
                    AddTVIPointChartISO(i, CHRT_DotgainISOBlack.Series["Series1"], DV[i]["CMYK_K"].ToString(), DV[i]["TargetDotGain"].ToString(), Color.Green.Name.ToString(), true);
                }
            }
            
            #endregion

            #region Other Measurement Dotgain Value            

            #region Cyan            
            DTTVI.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Three_Quarter_Tone).ToString() + "))";
            DV = DTTVI.DefaultView;
            if (DV.Count > 0)
                for (int i = 0; i < DV.Count; i++)
                    AddTVIOtherPointChartISO(i, CHRT_DotgainISOCyan.Series["Series2"], DV[i]["CMYK_C"].ToString(), DV[i]["Dotgain"].ToString(),DV[i]["HTMLColor"].ToString());
                


            #endregion

            #region Magenta            
            DTTVI.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Three_Quarter_Tone).ToString() + "))";
            DV = DTTVI.DefaultView;
            if (DV.Count > 0)
                for (int i = 0; i < DV.Count; i++)
                    AddTVIOtherPointChartISO(i, CHRT_DotgainISOMeganta.Series["Series2"], DV[i]["CMYK_M"].ToString(), DV[i]["Dotgain"].ToString(), DV[i]["HTMLColor"].ToString());
                


            #endregion

            #region Yellow            
            DTTVI.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Three_Quarter_Tone).ToString() + "))";
            DV = DTTVI.DefaultView;
            if (DV.Count > 0)
                for (int i = 0; i < DV.Count; i++)
                    AddTVIOtherPointChartISO(i, CHRT_DotgainISOYellow.Series["Series2"], DV[i]["CMYK_Y"].ToString(), DV[i]["Dotgain"].ToString(), DV[i]["HTMLColor"].ToString());
                

            #endregion

            #region Black            
            DTTVI.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Black_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Black_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Black_Three_Quarter_Tone).ToString() + "))";
            DV = DTTVI.DefaultView;
            if (DV.Count > 0)
                for (int i = 0; i < DV.Count; i++)
                    AddTVIOtherPointChartISO(i, CHRT_DotgainISOBlack.Series["Series2"], DV[i]["CMYK_K"].ToString(), DV[i]["Dotgain"].ToString(), DV[i]["HTMLColor"].ToString());
                

            #endregion


            #endregion


            #region Image Save
            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_DotgainISOCyan.ImageUrl) + "." + CHRT_DotgainISOCyan.ImageType;
            CHRT_DotgainISOCyan.Save(Server.MapPath(FileName));

            FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_DotgainISOMeganta.ImageUrl) + "." + CHRT_DotgainISOMeganta.ImageType;
            CHRT_DotgainISOMeganta.Save(Server.MapPath(FileName));

            FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_DotgainISOYellow.ImageUrl) + "." + CHRT_DotgainISOYellow.ImageType;
            CHRT_DotgainISOYellow.Save(Server.MapPath(FileName));

            FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(CHRT_DotgainISOBlack.ImageUrl) + "." + CHRT_DotgainISOBlack.ImageType;
            CHRT_DotgainISOBlack.Save(Server.MapPath(FileName));
            #endregion
        }

    }
    private void AddTVIOtherPointChartISO(int _Index, Series _objSeries, string _XValue, string _YValue, string HTMLColor)
    {
        try
        {
            _objSeries.Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));
            _objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(HTMLColor);
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
            _objSeries.Points[_Index].MarkerSize = 8;            
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    private void AddTVIPointChartISO(int _Index, Series _objSeries, string _XValue, string _YValue, string HTMLColor, bool showMarker)
    {
        try
        {
            _objSeries.Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));
            if (showMarker)
            {
                _objSeries.Type = SeriesChartType.Spline;
                _objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(HTMLColor);
                _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
                _objSeries.Points[_Index].MarkerSize = 8;
            }
            _objSeries.Points[_Index].BorderWidth = 1;


        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Fill Gray Balance Chart
    private void FillGryaBalanceChartISO(DataTable DT)
    {
        try
        {

            Double CValue = 0;
            //========== Clear Points and Show Chart ==========//
            //Chart_GrayBalanceEISO.Visible = true;
            Chart_GrayBalanceEISO.Series[0].Points.Clear();
            Chart_GrayBalanceEISO.Series[1].Points.Clear();
            Chart_GrayBalanceEISO.Series[2].Points.Clear();

            #region //========== Declarations ==========//
            // === Series0 to display Thresold value === //
            Series objSeries0 = (Series)Chart_GrayBalanceEISO.Series[0];
            objSeries0.XValueType = ChartValueTypes.String;
            objSeries0.YValueType = ChartValueTypes.String;
            objSeries0.ShowInLegend = false;
            objSeries0.Type = SeriesChartType.Column;
            objSeries0.BackGradientType = GradientType.DiagonalRight;

            // === Series1 to display Actual value === //
            Series objSeries1 = (Series)Chart_GrayBalanceEISO.Series[1];
            objSeries1.XValueType = ChartValueTypes.String;
            objSeries1.YValueType = ChartValueTypes.String;
            objSeries1.ShowInLegend = false;
            objSeries1.Type = SeriesChartType.Column;


            // === Series2 to display red marker on Thresold value === //
            Series objSeries2 = (Series)Chart_GrayBalanceEISO.Series[2];
            objSeries2.XValueType = ChartValueTypes.String;
            objSeries2.YValueType = ChartValueTypes.String;
            objSeries2.ShowInLegend = false;
            objSeries2.Type = SeriesChartType.Column;

            // === Make Chart area look like 2D even it is 3D === //
            Chart_GrayBalanceEISO.ChartAreas["ChartArea1"].Area3DStyle.RightAngleAxes = true;
            Chart_GrayBalanceEISO.ChartAreas["ChartArea1"].Area3DStyle.Clustered = true;
            Chart_GrayBalanceEISO.ChartAreas["ChartArea1"].Area3DStyle.XAngle = 0;
            Chart_GrayBalanceEISO.ChartAreas["ChartArea1"].Area3DStyle.YAngle = 0;
            #endregion


            if (DT != null && DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    CValue = Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(DT.Rows[i]["ΔE"]));

                    if (Convert.ToInt16(DT.Rows[i]["Gray"]).ToString() == "25")
                    {
                        AddGryaBalancePointISO(i, objSeries0, objSeries1, objSeries2, "25", CValue.ToString(), "#EAEEEC", DT.Rows[i]["ΔE"].ToString(), DT.Rows[i]["ΔELimit"].ToString());
                    }
                    else if (Convert.ToInt16(DT.Rows[i]["Gray"]).ToString() == "50")
                    {
                        AddGryaBalancePointISO(i, objSeries0, objSeries1, objSeries2, "50", CValue.ToString(), "#A9ADAC", DT.Rows[i]["ΔE"].ToString(), DT.Rows[i]["ΔELimit"].ToString());
                    }
                    else if (Convert.ToInt16(DT.Rows[i]["Gray"]).ToString() == "75")
                    {
                        AddGryaBalancePointISO(i, objSeries0, objSeries1, objSeries2, "75", CValue.ToString(), "#424242", DT.Rows[i]["ΔE"].ToString(), DT.Rows[i]["ΔELimit"].ToString());
                    }
                }
            }
            else
            {
                Chart_GrayBalanceEISO.ChartAreas["Default"].AxisY.Minimum = -10;
                Chart_GrayBalanceEISO.ChartAreas["Default"].AxisY.Maximum = 10;

                AddGryaBalancePointISO(0, objSeries0, objSeries1, objSeries2, "25", "0", "#E9E9E9", "0", "0");
                AddGryaBalancePointISO(1, objSeries0, objSeries1, objSeries2, "50", "0", "#E9E9E9", "0", "0");
                AddGryaBalancePointISO(2, objSeries0, objSeries1, objSeries2, "75", "0", "#E9E9E9", "0", "0");
            }
            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(Chart_GrayBalanceEISO.ImageUrl) + "." + Chart_GrayBalanceEISO.ImageType;
            Chart_GrayBalanceEISO.Save(Server.MapPath(FileName));
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    private void AddGryaBalancePointISO(int Index, Series objSeries0, Series objSeries1, Series objSeries2, string _XValue, string _YValue, string HTMLColor, string _Actual, string _Limit)
    {
        try
        {
            objSeries0.Points.AddXY(_XValue, _Limit);
            objSeries0["PointWidth"] = "0.4";
            objSeries1.Points.AddXY(_XValue, _YValue);    
    
            //for red threshold marker
            objSeries2.Points.AddXY(_XValue, _Limit);
            objSeries1.Points[Index].Color = System.Drawing.ColorTranslator.FromHtml(HTMLColor);
            objSeries1["PointWidth"] = "0.4";
        }
        catch (Exception Ex)
        {
            throw Ex;
        }
    }
    #endregion

    #region Fill L* a* b* Chart
    private void FillLABChartISO(DataTable DT)
    {
        try
        {
            Chart_LISO.Series["Series1"].Points.Clear();
            Chart_aISO.Series["Series1"].Points.Clear();
            Chart_bISO.Series["Series1"].Points.Clear();
            Chart_LISO.Series["Series2"].Points.Clear();
            Chart_aISO.Series["Series2"].Points.Clear();
            Chart_bISO.Series["Series2"].Points.Clear();

            if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    AddPointLABChartISO(i, Chart_LISO.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["CIELAB_L*"].ToString(), Color.Black.Name.ToString(), DT.Rows[i], true);
                    AddPointLABChartISO(i, Chart_aISO.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["CIELAB_a*"].ToString(), Color.Black.Name.ToString(), DT.Rows[i], true);
                    AddPointLABChartISO(i, Chart_bISO.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["CIELAB_b*"].ToString(), Color.Black.Name.ToString(), DT.Rows[i], true);

                    if (DT.Rows[i]["Target_L*"] != DBNull.Value)
                    {
                        AddPointLABChartISO(i, Chart_LISO.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["Target_L*"].ToString(), Color.Green.Name.ToString(), DT.Rows[i], false);
                    }
                    AddPointLABChartISO(i, Chart_aISO.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["Target_a*"].ToString(), Color.Green.Name.ToString(), DT.Rows[i], false);
                    AddPointLABChartISO(i, Chart_bISO.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["Target_b*"].ToString(), Color.Green.Name.ToString(), DT.Rows[i], false);
                }
            }
            string FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(Chart_LISO.ImageUrl) + "." + Chart_LISO.ImageType;
            Chart_LISO.Save(Server.MapPath(FileName));

            FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(Chart_aISO.ImageUrl) + "." + Chart_aISO.ImageType;
            Chart_aISO.Save(Server.MapPath(FileName));

            FileName = "UploadFiles/temp/" + Fuji.Common.Sessions.UserID + "_" + Path.GetFileName(Chart_bISO.ImageUrl) + "." + Chart_bISO.ImageType;
            Chart_bISO.Save(Server.MapPath(FileName));
        }
        catch (Exception Exc)
        {
            throw Exc;
        }
    }
    private void AddPointLABChartISO(int _Index, Series _objSeries, string _XValue, string _YValue, string ColorName, DataRow DR, bool isActual)
    {
        try
        {
            _objSeries.Type = SeriesChartType.Spline;
            _objSeries.Points.AddXY(Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_XValue)), Fuji.Common.CommonFunctions.DecimalRound(Convert.ToDouble(_YValue)));            
            
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

    #endregion
}
