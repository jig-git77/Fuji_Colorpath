using System;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using System.Text;
using System.IO;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ReportSingleMeasurement : BaseControl, IViewReportSingleMeasurement
    {
        #region Variable Declarations
        private PresenterReportSingleMeasurement _presenterReportSingleMeasurement;

        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterReportSingleMeasurement = new PresenterReportSingleMeasurement(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (txt_MeasurementID.Text != null)
                {
                    MeasurementId = Convert.ToInt32(txt_MeasurementID.Text);
                    From_ΔE = "E2000";
                    DSReport = new DataSet();
                    _presenterReportSingleMeasurement.GetReportDataOnMeasurement();
                    if (!IsErrorOccured)
                    {
                        if (DSReport != null && DSReport.Tables.Count > 0)
                        {
                            CreateReport(DSReport);
                        }
                    }
                }
            }
        }
        #endregion

        #region Fill Charts
        private void CreateReport(DataSet DSReport)
        {
            try
            {
              
                // ===== Get Data from data base  ==================//             

                if (DSReport != null && DSReport.Tables.Count > 0)
                {

                    #region " === Page : 1 Result Summary === "

                    Bind_TargetVersusActual_CHART(DSReport.Tables[1]); // Bind Chart Of Target And Actual Value
                    
                    BindSummaryDeltaValueBarChart(DSReport.Tables[2]); // Bind Chart of Summary Delta Value Bar Chart
                    
                    BindSummaryDeltaEValueBarChart(DSReport.Tables[3], 0, 11); //Bind Chart of Summary Delta E Value Bar Chart

                    #endregion

                    #region " === Page : 2 === "

                    BindLABDeviationGraph(DSReport.Tables[4]); //Bind Charts Of Gray L* A* B* Deviation Line Chart



                    //CalcNPDBlackAndGrayChartsMaxMin(DSReport.Tables[4], DSReport.Tables[5]); // Calculating Max Min Value For NPD Charts
                    //BindGrayNPDGraph(DSReport.Tables[4]);  //Bind Chart Of Gray NPD Line Chart
                    //BindBlackNPDGraph(DSReport.Tables[5]);  //Bind Chart Of Black NPD Line Chart

                    #endregion

                    #region " === Page : 3  === "

                    //BindDotgainActualGraph(DSReport.Tables[6]);  //Bind Chart DotGain Actual Line Chart


                    //BindDotgainDeviationGraph(DSReport.Tables[8]);  //Bind Chart DotGain Deviation Bar Chart


                    //BindDensityDeviationGraph(DSReport.Tables[7]);  //Bind Chart Density Deviation Bar Chart               


                    //BindPrintContrastDeviationGraph(DSReport.Tables[9]); //Bind Chart Print Contrast Deviation Bar Chart
                    #endregion

                }
                CreateReportPDF(DSReport); // Generates PDF of Reports.
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void CreateReportPDF(DataSet Ds)
        {
            try
            {
                // === Set all parameters to generate pdf file === //              

                Common.ReportPDF objReportPDF = new Common.ReportPDF();
                objReportPDF.ReportType = (int)enumReportType.ReportSingleMeasurement;
                objReportPDF.PDFPath = Server.MapPath("PDFReports/");
                objReportPDF.PDF_NAME = SessionManager.Instance.CurrentUser.UserId + "_ReportSingleMeasurement.PDF";
                objReportPDF.DS_Common = Ds;
                objReportPDF.MakePDF();

                // === After generat and save PDF file, Download it at a time === //
                //Common.Utilities.DownloadFile(objReportPDF.PDFPath, objReportPDF.PDF_NAME, objReportPDF.PDF_NAME);
            }
            catch (Exception Ex)
            {
            }
        }
        #endregion

        #region " === Spider Actual AB Plot === "
        private void Bind_TargetVersusActual_CHART(DataTable dtGamut)
        {
            try
            {
                DataView DV = new DataView();
                if (dtGamut != null && dtGamut.Rows.Count > 0)
                {
                    #region For Cyan Or Paper Line
                    dtGamut.DefaultView.RowFilter = "PatchType = 'Cyan' OR PatchType = 'Paper'";
                    dtGamut.DefaultView.Sort = " Rank asc";
                    DV = dtGamut.DefaultView;
                    if (DV.Count > 0)
                        for (int i = 0; i < DV.Count; i = i + 1)
                        {
                            CHART_TargetVersusActualAB.Series[0].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(DV[i]["Targeta"])), Utilities.DecimalRound(Convert.ToDouble(DV[i]["Targetb"])));
                            CHART_TargetVersusActualAB.Series[0].ChartType = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[0].Points[i].BorderWidth = 1;
                            CHART_TargetVersusActualAB.Series[0].Points[i].Color = Color.Green;
                            CHART_TargetVersusActualAB.Series[0].Points[i].MarkerColor = Color.Green;

                            if (i == 0)
                            {
                                CHART_TargetVersusActualAB.Series[6].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_a"])), Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_b"])));
                                CHART_TargetVersusActualAB.Series[6].ChartType = SeriesChartType.Line;
                                CHART_TargetVersusActualAB.Series[6].Points[i].BorderWidth = 2;
                                CHART_TargetVersusActualAB.Series[6].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                                CHART_TargetVersusActualAB.Series[6].Points[i].MarkerColor = Color.White;
                                CHART_TargetVersusActualAB.Series[6].Points[i].MarkerBorderColor = Color.Black;
                            }
                            else
                            {

                                CHART_TargetVersusActualAB.Series[6].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_a"])), Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_b"])));
                                CHART_TargetVersusActualAB.Series[6].ChartType = SeriesChartType.Line;
                                CHART_TargetVersusActualAB.Series[6].Points[i].BorderWidth = 2;
                                CHART_TargetVersusActualAB.Series[6].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                                CHART_TargetVersusActualAB.Series[6].Points[i].MarkerColor = Color.Cyan;
                                CHART_TargetVersusActualAB.Series[6].Points[i].MarkerBorderColor = Color.Black;
                            }


                        }


                    #endregion

                    #region For Green Line
                    dtGamut.DefaultView.RowFilter = "PatchType = 'Green' OR PatchType = 'Paper'";
                    dtGamut.DefaultView.Sort = " Rank asc";
                    DV = dtGamut.DefaultView;
                    if (DV.Count > 0)
                        for (int i = 0; i < DV.Count; i = i + 1)
                        {
                            CHART_TargetVersusActualAB.Series[1].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(DV[i]["Targeta"])), Utilities.DecimalRound(Convert.ToDouble(DV[i]["Targetb"])));
                            CHART_TargetVersusActualAB.Series[1].ChartType = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[1].Points[i].BorderWidth = 1;
                            CHART_TargetVersusActualAB.Series[1].Points[i].Color = Color.Green;
                            CHART_TargetVersusActualAB.Series[1].Points[i].MarkerColor = Color.Green;
                            if (i == 0)
                            {
                                CHART_TargetVersusActualAB.Series[7].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_a"])), Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_b"])));
                                CHART_TargetVersusActualAB.Series[7].ChartType = SeriesChartType.Line;
                                CHART_TargetVersusActualAB.Series[7].Points[i].BorderWidth = 2;
                                CHART_TargetVersusActualAB.Series[7].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                                CHART_TargetVersusActualAB.Series[7].Points[i].MarkerColor = Color.White;
                                CHART_TargetVersusActualAB.Series[7].Points[i].MarkerBorderColor = Color.Black;
                            }
                            else
                            {

                                CHART_TargetVersusActualAB.Series[7].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_a"])), Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_b"])));
                                CHART_TargetVersusActualAB.Series[7].ChartType = SeriesChartType.Line;
                                CHART_TargetVersusActualAB.Series[7].Points[i].BorderWidth = 2;
                                CHART_TargetVersusActualAB.Series[7].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                                CHART_TargetVersusActualAB.Series[7].Points[i].MarkerColor = Color.Green;
                                CHART_TargetVersusActualAB.Series[7].Points[i].MarkerBorderColor = Color.Black;
                            }
                        }
                    #endregion

                    #region For Yellow Line
                    dtGamut.DefaultView.RowFilter = "PatchType = 'Yellow' OR PatchType = 'Paper'";
                    dtGamut.DefaultView.Sort = " Rank asc";
                    DV = dtGamut.DefaultView;
                    if (DV.Count > 0)
                        for (int i = 0; i < DV.Count; i = i + 1)
                        {
                            CHART_TargetVersusActualAB.Series[2].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(DV[i]["Targeta"])), Utilities.DecimalRound(Convert.ToDouble(DV[i]["Targetb"])));
                            CHART_TargetVersusActualAB.Series[2].ChartType = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[2].Points[i].BorderWidth = 1;
                            CHART_TargetVersusActualAB.Series[2].Points[i].Color = Color.Green;
                            CHART_TargetVersusActualAB.Series[2].Points[i].MarkerColor = Color.Green;

                            if (i == 0)
                            {
                                CHART_TargetVersusActualAB.Series[8].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_a"])), Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_b"])));
                                CHART_TargetVersusActualAB.Series[8].ChartType = SeriesChartType.Line;
                                CHART_TargetVersusActualAB.Series[8].Points[i].BorderWidth = 2;
                                CHART_TargetVersusActualAB.Series[8].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                                CHART_TargetVersusActualAB.Series[8].Points[i].MarkerColor = Color.White;
                                CHART_TargetVersusActualAB.Series[8].Points[i].MarkerBorderColor = Color.Black;
                            }
                            else
                            {

                                CHART_TargetVersusActualAB.Series[8].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_a"])), Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_b"])));
                                CHART_TargetVersusActualAB.Series[8].ChartType = SeriesChartType.Line;
                                CHART_TargetVersusActualAB.Series[8].Points[i].BorderWidth = 2;
                                CHART_TargetVersusActualAB.Series[8].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                                CHART_TargetVersusActualAB.Series[8].Points[i].MarkerColor = Color.Yellow;
                                CHART_TargetVersusActualAB.Series[8].Points[i].MarkerBorderColor = Color.Black;
                            }
                        }
                    #endregion

                    #region For Red Line
                    dtGamut.DefaultView.RowFilter = "PatchType = 'Red' OR PatchType = 'Paper'";
                    dtGamut.DefaultView.Sort = " Rank asc";
                    DV = dtGamut.DefaultView;
                    if (DV.Count > 0)
                        for (int i = 0; i < DV.Count; i = i + 1)
                        {
                            CHART_TargetVersusActualAB.Series[3].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(DV[i]["Targeta"])), Utilities.DecimalRound(Convert.ToDouble(DV[i]["Targetb"])));
                            CHART_TargetVersusActualAB.Series[3].ChartType = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[3].Points[i].BorderWidth = 1;
                            CHART_TargetVersusActualAB.Series[3].Points[i].Color = Color.Green;
                            CHART_TargetVersusActualAB.Series[3].Points[i].MarkerColor = Color.Green;

                            if (i == 0)
                            {
                                CHART_TargetVersusActualAB.Series[9].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_a"])), Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_b"])));
                                CHART_TargetVersusActualAB.Series[9].ChartType = SeriesChartType.Line;
                                CHART_TargetVersusActualAB.Series[9].Points[i].BorderWidth = 2;
                                CHART_TargetVersusActualAB.Series[9].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                                CHART_TargetVersusActualAB.Series[9].Points[i].MarkerColor = Color.White;
                                CHART_TargetVersusActualAB.Series[9].Points[i].MarkerBorderColor = Color.Black;
                            }
                            else
                            {

                                CHART_TargetVersusActualAB.Series[9].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_a"])), Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_b"])));
                                CHART_TargetVersusActualAB.Series[9].ChartType = SeriesChartType.Line;
                                CHART_TargetVersusActualAB.Series[9].Points[i].BorderWidth = 2;
                                CHART_TargetVersusActualAB.Series[9].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                                CHART_TargetVersusActualAB.Series[9].Points[i].MarkerColor = Color.Red;
                                CHART_TargetVersusActualAB.Series[9].Points[i].MarkerBorderColor = Color.Black;
                            }
                        }
                    #endregion

                    #region For Magenta Line
                    dtGamut.DefaultView.RowFilter = "PatchType = 'Magenta' OR PatchType = 'Paper'";
                    dtGamut.DefaultView.Sort = " Rank asc";
                    DV = dtGamut.DefaultView;
                    if (DV.Count > 0)
                        for (int i = 0; i < DV.Count; i = i + 1)
                        {
                            CHART_TargetVersusActualAB.Series[4].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(DV[i]["Targeta"])), Utilities.DecimalRound(Convert.ToDouble(DV[i]["Targetb"])));
                            CHART_TargetVersusActualAB.Series[4].ChartType = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[4].Points[i].BorderWidth = 1;
                            CHART_TargetVersusActualAB.Series[4].Points[i].Color = Color.Green;
                            CHART_TargetVersusActualAB.Series[4].Points[i].MarkerColor = Color.Green;
                            if (i == 0)
                            {
                                CHART_TargetVersusActualAB.Series[10].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_a"])), Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_b"])));
                                CHART_TargetVersusActualAB.Series[10].ChartType = SeriesChartType.Line;
                                CHART_TargetVersusActualAB.Series[10].Points[i].BorderWidth = 2;
                                CHART_TargetVersusActualAB.Series[10].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                                CHART_TargetVersusActualAB.Series[10].Points[i].MarkerColor = Color.White;
                                CHART_TargetVersusActualAB.Series[10].Points[i].MarkerBorderColor = Color.Black;
                            }
                            else
                            {

                                CHART_TargetVersusActualAB.Series[10].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_a"])), Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_b"])));
                                CHART_TargetVersusActualAB.Series[10].ChartType = SeriesChartType.Line;
                                CHART_TargetVersusActualAB.Series[10].Points[i].BorderWidth = 2;
                                CHART_TargetVersusActualAB.Series[10].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                                CHART_TargetVersusActualAB.Series[10].Points[i].MarkerColor = Color.Magenta;
                                CHART_TargetVersusActualAB.Series[10].Points[i].MarkerBorderColor = Color.Black;
                            }
                        }
                    #endregion

                    #region For Blue Line
                    dtGamut.DefaultView.RowFilter = "PatchType = 'Blue' OR PatchType = 'Paper'";
                    dtGamut.DefaultView.Sort = " Rank asc";
                    DV = dtGamut.DefaultView;
                    if (DV.Count > 0)
                        for (int i = 0; i < DV.Count; i = i + 1)
                        {

                            CHART_TargetVersusActualAB.Series[5].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(DV[i]["Targeta"])), Utilities.DecimalRound(Convert.ToDouble(DV[i]["Targetb"])));
                            CHART_TargetVersusActualAB.Series[5].ChartType = SeriesChartType.Line;
                            CHART_TargetVersusActualAB.Series[5].Points[i].BorderWidth = 1;
                            CHART_TargetVersusActualAB.Series[5].Points[i].Color = Color.Green;
                            CHART_TargetVersusActualAB.Series[5].Points[i].MarkerColor = Color.Green;
                            if (i == 0)
                            {
                                CHART_TargetVersusActualAB.Series[11].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_a"])), Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_b"])));
                                CHART_TargetVersusActualAB.Series[11].ChartType = SeriesChartType.Line;
                                CHART_TargetVersusActualAB.Series[11].Points[i].BorderWidth = 2;
                                CHART_TargetVersusActualAB.Series[11].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                                CHART_TargetVersusActualAB.Series[11].Points[i].MarkerColor = Color.White;
                                CHART_TargetVersusActualAB.Series[11].Points[i].MarkerBorderColor = Color.Black;
                            }
                            else
                            {

                                CHART_TargetVersusActualAB.Series[11].Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_a"])), Utilities.DecimalRound(Convert.ToDouble(DV[i]["CIELAB_b"])));
                                CHART_TargetVersusActualAB.Series[11].ChartType = SeriesChartType.Line;
                                CHART_TargetVersusActualAB.Series[11].Points[i].BorderWidth = 2;
                                CHART_TargetVersusActualAB.Series[11].Points[i].Color = System.Drawing.ColorTranslator.FromHtml("#000000");
                                CHART_TargetVersusActualAB.Series[11].Points[i].MarkerColor = Color.Blue;
                                CHART_TargetVersusActualAB.Series[11].Points[i].MarkerBorderColor = Color.Black;
                            }
                        }
                    #endregion

                }

                string FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(CHART_TargetVersusActualAB.ImageLocation) + "." + CHART_TargetVersusActualAB.ImageType;
                CHART_TargetVersusActualAB.SaveImage(Server.MapPath(FileName));
            }
            catch (Exception Exc)
            { 
            
            }
        }
        #endregion

        #region " ==== Summary Delta Value Chart === "
        private void BindSummaryDeltaValueBarChart(DataTable DT)
        {
            try
            {
                ChartResultSummary_Delta.BorderColor = Color.White;
                ChartResultSummary_Delta.BorderWidth = 0;
                ChartResultSummary_Delta.BackColor = Color.White;

                //========== Clear Points and Show Chart ==========//
                ChartResultSummary_Delta.Visible = true;
                ChartResultSummary_Delta.Series[0].Points.Clear();
                ChartResultSummary_Delta.Series[1].Points.Clear();
          
                #region //========== Declarations ==========//
                // === Series0 to display Thresold value === //
                Series objSeries0 = (Series)ChartResultSummary_Delta.Series[0];
                objSeries0.XValueType = ChartValueType.Int32;
                objSeries0.YValueType = ChartValueType.String;
                objSeries0.IsVisibleInLegend = false;
                objSeries0.ChartType = SeriesChartType.Bar;
                objSeries0.BackGradientStyle = GradientStyle.DiagonalRight;

                // === Series1 to display Actual value === //
                Series objSeries1 = (Series)ChartResultSummary_Delta.Series[1];
                objSeries1.XValueType = ChartValueType.Int32;
                objSeries1.YValueType = ChartValueType.String;
                objSeries1.IsVisibleInLegend = false;
                objSeries1.ChartType = SeriesChartType.Bar;
                objSeries1.CustomProperties = "DrawingStyle=Cylinder";             

                // === Make Chart area look like 2D even it is 3D === //
                ChartResultSummary_Delta.ChartAreas["ChartArea1"].Area3DStyle.IsRightAngleAxes = true;
                ChartResultSummary_Delta.ChartAreas["ChartArea1"].Area3DStyle.IsClustered = true;              
                ChartResultSummary_Delta.ChartAreas["ChartArea1"].BackColor = Color.White;
                #endregion

                #region //========== ChartArea Settings ==========//
                ChartResultSummary_Delta.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
                ChartResultSummary_Delta.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;

                ChartResultSummary_Delta.ChartAreas["ChartArea1"].Position.Auto = true;
                ChartResultSummary_Delta.ChartAreas["ChartArea1"].Position.X = float.Parse("0");
                ChartResultSummary_Delta.ChartAreas["ChartArea1"].Position.Y = float.Parse("5");
                ChartResultSummary_Delta.ChartAreas["ChartArea1"].Position.Width = float.Parse("99");
                ChartResultSummary_Delta.ChartAreas["ChartArea1"].Position.Height = float.Parse("97");

                ChartResultSummary_Delta.ChartAreas["ChartArea1"].InnerPlotPosition.Auto = true;
                ChartResultSummary_Delta.ChartAreas["ChartArea1"].InnerPlotPosition.X = float.Parse("0");
                ChartResultSummary_Delta.ChartAreas["ChartArea1"].InnerPlotPosition.Y = float.Parse("2");
                ChartResultSummary_Delta.ChartAreas["ChartArea1"].InnerPlotPosition.Width = float.Parse("98");
                ChartResultSummary_Delta.ChartAreas["ChartArea1"].InnerPlotPosition.Height = float.Parse("97");

                ChartResultSummary_Delta.ChartAreas["ChartArea1"].ShadowOffset = 0;
                ChartResultSummary_Delta.BorderSkin.SkinStyle = BorderSkinStyle.None;
                ChartResultSummary_Delta.ChartAreas["ChartArea1"].BorderWidth = 0;
                ChartResultSummary_Delta.BorderSkin.BorderWidth = 0;

                #endregion

                #region " === Fill Series === "
                // === Limit with red marker === //
                if (DT != null && DT.Rows.Count > 0)
                {
                    if (Convert.IsDBNull(DT.Rows[0]["Avg_ΔE"]) || Convert.IsDBNull(DT.Rows[0]["ΔELimit"]))
                        BindDeltaValueACTTHValue(objSeries0, objSeries1,  0, 0, 0);
                    else
                        BindDeltaValueACTTHValue(objSeries0, objSeries1,  Utilities.DecimalRound(Convert.ToDouble(DT.Rows[0]["Avg_ΔE"].ToString())), Utilities.DecimalRound(Convert.ToDouble(DT.Rows[0]["ΔELimit"].ToString())), 0);

                    if (Convert.IsDBNull(DT.Rows[0]["Max_ΔE"]) || Convert.IsDBNull(DT.Rows[0]["ΔELimit"]))
                        BindDeltaValueACTTHValue(objSeries0, objSeries1,  0, 0, 1);
                    else
                        BindDeltaValueACTTHValue(objSeries0, objSeries1,  Utilities.DecimalRound(Convert.ToDouble(DT.Rows[0]["Max_ΔE"].ToString())), Utilities.DecimalRound(Convert.ToDouble(DT.Rows[0]["ΔELimit"].ToString())), 1);

                    if (Convert.IsDBNull(DT.Rows[0]["GrayΔE"]) || Convert.IsDBNull(DT.Rows[0]["ΔELimit"]))
                        BindDeltaValueACTTHValue(objSeries0, objSeries1,  0, 0, 4);
                    else
                        BindDeltaValueACTTHValue(objSeries0, objSeries1,  Utilities.DecimalRound(Convert.ToDouble(DT.Rows[0]["GrayΔE"].ToString())), Utilities.DecimalRound(Convert.ToDouble(DT.Rows[0]["ΔELimit"].ToString())), 2);

                    if (Convert.IsDBNull(DT.Rows[0]["NinetyFiveΔE"]) || Convert.IsDBNull(DT.Rows[0]["ΔELimit"]))
                        BindDeltaValueACTTHValue(objSeries0, objSeries1,  0, 0, 5);
                    else
                        BindDeltaValueACTTHValue(objSeries0, objSeries1, Utilities.DecimalRound(Convert.ToDouble(DT.Rows[0]["NinetyFiveΔE"].ToString())), Utilities.DecimalRound(Convert.ToDouble(DT.Rows[0]["ΔELimit"].ToString())), 3);
                                      
                    // === Save Chart Image after generating image with proper data === //
                    string FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(ChartResultSummary_Delta.ImageLocation) + "." + ChartResultSummary_Delta.ImageType;
                    ChartResultSummary_Delta.SaveImage(Server.MapPath(FileName));
                }
                #endregion
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        private void BindDeltaValueACTTHValue(Series objSeries0, Series objSeries1, double ACT, double TH, int Index)
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
                objSeries0.Points[Index]["PointWidth"] = "0.8";
                objSeries1.Points.AddXY((Double)0, Y2);
               
                objSeries0.Points[Index].BackGradientStyle = GradientStyle.DiagonalRight;

                SeriesActual.Points[Index]["PointWidth"] = "0.8";
       
                if (Y1 == 0 && Y2 == 0)
                {
                    objSeries0.Points[Index].MarkerImage = "";                   
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region " ==== Summary Delta E Value Chart === "
        private void BindSummaryDeltaEValueBarChart(DataTable DT, int _StartIndex, int _EndIndex)
        {
            try
            {
                ChartResultSummary_DeltaEValue.BorderColor = Color.White;
                ChartResultSummary_DeltaEValue.BorderWidth = 0;
                ChartResultSummary_DeltaEValue.BackColor = Color.White;

                //========== Clear Points and Show Chart ==========//
                ChartResultSummary_DeltaEValue.Visible = true;
                ChartResultSummary_DeltaEValue.Series[0].Points.Clear();
                ChartResultSummary_DeltaEValue.Series[1].Points.Clear();             

                #region //========== Declarations ==========//
                // === Series0 to display Thresold value === //
                Series objSeries0 = (Series)ChartResultSummary_DeltaEValue.Series[0];
                objSeries0.XValueType = ChartValueType.Int32;
                objSeries0.YValueType = ChartValueType.String;
                objSeries0.IsVisibleInLegend = false;
                objSeries0.ChartType = SeriesChartType.Bar;
                //objSeries0.Color = Color.Gray;
                objSeries0.BackGradientStyle = GradientStyle.DiagonalRight;

                // === Series1 to display Actual value === //
                Series objSeries1 = (Series)ChartResultSummary_DeltaEValue.Series[1];
                objSeries1.XValueType = ChartValueType.Int32;
                objSeries1.YValueType = ChartValueType.String;
                objSeries1.IsVisibleInLegend = false;
                objSeries1.ChartType = SeriesChartType.Bar;
                objSeries1.CustomProperties = "DrawingStyle=Cylinder";             

                // === Make Chart area look like 2D even it is 3D === //
                ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].Area3DStyle.IsRightAngleAxes = true;
                ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].Area3DStyle.IsClustered = true;              
                ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].BackColor = Color.White;
                #endregion

                #region //========== ChartArea Settings ==========//
                ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].AxisX.LabelStyle.Enabled = false;
                ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;

                ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].Position.Auto = true;
                ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].Position.X = float.Parse("0");
                ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].Position.Y = float.Parse("5");
                ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].Position.Width = float.Parse("99");
                ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].Position.Height = float.Parse("97");

                ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].InnerPlotPosition.Auto = true;
                ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].InnerPlotPosition.X = float.Parse("0");
                ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].InnerPlotPosition.Y = float.Parse("2");
                ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].InnerPlotPosition.Width = float.Parse("98");
                ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].InnerPlotPosition.Height = float.Parse("97");

                ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].ShadowOffset = 0;
                ChartResultSummary_DeltaEValue.BorderSkin.SkinStyle = BorderSkinStyle.None;
                ChartResultSummary_DeltaEValue.ChartAreas["ChartArea1"].BorderWidth = 0;
                ChartResultSummary_DeltaEValue.BorderSkin.BorderWidth = 0;

                #endregion

                #region " === Fill Series === "
                // === Limit with red marker === //
                for (int i = _StartIndex; i < _EndIndex; i = i + 1)
                {
                    if (DT != null && i < DT.Rows.Count)
                        BindDeltaEValueACTTHValue(objSeries0, objSeries1, DT.Rows[i], i);
                    else
                        BindDeltaEValueACTTHValue(objSeries0, objSeries1, null, i);
                }


                // === Save Chart Image after generating image with proper data === //
                string FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(ChartResultSummary_DeltaEValue.ImageLocation) + "." + ChartResultSummary_DeltaEValue.ImageType;
                ChartResultSummary_DeltaEValue.SaveImage(Server.MapPath(FileName));

                #endregion
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void BindDeltaEValueACTTHValue(Series objSeries0, Series objSeries1, DataRow DR, int Index)
        {
            try
            {
                string strHTMLColor = "";
                Double Y1, Y2;
                Y1 = Y2 = 0;
                
                if (DR != null)
                {
                    Y1 = (Convert.ToString(DR["ΔELimit"]) == "" ? 0 : Utilities.DecimalRound(Convert.ToDouble(DR["ΔELimit"].ToString())));
                    Y2 = (Convert.ToString(DR["ΔE"]) == "" ? 0 : Utilities.DecimalRound(Convert.ToDouble(DR["ΔE"].ToString())));
                    strHTMLColor = Convert.ToString(DR["HTMLColor"].ToString());
                }

                // === When Actual value is not available then no series will display and no marker will display
                if (Y2 == 0)
                    Y1 = Y2;


                Series SeriesActual = objSeries1;
                objSeries0.Points.AddXY((Double)0, Y1);
                objSeries0.Points[Index]["PointWidth"] = "0.8";
                objSeries1.Points.AddXY((Double)0, Y2);             
                objSeries0.Points[Index].BackGradientStyle = GradientStyle.DiagonalRight;

                SeriesActual.Points[Index].Color = System.Drawing.ColorTranslator.FromHtml(strHTMLColor);
                SeriesActual.Points[Index]["PointWidth"] = "0.8";
          
                if (Y1 == 0 && Y2 == 0)
                {
                    objSeries0.Points[Index].MarkerImage = "";
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion

        #region " ==== Gray L* A* B* Deviation Graph ==== "
        private void BindLABDeviationGraph(DataTable DT)
        {
            try
            {
            
                //Double LCValue = 0;
                //Double LMaxValue = 0;
                //Double LMinValue = 0;
                //Double aCValue = 0;
                //Double aMaxValue = 0;
                //Double aMinValue = 0;
                //Double bCValue = 0;
                //Double bMaxValue = 0;
                //Double bMinValue = 0;

            
                if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
                {
                    CHRT_GrayDeviation_L.Series["Series1"].Points.Clear();
                    CHRT_GrayDeviation_A.Series["Series1"].Points.Clear();
                    CHRT_GrayDeviation_B.Series["Series1"].Points.Clear();
                    CHRT_GrayDeviation_L.Series["Series2"].Points.Clear();
                    CHRT_GrayDeviation_A.Series["Series2"].Points.Clear();
                    CHRT_GrayDeviation_B.Series["Series2"].Points.Clear();

                    #region //========== ChartArea Settings ==========//
                    //========= Setting For Gray L Deviation Graph ==============//
                    CHRT_GrayDeviation_L.ChartAreas["Default"].Position.Auto = true;
                    CHRT_GrayDeviation_L.ChartAreas["Default"].Position.X = float.Parse("3");
                    CHRT_GrayDeviation_L.ChartAreas["Default"].Position.Y = float.Parse("5");
                    CHRT_GrayDeviation_L.ChartAreas["Default"].Position.Width = float.Parse("91");
                    CHRT_GrayDeviation_L.ChartAreas["Default"].Position.Height = float.Parse("86");

                    CHRT_GrayDeviation_L.ChartAreas["Default"].InnerPlotPosition.Auto = true;
                    CHRT_GrayDeviation_L.ChartAreas["Default"].InnerPlotPosition.X = float.Parse("5");
                    CHRT_GrayDeviation_L.ChartAreas["Default"].InnerPlotPosition.Y = float.Parse("5");
                    CHRT_GrayDeviation_L.ChartAreas["Default"].InnerPlotPosition.Width = float.Parse("95");
                    CHRT_GrayDeviation_L.ChartAreas["Default"].InnerPlotPosition.Height = float.Parse("86");

                    CHRT_GrayDeviation_L.ChartAreas["Default"].ShadowOffset = 0;
                    CHRT_GrayDeviation_L.BorderSkin.SkinStyle = BorderSkinStyle.None;
                    CHRT_GrayDeviation_L.ChartAreas["Default"].BorderWidth = 1;
                    CHRT_GrayDeviation_L.BorderSkin.BorderWidth = 0;

                    //========= Setting For Gray A Deviation Graph ==============//
                    CHRT_GrayDeviation_A.ChartAreas["Default"].Position.Auto = true;
                    CHRT_GrayDeviation_A.ChartAreas["Default"].Position.X = float.Parse("3");
                    CHRT_GrayDeviation_A.ChartAreas["Default"].Position.Y = float.Parse("5");
                    CHRT_GrayDeviation_A.ChartAreas["Default"].Position.Width = float.Parse("91");
                    CHRT_GrayDeviation_A.ChartAreas["Default"].Position.Height = float.Parse("86");

                    CHRT_GrayDeviation_A.ChartAreas["Default"].InnerPlotPosition.Auto = true;
                    CHRT_GrayDeviation_A.ChartAreas["Default"].InnerPlotPosition.X = float.Parse("5");
                    CHRT_GrayDeviation_A.ChartAreas["Default"].InnerPlotPosition.Y = float.Parse("5");
                    CHRT_GrayDeviation_A.ChartAreas["Default"].InnerPlotPosition.Width = float.Parse("95");
                    CHRT_GrayDeviation_A.ChartAreas["Default"].InnerPlotPosition.Height = float.Parse("86");

                    CHRT_GrayDeviation_A.ChartAreas["Default"].ShadowOffset = 0;
                    CHRT_GrayDeviation_A.BorderSkin.SkinStyle = BorderSkinStyle.None;
                    CHRT_GrayDeviation_A.ChartAreas["Default"].BorderWidth = 1;
                    CHRT_GrayDeviation_A.BorderSkin.BorderWidth = 0;

                    //========= Setting For Gray B Deviation Graph ==============//
                    CHRT_GrayDeviation_B.ChartAreas["Default"].Position.Auto = true;
                    CHRT_GrayDeviation_B.ChartAreas["Default"].Position.X = float.Parse("3");
                    CHRT_GrayDeviation_B.ChartAreas["Default"].Position.Y = float.Parse("5");
                    CHRT_GrayDeviation_B.ChartAreas["Default"].Position.Width = float.Parse("91");
                    CHRT_GrayDeviation_B.ChartAreas["Default"].Position.Height = float.Parse("86");

                    CHRT_GrayDeviation_B.ChartAreas["Default"].InnerPlotPosition.Auto = true;
                    CHRT_GrayDeviation_B.ChartAreas["Default"].InnerPlotPosition.X = float.Parse("5");
                    CHRT_GrayDeviation_B.ChartAreas["Default"].InnerPlotPosition.Y = float.Parse("5");
                    CHRT_GrayDeviation_B.ChartAreas["Default"].InnerPlotPosition.Width = float.Parse("95");
                    CHRT_GrayDeviation_B.ChartAreas["Default"].InnerPlotPosition.Height = float.Parse("86");

                    CHRT_GrayDeviation_B.ChartAreas["Default"].ShadowOffset = 0;
                    CHRT_GrayDeviation_B.BorderSkin.SkinStyle = BorderSkinStyle.None;
                    CHRT_GrayDeviation_B.ChartAreas["Default"].BorderWidth = 1;
                    CHRT_GrayDeviation_B.BorderSkin.BorderWidth = 0;
                    #endregion

                    #region Find And Set Maximum And Minimum To Chart 
                    CHRT_GrayDeviation_L.ChartAreas["Default"].AxisY.Minimum = -100.0;          //Math.Round(LMinValue);
                    CHRT_GrayDeviation_L.ChartAreas["Default"].AxisY.Maximum = 100.0;         //Math.Round(LMaxValue);
                    #endregion

                    #region Set Min Max For a Chart
                    CHRT_GrayDeviation_A.ChartAreas["Default"].AxisY.Minimum = -10.0; //Math.Round(aMinValue);
                    CHRT_GrayDeviation_A.ChartAreas["Default"].AxisY.Maximum = 10.0;    //Math.Round(aMaxValue);
               

                    CHRT_GrayDeviation_B.ChartAreas["Default"].AxisY.Minimum = -10.0;     //Math.Round(bMinValue);
                    CHRT_GrayDeviation_B.ChartAreas["Default"].AxisY.Maximum = 10.0;     //    Math.Round(bMaxValue);
                    #endregion
              

                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        //Plot Target Point in Chart L A B

                        AddTargetPointChart(i, CHRT_GrayDeviation_L.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), "0");// DT.Rows[i]["LAB_L1"].ToString());
                        AddTargetPointChart(i, CHRT_GrayDeviation_A.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), "0");// DT.Rows[i]["LAB_A1"].ToString());
                        AddTargetPointChart(i, CHRT_GrayDeviation_B.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), "0");// DT.Rows[i]["LAB_B1"].ToString());

                        AddPointChart(i, CHRT_GrayDeviation_L.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["CIELAB_L"].ToString(), Utilities.DecimalRound(Convert.ToDouble(DT.Rows[i]["CIELAB_L"].ToString())), Utilities.DecimalRound(Convert.ToDouble(DT.Rows[i]["CIELAB_a"].ToString())), Utilities.DecimalRound(Convert.ToDouble(DT.Rows[i]["CIELAB_b"].ToString())));
                        AddPointChart(i, CHRT_GrayDeviation_A.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["CIELAB_a"].ToString(), Utilities.DecimalRound(Convert.ToDouble(DT.Rows[i]["CIELAB_L"].ToString())), Utilities.DecimalRound(Convert.ToDouble(DT.Rows[i]["CIELAB_a"].ToString())), Utilities.DecimalRound(Convert.ToDouble(DT.Rows[i]["CIELAB_b"].ToString())));
                        AddPointChart(i, CHRT_GrayDeviation_B.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["CIELAB_b"].ToString(), Utilities.DecimalRound(Convert.ToDouble(DT.Rows[i]["CIELAB_L"].ToString())), Utilities.DecimalRound(Convert.ToDouble(DT.Rows[i]["CIELAB_a"].ToString())), Utilities.DecimalRound(Convert.ToDouble(DT.Rows[i]["CIELAB_b"].ToString())));
                    }

                    // === Save Chart Image after generating image with proper data === //
                    string LFileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(CHRT_GrayDeviation_L.ImageLocation) + "." + CHRT_GrayDeviation_L.ImageType;
                    CHRT_GrayDeviation_L.SaveImage(Server.MapPath(LFileName));
                    string AFileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(CHRT_GrayDeviation_A.ImageLocation) + "." + CHRT_GrayDeviation_A.ImageType;
                    CHRT_GrayDeviation_A.SaveImage(Server.MapPath(AFileName));
                    string BFileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(CHRT_GrayDeviation_B.ImageLocation) + "." + CHRT_GrayDeviation_B.ImageType;
                    CHRT_GrayDeviation_B.SaveImage(Server.MapPath(BFileName));
                }
            }
            catch (Exception Exc)
            {

                throw Exc;
            }

        }
        //Add Point In Chart for checking Paper Point
        private void AddTargetPointChart(int _Index, Series _objSeries, string _XValue, string _YValue)
        {
            try
            {
                _objSeries.ChartType = SeriesChartType.Spline;
                _objSeries.Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(_XValue)), Utilities.DecimalRound(Convert.ToDouble(_YValue)));
                _objSeries.Points[_Index].MarkerColor = Color.Green;
                _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
                _objSeries.Points[_Index].MarkerSize = 9;
                _objSeries.Points[_Index].BorderWidth = 2;
                _objSeries.Points[_Index].MarkerBorderColor = Color.Green;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //Add Point In Chart for checking Paper Point
        private void AddPointChart(int _Index, Series _objSeries, string _XValue, string _YValue, Double L, Double A, Double B)
        {
            try
            {
                //string strHTMLColor = Formulas.ColorCalcuations.LabtoRGB(L, A, B);
                string strHTMLColor = "#ffffff";
               
                _objSeries.ChartType = SeriesChartType.Spline;
                _objSeries.Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(_XValue)), Utilities.DecimalRound(Convert.ToDouble(_YValue)));
                _objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(strHTMLColor);
                _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
                _objSeries.Points[_Index].MarkerSize = 9;
                _objSeries.Points[_Index].BorderWidth = 2;
                _objSeries.Points[_Index].MarkerBorderColor = Color.Black;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

    #region IViewReportSingleMeasurement Members

    public int MeasurementId { get; set; }

        public DataSet DSReport { get; set; }

        public string From_ΔE { get; set; }

        #endregion

    }
}