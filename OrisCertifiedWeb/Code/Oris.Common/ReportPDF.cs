using Formulas;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using WebSupergoo.ABCpdf11;


namespace Oris.Common
{
    public class ReportPDF
    {
        #region Declaration

        //string strServer = System.Web.HttpContext.Current.Server.MapPath("Images") + "\\";
        string strServer = "";
        string currentBaseUrl = "";
        int _ReportType = 0;
        string _PDFPath;
        string _PDF_NAME;
        string CSSPath = "";
        int theCount = 0;
        private DataSet _DS_Common;
        private DataGrid _Dg;

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

        public DataGrid Dg
        {
            get { return _Dg; }
            set { _Dg = value; }
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

            currentBaseUrl = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority +
                                System.Web.HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";

            CSSPath = currentBaseUrl + "Templates/style.css";
            strServer = currentBaseUrl + "Images/";
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
                theDoc.HtmlOptions.Engine = EngineType.MSHtml;

                //========== Reads the template ==========//
                StreamReader fp;

                //========== The rectangle for the content on pdf ==========//
                theDoc.Rect.Left = 0;
                theDoc.Rect.Bottom = 30;
                theDoc.Rect.Width = 610;
                theDoc.Rect.Height = 770;

                //========== The page ID Default is 1 ==========//
                int theID;

                switch (ReportType)
                {
                    #region Case 1 : Branch Summery

                    case (int)Enums.enumReportType.BranchSummaryReport:

                        //========== Adds New Page IN PDF ==========//
                        theDoc.Page = theDoc.AddPage();

                        //========== Adds a counter to Page ==========//
                        theCount++;

                        //========== Read Template ==========//
                        FileName = "Templates/BranchSummaryReport.html";
                        fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                        str = TextReturn_BranchSummaryReport(fp.ReadToEnd());
                        fp.Close();

                        //========== Assign HTML to theID ==========//
                        //1 : str = The html content.
                        //3 : width(612) = The dimension of the page to fix in A4 size paper.
                        theID = theDoc.AddImageHtml(str, true, 612, false);

                        //========== Leave 30 Px on top of next page ==========//
                        theDoc.Rect.Inset(0, 30);

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
                        theDoc.Rect.Inset(0, -30);

                        break;
                    #endregion

                    #region Case 2 : Device Report
                    case (int)Enums.enumReportType.DeviceReport:

                        //========== Adds New Page IN PDF ==========//
                        theDoc.Page = theDoc.AddPage();

                        //========== Adds a counter to Page ==========//
                        theCount++;

                        //========== Read Template ==========//
                        FileName = "Templates/DeviceReport.html";
                        fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                        str = TextReturn_DeviceReport(fp.ReadToEnd());
                        fp.Close();

                        //========== Assign HTML to theID ==========//
                        //1 : str = The html content.
                        //3 : width(612) = The dimension of the page to fix in A4 size paper.
                        theID = theDoc.AddImageHtml(str, true, 612, false);

                        //========== Leave 30 Px on top of next page ==========//
                        theDoc.Rect.Inset(0, 30);

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
                        theDoc.Rect.Inset(0, -30);

                        break;
                    #endregion

                    #region Case 3 : Shift Variability Report
                    case (int)Enums.enumReportType.ShiftVariabilityReport:

                        #region Page 1 settings

                        //========== Adds New Page IN PDF ==========//
                        theDoc.Page = theDoc.AddPage();

                        //========== Adds a counter to Page ==========//
                        theCount++;

                        //========== Assign Template Name ==========//
                        FileName = "Templates/ShiftVariabilityReport.html";

                        //========== Read Template ==========//
                        fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                        str = TextReturn_ShiftVariabilityReport(fp.ReadToEnd());
                        fp.Close();

                        //========== Assign HTML to theID ==========//
                        //1 : str = The html content.
                        //3 : width(612) = The dimension of the page to fix in A4 size paper.
                        theID = theDoc.AddImageHtml(str, true, 612, false);

                        //========== Leave 30 Px on top of next page ==========//
                        theDoc.Rect.Inset(0, 30);

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
                        theDoc.Rect.Inset(0, -30);
                        #endregion

                        #region Page 2 settings
                        //========== Adds New Page IN PDF ==========//
                        theDoc.Page = theDoc.AddPage();

                        //========== Adds a counter to Page ==========//
                        theCount++;

                        //========== Read Template ==========//
                        FileName = "Templates/ShiftVariabilityReport_1.html";
                        fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                        str = TextReturn_ShiftVariabilityReport_1(fp.ReadToEnd());
                        fp.Close();

                        //========== Assign HTML to theID ==========//
                        //1 : str = The html content.
                        //3 : width(612) = The dimension of the page to fix in A4 size paper.
                        theID = theDoc.AddImageHtml(str, true, 612, false);

                        //========== Leave 30 Px on top of next page ==========//
                        theDoc.Rect.Inset(0, 30);

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
                        theDoc.Rect.Inset(0, -30);
                        #endregion

                        #region Page 3 settings
                        //========== Adds New Page IN PDF ==========//
                        theDoc.Page = theDoc.AddPage();

                        //========== Adds a counter to Page ==========//
                        theCount++;

                        //========== Read Template ==========//
                        FileName = "Templates/ShiftVariabilityReport_2.html";
                        fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                        str = TextReturn_ShiftVariabilityReport_2(fp.ReadToEnd());
                        fp.Close();

                        //========== Assign HTML to theID ==========//
                        //1 : str = The html content.
                        //3 : width(612) = The dimension of the page to fix in A4 size paper.
                        theID = theDoc.AddImageHtml(str, true, 612, false);

                        //========== Leave 30 Px on top of next page ==========//
                        theDoc.Rect.Inset(0, 30);

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
                        theDoc.Rect.Inset(0, -30);
                        #endregion

                        break;
                    #endregion

                    #region Case 4 : Shift Variability Report
                    case (int)Enums.enumReportType.DeviceVariabilityReport:

                        #region Page 1 Settings
                        //========== Adds New Page IN PDF ==========//
                        theDoc.Page = theDoc.AddPage();

                        //========== Adds a counter to Page ==========//
                        theCount++;

                        //========== Read Template ==========//
                        FileName = "Templates/DeviceVariabilityReport.html";
                        fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                        str = TextReturn_DeviceVariabilityReport(fp.ReadToEnd());
                        fp.Close();

                        //========== Assign HTML to theID ==========//
                        //1 : str = The html content.
                        //3 : width(612) = The dimension of the page to fix in A4 size paper.
                        theID = theDoc.AddImageHtml(str, true, 612, false);

                        //========== Leave 30 Px on top of next page ==========//
                        theDoc.Rect.Inset(0, 30);

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
                        theDoc.Rect.Inset(0, -30);
                        #endregion

                        #region Page 2 Settings
                        //========== Adds New Page IN PDF ==========//
                        theDoc.Page = theDoc.AddPage();

                        //========== Adds a counter to Page ==========//
                        theCount++;

                        //========== Read Template ==========//
                        FileName = "Templates/DeviceVariabilityReport_1.html";
                        fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                        str = TextReturn_DeviceVariabilityReport_1(fp.ReadToEnd());
                        fp.Close();

                        //========== Assign HTML to theID ==========//
                        //1 : str = The html content.
                        //3 : width(612) = The dimension of the page to fix in A4 size paper.
                        theID = theDoc.AddImageHtml(str, true, 612, false);

                        //========== Leave 30 Px on top of next page ==========//
                        theDoc.Rect.Inset(0, 30);

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
                        theDoc.Rect.Inset(0, -30);
                        #endregion

                        #region Page 3 Settings
                        //========== Adds New Page IN PDF ==========//
                        theDoc.Page = theDoc.AddPage();

                        //========== Adds a counter to Page ==========//
                        theCount++;

                        //========== Read Template ==========//
                        FileName = "Templates/DeviceVariabilityReport_2.html";
                        fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                        str = TextReturn_DeviceVariabilityReport_2(fp.ReadToEnd());
                        fp.Close();

                        //========== Assign HTML to theID ==========//
                        //1 : str = The html content.
                        //3 : width(612) = The dimension of the page to fix in A4 size paper.
                        theID = theDoc.AddImageHtml(str, true, 612, false);

                        //========== Leave 30 Px on top of next page ==========//
                        theDoc.Rect.Inset(0, 30);

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
                        theDoc.Rect.Inset(0, -30);
                        #endregion

                        break;
                    #endregion

                    #region Case 5 : Color Detail Report
                    case (int)Enums.enumReportType.ColorDetailReport:

                        //========== Adds New Page IN PDF ==========//
                        theDoc.Page = theDoc.AddPage();

                        //========== Adds a counter to Page ==========//
                        theCount++;

                        //========== Read Template ==========//
                        FileName = "Templates/ColorDetailReport.html";
                        fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                        str = TextReturn_ColorDetailReport(fp.ReadToEnd());
                        fp.Close();

                        //========== Assign HTML to theID ==========//
                        //1 : str = The html content.
                        //3 : width(612) = The dimension of the page to fix in A4 size paper.
                        theID = theDoc.AddImageHtml(str, true, 612, false);

                        //========== Leave 30 Px on top of next page ==========//
                        theDoc.Rect.Inset(0, 30);

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
                        theDoc.Rect.Inset(0, -30);
                        break;
                    #endregion

                    #region Case 6 : Patch Detail Report
                    case (int)Enums.enumReportType.PatchDetailReport:

                        //======SET PAGE FORMATTING===================== //
                        theDoc.HtmlOptions.Engine = EngineType.Gecko;

                        //========== Adds New Page IN PDF ==========//
                        theDoc.Page = theDoc.AddPage();

                        //========== Adds a counter to Page ==========//
                        theCount++;

                        //========== Read Template ==========//
                        FileName = "Templates/PatchReport.htm";
                        fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                        str = TextReturn_PatchReport(fp.ReadToEnd());
                        fp.Close();

                        //========== Assign HTML to theID ==========//
                        //1 : str = The html content.
                        //3 : width(612) = The dimension of the page to fix in A4 size paper.
                        theID = theDoc.AddImageHtml(str, true, 612, false);

                        //========== Leave 30 Px on top of next page ==========//
                        theDoc.Rect.Inset(0, 30);

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
                        theDoc.Rect.Inset(0, -30);
                        break;
                    #endregion

                    #region Case 7 : Productivity Report
                    case (int)Enums.enumReportType.ProductivityReport:

                        //========== Adds New Page IN PDF ==========//
                        theDoc.Page = theDoc.AddPage();

                        //========== Adds a counter to Page ==========//
                        theCount++;

                        //========== Read Template ==========//
                        FileName = "Templates/ProductivityReport.html";
                        fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                        str = TextReturn_ProductivityReport(fp.ReadToEnd());
                        fp.Close();

                        //========== Assign HTML to theID ==========//
                        //1 : str = The html content.
                        //3 : width(612) = The dimension of the page to fix in A4 size paper.
                        theID = theDoc.AddImageHtml(str, true, 612, false);

                        //========== Leave 30 Px on top of next page ==========//
                        theDoc.Rect.Inset(0, 30);

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
                        theDoc.Rect.Inset(0, -30);
                        break;
                    #endregion

                    #region Case 8 : Performace Report

                    case (int)Enums.enumReportType.ReportPerformanceSummary:

                        //========== Adds New Page IN PDF ==========//
                        theDoc.Page = theDoc.AddPage();

                        //========== Adds a counter to Page ==========//
                        theCount++;

                        //========== Read Template ==========//
                        FileName = "Templates/ReportPerformanceSummary.html";
                        fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                        str = TextReturn_ReportPerformanceSummary(fp.ReadToEnd());
                        fp.Close();

                        //========== Assign HTML to theID ==========//
                        //1 : str = The html content.
                        //3 : width(612) = The dimension of the page to fix in A4 size paper.
                        theID = theDoc.AddImageHtml(str, true, 700, false);

                        //========== Leave 30 Px on top of next page ==========//
                        theDoc.Rect.Inset(0, 30);

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
                        theDoc.Rect.Inset(0, -30);

                        break;
                    #endregion

                    #region Case 9 : Target Measurement Report Report

                    case (int)Enums.enumReportType.ReportTargetMeasurement:

                        //========== Adds New Page IN PDF ==========//
                        theDoc.Page = theDoc.AddPage();

                        //========== Adds a counter to Page ==========//
                        theCount++;

                        //========== Read Template ==========//
                        FileName = "Templates/TargetMeasurementReport.htm";
                        fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                        str = TextReturn_ReportTargetMeasurement(fp.ReadToEnd());
                        fp.Close();

                        //========== Assign HTML to theID ==========//
                        //1 : str = The html content.
                        //3 : width(612) = The dimension of the page to fix in A4 size paper.
                        theID = theDoc.AddImageHtml(str, true, 700, false);

                        //========== Leave 30 Px on top of next page ==========//
                        theDoc.Rect.Inset(0, 30);

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
                        theDoc.Rect.Inset(0, -30);

                        break;
                    #endregion                        

                    #region Case 10 : Job Run Certification Process Report

                    case (int)Enums.enumReportType.ReportJobRunCertificationProcess:

                        //========== Adds New Page IN PDF ==========//
                        theDoc.Page = theDoc.AddPage();

                        //========== Adds a counter to Page ==========//
                        theCount++;

                        //========== Read Template ==========//
                        FileName = "Templates/JobRunCertificationProcess.htm";
                        fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                        str = TextReturn_ReportJobRunCertificationProcess(fp.ReadToEnd());
                        fp.Close();

                        //========== Assign HTML to theID ==========//
                        //1 : str = The html content.
                        //3 : width(612) = The dimension of the page to fix in A4 size paper.
                        theID = theDoc.AddImageHtml(str, true, 700, false);

                        //========== Leave 30 Px on top of next page ==========//
                        theDoc.Rect.Inset(0, 30);

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
                        theDoc.Rect.Inset(0, -30);

                        break;
                    #endregion

                    #region Case 11 : Job Run Certification Non Process Report

                    case (int)Enums.enumReportType.ReportJobRunCertificationNonProcess:

                        //========== Adds New Page IN PDF ==========//
                        theDoc.Page = theDoc.AddPage();

                        //========== Adds a counter to Page ==========//
                        theCount++;

                        //========== Read Template ==========//
                        FileName = "Templates/JobRunCertificationNonProcess.htm";
                        fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                        str = TextReturn_ReportJobRunCertificationNonProcess(fp.ReadToEnd());
                        fp.Close();

                        //========== Assign HTML to theID ==========//
                        //1 : str = The html content.
                        //3 : width(612) = The dimension of the page to fix in A4 size paper.
                        theID = theDoc.AddImageHtml(str, true, 700, false);

                        //========== Leave 30 Px on top of next page ==========//
                        theDoc.Rect.Inset(0, 30);

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
                        theDoc.Rect.Inset(0, -30);

                        break;
                    #endregion

                    #region Case 12 : Press Make Ready ReportPressMakeReady

                    case (int)Enums.enumReportType.ReportPressMakeReady:

                        //========== Adds New Page IN PDF ==========//
                        theDoc.Page = theDoc.AddPage();

                        //========== Adds a counter to Page ==========//
                        theCount++;

                        //========== Read Template ==========//
                        FileName = "Templates/PressMakeReadyReport.htm";
                        fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                        str = TextReturn_ReportMakeReady(fp.ReadToEnd());
                        fp.Close();

                        //========== Assign HTML to theID ==========//
                        //1 : str = The html content.
                        //3 : width(612) = The dimension of the page to fix in A4 size paper.
                        theID = theDoc.AddImageHtml(str, true, 700, false);

                        //========== Leave 30 Px on top of next page ==========//
                        theDoc.Rect.Inset(0, 30);

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
                        theDoc.Rect.Inset(0, -30);

                        break;
                    #endregion

                    #region Case 13 : Prepress Single Measurement ReportSingleMeasurement
                    case (int)Enums.enumReportType.ReportSingleMeasurement:
                        //======SET PAGE FORMATTING===================== //
                        theDoc.HtmlOptions.Engine = EngineType.Gecko;

                        //========== Adds New Page IN PDF ==========//
                        theDoc.Page = theDoc.AddPage();

                        //========== Adds a counter to Page ==========//
                        theCount++;

                        //========== Read Template ==========//
                        FileName = "Templates/PrepressSingleMeasurement.htm";
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


                        break;
                        #endregion
                }

                if (FileName.Trim() == "")
                {
                    throw new Exception("Please Enter Report Type.");
                }

                #region Add Footer

                theDoc.Rect.String = "-10 0 622 30";

                //theDoc.Rect.String = "30 0 580 30";
                theDoc.HPos = 0.0;
                theDoc.VPos = 0.0;
                theDoc.Color.String = "255 255 255";
                theDoc.FontSize = 8;
                for (int i = 1; i <= theCount; i++)
                {
                    theDoc.HPos = 0.0;
                    theDoc.VPos = 0.0;
                    theDoc.PageNumber = i;

                    str = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> " +
                        "<tr style=\"height:25px\"><td align=\"center\" valign=\"middle\" bgcolor=\"#C1C1C1\" style=\"color:#000000; font-weight:bold ; font-size:8px; font-family:Tahoma, Arial, Helvetica, sans-serif;\" width=\"150px\" >" + DateTime.Now.ToLongDateString() + "</td> " +
                        "<td align=\"center\" valign=\"middle\" bgcolor=\"#C1C1C1\" style=\"color:#000000; font-weight:bold ; font-size:8px; font-family:Tahoma, Arial, Helvetica, sans-serif; \">&copy; CGS " + DateTime.Now.Year.ToString() + "</td> " +
                        "<td width=\"112px\" align=\"center\" valign=\"middle\" bgcolor=\"#5A5A5A\" style=\"color:#CCCCCC; font-weight:bold ; font-size:8px; font-family:Tahoma, Arial, Helvetica, sans-serif; \">Page " + i.ToString() + " of " + theCount.ToString() + " </td></tr></table>";
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
                throw Exc;
            }
        }
        #endregion

