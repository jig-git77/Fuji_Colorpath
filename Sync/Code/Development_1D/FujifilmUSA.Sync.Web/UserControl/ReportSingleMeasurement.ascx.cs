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
        public decimal DtELimit = 0;

        #endregion

        #region Constructor
        public ReportSingleMeasurement()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterReportSingleMeasurement = new PresenterReportSingleMeasurement(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                btn_GeneratePDF.ImageUrl = "../Images/Sync DLP Report_preview.jpg";         //For Sync 4D Projects             
                if (IsVerification)
                {
                    btn_GeneratePDF.ImageUrl = "../Images/Sync1D Report_preview_Vali.jpg";   //For Sync1D and Sync4D Validation Projects
                }
                else if (IsSync1DType)
                {
                    btn_GeneratePDF.ImageUrl = "../Images/Sync1D Report_preview.jpg";       //For Sync1D Projects
                }
            }
        }
        #endregion

        #region Fill Charts
        private void CreateReport(DataSet DSReport)
        {
            // ===== Get Data from data base  ==================//             
            if (DSReport != null && DSReport.Tables.Count > 0)
            {
                #region " === Page : 1 Result Summary === "

                Bind_TargetVersusActual_CHART(DSReport.Tables[1]); // Bind Chart Of Target And Actual Value

                BindSummaryDeltaValueBarChart(DSReport.Tables[2]); // Bind Chart of Summary Delta Value Bar Chart

                #endregion
            }
            CreateReportPDF(DSReport); // Generates PDF Report.           
        }
        private void CreateReportPDF(DataSet Ds)
        {
            // === Set all parameters to generate pdf file === //              

            Common.ReportPDF objReportPDF = new Common.ReportPDF();
            objReportPDF.ReportType = (int)enumReportType.ReportSingleMeasurement_ISO;
            if (ValidationType == 0) //For Default mode in Sync1D ISO, G7 or 4D validation project as choice of MTP
            {
                objReportPDF.ReportType = (int)enumReportType.ReportSingleMeasurement_Default;
            }
            objReportPDF.PDFPath = Server.MapPath("UploadFiles/Report_PDF/");
            objReportPDF.PDF_NAME = SessionManager.Instance.CurrentUser.UserId + "_ReportSingleMeasurement.PDF";
            objReportPDF.DS_Common = Ds;
            objReportPDF.From_ΔE = txt_dTE.Text;
            objReportPDF.DLPName = txt_DLPName.Text;
            objReportPDF.IsValidation = IsVerification;

            if (IsVerification)
            {
                objReportPDF.ReportName = "Validation Report";                
            }
            else if (IsSync1DType)
            {
                objReportPDF.ReportName = "Alignment Curves Report";
            }
            else
            {
                objReportPDF.ReportName = "DLP Optimization Report";
                objReportPDF.ReportType = (int)enumReportType.ReportSingleMeasurement; //Only in 4D Optimization part
            }
            objReportPDF.MakePDF();

            // === After generate and save PDF file, Download it at a time === //
            byte[] bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/UploadFiles/Report_PDF/" + objReportPDF.PDF_NAME));
            Common.Utilities.DownloadFile(bytes, objReportPDF.PDF_NAME, ".pdf");
        }
        #endregion

        #region " === Spider Actual AB Plot === "
        private void Bind_TargetVersusActual_CHART(DataTable dtGamut)
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
        #endregion

        #region " ==== Summary Delta Value Chart === "
        private void BindSummaryDeltaValueBarChart(DataTable DT)
        {
          
                ChartResultSummary_Delta.BorderColor = Color.White;
                ChartResultSummary_Delta.BorderWidth = 0;
                ChartResultSummary_Delta.BackColor = Color.White;

                //========== Clear Points and Show Chart ==========//
                ChartResultSummary_Delta.Visible = true;
                ChartResultSummary_Delta.Series[0].Points.Clear();
                ChartResultSummary_Delta.Series[1].Points.Clear();
                ChartResultSummary_Delta.Series[2].Points.Clear();

                #region //========== Declarations ==========//
                // === Series0 to display Thresold value === //
                Series objSeries0 = (Series)ChartResultSummary_Delta.Series[0];
                objSeries0.XValueType = ChartValueType.Int32;
                objSeries0.YValueType = ChartValueType.String;
                objSeries0.CustomProperties = "DrawingStyle=Cylinder";

                // === Series1 to display Actual value === //
                Series objSeries1 = (Series)ChartResultSummary_Delta.Series[1];
                objSeries1.XValueType = ChartValueType.Int32;
                objSeries1.YValueType = ChartValueType.String;             
                objSeries1.CustomProperties = "DrawingStyle=Cylinder";

                // === Series2 to display red marker on Thresold value === //
                Series objSeries2 = (Series)ChartResultSummary_Delta.Series[2];
                objSeries2.XValueType = ChartValueType.Int32;
                objSeries2.YValueType = ChartValueType.String;
                objSeries2.CustomProperties = "DrawingStyle=Cylinder";           
                            
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
                    if (!IsVerification && !IsSync1DType) //--For Sync4D Optimization only
                    {
                        #region //-----for Sync4D

                        ChartResultSummary_Delta.Height = Unit.Pixel(200);

                        List<decimal> levels = new List<decimal>();  //-----------Get ΔElimit by getting maxΔE / maxΔE2000 plus add 2 more points
                        levels.Add(Convert.ToDecimal(DT.Rows[0]["Avg_ΔE"].ToString()));
                        levels.Add(Convert.ToDecimal(DT.Rows[0]["Max_ΔE"].ToString()));
                        levels.Add(Convert.ToDecimal(DT.Rows[0]["PriΔE"].ToString()));
                        levels.Add(Convert.ToDecimal(DT.Rows[0]["PriΔH"].ToString()));
                        levels.Add(Convert.ToDecimal(DT.Rows[0]["GrayΔE"].ToString()));
                        levels.Add(Convert.ToDecimal(DT.Rows[0]["NinetyFiveΔE"].ToString()));
                        DtELimit = levels.Max() + 2;
                        DtELimit = (DtELimit > 4 ? DtELimit : DtELimit + 1);

                        //---------Normal Binding Graph-------//
                        if (Convert.IsDBNull(DT.Rows[0]["Avg_ΔE"]) || Convert.IsDBNull(DT.Rows[0]["ΔELimit"]))
                            BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, 0, 0, 0, 0);
                        else
                            BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, Utilities.DecimalRound(Convert.ToDouble(DT.Rows[0]["Avg_ΔE"].ToString())), Utilities.DecimalRound(Convert.ToDouble(DtELimit)), 0, 0);

                        if (Convert.IsDBNull(DT.Rows[0]["Max_ΔE"]) || Convert.IsDBNull(DT.Rows[0]["ΔELimit"]))
                            BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, 0, 0, 0, 1);
                        else
                            BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, Utilities.DecimalRound(Convert.ToDouble(DT.Rows[0]["Max_ΔE"].ToString())), Utilities.DecimalRound(Convert.ToDouble(DtELimit)), 0, 1);

                        if (Convert.IsDBNull(DT.Rows[0]["PriΔE"]) || Convert.IsDBNull(DT.Rows[0]["ΔELimit"]))
                            BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, 0, 0, 0, 2);
                        else
                            BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, Utilities.DecimalRound(Convert.ToDouble(DT.Rows[0]["PriΔE"].ToString())), Utilities.DecimalRound(Convert.ToDouble(DtELimit)), 0, 2);

                        if (Convert.IsDBNull(DT.Rows[0]["PriΔH"]) || Convert.IsDBNull(DT.Rows[0]["ΔELimit"]))
                            BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, 0, 0, 0, 3);
                        else
                            BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, Utilities.DecimalRound(Convert.ToDouble(DT.Rows[0]["PriΔH"].ToString())), Utilities.DecimalRound(Convert.ToDouble(DtELimit)), 0, 3);

                        if (Convert.IsDBNull(DT.Rows[0]["GrayΔE"]) || Convert.IsDBNull(DT.Rows[0]["ΔELimit"]))
                            BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, 0, 0, 0, 4);
                        else
                            BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, Utilities.DecimalRound(Convert.ToDouble(DT.Rows[0]["GrayΔE"].ToString())), Utilities.DecimalRound(Convert.ToDouble(DtELimit)), 0, 4);

                        if (Convert.IsDBNull(DT.Rows[0]["NinetyFiveΔE"]) || Convert.IsDBNull(DT.Rows[0]["ΔELimit"]))
                            BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, 0, 0, 0, 5);
                        else
                            BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, Utilities.DecimalRound(Convert.ToDouble(DT.Rows[0]["NinetyFiveΔE"].ToString())), Utilities.DecimalRound(Convert.ToDouble(DtELimit)),0, 5);

                        // === Save Chart Image after generating image with proper data === //
                        string FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(ChartResultSummary_Delta.ImageLocation) + "." + ChartResultSummary_Delta.ImageType;
                        ChartResultSummary_Delta.SaveImage(Server.MapPath(FileName));
                        #endregion
                    }
                    else   //---For Sync 1D ISO or 4D ISO validation
                    {
                        ChartResultSummary_Delta.Height = Unit.Pixel(30 * (DT.Rows.Count + 1));   //------Pass Chart height dynamically based on no. of Metric sums records  
                       
                        List<decimal> levels = new List<decimal>();  //-----------Get ΔElimit by getting maxΔE / maxΔE2000 plus add 2 more points
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            levels.Add(Convert.ToDecimal(DT.Rows[i]["MeasuredValue"].ToString()));
                        }
                        DtELimit = levels.Max() + 1;
                        DtELimit = (DtELimit > 4 ? DtELimit : DtELimit + 1);

                        //---------Normal Binding Graph-------//
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            BindDeltaValueACTTHValue(objSeries0, objSeries1, objSeries2, Utilities.DecimalRound(Convert.ToDouble(DT.Rows[i]["MeasuredValue"].ToString())), Utilities.DecimalRound(Convert.ToDouble(DtELimit)), Utilities.DecimalRound(Convert.ToDouble(DT.Rows[i]["TargetValue"].ToString())), 0);
                        }

                        // === Save Chart Image after generating image with proper data === //
                        string FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(ChartResultSummary_Delta.ImageLocation) + "." + ChartResultSummary_Delta.ImageType;
                        ChartResultSummary_Delta.SaveImage(Server.MapPath(FileName));
                    }
                }
                #endregion
          
        }
        private void BindDeltaValueACTTHValue(Series objSeries0, Series objSeries1, Series objSeries2, double ACT, double TH, double Tol, int Index)
        {
            Double Y1, Y2, Y3;
            Y1 = Y2 = Y3 = 0;

            Y1 = (Convert.ToString(TH) == "" ? 0 : Convert.ToDouble(TH.ToString()));
            Y2 = (Convert.ToString(ACT) == "" ? 0 : Convert.ToDouble(ACT.ToString()));
            Y3 = (Convert.ToString(Tol) == "" ? 0 : Convert.ToDouble(Tol.ToString()));

                // === When Actual value is not available then no series will display and no marker will display
                if (Y2 == 0)
                    Y1 = Y2;


                Series SeriesActual = objSeries1;
                objSeries0.Points.AddXY((Double)0, Y1);
                objSeries0.Points[Index]["PointWidth"] = "0.8";
                objSeries1.Points.AddXY((Double)0, Y2);

                objSeries0.Points[Index].BackGradientStyle = GradientStyle.DiagonalRight;
                if (IsSync1DType || IsVerification) //-----For Sync1D projects or validation projects (1D / 4D)
                {
                    objSeries2.Points.AddXY((Double)0, Y3); //------Red marker image plot for Tolerance
                }

                SeriesActual.Points[Index]["PointWidth"] = "0.8";

                if (Y1 == 0 && Y2 == 0)
                {
                    objSeries0.Points[Index].MarkerImage = "";
                }
         
        }
        #endregion

     
        protected void btn_GeneratePDF_Click(object sender, ImageClickEventArgs e)
        {
            if (txt_MeasurementID.Text != null)
            {
                MeasurementId = Convert.ToInt32(txt_MeasurementID.Text);
               
                From_ΔE = txt_dTE.Text;
                ValidationType = Convert.ToInt32(txt_Vali_Type.Text);
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

        #region IViewReportSingleMeasurement Members

        public int MeasurementId { get; set; }
        public DataSet DSReport { get; set; }

        public string From_ΔE { get; set; }

        public bool IsVerification
        {
            get
            {
                return (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);
            }
        }

        public bool IsSync1DType
        {
            get
            {
                return (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D);
            }
        }

        public DataSet DSReport_G7_Vali { get; set; }
        public int ValidationType { get; set; }

        public bool IsAverageOfMeasurements
        {
            get
            {
                return Convert.ToBoolean(txt_IsAvgMeasurement.Text);
            }
        }

        #endregion
    }
}