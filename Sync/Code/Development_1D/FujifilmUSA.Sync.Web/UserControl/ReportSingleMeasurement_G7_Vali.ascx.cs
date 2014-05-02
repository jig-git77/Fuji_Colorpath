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
    public partial class ReportSingleMeasurement_G7_Vali : BaseControl, IViewReportSingleMeasurement
    {
        #region Variable Declarations
        private PresenterReportSingleMeasurement _presenterReportSingleMeasurement;
        public decimal DtELimit = 0;    

        #endregion

        #region Constructor
        public ReportSingleMeasurement_G7_Vali()
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
                btn_GeneratePDF.ImageUrl = "../Images/Validation Report_G7_preview.jpg";    //For Sync1D and Sync4D Validation G7 Projects

                if (IsSync1DType) 
                {
                    btn_GeneratePDF.ImageUrl = "../Images/Sync1D Report_preview_G7.jpg";    //For Sync1D G7 Projects
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

                BindSummaryDeltaEValueBarChart(DSReport.Tables[3], 0, 11); //Bind Chart of Summary Delta E Value Bar Chart

                FillGrayG7Chart(DSReport.Tables[4], DSReport.Tables[5], DSReport.Tables[6]); //Bind charts of G7

                FillGrayLABCharts(DSReport.Tables[7]);  //Bind Gary Balance L*a*b* charts

                #endregion
            }
            CreateReportPDF(DSReport); // Generates PDF Report.           
        }
        private void CreateReportPDF(DataSet Ds)
        {
            // === Set all parameters to generate pdf file === //              

            Common.ReportPDF objReportPDF = new Common.ReportPDF();

            objReportPDF.PDFPath = Server.MapPath("UploadFiles/Report_PDF/");
            objReportPDF.PDF_NAME = SessionManager.Instance.CurrentUser.UserId + "_ReportSingleMeasurement.PDF";
            objReportPDF.DS_Common = Ds;
            objReportPDF.From_ΔE = txt_dTE.Text;
            objReportPDF.DLPName = txt_DLPName.Text;
            objReportPDF.IsValidation = true;
            ValidationType = Convert.ToInt32(txt_Vali_Type.Text);

            if (ValidationType == 1)
            {
                objReportPDF.ReportName = "G7 Validation Report";               
            }
            if (IsSync1DType)
            {
                objReportPDF.ReportName = "G7 Alignment Curves Report";                 
            }

            objReportPDF.ReportType = (int)enumReportType.ReportSingleMeasurement_G7;
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
                    ChartResultSummary_Delta.Height = Unit.Pixel(30 * DT.Rows.Count);  //------Pass Chart height dynamically based on no. of Metric sums records                  

                    List<decimal> levels = new List<decimal>();  //-----------Get ΔElimit by getting maxΔE / maxΔE2000 plus add 2 more points
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        levels.Add(Convert.ToDecimal(DT.Rows[i]["MeasuredValue"].ToString()));
                    }
                    DtELimit = levels.Max() + 2;
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

                objSeries2.Points.AddXY((Double)0, Y3); //------Red marker image plot for Tolerance
                objSeries0.Points[Index].BackGradientStyle = GradientStyle.DiagonalRight;

                SeriesActual.Points[Index]["PointWidth"] = "0.8";

                if (Y1 == 0 && Y2 == 0)
                {
                    objSeries0.Points[Index].MarkerImage = "";
                   // objSeries2.Points[Index].MarkerImage = "";
                }
         
        }
        #endregion

        #region " ==== Summary Delta E Value Chart === "
        private void BindSummaryDeltaEValueBarChart(DataTable DT, int _StartIndex, int _EndIndex)
        {
            
                ChartResultSummary_DeltaEValue.BorderColor = Color.White;
                ChartResultSummary_DeltaEValue.BorderWidth = 0;
                ChartResultSummary_DeltaEValue.BackColor = Color.White;

                //========== Clear Points and Show Chart ==========//
                ChartResultSummary_DeltaEValue.Visible = true;
                ChartResultSummary_DeltaEValue.Series[0].Points.Clear();
                ChartResultSummary_DeltaEValue.Series[1].Points.Clear();
                ChartResultSummary_DeltaEValue.Series[2].Points.Clear();

                #region //========== Declarations ==========//
                // === Series0 to display Thresold value === //
                Series objSeries0 = (Series)ChartResultSummary_DeltaEValue.Series[0];
                objSeries0.XValueType = ChartValueType.Int32;
                objSeries0.YValueType = ChartValueType.String;
                objSeries0.IsVisibleInLegend = false;
                objSeries0.CustomProperties = "DrawingStyle=Cylinder";

                // === Series1 to display Actual value === //
                Series objSeries1 = (Series)ChartResultSummary_DeltaEValue.Series[1];
                objSeries1.XValueType = ChartValueType.Int32;
                objSeries1.YValueType = ChartValueType.String;           
                objSeries1.CustomProperties = "DrawingStyle=Cylinder";

                // === Series2 to display red marker on Thresold value === //
                Series objSeries2 = (Series)ChartResultSummary_DeltaEValue.Series[2];
                objSeries2.XValueType = ChartValueType.Int32;
                objSeries2.YValueType = ChartValueType.String;
                objSeries2.CustomProperties = "DrawingStyle=Cylinder";
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
                    {
                        List<decimal> levels = DT.AsEnumerable().Select(al => al.Field<decimal>("ΔE")).Distinct().ToList(); //-----------Get ΔElimit by getting maxΔE / maxΔE2000 plus add 2 more points
                        DtELimit = levels.Max() + 2;
                        DtELimit = (DtELimit > 4 ? DtELimit : DtELimit + 1);

                        BindDeltaEValueACTTHValue(objSeries0, objSeries1, objSeries2, DT.Rows[i], i, DtELimit);
                    }
                    else
                    {
                        BindDeltaEValueACTTHValue(objSeries0, objSeries1, objSeries2, null, i, DtELimit);
                    }
                }


                // === Save Chart Image after generating image with proper data === //
                string FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(ChartResultSummary_DeltaEValue.ImageLocation) + "." + ChartResultSummary_DeltaEValue.ImageType;
                ChartResultSummary_DeltaEValue.SaveImage(Server.MapPath(FileName));

                #endregion
            
        }

        private void BindDeltaEValueACTTHValue(Series objSeries0, Series objSeries1, Series objSeries2, DataRow DR, int Index, decimal DtELimit)
        {

                string strHTMLColor = "";
                Double Y1, Y2, Y3;
                Y1 = Y2 = Y3 = 0;

                if (DR != null)
                {
                    Y1 = (Convert.ToString(DtELimit) == "" ? 0 : Utilities.DecimalRound(Convert.ToDouble(DtELimit)));
                    Y2 = (Convert.ToString(DR["ΔE"]) == "" ? 0 : Utilities.DecimalRound(Convert.ToDouble(DR["ΔE"].ToString())));
                    Y3 = (Convert.ToString(DR["ΔELimit"]) == "" ? 0 : Utilities.DecimalRound(Convert.ToDouble(DR["ΔELimit"].ToString())));
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

                if (IsSync1DType || IsVerification) //-----For Sync1D projects or validation projects
                {
                    objSeries2.Points.AddXY((Double)0, Y3); //------Red marker image plot for Tolerance
                }

                SeriesActual.Points[Index].Color = System.Drawing.ColorTranslator.FromHtml(strHTMLColor);
                SeriesActual.Points[Index]["PointWidth"] = "0.8";

                if (Y1 == 0 && Y2 == 0)
                {
                    objSeries0.Points[Index].MarkerImage = "";
                }           
        }

        #endregion

        #region Fill G7 Charts
        private void FillGrayG7Chart(DataTable DT, DataTable DTBlackL, DataTable DTGrayL)
        {
            #region Fill Gray Ch / F Chart
            Chart_GrayF.Series["Series1"].Points.Clear();
            Chart_GrayF.Series["Series1"]["TooltipOffset"] = "True";
            Chart_GrayF.Series["Series1Point"].Points.Clear();
            Chart_GrayF.Series["Series2"].Points.Clear();
            Chart_GrayF.Series["Series2Point"].Points.Clear();

            if (DT != null && DT.Rows.Count > 0)
            {
                #region add Paper patch (CMYK = 0) at 0 index
                    DataRow dr1 = DT.NewRow();
                    dr1["Cmyk_Value"] = 0;
                    dr1["ΔValue"] = 0;
                    dr1["ΔLimit"] = DT.Rows[0]["ΔLimit"].ToString();
                    DT.Rows.InsertAt(dr1, 0);
                #endregion

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    AddPointInChart(i, Chart_GrayF.Series["Series1"], Chart_GrayF.Series["Series1Point"], Convert.ToInt64(DT.Rows[i]["Cmyk_Value"]).ToString(), DT.Rows[i]["ΔValue"].ToString(), Color.Black.Name.ToString(), true, "ΔCh*");
                    AddPointInChart(i, Chart_GrayF.Series["Series2"], Chart_GrayF.Series["Series2Point"], Convert.ToInt64(DT.Rows[i]["Cmyk_Value"]).ToString(), DT.Rows[i]["ΔLimit"].ToString(), Color.Red.Name.ToString(), false, "ΔCh*");
                }
            }
            // === Save Chart Image after generating image with proper data === //
            string FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(Chart_GrayF.ImageLocation) + "." + Chart_GrayF.ImageType;
            Chart_GrayF.SaveImage(Server.MapPath(FileName));

            #endregion

            #region Fill Black ΔL Chart
            Chart_BlackL.Series["Series1"].Points.Clear();
            Chart_BlackL.Series["Series1"]["TooltipOffset"] = "True";
            Chart_BlackL.Series["Series1Point"].Points.Clear();
            Chart_BlackL.Series["Series2"].Points.Clear();
            Chart_BlackL.Series["Series2Point"].Points.Clear();

            if (DTBlackL != null && DTBlackL.Rows.Count > 0)
            {
                #region add Paper patch (CMYK = 0) at 0 index
                    DataRow dr2 = DTBlackL.NewRow();
                    dr2["Cmyk_Value"] = 0;
                    dr2["ΔValue"] = 0;
                    dr2["ΔLimit"] = DTBlackL.Rows[0]["ΔLimit"].ToString();
                    DTBlackL.Rows.InsertAt(dr2, 0);
                #endregion

                for (int i = 0; i < DTBlackL.Rows.Count; i++)
                {
                    AddPointInChart(i, Chart_BlackL.Series["Series1"], Chart_BlackL.Series["Series1Point"], Convert.ToInt64(DTBlackL.Rows[i]["Cmyk_Value"]).ToString(), DTBlackL.Rows[i]["ΔValue"].ToString(), Color.Black.Name.ToString(), true, "ΔL*");
                    AddPointInChart(i, Chart_BlackL.Series["Series2"], Chart_BlackL.Series["Series2Point"], Convert.ToInt64(DTBlackL.Rows[i]["Cmyk_Value"]).ToString(), DTBlackL.Rows[i]["ΔLimit"].ToString(), Color.Red.Name.ToString(), false, "ΔL*");
                }
            }

            // === Save Chart Image after generating image with proper data === //
            FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(Chart_BlackL.ImageLocation) + "." + Chart_BlackL.ImageType;
            Chart_BlackL.SaveImage(Server.MapPath(FileName));

            #endregion

            #region Fill CMY / Gray ΔL Chart
            Chart_GrayL.Series["Series1"].Points.Clear();
            Chart_GrayL.Series["Series1"]["TooltipOffset"] = "True";
            Chart_GrayL.Series["Series1Point"].Points.Clear();
            Chart_GrayL.Series["Series2"].Points.Clear();
            Chart_GrayL.Series["Series2Point"].Points.Clear();

            if (DTGrayL != null && DTGrayL.Rows.Count > 0)
            {
                #region add Paper patch (CMYK = 0) at 0 index
                    DataRow dr3 = DTGrayL.NewRow();
                    dr3["Cmyk_Value"] = 0;
                    dr3["ΔValue"] = 0;
                    dr3["ΔLimit"] = DTGrayL.Rows[0]["ΔLimit"].ToString();
                    DTGrayL.Rows.InsertAt(dr3, 0);
                #endregion
                for (int i = 0; i < DTGrayL.Rows.Count; i++)
                {
                    AddPointInChart(i, Chart_GrayL.Series["Series1"], Chart_GrayL.Series["Series1Point"], Convert.ToInt64(DTGrayL.Rows[i]["Cmyk_Value"]).ToString(), DTGrayL.Rows[i]["ΔValue"].ToString(), Color.Black.Name.ToString(), true, "ΔL*");
                    AddPointInChart(i, Chart_GrayL.Series["Series2"], Chart_GrayL.Series["Series2Point"], Convert.ToInt64(DTGrayL.Rows[i]["Cmyk_Value"]).ToString(), DTGrayL.Rows[i]["ΔLimit"].ToString(), Color.Red.Name.ToString(), false, "ΔL*");
                }
            }

            // === Save Chart Image after generating image with proper data === //
            FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(Chart_GrayL.ImageLocation) + "." + Chart_GrayL.ImageType;
            Chart_GrayL.SaveImage(Server.MapPath(FileName));

            #endregion
        }

        private void AddPointInChart(int _Index, Series _objSeries, Series _objSeriesPoint, string _XValue, string _YValue, string ColorName, bool showCircle, string From_Delta)
        {
            _objSeries.ChartType = SeriesChartType.Spline;
            _objSeries.Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(_XValue)), Utilities.DecimalRound(Convert.ToDouble(_YValue)));
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.None;
            _objSeries.Points[_Index].BorderWidth = 1;


            _objSeriesPoint.ChartType = SeriesChartType.Point;
            _objSeriesPoint.Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(_XValue)), Utilities.DecimalRound(Convert.ToDouble(_YValue)));
            _objSeriesPoint.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
            _objSeriesPoint.Points[_Index].MarkerStyle = showCircle ? MarkerStyle.Circle : MarkerStyle.None;
            _objSeriesPoint.Points[_Index].MarkerSize = showCircle ? 5 : 0;
            _objSeriesPoint.Points[_Index].BorderWidth = 1;
            _objSeriesPoint.Points[_Index].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
           
        }
        #endregion

        #region Fill Gray L* a* b* Charts
        private void FillGrayLABCharts(DataTable DT)
        {
            chartL.Series["Series1"].Points.Clear();
            chartA.Series["Series1"].Points.Clear();
            chartB.Series["Series1"].Points.Clear();
            chartL.Series["Series2"].Points.Clear();
            chartA.Series["Series2"].Points.Clear();
            chartB.Series["Series2"].Points.Clear();

            if (DT != null && DT.Rows.Count > 0)
            {
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    AddPointLABChart(i, chartL.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["CIELAB_L"].ToString(), Color.Black.Name.ToString(), DT.Rows[i], true);
                    AddPointLABChart(i, chartA.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["CIELAB_a"].ToString(), Color.Black.Name.ToString(), DT.Rows[i], true);
                    AddPointLABChart(i, chartB.Series["Series1"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["CIELAB_b"].ToString(), Color.Black.Name.ToString(), DT.Rows[i], true);

                    if (DT.Rows[i]["TargetL"] != null)
                    {
                        AddPointLABChart(i, chartL.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["TargetL"].ToString(), Color.Green.Name.ToString(), DT.Rows[i], false);
                    }
                    AddPointLABChart(i, chartA.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["Targeta"].ToString(), Color.Green.Name.ToString(), DT.Rows[i], false);
                    AddPointLABChart(i, chartB.Series["Series2"], Convert.ToInt64(DT.Rows[i]["CMYK_C"]).ToString(), DT.Rows[i]["Targetb"].ToString(), Color.Green.Name.ToString(), DT.Rows[i], false);
                }
            }
            // === Save Chart Image after generating image with proper data === //
            string FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(chartL.ImageLocation) + "." + chartL.ImageType;
            chartL.SaveImage(Server.MapPath(FileName));
            FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(chartA.ImageLocation) + "." + chartA.ImageType;
            chartA.SaveImage(Server.MapPath(FileName));
            FileName = "UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_" + Path.GetFileName(chartB.ImageLocation) + "." + chartB.ImageType;
            chartB.SaveImage(Server.MapPath(FileName));
        }

        private void AddPointLABChart(int _Index, Series _objSeries, string _XValue, string _YValue, string ColorName, DataRow DR, bool isActual)
        {       
            _objSeries.ChartType = SeriesChartType.Spline;
            _objSeries.Points.AddXY(Utilities.DecimalRound(Convert.ToDouble(_XValue)), Utilities.DecimalRound(Convert.ToDouble(_YValue)));
            
            _objSeries.Points[_Index].MarkerColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
            _objSeries.Points[_Index].MarkerStyle = MarkerStyle.Circle;
            _objSeries.Points[_Index].MarkerSize = 5;
            _objSeries.Points[_Index].BorderWidth = 1;
            _objSeries.Points[_Index].MarkerBorderColor = System.Drawing.ColorTranslator.FromHtml(ColorName);
        }
        #endregion

        protected void btn_GeneratePDF_Click(object sender, ImageClickEventArgs e)
        {
            if (txt_MeasurementID.Text != null)
            {
                MeasurementId = Convert.ToInt32(txt_MeasurementID.Text);
                From_ΔE = txt_dTE.Text;
                ValidationType = Convert.ToInt32(txt_Vali_Type.Text);
                DSReport_G7_Vali = new DataSet();
                _presenterReportSingleMeasurement.GetReportDataOnMeasurement_G7_Vali();
                if (!IsErrorOccured)
                {
                    if (DSReport_G7_Vali != null && DSReport_G7_Vali.Tables.Count > 0)
                    {
                        CreateReport(DSReport_G7_Vali);
                    }
                }
            }
        }        

        #region IViewReportSingleMeasurement Members

        public int MeasurementId { get; set; }
        public int OptimizeRunId { get; set; }
        public DataSet DSReport { get; set; }

        public string From_ΔE { get; set; }

        public bool IsVerification
        {
            get
            {
                return (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);
            }
        }

        public DataSet DSReport_G7_Vali { get; set; }
        public int ValidationType { get; set; }
        
        public bool IsSync1DType
        {
            get
            {
                return (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D);
            }
        }

        public bool IsAverageOfMeasurements
        {
            get
            {
                return false;
            }
        }
        #endregion
       
    }
}