        #region Report 1 : Branch Summery
        private string TextReturn_BranchSummaryReport(string str)
        {
            str = str.Replace("#csspath#", CSSPath);

            if (DS_Common.Tables["BasicInfo"] != null && DS_Common.Tables["BasicInfo"].Rows.Count > 0)
            {

                str = str.Replace("##CompanyName##", DS_Common.Tables["BasicInfo"].Rows[0]["CompanyName_"].ToString().Trim());
                str = str.Replace("##BranchName##", DS_Common.Tables["BasicInfo"].Rows[0]["BranchName_"].ToString().Trim());
                str = str.Replace("##DateRange##", DS_Common.Tables["BasicInfo"].Rows[0]["DateRange_"].ToString().Trim());
            }
            else
            {
                str = str.Replace("##CompanyName##", "");
                str = str.Replace("##BranchName##", "");
                str = str.Replace("##DateRange##", "");
            }

            string HTMLstr = "";
            string HTMLstr1 = "";
            string HTMLstr2 = "";

            #region ====  Search Criteria  ====

            if (DS_Common.Tables["SearchCriteria"] != null)
            {

                for (int i = 0; i < DS_Common.Tables["SearchCriteria"].Rows.Count; i++)
                {
                    if ((i % 2) == 0)
                    {
                        HTMLstr1 += "<tr class=\"GRIDEVEN\" > ";
                        HTMLstr2 += "<tr class=\"GRIDEVEN\" > ";
                    }
                    else
                    {
                        HTMLstr1 += "<tr class=\"GRIDODD\" > ";
                        HTMLstr2 += "<tr class=\"GRIDODD\" > ";
                    }

                    HTMLstr1 += "<td align=\"left\" class=\"BOLD1\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["Parameter_"].ToString() + "</td> ";
                    HTMLstr1 += "<td align=\"center\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["Result_"].ToString() + "</td> ";
                    HTMLstr1 += "</tr>";

                    HTMLstr2 += "<td class=\"BOLD1\" align=\"center\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["SearchField_"].ToString() + "</td>";
                    HTMLstr2 += "<td align=\"center\" class=\"BOLD1\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["Operator_"].ToString() + "</td>";
                    HTMLstr2 += "<td align=\"center\" class=\"BOLD1\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["Criteria_"].ToString() + "</td>";
                    HTMLstr2 += "</tr>";
                }

                str = str.Replace("##SearchCriteria1##", HTMLstr1);
                str = str.Replace("##SearchCriteria2##", HTMLstr2);
            }
            else
            {
                str = str.Replace("##SearchCriteria##", " ");

            }
            #endregion

            #region ====  Color Specification  ====

            if (DS_Common.Tables["ColorSpecification"] != null)
            {
                HTMLstr = "";

                for (int i = 0; i < DS_Common.Tables["ColorSpecification"].Rows.Count; i++)
                {
                    if ((i % 2) == 0)
                    {
                        HTMLstr += "<tr class=\"GRIDEVEN\" > ";
                    }
                    else
                    {
                        HTMLstr += "<tr class=\"GRIDODD\" > ";
                    }

                    HTMLstr += "<td align=\"left\">" + DS_Common.Tables["ColorSpecification"].Rows[i]["CPDeviceDesc"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"Center\">" + Convert.ToDouble(DS_Common.Tables["ColorSpecification"].Rows[i]["ErrorRate"].ToString()).ToString("00.00") + "%</td>";
                    HTMLstr += "<td align=\"Center\">" + DS_Common.Tables["ColorSpecification"].Rows[i]["Total_Errors"].ToString() + "</td>";
                    HTMLstr += "<td align=\"Center\">" + Convert.ToDouble(DS_Common.Tables["ColorSpecification"].Rows[i]["AVG_DE"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Center\">" + DS_Common.Tables["ColorSpecification"].Rows[i]["MAX_DE"].ToString() + "</td>";
                    HTMLstr += "</tr>";
                }

                str = str.Replace("##ColorSpecification##", HTMLstr);
                str = str.Replace("##divColorSpecification_S##", "  ");
                str = str.Replace("##divColorSpecification_E##", " ");
            }
            else
            {
                str = str.Replace("##ColorSpecification##", " ");
                str = str.Replace("##divColorSpecification_S##", " <!-- ");
                str = str.Replace("##divColorSpecification_E##", " --> ");
            }

            #endregion

            #region ====  Density Specification  ====

            if (DS_Common.Tables["DensitySpecification"] != null)
            {
                HTMLstr = "";

                for (int i = 0; i < DS_Common.Tables["DensitySpecification"].Rows.Count; i++)
                {
                    if ((i % 2) == 0)
                    {
                        HTMLstr += "<tr class=\"GRIDEVEN\" > ";
                    }
                    else
                    {
                        HTMLstr += "<tr class=\"GRIDODD\" > ";
                    }

                    HTMLstr += "<td align=\"left\">" + DS_Common.Tables["DensitySpecification"].Rows[i]["CPDeviceDesc"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"Center\">" + Convert.ToDouble(DS_Common.Tables["DensitySpecification"].Rows[i]["ErrorRate"].ToString()).ToString("00.00") + "%</td>";
                    HTMLstr += "<td align=\"Center\">" + DS_Common.Tables["DensitySpecification"].Rows[i]["Total_Errors"].ToString() + "</td>";
                    HTMLstr += "<td align=\"Center\">" + Convert.ToDouble(DS_Common.Tables["DensitySpecification"].Rows[i]["AVG_DE"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Center\">" + DS_Common.Tables["DensitySpecification"].Rows[i]["MAX_DE"].ToString() + "</td>";
                    HTMLstr += "</tr>";
                }

                str = str.Replace("##DensitySpecification##", HTMLstr);
                str = str.Replace("##divDensitySpecification_S##", " ");
                str = str.Replace("##divDensitySpecification_E##", " ");
            }
            else
            {
                str = str.Replace("##DensitySpecification##", " ");
                str = str.Replace("##divDensitySpecification_S##", " <!-- ");
                str = str.Replace("##divDensitySpecification_E##", " --> ");
            }

            #endregion

            #region ====  PlateSpecification  ====

            if (DS_Common.Tables["PlateSpecification"] != null)
            {

                HTMLstr = "";

                for (int i = 0; i < DS_Common.Tables["PlateSpecification"].Rows.Count; i++)
                {
                    if ((i % 2) == 0)
                    {
                        HTMLstr += "<tr class=\"GRIDEVEN\" > ";
                    }
                    else
                    {
                        HTMLstr += "<tr class=\"GRIDODD\" > ";
                    }

                    HTMLstr += "<td align=\"left\">" + DS_Common.Tables["PlateSpecification"].Rows[i]["CPDeviceDesc"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"Center\">" + Convert.ToDouble(DS_Common.Tables["PlateSpecification"].Rows[i]["ErrorRate"].ToString()).ToString("00.00") + "%</td>";
                    HTMLstr += "<td align=\"Center\">" + DS_Common.Tables["PlateSpecification"].Rows[i]["Total_Errors"].ToString() + "</td>";
                    HTMLstr += "<td align=\"Center\">" + Convert.ToDouble(DS_Common.Tables["PlateSpecification"].Rows[i]["AVG_Delta_NEG"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Center\">" + Convert.ToDouble(DS_Common.Tables["PlateSpecification"].Rows[i]["AVG_Delta_POS"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Center\">" + Convert.ToDouble(DS_Common.Tables["PlateSpecification"].Rows[i]["MAX_Delta_NEG"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Center\">" + Convert.ToDouble(DS_Common.Tables["PlateSpecification"].Rows[i]["MAX_Delta_POS"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "</tr>";
                }

                str = str.Replace("##PlateSpecification##", HTMLstr);
                str = str.Replace("##divPlateSpecification_S##", " ");
                str = str.Replace("##divPlateSpecification_E##", " ");
            }
            else
            {
                str = str.Replace("##PlateSpecification##", " ");
                str = str.Replace("##divPlateSpecification_S##", "<!-- ");
                str = str.Replace("##divPlateSpecification_E##", " --> ");
            }

            #endregion

            #region Assign Server
            str = str.Replace("##IMAGES##", strServer);
            #endregion

            return str;
        }
        #endregion

        #region Report 2 : Device Report
        private string TextReturn_DeviceReport(string str)
        {

            string HTMLstr = "";
            str = str.Replace("#csspath#", CSSPath);

            str = str.Replace("##Chart1_PDF##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_DeviceReport1.Jpeg");
            str = str.Replace("##Chart2_PDF##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_DeviceReport2.Jpeg");

            #region ====  Basic Info  ====
            if (DS_Common.Tables["BasicInfo"] != null && DS_Common.Tables["BasicInfo"].Rows.Count > 0)
            {

                str = str.Replace("##LBL_DateRange##", DS_Common.Tables["BasicInfo"].Rows[0]["DateRange_"].ToString().Trim());
                str = str.Replace("##LBL_DeviceName##", DS_Common.Tables["BasicInfo"].Rows[0]["Device_"].ToString().Trim());
                str = str.Replace("##LBL_DongleNumber##", DS_Common.Tables["BasicInfo"].Rows[0]["DongleNumber_"].ToString().Trim());
                str = str.Replace("##LBL_BranchName##", DS_Common.Tables["BasicInfo"].Rows[0]["Location_"].ToString().Trim());
                str = str.Replace("##LBL_CompanyName##", DS_Common.Tables["BasicInfo"].Rows[0]["CompanyName_"].ToString().Trim());
            }
            else
            {
                str = str.Replace("##LBL_DateRange##", "");
                str = str.Replace("##LBL_DeviceName##", "");
                str = str.Replace("##LBL_DongleNumber##", "");
                str = str.Replace("##LBL_BranchName##", "");
                str = str.Replace("##LBL_CompanyName##", "");
            }
            #endregion

            #region ====  Search Criteria  ====
            if (DS_Common.Tables["SearchCriteria"] != null && DS_Common.Tables["SearchCriteria"].Rows.Count > 0)
            {
                for (int i = 1; i <= DS_Common.Tables["SearchCriteria"].Rows.Count; i++)
                {
                    str = str.Replace("##LBL_Field" + i + "##", DS_Common.Tables["SearchCriteria"].Rows[i - 1]["SearchField_"].ToString().Trim());
                    str = str.Replace("##LBL_Operator" + i + "##", DS_Common.Tables["SearchCriteria"].Rows[i - 1]["Operator_"].ToString().Trim());
                    str = str.Replace("##LBL_Criteria" + i + "##", DS_Common.Tables["SearchCriteria"].Rows[i - 1]["Criteria_"].ToString().Trim());
                }
            }
            else
            {
                for (int i = 1; i <= 3; i++)
                {
                    str = str.Replace("##LBL_Field" + i + "##", "");
                    str = str.Replace("##LBL_Operator" + i + "##", "");
                    str = str.Replace("##LBL_Criteria" + i + "##", "");
                }
            }
            #endregion

            #region ====  Failure Rate  ====
            str = str.Replace("##ColorDetail_1##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_ColorDetail_1.Jpeg");

            if (DS_Common.Tables["FailureRate"] != null && DS_Common.Tables["FailureRate"].Rows.Count > 0)
            {
                str = str.Replace("##LBL_NumofMeasurements##", DS_Common.Tables["FailureRate"].Rows[0]["NumofMeasurements_"].ToString().Trim());
                str = str.Replace("##LBL_Passed##", DS_Common.Tables["FailureRate"].Rows[0]["Passed_"].ToString().Trim());
                str = str.Replace("##LBL_Failed##", DS_Common.Tables["FailureRate"].Rows[0]["Failed_"].ToString().Trim());
                str = str.Replace("##LBL_Cautions##", DS_Common.Tables["FailureRate"].Rows[0]["Cautions_"].ToString().Trim());
            }
            else
            {
                str = str.Replace("##LBL_NumofMeasurements##", "");
                str = str.Replace("##LBL_Passed##", "");
                str = str.Replace("##LBL_Failed##", "");
                str = str.Replace("##LBL_Cautions##", "");

            }
            #endregion

            #region ====  Environmental  ====
            if (DS_Common.Tables["Environmental"] != null && DS_Common.Tables["Environmental"].Rows.Count > 0)
            {
                if (
                    Convert.ToDecimal(DS_Common.Tables["Environmental"].Rows[0]["AveTemp_"]) == 0
                    && Convert.ToDecimal(DS_Common.Tables["Environmental"].Rows[0]["AveRH_"]) == 0
                && Convert.ToDecimal(DS_Common.Tables["Environmental"].Rows[0]["MinTemp_"]) == 0
                && Convert.ToDecimal(DS_Common.Tables["Environmental"].Rows[0]["MaxTemp_"]) == 0
                && Convert.ToDecimal(DS_Common.Tables["Environmental"].Rows[0]["MinRH_"]) == 0
                && Convert.ToDecimal(DS_Common.Tables["Environmental"].Rows[0]["MaxRH_"]) == 0
                )
                {
                    str = str.Replace("##TR_ENV1##", "<!--");
                    str = str.Replace("##TR_ENV2##", "-->");
                }
                else
                {
                    str = str.Replace("##TR_ENV1##", "");
                    str = str.Replace("##TR_ENV2##", "");

                    str = str.Replace("##LBL_AveTemp##", DS_Common.Tables["Environmental"].Rows[0]["AveTemp_"].ToString().Trim() + "°F");
                    str = str.Replace("##LBL_AveRH##", DS_Common.Tables["Environmental"].Rows[0]["AveRH_"].ToString().Trim() + "°F");
                    str = str.Replace("##LBL_MinTemp##", DS_Common.Tables["Environmental"].Rows[0]["MinTemp_"].ToString().Trim() + "°F");
                    str = str.Replace("##LBL_MaxTemp##", DS_Common.Tables["Environmental"].Rows[0]["MaxTemp_"].ToString().Trim() + "°F");
                    str = str.Replace("##LBL_MinRH##", DS_Common.Tables["Environmental"].Rows[0]["MinRH_"].ToString().Trim() + "%");
                    str = str.Replace("##LBL_MaxRH##", DS_Common.Tables["Environmental"].Rows[0]["MaxRH_"].ToString().Trim() + "%");
                }
            }
            else
            {
                str = str.Replace("##TR_ENV1##", "<!--");
                str = str.Replace("##TR_ENV2##", "-->");

                str = str.Replace("##LBL_AveTemp##", "");
                str = str.Replace("##LBL_AveRH##", "");
                str = str.Replace("##LBL_MinTemp##", "");
                str = str.Replace("##LBL_MaxTemp##", "");
                str = str.Replace("##LBL_MinRH##", "");
                str = str.Replace("##LBL_MaxRH##", "");
            }
            #endregion

            #region ====  Consumables  ====
            if (DS_Common.Tables["Consumables"] != null && DS_Common.Tables["Consumables"].Rows.Count > 0)
            {
                for (int i = 0; i < DS_Common.Tables["Consumables"].Rows.Count; i++)
                {
                    if (DS_Common.Tables["Consumables"].Rows[i]["Measurements_"].ToString() != "0" && DS_Common.Tables["Consumables"].Rows[i]["DeltaEAve_"].ToString() != "0" && DS_Common.Tables["Consumables"].Rows[i]["DeltaMax_"].ToString() != "0" && DS_Common.Tables["Consumables"].Rows[i]["DeltaDensityAve_"].ToString() != "0" && DS_Common.Tables["Consumables"].Rows[i]["Measurements_"].ToString() != "" && DS_Common.Tables["Consumables"].Rows[i]["DeltaEAve_"].ToString() != "" && DS_Common.Tables["Consumables"].Rows[i]["DeltaMax_"].ToString() != "" && DS_Common.Tables["Consumables"].Rows[i]["DeltaDensityAve_"].ToString() != "")
                    {
                        str = str.Replace("##divSubstrate_Ink_S##", "");
                        str = str.Replace("##divSubstrate_Ink_E##", "");

                        str = str.Replace("##LBL_InkMeasurements" + i + "##", DS_Common.Tables["Consumables"].Rows[i]["Measurements_"].ToString().Trim());
                        str = str.Replace("##LBL_InkDeltaEAve" + i + "##", DS_Common.Tables["Consumables"].Rows[i]["DeltaEAve_"].ToString().Trim());
                        str = str.Replace("##LBL_InkDeltaMax" + i + "##", DS_Common.Tables["Consumables"].Rows[i]["DeltaMax_"].ToString().Trim());
                        str = str.Replace("##LBL_InkDeltaDensityAve" + i + "##", DS_Common.Tables["Consumables"].Rows[i]["DeltaDensityAve_"].ToString().Trim());
                        str = str.Replace("##LBL_StdDev" + i + "##", DS_Common.Tables["Consumables"].Rows[i]["StdDev_"].ToString().Trim());
                    }
                    else
                    {
                        str = str.Replace("##divSubstrate_Ink_S##", "<!-- ");
                        str = str.Replace("##divSubstrate_Ink_E##", " --> ");
                    }
                }
            }
            #endregion

            #region ==== Consumabel ========
            HTMLstr = "";

            if (DS_Common.Tables["Consumabel"] != null && DS_Common.Tables["Consumabel"].Rows.Count > 0)
            {
                for (int i = 0; i < DS_Common.Tables["Consumabel"].Rows.Count; i++)
                {
                    if ((i % 2) == 0)
                    {
                        HTMLstr += "<tr class=\"GRIDEVEN\" > ";
                    }
                    else
                    {
                        HTMLstr += "<tr class=\"GRIDODD\" > ";
                    }

                    HTMLstr += "<td align=\"left\">" + DS_Common.Tables["Consumabel"].Rows[i]["Plate"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"Center\">" + DS_Common.Tables["Consumabel"].Rows[i]["TotalReads"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"Center\">" + Convert.ToDouble(DS_Common.Tables["Consumabel"].Rows[i]["AvgNegDev"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Center\">" + Convert.ToDouble(DS_Common.Tables["Consumabel"].Rows[i]["AvgPosDev"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Center\">" + Convert.ToDouble(DS_Common.Tables["Consumabel"].Rows[i]["MaxNegDev"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Center\">" + Convert.ToDouble(DS_Common.Tables["Consumabel"].Rows[i]["MaxPosDev"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "</tr>";
                }

                str = str.Replace(" ##Consumabel##", HTMLstr);
                str = str.Replace("##divSubstrate_S##", " ");
                str = str.Replace("##divSubstrate_E##", " ");
            }
            else
            {
                str = str.Replace(" ##Consumabel##", " ");
                str = str.Replace("##divSubstrate_S##", "<!-- ");
                str = str.Replace("##divSubstrate_E##", " --> ");
            }

            #endregion

            #region Assign Server
            str = str.Replace("##IMAGES##", strServer);
            #endregion

            return str;
        }
        #endregion

        #region Report 3 : Shift Variability Report
        private string TextReturn_ShiftVariabilityReport(string str)
        {
            str = str.Replace("#csspath#", CSSPath);

            #region ====  Basic Info  ====
            if (DS_Common.Tables["BasicInfo"] != null && DS_Common.Tables["BasicInfo"].Rows.Count > 0)
            {

                str = str.Replace("##LBL_DateRange##", DS_Common.Tables["BasicInfo"].Rows[0]["DateRange_"].ToString().Trim());
                str = str.Replace("##LBL_DeviceName##", DS_Common.Tables["BasicInfo"].Rows[0]["Device_"].ToString().Trim());
                str = str.Replace("##LBL_DongleNumber##", DS_Common.Tables["BasicInfo"].Rows[0]["DongleNumber_"].ToString().Trim());
                str = str.Replace("##LBL_BranchName##", DS_Common.Tables["BasicInfo"].Rows[0]["Location_"].ToString().Trim());
                str = str.Replace("##LBL_CompanyName##", DS_Common.Tables["BasicInfo"].Rows[0]["CompanyName_"].ToString().Trim());
            }
            else
            {
                str = str.Replace("##LBL_DateRange##", "");
                str = str.Replace("##LBL_DeviceName##", "");
                str = str.Replace("##LBL_DongleNumber##", "");
                str = str.Replace("##LBL_BranchName##", "");
                str = str.Replace("##LBL_CompanyName##", "");
            }
            #endregion

            #region ====  Search Criteria  ====
            if (DS_Common.Tables["SearchCriteria"] != null && DS_Common.Tables["SearchCriteria"].Rows.Count > 0)
            {
                for (int i = 1; i <= DS_Common.Tables["SearchCriteria"].Rows.Count; i++)
                {
                    str = str.Replace("##LBL_Field" + i + "##", DS_Common.Tables["SearchCriteria"].Rows[i - 1]["SearchField_"].ToString().Trim());
                    str = str.Replace("##LBL_Operator" + i + "##", DS_Common.Tables["SearchCriteria"].Rows[i - 1]["Operator_"].ToString().Trim());
                    str = str.Replace("##LBL_Criteria" + i + "##", DS_Common.Tables["SearchCriteria"].Rows[i - 1]["Criteria_"].ToString().Trim());
                }
            }
            else
            {
                for (int i = 1; i <= 3; i++)
                {
                    str = str.Replace("##LBL_Field" + i + "##", "");
                    str = str.Replace("##LBL_Operator" + i + "##", "");
                    str = str.Replace("##LBL_Criteria" + i + "##", "");
                }
            }
            #endregion

            #region ====  Show the Time Range  ====
            if (DS_Common.Tables["Shift1"] != null && DS_Common.Tables["Shift1"].Rows.Count > 0)
            {
                str = str.Replace("##LBL_Shift1Start##", DS_Common.Tables["Shift1"].Rows[0]["Start_"].ToString().Trim());
                str = str.Replace("##LBL_Shift1End##", DS_Common.Tables["Shift1"].Rows[0]["End_"].ToString().Trim());

                str = str.Replace("##LBL_Shift2Start##", DS_Common.Tables["Shift2"].Rows[0]["Start_"].ToString().Trim());
                str = str.Replace("##LBL_Shift2End##", DS_Common.Tables["Shift2"].Rows[0]["End_"].ToString().Trim());
            }
            #endregion

            #region Assign Server
            str = str.Replace("##IMAGES##", strServer);
            #endregion

            return str;
        }
        private string TextReturn_ShiftVariabilityReport_1(string str)
        {
            str = str.Replace("#csspath#", CSSPath);

            #region ====  Shift 1  ====
            if (DS_Common.Tables["Shift1"] != null && DS_Common.Tables["Shift1"].Rows.Count > 0)
            {
                str = str.Replace("##LBL_Shift1Start##", DS_Common.Tables["Shift1"].Rows[0]["Start_"].ToString().Trim());
                str = str.Replace("##LBL_Shift1End##", DS_Common.Tables["Shift1"].Rows[0]["End_"].ToString().Trim());

                str = str.Replace("##LBL_Shift2Start##", DS_Common.Tables["Shift2"].Rows[0]["Start_"].ToString().Trim());
                str = str.Replace("##LBL_Shift2End##", DS_Common.Tables["Shift2"].Rows[0]["End_"].ToString().Trim());

                str = str.Replace("##LBL_Shift1ErrorRatio##", DS_Common.Tables["Shift1"].Rows[0]["ErrorRatio_"].ToString().Trim());
                str = str.Replace("##LBL_Shift1NoOfReads##", DS_Common.Tables["Shift1"].Rows[0]["NumberOfReads_"].ToString().Trim());
                str = str.Replace("##LBL_Shift1TotalErrors##", DS_Common.Tables["Shift1"].Rows[0]["TotalErrors_"].ToString().Trim());
            }
            else
            {
                str = str.Replace("##LBL_Shift1Start##", "");
                str = str.Replace("##LBL_Shift1End##", "");
                str = str.Replace("##LBL_Shift1ErrorRatio##", "");
                str = str.Replace("##LBL_Shift1NoOfReads##", "");
                str = str.Replace("##LBL_Shift1TotalErrors##", "");

            }
            #endregion

            #region Show Hide Images
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_MaxDeltaE1"] == true) { str = str.Replace("##CHART_MaxDeltaE_1##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_MaxDeltaE_Shift1.Jpeg").Replace("##MAXDELTAE_O##", "").Replace("##MAXDELTAE_C##", ""); } else { str = str.Replace("##MAXDELTAE_O##", "<!--").Replace("##MAXDELTAE_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_Density1"] == true) { str = str.Replace("##CHART_Density_1##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_Density_Shift1.Jpeg").Replace("##DENSITY_O##", "").Replace("##DENSITY_C##", ""); } else { str = str.Replace("##DENSITY_O##", "<!--").Replace("##DENSITY_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_DotGain1"] == true) { str = str.Replace("##DotGain_Shift1##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_DotGain_Shift1.Jpeg").Replace("##DOTGAIN_O##", "").Replace("##DOTGAIN_C##", ""); } else { str = str.Replace("##DOTGAIN_O##", "<!--").Replace("##DOTGAIN_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_PaperWhite1"] == true) { str = str.Replace("##CHART_PaperWhite_1##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_PaperWhite_Shift1.Jpeg").Replace("##PAPERWHITE_O##", "").Replace("##PAPERWHITE_C##", ""); } else { str = str.Replace("##PAPERWHITE_O##", "<!--").Replace("##PAPERWHITE_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_TempHumidity1"] == true) { str = str.Replace("##CHART_TempHumidity_1##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_TempHumidity_Shift1.Jpeg").Replace("##HUMIDITY_O##", "").Replace("##HUMIDITY_C##", ""); } else { str = str.Replace("##HUMIDITY_O##", "<!--").Replace("##HUMIDITY_C##", "-->"); }
            #endregion

            #region Assign Server
            str = str.Replace("##IMAGES##", strServer);
            #endregion

            return str;
        }
        private string TextReturn_ShiftVariabilityReport_2(string str)
        {
            str = str.Replace("#csspath#", CSSPath);

            #region ====  Shift 2  ====
            if (DS_Common.Tables["Shift2"] != null && DS_Common.Tables["Shift2"].Rows.Count > 0)
            {
                str = str.Replace("##LBL_Shift2ErrorRatio##", DS_Common.Tables["Shift2"].Rows[0]["ErrorRatio_"].ToString().Trim());
                str = str.Replace("##LBL_Shift2NoOfReads##", DS_Common.Tables["Shift2"].Rows[0]["NumberOfReads_"].ToString().Trim());
                str = str.Replace("##LBL_Shift2TotalErrors##", DS_Common.Tables["Shift2"].Rows[0]["TotalErrors_"].ToString().Trim());
            }
            else
            {
                str = str.Replace("##LBL_Shift2ErrorRatio##", "");
                str = str.Replace("##LBL_Shift2NoOfReads##", "");
                str = str.Replace("##LBL_Shift2TotalErrors##", "");

            }
            #endregion

            #region Show Hide Images
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_MaxDeltaE2"] == true) { str = str.Replace("##CHART_MaxDeltaE_2##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_MaxDeltaE_Shift2.Jpeg").Replace("##MAXDELTAE_O##", "").Replace("##MAXDELTAE_C##", ""); } else { str = str.Replace("##MAXDELTAE_O##", "<!--").Replace("##MAXDELTAE_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_Density2"] == true) { str = str.Replace("##CHART_Density_2##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_Density_Shift2.Jpeg").Replace("##DENSITY_O##", "").Replace("##DENSITY_C##", ""); } else { str = str.Replace("##DENSITY_O##", "<!--").Replace("##DENSITY_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_DotGain2"] == true) { str = str.Replace("##Chart_Dotgain2##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_DotGain_Shift2.Jpeg").Replace("##DOTGAIN_O##", "").Replace("##DOTGAIN_C##", ""); } else { str = str.Replace("##DOTGAIN_O##", "<!--").Replace("##DOTGAIN_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_PaperWhite2"] == true) { str = str.Replace("##CHART_PaperWhite_2##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_PaperWhite_Shift2.Jpeg").Replace("##PAPERWHITE_O##", "").Replace("##PAPERWHITE_C##", ""); } else { str = str.Replace("##PAPERWHITE_O##", "<!--").Replace("##PAPERWHITE_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_TempHumidity2"] == true) { str = str.Replace("##CHART_TempHumidity_2##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_TempHumidity_Shift2.Jpeg").Replace("##HUMIDITY_O##", "").Replace("##HUMIDITY_C##", ""); } else { str = str.Replace("##HUMIDITY_O##", "<!--").Replace("##HUMIDITY_C##", "-->"); }
            #endregion

            #region Assign Server
            str = str.Replace("##IMAGES##", strServer);
            #endregion

            return str;
        }
        #endregion

        #region Report 4 : Device Variability Report
        private string TextReturn_DeviceVariabilityReport(string str)
        {
            str = str.Replace("#csspath#", CSSPath);

            #region ====  Basic Info  ====
            if (DS_Common.Tables["BasicInfo"] != null && DS_Common.Tables["BasicInfo"].Rows.Count > 0)
            {

                str = str.Replace("##LBL_DateRange##", DS_Common.Tables["BasicInfo"].Rows[0]["DateRange_"].ToString().Trim());
                str = str.Replace("##LBL_Device1##", DS_Common.Tables["BasicInfo"].Rows[0]["Device_"].ToString().Trim());
                str = str.Replace("##LBL_DongleNumber1##", DS_Common.Tables["BasicInfo"].Rows[0]["DongleNumber_"].ToString().Trim());
                str = str.Replace("##LBL_LocationName1##", DS_Common.Tables["BasicInfo"].Rows[0]["Location_"].ToString().Trim());
                str = str.Replace("##LBL_Company1##", DS_Common.Tables["BasicInfo"].Rows[0]["CompanyName_"].ToString().Trim());
                str = str.Replace("##LBL_MeasurementID1##", DS_Common.Tables["BasicInfo"].Rows[0]["MeasurementID_"].ToString().Trim());
            }
            else
            {
                str = str.Replace("##LBL_DateRange##", "");
                str = str.Replace("##LBL_DeviceName##", "");
                str = str.Replace("##LBL_DongleNumber1##", "");
                str = str.Replace("##LBL_LocationName1##", "");
                str = str.Replace("##LBL_Company1##", "");
                str = str.Replace("##LBL_MeasurementID1##", "");
            }
            if (DS_Common.Tables["BasicInfo"] != null && DS_Common.Tables["BasicInfo"].Rows.Count > 1)
            {
                str = str.Replace("##LBL_Device2##", DS_Common.Tables["BasicInfo"].Rows[1]["Device_"].ToString().Trim());
                str = str.Replace("##LBL_DongleNumber2##", DS_Common.Tables["BasicInfo"].Rows[1]["DongleNumber_"].ToString().Trim());
                str = str.Replace("##LBL_LocationName2##", DS_Common.Tables["BasicInfo"].Rows[1]["Location_"].ToString().Trim());
                str = str.Replace("##LBL_Company2##", DS_Common.Tables["BasicInfo"].Rows[1]["CompanyName_"].ToString().Trim());
                str = str.Replace("##LBL_MeasurementID2##", DS_Common.Tables["BasicInfo"].Rows[1]["MeasurementID_"].ToString().Trim());
            }
            else
            {
                str = str.Replace("##LBL_DeviceName##", "");
                str = str.Replace("##LBL_DongleNumber2##", "");
                str = str.Replace("##LBL_LocationName2##", "");
                str = str.Replace("##LBL_Company2##", "");
                str = str.Replace("##LBL_MeasurementID2##", "");
            }

            #endregion

            #region ====  Search Criteria  ====
            if (DS_Common.Tables["SearchCriteria"] != null && DS_Common.Tables["SearchCriteria"].Rows.Count > 0)
            {
                for (int i = 1; i <= DS_Common.Tables["SearchCriteria"].Rows.Count; i++)
                {
                    str = str.Replace("##LBL_Field" + i + "##", DS_Common.Tables["SearchCriteria"].Rows[i - 1]["SearchField_"].ToString().Trim());
                    str = str.Replace("##LBL_Operator" + i + "##", DS_Common.Tables["SearchCriteria"].Rows[i - 1]["Operator_"].ToString().Trim());
                    str = str.Replace("##LBL_Criteria" + i + "##", DS_Common.Tables["SearchCriteria"].Rows[i - 1]["Criteria_"].ToString().Trim());
                }
            }
            else
            {
                for (int i = 1; i <= 3; i++)
                {
                    str = str.Replace("##LBL_Field" + i + "##", "");
                    str = str.Replace("##LBL_Operator" + i + "##", "");
                    str = str.Replace("##LBL_Criteria" + i + "##", "");
                }
            }
            #endregion

            #region Assign Server
            str = str.Replace("##IMAGES##", strServer);
            #endregion

            return str;
        }
        private string TextReturn_DeviceVariabilityReport_1(string str)
        {
            str = str.Replace("#csspath#", CSSPath);

            #region ====  Device 1  ====
            if (DS_Common.Tables["BasicInfo"] != null && DS_Common.Tables["BasicInfo"].Rows.Count > 1)
            { str = str.Replace("##LBL_Device1##", DS_Common.Tables["BasicInfo"].Rows[0]["Device_"].ToString().Trim()); }
            else
            { str = str.Replace("##LBL_Device1##", ""); }
            #endregion

            #region Measurements
            if (DS_Common.Tables["Measurement"] != null && DS_Common.Tables["Measurement"].Rows.Count > 0)
            {
                str = str.Replace("##ErrorRate1##", DS_Common.Tables["Measurement"].Rows[0]["ErrorRate"].ToString());
                str = str.Replace("##NoOfReads1##", DS_Common.Tables["Measurement"].Rows[0]["NoOfReads"].ToString());
                str = str.Replace("##TotalErrors1##", DS_Common.Tables["Measurement"].Rows[0]["TotalErrors"].ToString());
            }
            #endregion

            #region Show Hide Images
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_MaxDeltaE1"] == true) { str = str.Replace("##CHART_MaxDeltaE_1##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_MaxDeltaE_Device_1.Jpeg").Replace("##MAXDELTAE_O##", "").Replace("##MAXDELTAE_C##", ""); } else { str = str.Replace("##MAXDELTAE_O##", "<!--").Replace("##MAXDELTAE_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_Density1"] == true) { str = str.Replace("##CHART_Density_1##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_Density_Device_1.Jpeg").Replace("##DENSITY_O##", "").Replace("##DENSITY_C##", ""); } else { str = str.Replace("##DENSITY_O##", "<!--").Replace("##DENSITY_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_DotGain1"] == true) { str = str.Replace("##Chart_DotGain_Device_1##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_DotGain_Device_1.Jpeg").Replace("##DOTGAIN_O##", "").Replace("##DOTGAIN_C##", ""); } else { str = str.Replace("##DOTGAIN_O##", "<!--").Replace("##DOTGAIN_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_PaperWhite1"] == true) { str = str.Replace("##CHART_PaperWhite_1##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_PaperWhite_Device_1.Jpeg").Replace("##PAPERWHITE_O##", "").Replace("##PAPERWHITE_C##", ""); } else { str = str.Replace("##PAPERWHITE_O##", "<!--").Replace("##PAPERWHITE_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_TempHumidity1"] == true) { str = str.Replace("##CHART_TempHumidity_1##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_TempHumidity_Device_1.Jpeg").Replace("##HUMIDITY_O##", "").Replace("##HUMIDITY_C##", ""); } else { str = str.Replace("##HUMIDITY_O##", "<!--").Replace("##HUMIDITY_C##", "-->"); }
            #endregion

            #region Assign Server
            str = str.Replace("##IMAGES##", strServer);
            #endregion

            return str;
        }
        private string TextReturn_DeviceVariabilityReport_2(string str)
        {
            str = str.Replace("#csspath#", CSSPath);

            #region ====  Device 2  ====
            if (DS_Common.Tables["BasicInfo"] != null && DS_Common.Tables["BasicInfo"].Rows.Count > 1)
            { str = str.Replace("##LBL_Device2##", DS_Common.Tables["BasicInfo"].Rows[1]["Device_"].ToString().Trim()); }
            else
            { str = str.Replace("##LBL_Device2##", ""); }
            #endregion

            #region Measurements
            if (DS_Common.Tables["Measurement"] != null && DS_Common.Tables["Measurement"].Rows.Count > 0)
            {
                str = str.Replace("##ErrorRate2##", DS_Common.Tables["Measurement"].Rows[1]["ErrorRate"].ToString());
                str = str.Replace("##NoOfReads2##", DS_Common.Tables["Measurement"].Rows[1]["NoOfReads"].ToString());
                str = str.Replace("##TotalErrors2##", DS_Common.Tables["Measurement"].Rows[1]["TotalErrors"].ToString());
            }
            #endregion

            #region Show Hide Images
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_MaxDeltaE2"] == true) { str = str.Replace("##CHART_MaxDeltaE_2##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_MaxDeltaE_Device_2.Jpeg").Replace("##MAXDELTAE_O##", "").Replace("##MAXDELTAE_C##", ""); } else { str = str.Replace("##MAXDELTAE_O##", "<!--").Replace("##MAXDELTAE_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_Density2"] == true) { str = str.Replace("##CHART_Density_2##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_Density_Device_2.Jpeg").Replace("##DENSITY_O##", "").Replace("##DENSITY_C##", ""); } else { str = str.Replace("##DENSITY_O##", "<!--").Replace("##DENSITY_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_DotGain2"] == true) { str = str.Replace("##Chart_Dotgain_device2##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_Dotgain_device2.Jpeg").Replace("##DOTGAIN_O##", "").Replace("##DOTGAIN_C##", ""); } else { str = str.Replace("##DOTGAIN_O##", "<!--").Replace("##DOTGAIN_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_PaperWhite2"] == true) { str = str.Replace("##CHART_PaperWhite_2##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_PaperWhite_Device_2.Jpeg").Replace("##PAPERWHITE_O##", "").Replace("##PAPERWHITE_C##", ""); } else { str = str.Replace("##PAPERWHITE_O##", "<!--").Replace("##PAPERWHITE_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_TempHumidity2"] == true) { str = str.Replace("##CHART_TempHumidity_2##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_TempHumidity_Device_2.Jpeg").Replace("##HUMIDITY_O##", "").Replace("##HUMIDITY_C##", ""); } else { str = str.Replace("##HUMIDITY_O##", "<!--").Replace("##HUMIDITY_C##", "-->"); }
            #endregion

            #region Assign Server
            str = str.Replace("##IMAGES##", strServer);
            #endregion

            return str;
        }
        #endregion

        #region Report 5 : Color Detail Report
        private string TextReturn_ColorDetailReport(string str)
        {
            str = str.Replace("#csspath#", CSSPath);

            str = str.Replace("##ColorDetail_1##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_ColorDetail_1.Jpeg");
            str = str.Replace("##ColorDetail_2##", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_ColorDetail_2.Jpeg");


            string HTMLstr = "";

            #region ====  Basic Info  ====
            if (DS_Common.Tables["BasicInfo"] != null && DS_Common.Tables["BasicInfo"].Rows.Count > 0)
            {

                str = str.Replace("##LBL_DateRange##", DS_Common.Tables["BasicInfo"].Rows[0]["DateRange_"].ToString().Trim());
                str = str.Replace("##LBL_DeviceName##", DS_Common.Tables["BasicInfo"].Rows[0]["Device_"].ToString().Trim());
                str = str.Replace("##LBL_Branch##", DS_Common.Tables["BasicInfo"].Rows[0]["Location_"].ToString().Trim());
                str = str.Replace("##LBL_CompanyName##", DS_Common.Tables["BasicInfo"].Rows[0]["CompanyName_"].ToString().Trim());
            }
            else
            {
                str = str.Replace("##LBL_DateRange##", "");
                str = str.Replace("##LBL_DeviceName##", "");
                str = str.Replace("##LBL_Branch##", "");
                str = str.Replace("##LBL_CompanyName##", "");
            }
            #endregion

            #region ====  Search Criteria  ====
            if (DS_Common.Tables["SearchCriteria"] != null)
            {

                for (int i = 0; i < DS_Common.Tables["SearchCriteria"].Rows.Count; i++)
                {
                    if ((i % 2) == 0)
                    {
                        HTMLstr += "<tr class=\"GRIDEVEN\" > ";
                    }
                    else
                    {
                        HTMLstr += "<tr class=\"GRIDODD\" > ";
                    }

                    HTMLstr += "<td align=\"left\" class=\"BOLD1\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["Parameter_"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"center\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["Result_"].ToString() + "</td> ";
                    HTMLstr += "<td class=\"BOLD1\" align=\"center\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["SearchField_"].ToString() + "</td>";
                    HTMLstr += "<td align=\"center\" class=\"BOLD1\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["Operator_"].ToString() + "</td>";
                    HTMLstr += "<td align=\"center\" class=\"BOLD1\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["Criteria_"].ToString() + "</td>";
                    HTMLstr += "</tr>";
                }

                str = str.Replace("##SearchCriteria##", HTMLstr);
            }
            else
            { str = str.Replace("##SearchCriteria##", " "); }
            #endregion

            #region ====  Environmental  ====
            if (DS_Common.Tables["Environmental"] != null && DS_Common.Tables["Environmental"].Rows.Count > 0)
            {
                str = str.Replace("##LBL_TempAvg##", DS_Common.Tables["Environmental"].Rows[0]["Average_"].ToString().Trim());
                str = str.Replace("##LBL_TempStandard##", DS_Common.Tables["Environmental"].Rows[0]["Standarad_"].ToString().Trim());
                str = str.Replace("##LBL_TempMin##", DS_Common.Tables["Environmental"].Rows[0]["Min_"].ToString().Trim());
            }
            else
            {
                str = str.Replace("##LBL_TempAvg##", "");
                str = str.Replace("##LBL_TempStandard##", "");
                str = str.Replace("##LBL_TempMin##", "");
            }

            if (DS_Common.Tables["Environmental"] != null && DS_Common.Tables["Environmental"].Rows.Count > 1)
            {
                str = str.Replace("##LBL_RhAvg##", DS_Common.Tables["Environmental"].Rows[1]["Average_"].ToString().Trim());
                str = str.Replace("##LBL_RhStandard##", DS_Common.Tables["Environmental"].Rows[1]["Standarad_"].ToString().Trim());
                str = str.Replace("##LBL_RhMin##", DS_Common.Tables["Environmental"].Rows[1]["Min_"].ToString().Trim());
            }
            else
            {
                str = str.Replace("##LBL_RhAvg##", "");
                str = str.Replace("##LBL_RhStandard##", "");
                str = str.Replace("##LBL_RhMin##", "");
            }
            #endregion

            #region ====  Consumables  ====
            if (DS_Common.Tables["Consumables"] != null && DS_Common.Tables["Consumables"].Rows.Count > 0)
            {
                for (int i = 0; i < DS_Common.Tables["Consumables"].Rows.Count; i++)
                {
                    str = str.Replace("##LBL_AvgDeltaE_" + i + "##", DS_Common.Tables["Consumables"].Rows[i]["AveDeltaE_"].ToString().Trim());
                    str = str.Replace("##LBL_AveHueDelta_" + i + "##", DS_Common.Tables["Consumables"].Rows[i]["AveHueDelta_"].ToString().Trim());
                    str = str.Replace("##LBL_AVELDelta_" + i + "##", DS_Common.Tables["Consumables"].Rows[i]["AveLDelta_"].ToString().Trim());
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    str = str.Replace("##LBL_AvgDeltaE_" + i + "##", "");
                    str = str.Replace("##LBL_AveHueDelta_" + i + "##", "");
                    str = str.Replace("##LBL_AVELDelta_" + i + "##", "");
                }
            }
            #endregion

            #region === Consumables SKU Information ===
            string ConSKUInfo1 = "<!--", ConSKUInfo2 = "-->";
            string ConSKUInfoPaper1 = "<!--", ConSKUInfoPaper2 = "-->";
            string ConSKUInfoInk1 = "<!--", ConSKUInfoInk2 = "-->";

            #region ====  SkuInfo  ====
            HTMLstr = "";
            if (DS_Common.Tables["SkuInfo"] != null && DS_Common.Tables["SkuInfo"].Rows.Count > 0)
            {
                for (int i = 0; i < DS_Common.Tables["SkuInfo"].Rows.Count; i++)
                {
                    if ((i % 2) == 0)
                    {
                        HTMLstr += "<tr class=\"GRIDEVEN\" > ";
                    }
                    else
                    {
                        HTMLstr += "<tr class=\"GRIDODD\" > ";
                    }

                    HTMLstr += "<td align=\"left\">" + DS_Common.Tables["SkuInfo"].Rows[i]["PAPER_SKU"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"left\">" + DS_Common.Tables["SkuInfo"].Rows[i]["MEASUREMENTS"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"left\">" + Convert.ToDouble(DS_Common.Tables["SkuInfo"].Rows[i]["ErrorRate"].ToString()).ToString("00.00") + "%</td>";
                    HTMLstr += "<td align=\"left\">" + Convert.ToDouble(DS_Common.Tables["SkuInfo"].Rows[i]["AVG_DE"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "</tr>";
                }

                str = str.Replace("##SkuInfo##", HTMLstr);
            }
            else
            { str = str.Replace("##SkuInfo##", " "); }
            if (HTMLstr != "") { ConSKUInfoPaper1 = ""; ConSKUInfoPaper2 = ""; }
            #endregion

            #region ====  Ink SkuInfo  ====
            HTMLstr = "";
            if (DS_Common.Tables["InkSkuInfo"] != null && DS_Common.Tables["InkSkuInfo"].Rows.Count > 0)
            {
                for (int i = 0; i < DS_Common.Tables["InkSkuInfo"].Rows.Count; i++)
                {
                    if ((i % 2) == 0)
                    {
                        HTMLstr += "<tr class=\"GRIDEVEN\" > ";
                    }
                    else
                    {
                        HTMLstr += "<tr class=\"GRIDODD\" > ";
                    }
                    HTMLstr += "<td align=\"left\">" + DS_Common.Tables["InkSkuInfo"].Rows[i]["INK_SKU"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"left\">" + DS_Common.Tables["InkSkuInfo"].Rows[i]["MEASUREMENTS"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"left\">" + Convert.ToDouble(DS_Common.Tables["InkSkuInfo"].Rows[i]["ErrorRate"].ToString()).ToString("00.00") + "%</td>";
                    HTMLstr += "<td align=\"left\">" + Convert.ToDouble(DS_Common.Tables["InkSkuInfo"].Rows[i]["AVG_DE"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "</tr>";
                }

                str = str.Replace("##InkSkuInfo##", HTMLstr);
            }
            else
            { str = str.Replace("##InkSkuInfo##", " "); }
            if (HTMLstr != "") { ConSKUInfoInk1 = ""; ConSKUInfoInk2 = ""; }
            #endregion

            if (ConSKUInfoPaper1 == "" || ConSKUInfoInk1 == "") { ConSKUInfo1 = ""; ConSKUInfo2 = ""; }
            if (ConSKUInfo1 != "")
            {
                ConSKUInfoPaper1 = ""; ConSKUInfoPaper2 = "";
                ConSKUInfoInk1 = ""; ConSKUInfoInk2 = "";
            }
            str = str.Replace("##ConSKUInfoPaper1##", ConSKUInfoPaper1);
            str = str.Replace("##ConSKUInfoPaper2##", ConSKUInfoPaper2);
            str = str.Replace("##ConSKUInfoInk1##", ConSKUInfoInk1);
            str = str.Replace("##ConSKUInfoInk2##", ConSKUInfoInk2);
            str = str.Replace("##ConSKUInfo1##", ConSKUInfo1);
            str = str.Replace("##ConSKUInfo2##", ConSKUInfo2);
            #endregion

            #region === Consumables Batch Information ===
            string ConBatchInfo1 = "<!--", ConBatchInfo2 = "-->";
            string ConBatchInfoSubstrate1 = "<!--", ConBatchInfoSubstrate2 = "-->";
            string ConBatchInfoInk1 = "<!--", ConBatchInfoInk2 = "-->";

            #region ====  BatchInfo  ====
            HTMLstr = "";
            if (DS_Common.Tables["BatchInfo"] != null && DS_Common.Tables["BatchInfo"].Rows.Count > 0)
            {
                for (int i = 0; i < DS_Common.Tables["BatchInfo"].Rows.Count; i++)
                {
                    if ((i % 2) == 0)
                    {
                        HTMLstr += "<tr class=\"GRIDEVEN\" > ";
                    }
                    else
                    {
                        HTMLstr += "<tr class=\"GRIDODD\" > ";
                    }

                    HTMLstr += "<td align=\"left\">" + DS_Common.Tables["BatchInfo"].Rows[i]["Substrate_Batch"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"left\">" + DS_Common.Tables["BatchInfo"].Rows[i]["MEASUREMENTS"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"left\">" + Convert.ToDouble(DS_Common.Tables["BatchInfo"].Rows[i]["ErrorRate"].ToString()).ToString("00.00") + "%</td>";
                    HTMLstr += "<td align=\"left\">" + Convert.ToDouble(DS_Common.Tables["BatchInfo"].Rows[i]["AVG_DE"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "</tr>";
                }

                str = str.Replace("##BatchInfo##", HTMLstr);
            }
            else
            { str = str.Replace("##BatchInfo##", " "); }
            if (HTMLstr != "") { ConBatchInfoSubstrate1 = ""; ConBatchInfoSubstrate2 = ""; }
            #endregion

            #region ====  Ink BatchInfo  ====
            HTMLstr = "";

            if (DS_Common.Tables["InkBatchInfo"] != null && DS_Common.Tables["InkBatchInfo"].Rows.Count > 0)
            {
                for (int i = 0; i < DS_Common.Tables["InkBatchInfo"].Rows.Count; i++)
                {
                    if ((i % 2) == 0)
                    { HTMLstr += "<tr class=\"GRIDEVEN\" > "; }
                    else
                    { HTMLstr += "<tr class=\"GRIDODD\" > "; }
                    HTMLstr += "<td align=\"left\">" + DS_Common.Tables["InkBatchInfo"].Rows[i]["Ink_Batch"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"left\">" + DS_Common.Tables["InkBatchInfo"].Rows[i]["MEASUREMENTS"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"left\">" + Convert.ToDouble(DS_Common.Tables["InkBatchInfo"].Rows[i]["ErrorRate"].ToString()).ToString("00.00") + "%</td>";
                    HTMLstr += "<td align=\"left\">" + Convert.ToDouble(DS_Common.Tables["InkBatchInfo"].Rows[i]["AVG_DE"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "</tr>";
                }

                str = str.Replace("##InkBatchInfo##", HTMLstr);
            }
            else
            { str = str.Replace("##InkBatchInfo##", " "); }
            if (HTMLstr != "") { ConBatchInfoInk1 = ""; ConBatchInfoInk2 = ""; }
            #endregion

            if (ConBatchInfoSubstrate1 == "" || ConBatchInfoInk1 == "") { ConBatchInfo1 = ""; ConBatchInfo2 = ""; }
            if (ConBatchInfo1 != "")
            {
                ConBatchInfoSubstrate1 = ""; ConBatchInfoSubstrate2 = "";
                ConBatchInfoInk1 = ""; ConBatchInfoInk2 = "";
            }
            str = str.Replace("##ConBatchInfoSubstrate1##", ConBatchInfoSubstrate1);
            str = str.Replace("##ConBatchInfoSubstrate2##", ConBatchInfoSubstrate2);
            str = str.Replace("##ConBatchInfoInk1##", ConBatchInfoInk1);
            str = str.Replace("##ConBatchInfoInk2##", ConBatchInfoInk2);
            str = str.Replace("##ConBatchInfo1##", ConBatchInfo1);
            str = str.Replace("##ConBatchInfo2##", ConBatchInfo2);
            #endregion

            #region ====  Worst Patches  ====
            HTMLstr = "";
            int Count = 15;

            if (DS_Common.Tables["PatchDetails"] != null && DS_Common.Tables["PatchDetails"].Rows.Count > 0)
            {
                if (DS_Common.Tables["PatchDetails"].Rows.Count < Count)
                { Count = DS_Common.Tables["PatchDetails"].Rows.Count; }

                for (int i = 0; i < Count; i++)
                {
                    if ((i % 2) == 0)
                    {
                        HTMLstr += "<tr class=\"GRIDEVEN\" > ";
                    }
                    else
                    {
                        HTMLstr += "<tr class=\"GRIDODD\" > ";
                    }

                    //HTMLstr += "<tr> ";
                    string lbl = "<span style='background-color:" + ColorCalcuations.LabtoRGB(Convert.ToDouble(DS_Common.Tables["PatchDetails"].Rows[i]["LAB_L1"]), Convert.ToDouble(DS_Common.Tables["PatchDetails"].Rows[i]["LAB_A1"]), Convert.ToDouble(DS_Common.Tables["PatchDetails"].Rows[i]["LAB_B1"])) + "'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                    HTMLstr += "<td align=\"left\">" + DS_Common.Tables["PatchDetails"].Rows[i]["Sample_Name"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"left\">" + DS_Common.Tables["PatchDetails"].Rows[i]["CMYK_C"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"left\">" + DS_Common.Tables["PatchDetails"].Rows[i]["CMYK_M"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"left\">" + DS_Common.Tables["PatchDetails"].Rows[i]["CMYK_Y"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"left\">" + DS_Common.Tables["PatchDetails"].Rows[i]["CMYK_K"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"left\">" + lbl + "</td> ";
                    HTMLstr += "<td align=\"left\">" + Convert.ToDouble(DS_Common.Tables["PatchDetails"].Rows[i]["AVG_DE"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"left\">" + Convert.ToDouble(DS_Common.Tables["PatchDetails"].Rows[i]["STDEV_DE"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "</tr>";
                }
                str = str.Replace("##PatchDetail##", HTMLstr);
            }
            else
            {
                str = str.Replace("##PatchDetail##", " ");
            }

            #endregion

            #region Assign Server
            str = str.Replace("##IMAGES##", strServer);
            #endregion

            return str;
        }
        #endregion

        #region Report 6 : Patch Detail Report
        private string TextReturn_PatchReport(string str)
        {
            str = str.Replace("#csspath#", CSSPath);

            // === Page Header with Report Name and logo Image === //
            #region ====  Page Header  ====
            string strPageHeader = "";
            strPageHeader += "<table style='width:100%;background-color:White' cellspacing='0' cellpadding='0' border='0' align='center'><tr style='height:1px'><td>&nbsp;</td></tr><tr>";
            strPageHeader += "<td style='width:20%'>&nbsp;</td>";
            strPageHeader += "<td style='width:60%' align='center' class='ReportHeader'>";
            strPageHeader += "##DeviceName##&nbsp;-&nbsp;##BranchName##&nbsp;Report";
            strPageHeader += "</td>";
            strPageHeader += "<td style='width:20%' align='right'>";
            strPageHeader += "<img src='##IMAGES##powered-by.gif' align='right' />";
            strPageHeader += "</td></tr>";
            strPageHeader += "</table>";
            #endregion

            // === Report table header structure === //
            #region ====  Table Header  ====
            string strReportHeader = "";
            strReportHeader += "<table style='width:100%;border: 1PX SOLID #C1C1C1' cellspacing='1' cellpadding='0' border='0' align='center'><tr>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom'align='center'><img src='##IMAGES##title_color.gif' align='bottom' vspace=5 /></td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom'align='center'><img src='##IMAGES##title_patch_id.gif' / vspace=5 ></td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom'align='center'> <img src='##IMAGES##title_c.gif' / vspace= 5 ></td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom'align='center'><img src='##IMAGES##title_m.gif'  / vspace=5></td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom'align='center'><img src='##IMAGES##title_y.gif'  / vspace=5></td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom'align='center'><img src='##IMAGES##title_k.gif' / vspace=5></td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom'align='center'><img src='##IMAGES##title_target_l.gif' / vspace=5></td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom' align='center'><img src='##IMAGES##title_target_a.gif' / vspace=5></td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom' align='center'><img src='##IMAGES##title_target_b.gif' / vspace=5></td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom' align='center'><img src='##IMAGES##title_min_delta_l.gif' / vspace=5></td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom' align='center'><img src='##IMAGES##max_delta_l.gif' / vspace=5></td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom' align='center'><img src='##IMAGES##title_ave_delta_l.gif' / vspace=5></td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom' align='center'><img src='##IMAGES##title_delta_a.gif' / vspace=5></td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom' align='center'><img src ='##IMAGES##title_max_delta_a.gif' / vspace=5></td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom' align='center'><img src ='##IMAGES##title_min_delta_b.gif' / vspace=5></td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom' align='center'><img src='##IMAGES##title_max_delta_b.gif' / vspace=5></td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom' align='center'><img src='##IMAGES##title_min_delta_e.gif' / vspace=5 ></td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom' align='center'><img src='##IMAGES##title_max_delta_e.gif' / vspace=5> </td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom' align='center'><img src='##IMAGES##title_ave_Delta_e.gif' / vspace=5> </td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom' align='center'><img src='##IMAGES##title_stddev_deltae.gif'/vspace=5> </td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom' align='center'><img src='##IMAGES##title_AVEDensity.gif' / vspace=5> </td>";
            strReportHeader += "<td style='width:5%;background-color:#D6E1C1;' valign='bottom' align='center'><img src='##IMAGES##title_STDDevDensity.gif' / vspace=5> </td>";
            strReportHeader += "</tr>";

            #endregion


            str = str.Replace("###ReportHeader###", strPageHeader);

            string HTMLstr = "";

            #region ====  Search Criteria  ====
            if (DS_Common.Tables["SearchCriteria"] != null)
            {
                for (int i = 0; i < DS_Common.Tables["SearchCriteria"].Rows.Count; i++)
                {
                    if ((i % 2) == 0)
                        HTMLstr += "<tr class=\"GRIDEVEN\" > ";
                    else
                        HTMLstr += "<tr class=\"GRIDODD\" > ";

                    HTMLstr += "<td class=\"BOLD1\" align=\"center\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["SearchField_"].ToString() + "</td>";
                    HTMLstr += "<td align=\"center\" class=\"BOLD1\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["Operator_"].ToString() + "</td>";
                    HTMLstr += "<td align=\"center\" class=\"BOLD1\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["Criteria_"].ToString() + "</td>";
                    HTMLstr += "</tr>";
                }
                str = str.Replace("##SearchCriteria##", HTMLstr);
            }
            else
            { str = str.Replace("##SearchCriteria##", " "); }
            #endregion


            #region ====  Patch Detail  ====
            HTMLstr = "";
            int Count_Data = 0;

            if (DS_Common.Tables["Patch_Details"] != null && DS_Common.Tables["Patch_Details"].Rows.Count > 0)
            {
                Count_Data = DS_Common.Tables["Patch_Details"].Rows.Count;

                for (int i = 0; i < Count_Data; i++)
                {

                    if (i == 0)
                    {
                        HTMLstr += strReportHeader;// ==== Add Header at starting of the table=== //
                    }
                    else if ((i % 29 == 0) && (i / 29 == 1)) // === Add header after adding 32 lines in first page === //
                    {
                        // === Finish previous Table tag and start with new table tag === //
                        HTMLstr += "</table>";
                        // === Add Page Header === //
                        HTMLstr += strPageHeader;
                        // === Add Report Table Header === //
                        HTMLstr += strReportHeader;

                    }
                    else if ((i - 29) % 35 == 0) // === remaining pdf pages contains 35 lines per page === //
                    {
                        // === Finish previous Table tag and start with new table tag === //
                        HTMLstr += "</table>";
                        // === Add Page Header === //
                        HTMLstr += strPageHeader;
                        // === Add Report Table Header === //
                        HTMLstr += strReportHeader;
                    }

                    // === Set alternating row style === //
                    if ((i % 2) == 0)
                        HTMLstr += "<tr class=\"GRIDEVEN\" > ";
                    else
                        HTMLstr += "<tr class=\"GRIDODD\" > ";

                    string lbl = "<span style='background-color:" + ColorCalcuations.LabtoRGB(Convert.ToDouble(DS_Common.Tables["Patch_Details"].Rows[i]["LAB_L1"]), Convert.ToDouble(DS_Common.Tables["Patch_Details"].Rows[i]["LAB_A1"]), Convert.ToDouble(DS_Common.Tables["Patch_Details"].Rows[i]["LAB_B1"])) + "'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                    HTMLstr += "<td align=\"left\">" + lbl + "</td> ";
                    HTMLstr += "<td align=\"left\">" + DS_Common.Tables["Patch_Details"].Rows[i]["Sample_Name"].ToString() + "</td> ";
                    HTMLstr += "<td align=\"Right\">" + DS_Common.Tables["Patch_Details"].Rows[i]["CMYK_C"].ToString() + "</td>";
                    HTMLstr += "<td align=\"Right\">" + DS_Common.Tables["Patch_Details"].Rows[i]["CMYK_M"].ToString() + "</td>";
                    HTMLstr += "<td align=\"Right\">" + DS_Common.Tables["Patch_Details"].Rows[i]["CMYK_Y"].ToString() + "</td>";
                    HTMLstr += "<td align=\"Right\">" + DS_Common.Tables["Patch_Details"].Rows[i]["CMYK_K"].ToString() + "</td>";
                    HTMLstr += "<td align=\"Right\">" + DS_Common.Tables["Patch_Details"].Rows[i]["LAB_L1"].ToString() + "</td>";
                    HTMLstr += "<td align=\"Right\">" + DS_Common.Tables["Patch_Details"].Rows[i]["LAB_A1"].ToString() + "</td>";
                    HTMLstr += "<td align=\"Right\">" + DS_Common.Tables["Patch_Details"].Rows[i]["LAB_B1"].ToString() + "</td>";
                    HTMLstr += "<td align=\"Right\">" + Convert.ToDouble(DS_Common.Tables["Patch_Details"].Rows[i]["MinDL"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Right\">" + Convert.ToDouble(DS_Common.Tables["Patch_Details"].Rows[i]["MaxDL"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Right\">" + Convert.ToDouble(DS_Common.Tables["Patch_Details"].Rows[i]["AvgDL"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Right\">" + Convert.ToDouble(DS_Common.Tables["Patch_Details"].Rows[i]["MinDA"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Right\">" + Convert.ToDouble(DS_Common.Tables["Patch_Details"].Rows[i]["MaxDA"].ToString()).ToString("00.00") + "</td>";
                    //HTMLstr += "<td align=\"Right\">" + Convert.ToDouble(DS_Common.Tables["Patch_Details"].Rows[i]["AvgDA"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Right\">" + Convert.ToDouble(DS_Common.Tables["Patch_Details"].Rows[i]["MinDB"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Right\">" + Convert.ToDouble(DS_Common.Tables["Patch_Details"].Rows[i]["MaxDB"].ToString()).ToString("00.00") + "</td>";
                    //HTMLstr += "<td align=\"Right\">" + Convert.ToDouble(DS_Common.Tables["Patch_Details"].Rows[i]["AvgDB"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Right\">" + Convert.ToDouble(DS_Common.Tables["Patch_Details"].Rows[i]["MinDE"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Right\">" + Convert.ToDouble(DS_Common.Tables["Patch_Details"].Rows[i]["MaxDE"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Right\">" + Convert.ToDouble(DS_Common.Tables["Patch_Details"].Rows[i]["AvgDE"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Right\">" + Convert.ToDouble(DS_Common.Tables["Patch_Details"].Rows[i]["STDDEV_DE"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Right\">" + Convert.ToDouble(DS_Common.Tables["Patch_Details"].Rows[i]["AveDensity"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "<td align=\"Right\">" + Convert.ToDouble(DS_Common.Tables["Patch_Details"].Rows[i]["StdDevDensity"].ToString()).ToString("00.00") + "</td>";
                    HTMLstr += "</tr>";
                }
                // === add last table' closing tag === //
                HTMLstr += "</table>";

                // === fit generated report in to template === //
                str = str.Replace("##PatchDetail##", HTMLstr);
            }
            else
            { str = str.Replace("##PatchDetail##", " "); }


            // === replace basic information in generated structure === //
            if (DS_Common.Tables["BasicInfo"] != null && DS_Common.Tables["BasicInfo"].Rows.Count > 0)
            {
                str = str.Replace("##DeviceName##", DS_Common.Tables["BasicInfo"].Rows[0]["DeviceName_"].ToString().Trim());
                str = str.Replace("##BranchName##", DS_Common.Tables["BasicInfo"].Rows[0]["BranchName_"].ToString().Trim());
                str = str.Replace("##DateRange##", DS_Common.Tables["BasicInfo"].Rows[0]["DateRange_"].ToString().Trim());
            }
            else
            {
                str = str.Replace("##DeviceName##", "");
                str = str.Replace("##BranchName##", "");
                str = str.Replace("##DateRange##", "");
            }

            str = str.Replace("~", System.Web.HttpContext.Current.Server.MapPath(".") + "\\");
            #endregion

            #region Assign Server
            str = str.Replace("##IMAGES##", strServer);
            #endregion

            return str;
        }
        #endregion

        #region Report 7 : Productivity Report
        private string TextReturn_ProductivityReport(string str)
        {
            str = str.Replace("#csspath#", CSSPath);

            if (DS_Common.Tables["BasicInfo"] != null && DS_Common.Tables["BasicInfo"].Rows.Count > 0)
            {
                str = str.Replace("##DongleName##", DS_Common.Tables["BasicInfo"].Rows[0]["DongleName_"].ToString().Trim());
                str = str.Replace("##CompanyName##", DS_Common.Tables["BasicInfo"].Rows[0]["CompanyName_"].ToString().Trim());
                str = str.Replace("##BranchName##", DS_Common.Tables["BasicInfo"].Rows[0]["BranchName_"].ToString().Trim());
                str = str.Replace("##DateRange##", DS_Common.Tables["BasicInfo"].Rows[0]["DateRange_"].ToString().Trim());
            }
            else
            {
                str = str.Replace("##DongleName##", "");
                str = str.Replace("##CompanyName##", "");
                str = str.Replace("##BranchName##", "");
                str = str.Replace("##DateRange##", "");
            }

            string HTMLstr = "";

            #region ====  Search Criteria  ====
            if (DS_Common.Tables["SearchCriteria"] != null)
            {

                for (int i = 0; i < DS_Common.Tables["SearchCriteria"].Rows.Count; i++)
                {
                    if ((i % 2) == 0)
                    {
                        HTMLstr += "<tr class=\"GRIDEVEN\" > ";
                    }
                    else
                    {
                        HTMLstr += "<tr class=\"GRIDODD\" > ";
                    }


                    HTMLstr += "<td class=\"BOLD1\" align=\"center\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["SearchField_"].ToString() + "</td>";
                    HTMLstr += "<td align=\"center\" class=\"BOLD1\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["Operator_"].ToString() + "</td>";
                    HTMLstr += "<td align=\"center\" class=\"BOLD1\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["Criteria_"].ToString() + "</td>";
                    HTMLstr += "</tr>";
                }

                str = str.Replace("##SearchCriteria##", HTMLstr);
            }
            else
            { str = str.Replace("##SearchCriteria##", " "); }
            #endregion

            #region ====  Productivity Detail  ====
            if (Dg.Items.Count > 0)
            {
                HTMLstr = "";

                for (int i = 0; i < Dg.Items.Count; i++)
                {
                    if ((i % 2) == 0)
                    {
                        HTMLstr += "<tr class=\"GRIDEVEN\" > ";
                    }
                    else
                    {
                        HTMLstr += "<tr class=\"GRIDODD\" > ";
                    }

                    HTMLstr += "<td align=\"left\">" + Dg.Items[i].Cells[0].Text.Trim() + "</td> ";
                    HTMLstr += "<td align=\"left\">" + Dg.Items[i].Cells[1].Text.Trim() + "</td> ";
                    HTMLstr += "<td align=\"center\">" + Dg.Items[i].Cells[2].Text.Trim() + "</td>";
                    HTMLstr += "<td align=\"center\">" + Dg.Items[i].Cells[3].Text.Trim() + "</td>";
                    HTMLstr += "<td align=\"center\">" + ((Label)Dg.Items[i].FindControl("Lbl_UniqueJobs")).Text + "</td>";
                    HTMLstr += "<td align=\"center\">" + Dg.Items[i].Cells[5].Text.Trim() + "</td>";
                    HTMLstr += "<td align=\"center\">" + ((Label)Dg.Items[i].FindControl("Lbl_Reruns")).Text + "</td>";
                    HTMLstr += "<td align=\"center\">" + Dg.Items[i].Cells[7].Text.Trim() + "</td>";
                    HTMLstr += "<td align=\"center\">" + ((Label)Dg.Items[i].FindControl("Lbl_RedState")).Text + "</td>";
                    HTMLstr += "<td align=\"center\">" + ((Label)Dg.Items[i].FindControl("Lbl_RedStatePer100")).Text + "%</td>";


                    HTMLstr += "</tr>";

                }
                str = str.Replace("##Productivity##", HTMLstr);
            }
            else
            { str = str.Replace("##Productivity##", " "); }
            #endregion

            #region Assign Server
            str = str.Replace("##IMAGES##", strServer);
            #endregion

            return str;
        }
        #endregion

        #region Report 8 : Color Adherence Report / Report Performance Summary
        private string TextReturn_ReportPerformanceSummary(string str)
        {
            str = str.Replace("#csspath#", CSSPath);

            if (DS_Common.Tables["BasicInfo"] != null && DS_Common.Tables["BasicInfo"].Rows.Count > 0)
            {

                str = str.Replace("##CompanyName##", DS_Common.Tables["BasicInfo"].Rows[0]["CompanyName_"].ToString().Trim());
                str = str.Replace("##BranchName##", DS_Common.Tables["BasicInfo"].Rows[0]["BranchName_"].ToString().Trim());
                str = str.Replace("##DateRange##", DS_Common.Tables["BasicInfo"].Rows[0]["DateRange_"].ToString().Trim());
            }
            else
            {
                str = str.Replace("##CompanyName##", "");
                str = str.Replace("##BranchName##", "");
                str = str.Replace("##DateRange##", "");
            }

            string HTMLstr = "";
            string HTMLstr1 = "";
            string HTMLstr2 = "";

            #region ====  Search Criteria  ====

            if (DS_Common.Tables["SearchCriteria"] != null)
            {

                for (int i = 0; i < DS_Common.Tables["SearchCriteria"].Rows.Count; i++)
                {
                    if ((i % 2) == 0)
                    {
                        HTMLstr1 += "<tr class=\"GRIDEVEN\" > ";
                        HTMLstr2 += "<tr class=\"GRIDEVEN\" > ";
                    }
                    else
                    {
                        HTMLstr1 += "<tr class=\"GRIDODD\" > ";
                        HTMLstr2 += "<tr class=\"GRIDODD\" > ";
                    }

                    HTMLstr1 += "<td align=\"left\" class=\"BOLD1\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["Parameter_"].ToString() + "</td> ";
                    HTMLstr1 += "<td align=\"center\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["Result_"].ToString() + "</td> ";
                    HTMLstr1 += "</tr>";

                    HTMLstr2 += "<td class=\"BOLD1\" align=\"center\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["SearchField_"].ToString() + "</td>";
                    HTMLstr2 += "<td align=\"center\" class=\"BOLD1\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["Operator_"].ToString() + "</td>";
                    HTMLstr2 += "<td align=\"center\" class=\"BOLD1\">" + DS_Common.Tables["SearchCriteria"].Rows[i]["Criteria_"].ToString() + "</td>";
                    HTMLstr2 += "</tr>";
                }

                str = str.Replace("##SearchCriteria1##", HTMLstr1);
                str = str.Replace("##SearchCriteria2##", HTMLstr2);
            }
            else
            {
                str = str.Replace("##SearchCriteria##", " ");

            }
            #endregion

            #region ##REPLACE_THE_BRACHES##
            string BranchInfo = Sessions.GetSession("RPT-8-BranchInfo").ToString();
            str = str.Replace("##REPLACE_THE_BRACHES##", BranchInfo);
            #endregion

            #region Print Buyer
            //##PRINT_BUYER##
            if (Sessions.RoleDesc == Enums.RoleType.PrintBuyer)
            {
                string PBName = "<td class=\"BOLD1\" align=\"left\" width=\"20%\">Print Buyer</td><td align=\"left\">" + Sessions.FirstName + " " + Sessions.LastName + "</td>";
                str = str.Replace("##PRINT_BUYER##", PBName);
            }
            else
            { str = str.Replace("##PRINT_BUYER##", ""); }
            #endregion

            #region Assign Server
            str = str.Replace("##IMAGES##", strServer);
            #endregion

            return str;
        }
        #endregion

        #region Report 9 : Target Measurement Report
        private string TextReturn_ReportTargetMeasurement(string str)
        {
            // === Image string to replace in chart tables === //
            string strStatusImage = "";
            string strStatusImageGreen = "<img src=\"##IMAGES##Proofok.gif\" width='13px' height='13px' >";
            string strStatusImageRed = "<img src=\"##IMAGES##ProofFailed.gif\" width='13px' height='13px' >";

            DataTable DT = new DataTable();

            str = str.Replace("#csspath#", CSSPath);

            #region ====  Job Detail  ====
            if (DS_Common.Tables[0] != null && DS_Common.Tables[0].Rows.Count > 0)
            {
                DT = DS_Common.Tables[0];

                str = str.Replace("###JobName###", Convert.ToString(DT.Rows[0]["Job"]).Trim());
                str = str.Replace("###BranchName###", Convert.ToString(DT.Rows[0]["BranchName"]).Trim());
                str = str.Replace("###TimeStamp###", Convert.ToString(DT.Rows[0]["CreatedDT"]).Trim());
                str = str.Replace("###Substrate###", Convert.ToString(DT.Rows[0]["Substrate"]).Trim());
                str = str.Replace("###Device###", Convert.ToString(DT.Rows[0]["CPDeviceDesc"]).Trim());
                str = str.Replace("###DeltaEFormula###", Convert.ToString(DT.Rows[0]["DeltaEFormula"]).Trim());


                int intStatus = Convert.ToInt32(DT.Rows[0]["Status"]);
                string strHeaderStatusImage = "";

                if (intStatus == 0)
                {
                    strHeaderStatusImage = "<img src=\"##IMAGES##ProofFailed.gif\" alt=\"Job Out of Tolerance\">&nbsp;<b>Error</b>";//Out of Tolerance
                    strStatusImage = "<img src=\"##IMAGES##ProofFailed.gif\" width='13px' height='13px' >";
                }
                else if (intStatus == 1)
                {
                    strHeaderStatusImage = "<img src=\"##IMAGES##ProofWarning.gif\" alt=\"Measurement Warning\">&nbsp;<b>Caution</b>";//Warning
                    strStatusImage = "<img src=\"##IMAGES##ProofWarning.gif\" width='13px' height='13px' >";
                }
                else
                {
                    strHeaderStatusImage = "<img src=\"##IMAGES##Proofok.gif\" alt=\"Measurement Passed\">&nbsp;<b>Passed</b><div>";
                    strStatusImage = "<img src=\"##IMAGES##Proofok.gif\" width='13px' height='13px' >";
                }

                str = str.Replace("###StatusImage###", strHeaderStatusImage);
            }
            else
            {
                str = str.Replace("###JobName###", "");
                str = str.Replace("###StatusImage###", "");
                str = str.Replace("###BranchName###", "");
                str = str.Replace("###TimeStamp###", "");
                str = str.Replace("###Substrate###", "");
                str = str.Replace("###Device###", "");
                str = str.Replace("###DeltaEFormula###", "");
            }
            #endregion

            #region ==== Target Vs Actual AB Plot ====
            // === Report-1 : Target Versus Actual AB === //
            if (DS_Common.Tables[1] != null && DS_Common.Tables[1].Rows.Count > 0)
                str = str.Replace("###TargetVersusActualAB###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_TargetMeasurement_TargetVersusActualAB_1.Jpeg");
            else
                str = str.Replace("###TargetVersusActualAB###", "");

            #endregion

            #region ==== Table = 2 Summary Delta (Upper Part with 7 rows data)  ====
            if (DS_Common.Tables[2] != null && DS_Common.Tables[2].Rows.Count > 0)
            {
                DT = DS_Common.Tables[2];

                //str = str.Replace("###Status1###", strStatusImage);

                Double ActualValue = 0;
                Double ThresholdValue = 0;

                ActualValue = Convert.ToString(DT.Rows[0]["AverageDE_ACT"]) == "" ? 0 : Convert.ToDouble(DT.Rows[0]["AverageDE_ACT"]);
                ThresholdValue = Convert.ToString(DT.Rows[0]["AverageDE_TOL"]) == "" ? 0 : Convert.ToDouble(DT.Rows[0]["AverageDE_TOL"]);
                if (ActualValue > ThresholdValue)
                    strStatusImage = strStatusImageRed;
                else
                    strStatusImage = strStatusImageGreen;
                strStatusImage = ActualValue == 0 ? "&nbsp;" : strStatusImage;
                str = str.Replace("###AverageStatus1###", strStatusImage);
                str = str.Replace("###ActualAverage###", Convert.ToString(DT.Rows[0]["AverageDE_ACT"]).Trim());
                str = str.Replace("###ThresholdAverage###", Convert.ToString(DT.Rows[0]["AverageDE_TOL"]).Trim());

                ActualValue = Convert.ToString(DT.Rows[0]["MaximumDE_ACT"]) == "" ? 0 : Convert.ToDouble(DT.Rows[0]["MaximumDE_ACT"]);
                ThresholdValue = Convert.ToString(DT.Rows[0]["MaximumDE_TOL"]) == "" ? 0 : Convert.ToDouble(DT.Rows[0]["MaximumDE_TOL"]);
                if (ActualValue > ThresholdValue)
                    strStatusImage = strStatusImageRed;
                else
                    strStatusImage = strStatusImageGreen;
                strStatusImage = ActualValue == 0 ? "&nbsp;" : strStatusImage;
                str = str.Replace("###MaximumStatus1###", strStatusImage);
                str = str.Replace("###ActualMaximum###", Convert.ToString(DT.Rows[0]["MaximumDE_ACT"]).Trim());
                str = str.Replace("###ThresholdMaximum###", Convert.ToString(DT.Rows[0]["MaximumDE_TOL"]).Trim());

                ActualValue = Convert.ToString(DT.Rows[0]["PrimaryDE_ACT"]) == "" ? 0 : Convert.ToDouble(DT.Rows[0]["PrimaryDE_ACT"]);
                ThresholdValue = Convert.ToString(DT.Rows[0]["PrimaryDE_TOL"]) == "" ? 0 : Convert.ToDouble(DT.Rows[0]["PrimaryDE_TOL"]);
                if (ActualValue > ThresholdValue)
                    strStatusImage = strStatusImageRed;
                else
                    strStatusImage = strStatusImageGreen;
                strStatusImage = ActualValue == 0 ? "&nbsp;" : strStatusImage;
                str = str.Replace("###PrimaryDeltaEStatus1###", strStatusImage);
                str = str.Replace("###ActualPrimaryDeltaE###", Convert.ToString(DT.Rows[0]["PrimaryDE_ACT"]).Trim());
                str = str.Replace("###ThresholdPrimaryDeltaE###", Convert.ToString(DT.Rows[0]["PrimaryDE_TOL"]).Trim());

                ActualValue = Convert.ToString(DT.Rows[0]["PrimaryDH_ACT"]) == "" ? 0 : Convert.ToDouble(DT.Rows[0]["PrimaryDH_ACT"]);
                ThresholdValue = Convert.ToString(DT.Rows[0]["PrimaryDH_TOL"]) == "" ? 0 : Convert.ToDouble(DT.Rows[0]["PrimaryDH_TOL"]);
                if (ActualValue > ThresholdValue)
                    strStatusImage = strStatusImageRed;
                else
                    strStatusImage = strStatusImageGreen;
                strStatusImage = ActualValue == 0 ? "&nbsp;" : strStatusImage;
                str = str.Replace("###PrimaryDeltaHStatus1###", strStatusImage);
                str = str.Replace("###ActualPrimaryDeltaH###", Convert.ToString(DT.Rows[0]["PrimaryDH_ACT"]).Trim());
                str = str.Replace("###ThresholdPrimaryDeltaH###", Convert.ToString(DT.Rows[0]["PrimaryDH_TOL"]).Trim());

                ActualValue = Convert.ToString(DT.Rows[0]["GrayAverageDH_ACT"]) == "" ? 0 : Convert.ToDouble(DT.Rows[0]["GrayAverageDH_ACT"]);
                ThresholdValue = Convert.ToString(DT.Rows[0]["GrayAverageDH_TOL"]) == "" ? 0 : Convert.ToDouble(DT.Rows[0]["GrayAverageDH_TOL"]);
                if (ActualValue > ThresholdValue)
                    strStatusImage = strStatusImageRed;
                else
                    strStatusImage = strStatusImageGreen;
                strStatusImage = ActualValue == 0 ? "&nbsp;" : strStatusImage;
                str = str.Replace("###GrayAverageDeltaHStatus1###", strStatusImage);
                str = str.Replace("###ActualGrayAverageDeltaH###", Convert.ToString(DT.Rows[0]["GrayAverageDH_ACT"]).Trim());
                str = str.Replace("###ThresholdGrayAverageDeltaH###", Convert.ToString(DT.Rows[0]["GrayAverageDH_TOL"]).Trim());

                /// === These 2 rows does not exists in database but availabel in pdf so mapped here === ///
                //str = str.Replace("###ActualQuartile###", DS_Common.Tables[2].Rows[0]["Quartile_ACT"].ToString().Trim());
                //str = str.Replace("###ThresholdQuartile###", DS_Common.Tables[2].Rows[0]["Quartile_TOL"].ToString().Trim());

                //str = str.Replace("###ActualOuter###", DS_Common.Tables[2].Rows[0]["Quartile_ACT"].ToString().Trim());
                //str = str.Replace("###ThresholdOuter###", DS_Common.Tables[2].Rows[0]["Quartile_TOL"].ToString().Trim());

                str = str.Replace("###QuartileStatus1###", "&nbsp;");
                str = str.Replace("###ActualQuartile###", "");
                str = str.Replace("###ThresholdQuartile###", "");

                str = str.Replace("###OuterStatus1###", "&nbsp;");
                str = str.Replace("###ActualOuter###", "");
                str = str.Replace("###ThresholdOuter###", "");

                // === Report-2 : SummaryDelta === //
                str = str.Replace("###SummaryDelta_2###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_TargetMeasurement_SummaryDelta_2.Jpeg");

            }
            else
            {
                str = str.Replace("###Status1###", strStatusImage);

                str = str.Replace("###ActualAverage###", "");
                str = str.Replace("###ThresholdAverage###", "");
                str = str.Replace("###ActualMaximum###", "");
                str = str.Replace("###ThresholdMaximum###", "");
                str = str.Replace("###ActualPrimaryDeltaE###", "");
                str = str.Replace("###ThresholdPrimaryDeltaE###", "");
                str = str.Replace("###ActualPrimaryDeltaH###", "");
                str = str.Replace("###ThresholdPrimaryDeltaH###", "");
                str = str.Replace("###ActualGrayAverageDeltaH###", "");
                str = str.Replace("###ThresholdGrayAverageDeltaH###", "");
                str = str.Replace("###ActualQuartile###", "");
                str = str.Replace("###ThresholdQuartile###", "");
                str = str.Replace("###ActualOuter###", "");
                str = str.Replace("###ThresholdOuter###", "");

                // === Report-2 : SummaryDelta === //
                str = str.Replace("###SummaryDelta_2###", "");
            }
            #endregion

            #region ==== Table = 3 Delta E 2000 Values (Lower Part with 10 rows data)  ====


            if (DS_Common.Tables[3] != null && DS_Common.Tables[3].Rows.Count > 0)
            {
                DT = DS_Common.Tables[3];
                //str = str.Replace("###Status2###", strStatusImage);

                Double ActualValue = 0;
                Double ThresholdValue = 0;


                ActualValue = Convert.ToString(DT.Rows[0]["DE"]) == "" ? 0 : Convert.ToDouble(DT.Rows[0]["DE"]);
                ThresholdValue = Convert.ToString(DT.Rows[0]["TOL"]) == "" ? 0 : Convert.ToDouble(DT.Rows[0]["TOL"]);
                if (ActualValue > ThresholdValue)
                    strStatusImage = strStatusImageRed;
                else
                    strStatusImage = strStatusImageGreen;
                strStatusImage = ActualValue == 0 ? "&nbsp;" : strStatusImage;
                str = str.Replace("###CyanStatus2###", strStatusImage);
                str = str.Replace("###ActualCyan###", Convert.ToString(DT.Rows[0]["DE"]).Trim());
                str = str.Replace("###ThresholdCyan###", Convert.ToString(DT.Rows[0]["TOL"]).Trim());


                ActualValue = Convert.ToString(DT.Rows[1]["DE"]) == "" ? 0 : Convert.ToDouble(DT.Rows[1]["DE"]);
                ThresholdValue = Convert.ToString(DT.Rows[1]["TOL"]) == "" ? 0 : Convert.ToDouble(DT.Rows[1]["TOL"]);
                if (ActualValue > ThresholdValue)
                    strStatusImage = strStatusImageRed;
                else
                    strStatusImage = strStatusImageGreen;
                strStatusImage = ActualValue == 0 ? "&nbsp;" : strStatusImage;
                str = str.Replace("###MagentaStatus2###", strStatusImage);
                str = str.Replace("###ActualMagenta###", Convert.ToString(DT.Rows[1]["DE"]).Trim());
                str = str.Replace("###ThresholdMagenta###", Convert.ToString(DT.Rows[1]["TOL"]).Trim());

                ActualValue = Convert.ToString(DT.Rows[2]["DE"]) == "" ? 0 : Convert.ToDouble(DT.Rows[2]["DE"]);
                ThresholdValue = Convert.ToString(DT.Rows[2]["TOL"]) == "" ? 0 : Convert.ToDouble(DT.Rows[2]["TOL"]);
                if (ActualValue > ThresholdValue)
                    strStatusImage = strStatusImageRed;
                else
                    strStatusImage = strStatusImageGreen;
                strStatusImage = ActualValue == 0 ? "&nbsp;" : strStatusImage;
                str = str.Replace("###YellowStatus2###", strStatusImage);
                str = str.Replace("###ActualYellow###", Convert.ToString(DT.Rows[2]["DE"]).Trim());
                str = str.Replace("###ThresholdYellow###", Convert.ToString(DT.Rows[2]["TOL"]).Trim());

                ActualValue = Convert.ToString(DT.Rows[3]["DE"]) == "" ? 0 : Convert.ToDouble(DT.Rows[3]["DE"]);
                ThresholdValue = Convert.ToString(DT.Rows[3]["TOL"]) == "" ? 0 : Convert.ToDouble(DT.Rows[3]["TOL"]);
                if (ActualValue > ThresholdValue)
                    strStatusImage = strStatusImageRed;
                else
                    strStatusImage = strStatusImageGreen;
                strStatusImage = ActualValue == 0 ? "&nbsp;" : strStatusImage;
                str = str.Replace("###RedStatus2###", strStatusImage);
                str = str.Replace("###ActualRed###", Convert.ToString(DT.Rows[3]["DE"]).Trim());
                str = str.Replace("###ThresholdRed###", Convert.ToString(DT.Rows[3]["TOL"]).Trim());

                ActualValue = Convert.ToString(DT.Rows[4]["DE"]) == "" ? 0 : Convert.ToDouble(DT.Rows[4]["DE"]);
                ThresholdValue = Convert.ToString(DT.Rows[4]["TOL"]) == "" ? 0 : Convert.ToDouble(DT.Rows[4]["TOL"]);
                if (ActualValue > ThresholdValue)
                    strStatusImage = strStatusImageRed;
                else
                    strStatusImage = strStatusImageGreen;
                strStatusImage = ActualValue == 0 ? "&nbsp;" : strStatusImage;
                str = str.Replace("###GreenStatus2###", strStatusImage);
                str = str.Replace("###ActualGreen###", Convert.ToString(DT.Rows[4]["DE"]).Trim());
                str = str.Replace("###ThresholdGreen###", Convert.ToString(DT.Rows[4]["TOL"]).Trim());

                ActualValue = Convert.ToString(DT.Rows[5]["DE"]) == "" ? 0 : Convert.ToDouble(DT.Rows[5]["DE"]);
                ThresholdValue = Convert.ToString(DT.Rows[5]["TOL"]) == "" ? 0 : Convert.ToDouble(DT.Rows[5]["TOL"]);
                if (ActualValue > ThresholdValue)
                    strStatusImage = strStatusImageRed;
                else
                    strStatusImage = strStatusImageGreen;
                strStatusImage = ActualValue == 0 ? "&nbsp;" : strStatusImage;
                str = str.Replace("###BlueStatus2###", strStatusImage);
                str = str.Replace("###ActualBlue###", Convert.ToString(DT.Rows[5]["DE"]).Trim());
                str = str.Replace("###ThresholdBlue###", Convert.ToString(DT.Rows[5]["TOL"]).Trim());

                ActualValue = Convert.ToString(DT.Rows[6]["DE"]) == "" ? 0 : Convert.ToDouble(DT.Rows[6]["DE"]);
                ThresholdValue = Convert.ToString(DT.Rows[6]["TOL"]) == "" ? 0 : Convert.ToDouble(DT.Rows[6]["TOL"]);
                if (ActualValue > ThresholdValue)
                    strStatusImage = strStatusImageRed;
                else
                    strStatusImage = strStatusImageGreen;
                strStatusImage = ActualValue == 0 ? "&nbsp;" : strStatusImage;
                str = str.Replace("###SubstrateStatus2###", strStatusImage);
                str = str.Replace("###ActualSubstrate###", Convert.ToString(DT.Rows[6]["DE"]).Trim());
                str = str.Replace("###ThresholdSubstrate###", Convert.ToString(DT.Rows[6]["TOL"]).Trim());

                ActualValue = Convert.ToString(DT.Rows[7]["DE"]) == "" ? 0 : Convert.ToDouble(DT.Rows[7]["DE"]);
                ThresholdValue = Convert.ToString(DT.Rows[7]["TOL"]) == "" ? 0 : Convert.ToDouble(DT.Rows[7]["TOL"]);
                if (ActualValue > ThresholdValue)
                    strStatusImage = strStatusImageRed;
                else
                    strStatusImage = strStatusImageGreen;
                strStatusImage = ActualValue == 0 ? "&nbsp;" : strStatusImage;
                str = str.Replace("###Gray25Status2###", strStatusImage);
                str = str.Replace("###ActualGray25###", Convert.ToString(DT.Rows[7]["DE"]).Trim());
                str = str.Replace("###ThresholdGray25###", Convert.ToString(DT.Rows[7]["TOL"]).Trim());

                ActualValue = Convert.ToString(DT.Rows[8]["DE"]) == "" ? 0 : Convert.ToDouble(DT.Rows[8]["DE"]);
                ThresholdValue = Convert.ToString(DT.Rows[8]["TOL"]) == "" ? 0 : Convert.ToDouble(DT.Rows[8]["TOL"]);
                if (ActualValue > ThresholdValue)
                    strStatusImage = strStatusImageRed;
                else
                    strStatusImage = strStatusImageGreen;
                strStatusImage = ActualValue == 0 ? "&nbsp;" : strStatusImage;
                str = str.Replace("###Gray50Status2###", strStatusImage);
                str = str.Replace("###ActualGray50###", Convert.ToString(DT.Rows[8]["DE"]).Trim());
                str = str.Replace("###ThresholdGray50###", Convert.ToString(DT.Rows[8]["TOL"]).Trim());

                ActualValue = Convert.ToString(DT.Rows[9]["DE"]) == "" ? 0 : Convert.ToDouble(DT.Rows[9]["DE"]);
                ThresholdValue = Convert.ToString(DT.Rows[9]["TOL"]) == "" ? 0 : Convert.ToDouble(DT.Rows[9]["TOL"]);
                if (ActualValue > ThresholdValue)
                    strStatusImage = strStatusImageRed;
                else
                    strStatusImage = strStatusImageGreen;
                strStatusImage = ActualValue == 0 ? "&nbsp;" : strStatusImage;
                str = str.Replace("###Gray75Status2###", strStatusImage);
                str = str.Replace("###ActualGray75###", Convert.ToString(DT.Rows[9]["DE"]).Trim());
                str = str.Replace("###ThresholdGray75###", Convert.ToString(DT.Rows[9]["TOL"]).Trim());

                // === Report-2 : Chart Delta E 2000 === //
                str = str.Replace("###ChartDeltaE2000###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_TargetMeasurement_ChartDeltaE2000_3.Jpeg");

            }
            else
            {
                str = str.Replace("###Status2###", strStatusImage);

                str = str.Replace("###ActualCyan###", "");
                str = str.Replace("###ThresholdCyan###", "");
                str = str.Replace("###ActualMagenta###", "");
                str = str.Replace("###ThresholdMagenta###", "");
                str = str.Replace("###ActualYellow###", "");
                str = str.Replace("###ThresholdYellow###", "");
                str = str.Replace("###ActualRed###", "");
                str = str.Replace("###ThresholdRed###", "");
                str = str.Replace("###ActualGreen###", "");
                str = str.Replace("###ThresholdGreen###", "");
                str = str.Replace("###ActualBlue###", "");
                str = str.Replace("###ThresholdBlue###", "");
                str = str.Replace("###ActualSubstrate###", "");
                str = str.Replace("###ThresholdSubstrate###", "");
                str = str.Replace("###ActualGray25###", "");
                str = str.Replace("###ThresholdGray25###", "");
                str = str.Replace("###ActualGray50###", "");
                str = str.Replace("###ThresholdGray50###", "");
                str = str.Replace("###ActualGray75###", "");
                str = str.Replace("###ThresholdGray75###", "");

                // === Report-2 : Chart Delta E 2000 === //
                str = str.Replace("###ChartDeltaE2000###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_TargetMeasurement_ChartDeltaE2000_3.Jpeg");
            }
            #endregion


            #region ==== Table = 4 Measurement Report (Patch)  ====
            if (DS_Common.Tables[4] != null && DS_Common.Tables[4].Rows.Count > 0)
            {

                string ReportHeader = "";
                string TableHeader = "";

                ReportHeader += "<table style='width:650px' align='center' cellpadding='0' cellspacing='0' border='0'>";
                ReportHeader += "<tr style='height:10px'><td colspan='3'></td></tr>";
                ReportHeader += "<tr><td width='30%'>&nbsp;</td>";
                ReportHeader += "<td align='center' width='40%' valign='middle' style='font-size: 14px; font-weight: bold'>Measurement Report</td>";
                ReportHeader += "<td align='right' width='30%'><img src='##IMAGES##powered-by.gif' alt='Powered By' id='imgPowered-by' /></td>";
                ReportHeader += "</tr><tr style='height:10px'><td colspan='3'></td></tr>";
                ReportHeader += "</table>";

                TableHeader += "<tr>";
                TableHeader += "<td align='center' valign='middle' class='TABLE6'>Color<br />T|A</td>";
                TableHeader += "<td align='center' valign='middle' class='TABLE6'>&Delta; E</td>";
                TableHeader += "<td align='center' valign='middle' class='TABLE6'>PatchID<br />#</td>";
                TableHeader += "<td align='center' valign='middle' class='TABLE6'>C</td>";
                TableHeader += "<td align='center' valign='middle' class='TABLE6'>M</td>";
                TableHeader += "<td align='center' valign='middle' class='TABLE6'>Y</td>";
                TableHeader += "<td align='center' valign='middle' class='TABLE6'>K</td>";
                TableHeader += "<td align='center' valign='middle' class='TABLE6'>Target<br />L</td>";
                TableHeader += "<td align='center' valign='middle' class='TABLE6'>Target<br />a</td>";
                TableHeader += "<td align='center' valign='middle' class='TABLE6'>Target<br />b</td>";
                TableHeader += "<td align='center' valign='middle' class='TABLE6'>Actual<br />L</td>";
                TableHeader += "<td align='center' valign='middle' class='TABLE6'>Actual<br />A</td>";
                TableHeader += "<td align='center' valign='middle' class='TABLE6'>Actual<br />B</td>";
                TableHeader += "</tr>";

                DT = DS_Common.Tables[4];

                string strPatch = string.Empty;
                StringBuilder strBuild = new StringBuilder();



                bool IsAlternate = false;

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (i == 0)
                    {
                        // ==== Add Header === //
                        strBuild.Append(ReportHeader);
                        strBuild.Append("<table cellspacing='0' cellpadding='0' style='border: SOLID 2PX #c4c4c4' width='650px'>");

                        // === Header Row with diff css and background color === //
                        strBuild.Append(TableHeader);
                    }
                    else if (i % 32 == 0)
                    {
                        strBuild.Append("</table>");

                        strBuild.Append(ReportHeader);
                        strBuild.Append("<table cellspacing='0' cellpadding='0' style='border: SOLID 2PX #c4c4c4' width='650px'>");
                        strBuild.Append(TableHeader);
                    }

                    // ==== First Column (T | A) Color Display in table structure === //
                    string strT = Formulas.ColorCalcuations.LabtoRGB(Convert.ToInt16(DT.Rows[i]["Target_L"]), Convert.ToInt16(DT.Rows[i]["Target_a"]), Convert.ToInt16(DT.Rows[i]["Target_b"]));
                    string strA = Formulas.ColorCalcuations.LabtoRGB(Convert.ToInt16(DT.Rows[i]["Actual_L"]), Convert.ToInt16(DT.Rows[i]["Actual_a"]), Convert.ToInt16(DT.Rows[i]["Actual_b"]));
                    string strColorTA = "<table cellspacing='2' width='100%'><tr style='height: 10px'>";
                    strColorTA += "<td style='width: 10px;background-color:" + strT + "'></td>";
                    strColorTA += "<td style='width: 10px;background-color:" + strA + "'></td>";
                    strColorTA += "</tr></table>";

                    // === Apply Alternating Row Style === ///
                    if (IsAlternate)
                        strBuild.Append("<tr style='background-color:#efefef'>");
                    else
                        strBuild.Append("<tr style='background-color:#ffffff'>");

                    string PatchStatusNo = Convert.ToString(DT.Rows[i]["PatchStatus"]).Trim();
                    string PatchStatusColor = "";
                    if (PatchStatusNo == "0")
                        PatchStatusColor = "style='background-color:#DA1B22'";// Red
                    else if (PatchStatusNo == "1")
                        PatchStatusColor = "style='background-color:#FFDD00'";// Yellow
                    else if (PatchStatusNo == "2")
                        PatchStatusColor = "";
                    //PatchStatusColor = "style='background-color:White'";

                    strBuild.Append("<td align='center' class='TABLE6CELL' valign='middle'>" + strColorTA + "</td>");
                    strBuild.Append("<td align='center' class='TABLE6CELL' valign='middle'>" + Convert.ToString(DT.Rows[i]["DE"]).Trim() + "</td>");
                    strBuild.Append("<td align='center' class='TABLE6CELL' valign='middle' " + PatchStatusColor + ">" + Convert.ToString(DT.Rows[i]["Sample_Name"]).Trim() + "</td>");
                    strBuild.Append("<td align='center' class='TABLE6CELL' valign='middle'>" + Convert.ToString(DT.Rows[i]["CMYK_C"]).Trim() + "</td>");
                    strBuild.Append("<td align='center' class='TABLE6CELL' valign='middle'>" + Convert.ToString(DT.Rows[i]["CMYK_M"]).Trim() + "</td>");
                    strBuild.Append("<td align='center' class='TABLE6CELL' valign='middle'>" + Convert.ToString(DT.Rows[i]["CMYK_Y"]).Trim() + "</td>");
                    strBuild.Append("<td align='center' class='TABLE6CELL' valign='middle'>" + Convert.ToString(DT.Rows[i]["CMYK_K"]).Trim() + "</td>");
                    strBuild.Append("<td align='center' class='TABLE6CELL' valign='middle'>" + Convert.ToString(DT.Rows[i]["Target_L"]).Trim() + "</td>");
                    strBuild.Append("<td align='center' class='TABLE6CELL' valign='middle'>" + Convert.ToString(DT.Rows[i]["Target_a"]).Trim() + "</td>");
                    strBuild.Append("<td align='center' class='TABLE6CELL' valign='middle'>" + Convert.ToString(DT.Rows[i]["Target_b"]).Trim() + "</td>");
                    strBuild.Append("<td align='center' class='TABLE6CELL' valign='middle'>" + Convert.ToString(DT.Rows[i]["Actual_L"]).Trim() + "</td>");
                    strBuild.Append("<td align='center' class='TABLE6CELL' valign='middle'>" + Convert.ToString(DT.Rows[i]["Actual_a"]).Trim() + "</td>");
                    strBuild.Append("<td align='center' class='TABLE6CELL' valign='middle'>" + Convert.ToString(DT.Rows[i]["Actual_b"]).Trim() + "</td>");
                    strBuild.Append("</tr>");

                    IsAlternate = !IsAlternate;
                }
                strBuild.Append("</table>");

                str = str.Replace("###PatchReport###", strBuild.ToString());
            }
            #endregion

            #region Assign Server
            str = str.Replace("##IMAGES##", strServer);
            #endregion

            return str;
        }
        #endregion



        //2009_06_15 by Softweb - Ajay //Start New Code//
        #region Report 10 : Job Run Certification Process Report
        private string TextReturn_ReportJobRunCertificationProcess(string str)
        {

            // === Image string to replace in chart tables === //
            string strStatusImageGreen = "<img src=\"##IMAGES##Proofok.gif\" width='13px' height='13px' >";
            string strStatusImageRed = "<img src=\"##IMAGES##ProofFailed.gif\" width='13px' height='13px' >";
            string strStatusImageCaution = "<img src=\"##IMAGES##ProofWarning.gif\" width='13px' height='13px' >";

            // ===== Check Error Show Or Not ==== //
            if (DS_Common.Tables["IsShowError"] != null && DS_Common.Tables["IsShowError"].Rows.Count > 0 && Convert.ToString(DS_Common.Tables["IsShowError"].Rows[0]["IsShowError"]) == "0")
            {
                strStatusImageGreen = "";
                strStatusImageRed = "";
                strStatusImageCaution = "";
            }

            DataTable DT = new DataTable();

            str = str.Replace("#csspath#", CSSPath);

            // === Table = "JobDetail" : Replace Job Detail In HTML Tamplate ==== //    
            #region ==== Table = 0,  Job Detail  ====                
            if (DS_Common.Tables["JobDetail"] != null && DS_Common.Tables["JobDetail"].Rows.Count > 0)
            {
                DT = DS_Common.Tables["JobDetail"];

                str = str.Replace("###JobName###", Convert.ToString(DT.Rows[0]["JobName"]).Trim());
                str = str.Replace("###Location###", Convert.ToString(DT.Rows[0]["Location"]).Trim());
                str = str.Replace("###PressName###", Convert.ToString(DT.Rows[0]["PressName"]).Trim());
                str = str.Replace("###TimeStamp###", Convert.ToString(Convert.ToDateTime(DT.Rows[0]["JobCreatedDT"]).ToString("MM/dd/yy - hh:mm tt")).Trim());
                str = str.Replace("###Substrate###", Convert.ToString(DT.Rows[0]["Substrate"]).Trim());
            }
            else
            {
                str = str.Replace("###JobName###", "");
                str = str.Replace("###Location###", "");
                str = str.Replace("###PressName###", "");
                str = str.Replace("###TimeStamp###", "");
                str = str.Replace("###Substrate###", "");
            }
            #endregion

            // === Table = "InkInformation" : Replace Inl Information In HTML Tamplate  ==== //
            #region ==== Ink Detail ====            
            string strInkInformation = "";
            if (DS_Common.Tables["InkInformation"] != null && DS_Common.Tables["InkInformation"].Rows.Count > 0)
            {
                for (int i = 0; i < DS_Common.Tables["InkInformation"].Rows.Count; i++)
                    strInkInformation += "<tr><td>Ink " + Convert.ToString(DS_Common.Tables["InkInformation"].Rows[i]["InkWellNum"]) + "&nbsp;:</td><td>" + Convert.ToString(DS_Common.Tables["InkInformation"].Rows[i]["InkName"]) + "</td></tr>";
            }
            str = str.Replace("###InkInformation###", strInkInformation);
            #endregion

            #region ==== Target Vs Actual AB Plot ====
            str = str.Replace("###TargetVersusActualAB###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_TargetVersusActualAB_1.Jpeg");
            #endregion

            // === Table = "ErrorRate" : Target Versus Actual AB === //
            #region ==== Result Summary : Error ====
            str = str.Replace("###patchErrorImage###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_ResultSummary_PatchError.Jpeg");
            str = str.Replace("###ActualError###", Convert.ToString(DS_Common.Tables["ErrorRate"].Rows[0]["ErrorRate"]));
            #endregion

            // === Table = "DeltaESummary" : Target Versus Actual AB === //                    
            #region ==== Result Summary : Delta E AB Value ====            
            string strDeltaEABValue = "";
            if (DS_Common.Tables["DeltaESummary"] != null && DS_Common.Tables["DeltaESummary"].Rows.Count > 0)
            {

                string strStatusImg = "";
                for (int i = 0; i < DS_Common.Tables["DeltaESummary"].Rows.Count; i++)
                {
                    DataRow dr = DS_Common.Tables["DeltaESummary"].Rows[i];// === Copy row to minimize code length ===
                    if (Convert.ToInt32(dr["Result"]) == 1)
                        strStatusImg = strStatusImageRed;
                    else if (Convert.ToInt32(dr["Result"]) == 2)
                        strStatusImg = strStatusImageCaution;
                    else if (Convert.ToInt32(dr["Result"]) == 3)
                        strStatusImg = strStatusImageGreen;

                    strDeltaEABValue += "<tr style='height: 22px'>";
                    strDeltaEABValue += "<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>";
                    strDeltaEABValue += "<td valign='middle' style='font-size:8px' class='BottomBorder'>" + Convert.ToString(dr["ShortColorName"]) + "</td>";
                    strDeltaEABValue += "<td valign='middle' align='center' class='BottomBorder'>" + Convert.ToDecimal(dr["AvgDeltaE"]).ToString("N2") + "&nbsp;</td>";
                    strDeltaEABValue += "<td valign='middle' align='center' class='BottomBorder'>" + Convert.ToString(dr["DeltaE_L"]) + "&nbsp;</td>";
                    strDeltaEABValue += "</tr>";
                }
                str = str.Replace("###DeltaESummaryDetail###", strDeltaEABValue);
            }
            else
            {
                str = str.Replace("###DeltaESummaryDetail###", "");
            }
            str = str.Replace("###ResultSummary_DeltaEValue###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_ResultSummary_DeltaEValue.Jpeg");
            #endregion

            // === Gray Balance AB 25 === //
            #region ==== Gray Balance : Gray_25 ====
            str = str.Replace("###GrayBalanceAB25###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page2_AB25.Jpeg");
            #endregion

            // === Gray Balance AB 50 === //
            #region ==== Gray Balance : Gray_50 ====
            str = str.Replace("###GrayBalanceAB50###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page2_AB50.Jpeg");
            #endregion

            // === Gray Balance AB 75 === //
            #region ==== Gray Balance : Gray_75 ====
            str = str.Replace("###GrayBalanceAB75###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page2_AB75.Jpeg");
            #endregion

            // === Gray Balance L* Chart === //
            #region === Gray Balance L* Chart ===
            str = str.Replace("###GrayBalanceL###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page2_GrayBalance_L.Jpeg");
            #endregion


            // === Gray_NPD === //
            #region ==== Gray Balance : Gray_NPD ====
            str = str.Replace("###Page2_GrayNPD###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page2_GrayNPD.Jpeg");
            #endregion

            // === K_NPD === //
            #region ==== Gray Balance : K_NPD ====
            str = str.Replace("###Page2_KNPD###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page2_KNPD.Jpeg");
            #endregion


            //----------------- Page 3 ---------------- //

            //str = str.Replace("###Page3_TrendDeltaE###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page3_TrendDeltaE.Jpeg");
            str = str.Replace("###Page3_ZoneDeltaE###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page3_ZoneDeltaE.Jpeg");

            // === Process  Densitometric Summary === //
            #region ==== Process  Densitometric Summary  ====
            str = str.Replace("###Page3_DensityDeviationP###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DensitometricSummary_DensityDeviationP.Jpeg");
            str = str.Replace("###Page3_DotgainDeviationP###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DensitometricSummary_DotgainDeviationP.Jpeg");
            str = str.Replace("###Page3_PrintDeviationP###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DensitometricSummary_PrintDeviationP.Jpeg");
            str = str.Replace("###Page3_TrapDeviationP###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DensitometricSummary_TrapDeviationP.Jpeg");

            #endregion

            // === Job Trands Charts === //
            #region ===== Job Trands Charts ===== 
            str = str.Replace("###Page4_TrendLine###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_TrendLine.Jpeg");
            str = str.Replace("###Page4_DeltaE###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DeltaE.Jpeg");
            str = str.Replace("###Page4_Density###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Density.Jpeg");
            str = str.Replace("###Page4_Dotgain###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Dotgain.Jpeg");
            str = str.Replace("###Page4_PrintContrast###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_PrintContrast.Jpeg");
            #endregion


            #region Assign Server
            str = str.Replace("##IMAGES##", strServer);
            #endregion

            return str;
        }
        #endregion

        #region Report 11 : Job Run Certification Non Process Report
        private string TextReturn_ReportJobRunCertificationNonProcess(string str)
        {
            // === Image string to replace in chart tables === //
            string strStatusImageGreen = "<img src=\"##IMAGES##Proofok.gif\" width='13px' height='13px' >";
            string strStatusImageRed = "<img src=\"##IMAGES##ProofFailed.gif\" width='13px' height='13px' >";
            string strStatusImageCaution = "<img src=\"##IMAGES##ProofWarning.gif\" width='13px' height='13px' >";

            // ===== Check Error Show Or Not ==== //
            if (DS_Common.Tables["IsShowError"] != null && DS_Common.Tables["IsShowError"].Rows.Count > 0 && Convert.ToString(DS_Common.Tables["IsShowError"].Rows[0]["IsShowError"]) == "0")
            {
                strStatusImageGreen = "";
                strStatusImageRed = "";
                strStatusImageCaution = "";
            }


            DataTable DT = new DataTable();

            str = str.Replace("#csspath#", CSSPath);

            // === Table = "JobDetail" : Replace Job Detail In HTML Tamplate ==== //    
            #region ==== Table = 0,  Job Detail  ====            
            if (DS_Common.Tables["JobDetail"] != null && DS_Common.Tables["JobDetail"].Rows.Count > 0)
            {
                DT = DS_Common.Tables["JobDetail"];
                str = str.Replace("###JobName###", Convert.ToString(DT.Rows[0]["JobName"]).Trim());
                str = str.Replace("###Location###", Convert.ToString(DT.Rows[0]["Location"]).Trim());
                str = str.Replace("###PressName###", Convert.ToString(DT.Rows[0]["PressName"]).Trim());
                str = str.Replace("###TimeStamp###", Convert.ToString(Convert.ToDateTime(DT.Rows[0]["JobCreatedDT"]).ToString("MM/dd/yy - hh:mm tt")).Trim());
                str = str.Replace("###Substrate###", Convert.ToString(DT.Rows[0]["Substrate"]).Trim());
            }
            else
            {
                str = str.Replace("###JobName###", "");
                str = str.Replace("###Location###", "");
                str = str.Replace("###PressName###", "");
                str = str.Replace("###TimeStamp###", "");
                str = str.Replace("###Substrate###", "");
            }
            #endregion

            // === Table = "InkInformation" : Replace Inl Information In HTML Tamplate  ==== //
            #region ==== Ink Detail ====
            string strInkInformation = "";
            if (DS_Common.Tables["InkInformation"] != null && DS_Common.Tables["InkInformation"].Rows.Count > 0)
            {
                for (int i = 0; i < DS_Common.Tables["InkInformation"].Rows.Count; i++)
                    strInkInformation += "<tr><td>Ink " + Convert.ToString(DS_Common.Tables["InkInformation"].Rows[i]["InkWellNum"]) + "&nbsp;:</td><td>" + Convert.ToString(DS_Common.Tables["InkInformation"].Rows[i]["InkName"]) + "</td></tr>";
            }
            str = str.Replace("###InkInformation###", strInkInformation);
            #endregion

            #region ==== Target Vs Actual AB Plot ====
            str = str.Replace("###TargetVersusActualAB###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_TargetVersusActualAB_1.Jpeg");
            #endregion

            // === Table = "ErrorRate" : Target Versus Actual AB === //
            #region ==== Result Summary : Error ====
            str = str.Replace("###patchErrorImage###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_ResultSummary_PatchError.Jpeg");
            str = str.Replace("###ActualError###", Convert.ToString(DS_Common.Tables["ErrorRate"].Rows[0]["ErrorRate"]));
            #endregion

            // === Table = "DeltaESummary" : Target Versus Actual AB === // 
            #region ==== Result Summary : Delta E AB Value ====
            string strDeltaEABValue = "";
            if (DS_Common.Tables["DeltaESummary"] != null && DS_Common.Tables["DeltaESummary"].Rows.Count > 0)
            {
                string strStatusImg = "";
                for (int i = 0; i < DS_Common.Tables["DeltaESummary"].Rows.Count; i++)
                {
                    DataRow dr = DS_Common.Tables["DeltaESummary"].Rows[i];// === Copy row to minimize code length ===
                    if (Convert.ToInt32(dr["Result"]) == 1)
                        strStatusImg = strStatusImageRed;
                    else if (Convert.ToInt32(dr["Result"]) == 2)
                        strStatusImg = strStatusImageCaution;
                    else if (Convert.ToInt32(dr["Result"]) == 3)
                        strStatusImg = strStatusImageGreen;

                    strDeltaEABValue += "<tr style='height: 22px'>";
                    strDeltaEABValue += "<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>";
                    strDeltaEABValue += "<td valign='middle' style='font-size:8px' class='BottomBorder'>" + Convert.ToString(dr["ShortColorName"]) + "</td>";
                    strDeltaEABValue += "<td valign='middle' align='center' class='BottomBorder'>" + Convert.ToDecimal(dr["AvgDeltaE"]).ToString("N2") + "&nbsp;</td>";
                    strDeltaEABValue += "<td valign='middle' align='center' class='BottomBorder'>" + Convert.ToString(dr["DeltaE_L"]) + "&nbsp;</td>";
                    strDeltaEABValue += "</tr>";
                }
                str = str.Replace("###DeltaESummaryDetail###", strDeltaEABValue);
            }
            else
            {
                str = str.Replace("###DeltaESummaryDetail###", "");
            }
            str = str.Replace("###ResultSummary_DeltaEValue###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_ResultSummary_DeltaEValue.Jpeg");
            #endregion

            //----------------- Page 2 ---------------- //
            //str = str.Replace("###Page3_TrendDeltaE###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page3_TrendDeltaE.Jpeg");
            str = str.Replace("###Page3_ZoneDeltaE###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page3_ZoneDeltaE.Jpeg");

            // === Non Process  Densitometric Summary === //
            #region ==== Non Process  Densitometric Summary  ====
            str = str.Replace("###Page3_DensityDeviationNP###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DensitometricSummary_DensityDeviationNP.Jpeg");
            #endregion

            // === Job Trands Charts === //
            #region ===== Job Trands Charts ===== 
            str = str.Replace("###Page4_TrendLine###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_TrendLine.Jpeg");
            str = str.Replace("###Page4_DeltaE###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DeltaE.Jpeg");
            str = str.Replace("###Page4_Density###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Density.Jpeg");
            str = str.Replace("###Page4_Dotgain###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Dotgain.Jpeg");
            str = str.Replace("###Page4_PrintContrast###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_PrintContrast.Jpeg");
            #endregion

            //str = str.Replace("###DensitometricSummary###", strDensitometricSummary);

            #region Assign Server
            str = str.Replace("##IMAGES##", strServer);
            #endregion

            return str;
        }
        #endregion
        //2009_06_15 by Softweb - Ajay //End New Code//

        #region Report 12 : Report Press Make Ready 
        private string TextReturn_ReportMakeReady(string str)
        {
            str = str.Replace("#csspath#", CSSPath);
            string TmpRepCompanyDetail = "";

            // === Table = "MakeReadyDetail" : Replace Job Detail In HTML Tamplate ==== //    
            #region === Table = "MakeReadyDetail" :
            if (DS_Common.Tables["MakeReadyDetail"] != null && DS_Common.Tables["MakeReadyDetail"].Rows.Count > 0)
            {
                str = str.Replace("###DateRange###", Convert.ToString(DS_Common.Tables["MakeReadyDetail"].Rows[0]["DateRange"]).Trim());
                str = str.Replace("###TimeRange###", Convert.ToString(DS_Common.Tables["MakeReadyDetail"].Rows[0]["TimeRange"]).Trim());
                str = str.Replace("###CompanyName###", Convert.ToString(DS_Common.Tables["MakeReadyDetail"].Rows[0]["CompanyName"]).Trim());
                str = str.Replace("###BranchName###", Convert.ToString(DS_Common.Tables["MakeReadyDetail"].Rows[0]["BranchName"]).Trim());
                str = str.Replace("###JobRunName###", Convert.ToString(DS_Common.Tables["MakeReadyDetail"].Rows[0]["JobRunName"]).Trim());
                str = str.Replace("###PressName###", Convert.ToString(DS_Common.Tables["MakeReadyDetail"].Rows[0]["PressName"]).Trim());
                if (Convert.ToString(DS_Common.Tables["MakeReadyDetail"].Rows[0]["CompanySummary"]).Trim() != "")
                {
                    str = str.Replace("###Start###", "");
                    str = str.Replace("###End###", "");
                    str = str.Replace("###CompanyDetail###", Convert.ToString(DS_Common.Tables["MakeReadyDetail"].Rows[0]["CompanySummary"]).Trim());
                }
                else
                {
                    TmpRepCompanyDetail = str.Substring(str.IndexOf("###Start###"));
                    TmpRepCompanyDetail = TmpRepCompanyDetail.Substring(0, TmpRepCompanyDetail.IndexOf("###End###") + 9);
                    str = str.Replace(TmpRepCompanyDetail, "");
                }
                str = str.Replace("###BranchDetail###", Convert.ToString(DS_Common.Tables["MakeReadyDetail"].Rows[0]["BranchSummary"]).Trim());
            }
            else
            {
                str = str.Replace("###DateRange###", "");
                str = str.Replace("###TimeRange###", "");
                str = str.Replace("###CompanyName###", "");
                str = str.Replace("###BranchName###", "");
                str = str.Replace("###JobRunName###", "");
                str = str.Replace("###PressName###", "");
                str = str.Replace("###CompanyDetail###", "");
                str = str.Replace("###BranchDetail###", "");
            }
            #endregion


            #region Assign Server
            str = str.Replace("##IMAGES##", strServer);
            #endregion


            return str;
        }
        #endregion


        #region Report 13 : Single Measureament Report
        private StringBuilder TextReturn_ReportSingleMeasurement(StringBuilder SBStr)
        {
            // === Image string to replace in chart tables === //
            string strStatusImageGreen = "<img src=\"##IMAGES##Proofok.gif\" width='13px' height='13px' >";
            string strStatusImageRed = "<img src=\"##IMAGES##ProofFailed.gif\" width='13px' height='13px' >";
            string strStatusImageCaution = "<img src=\"##IMAGES##ProofWarning.gif\" width='13px' height='13px' >";

            string strJobStatus = "";

            DataTable DT = new DataTable();

            SBStr.Replace("#csspath#", CSSPath);

            // === Table 0 = "JobDetail" : Replace Job Detail In HTML Tamplate ==== //    
            //CommonFunctions.FileTimeLog("Start Page 1 JobDetail", false);

            #region ==== Table = 0,  Job Detail  ====
            if (DS_Common.Tables[0] != null && DS_Common.Tables[0].Rows.Count > 0)
            {
                DT = DS_Common.Tables[0];

                if (Convert.ToInt16(DT.Rows[0]["Status"]) == 0)
                    strJobStatus = "<table border='0' align='left' cellpadding='0' cellspacing='0' class='failed-title-bg' ><tr><td width='47'><img src='##IMAGES##failed-icon.gif' style='margin: 7px 0 0 5px;' /></td><td class='failed-title-text'>Failed</td></tr></table>"; // "<img src=\"##IMAGES##ProofFailed.gif\" width='20px' height='20px' >" + "&nbspFailed";
                else if (Convert.ToInt16(DT.Rows[0]["Status"]) == 1)
                    strJobStatus = "<table border='0' align='left' cellpadding='0' cellspacing='0' class='warning-title-bg' ><tr><td width='47'><img src='##IMAGES##warning-icon.gif' style='margin: 7px 0 0 5px;' /></td><td class='warning-title-text'>Caution</td></tr></table>"; // "<img src=\"##IMAGES##ProofWarning.gif\" width='20px' height='20px' >" + "&nbspCaution";
                else if (Convert.ToInt16(DT.Rows[0]["Status"]) == 2)
                    strJobStatus = "<table border='0' align='left' cellpadding='0' cellspacing='0' class='passed-title-bg' ><img src='##IMAGES##passed-icon.gif' style='margin: 7px 0 0 5px;' /></td><td class='passed-title-text'>Passed</td></tr></table>"; // "<img src=\"##IMAGES##Proofok.gif\" width='20px' height='20px' >" + "&nbspPassed";

                SBStr.Replace("###JobStatus###", strJobStatus);


                string Substrate = Convert.ToString(DT.Rows[0]["Substrate"]).Trim();
                string Ink = Convert.ToString(DT.Rows[0]["Ink"]).Trim();
                string Location = Convert.ToString(DT.Rows[0]["CompanyName"]).Trim();

                Substrate = (Substrate.Trim() != "" ? Substrate.Trim() + (Convert.ToString(DT.Rows[0]["Substrate_SKU"]).Trim() != "" ? " - " + Convert.ToString(DT.Rows[0]["Substrate_SKU"]) : "") : (Convert.ToString(DT.Rows[0]["Substrate_SKU"]).Trim() != "" ? Convert.ToString(DT.Rows[0]["Substrate_SKU"]) : ""));
                Substrate = (Substrate.Trim() != "" ? Substrate.Trim() + (Convert.ToString(DT.Rows[0]["Substrate_Batch"]).Trim() != "" ? " - " + Convert.ToString(DT.Rows[0]["Substrate_Batch"]) : "") : (Convert.ToString(DT.Rows[0]["Substrate_Batch"]).Trim() != "" ? Convert.ToString(DT.Rows[0]["Substrate_Batch"]) : ""));

                Ink = (Ink.Trim() != "" ? Ink.Trim() + (Convert.ToString(DT.Rows[0]["Ink_SKU"]).Trim() != "" ? " - " + Convert.ToString(DT.Rows[0]["Ink_SKU"]) : "") : (Convert.ToString(DT.Rows[0]["Ink_SKU"]).Trim() != "" ? Convert.ToString(DT.Rows[0]["Ink_SKU"]) : ""));
                Ink = (Ink.Trim() != "" ? Ink.Trim() + (Convert.ToString(DT.Rows[0]["Ink_Batch"]).Trim() != "" ? " - " + Convert.ToString(DT.Rows[0]["Ink_Batch"]) : "") : (Convert.ToString(DT.Rows[0]["Ink_Batch"]).Trim() != "" ? Convert.ToString(DT.Rows[0]["Ink_Batch"]) : ""));

                Location = (Location.Trim() != "" ? Location.Trim() + (Convert.ToString(DT.Rows[0]["BranchName"]).Trim() != "" ? " - " + Convert.ToString(DT.Rows[0]["BranchName"]) : "") : (Convert.ToString(DT.Rows[0]["BranchName"]).Trim() != "" ? Convert.ToString(DT.Rows[0]["BranchName"]) : ""));
                Location = (Location.Trim() != "" ? Location.Trim() + (Convert.ToString(DT.Rows[0]["DongleDescription"]).Trim() != "" ? " - " + Convert.ToString(DT.Rows[0]["DongleDescription"]) : "") : (Convert.ToString(DT.Rows[0]["DongleDescription"]).Trim() != "" ? Convert.ToString(DT.Rows[0]["DongleDescription"]) : ""));

                SBStr.Replace("###JobName###", Convert.ToString(DT.Rows[0]["Job"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["Job"]).Trim());
                SBStr.Replace("###Device###", Convert.ToString(DT.Rows[0]["Device"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["Device"]).Trim());
                SBStr.Replace("###TimeStamp###", CommonFunctions.GetDateByCultures(Convert.ToDateTime(DT.Rows[0]["CreatedDT"])));
                SBStr.Replace("###PrintTarget###", Convert.ToString(DT.Rows[0]["PrintTarget"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["PrintTarget"]).Trim());
                SBStr.Replace("###Substrate###", Substrate.Trim() == "" ? "&nbsp;" : Substrate.Trim());
                SBStr.Replace("###Ink###", Ink.Trim() == "" ? "&nbsp;" : Ink.Trim());
                SBStr.Replace("###Location###", Location.Trim() == "" ? "&nbsp;" : Location.Trim());
                SBStr.Replace("###Customer###", Convert.ToString(DT.Rows[0]["Customer"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["Customer"]).Trim());
                SBStr.Replace("###Information###", Convert.ToString(DT.Rows[0]["Info"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["Info"]).Trim());
                SBStr.Replace("###DeltaEFormula###", Convert.ToString(DT.Rows[0]["DeltaEFormula"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["DeltaEFormula"]).Trim());
            }
            else
            {
                SBStr.Replace("###JobName###", "&nbsp;");
                SBStr.Replace("###Device###", "&nbsp;");
                SBStr.Replace("###TimeStamp###", "&nbsp;");
                SBStr.Replace("###PrintTarget###", "&nbsp;");
                SBStr.Replace("###Substrate###", "&nbsp;");
                SBStr.Replace("###Ink###", "&nbsp;");
                SBStr.Replace("###Location###", "&nbsp;");
                SBStr.Replace("###Customer###", "&nbsp;");
                SBStr.Replace("###Information###", "&nbsp;");
                SBStr.Replace("###DeltaEFormula###", "&nbsp;");
            }
            #endregion

            // === Table 1 = Chart of Target Vs Actual AB Plot === //
            //CommonFunctions.FileTimeLog("Start Page 1 a* b* ", false);
            #region ==== Target Vs Actual AB Plot ====
            SBStr.Replace("###TargetVersusActualAB###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page1_TargetVersusActualAB.Jpeg");
            #endregion

            // === Table 2 = Chart of Summary Delta Value Graph Image And Table Value Ploted === //
            //CommonFunctions.FileTimeLog("Start Page 1 Detlta", false);
            #region ==== Summary Delta Value Graph Image ====
            DT = DS_Common.Tables[2];
            StringBuilder SBstrDeltaValue = new StringBuilder();
            if (DT != null && DT.Rows.Count > 0)
            {
                string strStatusImg = "";

                #region "AverageDE"
                if (Convert.IsDBNull(DT.Rows[0]["AverageDE_ACT"]) || Convert.IsDBNull(DT.Rows[0]["AverageDE_TH"]))
                {
                    SBstrDeltaValue.Append("<tr style='height: 22px'>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' class='BottomBorder' style='width: 160px;'>Average &Delta; E</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("</tr>");
                }
                else
                {
                    if (Convert.ToDecimal(DT.Rows[0]["AverageDE_ACT"]) < Convert.ToDecimal(DT.Rows[0]["AverageDE_TH"]))
                        strStatusImg = strStatusImageGreen;
                    else
                        strStatusImg = strStatusImageRed;

                    SBstrDeltaValue.Append("<tr style='height: 22px'>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                    SBstrDeltaValue.Append("<td valign='middle' class='BottomBorder' style='width: 160px;'>Average &Delta; E</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["AverageDE_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["AverageDE_TH"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("</tr>");
                }
                #endregion

                #region "MaximumDE"
                if (Convert.IsDBNull(DT.Rows[0]["MaximumDE_ACT"]) || Convert.IsDBNull(DT.Rows[0]["MaximumDE_TH"]))
                {
                    SBstrDeltaValue.Append("<tr style='height: 22px'>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' class='BottomBorder' style='width: 160px;'>Maximum &Delta; E</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("</tr>");
                }
                else
                {
                    if (Convert.ToDecimal(DT.Rows[0]["MaximumDE_ACT"]) < Convert.ToDecimal(DT.Rows[0]["MaximumDE_TH"]))
                        strStatusImg = strStatusImageGreen;
                    else
                        strStatusImg = strStatusImageRed;

                    SBstrDeltaValue.Append("<tr style='height: 22px'>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                    SBstrDeltaValue.Append("<td valign='middle' class='BottomBorder' style='width: 160px;'>Maximum &Delta; E</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["MaximumDE_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["MaximumDE_TH"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("</tr>");
                }
                #endregion

                #region "PrimaryDE"
                if (Convert.IsDBNull(DT.Rows[0]["PrimaryDE_ACT"]) || Convert.IsDBNull(DT.Rows[0]["PrimaryDE_TH"]))
                {
                    SBstrDeltaValue.Append("<tr style='height: 22px'>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' class='BottomBorder' style='width: 160px;'>Primary &Delta; E</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("</tr>");
                }
                else
                {
                    if (Convert.ToDecimal(DT.Rows[0]["PrimaryDE_ACT"]) < Convert.ToDecimal(DT.Rows[0]["PrimaryDE_TH"]))
                        strStatusImg = strStatusImageGreen;
                    else
                        strStatusImg = strStatusImageRed;

                    SBstrDeltaValue.Append("<tr style='height: 22px'>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                    SBstrDeltaValue.Append("<td valign='middle' class='BottomBorder' style='width: 160px;'>Primary &Delta; E</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["PrimaryDE_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["PrimaryDE_TH"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("</tr>");
                }
                #endregion

                #region "PrimaryDH"
                if (Convert.IsDBNull(DT.Rows[0]["PrimaryDH_ACT"]) || Convert.IsDBNull(DT.Rows[0]["PrimaryDH_TH"]))
                {
                    SBstrDeltaValue.Append("<tr style='height: 22px'>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' class='BottomBorder' style='width: 160px;'>Primary &Delta; H</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("</tr>");
                }
                else
                {
                    if (Convert.ToDecimal(DT.Rows[0]["PrimaryDH_ACT"]) < Convert.ToDecimal(DT.Rows[0]["PrimaryDH_TH"]))
                        strStatusImg = strStatusImageGreen;
                    else
                        strStatusImg = strStatusImageRed;

                    SBstrDeltaValue.Append("<tr style='height: 22px'>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                    SBstrDeltaValue.Append("<td valign='middle' class='BottomBorder' style='width: 160px;'>Primary &Delta; H</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["PrimaryDH_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["PrimaryDH_TH"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("</tr>");
                }
                #endregion

                #region "GrayAverageDH"
                if (Convert.IsDBNull(DT.Rows[0]["GrayAverageDH_ACT"]) || Convert.IsDBNull(DT.Rows[0]["GrayAverageDH_TH"]))
                {
                    SBstrDeltaValue.Append("<tr style='height: 22px'>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' class='BottomBorder' style='width: 160px;'>Gray Average &Delta; E</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("</tr>");
                }
                else
                {
                    if (Convert.ToDecimal(DT.Rows[0]["GrayAverageDH_ACT"]) < Convert.ToDecimal(DT.Rows[0]["GrayAverageDH_TH"]))
                        strStatusImg = strStatusImageGreen;
                    else
                        strStatusImg = strStatusImageRed;

                    SBstrDeltaValue.Append("<tr style='height: 22px'>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                    SBstrDeltaValue.Append("<td valign='middle' class='BottomBorder' style='width: 160px;'>Gray Average &Delta; E</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["GrayAverageDH_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["GrayAverageDH_TH"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("</tr>");
                }

                #endregion

                #region "dE_95pct_Patches"
                if (Convert.IsDBNull(DT.Rows[0]["dE_95pct_Patches_ACT"]) || Convert.IsDBNull(DT.Rows[0]["dE_95pct_Patches_TH"]))
                {
                    SBstrDeltaValue.Append("<tr style='height: 22px'>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' class='BottomBorder' style='width: 160px;'>95% Quartile &Delta; E</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("</tr>");
                }
                else
                {
                    if (Convert.ToDecimal(DT.Rows[0]["dE_95pct_Patches_ACT"]) < Convert.ToDecimal(DT.Rows[0]["dE_95pct_Patches_TH"]))
                        strStatusImg = strStatusImageGreen;
                    else
                        strStatusImg = strStatusImageRed;

                    SBstrDeltaValue.Append("<tr style='height: 22px'>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                    SBstrDeltaValue.Append("<td valign='middle' class='BottomBorder' style='width: 160px;'>95% Quartile &Delta; E</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["dE_95pct_Patches_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["dE_95pct_Patches_TH"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("</tr>");
                }
                #endregion

                #region "dE_OuterGamut"
                if (Convert.IsDBNull(DT.Rows[0]["dE_OuterGamut_ACT"]) || Convert.IsDBNull(DT.Rows[0]["dE_OuterGamut_TH"]))
                {
                    SBstrDeltaValue.Append("<tr style='height: 22px'>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' class='BottomBorder' style='width: 160px';>&Delta; E Outer Gamut</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                    SBstrDeltaValue.Append("</tr>");
                }
                else
                {
                    if (Convert.ToDecimal(DT.Rows[0]["dE_OuterGamut_ACT"]) < Convert.ToDecimal(DT.Rows[0]["dE_OuterGamut_TH"]))
                        strStatusImg = strStatusImageGreen;
                    else
                        strStatusImg = strStatusImageRed;

                    SBstrDeltaValue.Append("<tr style='height: 22px'>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                    SBstrDeltaValue.Append("<td valign='middle' class='BottomBorder' style='width: 160px;'>&Delta; E Outer Gamut</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["dE_OuterGamut_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["dE_OuterGamut_TH"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("</tr>");
                }
                #endregion

                SBStr.Replace("###DeltaSummaryDetail###", SBstrDeltaValue.ToString());
            }
            else
            {
                SBStr.Replace("###DeltaSummaryDetail###", "");
            }
            SBStr.Replace("###ResultSummary_DeltaImage###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page1_ResultSummary_Delta.Jpeg");
            #endregion

            // === Table 3 = Chart of Summary Delta E Value Graph Image And Table Value Ploted  === //
            //CommonFunctions.FileTimeLog("Start Page 1 Delta E", false);
            #region ==== Summary Delta E Value Graph Image ====
            DT = DS_Common.Tables[3];
            StringBuilder SBstrDeltaEValue = new StringBuilder();
            if (DT != null && DT.Rows.Count > 0)
            {
                string strStatusImg = "";

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (Convert.ToDecimal(DT.Rows[i]["DE"]) < Convert.ToDecimal(DT.Rows[i]["TOL"]))
                        strStatusImg = strStatusImageGreen;
                    else
                        strStatusImg = strStatusImageRed;


                    SBstrDeltaEValue.Append("<tr style='height: 22px'>");
                    SBstrDeltaEValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                    SBstrDeltaEValue.Append("<td valign='middle' class='BottomBorder' style='width: 160px;'>" + Convert.ToString(DT.Rows[i]["PatchType"]) + "</td>");
                    SBstrDeltaEValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["DE"])) + "&nbsp;</td>");
                    SBstrDeltaEValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["TOL"])) + "&nbsp;</td>");
                    SBstrDeltaEValue.Append("</tr>");
                }
                SBStr.Replace("###DeltaESummaryDetail###", SBstrDeltaEValue.ToString());
            }
            else
            {
                SBStr.Replace("###DeltaESummaryDetail###", "");
            }
            SBStr.Replace("###ResultSummary_DeltaEImage###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page1_ResultSummary_DeltaEValue.Jpeg");
            #endregion

            // === Table 4 = Chart of Gray L* a* b* Graph Image And Table Value Ploted  === //
            //CommonFunctions.FileTimeLog("Start Page 2 L* A* B*", false);
            #region ==== Gray L* a* b* Deviation Graph ====
            DT = DS_Common.Tables[4];
            StringBuilder SBstrGrayLABValue = new StringBuilder();
            StringBuilder SBstrPatch = new StringBuilder();
            StringBuilder SBstrLActual = new StringBuilder();
            StringBuilder SBstrAActual = new StringBuilder();
            StringBuilder SBstrBActual = new StringBuilder();


            SBstrPatch.Append("<tr class='row-1'><td class='row-content1'style='padding-left:2px'>Patch</td>");
            SBstrLActual.Append("<tr class='row-2'><td class='row-content1' style='padding-left:2px'>L Actual</td>");
            SBstrAActual.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>A Actual</td>");
            SBstrBActual.Append("<tr class='row-2'><td class='row-content1' style='padding-left:2px'>B Actual</td>");

            if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
            {
                SBStr.Replace("###DBP###", "2px");
                SBStr.Replace("###GrayLDeviation_GraphImage###", "<table cellpadding='0' cellspacing='0' width='100%' border='0'><tr><td class='summary-bg' ><img src='##IMAGES##summary-icon.png' width='12' height='11' style='margin: 2px 0 0 5px;' /><span class='summary-title1'>Gray L* Deviation Graph</span></td></tr><tr><td ><img src='" + currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page2_GrayDeviation_L.Jpeg" + "' height='110px' width='658px' alt='Gray L* Deviation' id='img2' /></td></tr></table>");
                SBStr.Replace("###GrayADeviation_GraphImage###", "<table cellpadding='0' cellspacing='0' width='100%' border='0'><tr><td class='summary-bg' ><img src='##IMAGES##summary-icon.png' width='12' height='11' style='margin: 2px 0 0 5px;' /><span class='summary-title1'>Gray a* Deviation Graph</span></td></tr><tr><td ><img src='" + currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page2_GrayDeviation_A.Jpeg" + "' height='110px' width='658px' alt='Gray a* Deviation' id='img5' /></td></tr></table>");
                SBStr.Replace("###GrayBDeviation_GraphImage###", "<table cellpadding='0' cellspacing='0' width='100%' border='0'><tr><td class='summary-bg' ><img src='##IMAGES##summary-icon.png' width='12' height='11' style='margin: 2px 0 0 5px;' /><span class='summary-title1'>Gray b* Deviation Graph</span></td></tr><tr><td ><img src='" + currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page2_GrayDeviation_B.Jpeg" + "' height='110px' width='658px' alt='Gray b* Deviation' id='img6' /></td></tr></table>");

                SBstrGrayLABValue.Append("<table cellpadding='0' cellspacing='0' width='100%' border='0'><tr><td><table width='660' border='0' cellspacing='0' cellpadding='0'><tr><td class='summary-bg' ><img src='##IMAGES##summary-icon.png' width='12' height='11' style='margin: 2px 0 0 5px;' /><span class='summary-title1'>Gray Patch Values</span></td></tr></table></td></tr><tr><td><table cellpadding='0' cellspacing='0' width='100%' border='0'>###VALUE###</table></td></tr></table>");

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    SBstrPatch.Append("<td align='center' class='row-content1'>" + DT.Rows[i]["GRAY"].ToString() + "</td>");
                    SBstrLActual.Append("<td align='center' class='row-content1'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["DL"])) + "</td>");
                    SBstrAActual.Append("<td align='center' class='row-content1'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["DA"])) + "</td>");
                    SBstrBActual.Append("<td align='center' class='row-content1'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["DB"])) + "</td>");
                }
                SBstrPatch.Append("</tr>");
                SBstrLActual.Append("</tr>");
                SBstrAActual.Append("</tr>");
                SBstrBActual.Append("</tr>");

                SBstrGrayLABValue.Replace("###VALUE###", SBstrPatch.ToString() + SBstrLActual.ToString() + SBstrAActual.ToString() + SBstrBActual.ToString());
                SBStr.Replace("###GrayPathValues###", SBstrGrayLABValue.ToString());

            }
            else
            {
                SBStr.Replace("###DBP###", "0px");
                SBStr.Replace("###GrayLDeviation_GraphImage###", "&nbsp;");
                SBStr.Replace("###GrayADeviation_GraphImage###", "&nbsp;");
                SBStr.Replace("###GrayBDeviation_GraphImage###", "&nbsp;");
                SBStr.Replace("###GrayPathValues###", "<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>");

            }

            #endregion

            // === Table 4 = Chart of Gray NPD Graph Image === //
            //CommonFunctions.FileTimeLog("Start Page 2 Gray NPD", false);
            #region ==== Gray NPD Graph ====
            DT = DS_Common.Tables[4];
            if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
            {
                StringBuilder SBstrGrayNPDValue = new StringBuilder();
                StringBuilder SBstrNPDGrayPatch = new StringBuilder();
                StringBuilder SBstrNPDGrayActual = new StringBuilder();

                SBStr.Replace("###GrayNPD_GraphImage###", "<table cellpadding='0' cellspacing='0' width='100%' border='0' ><tr><td style='width:310px; height:270px'><img src='" + currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page2_GrayNPD.Jpeg" + "' height='270px' width='310px' alt='Gray NPD' id='img7' /></td></tr><tr><td height='3'></td></tr><tr><td align='center'>###NPDGrayValue###</td></tr></table>");
                SBstrGrayNPDValue.Append("<table cellpadding='0' cellspacing='0' width='100%' border='0'>");

                SBstrNPDGrayPatch.Append("<tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'><td class='row-content1'>&nbsp;</td>");
                SBstrNPDGrayActual.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Act</td>");

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    SBstrNPDGrayPatch.Append("<td align='center' class='row-content1'>" + Convert.ToInt64(DT.Rows[i]["GRAY"]).ToString() + "</td>");
                    SBstrNPDGrayActual.Append("<td align='center' class='row-content1'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Density"])) + "</td>");
                }
                SBstrNPDGrayPatch.Append("</tr>");
                SBstrNPDGrayActual.Append("</tr>");

                SBstrGrayNPDValue.Append(SBstrNPDGrayPatch.ToString() + SBstrNPDGrayActual.ToString() + "</table>");
                SBStr.Replace("###NPDGrayValue###", SBstrGrayNPDValue.ToString());
            }
            else
                SBStr.Replace("###GrayNPD_GraphImage###", "&nbsp;");


            #endregion

            // === Table 5 = Chart of Black NPD Graph Image === //
            //CommonFunctions.FileTimeLog("Start Page 2 Balck NPD", false);
            #region ==== Black NPD Graph ====
            SBStr.Replace("###BlackNPD_GraphImage###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page2_BlackNPD.Jpeg");
            DT = DS_Common.Tables[5];
            if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
            {
                StringBuilder SBstrBlackNPDValue = new StringBuilder();
                StringBuilder SBstrNPDBlackPatch = new StringBuilder();
                StringBuilder SBstrNPDBlackActual = new StringBuilder();

                SBstrBlackNPDValue.Append("<table cellpadding='0' cellspacing='0' width='100%' border='0'>");



                SBstrNPDBlackPatch.Append("<tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'><td class='row-content1'>&nbsp;</td>");
                SBstrNPDBlackActual.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Act</td>");

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    SBstrNPDBlackPatch.Append("<td align='center' class='row-content1'>" + Convert.ToInt64(DT.Rows[i]["Black"]).ToString() + "</td>");
                    SBstrNPDBlackActual.Append("<td align='center' class='row-content1'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Density"])) + "</td>");
                }
                SBstrNPDBlackPatch.Append("</tr>");
                SBstrNPDBlackActual.Append("</tr>");

                SBstrBlackNPDValue.Append(SBstrNPDBlackPatch.ToString() + SBstrNPDBlackActual.ToString() + "</table>");
                SBStr.Replace("###NPDBlackValue###", SBstrBlackNPDValue.ToString());
            }

            #endregion

            // === Table 6 = Chart of Actual Dot Percentage Graph Image And Value Ploted === //
            //CommonFunctions.FileTimeLog("Start Page 3 Dotgain Actual", false);
            #region ==== Actual Dot Percentage  Graph ====
            DT = DS_Common.Tables[6];
            List<int> intArrDistingRec = new List<int>();


            StringBuilder SBstrDotGainValue = new StringBuilder();
            StringBuilder SBstrDGPatch = new StringBuilder();
            StringBuilder SBstrCDG = new StringBuilder();
            StringBuilder SBstrMDG = new StringBuilder();
            StringBuilder SBstrYDG = new StringBuilder();
            StringBuilder SBstrKDG = new StringBuilder();

            SBstrDGPatch.Append("<tr style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'><td style='padding-left:2px'>Patch</td>");
            SBstrCDG.Append("<tr style='BACKGROUND-COLOR:#CCFFFF;COLOR:#000;'><td style='padding-left:2px'>C DG</td>");
            SBstrMDG.Append("<tr style='BACKGROUND-COLOR:#FF99CC;COLOR:#000;'><td style='padding-left:2px'>M DG</td>");
            SBstrYDG.Append("<tr style='BACKGROUND-COLOR:#FFFF99;COLOR:#000;'><td style='padding-left:2px'>Y DG</td>");
            SBstrKDG.Append("<tr style='BACKGROUND-COLOR:#C0C0C0;COLOR:#000;'><td style='padding-left:2px'>K DG</td>");

            if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
            {


                SBStr.Replace("###DotgainActual_GraphImage###", currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page3_CHRT_DotgainActual.Jpeg");
                SBstrDotGainValue.Append("<table cellpadding='0' cellspacing='0' width='100%' border='0' style='border: SOLID 2PX #000'><tr><td align='center'><table width='660' border='0' cellspacing='0' cellpadding='0'><tr><td class='summary-bg' ><img src='##IMAGES##summary-icon.png' width='12' height='11' style='margin: 2px 0 0 5px;' /><span class='summary-title1'>Patch DG Data</span></td></tr></table></td></tr><tr><td><table cellpadding='0' cellspacing='0' width='100%' border='0'>###VALUE###</table></td></tr></table>");

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (!intArrDistingRec.Contains(Convert.ToInt16(DT.Rows[i]["CMYK_C"])))
                        intArrDistingRec.Add(Convert.ToInt16(DT.Rows[i]["CMYK_C"]));

                    if (!intArrDistingRec.Contains(Convert.ToInt16(DT.Rows[i]["CMYK_M"])))
                        intArrDistingRec.Add(Convert.ToInt16(DT.Rows[i]["CMYK_M"]));

                    if (!intArrDistingRec.Contains(Convert.ToInt16(DT.Rows[i]["CMYK_Y"])))
                        intArrDistingRec.Add(Convert.ToInt16(DT.Rows[i]["CMYK_Y"]));

                    if (!intArrDistingRec.Contains(Convert.ToInt16(DT.Rows[i]["CMYK_K"])))
                        intArrDistingRec.Add(Convert.ToInt16(DT.Rows[i]["CMYK_K"]));

                }
                intArrDistingRec.Sort();

                DataView DV = DT.DefaultView;
                DV.RowFilter = "CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 0";
                if (DV.Count > 0)
                {
                    SBstrDGPatch.Append("<td align='center'>0</td>");
                    SBstrCDG.Append("<td align='center'>" + CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                    SBstrMDG.Append("<td align='center'>" + CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                    SBstrYDG.Append("<td align='center'>" + CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                    SBstrKDG.Append("<td align='center'>" + CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                }

                for (int i = 0; i < intArrDistingRec.Count; i++)
                {
                    if (intArrDistingRec[i].ToString() != "0")
                    {
                        SBstrDGPatch.Append("<td align='center'>" + intArrDistingRec[i].ToString() + "</td>");
                        DV.RowFilter = "CMYK_C = " + intArrDistingRec[i].ToString();
                        if (DV.Count > 0)
                            SBstrCDG.Append("<td align='center'>" + CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                        else
                            SBstrCDG.Append("<td align='center'>&nbsp;</td>");

                        DV.RowFilter = "CMYK_M = " + intArrDistingRec[i].ToString();
                        if (DV.Count > 0)
                            SBstrMDG.Append("<td align='center'>" + CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                        else
                            SBstrMDG.Append("<td align='center'>&nbsp;</td>");


                        DV.RowFilter = "CMYK_Y = " + intArrDistingRec[i].ToString();
                        if (DV.Count > 0)
                            SBstrYDG.Append("<td align='center'>" + CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                        else
                            SBstrYDG.Append("<td align='center'>&nbsp;</td>");


                        DV.RowFilter = "CMYK_K = " + intArrDistingRec[i].ToString();
                        if (DV.Count > 0)
                            SBstrKDG.Append("<td align='center'>" + CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                        else
                            SBstrKDG.Append("<td align='center'>&nbsp;</td>");

                    }
                }


                SBstrDGPatch.Append("</tr>");
                SBstrCDG.Append("</tr>");
                SBstrMDG.Append("</tr>");
                SBstrYDG.Append("</tr>");
                SBstrKDG.Append("</tr>");

                SBstrDotGainValue.Replace("###VALUE###", SBstrDGPatch.ToString() + SBstrCDG.ToString() + SBstrMDG.ToString() + SBstrYDG.ToString() + SBstrKDG.ToString());
                SBStr.Replace("###DotgainActualValues###", SBstrDotGainValue.ToString());


            }
            else
            {
                SBStr.Replace("###DotgainActualValues###", "");


            }
            #endregion

            // === Table 7,8,9 Record Count And Put Border === //

            #region Check Table 7,8,9 count
            if (DS_Common.Tables[7] != null && DS_Common.Tables[7].Rows.Count > 0 && DS_Common.Tables[7].Rows[0][0].ToString() != "NO DATA" && DS_Common.Tables[8] != null && DS_Common.Tables[8].Rows.Count > 0 && DS_Common.Tables[8].Rows[0][0].ToString() != "NO DATA" && DS_Common.Tables[9] != null && DS_Common.Tables[9].Rows.Count > 0 && DS_Common.Tables[9].Rows[0][0].ToString() != "NO DATA")
                SBStr.Replace("###DEVP###", "2px");
            else
                SBStr.Replace("###DEVP###", "0px");
            #endregion

            // === Table 8 = Chart of Dotgain Deviation Graph Image And Value Ploted === //
            //CommonFunctions.FileTimeLog("Start Page 3 Dotgain Deviation", false);
            #region ==== Dotgain Deviation Graph ====
            DT = DS_Common.Tables[8];

            StringBuilder SBstrDotgainDeviationValue = new StringBuilder();
            StringBuilder SBstrDotDPatch = new StringBuilder();
            StringBuilder SBstrDotTarget = new StringBuilder();
            StringBuilder SBstrDotActual = new StringBuilder();


            SBstrDotDPatch.Append("<tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'><td class='row-content1'>&nbsp;</td>");
            SBstrDotTarget.Append("<tr class='row-2'><td class='row-content1' style='padding-left:2px'>Tar</td>");
            SBstrDotActual.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Act</td>");
            if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
            {

                SBStr.Replace("###DotgainDeviation_GraphImage###", "<td><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td class='summary-bg' align='center' ><span class='summary-title1'>Dotgain Deviation</span></td></tr><tr><td><img src='" + currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page3_DotgainDeviation.Jpeg" + "' height='330px' width='220px' alt='Dotgain Deviation' id='img11' /></td></tr></table></td>");

                SBstrDotgainDeviationValue.Append("<table cellpadding='0' cellspacing='0' width='100%' border='0'>###VALUE###</table>");
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (Convert.ToInt16(DT.Rows[i]["CMYK_C"]) != 0)
                        SBstrDotDPatch.Append("<td align='center' class='row-content1'>C</td>");
                    else if (Convert.ToInt16(DT.Rows[i]["CMYK_M"]) != 0)
                        SBstrDotDPatch.Append("<td align='center' class='row-content1'>M</td>");
                    else if (Convert.ToInt16(DT.Rows[i]["CMYK_Y"]) != 0)
                        SBstrDotDPatch.Append("<td align='center' class='row-content1'>Y</td>");
                    else if (Convert.ToInt16(DT.Rows[i]["CMYK_K"]) != 0)
                        SBstrDotDPatch.Append("<td align='center' class='row-content1'>K</td>");

                    SBstrDotTarget.Append("<td align='center' class='row-content1'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Targ_DotGain"])) + "</td>");
                    SBstrDotActual.Append("<td align='center' class='row-content1'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["DotGain_ACT"])) + "</td>");
                }
                SBstrDotDPatch.Append("</tr>");
                SBstrDotTarget.Append("</tr>");
                SBstrDotActual.Append("</tr>");

                SBstrDotgainDeviationValue.Replace("###VALUE###", SBstrDotDPatch.ToString() + SBstrDotTarget.ToString() + SBstrDotActual.ToString());
                SBStr.Replace("###DotgainDeviationValues###", SBstrDotgainDeviationValue.ToString());
            }
            else
            {
                SBStr.Replace("###DotgainDeviation_GraphImage###", "<td style='width:200px; height:350px;'>&nbsp;<td>");
                SBStr.Replace("###DotgainDeviationValues###", "<br><br><br><br><br>");
            }
            #endregion

            // === Table 7 = Chart of Density Deviation Graph Image And Value Ploted === //
            //CommonFunctions.FileTimeLog("Start Page 3 Density Deviation", false);
            #region ==== Density Deviation Graph ====
            DT = DS_Common.Tables[7];
            StringBuilder SBstrDensityDeviationValue = new StringBuilder();
            StringBuilder SBstrDenDPatch = new StringBuilder();
            StringBuilder SBstrDenTarget = new StringBuilder();
            StringBuilder SBstrDenActual = new StringBuilder();


            SBstrDenDPatch.Append("<tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'><td class='row-content1'>&nbsp;</td>");
            SBstrDenTarget.Append("<tr class='row-2'><td class='row-content1' style='padding-left:2px'>Tar</td>");
            SBstrDenActual.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Act</td>");
            if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
            {
                SBStr.Replace("###DensityDeviation_GraphImage###", "<td><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td class='summary-bg' align='center' ><span class='summary-title1'>Density Deviation</span></td></tr><tr><td><img src='" + currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page3_DensityDeviation.Jpeg" + "' height='330px' width='220px' alt='Density Deviation' id='img13' /></td></tr></table></td>");
                SBstrDensityDeviationValue.Append("<table cellpadding='0' cellspacing='0' width='100%' border='0'>###VALUE###</table>");
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (DT.Rows[i]["ColorName"].ToString() == "Cyan")
                        SBstrDenDPatch.Append("<td align='center' class='row-content1'>C</td>");
                    else if (DT.Rows[i]["ColorName"].ToString() == "Magenta")
                        SBstrDenDPatch.Append("<td align='center' class='row-content1'>M</td>");
                    else if (DT.Rows[i]["ColorName"].ToString() == "Yellow")
                        SBstrDenDPatch.Append("<td align='center' class='row-content1'>Y</td>");
                    else if (DT.Rows[i]["ColorName"].ToString() == "Black")
                        SBstrDenDPatch.Append("<td align='center' class='row-content1'>K</td>");

                    SBstrDenTarget.Append("<td align='center' class='row-content1'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Density_Targ"])) + "</td>");
                    SBstrDenActual.Append("<td align='center' class='row-content1'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Density"])) + "</td>");
                }
                SBstrDenDPatch.Append("</tr>");
                SBstrDenTarget.Append("</tr>");
                SBstrDenActual.Append("</tr>");

                SBstrDensityDeviationValue.Replace("###VALUE###", SBstrDenDPatch.ToString() + SBstrDenTarget.ToString() + SBstrDenActual.ToString());
                SBStr.Replace("###DensityDeviationValues###", SBstrDensityDeviationValue.ToString());
            }
            else
            {
                SBStr.Replace("###DensityDeviation_GraphImage###", "<td style='width:200px; height:350px;'>&nbsp;<td>");
                SBStr.Replace("###DensityDeviationValues###", "<br><br><br><br><br>");
            }

            #endregion

            // === Table 9 = Chart of Print Contrast Deviation Graph Image And Value Ploted === //
            //CommonFunctions.FileTimeLog("Start Page 3 Print Contrast Deviation", false);
            #region ==== Print Contrast Deviation Graph ====
            DT = DS_Common.Tables[9];

            StringBuilder SBstrPrintConDeviationValue = new StringBuilder();
            StringBuilder SBstrPrintDPatch = new StringBuilder();
            StringBuilder SBstrPrintTarget = new StringBuilder();
            StringBuilder SBstrPrintActual = new StringBuilder();


            SBstrPrintDPatch.Append("<tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'><td class='row-content1'>&nbsp;</td>");
            SBstrPrintTarget.Append("<tr class='row-2'><td class='row-content1' style='padding-left:2px'>Tar</td>");
            SBstrPrintActual.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Act</td>");
            if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
            {
                SBStr.Replace("###PrintContrastDeviation_GraphImage###", "<td><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td class='summary-bg' align='center' ><span class='summary-title1'>Print Contrast Deviation</span></td></tr><tr><td><img src='" + currentBaseUrl + "UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page3_PrintContrastDeviation.Jpeg" + "' height='330px' width='220px' alt='Print Contrast Deviation' id='img15' /></td></tr></table></td>");
                SBstrPrintConDeviationValue.Append("<table cellpadding='0' cellspacing='0' width='100%' border='0'>###VALUE###</table>");
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (DT.Rows[i]["Color"].ToString() == "Cyan")
                        SBstrPrintDPatch.Append("<td align='center' class='row-content1'>C</td>");
                    else if (DT.Rows[i]["Color"].ToString() == "Magenta")
                        SBstrPrintDPatch.Append("<td align='center' class='row-content1'>M</td>");
                    else if (DT.Rows[i]["Color"].ToString() == "Yellow")
                        SBstrPrintDPatch.Append("<td align='center' class='row-content1'>Y</td>");
                    else if (DT.Rows[i]["Color"].ToString() == "Black")
                        SBstrPrintDPatch.Append("<td align='center' class='row-content1'>K</td>");

                    SBstrPrintTarget.Append("<td align='center' class='row-content1'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Print_Contrast_Tol"])) + "</td>");
                    SBstrPrintActual.Append("<td align='center' class='row-content1'>" + CommonFunctions.DecimalToString(Convert.ToString(Formulas.DotGain.CalcPrintContrast(Convert.ToDecimal(DT.Rows[i]["D100"]), Convert.ToDecimal(DT.Rows[i]["D75"])))) + "</td>");
                }
                SBstrPrintDPatch.Append("</tr>");
                SBstrPrintTarget.Append("</tr>");
                SBstrPrintActual.Append("</tr>");
                SBstrPrintConDeviationValue.Replace("###VALUE###", SBstrPrintDPatch.ToString() + SBstrPrintTarget.ToString() + SBstrPrintActual.ToString());
                SBStr.Replace("###PrintConDeviationValues###", SBstrPrintConDeviationValue.ToString());
            }
            else
            {
                SBStr.Replace("###PrintContrastDeviation_GraphImage###", "<td style='width:200px; height:350px;'>&nbsp;<td>");
                SBStr.Replace("###PrintConDeviationValues###", "<br><br><br><br><br>");
            }
            #endregion

            // === Teble 10 = Display Data of CMYK Table And Target And Actula value === //
            //CommonFunctions.FileTimeLog("Start Page 4 Table", false);
            #region ==== Table ====
            DT = DS_Common.Tables[10];

            StringBuilder SBstrTablePageHeader = new StringBuilder();
            StringBuilder SBstrTableHeader = new StringBuilder();
            StringBuilder SBstrTableDetail = new StringBuilder();
            StringBuilder SBstrCompleteTable = new StringBuilder();

            if (DT != null && DT.Rows.Count > 0)
            {


                SBstrTablePageHeader.Append("<table style='width: 660px' cellspacing='0' cellpadding='0' align='center' border='0'>");
                SBstrTablePageHeader.Append("<tr>");
                SBstrTablePageHeader.Append("<td align='center'>");
                SBstrTablePageHeader.Append("<table style='width: 660px' align='center' cellpadding='0' cellspacing='0' border='0'>");
                SBstrTablePageHeader.Append("<tr>");
                SBstrTablePageHeader.Append("<td width='9'><img src='##IMAGES##header-left-img.gif' /></td>");
                SBstrTablePageHeader.Append("<td class='header-bg' >");
                SBstrTablePageHeader.Append("<table width='642' border='0' cellspacing='0' cellpadding='0'>");
                SBstrTablePageHeader.Append("<tr>");
                SBstrTablePageHeader.Append("<td class='header-title'>Measurement Report</td>");
                SBstrTablePageHeader.Append("<td>&nbsp;</td>");
                SBstrTablePageHeader.Append("<td width='116' valign='middle'>&nbsp;</td>");
                SBstrTablePageHeader.Append("</tr></table></td>");
                SBstrTablePageHeader.Append("<td width='9'><img src='##IMAGES##header-right-img.gif' /></td></tr></table>");
                SBstrTablePageHeader.Append("</td></tr>");
                SBstrTablePageHeader.Append("<tr><td align='center' valign='top'>###Table###</td></tr></table>");


                SBstrTableHeader.Append("<table style='width: 99%; border: 1PX SOLID #C1C1C1' cellspacing='0' cellpadding=0' border='0' align='center'>");
                SBstrTableHeader.Append("<tr>");
                SBstrTableHeader.Append("<td style='width: 8%' class='BGRIDTITLE' align='center'>Color<br>T | A</td>");
                SBstrTableHeader.Append("<td style='width: 7%' class='BGRIDTITLE' align='center'>&Delta; E</td>");
                SBstrTableHeader.Append("<td style='width: 8%' class='BGRIDTITLE' align='center'>PatchID<br>#</td>");
                SBstrTableHeader.Append("<td style='width: 7%' class='BGRIDTITLE' align='center'>C</td>");
                SBstrTableHeader.Append("<td style='width: 7%' class='BGRIDTITLE' align='center'>M</td>");
                SBstrTableHeader.Append("<td style='width: 7%' class='BGRIDTITLE' align='center'>Y</td>");
                SBstrTableHeader.Append("<td style='width: 7%' class='BGRIDTITLE' align='center'>K</td>");
                SBstrTableHeader.Append("<td style='width: 8%' class='BGRIDTITLE' align='center'>Target<br>L</td>");
                SBstrTableHeader.Append("<td style='width: 8%' class='BGRIDTITLE' align='center'>Target<br>a</td>");
                SBstrTableHeader.Append("<td style='width: 8%' class='BGRIDTITLE' align='center'>Target<br>b</td>");
                SBstrTableHeader.Append("<td style='width: 8%' class='BGRIDTITLE' align='center'>Actual<br>L</td>");
                SBstrTableHeader.Append("<td style='width: 8%' class='BGRIDTITLE' align='center'>Actual<br>a</td>");
                SBstrTableHeader.Append("<td style='width: 8%' class='BGRIDTITLE' align='center'>Actual<br>b</td>");
                SBstrTableHeader.Append("</tr>");
                SBstrTableHeader.Append("###TableDetail###");

                Int64 RecNo = 0;

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    if (RecNo == 42)
                    {
                        SBstrCompleteTable.Append(SBstrTablePageHeader.ToString());
                        SBstrCompleteTable.Replace("###Table###", SBstrTableHeader.ToString());
                        SBstrCompleteTable.Replace("###TableDetail###", SBstrTableDetail.ToString() + "</table><br>");
                        SBstrTableDetail = new StringBuilder("");
                        RecNo = 0;
                    }

                    if ((i % 2) == 0)
                        SBstrTableDetail.Append("<tr class='BGRIDEVEN' > ");
                    else
                        SBstrTableDetail.Append("<tr class='BGRIDODD' > ");

                    string Tlbl = "<span style='background-color:" + ColorCalcuations.LabtoRGB(Convert.ToDouble(DT.Rows[i]["Target_L"]), Convert.ToDouble(DT.Rows[i]["Target_a"]), Convert.ToDouble(DT.Rows[i]["Target_b"])) + "'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                    string Albl = "<span style='background-color:" + ColorCalcuations.LabtoRGB(Convert.ToDouble(DT.Rows[i]["Actual_L"]), Convert.ToDouble(DT.Rows[i]["Actual_a"]), Convert.ToDouble(DT.Rows[i]["Actual_b"])) + "'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + Tlbl + "&nbsp;" + Albl + "</td>");
                    SBstrTableDetail.Append("<td style='width: 7%' align='center'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["DE"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + DT.Rows[i]["Sample_Name"].ToString() + "</td>");
                    SBstrTableDetail.Append("<td style='width: 7%' align='center'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["CMYK_C"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 7%' align='center'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["CMYK_M"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 7%' align='center'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["CMYK_Y"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 7%' align='center'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["CMYK_K"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Target_L"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Target_a"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Target_b"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Actual_L"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Actual_a"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Actual_b"])) + "</td>");
                    SBstrTableDetail.Append("</tr>");

                    if ((i == (DT.Rows.Count - 1)) && (RecNo <= 42))
                    {
                        SBstrCompleteTable.Append(SBstrTablePageHeader.ToString());
                        SBstrCompleteTable.Replace("###Table###", SBstrTableHeader.ToString());
                        SBstrCompleteTable.Replace("###TableDetail###", SBstrTableDetail.ToString() + "</table><br>");
                        SBstrTableDetail = new StringBuilder("");
                        RecNo = 0;
                    }
                    RecNo++;
                }
            }
            SBStr.Replace("###ReportColor_Table###", SBstrCompleteTable.ToString());
            #endregion

            #region Assign Server
            SBStr.Replace("##IMAGES##", strServer);
            #endregion

            return SBStr;
        }
        #endregion

    }
}
