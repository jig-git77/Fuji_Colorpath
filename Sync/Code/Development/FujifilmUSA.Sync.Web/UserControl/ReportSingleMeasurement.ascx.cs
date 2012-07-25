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
namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ReportSingleMeasurement : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        #region Fill Charts
        private void CreateReport()
        {
            try
            {
                // ===== Create Object of ReportPrepressSingleMeasurementData ======//



                // ===== Get Data from data base  ==================//

                DataSet dsSingleMeasurement = new DataSet();

                if (dsSingleMeasurement != null && dsSingleMeasurement.Tables.Count > 0)
                {

                    #region " === Page : 1 Result Summary === "

                    Bind_TargetVersusActual_CHART(dsSingleMeasurement.Tables[1]); // Bind Chart Of Target And Actual Value


                    //BindSummaryDeltaValueBarChart(dsSingleMeasurement.Tables[2]); // Bind Chart of Summary Delta Value Bar Chart


                    //BindSummaryDeltaEValueBarChart(dsSingleMeasurement.Tables[3], 0, 10); //Bind Chart of Summary Delta E Value Bar Chart


                    #endregion

                    #region " === Page : 2 === "

                    //BindLABDeviationGraph(dsSingleMeasurement.Tables[4]); //Bind Charts Of Gray L* A* B* Deviation Line Chart



                    //CalcNPDBlackAndGrayChartsMaxMin(dsSingleMeasurement.Tables[4], dsSingleMeasurement.Tables[5]); // Calculating Max Min Value For NPD Charts
                    //BindGrayNPDGraph(dsSingleMeasurement.Tables[4]);  //Bind Chart Of Gray NPD Line Chart
                    //BindBlackNPDGraph(dsSingleMeasurement.Tables[5]);  //Bind Chart Of Black NPD Line Chart

                    #endregion

                    #region " === Page : 3  === "

                    //BindDotgainActualGraph(dsSingleMeasurement.Tables[6]);  //Bind Chart DotGain Actual Line Chart


                    //BindDotgainDeviationGraph(dsSingleMeasurement.Tables[8]);  //Bind Chart DotGain Deviation Bar Chart


                    //BindDensityDeviationGraph(dsSingleMeasurement.Tables[7]);  //Bind Chart Density Deviation Bar Chart               


                    //BindPrintContrastDeviationGraph(dsSingleMeasurement.Tables[9]); //Bind Chart Print Contrast Deviation Bar Chart
                    #endregion

                }
                CreateReportPDF(dsSingleMeasurement); // Generates PDF of Reports.
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

        }
        #endregion
    }
}