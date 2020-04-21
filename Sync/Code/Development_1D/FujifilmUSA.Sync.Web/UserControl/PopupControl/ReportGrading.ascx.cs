using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using System.Data;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl.PopupControl
{
    public partial class ReportGrading : BaseControl, IViewReportGrading
    {
        #region Variable Declarations
        private PresenterReportGrading _presenterReportGrading;
        public decimal DtELimit = 0;

        #endregion

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterReportGrading = new PresenterReportGrading(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #endregion

        //protected void lbtnGenerateReport_Click(object sender, EventArgs e)
        //{
        //    FillEmptyBoxes();

        //    if (txt_MeasurementID.Text != null)
        //    {
        //        MeasurementId = Convert.ToInt32(txt_MeasurementID.Text);

        //        From_ΔE = txt_dTE.Text;
        //        IsAverageOfMeasurements = chkAverageOfMeasurements.Checked;
        //        ProjectId = SessionManager.Instance.CurrentProject.ProjectId;

        //        DSReport = new DataSet();
        //        _presenterReportGrading.GetMeasurementDataForScoring();
        //        if (!IsErrorOccured)
        //        {
        //            if (DSReport != null && DSReport.Tables.Count > 0)
        //            {
        //                RevisedDataSet(DSReport); //-----Call the function to get two separate DataSets for Avg Scoring and Individual Scoring Measurements list
        //                CreateReportPDF(DSReport); //----Call the function to generate report
        //            }
        //        }
        //    }
        //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "radalert", "<script type=text/javascript> CloseGradingWindow(); </script>");  
        //}

        //private void RevisedDataSet(DataSet DSReport)
        //{
        //    decimal CyanAvgScore = 0;
        //    decimal MagentaAvgScore = 0;
        //    decimal YellowAvgScore = 0;
        //    decimal BlackAvgScore = 0;
        //    decimal RedAvgScore = 0;
        //    decimal GreenAvgScore = 0;
        //    decimal BlueAvgScore = 0;
        //    decimal PaperAvgScore = 0;
        //    decimal CMY25AvgScore = 0;
        //    decimal CMY50AvgScore = 0;
        //    decimal CMY75AvgScore = 0;
        //    DSReportRevised = new DataSet();

        //    DataTable dt = new DataTable("MyTable");
        //    dt.Columns.Add(new DataColumn("MeasurementID", typeof(int)));
        //    dt.Columns.Add(new DataColumn("Name", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Date", typeof(string)));
        //    dt.Columns.Add(new DataColumn("Cyan", typeof(decimal)));           
        //    dt.Columns.Add(new DataColumn("Magenta", typeof(decimal)));          
        //    dt.Columns.Add(new DataColumn("Yellow", typeof(decimal)));         
        //    dt.Columns.Add(new DataColumn("Black", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("CMYKPri", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("Red", typeof(decimal)));          
        //    dt.Columns.Add(new DataColumn("Green", typeof(decimal)));           
        //    dt.Columns.Add(new DataColumn("Blue", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("RGBSec", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("CMY25", typeof(decimal)));          
        //    dt.Columns.Add(new DataColumn("CMY50", typeof(decimal)));          
        //    dt.Columns.Add(new DataColumn("CMY75", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("GrayBalance", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("Paper", typeof(decimal)));
        //    dt.Columns.Add(new DataColumn("Overall", typeof(decimal)));

        //    if (DSReport != null && DSReport.Tables.Count > 0)
        //    {
        //        for (int i = 0; i < DSReport.Tables[4].Rows.Count; i++)
        //        {
        //            DataRow dr = dt.NewRow();
        //            dr["MeasurementID"] = DSReport.Tables[4].Rows[i]["MeasurementID"].ToString();
        //            dr["Name"] = DSReport.Tables[4].Rows[i]["MeasurementName"].ToString();
        //            dr["Date"] = DSReport.Tables[4].Rows[i]["MeasurementDate"].ToString();
        //            dr["Cyan"] = CalculateGrading(DSReport.Tables[4].Rows[i]["Cyan"].ToString(), "Cyan");
        //            dr["Magenta"] = CalculateGrading(DSReport.Tables[4].Rows[i]["Magenta"].ToString(), "Magenta");
        //            dr["Yellow"] = CalculateGrading(DSReport.Tables[4].Rows[i]["Yellow"].ToString(), "Yellow");
        //            dr["Black"] = CalculateGrading(DSReport.Tables[4].Rows[i]["Black"].ToString(), "Black");
        //            dr["CMYKPri"] = (CalculateGrading(DSReport.Tables[4].Rows[i]["Cyan"].ToString(), "Cyan") + CalculateGrading(DSReport.Tables[4].Rows[i]["Magenta"].ToString(), "Magenta") + CalculateGrading(DSReport.Tables[4].Rows[i]["Yellow"].ToString(), "Yellow") + CalculateGrading(DSReport.Tables[4].Rows[i]["Black"].ToString(), "Black")) / 4;
                    
        //            dr["Red"] = CalculateGrading(DSReport.Tables[4].Rows[i]["Red"].ToString(), "Red");
        //            dr["Green"] = CalculateGrading(DSReport.Tables[4].Rows[i]["Green"].ToString(), "Green");
        //            dr["Blue"] = CalculateGrading(DSReport.Tables[4].Rows[i]["Blue"].ToString(), "Blue");
        //            dr["RGBSec"] = (CalculateGrading(DSReport.Tables[4].Rows[i]["Red"].ToString(), "Red") + CalculateGrading(DSReport.Tables[4].Rows[i]["Green"].ToString(), "Green") + CalculateGrading(DSReport.Tables[4].Rows[i]["Blue"].ToString(), "Blue")) / 3;

        //            dr["CMY25"] = CalculateGrading(DSReport.Tables[4].Rows[i]["CMY25"].ToString(), "CMY25");
        //            dr["CMY50"] = CalculateGrading(DSReport.Tables[4].Rows[i]["CMY50"].ToString(), "CMY50");
        //            dr["CMY75"] = CalculateGrading(DSReport.Tables[4].Rows[i]["CMY75"].ToString(), "CMY75");
        //            dr["GrayBalance"] = (CalculateGrading(DSReport.Tables[4].Rows[i]["CMY25"].ToString(), "CMY25") + CalculateGrading(DSReport.Tables[4].Rows[i]["CMY50"].ToString(), "CMY50") + CalculateGrading(DSReport.Tables[4].Rows[i]["CMY75"].ToString(), "CMY75")) / 3;

        //            dr["Paper"] = CalculateGrading(DSReport.Tables[4].Rows[i]["Paper"].ToString(), "Paper");
        //            dr["Overall"] = (Convert.ToDecimal(dr["CMYKPri"]) + Convert.ToDecimal(dr["RGBSec"]) + Convert.ToDecimal(dr["GrayBalance"]) + Convert.ToDecimal(dr["Paper"])) / 4;
        //            dt.Rows.Add(dr);
        //        }
        //    }
        //    DSReportRevised.Tables.Add(dt);

        //    DSReportTotalAvg = new DataSet();
        //    if (DSReportRevised != null && DSReportRevised.Tables.Count > 0)
        //    {
        //        DataTable dtAvg = new DataTable("MyTableAvg");

        //        dtAvg.Columns.Add(new DataColumn("Color", typeof(string)));
        //        dtAvg.Columns.Add(new DataColumn("AvgScore", typeof(decimal)));

        //        for (int i = 0; i < DSReportRevised.Tables[0].Rows.Count; i++)
        //        {
        //            CyanAvgScore = CyanAvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["Cyan"]);
        //            MagentaAvgScore = MagentaAvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["Magenta"]);
        //            YellowAvgScore = YellowAvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["Yellow"]);
        //            BlackAvgScore = BlackAvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["Black"]);

        //            RedAvgScore = RedAvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["Red"]);
        //            GreenAvgScore = GreenAvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["Green"]);
        //            BlueAvgScore = BlueAvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["Blue"]);

        //            CMY25AvgScore = CMY25AvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["CMY25"]);
        //            CMY50AvgScore = CMY50AvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["CMY50"]);
        //            CMY75AvgScore = CMY75AvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["CMY75"]);

        //            PaperAvgScore = PaperAvgScore + Convert.ToDecimal(DSReportRevised.Tables[0].Rows[i]["Paper"]);
        //        }

        //        DataRow dr1 = dtAvg.NewRow();
        //        dr1["Color"] = "Cyan";
        //        dr1["AvgScore"] = Math.Round((CyanAvgScore / DSReportRevised.Tables[0].Rows.Count),2);
        //        dtAvg.Rows.Add(dr1);

        //        DataRow dr2 = dtAvg.NewRow();
        //        dr2["Color"] = "Magenta";
        //        dr2["AvgScore"] = Math.Round((MagentaAvgScore / DSReportRevised.Tables[0].Rows.Count),2);
        //        dtAvg.Rows.Add(dr2);

        //        DataRow dr3 = dtAvg.NewRow();
        //        dr3["Color"] = "Yellow";
        //        dr3["AvgScore"] = Math.Round((YellowAvgScore / DSReportRevised.Tables[0].Rows.Count),2);
        //        dtAvg.Rows.Add(dr3);

        //        DataRow dr4 = dtAvg.NewRow();
        //        dr4["Color"] = "Black";
        //        dr4["AvgScore"] = Math.Round((BlackAvgScore / DSReportRevised.Tables[0].Rows.Count),2);
        //        dtAvg.Rows.Add(dr4);

        //        DataRow dr5 = dtAvg.NewRow();
        //        dr5["Color"] = "Red";
        //        dr5["AvgScore"] = Math.Round((RedAvgScore / DSReportRevised.Tables[0].Rows.Count),2);
        //        dtAvg.Rows.Add(dr5);

        //        DataRow dr6 = dtAvg.NewRow();
        //        dr6["Color"] = "Green";
        //        dr6["AvgScore"] = Math.Round((GreenAvgScore / DSReportRevised.Tables[0].Rows.Count),2);
        //        dtAvg.Rows.Add(dr6);

        //        DataRow dr7 = dtAvg.NewRow();
        //        dr7["Color"] = "Blue";
        //        dr7["AvgScore"] = Math.Round((BlueAvgScore / DSReportRevised.Tables[0].Rows.Count), 2);
        //        dtAvg.Rows.Add(dr7);

        //        DataRow dr8 = dtAvg.NewRow();
        //        dr8["Color"] = "CMY25";
        //        dr8["AvgScore"] = Math.Round((CMY25AvgScore / DSReportRevised.Tables[0].Rows.Count), 2);
        //        dtAvg.Rows.Add(dr8);

        //        DataRow dr9 = dtAvg.NewRow();
        //        dr9["Color"] = "CMY50";
        //        dr9["AvgScore"] = Math.Round((CMY50AvgScore / DSReportRevised.Tables[0].Rows.Count), 2);
        //        dtAvg.Rows.Add(dr9);

        //        DataRow dr10 = dtAvg.NewRow();
        //        dr10["Color"] = "CMY75";
        //        dr10["AvgScore"] = Math.Round((CMY75AvgScore / DSReportRevised.Tables[0].Rows.Count), 2);
        //        dtAvg.Rows.Add(dr10);

        //        DataRow dr11 = dtAvg.NewRow();
        //        dr11["Color"] = "Paper";
        //        dr11["AvgScore"] = Math.Round((PaperAvgScore / DSReportRevised.Tables[0].Rows.Count), 2);
        //        dtAvg.Rows.Add(dr11);

        //        DSReportTotalAvg.Tables.Add(dtAvg);
        //    }
        //}

        //private void FillEmptyBoxes()
        //{
        //    txtCMYKTol1.Text = (txtCMYKTol1.Text == "") ? "0" : txtCMYKTol1.Text;
        //    txtCMYKSco1.Text = (txtCMYKSco1.Text == "") ? "0" : txtCMYKSco1.Text;
        //    txtCMYKTol2.Text = (txtCMYKTol2.Text == "") ? "0" : txtCMYKTol2.Text;
        //    txtCMYKSco2.Text = (txtCMYKSco2.Text == "") ? "0" : txtCMYKSco2.Text;
        //    txtCMYKTol3.Text = (txtCMYKTol3.Text == "") ? "0" : txtCMYKTol3.Text;
        //    txtCMYKSco3.Text = (txtCMYKSco3.Text == "") ? "0" : txtCMYKSco3.Text;

        //    txtRGBTol1.Text = (txtRGBTol1.Text == "") ? "0" : txtRGBTol1.Text;
        //    txtRGBSco1.Text = (txtRGBSco1.Text == "") ? "0" : txtRGBSco1.Text;
        //    txtRGBTol2.Text = (txtRGBTol2.Text == "") ? "0" : txtRGBTol2.Text;
        //    txtRGBSco2.Text = (txtRGBSco2.Text == "") ? "0" : txtRGBSco2.Text;
        //    txtRGBTol3.Text = (txtRGBTol3.Text == "") ? "0" : txtRGBTol3.Text;
        //    txtRGBSco3.Text = (txtRGBSco3.Text == "") ? "0" : txtRGBSco3.Text;

        //    txtCMYTol1.Text = (txtCMYTol1.Text == "") ? "0" : txtCMYTol1.Text;
        //    txtCMYSco1.Text = (txtCMYSco1.Text == "") ? "0" : txtCMYSco1.Text;
        //    txtCMYTol2.Text = (txtCMYTol2.Text == "") ? "0" : txtCMYTol2.Text;
        //    txtCMYSco2.Text = (txtCMYSco2.Text == "") ? "0" : txtCMYSco2.Text;
        //    txtCMYTol3.Text = (txtCMYTol3.Text == "") ? "0" : txtCMYTol3.Text;
        //    txtCMYSco3.Text = (txtCMYSco3.Text == "") ? "0" : txtCMYSco3.Text;

        //    txtPaperTol1.Text = (txtPaperTol1.Text == "") ? "0" : txtPaperTol1.Text;
        //    txtPaperSco1.Text = (txtPaperSco1.Text == "") ? "0" : txtPaperSco1.Text;
        //    txtPaperTol2.Text = (txtPaperTol2.Text == "") ? "0" : txtPaperTol2.Text;
        //    txtPaperSco2.Text = (txtPaperSco2.Text == "") ? "0" : txtPaperSco2.Text;
        //    txtPaperTol3.Text = (txtPaperTol3.Text == "") ? "0" : txtPaperTol3.Text;
        //    txtPaperSco3.Text = (txtPaperSco3.Text == "") ? "0" : txtPaperSco3.Text;
        //}

        //decimal ΔEGrade = 0;

        //private decimal CalculateGrading(string Orig_ΔE, string OriginFrom)
        //{
        //    decimal OriginalValue = 0;
        //    OriginalValue = Convert.ToDecimal(Orig_ΔE);
        //    string CMYKSoc_C, CMYKMaxSoc_C, CMYKIndPer_C = "0";
        //    string CMYKSoc_M, CMYKMaxSoc_M, CMYKIndPer_M = "0";
        //    string CMYKSoc_Y, CMYKMaxSoc_Y, CMYKIndPer_Y = "0";
        //    string CMYKSoc_K, CMYKMaxSoc_K, CMYKIndPer_K = "0";

        //    string RGBSoc_R, RGBMaxSoc_R, RGBIndPer_R = "0";
        //    string RGBSoc_G, RGBMaxSoc_G, RGBIndPer_G = "0";
        //    string RGBSoc_B, RGBMaxSoc_B, RGBIndPer_B = "0";

        //    string CMYSoc_25, CMYMaxSoc_25, CMYIndPer_25 = "0";
        //    string CMYSoc_50, CMYMaxSoc_50, CMYIndPer_50 = "0";
        //    string CMYSoc_75, CMYMaxSoc_75, CMYIndPer_75 = "0";

        //    string PaperSoc, PaperMaxSoc, PaperIndPer = "0";

        //        lblCMYKMaxSoc.Text = (txtCMYKSco1.Text == "") ? "0" : (Convert.ToDecimal(txtCMYKSco1.Text) * 4).ToString();
        //        lblRGBMaxSoc.Text = (txtRGBSco1.Text == "") ? "0" : (Convert.ToDecimal(txtRGBSco1.Text) * 3).ToString();
        //        lblCMYMaxSoc.Text = (txtCMYSco1.Text == "") ? "0" : (Convert.ToDecimal(txtCMYSco1.Text) * 3).ToString();
        //        lblPaperMaxSoc.Text = (txtPaperSco1.Text == "") ? "0" : txtPaperSco1.Text;

        //        if (OriginFrom == "Cyan")
        //        {
        //            #region Cyan calculation
        //            CMYKMaxSoc_C = (txtCMYKSco1.Text == "") ? "0" : txtCMYKSco1.Text;

        //            if (OriginalValue <= Convert.ToDecimal(txtCMYKTol1.Text))
        //            {
        //                CMYKSoc_C = txtCMYKSco1.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtCMYKTol1.Text) + Convert.ToDecimal(txtCMYKTol2.Text)))
        //            {
        //                CMYKSoc_C = txtCMYKSco2.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtCMYKTol1.Text) + Convert.ToDecimal(txtCMYKTol3.Text)))
        //            {
        //                CMYKSoc_C = txtCMYKSco3.Text;
        //            }
        //            else
        //            {
        //                CMYKSoc_C = "0";
        //            }

        //            if (CMYKMaxSoc_C != "0")
        //            {
        //                CMYKIndPer_C = Math.Round(((Convert.ToDecimal(CMYKSoc_C) / Convert.ToDecimal(CMYKMaxSoc_C)) * 100), 2).ToString();
        //            }

        //            ΔEGrade = Convert.ToDecimal(CMYKIndPer_C);
        //            #endregion
        //        }
        //        else if (OriginFrom == "Magenta")
        //        {
        //            #region Magenta calculation
        //            CMYKMaxSoc_M = (txtCMYKSco1.Text == "") ? "0" : txtCMYKSco1.Text;

        //            if (OriginalValue <= Convert.ToDecimal(txtCMYKTol1.Text))
        //            {
        //                CMYKSoc_M = txtCMYKSco1.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtCMYKTol1.Text) + Convert.ToDecimal(txtCMYKTol2.Text)))
        //            {
        //                CMYKSoc_M = txtCMYKSco2.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtCMYKTol1.Text) + Convert.ToDecimal(txtCMYKTol3.Text)))
        //            {
        //                CMYKSoc_M = txtCMYKSco3.Text;
        //            }
        //            else
        //            {
        //                CMYKSoc_M = "0";
        //            }

        //            if (CMYKMaxSoc_M != "0")
        //            {
        //                CMYKIndPer_M = Math.Round(((Convert.ToDecimal(CMYKSoc_M) / Convert.ToDecimal(CMYKMaxSoc_M)) * 100), 2).ToString();
        //            }
        //            ΔEGrade = Convert.ToDecimal(CMYKIndPer_M);
        //            #endregion
        //        }
        //        else if (OriginFrom == "Yellow")
        //        {
        //            #region Yellow calculation
        //            CMYKMaxSoc_Y = (txtCMYKSco1.Text == "") ? "0" : txtCMYKSco1.Text;

        //            if (OriginalValue <= Convert.ToDecimal(txtCMYKTol1.Text))
        //            {
        //                CMYKSoc_Y = txtCMYKSco1.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtCMYKTol1.Text) + Convert.ToDecimal(txtCMYKTol2.Text)))
        //            {
        //                CMYKSoc_Y = txtCMYKSco2.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtCMYKTol1.Text) + Convert.ToDecimal(txtCMYKTol3.Text)))
        //            {
        //                CMYKSoc_Y = txtCMYKSco3.Text;
        //            }
        //            else
        //            {
        //                CMYKSoc_Y = "0";
        //            }

        //            if (CMYKMaxSoc_Y != "0")
        //            {
        //                CMYKIndPer_Y = Math.Round(((Convert.ToDecimal(CMYKSoc_Y) / Convert.ToDecimal(CMYKMaxSoc_Y)) * 100), 2).ToString();
        //            }
        //            ΔEGrade = Convert.ToDecimal(CMYKIndPer_Y);
        //            #endregion
        //        }
        //        else if (OriginFrom == "Black")
        //        {
        //            #region Black calculation
        //            CMYKMaxSoc_K = (txtCMYKSco1.Text == "") ? "0" : txtCMYKSco1.Text;

        //            if (OriginalValue <= Convert.ToDecimal(txtCMYKTol1.Text))
        //            {
        //                CMYKSoc_K = txtCMYKSco1.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtCMYKTol1.Text) + Convert.ToDecimal(txtCMYKTol2.Text)))
        //            {
        //                CMYKSoc_K = txtCMYKSco2.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtCMYKTol1.Text) + Convert.ToDecimal(txtCMYKTol3.Text)))
        //            {
        //                CMYKSoc_K = txtCMYKSco3.Text;
        //            }
        //            else
        //            {
        //                CMYKSoc_K = "0";
        //            }

        //            if (CMYKMaxSoc_K != "0")
        //            {
        //                CMYKIndPer_K = Math.Round(((Convert.ToDecimal(CMYKSoc_K) / Convert.ToDecimal(CMYKMaxSoc_K)) * 100), 2).ToString();
        //            }
        //            ΔEGrade = Convert.ToDecimal(CMYKIndPer_K);
        //            #endregion
        //        }
        //        else if (OriginFrom == "Red")
        //        {
        //            #region Red calculation
        //            RGBMaxSoc_R = (txtRGBSco1.Text == "") ? "0" : txtRGBSco1.Text;

        //            if (OriginalValue <= Convert.ToDecimal(txtRGBTol1.Text))
        //            {
        //                RGBSoc_R = txtRGBSco1.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtRGBTol1.Text) + Convert.ToDecimal(txtRGBTol2.Text)))
        //            {
        //                RGBSoc_R = txtRGBSco2.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtRGBTol1.Text) + Convert.ToDecimal(txtRGBTol3.Text)))
        //            {
        //                RGBSoc_R = txtRGBSco3.Text;
        //            }
        //            else
        //            {
        //                RGBSoc_R = "0";
        //            }

        //            if (RGBMaxSoc_R != "0")
        //            {
        //                RGBIndPer_R = Math.Round(((Convert.ToDecimal(RGBSoc_R) / Convert.ToDecimal(RGBMaxSoc_R)) * 100), 2).ToString();
        //            }
        //            ΔEGrade = Convert.ToDecimal(RGBIndPer_R);
        //            #endregion
        //        }
        //        else if (OriginFrom == "Green")
        //        {
        //            #region Green calculation
        //            RGBMaxSoc_G = (txtRGBSco1.Text == "") ? "0" : txtRGBSco1.Text;

        //            if (OriginalValue <= Convert.ToDecimal(txtRGBTol1.Text))
        //            {
        //                RGBSoc_G = txtRGBSco1.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtRGBTol1.Text) + Convert.ToDecimal(txtRGBTol2.Text)))
        //            {
        //                RGBSoc_G = txtRGBSco2.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtRGBTol1.Text) + Convert.ToDecimal(txtRGBTol3.Text)))
        //            {
        //                RGBSoc_G = txtRGBSco3.Text;
        //            }
        //            else
        //            {
        //                RGBSoc_G = "0";
        //            }

        //            if (RGBMaxSoc_G != "0")
        //            {
        //                RGBIndPer_G = Math.Round(((Convert.ToDecimal(RGBSoc_G) / Convert.ToDecimal(RGBMaxSoc_G)) * 100), 2).ToString();
        //            }
        //            ΔEGrade = Convert.ToDecimal(RGBIndPer_G);
        //            #endregion
        //        }
        //        else if (OriginFrom == "Blue")
        //        {
        //            #region Blue calculation
        //            RGBMaxSoc_B = (txtRGBSco1.Text == "") ? "0" : txtRGBSco1.Text;

        //            if (OriginalValue <= Convert.ToDecimal(txtRGBTol1.Text))
        //            {
        //                RGBSoc_B = txtRGBSco1.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtRGBTol1.Text) + Convert.ToDecimal(txtRGBTol2.Text)))
        //            {
        //                RGBSoc_B = txtRGBSco2.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtRGBTol1.Text) + Convert.ToDecimal(txtRGBTol3.Text)))
        //            {
        //                RGBSoc_B = txtRGBSco3.Text;
        //            }
        //            else
        //            {
        //                RGBSoc_B = "0";
        //            }

        //            if (RGBMaxSoc_B != "0")
        //            {
        //                RGBIndPer_B = Math.Round(((Convert.ToDecimal(RGBSoc_B) / Convert.ToDecimal(RGBMaxSoc_B)) * 100), 2).ToString();
        //            }
        //            ΔEGrade = Convert.ToDecimal(RGBIndPer_B);
        //            #endregion
        //        }
        //        else if (OriginFrom == "CMY25")
        //        {
        //            #region CMY25 calculation
        //            CMYMaxSoc_25 = (txtCMYSco1.Text == "") ? "0" : txtCMYSco1.Text;

        //            if (OriginalValue <= Convert.ToDecimal(txtCMYTol1.Text))
        //            {
        //                CMYSoc_25 = txtCMYSco1.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtCMYTol1.Text) + Convert.ToDecimal(txtCMYTol2.Text)))
        //            {
        //                CMYSoc_25 = txtCMYSco2.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtCMYTol1.Text) + Convert.ToDecimal(txtCMYTol3.Text)))
        //            {
        //                CMYSoc_25 = txtCMYSco3.Text;
        //            }
        //            else
        //            {
        //                CMYSoc_25 = "0";
        //            }

        //            if (CMYMaxSoc_25 != "0")
        //            {
        //                CMYIndPer_25 = Math.Round(((Convert.ToDecimal(CMYSoc_25) / Convert.ToDecimal(CMYMaxSoc_25)) * 100), 2).ToString();
        //            }
        //            ΔEGrade = Convert.ToDecimal(CMYIndPer_25);
        //            #endregion
        //        }
        //        else if (OriginFrom == "CMY50")
        //        {
        //            #region CMY50 calculation
        //            CMYMaxSoc_50 = (txtCMYSco1.Text == "") ? "0" : txtCMYSco1.Text;

        //            if (OriginalValue <= Convert.ToDecimal(txtCMYTol1.Text))
        //            {
        //                CMYSoc_50 = txtCMYSco1.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtCMYTol1.Text) + Convert.ToDecimal(txtCMYTol2.Text)))
        //            {
        //                CMYSoc_50 = txtCMYSco2.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtCMYTol1.Text) + Convert.ToDecimal(txtCMYTol3.Text)))
        //            {
        //                CMYSoc_50 = txtCMYSco3.Text;
        //            }
        //            else
        //            {
        //                CMYSoc_50 = "0";
        //            }

        //            if (CMYMaxSoc_50 != "0")
        //            {
        //                CMYIndPer_50 = Math.Round(((Convert.ToDecimal(CMYSoc_50) / Convert.ToDecimal(CMYMaxSoc_50)) * 100), 2).ToString();
        //            }
        //            ΔEGrade = Convert.ToDecimal(CMYIndPer_50);
        //            #endregion
        //        }
        //        else if (OriginFrom == "CMY75")
        //        {
        //            #region CMY75 calculation
        //            CMYMaxSoc_75 = (txtCMYSco1.Text == "") ? "0" : txtCMYSco1.Text;

        //            if (OriginalValue <= Convert.ToDecimal(txtCMYTol1.Text))
        //            {
        //                CMYSoc_75 = txtCMYSco1.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtCMYTol1.Text) + Convert.ToDecimal(txtCMYTol2.Text)))
        //            {
        //                CMYSoc_75 = txtCMYSco2.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtCMYTol1.Text) + Convert.ToDecimal(txtCMYTol3.Text)))
        //            {
        //                CMYSoc_75 = txtCMYSco3.Text;
        //            }
        //            else
        //            {
        //                CMYSoc_75 = "0";
        //            }

        //            if (CMYMaxSoc_75 != "0")
        //            {
        //                CMYIndPer_75 = Math.Round(((Convert.ToDecimal(CMYSoc_75) / Convert.ToDecimal(CMYMaxSoc_75)) * 100), 2).ToString();
        //            }
        //            ΔEGrade = Convert.ToDecimal(CMYIndPer_75);
        //            #endregion
        //        }
        //        else if (OriginFrom == "Paper")
        //        {
        //            #region Paper calculation
        //            PaperMaxSoc = (txtPaperSco1.Text == "") ? "0" : txtPaperSco1.Text;

        //            if (OriginalValue <= Convert.ToDecimal(txtPaperTol1.Text))
        //            {
        //                PaperSoc = txtPaperSco1.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtPaperTol1.Text) + Convert.ToDecimal(txtPaperTol2.Text)))
        //            {
        //                PaperSoc = txtPaperSco2.Text;
        //            }
        //            else if (OriginalValue <= (Convert.ToDecimal(txtPaperTol1.Text) + Convert.ToDecimal(txtPaperTol3.Text)))
        //            {
        //                PaperSoc = txtPaperSco3.Text;
        //            }
        //            else
        //            {
        //                PaperSoc = "0";
        //            }

        //            if (PaperMaxSoc != "0")
        //            {
        //                PaperIndPer = Math.Round(((Convert.ToDecimal(PaperSoc) / Convert.ToDecimal(PaperMaxSoc)) * 100), 2).ToString();
        //            }
        //            ΔEGrade = Convert.ToDecimal(PaperIndPer);
        //            #endregion
        //        }
        //    return ΔEGrade;
        //}

        //private void CreateReportPDF(DataSet Ds)
        //{
        //    // === Set all parameters to generate pdf file === //              

        //    Common.ReportPDF objReportPDF = new Common.ReportPDF();
        //    objReportPDF.ReportType = (int)enumReportType.ReportSingleMeasurement_ISO;

        //    objReportPDF.ReportType = (int)enumReportType.ReportScoringMeasurement_Default;

        //    objReportPDF.PDFPath = Server.MapPath("UploadFiles/Report_PDF/");
        //    objReportPDF.PDF_NAME = SessionManager.Instance.CurrentUser.UserId + "_ReportScoringMeasurement.PDF";
        //    objReportPDF.DS_Common = Ds;
        //    objReportPDF.DS_MetricAvgScoring = DSReportTotalAvg;
        //    objReportPDF.DS_ScoringMeasurementList = DSReportRevised;
        //    objReportPDF.From_ΔE = txt_dTE.Text;
        //    objReportPDF.DLPName = txt_DLPName.Text;
        //    objReportPDF.IsValidation = IsVerification;

        //    objReportPDF.ReportName = "Scoring Report";

        //    objReportPDF.MakePDF();

        //    // === After generate and save PDF file, Download it at a time === //
        //    byte[] bytes = System.IO.File.ReadAllBytes(Server.MapPath("~/UploadFiles/Report_PDF/" + objReportPDF.PDF_NAME));
        //    Common.Utilities.DownloadFile(bytes, objReportPDF.PDF_NAME, ".pdf");
        //}

        #region IViewReportGrading Members

        public int MeasurementId { get; set; }
        public int ProjectId { get; set; }
        public DataSet DSReport { get; set; }
        public DataSet DSReportRevised { get; set; }
        public DataSet DSReportTotalAvg { get; set; }

        public string From_ΔE { get; set; }

        public bool IsVerification
        {
            get
            {
                return (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);
            }
        }

        public int ValidationType { get; set; }

        public bool IsAverageOfMeasurements { get; set; }
       
        #endregion
 
    }
}