using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using WebSupergoo.ABCpdf8;
using WebSupergoo.ABCpdf8.Atoms;
using WebSupergoo.ABCpdf8.Objects;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Common
{
    public class ReportPDF
    {
        #region Declaration

        //string strServer = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Substring(0, System.Web.HttpContext.Current.Request.Url.AbsoluteUri.LastIndexOf("/") + 1);
        string strServer = System.Web.HttpContext.Current.Server.MapPath("Images") + "\\";
        string strServerStyleSheet = System.Web.HttpContext.Current.Server.MapPath("Templates") + "\\";

        int _ReportType = 0;
        string _PDFPath;
        string _PDF_NAME;
        string CSSPath = "";
        int theCount = 0;
        private DataSet _DS_Common;
        private DataSet _DS_MetricAvgScoring;
        private DataSet _DS_ScoringMeasurementList;
        private DataGrid _Dg;
        string _From_ΔE;
        string _DLPName;
        string _ReportName;
        bool _IsValidation;
        string _WebURL;
        private ChartContent _chartContent;
        private DataTable dtPatchDetails;
        #endregion


        #region Property

        public int ReportType
        {
            get { return _ReportType; }
            set { _ReportType = value; }
        }

        public string PDFPath
        {
            get { return _PDFPath; }
            set { _PDFPath = value; }
        }

        public string PDF_NAME
        {
            get { return _PDF_NAME; }
            set { _PDF_NAME = value; }
        }

        public DataSet DS_Common
        {
            get { return _DS_Common; }
            set { _DS_Common = value; }
        }

        public DataSet DS_MetricAvgScoring
        {
            get { return _DS_MetricAvgScoring; }
            set { _DS_MetricAvgScoring = value; }
        }

        public DataSet DS_ScoringMeasurementList
        {
            get { return _DS_ScoringMeasurementList; }
            set { _DS_ScoringMeasurementList = value; }
        }

        public DataGrid Dg
        {
            get { return _Dg; }
            set { _Dg = value; }
        }

        public string From_ΔE
        {
            get { return _From_ΔE; }
            set { _From_ΔE = value; }
        }

        public string DLPName
        {
            get { return _DLPName; }
            set { _DLPName = value; }
        }

        public string ReportName
        {
            get { return _ReportName; }
            set { _ReportName = value; }
        }

        public bool IsValidation
        {
            get { return _IsValidation; }
            set { _IsValidation = value; }
        }

        public string BaseWebURL
        {
            get { return _WebURL; }
            set { _WebURL = value; }
        }
        public ChartContent chartContent
        {
            get { return _chartContent; }
            set { _chartContent = value; }
        }
        public DataTable dtSpotPatchDetails
        {
            get { return dtPatchDetails; }
            set { dtPatchDetails = value; }
        }
        #endregion

        #region Constructor
        public ReportPDF()
        {
            string Dir1 = System.Web.HttpContext.Current.Server.MapPath("UploadFiles");
            string Dir2 = System.Web.HttpContext.Current.Server.MapPath("UploadFiles/Report_PDF");
            string Dir3 = System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp");
            if (System.IO.Directory.Exists(Dir1) == false) { System.IO.Directory.CreateDirectory(Dir1); }
            if (System.IO.Directory.Exists(Dir2) == false) { System.IO.Directory.CreateDirectory(Dir2); }
            if (System.IO.Directory.Exists(Dir3) == false) { System.IO.Directory.CreateDirectory(Dir3); }

            string currentBaseUrl = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority +
                                  System.Web.HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";

            CSSPath = currentBaseUrl + "Templates/style.css";

            //CSSPath = System.Web.HttpContext.Current.Server.MapPath("Templates/style.css");
            DS_Common = new DataSet();
        }
        #endregion

        #region Make PDF
        public void MakePDF()
        {
            try
            {
                string FileName = "";
                string str = "";

                //========== Object of PDF ==========//
                Doc theDoc = new Doc();

                //========== Reads the template ==========//
                StreamReader fp;

                //========== The rectangle for the content on pdf ==========//
                theDoc.Rect.Left = 0;
                theDoc.Rect.Bottom = 30;
                theDoc.Rect.Width = 610;
                theDoc.Rect.Height = 770;

                //========== The page ID Default is 1 ==========//
                int theID;


                #region Prepress Single Measurement ReportSingleMeasurement


                //========== Adds New Page IN PDF ==========//
                theDoc.Page = theDoc.AddPage();

                //========== Adds a counter to Page ==========//
                theCount++;

                //========== Read Template ==========//
                if (ReportType == 16) //----For G7 Opti / Validation Report + 4D G7 validation report
                {
                    FileName = "Templates/PrepressSingleMeasurement_G7.htm";
                }
                else if (ReportType == 17) //----For ISO Opti / Validation Report + 4D ISO validation report
                {
                    FileName = "Templates/PrepressSingleMeasurement_ISO.htm";
                }
                else if (ReportType == 18) //----For Default mode in Sync1D ISO, G7 or 4D validation project as choice of MTP
                {
                    FileName = "Templates/PrepressSingleMeasurement_Default.htm";
                }
                else if (ReportType == 19) //----For Default mode in Sync4D validation project for Scoring report
                {
                    FileName = "Templates/ScoringMeasurement_Default.htm";
                }
                else  //-----For 4D Optimization report
                {
                    FileName = "Templates/PrepressSingleMeasurement.htm";
                }

                fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                StringBuilder SBStr = new StringBuilder(fp.ReadToEnd());

                SBStr = TextReturn_ReportSingleMeasurement(SBStr);

                fp.Close();

                //========== Assign HTML to theID ==========//
                //1 : str = The html content.
                //3 : width(612) = The dimension of the page to fix in A4 size paper.
                theID = theDoc.AddImageHtml(SBStr.ToString(), true, 700, false);

                //========== Leave 30 Px on top of next page ==========//
                theDoc.Rect.Inset(0, 1);

                //========== Loops till the paging ==========//
                while (true)
                {
                    //========== Create A chanin for a pdf with multiple page ==========//
                    if (!theDoc.Chainable(theID))
                        break;

                    theDoc.Page = theDoc.AddPage();
                    theCount++;
                    theID = theDoc.AddImageToChain(theID);
                }

                //========== Reduce the top to start the next page from ==========//
                theDoc.Rect.Inset(0, -1);

                #endregion


                if (FileName.Trim() == "")
                {
                    throw new Exception("Please Enter Report Type.");
                }

                #region Add Footer
                theDoc.HPos = 0.0;
                theDoc.VPos = 0.0;
                theDoc.Color.String = "255 255 255";
                theDoc.FontSize = 8;

                theDoc.Rect.String = "-10 0 622 52";
                for (int i = 1; i <= theCount; i++)
                {
                    theDoc.HPos = 0.0;
                    theDoc.VPos = 0.0;
                    theDoc.PageNumber = i;


                    if (ReportType == 16) //----For G7 Opti / Validation Report + 4D G7 validation report
                    {
                        str = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> " +
                       "<tr style=\"height:52px\"><td align=\"left\" valign=\"middle\" bgcolor=\"#22af7c\" style=\"color:#000000; font-size:10px; font-family: Arial; padding: 0 0 0 50px; \" width=\"218px\" >" + DateTime.Now.ToLongDateString() + "</td> " +
                       "<td width=\"443px\" align=\"right\" valign=\"middle\" bgcolor=\"#b5ddc6\" style=\"color:#000000; font-size:10px; font-family: Arial; \">© Fujifilm North America Corporation, Graphic Systems Division " + " </td></tr></table>";

                    }
                    else
                    {
                        str = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> " +
                            "<tr style=\"height:25px\"><td align=\"center\" valign=\"middle\" bgcolor=\"#C1C1C1\" style=\"color:#000000; font-weight:bold ; font-size:8px; font-family:Tahoma, Arial, Helvetica, sans-serif;\" width=\"150px\" >" + DateTime.Now.ToLongDateString() + "</td> " +
                            "<td align=\"center\" valign=\"middle\" bgcolor=\"#C1C1C1\" style=\"color:#000000; font-weight:bold ; font-size:8px; font-family:Tahoma, Arial, Helvetica, sans-serif; \">&copy; Fujifilm " + DateTime.Now.Year.ToString() + "</td> " +
                            "<td width=\"112px\" align=\"center\" valign=\"middle\" bgcolor=\"#5A5A5A\" style=\"color:#CCCCCC; font-weight:bold ; font-size:8px; font-family:Tahoma, Arial, Helvetica, sans-serif; \">Page " + i.ToString() + " of " + theCount.ToString() + " </td></tr></table>";
                    }
                    theDoc.AddImageHtml(str);
                }

                #endregion

                string pth = PDFPath + PDF_NAME;

                // === If same named pdf file is already available then remove === //
                if (System.IO.File.Exists(pth))
                    System.IO.File.Delete(pth);

                theDoc.Save(pth);
                theDoc.Clear();
                theDoc.Dispose();
            }
            catch (Exception Exc)
            {
                throw new Exception(Exc.ToString());
            }
        }
        public void GenerateClientReportPDF()
        {
            try
            {
                string str = "";
                //========== Object of PDF ==========//
                Doc theDoc = new Doc();

                //========== Reads the template ==========//
                StreamReader fp;

                //========== The rectangle for the content on pdf ==========//
                theDoc.Rect.Left = 0;
                theDoc.Rect.Bottom = 30;
                theDoc.Rect.Width = 610;
                theDoc.Rect.Height = 770;

                //========== The page ID Default is 1 ==========//
                int theID;

                #region Prepress Single Measurement ReportSingleMeasurement
                //========== Adds New Page IN PDF ==========//
                theDoc.Page = theDoc.AddPage();

                //========== Adds a counter to Page ==========//
                theCount++;

                string FileName = "Templates/kendoPrepressSingleMeasurement.htm";
                //CSSPath = this.BaseWebURL + "Templates/style.css";
                CSSPath = strServerStyleSheet;
                if (ReportType == 16) //----For G7 Opti / Validation Report + 4D G7 validation report
                {
                    FileName = "Templates/kendoPrepressSingleMeasurement_G7.htm";
                }
                else if (ReportType == 17) //----For ISO Opti / Validation Report + 4D ISO validation report
                {
                    FileName = "Templates/kendoPrepressSingleMeasurement_ISO.htm";
                }
                else if (ReportType == 18) //----For Default mode in Sync1D ISO, G7 or 4D validation project as choice of MTP
                {
                    FileName = "Templates/kendoPrepressSingleMeasurement_Default.htm";
                }
                else if (ReportType == 19) //----For Default mode in Sync4D validation project for Scoring report
                {
                    FileName = "Templates/kendoScoringMeasurement_Default.htm";
                }

                //string FilePath = this._WebURL + FileName;
                string FilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/" + FileName);// System.Web.HttpContext.Current.Server.MapPath(FileName);

                if (FilePath.Trim() == "")
                {
                    throw new Exception("Please Enter Report Type.");
                }



                ////========== The rectangle for the content on pdf ==========//
                //theDoc.Rect.Left = 0;
                //theDoc.Rect.Bottom = 30;
                //theDoc.Rect.Width = 610;
                //theDoc.Rect.Height = 770;
                //theDoc.Page = theDoc.AddPage();

                ////========== The page ID Default is 1 ==========//
                //int theID;

                //========== Reads the template ==========//
                //var webClient = new System.Net.WebClient();
                //StringBuilder SBStr = new StringBuilder(webClient.DownloadString(FilePath));

                fp = File.OpenText(FilePath);
                StringBuilder SBStr = new StringBuilder(fp.ReadToEnd());
                fp.Close();
                SBStr = SetPDFContent(SBStr);
                
                theID = theDoc.AddImageHtml(SBStr.ToString(), true, 700, false);
                theDoc.Rect.Inset(0, 1);

                while (true)
                {
                    //========== Create A chanin for a pdf with multiple page ==========//
                    if (!theDoc.Chainable(theID))
                        break;

                    theDoc.Page = theDoc.AddPage();
                    theCount++;
                    theID = theDoc.AddImageToChain(theID);
                }

                theDoc.Rect.Inset(0, -1);

                #endregion

                #region Add Footer
                theDoc.HPos = 0.0;
                theDoc.VPos = 0.0;
                theDoc.Color.String = "255 255 255";
                theDoc.FontSize = 8;

                theDoc.Rect.String = "-10 0 622 52";
                for (int i = 1; i <= theCount; i++)
                {
                    theDoc.HPos = 0.0;
                    theDoc.VPos = 0.0;
                    theDoc.PageNumber = i;


                    if (ReportType == 16) //----For G7 Opti / Validation Report + 4D G7 validation report
                    {
                        str = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> " +
                       "<tr style=\"height:52px\"><td align=\"left\" valign=\"middle\" bgcolor=\"#22af7c\" style=\"color:#000000; font-size:10px; font-family: Arial; padding: 0 0 0 50px; \" width=\"218px\" >" + DateTime.Now.ToLongDateString() + "</td> " +
                       "<td width=\"443px\" align=\"right\" valign=\"middle\" bgcolor=\"#b5ddc6\" style=\"color:#000000; font-size:10px; font-family: Arial; \">© Fujifilm North America Corporation, Graphic Systems Division " + " </td></tr></table>";

                    }
                    else
                    {
                        str = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> " +
                            "<tr style=\"height:25px\"><td align=\"center\" valign=\"middle\" bgcolor=\"#C1C1C1\" style=\"color:#000000; font-weight:bold ; font-size:8px; font-family:Tahoma, Arial, Helvetica, sans-serif;\" width=\"150px\" >" + DateTime.Now.ToLongDateString() + "</td> " +
                            "<td align=\"center\" valign=\"middle\" bgcolor=\"#C1C1C1\" style=\"color:#000000; font-weight:bold ; font-size:8px; font-family:Tahoma, Arial, Helvetica, sans-serif; \">&copy; Fujifilm " + DateTime.Now.Year.ToString() + "</td> " +
                            "<td width=\"112px\" align=\"center\" valign=\"middle\" bgcolor=\"#5A5A5A\" style=\"color:#CCCCCC; font-weight:bold ; font-size:8px; font-family:Tahoma, Arial, Helvetica, sans-serif; \">Page " + i.ToString() + " of " + theCount.ToString() + " </td></tr></table>";
                    }
                    theDoc.AddImageHtml(str);
                }

                #endregion

                string pth = PDFPath + PDF_NAME;

                if (System.IO.File.Exists(pth))
                    System.IO.File.Delete(pth);

                theDoc.Save(pth);
                theDoc.Clear();
                theDoc.Dispose();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion

        #region Report 13 : Single Measureament Report
        private StringBuilder TextReturn_ReportSingleMeasurement(StringBuilder SBStr)
        {
            // === Image string to replace in chart tables === //

            DataTable DT = new DataTable();

            //SBStr.Replace("#csspath#", CSSPath);  //-- It was commented out passing path this way since it was giving error after putting KendoChart
            SBStr.Replace("###ReportName###", ReportName);

            // === Table 0 = "JobDetail" : Replace Job Detail In HTML Template ==== //    
            #region ==== Table = 0,  Job Detail  ====
            if (DS_Common.Tables[0] != null && DS_Common.Tables[0].Rows.Count > 0)
            {
                DT = DS_Common.Tables[0];

                string Media = Convert.ToString(DT.Rows[0]["Media"]).Trim();
                string Colorant = Convert.ToString(DT.Rows[0]["Colorant"]).Trim();
                string RipSettings = Convert.ToString(DT.Rows[0]["RipSettings"]).Trim();
                string Location = Convert.ToString(DT.Rows[0]["CompanyName"]).Trim();

                Location = (Location.Trim() != "" ? Location.Trim() + (Convert.ToString(DT.Rows[0]["BranchName"]).Trim() != "" ? " - " + Convert.ToString(DT.Rows[0]["BranchName"]) : "") : (Convert.ToString(DT.Rows[0]["BranchName"]).Trim() != "" ? Convert.ToString(DT.Rows[0]["BranchName"]) : ""));

                SBStr.Replace("###JobName###", Convert.ToString(DT.Rows[0]["MeasurementName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["MeasurementName"]).Trim());
                SBStr.Replace("###ProjectName###", Convert.ToString(DT.Rows[0]["ProjectName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["ProjectName"]).Trim());
                SBStr.Replace("###Device###", Convert.ToString(DT.Rows[0]["DeviceName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["DeviceName"]).Trim());
                SBStr.Replace("###TimeStamp###", Common.Utilities.GetDateByCultures(Convert.ToDateTime(DT.Rows[0]["MeasurementDate"])));
                SBStr.Replace("###OptimizedDLP###", DLPName.Trim() == "" ? "&nbsp;" : DLPName.Trim());
                SBStr.Replace("###PrintTarget###", Convert.ToString(DT.Rows[0]["ProfileName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["ProfileName"]).Trim());
                SBStr.Replace("###Media###", Media.Trim() == "" ? "&nbsp;" : Media.Trim());
                SBStr.Replace("###Colorant###", Colorant.Trim() == "" ? "&nbsp;" : Colorant.Trim());
                SBStr.Replace("###RipSettings###", RipSettings.Trim() == "" ? "&nbsp;" : RipSettings.Trim());
                SBStr.Replace("###Location###", Location.Trim() == "" ? "&nbsp;" : Location.Trim());
                SBStr.Replace("###DeltaEFormula###", Convert.ToString(DT.Rows[0]["DeltaEFormula"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["DeltaEFormula"]).Trim());
                SBStr.Replace("###OffsetNA###", Convert.ToString(DT.Rows[0]["LicenseTypeID"]).Trim() == "2" ? "(N/A)" : "&nbsp;"); //----Display "N/A" in offset printing data label for Sync 4D project types---//

                //--------For ISO / Default Reports OR 4D Validation Scoring Report
                if (ReportType == 16 || ReportType == 17 || ReportType == 18 || ReportType == 19)
                {
                    if (IsValidation) //Only show Measurement Status in Validation mode in G7/ ISO / Default/ 4D
                    {
                        SBStr.Replace("###JobStatus###", Convert.ToString(DT.Rows[0]["JobStatus"]).Trim() == "" ? "<tr class='row-2'><td class='row-title'>Meas. Status:</td><td class='row-content'>&nbsp;</td></tr>" : (Convert.ToString(DT.Rows[0]["JobStatus"]).Contains("OK")) ? "<tr class='row-2'><td class='row-title'>Meas. Status:</td><td class='row-content'>" + Convert.ToString(DT.Rows[0]["JobStatus"]).Trim() + "</td></tr>" : "<tr class='row-2'><td class='row-title'  style='color:Red;'>Meas. Status:</td><td class='row-content'  style='color:Red;'>" + Convert.ToString(DT.Rows[0]["JobStatus"]).Trim() + "</td></tr>");
                        SBStr.Replace("###RunNo###", "&nbsp;");   //-----In validation report there is no run numbers-------//
                        SBStr.Replace("##ReportTitle##", "G7VAlignmentReportTitle");
                    }
                    else
                    {
                        SBStr.Replace("###JobStatus###", "&nbsp;");
                        SBStr.Replace("###RunNo###", "(" + (Convert.ToString(DT.Rows[0]["RunName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["RunName"]).Trim()) + ")");
                        SBStr.Replace("##ReportTitle##", "G7AlignmentReportTitle");
                    }
                    SBStr.Replace("###MetricPreset###", Convert.ToString(DT.Rows[0]["MetricTargetPresetName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["MetricTargetPresetName"]).Trim());
                    SBStr.Replace("###CompanyName###", Convert.ToString(DT.Rows[0]["CompanyName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["CompanyName"]).Trim());
                    SBStr.Replace("###BranchName###", Convert.ToString(DT.Rows[0]["BranchName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["BranchName"]).Trim());
                    SBStr.Replace("###PressName###", Convert.ToString(DT.Rows[0]["DeviceName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["DeviceName"]).Trim());
                    SBStr.Replace("###ProjectName###", Convert.ToString(DT.Rows[0]["ProjectName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["ProjectName"]).Trim());
                    SBStr.Replace("###CurrentDate###", System.DateTime.Now.ToString("dddd, MMMM dd, yyyy"));

                    if (ReportType == 16) //--------For G7 Report
                    {
                        SBStr.Replace("###MeasurementType###", Convert.ToString(DT.Rows[0]["MeasurementName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["MeasurementName"]).Trim().Contains("Averaging") == true ? "This report details an average of measurements." : "This report details a single measurement.");
                        SBStr.Replace("###Technician###", Convert.ToString(DT.Rows[0]["TechnicianName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["TechnicianName"]).Trim());
                        SBStr.Replace("###MeasurementDevice###", Convert.ToString(DT.Rows[0]["InstrumentType"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["InstrumentType"]).Trim());
                        SBStr.Replace("###SerialNumber###", Convert.ToString(DT.Rows[0]["SerialNumber"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["SerialNumber"]).Trim());
                        SBStr.Replace("###UVMode###", Convert.ToString(DT.Rows[0]["UVMode"]).Trim() == "" ? "&nbsp;" : 'M' + Convert.ToString(DT.Rows[0]["UVMode"]).Trim());
                        SBStr.Replace("###DLPChart###", Convert.ToString(DT.Rows[0]["ChartName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["ChartName"]).Trim());
                        SBStr.Replace("###OptiChart###", Convert.ToString(DT.Rows[0]["OPTChartName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["OPTChartName"]).Trim());
                        SBStr.Replace("###InkOrder###", Convert.ToString(DT.Rows[0]["InkOrder"]).Trim() == "" ? "&nbsp;" : Convert.ToString((Entity.EInkOrder)Convert.ToInt16(DT.Rows[0]["InkOrder"])).Trim());
                        SBStr.Replace("###Fountain###", Convert.ToString(DT.Rows[0]["FountainSolution"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["FountainSolution"]).Trim());
                        SBStr.Replace("###Blanket###", Convert.ToString(DT.Rows[0]["Blanket"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["Blanket"]).Trim());
                        SBStr.Replace("###BlanketAge###", Convert.ToString(DT.Rows[0]["BlanketAge"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["BlanketAge"]).Trim());
                        SBStr.Replace("###Screening###", Convert.ToString(DT.Rows[0]["Screening"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["Screening"]).Trim());
                        SBStr.Replace("###Coating###", Convert.ToString(DT.Rows[0]["Coating"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["Coating"]).Trim());
                        SBStr.Replace("###Notes###", Convert.ToString(DT.Rows[0]["Notes"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["Notes"]).Trim());
                    }
                }

            }
            else
            {
                SBStr.Replace("###JobName###", "&nbsp;");
                SBStr.Replace("###ProjectName###", "&nbsp;");
                SBStr.Replace("###Device###", "&nbsp;");
                SBStr.Replace("###TimeStamp###", "&nbsp;");
                SBStr.Replace("###OptimizedDLP###", "&nbsp;");
                SBStr.Replace("###PrintTarget###", "&nbsp;");
                SBStr.Replace("###Media###", "&nbsp;");
                SBStr.Replace("###Colorant###", "&nbsp;");
                SBStr.Replace("###RipSettings###", "&nbsp;");
                SBStr.Replace("###Location###", "&nbsp;");
                SBStr.Replace("###DeltaEFormula###", "&nbsp;");

                //--------For G7 / ISO / Default Reports OR 4D Validation Scoring Report
                if (ReportType == 16 || ReportType == 17 || ReportType == 18 || ReportType == 19)
                {
                    if (IsValidation) //Only show Measurement STatus in Validation mode in G7/ ISO / Default/ 4D
                        SBStr.Replace("###JobStatus###", "<tr class='row-2'><td class='row-title'>Meas. Status:</td><td class='row-content'>&nbsp;</td></tr>");
                    else
                        SBStr.Replace("###JobStatus###", "&nbsp;");

                    SBStr.Replace("###MetricPreset###", "&nbsp;");
                    SBStr.Replace("###CompanyName###", "&nbsp;");
                    SBStr.Replace("###BranchName###", "&nbsp;");
                    SBStr.Replace("###PressName###", "&nbsp;");
                    SBStr.Replace("###ProjectName###", "&nbsp;");

                    if (ReportType == 16) //--------For G7 Report
                    {
                        SBStr.Replace("###Technician###", "&nbsp;");
                        SBStr.Replace("###MeasurementDevice###", "&nbsp;");
                        SBStr.Replace("###SerialNumber###", "&nbsp;");
                        SBStr.Replace("###UVMode###", "&nbsp;");
                        SBStr.Replace("###DLPChart###", "&nbsp;");
                        SBStr.Replace("###OptiChart###", "&nbsp;");
                        SBStr.Replace("###InkOrder###", "&nbsp;");
                        SBStr.Replace("###Fountain###", "&nbsp;");
                        SBStr.Replace("###Blanket###", "&nbsp;");
                        SBStr.Replace("###BlanketAge###", "&nbsp;");
                        SBStr.Replace("###Screening###", "&nbsp;");
                        SBStr.Replace("###Coating###", "&nbsp;");
                        SBStr.Replace("###Notes###", "&nbsp;");
                    }
                }
            }
            #endregion

            // === Table 1 = Chart of Target Vs Actual AB Plot === //           
            #region ==== Target Vs Actual AB Plot ====
            SBStr.Replace("###TargetVersusActualAB###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_TargetVersusActualAB.Jpeg"));
            #endregion

            if (ReportType != 19) //---Only if not 4D Validation Scoring Report
            {
                // === Table 2 = Chart of Summary Delta Value Graph Image And Table Value Ploted === //          
                #region ==== Summary Delta Value Graph Image ====

                DT = DS_Common.Tables[2];

                StringBuilder SBstrDeltaValue = new StringBuilder();
                if (DT != null && DT.Rows.Count > 0)
                {
                    string strStatusImg = "";
                    int RowHeight = 29;

                    //--------For ISO /Default Reports
                    if (ReportType == 17 || ReportType == 18)
                    {
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            if (Convert.ToDecimal(DT.Rows[i]["MeasuredValue"]) > Convert.ToDecimal(DT.Rows[i]["TargetValue"].ToString()))
                            {
                                SBstrDeltaValue.Append("<tr style='height: " + RowHeight + "px; color:Red;'>");
                            }
                            else
                            {
                                SBstrDeltaValue.Append("<tr style='height: " + RowHeight + "px'>");
                            }
                            SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>" + DT.Rows[i]["MetricSumName"] + "</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["TargetValue"])) + "&nbsp;</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["MeasuredValue"])) + "&nbsp;</td>");
                            SBstrDeltaValue.Append("</tr>");
                        }
                    }
                    else if (ReportType == 16) //--------For G7 Report
                    {
                        RowHeight = 12;
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            if ((i % 2) == 0) //----If even row then make background color green----//
                                SBstrDeltaValue.Append("<tr class = 'Metadata-Table _idGenTableRowColumn-3' style='height: " + RowHeight + "px'>");
                            else             //------Else keep background color white-----//
                                SBstrDeltaValue.Append("<tr class = 'Metadata-Table _idGenTableRowColumn-4' style='height: " + RowHeight + "px'>");

                            SBstrDeltaValue.Append("<td valign='middle' align='left' class='Metadata-Table Green' style='width: 160px;'> <p class='Body-Text-Bold'>" + DT.Rows[i]["MetricSumName"] + "</p></td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["TargetValue"])) + "&nbsp;</p></td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["MeasuredValue"])) + "&nbsp;</p></td>");
                            SBstrDeltaValue.Append("</tr>");
                        }

                    }
                    else
                    {
                        #region "AverageDE"
                        if (Convert.IsDBNull(DT.Rows[0]["Avg_ΔE"]))
                        {
                            SBstrDeltaValue.Append("<tr style='height: 27px'>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("<td valign='middle' class='BottomBorder' style='width: 160px;'>Average &Delta;" + From_ΔE + "</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("</tr>");
                        }
                        else
                        {
                            SBstrDeltaValue.Append("<tr style='height: 27px'>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Average &Delta;" + From_ΔE + "</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Avg_ΔE"])) + "&nbsp;</td>");
                            SBstrDeltaValue.Append("</tr>");
                        }
                        #endregion

                        #region "MaximumDE"
                        if (Convert.IsDBNull(DT.Rows[0]["Max_ΔE"]))
                        {
                            SBstrDeltaValue.Append("<tr style='height: 27px'>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Maximum &Delta;" + From_ΔE + "</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("</tr>");
                        }
                        else
                        {

                            SBstrDeltaValue.Append("<tr style='height: 27px'>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Maximum &Delta;" + From_ΔE + "</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Max_ΔE"])) + "&nbsp;</td>");
                            SBstrDeltaValue.Append("</tr>");
                        }
                        #endregion

                        #region "PrimaryDE"
                        if (Convert.IsDBNull(DT.Rows[0]["PriΔE"]))
                        {
                            SBstrDeltaValue.Append("<tr style='height: 27px'>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Max Primary &Delta;" + From_ΔE + "</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("</tr>");
                        }
                        else
                        {
                            SBstrDeltaValue.Append("<tr style='height: 27px'>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Max Primary &Delta;" + From_ΔE + "</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["PriΔE"])) + "&nbsp;</td>");
                            SBstrDeltaValue.Append("</tr>");
                        }
                        #endregion

                        #region "PrimaryDH"
                        if (Convert.IsDBNull(DT.Rows[0]["PriΔH"]))
                        {
                            SBstrDeltaValue.Append("<tr style='height: 27px'>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Max Primary &Delta;H</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("</tr>");
                        }
                        else
                        {

                            SBstrDeltaValue.Append("<tr style='height: 27px'>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Max Primary &Delta;H</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["PriΔH"])) + "&nbsp;</td>");
                            SBstrDeltaValue.Append("</tr>");
                        }
                        #endregion

                        #region "GrayAverageDE"
                        if (Convert.IsDBNull(DT.Rows[0]["GrayΔE"]))
                        {
                            SBstrDeltaValue.Append("<tr style='height: 27px'>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Gray Average &Delta;" + From_ΔE + "</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("</tr>");
                        }
                        else
                        {

                            SBstrDeltaValue.Append("<tr style='height: 27px'>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Gray Average &Delta;" + From_ΔE + "</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["GrayΔE"])) + "&nbsp;</td>");
                            SBstrDeltaValue.Append("</tr>");
                        }

                        #endregion

                        #region "dE_95pct_Patches"
                        if (Convert.IsDBNull(DT.Rows[0]["NinetyFiveΔE"]))
                        {
                            SBstrDeltaValue.Append("<tr style='height: 27px'>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>95th Percentile &Delta;" + From_ΔE + "</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("</tr>");
                        }
                        else
                        {

                            SBstrDeltaValue.Append("<tr style='height: 27px'>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>95th Percentile &Delta;" + From_ΔE + "</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                            SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["NinetyFiveΔE"])) + "&nbsp;</td>");
                            SBstrDeltaValue.Append("</tr>");
                        }
                        #endregion
                    }
                    SBStr.Replace("###DeltaSummaryDetail###", SBstrDeltaValue.ToString());
                }
                else
                {
                    SBStr.Replace("###DeltaSummaryDetail###", "");
                }
                SBStr.Replace("###ResultSummary_DeltaImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_ResultSummary_Delta.Jpeg"));
                #endregion
            }

            // === Table 3 = Chart of Summary Delta E Value Graph Image And Table Value Ploted  === //         
            #region ==== Summary Delta E Value Graph Image ====
            DT = DS_Common.Tables[3];
            StringBuilder SBstrDeltaEValue = new StringBuilder();
            if (DT != null && DT.Rows.Count > 0)
            {
                string strStatusImg = "";

                int RowHeight = 280 / (DT.Rows.Count + 2);
                if (ReportType == 16) //--------For G7 Report
                {
                    RowHeight = 12;

                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        SBstrDeltaEValue.Append("<tr class = 'Metadata-Table _idGenTableRowColumn-4'>");
                        SBstrDeltaEValue.Append("<td valign='middle' align='left' class='Metadata-Table " + DT.Rows[i]["ShortPatchType"] + "'> <p class='Body-Text-Bold'>" + DT.Rows[i]["ShortPatchType"] + "</p></td>");
                        SBstrDeltaEValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["TargetL"])) + "&nbsp;</p></td>");
                        SBstrDeltaEValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Targeta"])) + "&nbsp;</p></td>");
                        SBstrDeltaEValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Targetb"])) + "&nbsp;</p></td>");
                        SBstrDeltaEValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["ActualL"])) + "&nbsp;</p></td>");
                        SBstrDeltaEValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Actuala"])) + "&nbsp;</p></td>");
                        SBstrDeltaEValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Actualb"])) + "&nbsp;</p></td>");
                        SBstrDeltaEValue.Append("<td valign='middle' align='center' class='Metadata-Table Gray'> <p class='Body-Text-Center'>" + (DT.Rows[i]["ΔELimit"].ToString() == "0.00" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["ΔELimit"]))) + "&nbsp;</p></td>");
                        SBstrDeltaEValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["ΔE"])) + "&nbsp;</p></td>");
                        SBstrDeltaEValue.Append("</tr>");
                    }
                }
                else
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        SBstrDeltaEValue.Append("<tr style='height: " + RowHeight + "px'>");
                        SBstrDeltaEValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                        SBstrDeltaEValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>" + Convert.ToString(DT.Rows[i]["PatchType"]) + "</td>");
                        //--------For ISO Reports put Tolerance
                        if (ReportType == 17 || ReportType == 18 || ReportType == 19)
                        {
                            if (DT.Rows[i]["ΔELimit"].ToString() != "0.00") //If no Tolerance value exists for particular Metric Target Preset then don't display "0.00" instead even.
                            {
                                SBstrDeltaEValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["ΔELimit"])) + "&nbsp;</td>");
                            }
                            else
                                SBstrDeltaEValue.Append("<td valign='middle' align='center' class='BottomBorder'> &nbsp;</td>");
                        }
                        else
                            SBstrDeltaEValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + "&nbsp;</td>");
                        SBstrDeltaEValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["ΔE"])) + "&nbsp;</td>");
                        SBstrDeltaEValue.Append("</tr>");
                    }
                }
                SBStr.Replace("###DeltaESummaryDetail###", SBstrDeltaEValue.ToString());
            }
            else
            {
                SBStr.Replace("###DeltaESummaryDetail###", "");
            }
            SBStr.Replace("###ResultSummary_DeltaEImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_ResultSummary_DeltaEValue.Jpeg"));
            #endregion

            if (ReportType == 17 || ReportType == 18 || ReportType == 19)   //----For ISO Opti / Validation Report & 4D ISO validation report + Default mode report OR 4D Validation Scoring report
            {
                // === Table 4 = Chart of Paper L*A*B* === //           
                #region ==== Paper  L*A*B* (Part 1)====
                SBStr.Replace("###PaperLab1###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_PaperLab1.Jpeg"));
                #endregion

                // === Table 5 = Chart of Paper L*A*B* === //           
                #region ==== Paper  L*A*B* (Part 2)====
                SBStr.Replace("###PaperLab2###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_PaperLab2.Jpeg"));
                #endregion

                if (ReportType == 17) //For ISO Opti / Validation Report & 4D ISO validation report
                {
                    // === Table 6 = Chart of Density === //           
                    #region ==== Density ====
                    SBStr.Replace("###DensityChart###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_Density.Jpeg"));
                    #endregion

                    // === Table 7 = Chart of TVISpread === //           
                    #region ==== TVISpread ====
                    SBStr.Replace("###TVISpread###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page4_TVISpread.Jpeg"));
                    #endregion

                    // === Table 8 = Chart of TVISpread Data === //           
                    #region ==== TVISpreadData ====
                    DT = DS_Common.Tables[4];

                    StringBuilder SBstrTVIPatch = new StringBuilder();
                    StringBuilder SBstrTVIPatchVariation = new StringBuilder();


                    SBstrTVIPatch.Append("<table width='100%' border='0' align='center' cellpadding='0' cellspacing='0'>");
                    SBstrTVIPatch.Append("<tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'>");
                    SBstrTVIPatchVariation.Append("<tr class='row-1'>");

                    StringBuilder SBstrDeltaValue1 = new StringBuilder();
                    if (DT != null && DT.Rows.Count > 0)
                    {
                        //--------For ISO / 4D Validation ISO Reports


                        if (Convert.ToDecimal(DT.Rows[0]["Tone20"]) != 0)
                        {

                            SBstrTVIPatch.Append("<td valign='middle' align='left'  class='row-content1'> Tone20 </td>");

                            SBstrTVIPatchVariation.Append("<td valign='middle' align='left'  class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Tone20"])) + "&nbsp;</td>");

                        }
                        if (Convert.ToDecimal(DT.Rows[0]["Tone25"]) != 0)
                        {

                            SBstrTVIPatch.Append("<td valign='middle' align='left'  class='row-content1'> Tone25 </td>");

                            SBstrTVIPatchVariation.Append("<td valign='middle' align='left'   class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Tone25"])) + "&nbsp;</td>");

                        }
                        if (Convert.ToDecimal(DT.Rows[0]["Tone40"]) != 0)
                        {

                            SBstrTVIPatch.Append("<td valign='middle' align='left'  class='row-content1'> Tone40 </td>");

                            SBstrTVIPatchVariation.Append("<td valign='middle' align='left' class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Tone40"])) + "&nbsp;</td>");

                        }
                        if (Convert.ToDecimal(DT.Rows[0]["Tone50"]) != 0)
                        {

                            SBstrTVIPatch.Append("<td valign='middle' align='left'  class='row-content1'> Tone50 </td>");

                            SBstrTVIPatchVariation.Append("<td valign='middle' align='left'  class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Tone50"])) + "&nbsp;</td>");
                        }
                        if (Convert.ToDecimal(DT.Rows[0]["Tone60"]) != 0)
                        {
                            SBstrTVIPatch.Append("<td valign='middle' align='left' class='row-content1'> Tone60 </td>");

                            SBstrTVIPatchVariation.Append("<td valign='middle' align='left'  class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Tone60"])) + "&nbsp;</td>");

                        }
                        if (Convert.ToDecimal(DT.Rows[0]["Tone75"]) != 0)
                        {
                            SBstrTVIPatch.Append("<td valign='middle' align='left'  class='row-content1'> Tone75 </td>");

                            SBstrTVIPatchVariation.Append("<td valign='middle' align='left'  class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Tone75"])) + "&nbsp;</td>");

                        }
                        if (Convert.ToDecimal(DT.Rows[0]["Tone80"]) != 0)
                        {
                            SBstrTVIPatch.Append("<td valign='middle' align='left' class='row-content1'> Tone80 </td>");

                            SBstrTVIPatchVariation.Append("<td valign='middle' align='left'  class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Tone80"])) + "&nbsp;</td>");

                        }
                        SBstrTVIPatch.Append("</tr>");
                        SBstrTVIPatchVariation.Append("</tr>");

                    }
                    SBStr.Replace("###TVISpreadData###", SBstrTVIPatch.ToString() + SBstrTVIPatchVariation.ToString() + "</table>");
                    #endregion

                    // === Table 9 = Chart of CyanTVI === //           
                    #region ==== CyanTVI ====
                    SBStr.Replace("###CyanTVI###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page4_CyanTVI.Jpeg"));
                    #endregion

                    // === Table 10 = Chart of MagentaTVI === //           
                    #region ==== MagentaTVI ====
                    SBStr.Replace("###MagentaTVI###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page4_MagentaTVI.Jpeg"));
                    #endregion

                    // === Table 11 = Chart of YellowTVI === //           
                    #region ==== YellowTVI ====
                    SBStr.Replace("###YellowTVI###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page4_YellowTVI.Jpeg"));
                    #endregion

                    // === Table 12 = Chart of BlackTVI === //           
                    #region ==== BlackTVI ====
                    SBStr.Replace("###BlackTVI###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page4_BlackTVI.Jpeg"));
                    #endregion

                    // === Table 13 = >Gray Balance L*a*b* === //           
                    #region ==== Gray LAB* ====
                    SBStr.Replace("###GrayL###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_LChart.Jpeg"));
                    SBStr.Replace("###GrayA###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_AChart.Jpeg"));
                    SBStr.Replace("###GrayB###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_BChart.Jpeg"));
                    #endregion
                }
                else if (ReportType == 18 || ReportType == 19) //For Default mode report OR 4D Validation Scoring Report
                {
                    // === Table 13 = >Gray Balance L*a*b* === //           
                    #region ==== Gray LAB* ====
                    SBStr.Replace("###GrayL###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_LChart.Jpeg"));
                    SBStr.Replace("###GrayA###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_AChart.Jpeg"));
                    SBStr.Replace("###GrayB###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_BChart.Jpeg"));
                    #endregion
                }
            }

            //--------For G7 / All Validation Report
            if (ReportType == 16)
            {
                // === Table 4 = for Chart of Gray ΔCh === //           
                #region ==== Gray ΔCh Chart ====
                SBStr.Replace("###ResultSummary_DeltaChImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_DeltaChChart.Jpeg"));
                #endregion

                // === Table 5 = for Chart of Black ΔL === //           
                #region ==== Black ΔL Chart ====
                SBStr.Replace("###ResultSummary_BlackDeltaLImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_BlackDeltaLChart.Jpeg"));
                #endregion

                // === Table 6 = for Chart of Gray ΔL === //           
                #region ==== Gray ΔL Chart ====
                SBStr.Replace("###ResultSummary_GrayDeltaLImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_GrayDeltaLChart.Jpeg"));
                #endregion

                // === Table 7 = for Chart of Gray Balance L* === //           
                #region ==== Gray Balance L* Chart ====
                SBStr.Replace("###ResultSummary_GrayLImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_LChart.Jpeg"));
                #endregion

                // === Table 8 = for Chart of Gray Balance a* === //           
                #region ==== Gray Balance a* Chart ====
                SBStr.Replace("###ResultSummary_GrayaImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_AChart.Jpeg"));
                #endregion

                // === Table 9 = for Chart of Gray Balance b* === //           
                #region ==== Gray Balance b* Chart ====
                SBStr.Replace("###ResultSummary_GraybImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_BChart.Jpeg"));
                #endregion

                // === Table 10 = for Chart of Black NPD === //           
                #region ==== Black NPD Chart ====
                SBStr.Replace("###ResultSummary_BlackNPDImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_BlackNPD.Jpeg"));
                #endregion

                // === Table 11 = for Chart of Gray NPD === //           
                #region ==== Gray NPD Chart ====
                SBStr.Replace("###ResultSummary_GrayNPDImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_GrayNPD.Jpeg"));
                #endregion

                //-------Extra items for new G7 formatted reports--------------//
                SBStr.Replace("###DensityChart###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_Density.Jpeg"));
                SBStr.Replace("###ABComparison###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page4_Gamut_ABComparison.Jpeg"));
                SBStr.Replace("###LComparison###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page4_Gamut_LComparison.Jpeg"));
                SBStr.Replace("###ResultSummary_TVIImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page4_CHRT_SummaryTVI.Jpeg"));

                //------Region: for Density Data----//
                DT = DS_Common.Tables[5];

                StringBuilder SBstrDeltaValue = new StringBuilder();
                StringBuilder SBstrDeltaValue1 = new StringBuilder();
                StringBuilder SBstrDeltaValue2 = new StringBuilder();

                if (DT != null && DT.Rows.Count > 0)
                {
                    int RowHeight = 12;

                    SBstrDeltaValue.Append("<tr class = 'Metadata-Table _idGenTableRowColumn-3' style='height: " + RowHeight + "px'>");
                    SBstrDeltaValue1.Append("<tr class = 'Metadata-Table _idGenTableRowColumn-4' style='height: " + RowHeight + "px'>");
                    SBstrDeltaValue2.Append("<tr class = 'Metadata-Table _idGenTableRowColumn-3' style='height: " + RowHeight + "px'>");
                    SBstrDeltaValue.Append("<td valign='middle' align='left' class='Metadata-Table Green'> <p class='Body-Text-Bold'>Target</p></td>");
                    SBstrDeltaValue1.Append("<td valign='middle' align='left' class='Metadata-Table Green'> <p class='Body-Text-Bold'>Actual</p></td>");
                    SBstrDeltaValue2.Append("<td valign='middle' align='left' class='Metadata-Table Green'> <p class='Body-Text-Bold'>Previous</p></td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[0]["DensityLimit"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["DensityLimit"]))) + "&nbsp;</p></td>");
                    SBstrDeltaValue1.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[0]["Density"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Density"]))) + "&nbsp;</p></td>");
                    SBstrDeltaValue2.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[0]["PreAvgDensity"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["PreAvgDensity"]))) + "&nbsp;</p></td>");

                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[1]["DensityLimit"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[1]["DensityLimit"]))) + "&nbsp;</p></td>");
                    SBstrDeltaValue1.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[1]["Density"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[1]["Density"]))) + "&nbsp;</p></td>");
                    SBstrDeltaValue2.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[1]["PreAvgDensity"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[1]["PreAvgDensity"]))) + "&nbsp;</p></td>");

                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[2]["DensityLimit"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[2]["DensityLimit"]))) + "&nbsp;</p></td>");
                    SBstrDeltaValue1.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[2]["Density"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[2]["Density"]))) + "&nbsp;</p></td>");
                    SBstrDeltaValue2.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[2]["PreAvgDensity"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[2]["PreAvgDensity"]))) + "&nbsp;</p></td>");

                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[3]["DensityLimit"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[3]["DensityLimit"]))) + "&nbsp;</p></td>");
                    SBstrDeltaValue1.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[3]["Density"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[3]["Density"]))) + "&nbsp;</p></td>");
                    SBstrDeltaValue2.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[3]["PreAvgDensity"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[3]["PreAvgDensity"]))) + "&nbsp;</p></td>");

                    SBstrDeltaValue.Append("</tr>");
                    SBstrDeltaValue1.Append("</tr>");
                    SBstrDeltaValue2.Append("</tr>");

                    SBstrDeltaValue.Append(SBstrDeltaValue1);
                    if (!IsValidation) //----Don't add Previous run row into Validation report since there is no previous run in validation
                    {
                        SBstrDeltaValue.Append(SBstrDeltaValue2);
                    }

                    SBStr.Replace("###DensitySummaryDetails###", SBstrDeltaValue.ToString());
                }
                //------End Region: for Density Data--------//

                //------Region: for ∆E Primary Colorimetric Aims (Ink tab)-------//


                //------End Region: for ∆E Primary Colorimetric Aims (Ink tab)-------//
            }

            if (ReportType == 19) //---Only if 4D Validation Scoring Report
            {
                // === Table 4.1 = Display Metrics with Average Scoring on list === //          
                #region ==== Metric Avg Scoring Graph Image ====

                DT = DS_MetricAvgScoring.Tables[0];

                StringBuilder SBstrMetricAvgScoring = new StringBuilder();
                decimal CMYKPrimaries = 0;
                decimal RGBSecondaries = 0;
                decimal dt_HGray = 0;
                decimal dt_EPaper = 0;

                if (DT != null && DT.Rows.Count > 0)
                {
                    string strStatusImg = "";
                    int RowHeight = 29;

                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        SBstrMetricAvgScoring.Append("<tr style='height: " + RowHeight + "px'>");
                        SBstrMetricAvgScoring.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                        SBstrMetricAvgScoring.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>" + DT.Rows[i]["Color"] + "</td>");
                        SBstrMetricAvgScoring.Append("<td valign='middle' align='center' class='BottomBorder'>" + Math.Round((Convert.ToDecimal(DT.Rows[i]["AvgScore"])), 0) + "&nbsp;</td>");
                        SBstrMetricAvgScoring.Append("</tr>");

                        if (i >= 0 && i <= 3)
                        {
                            CMYKPrimaries = CMYKPrimaries + Convert.ToDecimal(DT.Rows[i]["AvgScore"]);
                        }
                        else if (i >= 4 && i <= 6)
                        {
                            RGBSecondaries = RGBSecondaries + Convert.ToDecimal(DT.Rows[i]["AvgScore"]);
                        }
                        else if (i >= 7 && i <= 9)
                        {
                            dt_HGray = dt_HGray + Convert.ToDecimal(DT.Rows[i]["AvgScore"]);
                        }
                        else if (i == 10)
                        {
                            dt_EPaper = dt_EPaper + Convert.ToDecimal(DT.Rows[i]["AvgScore"]);
                        }
                    }
                    CMYKPrimaries = ((CMYKPrimaries) / 4);
                    RGBSecondaries = ((RGBSecondaries) / 3);
                    dt_HGray = ((dt_HGray) / 3);

                    SBStr.Replace("###MetricAvgScoringDetail###", SBstrMetricAvgScoring.ToString());
                    SBStr.Replace("###AvgScoringCMYKPrimaries###", Math.Round(CMYKPrimaries, 0).ToString());
                    SBStr.Replace("###AvgScoringRGBSecondaries###", Math.Round(RGBSecondaries, 0).ToString());
                    SBStr.Replace("###AvgScoringΔHGray###", Math.Round(dt_HGray, 0).ToString());
                    SBStr.Replace("###AvgScoringPaperΔE###", Math.Round(dt_EPaper, 0).ToString());
                    SBStr.Replace("###TotalScore###", Math.Round(((CMYKPrimaries + RGBSecondaries + dt_HGray + dt_EPaper) / 4), 0).ToString());
                }
                else
                {
                    SBStr.Replace("###MetricAvgScoringDetail###", "");
                    SBStr.Replace("###AvgScoringCMYKPrimaries###", "");
                    SBStr.Replace("###AvgScoringRGBSecondaries###", "");
                    SBStr.Replace("###AvgScoringΔHGray###", "");
                    SBStr.Replace("###AvgScoringPaperΔE###", "");
                    SBStr.Replace("###TotalScore###", "");
                }

                // === Table 4.2 = Display Scoring Measurement List === //          

                DT = DS_ScoringMeasurementList.Tables[0];

                StringBuilder SBstrScoringMeasurementList = new StringBuilder();
                if (DT != null && DT.Rows.Count > 0)
                {
                    int RowHeight = 29;

                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        SBstrScoringMeasurementList.Append("<tr style='height: " + RowHeight + "px'>");
                        SBstrScoringMeasurementList.Append("<td valign='middle' align='left' class='BottomBorder'>" + DT.Rows[i]["MeasurementID"] + "</td>");
                        SBstrScoringMeasurementList.Append("<td valign='middle' align='left' class='BottomBorder'>" + DT.Rows[i]["Name"] + "</td>");
                        SBstrScoringMeasurementList.Append("<td valign='middle' align='left' class='BottomBorder'>" + DT.Rows[i]["Date"] + "</td>");
                        SBstrScoringMeasurementList.Append("<td valign='middle' align='center' class='BottomBorder'>" + Math.Round((Convert.ToDecimal(DT.Rows[i]["CMYKPri"])), 0) + "&nbsp;</td>");
                        SBstrScoringMeasurementList.Append("<td valign='middle' align='center' class='BottomBorder'>" + Math.Round((Convert.ToDecimal(DT.Rows[i]["RGBSec"])), 0) + "&nbsp;</td>");
                        SBstrScoringMeasurementList.Append("<td valign='middle' align='center' class='BottomBorder'>" + Math.Round((Convert.ToDecimal(DT.Rows[i]["GrayBalance"])), 0) + "&nbsp;</td>");
                        SBstrScoringMeasurementList.Append("<td valign='middle' align='center' class='BottomBorder'>" + Math.Round((Convert.ToDecimal(DT.Rows[i]["Paper"])), 0) + "&nbsp;</td>");
                        SBstrScoringMeasurementList.Append("<td valign='middle' align='center' class='BottomBorder'>" + Math.Round((Convert.ToDecimal(DT.Rows[i]["Overall"])), 0) + "&nbsp;</td>");
                        SBstrScoringMeasurementList.Append("</tr>");
                    }

                    SBStr.Replace("###ScoringMeasurementList###", SBstrScoringMeasurementList.ToString());
                }
                else
                {
                    SBStr.Replace("###ScoringMeasurementList###", "");
                }
                //SBStr.Replace("###ResultSummary_DeltaImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_ResultSummary_Delta.Jpeg"));
                #endregion
            }


            #region Assign Server
            SBStr.Replace("##IMAGES##", strServer);
            SBStr.Replace("#csspath#", strServerStyleSheet);
            #endregion

            return SBStr;
        }
        public StringBuilder SetPDFContent(StringBuilder SBStr)
        {
            DataTable DT = new DataTable();
            try
            {
                //string currentBaseUrl = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority +
                //                    System.Web.HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";

                //** Add Styles Path ***//
                //string cssFullPath = this._WebURL + "Templates/style.css";
                string cssFullPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Templates/style.css");
                SBStr.Replace("#csspath#", cssFullPath);

                //** Add Images Path ***//
                string imagesPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Images") + "\\";
                SBStr.Replace("##LOGOIMAGES##", imagesPath);


                //** Add Images Path ***//
                //string imagesPath = this.BaseWebURL + "Images/";
                //string imagesPath = System.Web.HttpContext.Current.Server.MapPath("Images") + "\\"; ;
                SBStr.Replace("##IMAGES##", imagesPath);

                //** Add Content ***//
                SBStr.Replace("###ReportName###", this.ReportName);

                #region ==== Table = 0,  Job Detail  ====
                if (DS_Common.Tables[0] != null && DS_Common.Tables[0].Rows.Count > 0)
                {
                    DT = DS_Common.Tables[0];
                    string Media = Convert.ToString(DT.Rows[0]["Media"]).Trim();
                    string Colorant = Convert.ToString(DT.Rows[0]["Colorant"]).Trim();
                    string RipSettings = Convert.ToString(DT.Rows[0]["RipSettings"]).Trim();
                    string Location = Convert.ToString(DT.Rows[0]["CompanyName"]).Trim();

                    Location = (Location.Trim() != "" ? Location.Trim() + (Convert.ToString(DT.Rows[0]["BranchName"]).Trim() != "" ? " - " + Convert.ToString(DT.Rows[0]["BranchName"]) : "") : (Convert.ToString(DT.Rows[0]["BranchName"]).Trim() != "" ? Convert.ToString(DT.Rows[0]["BranchName"]) : ""));

                    SBStr.Replace("###JobName###", Convert.ToString(DT.Rows[0]["MeasurementName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["MeasurementName"]).Trim());
                    SBStr.Replace("###ProjectName###", Convert.ToString(DT.Rows[0]["ProjectName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["ProjectName"]).Trim());
                    SBStr.Replace("###Device###", Convert.ToString(DT.Rows[0]["DeviceName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["DeviceName"]).Trim());
                    SBStr.Replace("###TimeStamp###", Common.Utilities.GetDateByCultures(Convert.ToDateTime(DT.Rows[0]["MeasurementDate"])));
                    SBStr.Replace("###OptimizedDLP###", DLPName.Trim() == "" ? "&nbsp;" : DLPName.Trim());
                    SBStr.Replace("###PrintTarget###", Convert.ToString(DT.Rows[0]["ProfileName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["ProfileName"]).Trim());
                    SBStr.Replace("###Media###", Media.Trim() == "" ? "&nbsp;" : Media.Trim());
                    SBStr.Replace("###Colorant###", Colorant.Trim() == "" ? "&nbsp;" : Colorant.Trim());
                    SBStr.Replace("###RipSettings###", RipSettings.Trim() == "" ? "&nbsp;" : RipSettings.Trim());
                    SBStr.Replace("###Location###", Location.Trim() == "" ? "&nbsp;" : Location.Trim());
                    SBStr.Replace("###DeltaEFormula###", Convert.ToString(DT.Rows[0]["DeltaEFormula"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["DeltaEFormula"]).Trim());
                    SBStr.Replace("###OffsetNA###", Convert.ToString(DT.Rows[0]["LicenseTypeID"]).Trim() == "2" ? "(N/A)" : "&nbsp;"); //----Display "N/A" in offset printing data label for Sync 4D project types---//

                    if (ReportType == 16 || ReportType == 17 || ReportType == 18 || ReportType == 19)
                    {
                        if (IsValidation) //Only show Measurement Status in Validation mode in G7/ ISO / Default/ 4D
                        {
                            SBStr.Replace("###JobStatus###", Convert.ToString(DT.Rows[0]["JobStatus"]).Trim() == "" ? "<tr class='row-2'><td class='row-title'>Meas. Status:</td><td class='row-content'>&nbsp;</td></tr>" : (Convert.ToString(DT.Rows[0]["JobStatus"]).Contains("OK")) ? "<tr class='row-2'><td class='row-title'>Meas. Status:</td><td class='row-content'>" + Convert.ToString(DT.Rows[0]["JobStatus"]).Trim() + "</td></tr>" : "<tr class='row-2'><td class='row-title'  style='color:Red;'>Meas. Status:</td><td class='row-content'  style='color:Red;'>" + Convert.ToString(DT.Rows[0]["JobStatus"]).Trim() + "</td></tr>");
                            SBStr.Replace("###RunNo###", "&nbsp;");   //-----In validation report there is no run numbers-------//
                            SBStr.Replace("##ReportTitle##", "G7VAlignmentReportTitle");
                        }
                        else
                        {
                            SBStr.Replace("###JobStatus###", "&nbsp;");
                            SBStr.Replace("###RunNo###", "(" + (Convert.ToString(DT.Rows[0]["RunName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["RunName"]).Trim()) + ")");
                            SBStr.Replace("##ReportTitle##", "G7AlignmentReportTitle");
                        }
                        SBStr.Replace("###MetricPreset###", Convert.ToString(DT.Rows[0]["MetricTargetPresetName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["MetricTargetPresetName"]).Trim());
                        SBStr.Replace("###CompanyName###", Convert.ToString(DT.Rows[0]["CompanyName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["CompanyName"]).Trim());
                        SBStr.Replace("###BranchName###", Convert.ToString(DT.Rows[0]["BranchName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["BranchName"]).Trim());
                        SBStr.Replace("###PressName###", Convert.ToString(DT.Rows[0]["DeviceName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["DeviceName"]).Trim());
                        SBStr.Replace("###ProjectName###", Convert.ToString(DT.Rows[0]["ProjectName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["ProjectName"]).Trim());
                        SBStr.Replace("###CurrentDate###", System.DateTime.Now.ToString("dddd, MMMM dd, yyyy"));

                        if (ReportType == 16) //--------For G7 Report
                        {
                            SBStr.Replace("###MeasurementType###", Convert.ToString(DT.Rows[0]["MeasurementName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["MeasurementName"]).Trim().Contains("Averaging") == true ? "This report details an average of measurements." : "This report details a single measurement.");
                            SBStr.Replace("###Technician###", Convert.ToString(DT.Rows[0]["TechnicianName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["TechnicianName"]).Trim());
                            SBStr.Replace("###MeasurementDevice###", Convert.ToString(DT.Rows[0]["InstrumentType"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["InstrumentType"]).Trim());
                            SBStr.Replace("###SerialNumber###", Convert.ToString(DT.Rows[0]["SerialNumber"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["SerialNumber"]).Trim());
                            SBStr.Replace("###UVMode###", Convert.ToString(DT.Rows[0]["UVMode"]).Trim() == "" ? "&nbsp;" : 'M' + Convert.ToString(DT.Rows[0]["UVMode"]).Trim());
                            SBStr.Replace("###DLPChart###", Convert.ToString(DT.Rows[0]["ChartName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["ChartName"]).Trim());
                            SBStr.Replace("###OptiChart###", Convert.ToString(DT.Rows[0]["OPTChartName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["OPTChartName"]).Trim());
                            SBStr.Replace("###InkOrder###", Convert.ToString(DT.Rows[0]["InkOrder"]).Trim() == "" ? "&nbsp;" : Convert.ToString((Entity.EInkOrder)Convert.ToInt16(DT.Rows[0]["InkOrder"])).Trim());
                            SBStr.Replace("###Fountain###", Convert.ToString(DT.Rows[0]["FountainSolution"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["FountainSolution"]).Trim());
                            SBStr.Replace("###Blanket###", Convert.ToString(DT.Rows[0]["Blanket"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["Blanket"]).Trim());
                            SBStr.Replace("###BlanketAge###", Convert.ToString(DT.Rows[0]["BlanketAge"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["BlanketAge"]).Trim());
                            SBStr.Replace("###Screening###", Convert.ToString(DT.Rows[0]["Screening"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["Screening"]).Trim());
                            SBStr.Replace("###Coating###", Convert.ToString(DT.Rows[0]["Coating"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["Coating"]).Trim());
                            SBStr.Replace("###Notes###", Convert.ToString(DT.Rows[0]["Notes"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["Notes"]).Trim());
                        }
                    }
                }
                else
                {
                    SBStr.Replace("###JobName###", "&nbsp;");
                    SBStr.Replace("###ProjectName###", "&nbsp;");
                    SBStr.Replace("###Device###", "&nbsp;");
                    SBStr.Replace("###TimeStamp###", "&nbsp;");
                    SBStr.Replace("###OptimizedDLP###", "&nbsp;");
                    SBStr.Replace("###PrintTarget###", "&nbsp;");
                    SBStr.Replace("###Media###", "&nbsp;");
                    SBStr.Replace("###Colorant###", "&nbsp;");
                    SBStr.Replace("###RipSettings###", "&nbsp;");
                    SBStr.Replace("###Location###", "&nbsp;");
                    SBStr.Replace("###DeltaEFormula###", "&nbsp;");

                    //--------For G7 / ISO / Default Reports OR 4D Validation Scoring Report
                    if (ReportType == 16 || ReportType == 17 || ReportType == 18 || ReportType == 19)
                    {
                        if (IsValidation) //Only show Measurement STatus in Validation mode in G7/ ISO / Default/ 4D
                            SBStr.Replace("###JobStatus###", "<tr class='row-2'><td class='row-title'>Meas. Status:</td><td class='row-content'>&nbsp;</td></tr>");
                        else
                            SBStr.Replace("###JobStatus###", "&nbsp;");

                        SBStr.Replace("###MetricPreset###", "&nbsp;");
                        SBStr.Replace("###CompanyName###", "&nbsp;");
                        SBStr.Replace("###BranchName###", "&nbsp;");
                        SBStr.Replace("###PressName###", "&nbsp;");
                        SBStr.Replace("###ProjectName###", "&nbsp;");

                        if (ReportType == 16) //--------For G7 Report
                        {
                            SBStr.Replace("###Technician###", "&nbsp;");
                            SBStr.Replace("###MeasurementDevice###", "&nbsp;");
                            SBStr.Replace("###SerialNumber###", "&nbsp;");
                            SBStr.Replace("###UVMode###", "&nbsp;");
                            SBStr.Replace("###DLPChart###", "&nbsp;");
                            SBStr.Replace("###OptiChart###", "&nbsp;");
                            SBStr.Replace("###InkOrder###", "&nbsp;");
                            SBStr.Replace("###Fountain###", "&nbsp;");
                            SBStr.Replace("###Blanket###", "&nbsp;");
                            SBStr.Replace("###BlanketAge###", "&nbsp;");
                            SBStr.Replace("###Screening###", "&nbsp;");
                            SBStr.Replace("###Coating###", "&nbsp;");
                            SBStr.Replace("###Notes###", "&nbsp;");
                        }
                    }
                }


                #endregion

                // === Table 1 = Chart of Target Vs Actual AB Plot === //           
                #region ==== Target Vs Actual AB Plot ====
                SBStr.Replace("###TargetVersusActualAB###", this.chartContent.ABComparisionSpiderChart);
                #endregion

                #region ==== Summary Delta E Value Graph Image ====
                DT = DS_Common.Tables[3];
                StringBuilder SBstrDeltaEValue = new StringBuilder();
                if (DT != null && DT.Rows.Count > 0)
                {
                    string strStatusImg = "";

                    int RowHeight = 280 / (DT.Rows.Count + 2);
                    if (ReportType == 16) //--------For G7 Report
                    {
                        RowHeight = 12;

                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            SBstrDeltaEValue.Append("<tr class = 'Metadata-Table _idGenTableRowColumn-4'>");
                            SBstrDeltaEValue.Append("<td valign='middle' align='left' class='Metadata-Table " + DT.Rows[i]["ShortPatchType"] + "'> <p class='Body-Text-Bold'>" + DT.Rows[i]["ShortPatchType"] + "</p></td>");
                            SBstrDeltaEValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["TargetL"])) + "&nbsp;</p></td>");
                            SBstrDeltaEValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Targeta"])) + "&nbsp;</p></td>");
                            SBstrDeltaEValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Targetb"])) + "&nbsp;</p></td>");
                            SBstrDeltaEValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["ActualL"])) + "&nbsp;</p></td>");
                            SBstrDeltaEValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Actuala"])) + "&nbsp;</p></td>");
                            SBstrDeltaEValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Actualb"])) + "&nbsp;</p></td>");
                            SBstrDeltaEValue.Append("<td valign='middle' align='center' class='Metadata-Table Gray'> <p class='Body-Text-Center'>" + (DT.Rows[i]["ΔELimit"].ToString() == "0.00" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["ΔELimit"]))) + "&nbsp;</p></td>");
                            SBstrDeltaEValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["ΔE"])) + "&nbsp;</p></td>");
                            SBstrDeltaEValue.Append("</tr>");
                        }
                    }
                    else
                    {
                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            SBstrDeltaEValue.Append("<tr style='height: " + RowHeight + "px'>");
                            SBstrDeltaEValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                            SBstrDeltaEValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>" + Convert.ToString(DT.Rows[i]["PatchType"]) + "</td>");
                            //--------For ISO Reports put Tolerance
                            if (ReportType == 17 || ReportType == 18 || ReportType == 19)
                            {
                                if (DT.Rows[i]["ΔELimit"].ToString() != "0.00") //If no Tolerance value exists for particular Metric Target Preset then don't display "0.00" instead even.
                                {
                                    SBstrDeltaEValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["ΔELimit"])) + "&nbsp;</td>");
                                }
                                else
                                    SBstrDeltaEValue.Append("<td valign='middle' align='center' class='BottomBorder'> &nbsp;</td>");
                            }
                            else
                                SBstrDeltaEValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + "&nbsp;</td>");
                            SBstrDeltaEValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["ΔE"])) + "&nbsp;</td>");
                            SBstrDeltaEValue.Append("</tr>");
                        }
                    }
                    SBStr.Replace("###DeltaESummaryDetail###", SBstrDeltaEValue.ToString());
                }
                else
                {
                    SBStr.Replace("###DeltaESummaryDetail###", "");
                }
                SBStr.Replace("###ResultSummary_DeltaEImage###", this.chartContent.InkPaperPatchChart);
                #endregion

                if (ReportType != 19) //---Only if not 4D Validation Scoring Report
                {
                    // === Table 2 = Chart of Summary Delta Value Graph Image And Table Value Ploted === //          
                    #region ==== Summary Delta Value Graph Image ====

                    DT = DS_Common.Tables[2];

                    StringBuilder SBstrDeltaValue = new StringBuilder();
                    if (DT != null && DT.Rows.Count > 0)
                    {
                        string strStatusImg = "";
                        int RowHeight = 29;

                        //--------For ISO /Default Reports
                        if (ReportType == 17 || ReportType == 18)
                        {
                            for (int i = 0; i < DT.Rows.Count; i++)
                            {
                                if (Convert.ToDecimal(DT.Rows[i]["MeasuredValue"]) > Convert.ToDecimal(DT.Rows[i]["TargetValue"].ToString()))
                                {
                                    SBstrDeltaValue.Append("<tr style='height: " + RowHeight + "px; color:Red;'>");
                                }
                                else
                                {
                                    SBstrDeltaValue.Append("<tr style='height: " + RowHeight + "px'>");
                                }
                                SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>" + DT.Rows[i]["MetricSumName"] + "</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["TargetValue"])) + "&nbsp;</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["MeasuredValue"])) + "&nbsp;</td>");
                                SBstrDeltaValue.Append("</tr>");
                            }
                        }
                        else if (ReportType == 16) //--------For G7 Report
                        {
                            RowHeight = 12;
                            for (int i = 0; i < DT.Rows.Count; i++)
                            {
                                if ((i % 2) == 0) //----If even row then make background color green----//
                                    SBstrDeltaValue.Append("<tr class = 'Metadata-Table _idGenTableRowColumn-3' style='height: " + RowHeight + "px'>");
                                else             //------Else keep background color white-----//
                                    SBstrDeltaValue.Append("<tr class = 'Metadata-Table _idGenTableRowColumn-4' style='height: " + RowHeight + "px'>");

                                SBstrDeltaValue.Append("<td valign='middle' align='left' class='Metadata-Table Green' style='width: 160px;'> <p class='Body-Text-Bold'>" + DT.Rows[i]["MetricSumName"] + "</p></td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["TargetValue"])) + "&nbsp;</p></td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["MeasuredValue"])) + "&nbsp;</p></td>");
                                SBstrDeltaValue.Append("</tr>");
                            }

                        }
                        else
                        {
                            #region "AverageDE"
                            if (Convert.IsDBNull(DT.Rows[0]["Avg_ΔE"]))
                            {
                                SBstrDeltaValue.Append("<tr style='height: 27px'>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("<td valign='middle' class='BottomBorder' style='width: 160px;'>Average &Delta;" + From_ΔE + "</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("</tr>");
                            }
                            else
                            {
                                SBstrDeltaValue.Append("<tr style='height: 27px'>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Average &Delta;" + From_ΔE + "</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Avg_ΔE"])) + "&nbsp;</td>");
                                SBstrDeltaValue.Append("</tr>");
                            }
                            #endregion

                            #region "MaximumDE"
                            if (Convert.IsDBNull(DT.Rows[0]["Max_ΔE"]))
                            {
                                SBstrDeltaValue.Append("<tr style='height: 27px'>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Maximum &Delta;" + From_ΔE + "</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("</tr>");
                            }
                            else
                            {

                                SBstrDeltaValue.Append("<tr style='height: 27px'>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Maximum &Delta;" + From_ΔE + "</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Max_ΔE"])) + "&nbsp;</td>");
                                SBstrDeltaValue.Append("</tr>");
                            }
                            #endregion

                            #region "PrimaryDE"
                            if (Convert.IsDBNull(DT.Rows[0]["PriΔE"]))
                            {
                                SBstrDeltaValue.Append("<tr style='height: 27px'>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Max Primary &Delta;" + From_ΔE + "</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("</tr>");
                            }
                            else
                            {
                                SBstrDeltaValue.Append("<tr style='height: 27px'>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Max Primary &Delta;" + From_ΔE + "</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["PriΔE"])) + "&nbsp;</td>");
                                SBstrDeltaValue.Append("</tr>");
                            }
                            #endregion

                            #region "PrimaryDH"
                            if (Convert.IsDBNull(DT.Rows[0]["PriΔH"]))
                            {
                                SBstrDeltaValue.Append("<tr style='height: 27px'>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Max Primary &Delta;H</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("</tr>");
                            }
                            else
                            {

                                SBstrDeltaValue.Append("<tr style='height: 27px'>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Max Primary &Delta;H</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["PriΔH"])) + "&nbsp;</td>");
                                SBstrDeltaValue.Append("</tr>");
                            }
                            #endregion

                            #region "GrayAverageDE"
                            if (Convert.IsDBNull(DT.Rows[0]["GrayΔE"]))
                            {
                                SBstrDeltaValue.Append("<tr style='height: 27px'>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Gray Average &Delta;" + From_ΔE + "</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("</tr>");
                            }
                            else
                            {

                                SBstrDeltaValue.Append("<tr style='height: 27px'>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Gray Average &Delta;" + From_ΔE + "</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["GrayΔE"])) + "&nbsp;</td>");
                                SBstrDeltaValue.Append("</tr>");
                            }

                            #endregion

                            #region "dE_95pct_Patches"
                            if (Convert.IsDBNull(DT.Rows[0]["NinetyFiveΔE"]))
                            {
                                SBstrDeltaValue.Append("<tr style='height: 27px'>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>95th Percentile &Delta;" + From_ΔE + "</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("</tr>");
                            }
                            else
                            {

                                SBstrDeltaValue.Append("<tr style='height: 27px'>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>95th Percentile &Delta;" + From_ΔE + "</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                                SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["NinetyFiveΔE"])) + "&nbsp;</td>");
                                SBstrDeltaValue.Append("</tr>");
                            }
                            #endregion
                        }
                        SBStr.Replace("###DeltaSummaryDetail###", SBstrDeltaValue.ToString());
                    }
                    else
                    {
                        SBStr.Replace("###DeltaSummaryDetail###", "");
                    }
                    SBStr.Replace("###ResultSummary_DeltaImage###", this.chartContent.DeltaSummaryChart);
                    #endregion
                }

                if (ReportType == 17 || ReportType == 18 || ReportType == 19)   //----For ISO Opti / Validation Report & 4D ISO validation report + Default mode report OR 4D Validation Scoring report
                {
                    // === Table 4 = Chart of Paper L*A*B* === //           
                    #region ==== Paper  L*A*B* (Part 1)====
                    SBStr.Replace("###PaperLab1###", this.chartContent.InkPaperLAB1Chart);
                    #endregion

                    // === Table 5 = Chart of Paper L*A*B* === //           
                    #region ==== Paper  L*A*B* (Part 2)====
                    SBStr.Replace("###PaperLab2###", this.chartContent.InkPaperLAB2Chart);
                    #endregion

                    if (ReportType == 17) //For ISO Opti / Validation Report & 4D ISO validation report
                    {
                        // === Table 6 = Chart of Density === //           
                        #region ==== Density ====
                        SBStr.Replace("###DensityChart###", this.chartContent.InkPaperDansityChart);
                        #endregion

                        // === Table 7 = Chart of TVISpread === //           
                        #region ==== TVISpread ====
                        SBStr.Replace("###TVISpread###", this.chartContent.TVI_ISO_CMYK_Chart);
                        #endregion

                        // === Table 8 = Chart of TVISpread Data === //           
                        #region ==== TVISpreadData ====
                        DT = DS_Common.Tables[4];

                        StringBuilder SBstrTVIPatch = new StringBuilder();
                        StringBuilder SBstrTVIPatchVariation = new StringBuilder();


                        SBstrTVIPatch.Append("<table width='100%' border='0' align='center' cellpadding='0' cellspacing='0'>");
                        SBstrTVIPatch.Append("<tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'>");
                        SBstrTVIPatchVariation.Append("<tr class='row-1'>");

                        StringBuilder SBstrDeltaValue1 = new StringBuilder();
                        if (DT != null && DT.Rows.Count > 0)
                        {
                            //--------For ISO / 4D Validation ISO Reports


                            if (Convert.ToDecimal(DT.Rows[0]["Tone20"]) != 0)
                            {

                                SBstrTVIPatch.Append("<td valign='middle' align='left'  class='row-content1'> Tone20 </td>");

                                SBstrTVIPatchVariation.Append("<td valign='middle' align='left'  class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Tone20"])) + "&nbsp;</td>");

                            }
                            if (Convert.ToDecimal(DT.Rows[0]["Tone25"]) != 0)
                            {

                                SBstrTVIPatch.Append("<td valign='middle' align='left'  class='row-content1'> Tone25 </td>");

                                SBstrTVIPatchVariation.Append("<td valign='middle' align='left'   class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Tone25"])) + "&nbsp;</td>");

                            }
                            if (Convert.ToDecimal(DT.Rows[0]["Tone40"]) != 0)
                            {

                                SBstrTVIPatch.Append("<td valign='middle' align='left'  class='row-content1'> Tone40 </td>");

                                SBstrTVIPatchVariation.Append("<td valign='middle' align='left' class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Tone40"])) + "&nbsp;</td>");

                            }
                            if (Convert.ToDecimal(DT.Rows[0]["Tone50"]) != 0)
                            {

                                SBstrTVIPatch.Append("<td valign='middle' align='left'  class='row-content1'> Tone50 </td>");

                                SBstrTVIPatchVariation.Append("<td valign='middle' align='left'  class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Tone50"])) + "&nbsp;</td>");
                            }
                            if (Convert.ToDecimal(DT.Rows[0]["Tone60"]) != 0)
                            {
                                SBstrTVIPatch.Append("<td valign='middle' align='left' class='row-content1'> Tone60 </td>");

                                SBstrTVIPatchVariation.Append("<td valign='middle' align='left'  class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Tone60"])) + "&nbsp;</td>");

                            }
                            if (Convert.ToDecimal(DT.Rows[0]["Tone75"]) != 0)
                            {
                                SBstrTVIPatch.Append("<td valign='middle' align='left'  class='row-content1'> Tone75 </td>");

                                SBstrTVIPatchVariation.Append("<td valign='middle' align='left'  class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Tone75"])) + "&nbsp;</td>");

                            }
                            if (Convert.ToDecimal(DT.Rows[0]["Tone80"]) != 0)
                            {
                                SBstrTVIPatch.Append("<td valign='middle' align='left' class='row-content1'> Tone80 </td>");

                                SBstrTVIPatchVariation.Append("<td valign='middle' align='left'  class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Tone80"])) + "&nbsp;</td>");

                            }
                            SBstrTVIPatch.Append("</tr>");
                            SBstrTVIPatchVariation.Append("</tr>");

                        }
                        SBStr.Replace("###TVISpreadData###", SBstrTVIPatch.ToString() + SBstrTVIPatchVariation.ToString() + "</table>");
                        #endregion

                        // === Table 9 = Chart of CyanTVI === //           
                        #region ==== CyanTVI ====
                        SBStr.Replace("###CyanTVI###", this.chartContent.TVI_ISO_Cyan_Chart);
                        #endregion

                        // === Table 10 = Chart of MagentaTVI === //           
                        #region ==== MagentaTVI ====
                        SBStr.Replace("###MagentaTVI###", this.chartContent.TVI_ISO_Magenta_Chart);
                        #endregion

                        // === Table 11 = Chart of YellowTVI === //           
                        #region ==== YellowTVI ====
                        SBStr.Replace("###YellowTVI###", this.chartContent.TVI_ISO_Yellow_Chart);
                        #endregion

                        // === Table 12 = Chart of BlackTVI === //           
                        #region ==== BlackTVI ====
                        SBStr.Replace("###BlackTVI###", this.chartContent.TVI_ISO_Black_Chart);
                        #endregion

                        // === Table 13 = >Gray Balance L*a*b* === //           
                        #region ==== Gray LAB* ====
                        SBStr.Replace("###GrayL###", this.chartContent.LGrayBalanceChart);
                        SBStr.Replace("###GrayA###", this.chartContent.AGrayBalanceChart);
                        SBStr.Replace("###GrayB###", this.chartContent.BGrayBalanceChart);
                        #endregion
                    }
                    else if (ReportType == 18 || ReportType == 19) //For Default mode report OR 4D Validation Scoring Report
                    {
                        // === Table 13 = >Gray Balance L*a*b* === //           
                        #region ==== Gray LAB* ====
                        SBStr.Replace("###GrayL###", this.chartContent.LGrayBalanceChart);
                        SBStr.Replace("###GrayA###", this.chartContent.AGrayBalanceChart);
                        SBStr.Replace("###GrayB###", this.chartContent.BGrayBalanceChart);
                        #endregion
                    }
                }

                //--------For G7 / All Validation Report
                if (ReportType == 16)
                {
                    // === Table 4 = for Chart of Gray ΔCh === //           
                    #region ==== Gray ΔCh Chart ====
                    SBStr.Replace("###ResultSummary_DeltaChImage###", this.chartContent.GrayBalanceDeltaCH_Chart);
                    #endregion

                    // === Table 5 = for Chart of Black ΔL === //           
                    #region ==== Black ΔL Chart ====
                    SBStr.Replace("###ResultSummary_BlackDeltaLImage###", this.chartContent.BlackBalanceDeltaL_Chart);
                    #endregion

                    // === Table 6 = for Chart of Gray ΔL === //           
                    #region ==== Gray ΔL Chart ====
                    SBStr.Replace("###ResultSummary_GrayDeltaLImage###", this.chartContent.GrayBalanceDeltaL_Chart);
                    #endregion

                    // === Table 7 = for Chart of Gray Balance L* === //           
                    #region ==== Gray Balance L* Chart ====
                    SBStr.Replace("###ResultSummary_GrayLImage###", this.chartContent.LGrayBalanceChart);
                    #endregion

                    // === Table 8 = for Chart of Gray Balance a* === //           
                    #region ==== Gray Balance a* Chart ====
                    SBStr.Replace("###ResultSummary_GrayaImage###", this.chartContent.AGrayBalanceChart);
                    #endregion

                    // === Table 9 = for Chart of Gray Balance b* === //           
                    #region ==== Gray Balance b* Chart ====
                    SBStr.Replace("###ResultSummary_GraybImage###", this.chartContent.BGrayBalanceChart);
                    #endregion

                    // === Table 10 = for Chart of Black NPD === //           
                    #region ==== Black NPD Chart ====
                    SBStr.Replace("###ResultSummary_BlackNPDImage###", this.chartContent.BlackNPDChart);
                    #endregion

                    // === Table 11 = for Chart of Gray NPD === //           
                    #region ==== Gray NPD Chart ====
                    SBStr.Replace("###ResultSummary_GrayNPDImage###", this.chartContent.GrayNPDChart);
                    #endregion

                    //-------Extra items for new G7 formatted reports--------------//
                    SBStr.Replace("###DensityChart###", this.chartContent.InkPaperDansityChart);
                    SBStr.Replace("###ABComparison###", this.chartContent.ABActualTargetChart);
                    SBStr.Replace("###LComparison###", this.chartContent.LComparisionChart);
                    SBStr.Replace("###ResultSummary_TVIImage###", this.chartContent.TVI_Summary_Chart);

                    //------Region: for Density Data----//
                    DT = DS_Common.Tables[5];

                    StringBuilder SBstrDeltaValue = new StringBuilder();
                    StringBuilder SBstrDeltaValue1 = new StringBuilder();
                    StringBuilder SBstrDeltaValue2 = new StringBuilder();

                    if (DT != null && DT.Rows.Count > 0)
                    {
                        int RowHeight = 12;

                        SBstrDeltaValue.Append("<tr class = 'Metadata-Table _idGenTableRowColumn-3' style='height: " + RowHeight + "px'>");
                        SBstrDeltaValue1.Append("<tr class = 'Metadata-Table _idGenTableRowColumn-4' style='height: " + RowHeight + "px'>");
                        SBstrDeltaValue2.Append("<tr class = 'Metadata-Table _idGenTableRowColumn-3' style='height: " + RowHeight + "px'>");
                        SBstrDeltaValue.Append("<td valign='middle' align='left' class='Metadata-Table Green'> <p class='Body-Text-Bold'>Target</p></td>");
                        SBstrDeltaValue1.Append("<td valign='middle' align='left' class='Metadata-Table Green'> <p class='Body-Text-Bold'>Actual</p></td>");
                        SBstrDeltaValue2.Append("<td valign='middle' align='left' class='Metadata-Table Green'> <p class='Body-Text-Bold'>Previous</p></td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[0]["DensityLimit"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["DensityLimit"]))) + "&nbsp;</p></td>");
                        SBstrDeltaValue1.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[0]["Density"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["Density"]))) + "&nbsp;</p></td>");
                        SBstrDeltaValue2.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[0]["PreAvgDensity"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["PreAvgDensity"]))) + "&nbsp;</p></td>");

                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[1]["DensityLimit"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[1]["DensityLimit"]))) + "&nbsp;</p></td>");
                        SBstrDeltaValue1.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[1]["Density"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[1]["Density"]))) + "&nbsp;</p></td>");
                        SBstrDeltaValue2.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[1]["PreAvgDensity"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[1]["PreAvgDensity"]))) + "&nbsp;</p></td>");

                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[2]["DensityLimit"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[2]["DensityLimit"]))) + "&nbsp;</p></td>");
                        SBstrDeltaValue1.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[2]["Density"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[2]["Density"]))) + "&nbsp;</p></td>");
                        SBstrDeltaValue2.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[2]["PreAvgDensity"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[2]["PreAvgDensity"]))) + "&nbsp;</p></td>");

                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[3]["DensityLimit"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[3]["DensityLimit"]))) + "&nbsp;</p></td>");
                        SBstrDeltaValue1.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[3]["Density"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[3]["Density"]))) + "&nbsp;</p></td>");
                        SBstrDeltaValue2.Append("<td valign='middle' align='center' class='Metadata-Table Green'> <p class='Body-Text-Center'>" + (Convert.ToString(DT.Rows[3]["PreAvgDensity"]) == "" ? "N/A" : Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[3]["PreAvgDensity"]))) + "&nbsp;</p></td>");

                        SBstrDeltaValue.Append("</tr>");
                        SBstrDeltaValue1.Append("</tr>");
                        SBstrDeltaValue2.Append("</tr>");

                        SBstrDeltaValue.Append(SBstrDeltaValue1);
                        if (!IsValidation) //----Don't add Previous run row into Validation report since there is no previous run in validation
                        {
                            SBstrDeltaValue.Append(SBstrDeltaValue2);
                        }

                        SBStr.Replace("###DensitySummaryDetails###", SBstrDeltaValue.ToString());
                    }
                    //------End Region: for Density Data--------//

                    //------Region: for ∆E Primary Colorimetric Aims (Ink tab)-------//


                    //------End Region: for ∆E Primary Colorimetric Aims (Ink tab)-------//
                }

                if (ReportType == 19) //---Only if 4D Validation Scoring Report
                {
                    // === Table 4.1 = Display Metrics with Average Scoring on list === //          
                    #region ==== Metric Avg Scoring Graph Image ====

                    DT = DS_MetricAvgScoring.Tables[0];

                    StringBuilder SBstrMetricAvgScoring = new StringBuilder();
                    decimal CMYKPrimaries = 0;
                    decimal RGBSecondaries = 0;
                    decimal dt_HGray = 0;
                    decimal dt_EPaper = 0;

                    if (DT != null && DT.Rows.Count > 0)
                    {
                        string strStatusImg = "";
                        int RowHeight = 29;

                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            SBstrMetricAvgScoring.Append("<tr style='height: " + RowHeight + "px'>");
                            SBstrMetricAvgScoring.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                            SBstrMetricAvgScoring.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>" + DT.Rows[i]["Color"] + "</td>");
                            SBstrMetricAvgScoring.Append("<td valign='middle' align='center' class='BottomBorder'>" + Math.Round((Convert.ToDecimal(DT.Rows[i]["AvgScore"])), 0) + "&nbsp;</td>");
                            SBstrMetricAvgScoring.Append("</tr>");

                            if (i >= 0 && i <= 3)
                            {
                                CMYKPrimaries = CMYKPrimaries + Convert.ToDecimal(DT.Rows[i]["AvgScore"]);
                            }
                            else if (i >= 4 && i <= 6)
                            {
                                RGBSecondaries = RGBSecondaries + Convert.ToDecimal(DT.Rows[i]["AvgScore"]);
                            }
                            else if (i >= 7 && i <= 9)
                            {
                                dt_HGray = dt_HGray + Convert.ToDecimal(DT.Rows[i]["AvgScore"]);
                            }
                            else if (i == 10)
                            {
                                dt_EPaper = dt_EPaper + Convert.ToDecimal(DT.Rows[i]["AvgScore"]);
                            }
                        }
                        CMYKPrimaries = ((CMYKPrimaries) / 4);
                        RGBSecondaries = ((RGBSecondaries) / 3);
                        dt_HGray = ((dt_HGray) / 3);

                        SBStr.Replace("###MetricAvgScoringDetail###", SBstrMetricAvgScoring.ToString());
                        SBStr.Replace("###AvgScoringCMYKPrimaries###", Math.Round(CMYKPrimaries, 0).ToString());
                        SBStr.Replace("###AvgScoringRGBSecondaries###", Math.Round(RGBSecondaries, 0).ToString());
                        SBStr.Replace("###AvgScoringΔHGray###", Math.Round(dt_HGray, 0).ToString());
                        SBStr.Replace("###AvgScoringPaperΔE###", Math.Round(dt_EPaper, 0).ToString());
                        SBStr.Replace("###TotalScore###", Math.Round(((CMYKPrimaries + RGBSecondaries + dt_HGray + dt_EPaper) / 4), 0).ToString());
                    }
                    else
                    {
                        SBStr.Replace("###MetricAvgScoringDetail###", "");
                        SBStr.Replace("###AvgScoringCMYKPrimaries###", "");
                        SBStr.Replace("###AvgScoringRGBSecondaries###", "");
                        SBStr.Replace("###AvgScoringΔHGray###", "");
                        SBStr.Replace("###AvgScoringPaperΔE###", "");
                        SBStr.Replace("###TotalScore###", "");
                    }

                    // === Table 4.2 = Display Scoring Measurement List === //          

                    DT = DS_ScoringMeasurementList.Tables[0];

                    StringBuilder SBstrScoringMeasurementList = new StringBuilder();
                    if (DT != null && DT.Rows.Count > 0)
                    {
                        int RowHeight = 29;

                        for (int i = 0; i < DT.Rows.Count; i++)
                        {
                            SBstrScoringMeasurementList.Append("<tr style='height: " + RowHeight + "px'>");
                            SBstrScoringMeasurementList.Append("<td valign='middle' align='left' class='BottomBorder'>" + DT.Rows[i]["MeasurementID"] + "</td>");
                            SBstrScoringMeasurementList.Append("<td valign='middle' align='left' class='BottomBorder'>" + DT.Rows[i]["Name"] + "</td>");
                            SBstrScoringMeasurementList.Append("<td valign='middle' align='left' class='BottomBorder'>" + DT.Rows[i]["Date"] + "</td>");
                            SBstrScoringMeasurementList.Append("<td valign='middle' align='center' class='BottomBorder'>" + Math.Round((Convert.ToDecimal(DT.Rows[i]["CMYKPri"])), 0) + "&nbsp;</td>");
                            SBstrScoringMeasurementList.Append("<td valign='middle' align='center' class='BottomBorder'>" + Math.Round((Convert.ToDecimal(DT.Rows[i]["RGBSec"])), 0) + "&nbsp;</td>");
                            SBstrScoringMeasurementList.Append("<td valign='middle' align='center' class='BottomBorder'>" + Math.Round((Convert.ToDecimal(DT.Rows[i]["GrayBalance"])), 0) + "&nbsp;</td>");
                            SBstrScoringMeasurementList.Append("<td valign='middle' align='center' class='BottomBorder'>" + Math.Round((Convert.ToDecimal(DT.Rows[i]["Paper"])), 0) + "&nbsp;</td>");
                            SBstrScoringMeasurementList.Append("<td valign='middle' align='center' class='BottomBorder'>" + Math.Round((Convert.ToDecimal(DT.Rows[i]["Overall"])), 0) + "&nbsp;</td>");
                            SBstrScoringMeasurementList.Append("</tr>");
                        }

                        SBStr.Replace("###ScoringMeasurementList###", SBstrScoringMeasurementList.ToString());
                    }
                    else
                    {
                        SBStr.Replace("###ScoringMeasurementList###", "");
                    }
                    //SBStr.Replace("###ResultSummary_DeltaImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_ResultSummary_Delta.Jpeg"));
                    #endregion
                }


                //SBStr.Replace("###LComparison###", this.chartContent.LComparisionChart);
                //SBStr.Replace("###GrayL###", this.chartContent.LGrayBalanceChart);
                //SBStr.Replace("###GrayA###", this.chartContent.AGrayBalanceChart);
                //SBStr.Replace("###GrayB###", this.chartContent.BGrayBalanceChart);

            }
            catch (Exception ex)
            {
                throw;
            }

            return SBStr;
        }
        #endregion

        public void GenerateSpotPatchDetailsReport()
        {
            try
            {
                //========== Object of PDF ==========//
                Doc theDoc = new Doc();

                //========== Reads the template ==========//
                StreamReader fp;

                //========== The page ID Default is 1 ==========//
                int theID;

                //========== Adds New Page IN PDF ==========//
                theDoc.Page = theDoc.AddPage();
                //========== Adds a counter to Page ==========//
                theCount++;

                string FileName = "Templates/kendoPrepressSpotProjectPatch.htm";
                string FilePath = this._WebURL + FileName;

                //========== Reads the template ==========//
                fp = File.OpenText(System.Web.Hosting.HostingEnvironment.MapPath("~/" + FileName));
                StringBuilder SBStr = new StringBuilder(fp.ReadToEnd());
                fp.Close();
                //** Add Styles Path ***//
                string cssFullPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Templates/style.css");
                SBStr.Replace("#csspath#", cssFullPath);

                //** Add Images Path ***//
                string imagesPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Images") + "\\";
                SBStr.Replace("##LOGOIMAGES##", imagesPath);


                //** Add Images Path ***//
                SBStr.Replace("##IMAGES##", imagesPath);


                StringBuilder PatchStr = new StringBuilder();
                PatchStr.Append("<table width='100%' style='text-align:left;'><tr><th>SpotID</th><th>SpotName</th><th>HTMLColor</th>");
                int measurementID = dtPatchDetails != null && dtPatchDetails.Rows.Count > 0 ? Convert.ToInt32(dtPatchDetails.Rows[0]["MeasurementID"]) : 0;
                if (measurementID == 0)
                    PatchStr.Append("<th>pΔL</th><th>pΔa</th><th>pΔb</th></tr>");
                else
                    PatchStr.Append("<th>ΔE_T_P</th><th>ΔE_M_T</th><th>ΔE_M_P</th></tr>");
                foreach (DataRow row in dtPatchDetails.Rows)
                {
                    PatchStr.Append("<tr><td>" + Convert.ToString(row["SpotID"]) + "</td>");
                    PatchStr.Append("<td>" + Convert.ToString(row["SpotName"]) + " </td>");
                    PatchStr.Append("<td><span id='sp' style='display:inline-block;background-color:" + Convert.ToString(row["HTMLColor"]) + ";height:14px;width:68px;'></span></td>");
                    if (measurementID == 0)
                        PatchStr.Append("<td>" + Convert.ToString(row["ΔL"]) + "</td><td>" + Convert.ToString(row["Δa"]) + "</td><td>" + Convert.ToString(row["Δb"]) + "</td>");
                    else
                        PatchStr.Append("<td>" + Convert.ToString(row["ΔE_T_P"]) + "</td><td>" + Convert.ToString(row["ΔE_M_T"]) + "</td><td>" + Convert.ToString(row["ΔE_M_P"]) + "</td>");
                    PatchStr.Append("</tr>");
                }
                PatchStr.Append("</table>");

                SBStr.Replace("###SpotProjectPatchDetails###", PatchStr.ToString());

                //========== Assign HTML to theID ==========//
                //1 : str = The html content.
                //3 : width(612) = The dimension of the page to fix in A4 size paper.
                theID = theDoc.AddImageHtml(SBStr.ToString(), true, 700, false);

                //========== Leave 30 Px on top of next page ==========//
                theDoc.Rect.Inset(0, 1);

                //========== Loops till the paging ==========//
                while (true)
                {
                    //========== Create A chanin for a pdf with multiple page ==========//
                    if (!theDoc.Chainable(theID))
                        break;

                    theDoc.Page = theDoc.AddPage();
                    theCount++;
                    theID = theDoc.AddImageToChain(theID);
                }

                //========== Reduce the top to start the next page from ==========//
                theDoc.Rect.Inset(0, -1);

                string pth = PDFPath + PDF_NAME;

                // === If same named pdf file is already available then remove === //
                if (System.IO.File.Exists(pth))
                    System.IO.File.Delete(pth);

                theDoc.Save(pth);
                theDoc.Clear();
                theDoc.Dispose();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }


}
