#region System
using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using WebSupergoo.ABCpdf8;
using WebSupergoo.ABCpdf8.Atoms;
using WebSupergoo.ABCpdf8.Objects;

#endregion
#region Others
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Formulas;
using Fuji.Common;
#endregion
namespace Fuji.Common
{
    public class ReportPDF
    {
        #region Declaration

        //string strServer = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Substring(0, System.Web.HttpContext.Current.Request.Url.AbsoluteUri.LastIndexOf("/") + 1);
        string strServer = System.Web.HttpContext.Current.Server.MapPath("Images") + "\\";
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

            CSSPath = System.Web.HttpContext.Current.Server.MapPath("Templates/style.css");
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

                switch (ReportType)
                {
                    #region Case 1 : Branch Summery

                    case (int)Fuji.Common.Enums.enumReportType.BranchSummaryReport:

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
                    case (int)Fuji.Common.Enums.enumReportType.DeviceReport:

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
                    case (int)Fuji.Common.Enums.enumReportType.ShiftVariabilityReport:

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
                    case (int)Fuji.Common.Enums.enumReportType.DeviceVariabilityReport:

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
                    case (int)Fuji.Common.Enums.enumReportType.ColorDetailReport:

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
                    case (int)Fuji.Common.Enums.enumReportType.PatchDetailReport:
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
                    case (int)Fuji.Common.Enums.enumReportType.ProductivityReport:

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

                    case (int)Fuji.Common.Enums.enumReportType.ReportPerformanceSummary:

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

                    case (int)Fuji.Common.Enums.enumReportType.ReportTargetMeasurement:

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

                    case (int)Fuji.Common.Enums.enumReportType.ReportJobRunCertificationProcess:

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

                    case (int)Fuji.Common.Enums.enumReportType.ReportJobRunCertificationNonProcess:

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

                    case (int)Fuji.Common.Enums.enumReportType.ReportPressMakeReady:

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
                    case (int)Fuji.Common.Enums.enumReportType.ReportSingleMeasurement:

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

                    #region Case 14 : CPS Print Standards Report
                    case (int)Fuji.Common.Enums.enumReportType.CPSPrintStandardsReport:

                        //========== Adds New Page IN PDF ==========//
                        theDoc.Page = theDoc.AddPage();

                        //========== Adds a counter to Page ==========//
                        theCount++;

                        //========== Read Template ==========//
                        FileName = "Templates/CPSPrintStandardReport.htm";
                        fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                        StringBuilder CPSStr = new StringBuilder(fp.ReadToEnd());

                        CPSStr = TextReturn_CPSPrintStandardsReport(CPSStr);

                        fp.Close();

                        //========== Assign HTML to theID ==========//
                        //1 : str = The html content.
                        //3 : width(612) = The dimension of the page to fix in A4 size paper.
                        theID = theDoc.AddImageHtml(CPSStr.ToString(), true, 700, false);

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

                    #region Case 15 : CPS Print Standards Report ISO
                    case (int)Fuji.Common.Enums.enumReportType.CPSPrintStandardsReportISO:

                        //========== Adds New Page IN PDF ==========//
                        theDoc.Page = theDoc.AddPage();

                        //========== Adds a counter to Page ==========//
                        theCount++;

                        //========== Read Template ==========//
                        FileName = "Templates/CPSPrintStandardReportISO.htm";
                        fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                        StringBuilder CPSISOStr = new StringBuilder(fp.ReadToEnd());

                        CPSStr = TextReturn_CPSPrintStandardsReportISO(CPSISOStr);

                        fp.Close();

                        //========== Assign HTML to theID ==========//
                        //1 : str = The html content.
                        //3 : width(612) = The dimension of the page to fix in A4 size paper.
                        theID = theDoc.AddImageHtml(CPSStr.ToString(), true, 700, false);

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
                theDoc.HPos = 0.0;
                theDoc.VPos = 0.0;
                theDoc.Color.String = "255 255 255";
                theDoc.FontSize = 8;
                if (ReportType == (int)Fuji.Common.Enums.enumReportType.CPSPrintStandardsReport || ReportType == (int)Fuji.Common.Enums.enumReportType.CPSPrintStandardsReportISO)
                {
                    theDoc.Rect.String = "-20 -10 622 55";
                    for (int i = 2; i <= theCount; i++)
                    {
                        theDoc.HPos = 0.0;
                        theDoc.VPos = 0.0;
                        theDoc.PageNumber = i;
                        DataTable dtCPSPrint = DS_Common.Tables[0];

                        str = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">" +
                            "<tr style=\"height:25px\"><td align=\"center\" valign=\"middle\" bgcolor=\"#C1C1C1\" style=\"color:#000000; font-weight:bold ; font-size:8px; font-family:Tahoma, Arial, Helvetica, sans-serif;padding-left: 22px;\" colspan=\"2\">Report is for the " + Convert.ToString(dtCPSPrint.Rows[0]["PressName"]) + ", SN " + Convert.ToString(dtCPSPrint.Rows[0]["SerialNum"]) + ", at the " + Convert.ToString(dtCPSPrint.Rows[0]["BranchName"]) + " branch of " + Convert.ToString(dtCPSPrint.Rows[0]["ProjectName"]) + " in " + Convert.ToString(dtCPSPrint.Rows[0]["BranchLocation"]) + "</td><td width=\"112px\" bgcolor=\"#5A5A5A\"></td></tr>" +
                            "<tr style=\"height:25px\"><td align=\"center\" valign=\"middle\" bgcolor=\"#C1C1C1\" style=\"color:#000000; font-weight:bold ; font-size:8px; font-family:Tahoma, Arial, Helvetica, sans-serif;\" width=\"150px\" >" + DateTime.Now.ToLongDateString() + "</td> " +
                            "<td align=\"left\" valign=\"middle\" bgcolor=\"#C1C1C1\" style=\"color:#000000; font-weight:bold ; font-size:8px; font-family:Tahoma, Arial, Helvetica, sans-serif; \" width=\"150px\">&copy; Fujifilm " + DateTime.Now.Year.ToString() + "</td>" +
                            "<td width=\"112px\" align=\"center\" valign=\"middle\" bgcolor=\"#5A5A5A\" style=\"color:#CCCCCC; font-weight:bold ; font-size:8px; font-family:Tahoma, Arial, Helvetica, sans-serif; \">Page " + i.ToString() + " of " + theCount.ToString() + " </td></tr></table>";
                        theDoc.AddImageHtml(str);
                    }
                }
                else
                {
                    theDoc.Rect.String = "-10 0 622 30";
                    for (int i = 1; i <= theCount; i++)
                    {
                        theDoc.HPos = 0.0;
                        theDoc.VPos = 0.0;
                        theDoc.PageNumber = i;

                        str = "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\"> " +
                            "<tr style=\"height:25px\"><td align=\"center\" valign=\"middle\" bgcolor=\"#C1C1C1\" style=\"color:#000000; font-weight:bold ; font-size:8px; font-family:Tahoma, Arial, Helvetica, sans-serif;\" width=\"150px\" >" + DateTime.Now.ToLongDateString() + "</td> " +
                            "<td align=\"center\" valign=\"middle\" bgcolor=\"#C1C1C1\" style=\"color:#000000; font-weight:bold ; font-size:8px; font-family:Tahoma, Arial, Helvetica, sans-serif; \">&copy; Fujifilm " + DateTime.Now.Year.ToString() + "</td> " +
                            "<td width=\"112px\" align=\"center\" valign=\"middle\" bgcolor=\"#5A5A5A\" style=\"color:#CCCCCC; font-weight:bold ; font-size:8px; font-family:Tahoma, Arial, Helvetica, sans-serif; \">Page " + i.ToString() + " of " + theCount.ToString() + " </td></tr></table>";
                        theDoc.AddImageHtml(str);
                    }
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

            str = str.Replace("##Chart1_PDF##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_DeviceReport1.Jpeg"));
            str = str.Replace("##Chart2_PDF##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_DeviceReport2.Jpeg"));

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
            str = str.Replace("##ColorDetail_1##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_ColorDetail_1.Jpeg"));

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
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_MaxDeltaE1"] == true) { str = str.Replace("##CHART_MaxDeltaE_1##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_MaxDeltaE_Shift1.Jpeg")).Replace("##MAXDELTAE_O##", "").Replace("##MAXDELTAE_C##", ""); } else { str = str.Replace("##MAXDELTAE_O##", "<!--").Replace("##MAXDELTAE_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_Density1"] == true) { str = str.Replace("##CHART_Density_1##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_Density_Shift1.Jpeg")).Replace("##DENSITY_O##", "").Replace("##DENSITY_C##", ""); } else { str = str.Replace("##DENSITY_O##", "<!--").Replace("##DENSITY_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_DotGain1"] == true) { str = str.Replace("##DotGain_Shift1##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_DotGain_Shift1.Jpeg")).Replace("##DOTGAIN_O##", "").Replace("##DOTGAIN_C##", ""); } else { str = str.Replace("##DOTGAIN_O##", "<!--").Replace("##DOTGAIN_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_PaperWhite1"] == true) { str = str.Replace("##CHART_PaperWhite_1##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_PaperWhite_Shift1.Jpeg")).Replace("##PAPERWHITE_O##", "").Replace("##PAPERWHITE_C##", ""); } else { str = str.Replace("##PAPERWHITE_O##", "<!--").Replace("##PAPERWHITE_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_TempHumidity1"] == true) { str = str.Replace("##CHART_TempHumidity_1##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_TempHumidity_Shift1.Jpeg")).Replace("##HUMIDITY_O##", "").Replace("##HUMIDITY_C##", ""); } else { str = str.Replace("##HUMIDITY_O##", "<!--").Replace("##HUMIDITY_C##", "-->"); }
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
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_MaxDeltaE2"] == true) { str = str.Replace("##CHART_MaxDeltaE_2##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_MaxDeltaE_Shift2.Jpeg")).Replace("##MAXDELTAE_O##", "").Replace("##MAXDELTAE_C##", ""); } else { str = str.Replace("##MAXDELTAE_O##", "<!--").Replace("##MAXDELTAE_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_Density2"] == true) { str = str.Replace("##CHART_Density_2##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_Density_Shift2.Jpeg")).Replace("##DENSITY_O##", "").Replace("##DENSITY_C##", ""); } else { str = str.Replace("##DENSITY_O##", "<!--").Replace("##DENSITY_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_DotGain2"] == true) { str = str.Replace("##Chart_Dotgain2##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_DotGain_Shift2.Jpeg")).Replace("##DOTGAIN_O##", "").Replace("##DOTGAIN_C##", ""); } else { str = str.Replace("##DOTGAIN_O##", "<!--").Replace("##DOTGAIN_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_PaperWhite2"] == true) { str = str.Replace("##CHART_PaperWhite_2##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_PaperWhite_Shift2.Jpeg")).Replace("##PAPERWHITE_O##", "").Replace("##PAPERWHITE_C##", ""); } else { str = str.Replace("##PAPERWHITE_O##", "<!--").Replace("##PAPERWHITE_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_TempHumidity2"] == true) { str = str.Replace("##CHART_TempHumidity_2##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_TempHumidity_Shift2.Jpeg")).Replace("##HUMIDITY_O##", "").Replace("##HUMIDITY_C##", ""); } else { str = str.Replace("##HUMIDITY_O##", "<!--").Replace("##HUMIDITY_C##", "-->"); }
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
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_MaxDeltaE1"] == true) { str = str.Replace("##CHART_MaxDeltaE_1##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_MaxDeltaE_Device_1.Jpeg")).Replace("##MAXDELTAE_O##", "").Replace("##MAXDELTAE_C##", ""); } else { str = str.Replace("##MAXDELTAE_O##", "<!--").Replace("##MAXDELTAE_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_Density1"] == true) { str = str.Replace("##CHART_Density_1##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_Density_Device_1.Jpeg")).Replace("##DENSITY_O##", "").Replace("##DENSITY_C##", ""); } else { str = str.Replace("##DENSITY_O##", "<!--").Replace("##DENSITY_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_DotGain1"] == true) { str = str.Replace("##Chart_DotGain_Device_1##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_DotGain_Device_1.Jpeg")).Replace("##DOTGAIN_O##", "").Replace("##DOTGAIN_C##", ""); } else { str = str.Replace("##DOTGAIN_O##", "<!--").Replace("##DOTGAIN_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_PaperWhite1"] == true) { str = str.Replace("##CHART_PaperWhite_1##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_PaperWhite_Device_1.Jpeg")).Replace("##PAPERWHITE_O##", "").Replace("##PAPERWHITE_C##", ""); } else { str = str.Replace("##PAPERWHITE_O##", "<!--").Replace("##PAPERWHITE_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_TempHumidity1"] == true) { str = str.Replace("##CHART_TempHumidity_1##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_TempHumidity_Device_1.Jpeg")).Replace("##HUMIDITY_O##", "").Replace("##HUMIDITY_C##", ""); } else { str = str.Replace("##HUMIDITY_O##", "<!--").Replace("##HUMIDITY_C##", "-->"); }
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
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_MaxDeltaE2"] == true) { str = str.Replace("##CHART_MaxDeltaE_2##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_MaxDeltaE_Device_2.Jpeg")).Replace("##MAXDELTAE_O##", "").Replace("##MAXDELTAE_C##", ""); } else { str = str.Replace("##MAXDELTAE_O##", "<!--").Replace("##MAXDELTAE_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_Density2"] == true) { str = str.Replace("##CHART_Density_2##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_Density_Device_2.Jpeg")).Replace("##DENSITY_O##", "").Replace("##DENSITY_C##", ""); } else { str = str.Replace("##DENSITY_O##", "<!--").Replace("##DENSITY_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_DotGain2"] == true) { str = str.Replace("##Chart_Dotgain_device2##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_Dotgain_device2.Jpeg")).Replace("##DOTGAIN_O##", "").Replace("##DOTGAIN_C##", ""); } else { str = str.Replace("##DOTGAIN_O##", "<!--").Replace("##DOTGAIN_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_PaperWhite2"] == true) { str = str.Replace("##CHART_PaperWhite_2##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_PaperWhite_Device_2.Jpeg")).Replace("##PAPERWHITE_O##", "").Replace("##PAPERWHITE_C##", ""); } else { str = str.Replace("##PAPERWHITE_O##", "<!--").Replace("##PAPERWHITE_C##", "-->"); }
            if ((bool)System.Web.HttpContext.Current.Session["CHRT_TempHumidity2"] == true) { str = str.Replace("##CHART_TempHumidity_2##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_TempHumidity_Device_2.Jpeg")).Replace("##HUMIDITY_O##", "").Replace("##HUMIDITY_C##", ""); } else { str = str.Replace("##HUMIDITY_O##", "<!--").Replace("##HUMIDITY_C##", "-->"); }
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

            str = str.Replace("##ColorDetail_1##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_ColorDetail_1.Jpeg"));
            str = str.Replace("##ColorDetail_2##", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_ColorDetail_2.Jpeg"));


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
            if (Fuji.Common.Sessions.RoleDesc == Enums.RoleType.PrintBuyer)
            {
                string PBName = "<td class=\"BOLD1\" align=\"left\" width=\"20%\">Print Buyer</td><td align=\"left\">" + Fuji.Common.Sessions.FirstName + " " + Fuji.Common.Sessions.LastName + "</td>";
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
                str = str.Replace("###TargetVersusActualAB###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_TargetMeasurement_TargetVersusActualAB_1.Jpeg"));
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
                str = str.Replace("###SummaryDelta_2###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_TargetMeasurement_SummaryDelta_2.Jpeg"));

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
                str = str.Replace("###ChartDeltaE2000###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_TargetMeasurement_ChartDeltaE2000_3.Jpeg"));

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
                str = str.Replace("###ChartDeltaE2000###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_TargetMeasurement_ChartDeltaE2000_3.Jpeg"));
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

        //2009_06_15 by Softweb - Ajay // Start Comment 
        //#region Report 10 : Job Run Certification Report
        //private string TextReturn_ReportJobRunCertification(string str)
        //{
        //    int intTable = 0;
        //    // === Image string to replace in chart tables === //

        //    string strStatusImageGreen = "<img src=\"##IMAGES##Proofok.gif\" width='13px' height='13px' >";
        //    string strStatusImageRed = "<img src=\"##IMAGES##ProofFailed.gif\" width='13px' height='13px' >";
        //    string strStatusImageCaution = "<img src=\"##IMAGES##ProofWarning.gif\" width='13px' height='13px' >";


        //    if (DS_Common.Tables.Count > 10 && DS_Common.Tables[10] != null && DS_Common.Tables[10].Rows.Count > 0 && Convert.ToString(DS_Common.Tables[10].Rows[0]["IsShowError"]) == "0")
        //    {
        //        strStatusImageGreen = "";
        //        strStatusImageRed = "";
        //        strStatusImageCaution = "";
        //    }


        //    DataTable DT = new DataTable();

        //    str = str.Replace("#csspath#", CSSPath);

        //    #region ==== Table = 0,  Job Detail  ====
        //    intTable = 0;
        //    if (DS_Common.Tables[intTable] != null && DS_Common.Tables[intTable].Rows.Count > 0)
        //    {
        //        DT = DS_Common.Tables[intTable];

        //        str = str.Replace("###JobName###", Convert.ToString(DT.Rows[0]["JobName"]).Trim());
        //        str = str.Replace("###Location###", Convert.ToString(DT.Rows[0]["Location"]).Trim());
        //        str = str.Replace("###PressName###", Convert.ToString(DT.Rows[0]["PressName"]).Trim());
        //        str = str.Replace("###TimeStamp###", Convert.ToString(Convert.ToDateTime(DT.Rows[0]["JobCreatedDT"]).ToString("MM/dd/yy - hh:mm tt")).Trim());
        //        str = str.Replace("###Substrate###", Convert.ToString(DT.Rows[0]["Substrate"]).Trim());
        //    }
        //    else
        //    {
        //        str = str.Replace("###JobName###", "");
        //        str = str.Replace("###Location###", "");
        //        str = str.Replace("###PressName###", "");
        //        str = str.Replace("###TimeStamp###", "");
        //        str = str.Replace("###Substrate###", "");
        //    }
        //    #endregion

        //    #region ==== Ink Detail ====
        //    // === Table = 1 : Ink Detail === //
        //    intTable = 1;
        //    string strInkInformation = "";
        //    if (DS_Common.Tables[intTable] != null && DS_Common.Tables[intTable].Rows.Count > 0)
        //    {
        //        for (int i = 0; i < DS_Common.Tables[intTable].Rows.Count; i++)
        //            strInkInformation += "<tr><td>Ink " + Convert.ToString(DS_Common.Tables[intTable].Rows[i]["InkWellNum"]) + "&nbsp;:</td><td>" + Convert.ToString(DS_Common.Tables[intTable].Rows[i]["InkName"]) + "</td></tr>";
        //    }
        //    str = str.Replace("###InkInformation###", strInkInformation);
        //    #endregion

        //    #region ==== Target Vs Actual AB Plot ====
        //    // === Table = 2 : Target Versus Actual AB === //
        //    intTable = 2;
        //    //if (DS_Common.Tables[intTable] != null && DS_Common.Tables[intTable].Rows.Count > 0)
        //    str = str.Replace("###TargetVersusActualAB###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_TargetVersusActualAB_1.Jpeg"));
        //    //else
        //    //str = str.Replace("###TargetVersusActualAB###", "");
        //    #endregion

        //    #region ==== Result Summary : Error ====
        //    // === Table = 3 : Target Versus Actual AB === //
        //    intTable = 3;
        //    //if (DS_Common.Tables[intTable] != null && DS_Common.Tables[intTable].Rows.Count > 0)
        //    //{
        //    str = str.Replace("###patchErrorImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_ResultSummary_PatchError.Jpeg"));
        //    str = str.Replace("###ActualError###", Convert.ToString(DS_Common.Tables["ErrorRate"].Rows[0]["ErrorRate"]));
        //    //}
        //    //else
        //    //{
        //    //  str = str.Replace("###patchErrorImage###", "");
        //    //str = str.Replace("###ActualError###", "");
        //    //}
        //    #endregion

        //    #region ==== Result Summary : Delta E AB Value ====
        //    // === Table = 4 : Target Versus Actual AB === //
        //    intTable = 4;
        //    string strDeltaEABValue = "";
        //    if (DS_Common.Tables[intTable] != null && DS_Common.Tables[intTable].Rows.Count > 0)
        //    {

        //        string strStatusImg = "";
        //        for (int i = 0; i < DS_Common.Tables[intTable].Rows.Count; i++)
        //        {
        //            DataRow dr = DS_Common.Tables[intTable].Rows[i];// === Copy row to minimize code length ===
        //            if (Convert.ToInt32(dr["Result"]) == 1)
        //                strStatusImg = strStatusImageRed;
        //            else if (Convert.ToInt32(dr["Result"]) == 2)
        //                strStatusImg = strStatusImageCaution;
        //            else if (Convert.ToInt32(dr["Result"]) == 3)
        //                strStatusImg = strStatusImageGreen;

        //            strDeltaEABValue += "<tr style='height: 22px'>";
        //            strDeltaEABValue += "<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>";
        //            strDeltaEABValue += "<td valign='middle' style='font-size:8px' class='BottomBorder'>" + Convert.ToString(dr["ShortColorName"]) + "</td>";
        //            strDeltaEABValue += "<td valign='middle' align='center' class='BottomBorder'>" + Convert.ToDecimal(dr["AvgDeltaE"]).ToString("N2") + "&nbsp;</td>";
        //            strDeltaEABValue += "<td valign='middle' align='center' class='BottomBorder'>" + Convert.ToString(dr["DeltaE_L"]) + "&nbsp;</td>";
        //            strDeltaEABValue += "</tr>";
        //        }
        //        str = str.Replace("###DeltaESummaryDetail###", strDeltaEABValue);
        //    }
        //    else
        //    {
        //        str = str.Replace("###DeltaESummaryDetail###", "");
        //    }
        //    str = str.Replace("###ResultSummary_DeltaEValue###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_ResultSummary_DeltaEValue.Jpeg"));
        //    #endregion

        //    #region ==== Gray Balance : Gray_25 ====
        //    // === Table = 5 : Gray Balance AB 25 === //
        //    intTable = 5;
        //    str = str.Replace("###GrayBalanceAB25###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page2_AB25.Jpeg"));
        //    #endregion

        //    #region ==== Gray Balance : Gray_50 ====
        //    // === Table = 6 : Gray Balance AB 50 === //
        //    intTable = 6;
        //    str = str.Replace("###GrayBalanceAB50###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page2_AB50.Jpeg"));
        //    #endregion

        //    #region ==== Gray Balance : Gray_75 ====
        //    // === Table = 7 : Gray Balance AB 75 === //
        //    intTable = 7;
        //    str = str.Replace("###GrayBalanceAB75###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page2_AB75.Jpeg"));
        //    #endregion

        //    #region === Gray Balance L* Chart ===
        //    // === Gray Balance L* Chart === //

        //    str = str.Replace("###GrayBalanceL###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page2_GrayBalance_L.Jpeg"));
        //    #endregion


        //    #region ==== Gray Balance : Gray_NPD ====
        //    // === Table = 8 : Gray_NPD === //
        //    intTable = 8;
        //    str = str.Replace("###Page2_GrayNPD###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page2_GrayNPD.Jpeg"));
        //    #endregion

        //    #region ==== Gray Balance : K_NPD ====
        //    // === Table = 9 : K_NPD === //
        //    intTable = 9;
        //    str = str.Replace("###Page2_KNPD###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page2_KNPD.Jpeg"));
        //    #endregion


        //    //----------------- Page 3 ---------------- //

        //    //str = str.Replace("###Page3_TrendDeltaE###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page3_TrendDeltaE.Jpeg"));
        //    str = str.Replace("###Page3_ZoneDeltaE###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page3_ZoneDeltaE.Jpeg"));

        //    #region ==== Table 10, IsNonProcess AND IsShowError ====
        //    // === Table = 10 : IsNonProcessANDIsShowError === //
        //    intTable = 10;

        //    str = str.Replace("###Page3_DensityDeviationP###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DensitometricSummary_DensityDeviationP.Jpeg"));
        //    str = str.Replace("###Page3_DotgainDeviationP###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DensitometricSummary_DotgainDeviationP.Jpeg"));
        //    str = str.Replace("###Page3_PrintDeviationP###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DensitometricSummary_PrintDeviationP.Jpeg"));
        //    str = str.Replace("###Page3_TrapDeviationP###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DensitometricSummary_TrapDeviationP.Jpeg"));
        //    str = str.Replace("###Page3_DensityDeviationNP###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DensitometricSummary_DensityDeviationNP.Jpeg"));

        //    if (DS_Common.Tables[intTable].Rows.Count > 0 && DS_Common.Tables[intTable].Rows[0]["IsNonProcess"].ToString() == "1")
        //    {

        //        int intStartNP = str.IndexOf("###Start_P###");
        //        int intEndNP = str.IndexOf("###End_P###") + 11;
        //        string strPart = str.Substring(intStartNP, (intEndNP - intStartNP));
        //        str = str.Replace(strPart, "");
        //    }
        //    else
        //    {
        //        int intStartNP = str.IndexOf("###Start_NP###");
        //        int intEndNP = (str.IndexOf("###End_NP###") + 12);

        //        string strPart = str.Substring(intStartNP, (intEndNP - intStartNP));
        //        str = str.Replace(strPart, "");

        //        //string imgPath;
        //        //strDensitometricSummary += "<table cellspacing='0' cellpadding='0' style='border: SOLID 2PX #000;width: 650px;height:370px;background-color:Gray' border='0'><tr>";

        //        //imgPath = System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DensitometricSummary_DensityDeviationP.Jpeg");
        //        //strDensitometricSummary += "<td style='width:325px'><img src='" + imgPath + "' height='120px' width='645px' alt='Gray 75' id='img8' /></td>";

        //        //imgPath = System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DensitometricSummary_DotgainDeviationP.Jpeg");
        //        //strDensitometricSummary += "<td style='width:325px'><img src='" + imgPath + "' height='120px' width='645px' alt='Gray 75' id='img8' /></td>";
        //        //strDensitometricSummary += "</tr><tr>";

        //        //imgPath = System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DensitometricSummary_PrintDeviationP.Jpeg");
        //        //strDensitometricSummary += "<td style='width:325px'><img src='" + imgPath + "' height='120px' width='645px' alt='Gray 75' id='img8' /></td>";

        //        //imgPath = System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DensitometricSummary_TrapDeviationP.Jpeg");
        //        //strDensitometricSummary += "<td style='width:325px'><img src='" + imgPath + "' height='120px' width='645px' alt='Gray 75' id='img8' /></td>";
        //        //strDensitometricSummary += "</tr></table>";
        //    }
        //    str = str.Replace("###Start_P###", "");
        //    str = str.Replace("###End_P###", "");
        //    str = str.Replace("###Start_NP###", "");
        //    str = str.Replace("###End_NP###", "");

        //    str = str.Replace("###Page4_TrendLine###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_TrendLine.Jpeg"));
        //    str = str.Replace("###Page4_DeltaE###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DeltaE.Jpeg"));
        //    str = str.Replace("###Page4_Density###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Density.Jpeg"));
        //    str = str.Replace("###Page4_Dotgain###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Dotgain.Jpeg"));
        //    str = str.Replace("###Page4_PrintContrast###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_PrintContrast.Jpeg"));
        //    #endregion

        //    //str = str.Replace("###DensitometricSummary###", strDensitometricSummary);



        //    #region Assign Server
        //    str = str.Replace("##IMAGES##", strServer);
        //    #endregion

        //    return str;
        //}
        //#endregion
        //2009_06_15 by Softweb - Ajay // End Comment 


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
            str = str.Replace("###TargetVersusActualAB###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_TargetVersusActualAB_1.Jpeg"));
            #endregion

            // === Table = "ErrorRate" : Target Versus Actual AB === //
            #region ==== Result Summary : Error ====
            str = str.Replace("###patchErrorImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_ResultSummary_PatchError.Jpeg"));
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
            str = str.Replace("###ResultSummary_DeltaEValue###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_ResultSummary_DeltaEValue.Jpeg"));
            #endregion

            // === Gray Balance AB 25 === //
            #region ==== Gray Balance : Gray_25 ====
            str = str.Replace("###GrayBalanceAB25###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page2_AB25.Jpeg"));
            #endregion

            // === Gray Balance AB 50 === //
            #region ==== Gray Balance : Gray_50 ====
            str = str.Replace("###GrayBalanceAB50###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page2_AB50.Jpeg"));
            #endregion

            // === Gray Balance AB 75 === //
            #region ==== Gray Balance : Gray_75 ====
            str = str.Replace("###GrayBalanceAB75###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page2_AB75.Jpeg"));
            #endregion

            // === Gray Balance L* Chart === //
            #region === Gray Balance L* Chart ===
            str = str.Replace("###GrayBalanceL###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page2_GrayBalance_L.Jpeg"));
            #endregion


            // === Gray_NPD === //
            #region ==== Gray Balance : Gray_NPD ====
            str = str.Replace("###Page2_GrayNPD###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page2_GrayNPD.Jpeg"));
            #endregion

            // === K_NPD === //
            #region ==== Gray Balance : K_NPD ====
            str = str.Replace("###Page2_KNPD###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page2_KNPD.Jpeg"));
            #endregion


            //----------------- Page 3 ---------------- //

            //str = str.Replace("###Page3_TrendDeltaE###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page3_TrendDeltaE.Jpeg"));
            str = str.Replace("###Page3_ZoneDeltaE###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page3_ZoneDeltaE.Jpeg"));

            // === Process  Densitometric Summary === //
            #region ==== Process  Densitometric Summary  ====
            str = str.Replace("###Page3_DensityDeviationP###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DensitometricSummary_DensityDeviationP.Jpeg"));
            str = str.Replace("###Page3_DotgainDeviationP###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DensitometricSummary_DotgainDeviationP.Jpeg"));
            str = str.Replace("###Page3_PrintDeviationP###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DensitometricSummary_PrintDeviationP.Jpeg"));
            str = str.Replace("###Page3_TrapDeviationP###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DensitometricSummary_TrapDeviationP.Jpeg"));

            #endregion

            // === Job Trands Charts === //
            #region ===== Job Trands Charts =====
            str = str.Replace("###Page4_TrendLine###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_TrendLine.Jpeg"));
            str = str.Replace("###Page4_DeltaE###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DeltaE.Jpeg"));
            str = str.Replace("###Page4_Density###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Density.Jpeg"));
            str = str.Replace("###Page4_Dotgain###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Dotgain.Jpeg"));
            str = str.Replace("###Page4_PrintContrast###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_PrintContrast.Jpeg"));
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
            str = str.Replace("###TargetVersusActualAB###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_TargetVersusActualAB_1.Jpeg"));
            #endregion

            // === Table = "ErrorRate" : Target Versus Actual AB === //
            #region ==== Result Summary : Error ====
            str = str.Replace("###patchErrorImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_ResultSummary_PatchError.Jpeg"));
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
            str = str.Replace("###ResultSummary_DeltaEValue###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_ResultSummary_DeltaEValue.Jpeg"));
            #endregion

            //----------------- Page 2 ---------------- //
            //str = str.Replace("###Page3_TrendDeltaE###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page3_TrendDeltaE.Jpeg"));
            str = str.Replace("###Page3_ZoneDeltaE###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Page3_ZoneDeltaE.Jpeg"));

            // === Non Process  Densitometric Summary === //
            #region ==== Non Process  Densitometric Summary  ====
            str = str.Replace("###Page3_DensityDeviationNP###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DensitometricSummary_DensityDeviationNP.Jpeg"));
            #endregion

            // === Job Trands Charts === //
            #region ===== Job Trands Charts =====
            str = str.Replace("###Page4_TrendLine###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_TrendLine.Jpeg"));
            str = str.Replace("###Page4_DeltaE###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_DeltaE.Jpeg"));
            str = str.Replace("###Page4_Density###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Density.Jpeg"));
            str = str.Replace("###Page4_Dotgain###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_Dotgain.Jpeg"));
            str = str.Replace("###Page4_PrintContrast###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_JobRunCertification_PrintContrast.Jpeg"));
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
            //Fuji.Common.CommonFunctions.FileTimeLog("Start Page 1 JobDetail", false);

            #region ==== Table = 0,  Job Detail  ====
            if (DS_Common.Tables[0] != null && DS_Common.Tables[0].Rows.Count > 0)
            {
                DT = DS_Common.Tables[0];

                if (Convert.ToInt16(DT.Rows[0]["Status"]) == 0)
                    strJobStatus = "<table border='0' align='left' cellpadding='0' cellspacing='0' class='failed-title-bg' style='background: url(##URLIMAGES##failed-title-bg.gif) repeat-x left top;'><tr><td width='47'><img src='##IMAGES##failed-icon.gif' style='margin: 7px 0 0 5px;' /></td><td class='failed-title-text'>Failed</td></tr></table>"; // "<img src=\"##IMAGES##ProofFailed.gif\" width='20px' height='20px' >" + "&nbspFailed";
                else if (Convert.ToInt16(DT.Rows[0]["Status"]) == 1)
                    strJobStatus = "<table border='0' align='left' cellpadding='0' cellspacing='0' class='warning-title-bg' style='background: url(##URLIMAGES##warning-title-bg.gif) repeat-x left top;'><tr><td width='47'><img src='##IMAGES##warning-icon.gif' style='margin: 7px 0 0 5px;' /></td><td class='warning-title-text'>Caution</td></tr></table>"; // "<img src=\"##IMAGES##ProofWarning.gif\" width='20px' height='20px' >" + "&nbspCaution";
                else if (Convert.ToInt16(DT.Rows[0]["Status"]) == 2)
                    strJobStatus = "<table border='0' align='left' cellpadding='0' cellspacing='0' class='passed-title-bg' style='background: url(##URLIMAGES##passed-title-bg.gif) repeat-x left top;'><tr><td width='40'><img src='##IMAGES##passed-icon.gif' style='margin: 7px 0 0 5px;' /></td><td class='passed-title-text'>Passed</td></tr></table>"; // "<img src=\"##IMAGES##Proofok.gif\" width='20px' height='20px' >" + "&nbspPassed";

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
                SBStr.Replace("###TimeStamp###", Fuji.Common.CommonFunctions.GetDateByCultures(Convert.ToDateTime(DT.Rows[0]["CreatedDT"])));
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
            //Fuji.Common.CommonFunctions.FileTimeLog("Start Page 1 a* b* ", false);
            #region ==== Target Vs Actual AB Plot ====
            SBStr.Replace("###TargetVersusActualAB###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page1_TargetVersusActualAB.Jpeg"));
            #endregion

            // === Table 2 = Chart of Summary Delta Value Graph Image And Table Value Ploted === //
            //Fuji.Common.CommonFunctions.FileTimeLog("Start Page 1 Detlta", false);
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
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["AverageDE_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["AverageDE_TH"])) + "&nbsp;</td>");
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
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["MaximumDE_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["MaximumDE_TH"])) + "&nbsp;</td>");
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
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["PrimaryDE_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["PrimaryDE_TH"])) + "&nbsp;</td>");
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
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["PrimaryDH_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["PrimaryDH_TH"])) + "&nbsp;</td>");
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
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["GrayAverageDH_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["GrayAverageDH_TH"])) + "&nbsp;</td>");
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
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["dE_95pct_Patches_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["dE_95pct_Patches_TH"])) + "&nbsp;</td>");
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
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["dE_OuterGamut_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[0]["dE_OuterGamut_TH"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("</tr>");
                }
                #endregion

                SBStr.Replace("###DeltaSummaryDetail###", SBstrDeltaValue.ToString());
            }
            else
            {
                SBStr.Replace("###DeltaSummaryDetail###", "");
            }
            SBStr.Replace("###ResultSummary_DeltaImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page1_ResultSummary_Delta.Jpeg"));
            #endregion

            // === Table 3 = Chart of Summary Delta E Value Graph Image And Table Value Ploted  === //
            //Fuji.Common.CommonFunctions.FileTimeLog("Start Page 1 Delta E", false);
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
                    SBstrDeltaEValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["DE"])) + "&nbsp;</td>");
                    SBstrDeltaEValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["TOL"])) + "&nbsp;</td>");
                    SBstrDeltaEValue.Append("</tr>");
                }
                SBStr.Replace("###DeltaESummaryDetail###", SBstrDeltaEValue.ToString());
            }
            else
            {
                SBStr.Replace("###DeltaESummaryDetail###", "");
            }
            SBStr.Replace("###ResultSummary_DeltaEImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page1_ResultSummary_DeltaEValue.Jpeg"));
            #endregion

            // === Table 4 = Chart of Gray L* a* b* Graph Image And Table Value Ploted  === //
            //Fuji.Common.CommonFunctions.FileTimeLog("Start Page 2 L* A* B*", false);
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
                SBStr.Replace("###GrayLDeviation_GraphImage###", "<table cellpadding='0' cellspacing='0' width='100%' border='0'><tr><td class='summary-bg' style='background: url(##URLIMAGES##summary-title-bg.gif) repeat-x left top;'><img src='##IMAGES##summary-icon.png' width='12' height='11' style='margin: 2px 0 0 5px;' /><span class='summary-title1'>Gray L* Deviation Graph</span></td></tr><tr><td ><img src='" + System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page2_GrayDeviation_L.Jpeg") + "' height='110px' width='658px' alt='Gray L* Deviation' id='img2' /></td></tr></table>");
                SBStr.Replace("###GrayADeviation_GraphImage###", "<table cellpadding='0' cellspacing='0' width='100%' border='0'><tr><td class='summary-bg' style='background: url(##URLIMAGES##summary-title-bg.gif) repeat-x left top;'><img src='##IMAGES##summary-icon.png' width='12' height='11' style='margin: 2px 0 0 5px;' /><span class='summary-title1'>Gray a* Deviation Graph</span></td></tr><tr><td ><img src='" + System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page2_GrayDeviation_A.Jpeg") + "' height='110px' width='658px' alt='Gray a* Deviation' id='img5' /></td></tr></table>");
                SBStr.Replace("###GrayBDeviation_GraphImage###", "<table cellpadding='0' cellspacing='0' width='100%' border='0'><tr><td class='summary-bg' style='background: url(##URLIMAGES##summary-title-bg.gif) repeat-x left top;'><img src='##IMAGES##summary-icon.png' width='12' height='11' style='margin: 2px 0 0 5px;' /><span class='summary-title1'>Gray b* Deviation Graph</span></td></tr><tr><td ><img src='" + System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page2_GrayDeviation_B.Jpeg") + "' height='110px' width='658px' alt='Gray b* Deviation' id='img6' /></td></tr></table>");

                SBstrGrayLABValue.Append("<table cellpadding='0' cellspacing='0' width='100%' border='0'><tr><td><table width='660' border='0' cellspacing='0' cellpadding='0'><tr><td class='summary-bg' style='background: url(##URLIMAGES##summary-title-bg.gif) repeat-x left top;'><img src='##IMAGES##summary-icon.png' width='12' height='11' style='margin: 2px 0 0 5px;' /><span class='summary-title1'>Gray Patch Values</span></td></tr></table></td></tr><tr><td><table cellpadding='0' cellspacing='0' width='100%' border='0'>###VALUE###</table></td></tr></table>");

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    SBstrPatch.Append("<td align='center' class='row-content1'>" + DT.Rows[i]["GRAY"].ToString() + "</td>");
                    SBstrLActual.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["DL"])) + "</td>");
                    SBstrAActual.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["DA"])) + "</td>");
                    SBstrBActual.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["DB"])) + "</td>");
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
            //Fuji.Common.CommonFunctions.FileTimeLog("Start Page 2 Gray NPD", false);
            #region ==== Gray NPD Graph ====
            DT = DS_Common.Tables[4];
            if (DT != null && DT.Rows.Count > 0 && DT.Rows[0][0].ToString() != "NO DATA")
            {
                StringBuilder SBstrGrayNPDValue = new StringBuilder();
                StringBuilder SBstrNPDGrayPatch = new StringBuilder();
                StringBuilder SBstrNPDGrayActual = new StringBuilder();

                SBStr.Replace("###GrayNPD_GraphImage###", "<table cellpadding='0' cellspacing='0' width='100%' border='0' ><tr><td style='width:310px; height:270px'><img src='" + System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page2_GrayNPD.Jpeg") + "' height='270px' width='310px' alt='Gray NPD' id='img7' /></td></tr><tr><td height='3'></td></tr><tr><td align='center'>###NPDGrayValue###</td></tr></table>");
                SBstrGrayNPDValue.Append("<table cellpadding='0' cellspacing='0' width='100%' border='0'>");

                SBstrNPDGrayPatch.Append("<tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'><td class='row-content1'>&nbsp;</td>");
                SBstrNPDGrayActual.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Target</td>");

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    SBstrNPDGrayPatch.Append("<td align='center' class='row-content1'>" + Convert.ToInt64(DT.Rows[i]["GRAY"]).ToString() + "</td>");
                    SBstrNPDGrayActual.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Density"])) + "</td>");
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
            //Fuji.Common.CommonFunctions.FileTimeLog("Start Page 2 Balck NPD", false);
            #region ==== Black NPD Graph ====
            SBStr.Replace("###BlackNPD_GraphImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page2_BlackNPD.Jpeg"));
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
                    SBstrNPDBlackActual.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Density"])) + "</td>");
                }
                SBstrNPDBlackPatch.Append("</tr>");
                SBstrNPDBlackActual.Append("</tr>");

                SBstrBlackNPDValue.Append(SBstrNPDBlackPatch.ToString() + SBstrNPDBlackActual.ToString() + "</table>");
                SBStr.Replace("###NPDBlackValue###", SBstrBlackNPDValue.ToString());
            }

            #endregion

            // === Table 6 = Chart of Actual Dot Percentage Graph Image And Value Ploted === //
            //Fuji.Common.CommonFunctions.FileTimeLog("Start Page 3 Dotgain Actual", false);
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


                SBStr.Replace("###DotgainActual_GraphImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page3_CHRT_DotgainActual.Jpeg"));
                SBstrDotGainValue.Append("<table cellpadding='0' cellspacing='0' width='100%' border='0' style='border: SOLID 2PX #000'><tr><td align='center'><table width='660' border='0' cellspacing='0' cellpadding='0'><tr><td class='summary-bg' style='background: url(##URLIMAGES##summary-title-bg.gif) repeat-x left top;'><img src='##IMAGES##summary-icon.png' width='12' height='11' style='margin: 2px 0 0 5px;' /><span class='summary-title1'>Patch DG Data</span></td></tr></table></td></tr><tr><td><table cellpadding='0' cellspacing='0' width='100%' border='0'>###VALUE###</table></td></tr></table>");

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
                    SBstrCDG.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                    SBstrMDG.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                    SBstrYDG.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                    SBstrKDG.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                }

                for (int i = 0; i < intArrDistingRec.Count; i++)
                {
                    if (intArrDistingRec[i].ToString() != "0")
                    {
                        SBstrDGPatch.Append("<td align='center'>" + intArrDistingRec[i].ToString() + "</td>");
                        DV.RowFilter = "CMYK_C = " + intArrDistingRec[i].ToString();
                        if (DV.Count > 0)
                            SBstrCDG.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                        else
                            SBstrCDG.Append("<td align='center'>&nbsp;</td>");

                        DV.RowFilter = "CMYK_M = " + intArrDistingRec[i].ToString();
                        if (DV.Count > 0)
                            SBstrMDG.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                        else
                            SBstrMDG.Append("<td align='center'>&nbsp;</td>");


                        DV.RowFilter = "CMYK_Y = " + intArrDistingRec[i].ToString();
                        if (DV.Count > 0)
                            SBstrYDG.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                        else
                            SBstrYDG.Append("<td align='center'>&nbsp;</td>");


                        DV.RowFilter = "CMYK_K = " + intArrDistingRec[i].ToString();
                        if (DV.Count > 0)
                            SBstrKDG.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
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
            //Fuji.Common.CommonFunctions.FileTimeLog("Start Page 3 Dotgain Deviation", false);
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

                SBStr.Replace("###DotgainDeviation_GraphImage###", "<td><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td class='summary-bg' align='center' style='background: url(##URLIMAGES##summary-title-bg.gif) repeat-x left top;'><span class='summary-title1'>Dotgain Deviation</span></td></tr><tr><td><img src='" + System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page3_DotgainDeviation.Jpeg") + "' height='330px' width='220px' alt='Dotgain Deviation' id='img11' /></td></tr></table></td>");

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

                    SBstrDotTarget.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Targ_DotGain"])) + "</td>");
                    SBstrDotActual.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["DotGain_ACT"])) + "</td>");
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
            //Fuji.Common.CommonFunctions.FileTimeLog("Start Page 3 Density Deviation", false);
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
                SBStr.Replace("###DensityDeviation_GraphImage###", "<td><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td class='summary-bg' align='center' style='background: url(##URLIMAGES##summary-title-bg.gif) repeat-x left top;'><span class='summary-title1'>Density Deviation</span></td></tr><tr><td><img src='" + System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page3_DensityDeviation.Jpeg") + "' height='330px' width='220px' alt='Density Deviation' id='img13' /></td></tr></table></td>");
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

                    SBstrDenTarget.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Density_Targ"])) + "</td>");
                    SBstrDenActual.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Density"])) + "</td>");
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
            //Fuji.Common.CommonFunctions.FileTimeLog("Start Page 3 Print Contrast Deviation", false);
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
                SBStr.Replace("###PrintContrastDeviation_GraphImage###", "<td><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td class='summary-bg' align='center' style='background: url(##URLIMAGES##summary-title-bg.gif) repeat-x left top;'><span class='summary-title1'>Print Contrast Deviation</span></td></tr><tr><td><img src='" + System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_SingleMeasurement_Page3_PrintContrastDeviation.Jpeg") + "' height='330px' width='220px' alt='Print Contrast Deviation' id='img15' /></td></tr></table></td>");
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

                    SBstrPrintTarget.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Print_Contrast_Tol"])) + "</td>");
                    SBstrPrintActual.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(Formulas.DotGain.CalcPrintContrast(Convert.ToDecimal(DT.Rows[i]["D100"]), Convert.ToDecimal(DT.Rows[i]["D75"])))) + "</td>");
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
            //Fuji.Common.CommonFunctions.FileTimeLog("Start Page 4 Table", false);
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
                SBstrTablePageHeader.Append("<td class='header-bg' style='background: url(##URLIMAGES##header-middle-bg.gif) repeat-x left top;'>");
                SBstrTablePageHeader.Append("<table width='642' border='0' cellspacing='0' cellpadding='0'>");
                SBstrTablePageHeader.Append("<tr>");
                SBstrTablePageHeader.Append("<td class='header-title'>Measurement Report</td>");
                SBstrTablePageHeader.Append("<td>&nbsp;</td>");
                SBstrTablePageHeader.Append("<td width='116' valign='middle'><img src='##IMAGES##powered-logo.gif' /></td>");
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
                    SBstrTableDetail.Append("<td style='width: 7%' align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["DE"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + DT.Rows[i]["Sample_Name"].ToString() + "</td>");
                    SBstrTableDetail.Append("<td style='width: 7%' align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["CMYK_C"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 7%' align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["CMYK_M"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 7%' align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["CMYK_Y"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 7%' align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["CMYK_K"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Target_L"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Target_a"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Target_b"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Actual_L"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Actual_a"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DT.Rows[i]["Actual_b"])) + "</td>");
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
            //SBStr.Replace("##URLIMAGES##", System.Web.HttpContext.Current.Request.Url.ToString().Substring(0, System.Web.HttpContext.Current.Request.Url.ToString().LastIndexOf('/')) + "/Images/");
            SBStr.Replace("##URLIMAGES##", WebSettings.ImageURL + "Images/");
            SBStr.Replace("##IMAGES##", strServer);
            #endregion

            return SBStr;
        }
        #endregion

        #region Report 14 : CPS Print Standard Report
        private StringBuilder TextReturn_CPSPrintStandardsReport(StringBuilder SBStr)
        {
            string strStatusImageGreen = "<img src=\"##IMAGES##Proofok.gif\" width='13px' height='13px' >";
            string strStatusImageRed = "<img src=\"##IMAGES##ProofFailed.gif\" width='13px' height='13px' >";
            string strNoRecordFound = "No Records Found";
            StringBuilder SBCommon = new StringBuilder();

            SBStr.Replace("#csspath#", CSSPath);

            DataTable currentTable = null;

            #region Page 1

            #region Report Front Information
            SBStr.Replace("###CURRENTDATE###", DateTime.Today.ToLongDateString());
            currentTable = DS_Common.Tables[0];
            if (currentTable != null && currentTable.Rows.Count > 0)
            {
                SBStr.Replace("###PressName###", Convert.ToString(currentTable.Rows[0]["PressName"]));
                SBStr.Replace("###BranchName###", Convert.ToString(currentTable.Rows[0]["BranchName"]));
                SBStr.Replace("###BranchLocation###", Convert.ToString(currentTable.Rows[0]["BranchLocation"]));
                SBStr.Replace("##RunNo##", Convert.ToString(currentTable.Rows[0]["RunCount"]));
            }
            else
            {
                SBStr.Replace("###PressName###", "&nbsp;");
                SBStr.Replace("###BranchName###", "&nbsp;");
                SBStr.Replace("###BranchLocation###", "&nbsp;");
            }
            #endregion

            #endregion

            #region Page 2

            #region Report Information
            currentTable = DS_Common.Tables[0];
            if (currentTable != null && currentTable.Rows.Count > 0)
            {
                SBStr.Replace("###ProjectName###", Convert.ToString(currentTable.Rows[0]["ProjectName"]));
                SBStr.Replace("###PressName###", Convert.ToString(currentTable.Rows[0]["PressName"]));
                SBStr.Replace("###TimeStamp###", Fuji.Common.CommonFunctions.GetDateByCultures(Convert.ToDateTime(currentTable.Rows[0]["StartDateTime"])));
                SBStr.Replace("###Substrate###", currentTable.Rows[0]["Substrate"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["Substrate"].ToString());
                SBStr.Replace("###InkManufacturer###", currentTable.Rows[0]["Ink"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["Ink"].ToString());
                SBStr.Replace("###BranchName###", Convert.ToString(currentTable.Rows[0]["BranchName"]));
                if (currentTable.Rows[0]["OutputTypeID"].ToString() == "1")
                {
                    SBStr.Replace("###PrintCond_1###", "Screening:");
                }
                else if (currentTable.Rows[0]["OutputTypeID"].ToString() == "2")
                {
                    SBStr.Replace("###PrintCond_1###", "Stencil:");
                }
                else if (currentTable.Rows[0]["OutputTypeID"].ToString() == "3")
                {
                    SBStr.Replace("###PrintCond_1###", "Print Mode:");
                }
                SBStr.Replace("###SCREENING-TYPE###", currentTable.Rows[0]["PrintCond"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["PrintCond"].ToString());
            }
            else
            {
                SBStr.Replace("###ProjectName###", "&nbsp;");
                SBStr.Replace("###PressName###", "&nbsp;");
                SBStr.Replace("###TimeStamp###", "&nbsp;");
                SBStr.Replace("###Substrate###", "&nbsp;");
                SBStr.Replace("###InkManufacturer###", "&nbsp;");
                SBStr.Replace("###BranchName###", "&nbsp;");
            }
            #endregion

            #region G7 Summary
            currentTable = DS_Common.Tables[1];
            SBCommon = new StringBuilder();
            if (currentTable != null && currentTable.Rows.Count > 0)
            {
                int rowCnt = 0;
                foreach (DataRow dr in currentTable.Rows)
                {
                    SBCommon.Append(((rowCnt++ % 2) == 0) ? "<tr class='row-2'>" : "<tr class='row-1'>");
                    SBCommon.Append("<td align='left' style='padding:4px;'>" + Convert.ToString(dr["G7Metrics"]) + "</td>");
                    SBCommon.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(dr["G7NPDC"])) + "</td>");
                    SBCommon.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(dr["ΔF*"])) + "</td>");
                    SBCommon.Append("</tr>");
                }
            }
            else
            {
                SBCommon.Append("<tr class='row-1'>");
                SBCommon.Append("<td align='center' colspan='3'>" + strNoRecordFound + "</td>");
                SBCommon.Append("</tr>");
            }
            SBStr.Replace("###G7Summary###", SBCommon.ToString());
            #endregion

            #region Gray ΔF* Quartile Summary
            currentTable = DS_Common.Tables[3];
            SBCommon = new StringBuilder();
            if (currentTable != null && currentTable.Rows.Count > 0)
            {
                int rowCnt = 0;
                foreach (DataRow dr in currentTable.Rows)
                {
                    SBCommon.Append(((rowCnt++ % 2) == 0) ? "<tr class='row-2'>" : "<tr class='row-1'>");
                    SBCommon.Append("<td align='left' style='padding:4px;'>" + Convert.ToString(dr["Gray Quartile"]) + "</td>");
                    SBCommon.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(dr["AvgΔF"])) + "</td>");
                    SBCommon.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(dr["MAXΔF"])) + "</td>");
                    SBCommon.Append("</tr>");
                }
            }
            else
            {
                SBCommon.Append("<tr class='row-1'>");
                SBCommon.Append("<td align='center' colspan='3'>" + strNoRecordFound + "</td>");
                SBCommon.Append("</tr>");
            }
            SBStr.Replace("###GrayQuartileSummary###", SBCommon.ToString());
            #endregion

            #region Gray Balance ΔF* Plot
            SBStr.Replace("###GrayBalanceF###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandards_Page2_GrayBalanceF.Jpeg"));
            #endregion

            #region Black NPD Plot
            SBStr.Replace("###BlackNPD###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandards_Page2_BlackNPD.Jpeg"));
            #endregion

            #region Gray NPD Plot
            SBStr.Replace("###GrayNPD###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandards_Page2_GrayNPD.Jpeg"));
            #endregion

            #region Black NPD Values
            currentTable = DS_Common.Tables[4];
            StringBuilder SBstrPatch = new StringBuilder();
            StringBuilder SBstrActual = new StringBuilder();
            StringBuilder SBstrTarget = new StringBuilder();

            SBstrPatch.Append("<tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'><td class='row-content1'>&nbsp;</td>");
            SBstrActual.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Targ</td>");
            SBstrTarget.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Act</td>");

            if (currentTable != null && currentTable.Rows.Count > 0 && currentTable.Rows[0][0].ToString() != "NO DATA")
            {
                SBStr.Replace("###DBP###", "2px");

                foreach (DataRow dr in currentTable.Rows)
                {
                    if (Convert.ToBoolean(dr["ShowData"]))
                    {
                        SBstrPatch.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.IntToString(Convert.ToString(dr["CMYK_K"])) + "</td>");
                        SBstrActual.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(dr["G7NPDC"])) + "</td>");
                        SBstrTarget.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(dr["Density"])) + "</td>");
                    }
                }
                SBstrPatch.Append("</tr>");
                SBstrActual.Append("</tr>");
                SBstrTarget.Append("</tr>");

                SBStr.Replace("###BlackNPDValues###", SBstrPatch.ToString() + SBstrActual.ToString() + SBstrTarget.ToString());
            }
            else
            {
                SBStr.Replace("###DBP###", "0px");
                SBStr.Replace("###BlackNPDValues###", "<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>");
            }
            #endregion

            #region Gray NPD Values
            currentTable = DS_Common.Tables[5];
            SBstrPatch = new StringBuilder();
            SBstrActual = new StringBuilder();
            SBstrTarget = new StringBuilder();

            SBstrPatch.Append("<tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'><td class='row-content1'>&nbsp;</td>");
            SBstrActual.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Targ</td>");
            SBstrTarget.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Act</td>");

            if (currentTable != null && currentTable.Rows.Count > 0 && currentTable.Rows[0][0].ToString() != "NO DATA")
            {
                SBStr.Replace("###DBP###", "2px");
                foreach (DataRow dr in currentTable.Rows)
                {
                    if (Convert.ToBoolean(dr["ShowData"]))
                    {
                        SBstrPatch.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.IntToString(Convert.ToString(dr["CMYK_C"])) + "</td>");
                        SBstrActual.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(dr["G7NPDC"])) + "</td>");
                        SBstrTarget.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(dr["Density"])) + "</td>");
                    }
                }
                SBstrPatch.Append("</tr>");
                SBstrActual.Append("</tr>");
                SBstrTarget.Append("</tr>");

                SBStr.Replace("###GrayNPDValues###", SBstrPatch.ToString() + SBstrActual.ToString() + SBstrTarget.ToString());
            }
            else
            {
                SBStr.Replace("###DBP###", "0px");
                SBStr.Replace("###GrayNPDValues###", "<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>");
            }
            #endregion

            #endregion

            #region Page 3

            #region ΔE Values
            currentTable = DS_Common.Tables[6];
            StringBuilder SBDeltaEPatches = new StringBuilder();
            if (currentTable != null && currentTable.Rows.Count > 0 && currentTable.Rows[0][0].ToString() != "NO DATA")
            {
                for (int i = 0; i < currentTable.Rows.Count; i++)
                {
                    SBDeltaEPatches.Append("<tr style='height: 17px'>");
                    if (Convert.ToDecimal(currentTable.Rows[i]["ΔE"]) < Convert.ToDecimal(currentTable.Rows[i]["ΔELimit"]))
                    {
                        SBDeltaEPatches.Append("<td valign='middle' align='center' style='padding-right: 5px' class='BottomBorder'>" + strStatusImageGreen + "</td>");
                    }
                    else
                    {
                        SBDeltaEPatches.Append("<td valign='middle' align='center' style='padding-right: 5px' class='BottomBorder'>" + strStatusImageRed + "</td>");
                    }

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString())
                        SBDeltaEPatches.Append("<td valign='middle' class='BottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>Cyan</td>");
                    else if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString())
                        SBDeltaEPatches.Append("<td valign='middle' class='BottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>Magenta</td>");
                    else if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString())
                        SBDeltaEPatches.Append("<td valign='middle' class='BottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>Yellow</td>");
                    else if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString())
                        SBDeltaEPatches.Append("<td valign='middle' class='BottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>Black</td>");
                    else if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString())
                        SBDeltaEPatches.Append("<td valign='middle' class='BottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>Red</td>");
                    else if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString())
                        SBDeltaEPatches.Append("<td valign='middle' class='BottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>Green</td>");
                    else if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString())
                        SBDeltaEPatches.Append("<td valign='middle' class='BottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>Blue</td>");
                    else if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString())
                        SBDeltaEPatches.Append("<td valign='middle' class='BottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>Paper</td>");
                    else if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString())
                        SBDeltaEPatches.Append("<td valign='middle' class='BottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>3-C</td>");

                    SBDeltaEPatches.Append("<td valign='middle' align='center' class='BottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["ΔE"])) + "</td>");
                    SBDeltaEPatches.Append("<td valign='middle' align='center' class='BottomBorder' style='padding-top:6px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["ΔELimit"])) + "</td>");

                    SBDeltaEPatches.Append("</tr>");
                }
                SBStr.Replace("###DeltaEValues###", SBDeltaEPatches.ToString());
            }
            else
            {
                SBStr.Replace("###DeltaEValues###", "<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>");
            }
            SBStr.Replace("###DeltaEChart###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandards_Page2_DeltaEValues.Jpeg"));
            #endregion

            #region AB Comparison
            SBStr.Replace("###ABComparisonChart###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandards_Page2_ABComparison.Jpeg"));
            #endregion

            #region Gray Balance Chart L
            SBStr.Replace("###GrayBalanceChartL###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandards_Page2_ChartL.Jpeg"));
            #endregion

            #region Gray Balance Chart A
            SBStr.Replace("###GrayBalanceChartA###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandards_Page2_ChartA.Jpeg"));
            #endregion

            #region Gray Balance Chart B
            SBStr.Replace("###GrayBalanceChartB###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandards_Page2_ChartB.Jpeg"));
            #endregion

            #region Gray Balance L* a* b*
            currentTable = DS_Common.Tables[9];
            SBDeltaEPatches = new StringBuilder();

            StringBuilder SBstrGrayLABValue = new StringBuilder();
            SBstrPatch = new StringBuilder();
            StringBuilder SBstrLActual = new StringBuilder();
            StringBuilder SBstrAActual = new StringBuilder();
            StringBuilder SBstrBActual = new StringBuilder();


            SBstrPatch.Append("<tr class='row-1'><td class='row-content1'style='padding-left:2px'>Patch</td>");
            SBstrLActual.Append("<tr class='row-2'><td class='row-content1' style='padding-left:2px'>L Actual</td>");
            SBstrAActual.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>A Actual</td>");
            SBstrBActual.Append("<tr class='row-2'><td class='row-content1' style='padding-left:2px'>B Actual</td>");

            if (currentTable != null && currentTable.Rows.Count > 0 && currentTable.Rows[0][0].ToString() != "NO DATA")
            {
                int totalRows = currentTable.Rows.Count;
                int interval = (totalRows < 10) ? 1 : (totalRows / 10);
                int currentPoint = interval;
                int currentRow = 0;

                SBStr.Replace("###DBP###", "2px");
                SBstrGrayLABValue.Append("<table cellpadding='0' cellspacing='0' width='100%' border='0'><tr><td><table width='660' border='0' cellspacing='0' cellpadding='0'><tr><td class='summary-bg' style='background: url(##URLIMAGES##summary-title-bg.gif) repeat-x left top;'><img src='##IMAGES##summary-icon.png' width='12' height='11' style='margin: 2px 0 0 5px;' /><span class='summary-title1'>Gray Patch Values</span></td></tr></table></td></tr><tr><td><table cellpadding='0' cellspacing='0' width='100%' border='0'>###VALUE###</table></td></tr></table>");

                for (int i = 0; i < currentTable.Rows.Count; i++)
                {
                    if (currentPoint == currentRow)
                    {
                        SBstrPatch.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.IntToString(currentTable.Rows[i]["CMYK_C"].ToString()) + "</td>");
                        SBstrLActual.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["CIELAB_L*"])) + "</td>");
                        SBstrAActual.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["CIELAB_a*"])) + "</td>");
                        SBstrBActual.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["CIELAB_b*"])) + "</td>");
                        currentPoint += interval;
                    }
                    currentRow++;
                }
                SBstrPatch.Append("</tr>");
                SBstrLActual.Append("</tr>");
                SBstrAActual.Append("</tr>");
                SBstrBActual.Append("</tr>");

                SBstrGrayLABValue.Replace("###VALUE###", SBstrPatch.ToString() + SBstrLActual.ToString() + SBstrAActual.ToString() + SBstrBActual.ToString());
                SBStr.Replace("###GrayBalanceLABPathValues###", SBstrGrayLABValue.ToString());

            }
            else
            {
                SBStr.Replace("###DBP###", "2px");
                SBStr.Replace("###GrayBalanceLABPathValues###", "<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>");
            }

            #endregion

            #endregion

            #region Page 4

            #region Density Chart
            SBStr.Replace("###DensityChart###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandards_Page2_Density.Jpeg"));
            #endregion

            #region Density Path Values
            currentTable = DS_Common.Tables[10];
            SBstrPatch = new StringBuilder();
            SBstrActual = new StringBuilder();
            SBstrTarget = new StringBuilder();

            SBstrPatch.Append("<tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'><td class='row-content1' align='center'>&nbsp;</td><td class='row-content1' style='padding-left:2px;' align='center'>C</td><td class='row-content1' style='padding-left:2px;' align='center'>M</td><td class='row-content1' style='padding-left:2px;' align='center'>Y</td><td class='row-content1' style='padding-left:2px;' align='center'>K</td></tr>");
            SBstrActual.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Act</td>");
            SBstrTarget.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Targ</td>");

            if (currentTable != null && currentTable.Rows.Count > 0 && currentTable.Rows[0][0].ToString() != "NO DATA")
            {
                SBStr.Replace("###DBP###", "2px");

                foreach (DataRow dr in currentTable.Rows)
                {
                    SBstrActual.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(dr["Density"])) + "</td>");
                    SBstrTarget.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(dr["Target"])) + "</td>");
                }
                SBstrActual.Append("</tr>");
                SBstrTarget.Append("</tr>");

                SBStr.Replace("###DensityPathValues###", SBstrPatch.ToString() + SBstrActual.ToString() + SBstrTarget.ToString());
            }
            else
            {
                SBStr.Replace("###DBP###", "0px");
                SBStr.Replace("###DensityPathValues###", "<br/><br/><br/><br/>");
            }
            #endregion

            #region DotGain Chart
            SBStr.Replace("###DotGainChart###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandards_Page2_DotGainChart.Jpeg"));
            #endregion

            #region DotGain Path Values
            currentTable = DS_Common.Tables[11];
            List<decimal> intArrDistingRec = new List<decimal>();
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

            if (currentTable != null && currentTable.Rows.Count > 0 && currentTable.Rows[0][0].ToString() != "NO DATA")
            {
                SBstrDotGainValue.Append("<table cellpadding='0' cellspacing='0' width='660px' border='0' style='border: SOLID 2PX #000'><tr><td align='center'><table width='660px' border='0' cellspacing='0' cellpadding='0'><tr><td class='summary-bg' style='background: url(##URLIMAGES##summary-title-bg.gif) repeat-x left top;'><img src='##IMAGES##summary-icon.png' width='12' height='11' style='margin: 2px 0 0 5px;' /><span class='summary-title1'>Patch DG Data</span></td></tr></table></td></tr><tr><td><table cellpadding='0' cellspacing='0' width='660px' border='0'>###VALUE###</table></td></tr></table>");
                for (int i = 0; i < currentTable.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(currentTable.Rows[i]["ShowData"]))
                    {
                        if (!intArrDistingRec.Contains(Convert.ToDecimal(currentTable.Rows[i]["CMYK_C"])))
                            intArrDistingRec.Add(Convert.ToDecimal(currentTable.Rows[i]["CMYK_C"]));

                        if (!intArrDistingRec.Contains(Convert.ToDecimal(currentTable.Rows[i]["CMYK_M"])))
                            intArrDistingRec.Add(Convert.ToDecimal(currentTable.Rows[i]["CMYK_M"]));

                        if (!intArrDistingRec.Contains(Convert.ToDecimal(currentTable.Rows[i]["CMYK_Y"])))
                            intArrDistingRec.Add(Convert.ToDecimal(currentTable.Rows[i]["CMYK_Y"]));

                        if (!intArrDistingRec.Contains(Convert.ToDecimal(currentTable.Rows[i]["CMYK_K"])))
                            intArrDistingRec.Add(Convert.ToDecimal(currentTable.Rows[i]["CMYK_K"]));
                    }
                }
                intArrDistingRec.Sort();

                DataView DV = currentTable.DefaultView;
                DV.RowFilter = "CMYK_C = 0 AND CMYK_M = 0 AND CMYK_Y = 0 AND CMYK_K = 0";
                if (DV.Count > 0)
                {
                    SBstrDGPatch.Append("<td align='center'>0</td>");
                    SBstrCDG.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                    SBstrMDG.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                    SBstrYDG.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                    SBstrKDG.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                }

                for (int i = 0; i < intArrDistingRec.Count; i++)
                {
                    if (Convert.ToInt16(intArrDistingRec[i]) != 0)
                    {
                        SBstrDGPatch.Append("<td align='center'>" + Convert.ToInt16(intArrDistingRec[i]).ToString() + "</td>");
                        DV.RowFilter = "CMYK_C = " + intArrDistingRec[i].ToString() + " AND ShowData=1";
                        if (DV.Count > 0)
                            SBstrCDG.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                        else
                            SBstrCDG.Append("<td align='center'>&nbsp;</td>");

                        DV.RowFilter = "CMYK_M = " + intArrDistingRec[i].ToString() + " AND ShowData=1";
                        if (DV.Count > 0)
                            SBstrMDG.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                        else
                            SBstrMDG.Append("<td align='center'>&nbsp;</td>");

                        DV.RowFilter = "CMYK_Y = " + intArrDistingRec[i].ToString() + " AND ShowData=1";
                        if (DV.Count > 0)
                            SBstrYDG.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                        else
                            SBstrYDG.Append("<td align='center'>&nbsp;</td>");

                        DV.RowFilter = "CMYK_K = " + intArrDistingRec[i].ToString() + " AND ShowData=1";
                        if (DV.Count > 0)
                            SBstrKDG.Append("<td align='center'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
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
                SBStr.Replace("###DotGainPathValues###", SBstrDotGainValue.ToString());
            }
            else
            {
                SBStr.Replace("###DotGainPathValues###", "");
            }
            #endregion

            #endregion

            #region Page 5

            #region Run Information
            currentTable = DS_Common.Tables[0];
            string strCompany = "";
            if (currentTable != null && currentTable.Rows.Count > 0)
            {
                strCompany = Convert.ToString(currentTable.Rows[0]["CompanyName"]);
                strCompany += " - " + Convert.ToString(currentTable.Rows[0]["BranchName"]);
                strCompany += " - " + Convert.ToString(currentTable.Rows[0]["BranchLocation"]);

                SBStr.Replace("###RCompanyName###", strCompany);
                SBStr.Replace("###RPaperName###", currentTable.Rows[0]["Substrate"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["Substrate"].ToString());
                SBStr.Replace("###RPressName###", Convert.ToString(currentTable.Rows[0]["PressName"]));
                SBStr.Replace("###RScreening###", currentTable.Rows[0]["PrintCond"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["PrintCond"].ToString());

                //SBStr.Replace("###SCREENING-TYPE###", currentTable.Rows[0]["PrintCond"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["PrintCond"].ToString());                


                SBStr.Replace("###RInk###", currentTable.Rows[0]["Ink"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["Ink"].ToString());
                SBStr.Replace("###RRunTimestamp###", Fuji.Common.CommonFunctions.GetDateByCultures(Convert.ToDateTime(currentTable.Rows[0]["StartDateTime"])));
            }
            else
            {
                SBStr.Replace("###RCompanyName###", "&nbsp;");
                SBStr.Replace("###RPaperName###", "&nbsp;");
                SBStr.Replace("###RPressName###", "&nbsp;");
                SBStr.Replace("###RScreening###", "&nbsp;");
                SBStr.Replace("###RInk###", "&nbsp;");
                SBStr.Replace("###RRunTimestamp###", "&nbsp;");
            }
            #endregion

            #region Primary Densitometric Print Target
            currentTable = DS_Common.Tables[10];
            DataView dvDotGain = DS_Common.Tables[11].DefaultView;
            dvDotGain.RowFilter = "CMYK_C <> 0 AND CMYK_C <> 100 AND ShowData = 1";
            List<decimal> colValues = new List<decimal>();
            foreach (DataRowView dr in dvDotGain)
            {
                colValues.Add(Convert.ToDecimal(dr["CMYK_C"]));
            }
            StringBuilder SBstrTablePrimaryDensPrintTar = new StringBuilder();
            if (currentTable != null && currentTable.Rows.Count > 0)
            {
                SBstrTablePrimaryDensPrintTar.Append("<table style='width: 100%; border-right: 1PX SOLID #C1C1C1;border-bottom: 1PX SOLID #C1C1C1' cellspacing='0' cellpadding=2'>");
                SBstrTablePrimaryDensPrintTar.Append("<tr>");
                SBstrTablePrimaryDensPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:130px;'></td>");
                SBstrTablePrimaryDensPrintTar.Append("<td class='BGRIDTITLE' align='center' colspan='2' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;'>Density</td>");
                SBstrTablePrimaryDensPrintTar.Append("<td class='BGRIDTITLE' align='center' colspan='3' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>TVI</td>");
                SBstrTablePrimaryDensPrintTar.Append("</tr>");

                SBstrTablePrimaryDensPrintTar.Append("<tr>");
                SBstrTablePrimaryDensPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;witdh:130px;'></td>");
                SBstrTablePrimaryDensPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;witdh:15%;'>Tol.</td>");
                SBstrTablePrimaryDensPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;witdh:15%;'>Den.</td>");
                foreach (decimal colVal in colValues)
                {
                    SBstrTablePrimaryDensPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;witdh:20%'>" + Math.Round(colVal) + "%</td>");
                }
                SBstrTablePrimaryDensPrintTar.Append("</tr>");

                for (int i = 0; i < currentTable.Rows.Count; i++)
                {
                    SBstrTablePrimaryDensPrintTar.Append("<tr>");
                    if (Convert.ToString(currentTable.Rows[i]["PatchTypeID"]) == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString()) // "1")
                        SBstrTablePrimaryDensPrintTar.Append("<td style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;BACKGROUND-COLOR:#CCFFFF;COLOR:#000;width:130px;'><b>Cyan</b></td>");
                    else if (Convert.ToString(currentTable.Rows[i]["PatchTypeID"]) == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString()) //"2")
                        SBstrTablePrimaryDensPrintTar.Append("<td style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;BACKGROUND-COLOR:#FF99CC;COLOR:#000;width:130px;'><b>Magenta</b></td>");
                    else if (Convert.ToString(currentTable.Rows[i]["PatchTypeID"]) == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString()) //"3")
                        SBstrTablePrimaryDensPrintTar.Append("<td style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;BACKGROUND-COLOR:#FFFF99;COLOR:#000;width:130px;'><b>Yellow</b></td>");
                    else if (Convert.ToString(currentTable.Rows[i]["PatchTypeID"]) == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString()) //"4")
                        SBstrTablePrimaryDensPrintTar.Append("<td style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;BACKGROUND-COLOR:#C0C0C0;COLOR:#000;width:130px;'><b>Black</b></td>");

                    SBstrTablePrimaryDensPrintTar.Append("<td align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>+-" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["Tol"])) + "</td>");
                    SBstrTablePrimaryDensPrintTar.Append("<td align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["Density"])) + "</td>");

                    foreach (decimal colVal in colValues)
                    {
                        if (Convert.ToString(currentTable.Rows[i]["PatchTypeID"]) == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString()) //"1")
                        {
                            dvDotGain.RowFilter = "CMYK_C=" + colVal.ToString();
                        }
                        else if (Convert.ToString(currentTable.Rows[i]["PatchTypeID"]) == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString()) //"2")
                        {
                            dvDotGain.RowFilter = "CMYK_M=" + colVal.ToString();
                        }
                        else if (Convert.ToString(currentTable.Rows[i]["PatchTypeID"]) == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString()) //"3")
                        {
                            dvDotGain.RowFilter = "CMYK_Y=" + colVal.ToString();
                        }
                        else if (Convert.ToString(currentTable.Rows[i]["PatchTypeID"]) == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString()) //"4")
                        {
                            dvDotGain.RowFilter = "CMYK_K=" + colVal.ToString();
                        }
                        if (dvDotGain.Count > 0)
                        {
                            SBstrTablePrimaryDensPrintTar.Append("<td align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(dvDotGain[0]["DotGain"])) + "</td>");
                        }
                        else
                        {
                            SBstrTablePrimaryDensPrintTar.Append("<td align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>&nbsp;</td>");
                        }
                    }
                    SBstrTablePrimaryDensPrintTar.Append("</tr>");
                }
                SBstrTablePrimaryDensPrintTar.Append("</table>");
            }
            SBStr.Replace("###PrimaryDensPrintTar###", SBstrTablePrimaryDensPrintTar.ToString());

            #endregion

            #region Primary Lab Print Targets
            currentTable = DS_Common.Tables[12];
            StringBuilder SBstrTablePrimaryLabPrintTar = new StringBuilder();
            if (currentTable != null && currentTable.Rows.Count > 0)
            {
                SBstrTablePrimaryLabPrintTar.Append("<table style='width: 100%; border-right: 1PX SOLID #C1C1C1;border-bottom: 1PX SOLID #C1C1C1;' cellspacing='0' cellpadding=2'>");
                SBstrTablePrimaryLabPrintTar.Append("<tr>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;width:130px;'></td>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;width:18%;'>&Delta;E Tol.</td>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:11%;'>L*</td>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:11%;border-right: 1PX SOLID #C1C1C1;'>a*</td>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:11%;'>b*</td>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:11%;border-right: 1PX SOLID #C1C1C1;'>C*</td>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:11%;'>h*</td>");
                SBstrTablePrimaryLabPrintTar.Append("</tr>");
                for (int i = 0; i < currentTable.Rows.Count; i++)
                {
                    SBstrTablePrimaryLabPrintTar.Append("<tr>");
                    SBstrTablePrimaryLabPrintTar.Append("<td style='border-left: 1PX SOLID #C1C1C1;width:130px;background-color:" + Convert.ToString(currentTable.Rows[i]["HTMLColor"]) + "'><b>" + Convert.ToString(currentTable.Rows[i]["PatchType"]) + "</b></td>");
                    SBstrTablePrimaryLabPrintTar.Append("<td align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["ΔELimit"])) + "</td>");
                    SBstrTablePrimaryLabPrintTar.Append("<td align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["CIELAB_L*"])) + "</td>");
                    SBstrTablePrimaryLabPrintTar.Append("<td align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["CIELAB_a*"])) + "</td>");
                    SBstrTablePrimaryLabPrintTar.Append("<td align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["CIELAB_b*"])) + "</td>");
                    SBstrTablePrimaryLabPrintTar.Append("<td align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["C*"])) + "</td>");
                    SBstrTablePrimaryLabPrintTar.Append("<td align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["H'"])) + "</td>");
                    SBstrTablePrimaryLabPrintTar.Append("</tr>");
                }
            }

            currentTable = DS_Common.Tables[13];
            if (currentTable != null && currentTable.Rows.Count > 0)
            {
                SBstrTablePrimaryLabPrintTar.Append("<tr>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;width:130px;'></td>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:18%;'>&Delta;Ch Tol.</td>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:11%;'>L*</td>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;width:11%;'>a*</td>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:11%;'>b*</td>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;width:11%;'>C*</td>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:11%;'>h*</td>");
                SBstrTablePrimaryLabPrintTar.Append("</tr>");
                for (int i = 0; i < currentTable.Rows.Count; i++)
                {
                    SBstrTablePrimaryLabPrintTar.Append("<tr>");
                    SBstrTablePrimaryLabPrintTar.Append("<td style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:130px;'><b>" + Convert.ToString(currentTable.Rows[i]["PatchType"]) + "</b></td>");
                    SBstrTablePrimaryLabPrintTar.Append("<td align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["ΔF*Tol"])) + "</td>");
                    SBstrTablePrimaryLabPrintTar.Append("<td align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["CIELAB_L*"])) + "</td>");
                    SBstrTablePrimaryLabPrintTar.Append("<td align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["CIELAB_a*"])) + "</td>");
                    SBstrTablePrimaryLabPrintTar.Append("<td align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["CIELAB_b*"])) + "</td>");
                    SBstrTablePrimaryLabPrintTar.Append("<td align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["C*"])) + "</td>");
                    SBstrTablePrimaryLabPrintTar.Append("<td align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["H'"])) + "</td>");
                    SBstrTablePrimaryLabPrintTar.Append("</tr>");
                }
                SBstrTablePrimaryLabPrintTar.Append("</table>");
            }
            SBStr.Replace("###PrimaryLabPrintTar###", SBstrTablePrimaryLabPrintTar.ToString());
            #endregion

            #region G7 NPD Print Targets
            StringBuilder SBstrTablG7NPDLabPrintTarHead = new StringBuilder();
            StringBuilder SBstrTablG7NPDLabPrintTarBlack = new StringBuilder();
            StringBuilder SBstrTablG7NPDLabPrintTarGray = new StringBuilder();
            currentTable = DS_Common.Tables[4];
            SBstrTablG7NPDLabPrintTarHead.Append("<table style='width: 100%; border-right: 1PX SOLID #C1C1C1;border-bottom: 1PX SOLID #C1C1C1;' cellspacing='0' cellpadding=2'>");

            SBstrTablG7NPDLabPrintTarHead.Append("<tr>");
            SBstrTablG7NPDLabPrintTarHead.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:20%;'></td>");

            SBstrTablG7NPDLabPrintTarBlack.Append("<tr>");
            SBstrTablG7NPDLabPrintTarBlack.Append("<td align='left' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'><b>Black</b></td>");

            SBstrTablG7NPDLabPrintTarGray.Append("<tr>");
            SBstrTablG7NPDLabPrintTarGray.Append("<td align='left' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'><b>Gray</b></td>");
            int totalCol = 0;
            if (currentTable != null && currentTable.Rows.Count > 0)
            {
                foreach (DataRow dr in currentTable.Rows)
                {
                    totalCol++;
                    if (Convert.ToBoolean(dr["ShowData"]))
                    {
                        SBstrTablG7NPDLabPrintTarHead.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.IntToString(Convert.ToString(dr["CMYK_K"])) + "%" + "</td>");
                        SBstrTablG7NPDLabPrintTarBlack.Append("<td align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(dr["G7NPDC"])) + "</td>");
                    }
                }
            }

            currentTable = DS_Common.Tables[5];
            if (currentTable != null && currentTable.Rows.Count > 0)
            {
                foreach (DataRow dr in currentTable.Rows)
                {
                    if (Convert.ToBoolean(dr["ShowData"]))
                    {
                        SBstrTablG7NPDLabPrintTarGray.Append("<td align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(dr["G7NPDC"])) + "</td>");
                    }
                }
            }
            else
            {
                SBstrTablG7NPDLabPrintTarGray.Append("<td align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;' colspan='" + totalCol.ToString() + "'>&nbsp;</td>");
            }
            SBstrTablG7NPDLabPrintTarHead.Append("</tr>");
            SBstrTablG7NPDLabPrintTarBlack.Append("</tr>");
            SBstrTablG7NPDLabPrintTarGray.Append("</tr>");

            SBStr.Replace("###G7NPDPrintTar###", SBstrTablG7NPDLabPrintTarHead.ToString() + SBstrTablG7NPDLabPrintTarBlack.ToString() + SBstrTablG7NPDLabPrintTarGray.ToString() + "</table>");

            #endregion

            #endregion

            #region Page 6

            #region Comsumables

            currentTable = DS_Common.Tables[15];
            if (currentTable.Rows.Count > 0)
            {
                //SBStr.Replace("",currentTable.Rows[0][""].ToString());
                SBStr.Replace("###Substrate###", (currentTable.Rows[0]["Substrate"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["Substrate"].ToString()));

                SBStr.Replace("##INK##", (currentTable.Rows[0]["Ink"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["Ink"].ToString())); ;
                SBStr.Replace("##ISO-COMPLAINT##", (currentTable.Rows[0]["ISO2846Compliant"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["ISO2846Compliant"].ToString()));
                SBStr.Replace("##InkOrder##", (currentTable.Rows[0]["InkOrder"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["InkOrder"].ToString()));

                SBStr.Replace("##CONSUMERABLES-BLANKET##", (currentTable.Rows[0]["Blanket"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["Blanket"].ToString())); ;
                SBStr.Replace("##CONSUMERABLES-BLANKETS-AGE##", (currentTable.Rows[0]["BlanketAge"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["BlanketAge"].ToString())); ;

                SBStr.Replace("##CONSUMERABLES-FS##", (currentTable.Rows[0]["FountainSolution"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["FountainSolution"].ToString())); ;
                StringBuilder strPrintData = new StringBuilder();
                //based on the output type prepare table 
                if (currentTable.Rows[0]["OutputTypeID"].ToString() == "1")
                {

                    strPrintData.Append("<table cellspacing=\"0\" cellpadding=\"3\" border=\"0\" width=\"98%\" style=\"border-bottom: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;\">");
                    strPrintData.Append("<tr>");
                    strPrintData.Append("<td width=\"171\" style=\"border-left: 1PX SOLID #C1C1C1;\">ISO 2846 Ink</td>");
                    strPrintData.Append("<td width='354' style='border-left: 1PX SOLID #C1C1C1;'>##ISO2846Ink##</td>");
                    strPrintData.Append("<td width='175' style='border-left: 1PX SOLID #C1C1C1;'>Ink Order</td>");
                    strPrintData.Append("<td width='504' style='border-left: 1PX SOLID #C1C1C1;'>##InkOrder##</td>");
                    strPrintData.Append("</tr>");
                    strPrintData.Append("<tr>");
                    strPrintData.Append("<td style=\"border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\">Fountain Solution</td>");
                    strPrintData.Append("<td style=\"border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\">##FSolution##</td>");
                    strPrintData.Append("<td style=\"border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\">Blanket</td>");
                    strPrintData.Append("<td style=\"border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\">##Blanket##</td>");
                    strPrintData.Append("</tr>");
                    strPrintData.Append("<tr>");
                    strPrintData.Append("<td style=\"border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\">Blanket Age</td>");
                    strPrintData.Append("<td style=\"border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\">##BlanketAge## </td>");
                    strPrintData.Append("<td style=\"border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\">Screening</td>");
                    strPrintData.Append("<td style=\"border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\">##Screening##</td>");
                    strPrintData.Append("</tr>");
                    strPrintData.Append("</table>");

                    strPrintData.Replace("##ISO2846Ink##", (currentTable.Rows[0]["ISO2846Compliant"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["ISO2846Compliant"].ToString()));
                    strPrintData.Replace("##InkOrder##", (currentTable.Rows[0]["InkOrder"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["InkOrder"].ToString()));
                    strPrintData.Replace("##FSolution##", (currentTable.Rows[0]["FountainSolution"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["FountainSolution"].ToString()));
                    strPrintData.Replace("##Blanket##", (currentTable.Rows[0]["Blanket"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["Blanket"].ToString()));
                    strPrintData.Replace("##BlanketAge##", (currentTable.Rows[0]["BlanketAge"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["BlanketAge"].ToString()));
                    strPrintData.Replace("##Screening##", (currentTable.Rows[0]["Screening"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["Screening"].ToString()));
                }
                else if (currentTable.Rows[0]["OutputTypeID"].ToString() == "3")
                {
                    strPrintData.Append("<table cellspacing=\"0\" cellpadding=\"3\" border=\"0\" width=\"98%\" style=\"border-bottom: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;\">");
                    strPrintData.Append("<tr>");
                    strPrintData.Append("<td width=\"171\" style=\"border-left: 1PX SOLID #C1C1C1;\">Rip</td>");
                    strPrintData.Append("<td width=\"354\" style=\"border-left: 1PX SOLID #C1C1C1;\">##Rip##</td>");
                    strPrintData.Append("<td width=\"239\" style=\"border-left: 1PX SOLID #C1C1C1;\">Print Mode</td>");
                    strPrintData.Append("<td width=\"504\" style=\"border-left: 1PX SOLID #C1C1C1;\">##PrintMode##</td>");
                    strPrintData.Append("</tr>");
                    strPrintData.Append("<tr>");
                    strPrintData.Append("<td style=\"border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\">Screening Type </td>");
                    strPrintData.Append("<td style=\"border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\">##ScreeningType##</td>");
                    strPrintData.Append("<td style=\"border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\">UV Lamp Settings</td>");
                    strPrintData.Append("<td style=\"border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\">##UVLamp##</td>");
                    strPrintData.Append("</tr>");
                    strPrintData.Append("</table>");

                    strPrintData.Replace("##Rip##", (currentTable.Rows[0]["Rip"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["Rip"].ToString()));
                    strPrintData.Replace("##PrintMode##", (currentTable.Rows[0]["PrintMode"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["PrintMode"].ToString()));
                    strPrintData.Replace("##ScreeningType##", (currentTable.Rows[0]["Screening"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["Screening"].ToString()));
                    strPrintData.Replace("##UVLamp##", (currentTable.Rows[0]["UVLamp"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["UVLamp"].ToString()));

                }
                else if (currentTable.Rows[0]["OutputTypeID"].ToString() == "2")
                {
                    strPrintData.Append("<table cellspacing=\"0\" cellpadding=\"3\" border=\"0\" width=\"98%\" style=\"border-bottom: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;\">");
                    strPrintData.Append("<tr>");
                    strPrintData.Append("<td colspan='2' style=\"border-left: 1PX SOLID #C1C1C1;\">&nbsp;</td>");
                    strPrintData.Append("<td style=\"border-left: 1PX SOLID #C1C1C1;\">&nbsp;</td>");
                    strPrintData.Append("<td style=\"border-left: 1PX SOLID #C1C1C1;\">&nbsp;</td>");
                    strPrintData.Append("<td width=\"684\" style=\"border-left: 1PX SOLID #C1C1C1;\"><b>Frequency and Angles</b></td>");
                    strPrintData.Append("</tr>");
                    strPrintData.Append("<tr>");
                    strPrintData.Append("<tr>");
                    strPrintData.Append("<td width=\"103\" style=\"width: 100px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\"><strong>Stencil</strong></td>");
                    strPrintData.Append("<td width=\"148\" style=\"width: 110px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\">##Stencil##</td>");
                    strPrintData.Append("<td width=\"54\" style=\"border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\"><strong>Rip</strong></td>");
                    strPrintData.Append("<td width=\"267\" style=\"border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\">##Rip## <td rowspan=\"2\"><table width=\"98%\">");
                    strPrintData.Append("<tr>");
                    strPrintData.Append("<td>Cyan</td>");
                    strPrintData.Append("<td>###Cyan###</td>");
                    strPrintData.Append("<td>Magenta</td>");
                    strPrintData.Append("<td>###Mag###</td>");
                    strPrintData.Append("</tr>");
                    strPrintData.Append("<tr>");
                    strPrintData.Append("<td>Yellow</td>");
                    strPrintData.Append("<td>###Yellow###</td>");
                    strPrintData.Append("<td>Black</td>");
                    strPrintData.Append("<td>###Black###</td>");
                    strPrintData.Append("</tr></table></td>");
                    strPrintData.Append("</tr");
                    strPrintData.Append("<tr>");
                    strPrintData.Append("<td style=\"border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\">&nbsp;</td>");
                    strPrintData.Append("<td style=\"border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\">&nbsp;</td>");
                    strPrintData.Append("<td style=\"border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\"><strong>CTS/Film Device</strong> </td>");
                    strPrintData.Append(" <td style=\"border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;\">##CTS## </td></tr></table>");


                    strPrintData.Replace("##Stencil##", (currentTable.Rows[0]["Stencil"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["Stencil"].ToString()));
                    strPrintData.Replace("##Rip##", (currentTable.Rows[0]["Rip"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["Rip"].ToString()));
                    strPrintData.Replace("##CTS##", (currentTable.Rows[0]["CTS_FilmDevice"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["CTS_FilmDevice"].ToString()));

                    strPrintData.Replace("###Cyan###", (currentTable.Rows[0]["CyanA&F"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["CyanA&F"].ToString()));
                    strPrintData.Replace("###Mag###", (currentTable.Rows[0]["MagentaA&F"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["MagentaA&F"].ToString()));
                    strPrintData.Replace("###Yellow###", (currentTable.Rows[0]["YellowA&F"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["YellowA&F"].ToString()));
                    strPrintData.Replace("###Black###", (currentTable.Rows[0]["BlackA&F"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["BlackA&F"].ToString()));

                }

                SBStr.Replace("###PrintData###", strPrintData.ToString());
                SBStr.Replace("##Notes##", (currentTable.Rows[0]["Notes"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["Notes"].ToString()));
            }
            else  //if thee is no data display empty space
            {
                SBStr.Replace("###PrintData###", "&nbsp;");
                SBStr.Replace("##Notes##", "&nbsp;");
                SBStr.Replace("###Substrate###", "&nbsp;");

                SBStr.Replace("##INK##", "&nbsp;"); ;
                SBStr.Replace("##ISO-COMPLAINT##", "&nbsp;");
                SBStr.Replace("##InkOrder##", "&nbsp;");

                SBStr.Replace("##CONSUMERABLES-BLANKET##", "&nbsp;"); ;
                SBStr.Replace("##CONSUMERABLES-BLANKETS-AGE##", "&nbsp;"); ;

                SBStr.Replace("##CONSUMERABLES-FS##", "&nbsp;"); ;
            }

            #region Fill Data from database
            currentTable = DS_Common.Tables[14];
            if (currentTable.Rows.Count > 0)
            {
                for (int i = 0; i < currentTable.Rows.Count; i++)
                {
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())// "5")
                        SBStr.Replace("##LAB-PAPER-TARGET-L##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())// "6")
                        SBStr.Replace("##LAB-PAPER-TARGET-A##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())//"7")
                        SBStr.Replace("##LAB-PAPER-TARGET-B##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));

                    SBStr.Replace("##LAB-PAPER-ETHRESH##", "5.00");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())// "5")
                        SBStr.Replace("##LAB-CYAN-TARGET-L##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())// "6")
                        SBStr.Replace("##LAB-CYAN-TARGET-A##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())//"7")
                        SBStr.Replace("##LAB-CYAN-TARGET-B##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));

                    SBStr.Replace("##LAB-CYAN-ETHRESH##", "5.00");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())// "5")
                        SBStr.Replace("##LAB-MAGENTA-TARGET-L##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())// "6")
                        SBStr.Replace("##LAB-MAGENTA-TARGET-A##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())//"7")
                        SBStr.Replace("##LAB-MAGENTA-TARGET-B##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));

                    SBStr.Replace("##LAB-MAGENTA-ETHRESH##", "5.00");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())// "5")
                        SBStr.Replace("##LAB-YELLOW-TARGET-L##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())// "6")
                        SBStr.Replace("##LAB-YELLOW-TARGET-A##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())//"7")
                        SBStr.Replace("##LAB-YELLOW-TARGET-B##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));

                    SBStr.Replace("##LAB-YELLOW-ETHRESH##", "5.00");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())// "5")
                        SBStr.Replace("##LAB-BLACK-TARGET-L##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())// "6")
                        SBStr.Replace("##LAB-BLACK-TARGET-A##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())//"7")
                        SBStr.Replace("##LAB-BLACK-TARGET-B##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));

                    SBStr.Replace("##LAB-BLACK-ETHRESH##", "5.00");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())// "5")
                        SBStr.Replace("##LAB-RED-TARGET-L##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())// "6")
                        SBStr.Replace("##LAB-RED-TARGET-A##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())//"7")
                        SBStr.Replace("##LAB-RED-TARGET-B##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));

                    SBStr.Replace("##LAB-RED-ETHRESH##", "5.00");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())// "5")
                        SBStr.Replace("##LAB-GREEN-TARGET-L##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())// "6")
                        SBStr.Replace("##LAB-GREEN-TARGET-A##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())//"7")
                        SBStr.Replace("##LAB-GREEN-TARGET-B##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));

                    SBStr.Replace("##LAB-GREEN-ETHRESH##", "5.00");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())// "5")
                        SBStr.Replace("##LAB-BLUE-TARGET-L##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())// "6")
                        SBStr.Replace("##LAB-BLUE-TARGET-A##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())//"7")
                        SBStr.Replace("##LAB-BLUE-TARGET-B##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));

                    SBStr.Replace("##LAB-BLUE-ETHRESH##", "5.00");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())// "5")
                        SBStr.Replace("##LAB-3C-TARGET-L##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())// "6")
                        SBStr.Replace("##LAB-3C-TARGET-A##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())//"7")
                        SBStr.Replace("##LAB-3C-TARGET-B##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));

                    SBStr.Replace("##LAB-3C-ETHRESH##", "5.00");

                    SBStr.Replace("##GRAYBALANCE-THRESH##", "1.50");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())//"1")
                        SBStr.Replace("##DENTAR-CYAN-TARGET##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    SBStr.Replace("##DENTAR-CYAN-THRESH##", "0.10");


                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())//"1")
                        SBStr.Replace("##DENTAR-MAGENTA-TARGET##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    SBStr.Replace("##DENTAR-MAGENTA-THRESH##", "0.10");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())//"1")
                        SBStr.Replace("##DENTAR-YELLOW-TARGET##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    SBStr.Replace("##DENTAR-YELLOW-THRESH##", "0.10");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())//"1")
                        SBStr.Replace("##DENTAR-BLACK-TARGET##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    SBStr.Replace("##DENTAR-BLACK-THRESH##", "0.10");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Quarter_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())//"9")
                        SBStr.Replace("##DOTGAIN-QT-TARGET-C##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Quarter_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())//"9")
                        SBStr.Replace("##DOTGAIN-QT-TARGET-M##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Quarter_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())// "9")
                        SBStr.Replace("##DOTGAIN-QT-TARGET-Y##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Quarter_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())//"9")
                        SBStr.Replace("##DOTGAIN-QT-TARGET-K##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    SBStr.Replace("##DOTGAIN-QT-TARGET-THRESH##", "5.00");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Half_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())//"9")
                        SBStr.Replace("##DOTGAIN-HT-TARGET-C##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Half_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())//"9")
                        SBStr.Replace("##DOTGAIN-HT-TARGET-M##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Half_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())//"9")
                        SBStr.Replace("##DOTGAIN-HT-TARGET-Y##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Half_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())//"9")
                        SBStr.Replace("##DOTGAIN-HT-TARGET-K##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    SBStr.Replace("##DOTGAIN-HT-TARGET-THRESH##", "5.00");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Three_Quarter_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())//"9")
                        SBStr.Replace("##DOTGAIN-3Q-TARGET-C##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Three_Quarter_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())//"9")
                        SBStr.Replace("##DOTGAIN-3Q-TARGET-M##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Three_Quarter_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())//"9")
                        SBStr.Replace("##DOTGAIN-3Q-TARGET-Y##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Three_Quarter_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())//"9")
                        SBStr.Replace("##DOTGAIN-3Q-TARGET-K##", Convert.ToDecimal(currentTable.Rows[i]["Target"]).ToString("#.00"));
                    SBStr.Replace("##DOTGAIN-3Q-TARGET-THRESH##", "5.00");

                }
            }
            #endregion

            #endregion

            #endregion

            #region Assign Server

            SBStr.Replace("##URLIMAGES##", WebSettings.ImageURL + "Images/");
            SBStr.Replace("##IMAGES##", strServer);

            #endregion

            return SBStr;
        }
        #endregion

        #region Report 15 :  Print Standard Report ISO
        private StringBuilder TextReturn_CPSPrintStandardsReportISO(StringBuilder SBStr)
        {
            string strStatusImageGreen = "<img src=\"##IMAGES##Proofok.gif\" width='13px' height='13px' >";
            string strStatusImageRed = "<img src=\"##IMAGES##ProofFailed.gif\" width='13px' height='13px' >";
            string strNoRecordFound = "No Records Found";
            StringBuilder SBCommon = new StringBuilder();

            SBStr.Replace("#csspath#", CSSPath);

            DataTable currentTable = null;

            #region Page 1
            Enums.OutputType objOutputType = new Enums.OutputType();
            #region Report Front Information

            StringBuilder SBstrProjectType = new StringBuilder();
            SBStr.Replace("###CURRENTDATE###", DateTime.Today.ToLongDateString());
            currentTable = DS_Common.Tables[0];
            if (currentTable != null && currentTable.Rows.Count > 0)
            {
                SBStr.Replace("###ProjectName###", Convert.ToString(currentTable.Rows[0]["ProjectName"]));
                SBStr.Replace("###PressName###", Convert.ToString(currentTable.Rows[0]["PressName"]));
                SBStr.Replace("###BranchName###", Convert.ToString(currentTable.Rows[0]["BranchName"]));
                SBStr.Replace("###BranchLocation###", Convert.ToString(currentTable.Rows[0]["BranchLocation"]));
                SBStr.Replace("##RunNo##", Convert.ToString(currentTable.Rows[0]["RunCount"]));
                objOutputType = (Enums.OutputType)Enum.Parse(typeof(Enums.OutputType), Convert.ToString(currentTable.Rows[0]["OutputTypeID"]));

                switch (objOutputType)
                {

                    case Enums.OutputType.Offset:
                        #region Off Set
                        StringBuilder TrOffsett = new StringBuilder();
                        TrOffsett.Append("<tr><td valign='top' style='padding-top:3px;'>");
                        TrOffsett.Append("<table border='0' width='100%' cellpadding='0' cellspacing='0'><tr><td align='center'>");
                        TrOffsett.Append("<fieldset><legend class='subtitle'>Offsett Printing Data</legend>");
                        TrOffsett.Append("<table border='0' width='99%' cellpadding='0' cellspacing='4'><tr>");
                        TrOffsett.Append("<td style='font-size:9PX;padding-left: 2px; width: 25%;'><b>ISO 2846 Ink</b></td>");
                        TrOffsett.Append("<td style='font-size:9PX;width: 25%;'><b>Ink Order</b></td>");
                        TrOffsett.Append("<td style='font-size:9PX;width: 50%;'><b>Fountain Solution</b></td></tr>");
                        TrOffsett.Append("<tr><td style='font-size:9PX;padding-left: 2px;border: 1px solid #7f9db9;'>###OPDISO2846Ink</td><td style='font-size:9PX;border: 1px solid #7f9db9;'>###OPDInkOrder###</td><td valign='top' style='font-size:9PX;border: 1px solid #7f9db9;'>###OPDFountainSolution###</td></tr>");
                        TrOffsett.Append("<tr><td colspan='2'></td><td style='font-size:7PX;'>*Include Manufacturer, Name</td></tr>");
                        TrOffsett.Append("<tr><td style='font-size:9PX;padding-left: 2px;' colspan='2'><b>Blanket</b></td><td style='font-size:9PX;'><b>Screening</b></td></tr>");
                        TrOffsett.Append("<tr><td style='font-size:9PX;padding-left: 2px;border: 1px solid #7f9db9;' colspan='2'>###OPDBlanket###</td><td valign='top' style='font-size:9PX;border: 1px solid #7f9db9;'>###OPDScreening###</td></tr>");
                        TrOffsett.Append("<tr><td style='font-size:7PX;padding-left: 2px;' colspan='2'>*Include Manufacturer, Name</td><td style='font-size:7PX;'>*Include Type(AM-FM), Dot Shape, Frequency</td></tr>");
                        TrOffsett.Append("<tr><td style='font-size:9PX;padding-left: 2px;' colspan='3'><b>Blanket Age</b></td></tr>");
                        TrOffsett.Append("<tr><td style='font-size:9PX;padding-left: 2px;border: 1px solid #7f9db9;' colspan='3'>###OPDBlanketAge###</td></tr></table></fieldset></td></tr></table></td></tr>");
                        #endregion
                        SBstrProjectType = TrOffsett;

                        break;
                    case Enums.OutputType.Screen:
                        #region Screen
                        StringBuilder TrScreen = new StringBuilder();
                        TrScreen.Append("<tr><td valign='top' style='padding-top:3px;'><table border='0' width='100%' cellpadding='0' cellspacing='0'>");
                        TrScreen.Append("<tr><td align='center'><fieldset><legend class='subtitle'>Screen Printing Data</legend><table border='0' width='99%' cellpadding='0' cellspacing='4'>");
                        TrScreen.Append("<tr><td style='font-size:9PX;padding-left: 2px; width: 50%;'><b>Stencil</b></td><td style='width: 50%;font-size:9PX;'><b>Angle and Frequency</b></td></tr>");
                        TrScreen.Append("<tr><td style='font-size:9PX;padding-left: 2px;border: 1px solid #7f9db9;'>###SPDStencil###</td><td rowspan='8' valign='top'><table border='0' width='99%' cellpadding='0' cellspacing='2'>");
                        TrScreen.Append("<tr><td style='font-size:9PX;'>Cyan</td><td style='font-size:9PX;border: 1px solid #7f9db9;'>###SPDCyan###</td><td style='font-size:9PX;'>Magenta</td><td style='font-size:9PX;border: 1px solid #7f9db9;'>###SPDMagenta###</td></tr>");
                        TrScreen.Append("<tr><td style='font-size:9PX;'>Yellow</td><td style='font-size:9PX;border: 1px solid #7f9db9;'>###SPDYellow###</td><td style='font-size:9PX;'>Black</td><td style='font-size:9PX;border: 1px solid #7f9db9;'>###SPDBlack###</td></tr>");
                        TrScreen.Append("<tr><td colspan='4' style='font-size:7PX;'>*Include angle and frequency</td></tr></table></td></tr>");
                        TrScreen.Append("<tr><td style='font-size:7PX;padding-left: 2px;'>*Include Mesh Count, Coating</td></tr>");
                        TrScreen.Append("<tr><td style='font-size:9PX;padding-left: 2px;'><b>Rip</b></td></tr>");
                        TrScreen.Append("<tr><td style='font-size:9PX;padding-left: 2px;border: 1px solid #7f9db9;'>###SPDRip###</td></tr>");
                        TrScreen.Append("<tr><td style='font-size:7PX;padding-left: 2px;'>*Include Rip Name, Version</td></tr>");
                        TrScreen.Append("<tr><td style='font-size:9PX;padding-left: 2px;'><b>CTS/Film Device</b></td></tr>");
                        TrScreen.Append("<tr><td style='font-size:9PX;padding-left: 2px;border: 1px solid #7f9db9;'>###SPDCTSFilmDevice###</td></tr>");
                        TrScreen.Append("<tr><td style='font-size:7PX;padding-left: 2px'>*Include Manufacturer, Model</td></tr></table></fieldset></td></tr></table></td></tr>");
                        #endregion
                        SBstrProjectType = TrScreen;

                        break;
                    case Enums.OutputType.Wideformat:
                        #region Wide Format
                        StringBuilder TrWideFormat = new StringBuilder();
                        TrWideFormat.Append("<tr><td valign='top' style='padding-top:3px;'>");
                        TrWideFormat.Append("<table border='0' width='100%' cellpadding='0' cellspacing='0'><tr><td align='center'>");
                        TrWideFormat.Append("<fieldset><legend class='subtitle'>Wide Format Printing Data</legend>");
                        TrWideFormat.Append("<table border='0' width='99%' cellpadding='0' cellspacing='4'>");
                        TrWideFormat.Append("<tr><td style='font-size:9PX;padding-left: 2px; width: 50%;'><b>Rip</b></td><td style='font-size:9PX;width: 50%;'><b>Print Mode</b></td></tr>");
                        TrWideFormat.Append("<tr><td style='font-size:9PX;padding-left: 2px;border: 1px solid #7f9db9;'>###WFRip###</td><td valign='top' style='font-size:9PX;border: 1px solid #7f9db9;'>###WFPrintMode###</td></tr>");
                        TrWideFormat.Append("<tr><td style='font-size:7PX;padding-left: 2px;'>*Include Rip Name, Version</td><td></td></tr>");
                        TrWideFormat.Append("<tr><td style='font-size:9PX;padding-left: 2px;'><b>Screening Type</b></td><td style='font-size:9PX;'><b>UV Lamp Settings</b></td></tr>");
                        TrWideFormat.Append("<tr><td style='font-size:9PX;padding-left: 2px;border: 1px solid #7f9db9;'>###WFScreeningType###</td><td valign='top' style ='font-size:9PX;border: 1px solid #7f9db9;'>###WFUVLampSettings###</td></tr>");
                        TrWideFormat.Append("</table></fieldset></td></tr></table></td></tr>");
                        #endregion
                        SBstrProjectType = TrWideFormat;

                        break;
                }
            }
            else
            {
                SBStr.Replace("###ProjectName###", "&nbsp;");
                SBStr.Replace("###PressName###", "&nbsp;");
                SBStr.Replace("###BranchName###", "&nbsp;");
                SBStr.Replace("###BranchLocation###", "&nbsp;");
            }
            #endregion

            #endregion

            #region Page 2

            #region Print Condition
            currentTable = DS_Common.Tables[1];
            if (currentTable != null && currentTable.Rows.Count > 0 && currentTable.Rows[0][0].ToString() != "NO DATA")
            {
                #region General Data
                currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 1";
                if (currentTable.DefaultView.Count > 0)
                    SBStr.Replace("###GDSubstrateDesc###", currentTable.DefaultView[0]["PrintCondition"].ToString());

                currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 2";
                if (currentTable.DefaultView.Count > 0)
                    SBStr.Replace("###GDInk###", currentTable.DefaultView[0]["PrintCondition"].ToString());
                #endregion

                #region Offsett Data
                switch (objOutputType)
                {

                    case Enums.OutputType.Offset:
                        #region Off Set
                        currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 3";
                        if (currentTable.DefaultView.Count > 0)
                            SBstrProjectType.Replace("###OPDISO2846Ink###", currentTable.DefaultView[0]["PrintCondition"].ToString());

                        currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 4";
                        if (currentTable.DefaultView.Count > 0)
                            SBstrProjectType.Replace("###OPDInkOrder###", currentTable.DefaultView[0]["PrintCondition"].ToString());

                        currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 5";
                        if (currentTable.DefaultView.Count > 0)
                            SBstrProjectType.Replace("###OPDFountainSolution###", currentTable.DefaultView[0]["PrintCondition"].ToString());

                        currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 6";
                        if (currentTable.DefaultView.Count > 0)
                            SBstrProjectType.Replace("###OPDBlanket###", currentTable.DefaultView[0]["PrintCondition"].ToString());

                        currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 7";
                        if (currentTable.DefaultView.Count > 0)
                            SBstrProjectType.Replace("###OPDScreening###", currentTable.DefaultView[0]["PrintCondition"].ToString());

                        currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 8";
                        if (currentTable.DefaultView.Count > 0)
                            SBstrProjectType.Replace("###OPDBlanketAge###", currentTable.DefaultView[0]["PrintCondition"].ToString());
                        #endregion
                        break;
                    case Enums.OutputType.Screen:
                        #region Screen
                        currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 9";
                        if (currentTable.DefaultView.Count > 0)
                            SBstrProjectType.Replace("###SPDStencil###", currentTable.DefaultView[0]["PrintCondition"].ToString());

                        currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 12";
                        if (currentTable.DefaultView.Count > 0)
                            SBstrProjectType.Replace("###SPDCyan###", currentTable.DefaultView[0]["PrintCondition"].ToString());

                        currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 13";
                        if (currentTable.DefaultView.Count > 0)
                            SBstrProjectType.Replace("###SPDMagenta###", currentTable.DefaultView[0]["PrintCondition"].ToString());

                        currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 14";
                        if (currentTable.DefaultView.Count > 0)
                            SBstrProjectType.Replace("###SPDYellow###", currentTable.DefaultView[0]["PrintCondition"].ToString());

                        currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 15";
                        if (currentTable.DefaultView.Count > 0)
                            SBstrProjectType.Replace("###SPDBlack###", currentTable.DefaultView[0]["PrintCondition"].ToString());

                        currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 10";
                        if (currentTable.DefaultView.Count > 0)
                            SBstrProjectType.Replace("###SPDRip###", currentTable.DefaultView[0]["PrintCondition"].ToString());

                        currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 11";
                        if (currentTable.DefaultView.Count > 0)
                            SBstrProjectType.Replace("###SPDCTSFilmDevice###", currentTable.DefaultView[0]["PrintCondition"].ToString());

                        #endregion
                        break;

                    case Enums.OutputType.Wideformat:
                        #region Wide Format
                        currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 10";
                        if (currentTable.DefaultView.Count > 0)
                            SBstrProjectType.Replace("###WFRip###", currentTable.DefaultView[0]["PrintCondition"].ToString());

                        currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 17";
                        if (currentTable.DefaultView.Count > 0)
                            SBstrProjectType.Replace("###WFPrintMode###", currentTable.DefaultView[0]["PrintCondition"].ToString());

                        currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 7";
                        if (currentTable.DefaultView.Count > 0)
                            SBstrProjectType.Replace("###WFScreeningType###", currentTable.DefaultView[0]["PrintCondition"].ToString());

                        currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 16";
                        if (currentTable.DefaultView.Count > 0)
                            SBstrProjectType.Replace("###WFUVLampSettings###", currentTable.DefaultView[0]["PrintCondition"].ToString());

                        #endregion
                        break;

                }
                #endregion
                #region Note
                currentTable.DefaultView.RowFilter = "PrintConditionTypeID = 18";
                if (currentTable.DefaultView.Count > 0)
                    SBStr.Replace("###Notes###", currentTable.DefaultView[0]["PrintCondition"].ToString());
                #endregion

            }
            SBStr.Replace("###GDSubstrateDesc###", "&nbsp;");
            SBStr.Replace("###GDInk###", "&nbsp;");
            SBStr.Replace("###Notes###", "&nbsp;");

            #region Off Set
            SBstrProjectType.Replace("###OPDISO2846Ink", "&nbsp;");
            SBstrProjectType.Replace("###OPDInkOrder###", "&nbsp;");
            SBstrProjectType.Replace("###OPDFountainSolution###", "&nbsp;");
            SBstrProjectType.Replace("###OPDBlanket###", "&nbsp;");
            SBstrProjectType.Replace("###OPDScreening###", "&nbsp;");
            SBstrProjectType.Replace("###OPDBlanketAge###", "&nbsp;");
            #endregion
            #region Screen
            SBstrProjectType.Replace("###SPDStencil###", "&nbsp;");
            SBstrProjectType.Replace("###SPDCyan###", "&nbsp;");
            SBstrProjectType.Replace("###SPDMagenta###", "&nbsp;");
            SBstrProjectType.Replace("###SPDYellow###", "&nbsp;");
            SBstrProjectType.Replace("###SPDBlack###", "&nbsp;");
            SBstrProjectType.Replace("###SPDRip###", "&nbsp;");
            SBstrProjectType.Replace("###SPDCTSFilmDevice###", "&nbsp;");
            #endregion
            #region Wide Format
            SBstrProjectType.Replace("###WFRip###", "&nbsp;");
            SBstrProjectType.Replace("###WFPrintMode###", "&nbsp;");
            SBstrProjectType.Replace("###WFScreeningType###", "&nbsp;");
            SBstrProjectType.Replace("###WFUVLampSettings###", "&nbsp;");
            #endregion

            SBStr.Replace("###ProjectType###", SBstrProjectType.ToString());

            #endregion


            #region AB Comparison
            SBStr.Replace("###ABComparisonChart###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandardsISO_Page2_ABComparison.Jpeg"));
            #endregion

            #region L Comparison
            SBStr.Replace("###LComparisonChart###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandardsISO_Page2_LComparison.Jpeg"));
            #endregion

            #region Delta E
            currentTable = DS_Common.Tables[4];
            StringBuilder SBDeltaEPatches = new StringBuilder();
            if (currentTable != null && currentTable.Rows.Count > 0 && currentTable.Rows[0][0].ToString() != "NO DATA")
            {
                for (int i = 0; i < currentTable.Rows.Count; i++)
                {
                    SBDeltaEPatches.Append("<tr style='height: 16px'>");
                    if (Convert.ToDecimal(currentTable.Rows[i]["ΔE"]) < Convert.ToDecimal(currentTable.Rows[i]["ΔELimit"]))
                    {
                        SBDeltaEPatches.Append("<td valign='middle' align='center' style='padding-right: 5px' class='BottomBorder'>" + strStatusImageGreen + "</td>");
                    }
                    else
                    {
                        SBDeltaEPatches.Append("<td valign='middle' align='center' style='padding-right: 5px' class='BottomBorder'>" + strStatusImageRed + "</td>");
                    }

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString())
                        SBDeltaEPatches.Append("<td valign='middle' class='BottomBorder' style='padding-top:3px; font-family:Tahoma; font-size:11px; padding-bottom:3px;'>Cyan</td>");
                    else if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString())
                        SBDeltaEPatches.Append("<td valign='middle' class='BottomBorder' style='padding-top:3px; font-family:Tahoma; font-size:11px; padding-bottom:3px;'>Magenta</td>");
                    else if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString())
                        SBDeltaEPatches.Append("<td valign='middle' class='BottomBorder' style='padding-top:3px; font-family:Tahoma; font-size:11px; padding-bottom:3px;'>Yellow</td>");
                    else if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString())
                        SBDeltaEPatches.Append("<td valign='middle' class='BottomBorder' style='padding-top:3px; font-family:Tahoma; font-size:11px; padding-bottom:3px;'>Black</td>");
                    else if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString())
                        SBDeltaEPatches.Append("<td valign='middle' class='BottomBorder' style='padding-top:3px; font-family:Tahoma; font-size:11px; padding-bottom:3px;'>Red</td>");
                    else if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString())
                        SBDeltaEPatches.Append("<td valign='middle' class='BottomBorder' style='padding-top:3px; font-family:Tahoma; font-size:11px; padding-bottom:3px;'>Green</td>");
                    else if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString())
                        SBDeltaEPatches.Append("<td valign='middle' class='BottomBorder' style='padding-top:3px; font-family:Tahoma; font-size:11px; padding-bottom:3px;'>Blue</td>");
                    else if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString())
                        SBDeltaEPatches.Append("<td valign='middle' class='BottomBorder' style='padding-top:3px; font-family:Tahoma; font-size:13px; padding-bottom:3px;'>3-C</td>");

                    SBDeltaEPatches.Append("<td valign='middle' align='center' class='BottomBorder' style='padding-top:3px; font-family:Tahoma; font-size:11px; padding-bottom:3px;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["ΔE"])) + "</td>");
                    SBDeltaEPatches.Append("<td valign='middle' align='center' class='BottomBorder' style='padding-top:3px; font-family:Tahoma; font-size:11px; padding-bottom:3px;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["ΔELimit"])) + "</td>");

                    SBDeltaEPatches.Append("</tr>");
                }
                SBStr.Replace("###DeltaEValues###", SBDeltaEPatches.ToString());
            }
            else
            {
                SBStr.Replace("###DeltaEValues###", "<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>");
            }
            SBStr.Replace("###DeltaEChart###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandardsISO_Page2_DeltaE.Jpeg"));
            #endregion

            #region Papaer Lab 1
            SBStr.Replace("###PaperLab1###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandardsISO_Page2_ISOPaper1.Jpeg"));
            #endregion

            #region Papaer Lab 2
            SBStr.Replace("###PaperLab2###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandardsISO_Page2_ISOPaper2.Jpeg"));
            #endregion

            #region Density

            #region Density Chart
            SBStr.Replace("###DensityChart###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandardsISO_Page2_ISODensity.Jpeg"));
            #endregion

            #region Density Path Values
            currentTable = DS_Common.Tables[6];
            StringBuilder SBstrPatch = new StringBuilder();
            StringBuilder SBstrActual = new StringBuilder();
            StringBuilder SBstrTarget = new StringBuilder();

            SBstrPatch.Append("<table width='100%' border='0' align='center' cellpadding='0' cellspacing='0'><tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'><td class='row-content1' align='center'>&nbsp;</td><td class='row-content1' style='padding-left:2px;' align='center'>C</td><td class='row-content1' style='padding-left:2px;' align='center'>M</td><td class='row-content1' style='padding-left:2px;' align='center'>Y</td><td class='row-content1' style='padding-left:2px;' align='center'>K</td></tr>");
            SBstrActual.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Act</td>");
            SBstrTarget.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Targ</td>");

            if (currentTable != null && currentTable.Rows.Count > 0 && currentTable.Rows[0][0].ToString() != "NO DATA")
            {
                SBStr.Replace("###DBP###", "2px");

                foreach (DataRow dr in currentTable.Rows)
                {
                    SBstrActual.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(dr["Density"])) + "</td>");
                    SBstrTarget.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(dr["TargetDens"])) + "</td>");
                }
                SBstrActual.Append("</tr>");
                SBstrTarget.Append("</tr>");

                SBStr.Replace("###DensityPathValues###", SBstrPatch.ToString() + SBstrActual.ToString() + SBstrTarget.ToString() + "</table>");
            }
            else
            {
                SBStr.Replace("###DBP###", "0px");
                SBStr.Replace("###DensityPathValues###", "<br/><br/><br/><br/>");
            }
            #endregion


            #endregion

            #endregion


            #region Page 3


            #region TVI Spread
            #region TVI Spread Chart
            SBStr.Replace("###TVISpread###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandardsISO_Page3_ISOTVISpread.Jpeg"));
            #endregion

            #region TVI Patch Value
            currentTable = DS_Common.Tables[9];
            StringBuilder SBstrTVIPatch = new StringBuilder();
            StringBuilder SBstrTVIPatchMax = new StringBuilder();
            StringBuilder SBstrTVIPatchTH = new StringBuilder();

            SBstrTVIPatch.Append("<table width='100%' border='0' align='center' cellpadding='0' cellspacing='0'>");
            SBstrTVIPatch.Append("<tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'><td class='row-content1' align='left'>Patch</td>");
            SBstrTVIPatchMax.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Max</td>");
            SBstrTVIPatchTH.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Threshold</td>");

            if (currentTable != null && currentTable.Rows.Count > 0 && currentTable.Rows[0][0].ToString() != "NO DATA")
            {
                SBStr.Replace("###DBP###", "2px");

                foreach (DataRow dr in currentTable.Rows)
                {
                    SBstrTVIPatch.Append("<td class='row-content1' style='padding-left:2px;' align='center'>" + Convert.ToString(dr["TVI"]) + "%</td>");
                    SBstrTVIPatchMax.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(dr["MaxSpread"])) + "</td>");
                    SBstrTVIPatchTH.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(dr["SpreadThreshold"])) + "</td>");
                }
                SBstrTVIPatch.Append("</tr>");
                SBstrTVIPatchMax.Append("</tr>");
                SBstrTVIPatchTH.Append("</tr>");

                SBStr.Replace("###TVISpreadPathValues###", SBstrTVIPatch.ToString() + SBstrTVIPatchMax.ToString() + SBstrTVIPatchTH.ToString() + "</table>");
            }
            else
            {
                SBStr.Replace("###DBP###", "0px");
                SBStr.Replace("###TVISpreadPathValues###", "<br/><br/><br/><br/>");
            }
            #endregion

            #endregion

            #region Cyan TVI
            #region Cyan TVI Chart
            SBStr.Replace("###CyanTVI###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandardsISO_Page3_ISOCyanTVI.Jpeg"));
            #endregion

            #region Cyan Patch Value
            DataView DV = new DataView();
            currentTable = DS_Common.Tables[10];
            StringBuilder SBstrCyanPatch = new StringBuilder();
            StringBuilder SBstrCyanPatchActual = new StringBuilder();
            StringBuilder SBstrCyanPatchTarget = new StringBuilder();
            StringBuilder SBstrCyanPatchThreshold = new StringBuilder();

            SBstrCyanPatch.Append("<table width='100%' border='0' align='center' cellpadding='0' cellspacing='0'>");
            SBstrCyanPatch.Append("<tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'><td class='row-content1' align='left'>Patch</td>");
            SBstrCyanPatchActual.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Actual</td>");
            SBstrCyanPatchTarget.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Target</td>");
            SBstrCyanPatchThreshold.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Threshold</td>");

            if (currentTable != null && currentTable.Rows.Count > 0 && currentTable.Rows[0][0].ToString() != "NO DATA")
            {
                SBStr.Replace("###DBP###", "2px");

                currentTable.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Three_Quarter_Tone).ToString() + "))";
                DV = currentTable.DefaultView;
                DV.Sort = "CMYK_C";
                if (DV.Count > 0)
                {
                    for (int i = 0; i < DV.Count; i++)
                    {
                        if (Convert.ToInt16(DV[i]["CMYK_C"]) != 0 && Convert.ToInt16(DV[i]["CMYK_C"]) != 100)
                        {
                            SBstrCyanPatch.Append("<td class='row-content1' style='padding-left:2px;' align='center'>" + Fuji.Common.CommonFunctions.IntToString(Convert.ToString(DV[i]["CMYK_C"])) + "%</td>");
                            SBstrCyanPatchActual.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[i]["Dotgain"])) + "</td>");
                            SBstrCyanPatchTarget.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[i]["TargetDotGain"])) + "</td>");
                            SBstrCyanPatchThreshold.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[i]["Threshold"])) + "</td>");
                        }
                    }
                }
                SBstrCyanPatch.Append("</tr>");
                SBstrCyanPatchActual.Append("</tr>");
                SBstrCyanPatchTarget.Append("</tr>");
                SBstrCyanPatchThreshold.Append("</tr>");

                SBStr.Replace("###CyanTVIPathValues###", SBstrCyanPatch.ToString() + SBstrCyanPatchActual.ToString() + SBstrCyanPatchTarget.ToString() + SBstrCyanPatchThreshold + "</table>");
            }
            else
            {
                SBStr.Replace("###DBP###", "0px");
                SBStr.Replace("###CyanTVIPathValues###", "<br/><br/><br/><br/>");
            }

            #endregion
            #endregion

            #region Magenta TVI
            #region Magenta TVI Chart
            SBStr.Replace("###MagentaTVI###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandardsISO_Page3_ISOMagentaTVI.Jpeg"));
            #endregion

            #region Magenta Patch Value
            DV = new DataView();
            currentTable = DS_Common.Tables[10];
            StringBuilder SBstrMagentaPatch = new StringBuilder();
            StringBuilder SBstrMagentaPatchActual = new StringBuilder();
            StringBuilder SBstrMagentaPatchTarget = new StringBuilder();
            StringBuilder SBstrMagentaPatchThreshold = new StringBuilder();

            SBstrMagentaPatch.Append("<table width='100%' border='0' align='center' cellpadding='0' cellspacing='0'>");
            SBstrMagentaPatch.Append("<tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'><td class='row-content1' align='left'>Patch</td>");
            SBstrMagentaPatchActual.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Actual</td>");
            SBstrMagentaPatchTarget.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Target</td>");
            SBstrMagentaPatchThreshold.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Threshold</td>");

            if (currentTable != null && currentTable.Rows.Count > 0 && currentTable.Rows[0][0].ToString() != "NO DATA")
            {
                SBStr.Replace("###DBP###", "2px");

                currentTable.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Three_Quarter_Tone).ToString() + "))";
                DV = currentTable.DefaultView;
                DV.Sort = "CMYK_M";
                if (DV.Count > 0)
                {
                    for (int i = 0; i < DV.Count; i++)
                    {
                        if (Convert.ToInt16(DV[i]["CMYK_M"]) != 0 && Convert.ToInt16(DV[i]["CMYK_M"]) != 100)
                        {
                            SBstrMagentaPatch.Append("<td class='row-content1' style='padding-left:2px;' align='center'>" + Fuji.Common.CommonFunctions.IntToString(Convert.ToString(DV[i]["CMYK_M"])) + "%</td>");
                            SBstrMagentaPatchActual.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[i]["Dotgain"])) + "</td>");
                            SBstrMagentaPatchTarget.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[i]["TargetDotGain"])) + "</td>");
                            SBstrMagentaPatchThreshold.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[i]["Threshold"])) + "</td>");
                        }
                    }
                }
                SBstrMagentaPatch.Append("</tr>");
                SBstrMagentaPatchActual.Append("</tr>");
                SBstrMagentaPatchTarget.Append("</tr>");
                SBstrMagentaPatchThreshold.Append("</tr>");

                SBStr.Replace("###MagentaTVIPathValues###", SBstrMagentaPatch.ToString() + SBstrMagentaPatchActual.ToString() + SBstrMagentaPatchTarget.ToString() + SBstrMagentaPatchThreshold + "</table>");
            }
            else
            {
                SBStr.Replace("###DBP###", "0px");
                SBStr.Replace("###MagentaTVIPathValues###", "<br/><br/><br/><br/>");
            }

            #endregion

            #endregion

            #region Yellow TVI

            #region Yellow TVI Chart
            SBStr.Replace("###YellowTVI###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandardsISO_Page3_ISOYellowTVI.Jpeg"));
            #endregion

            #region Yellow Patch Value
            DV = new DataView();
            currentTable = DS_Common.Tables[10];
            StringBuilder SBstrYellowPatch = new StringBuilder();
            StringBuilder SBstrYellowPatchActual = new StringBuilder();
            StringBuilder SBstrYellowPatchTarget = new StringBuilder();
            StringBuilder SBstrYellowPatchThreshold = new StringBuilder();

            SBstrYellowPatch.Append("<table width='100%' border='0' align='center' cellpadding='0' cellspacing='0'>");
            SBstrYellowPatch.Append("<tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'><td class='row-content1' align='left'>Patch</td>");
            SBstrYellowPatchActual.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Actual</td>");
            SBstrYellowPatchTarget.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Target</td>");
            SBstrYellowPatchThreshold.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Threshold</td>");

            if (currentTable != null && currentTable.Rows.Count > 0 && currentTable.Rows[0][0].ToString() != "NO DATA")
            {
                SBStr.Replace("###DBP###", "2px");

                currentTable.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Three_Quarter_Tone).ToString() + "))";
                DV = currentTable.DefaultView;
                DV.Sort = "CMYK_Y";
                if (DV.Count > 0)
                {
                    for (int i = 0; i < DV.Count; i++)
                    {
                        if (Convert.ToInt16(DV[i]["CMYK_Y"]) != 0 && Convert.ToInt16(DV[i]["CMYK_Y"]) != 100)
                        {
                            SBstrYellowPatch.Append("<td class='row-content1' style='padding-left:2px;' align='center'>" + Fuji.Common.CommonFunctions.IntToString(Convert.ToString(DV[i]["CMYK_Y"])) + "%</td>");
                            SBstrYellowPatchActual.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[i]["Dotgain"])) + "</td>");
                            SBstrYellowPatchTarget.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[i]["TargetDotGain"])) + "</td>");
                            SBstrYellowPatchThreshold.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[i]["Threshold"])) + "</td>");
                        }
                    }
                }
                SBstrYellowPatch.Append("</tr>");
                SBstrYellowPatchActual.Append("</tr>");
                SBstrYellowPatchTarget.Append("</tr>");
                SBstrYellowPatchThreshold.Append("</tr>");

                SBStr.Replace("###YellowTVIPathValues###", SBstrYellowPatch.ToString() + SBstrYellowPatchActual.ToString() + SBstrYellowPatchTarget.ToString() + SBstrYellowPatchThreshold + "</table>");
            }
            else
            {
                SBStr.Replace("###DBP###", "0px");
                SBStr.Replace("###YellowTVIPathValues###", "<br/><br/><br/><br/>");
            }

            #endregion

            #endregion

            #region Black TVI
            #region Black TVI Chart
            SBStr.Replace("###BlackTVI###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandardsISO_Page3_ISOBlackTVI.Jpeg"));
            #endregion


            #region Black Patch Value
            DV = new DataView();
            currentTable = DS_Common.Tables[10];
            StringBuilder SBstrBlackPatch = new StringBuilder();
            StringBuilder SBstrBlackPatchActual = new StringBuilder();
            StringBuilder SBstrBlackPatchTarget = new StringBuilder();
            StringBuilder SBstrBlackPatchThreshold = new StringBuilder();

            SBstrBlackPatch.Append("<table width='100%' border='0' align='center' cellpadding='0' cellspacing='0'>");
            SBstrBlackPatch.Append("<tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'><td class='row-content1' align='left'>Patch</td>");
            SBstrBlackPatchActual.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Actual</td>");
            SBstrBlackPatchTarget.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Target</td>");
            SBstrBlackPatchThreshold.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Threshold</td>");

            if (currentTable != null && currentTable.Rows.Count > 0 && currentTable.Rows[0][0].ToString() != "NO DATA")
            {
                SBStr.Replace("###DBP###", "2px");

                currentTable.DefaultView.RowFilter = "(PatchTypeID in (" + Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Black_Quarter_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Black_Half_Tone).ToString() + "," + Convert.ToInt16(Enums.CPSyncPatchType.Black_Three_Quarter_Tone).ToString() + "))";
                DV = currentTable.DefaultView;
                DV.Sort = "CMYK_K";
                if (DV.Count > 0)
                {
                    for (int i = 0; i < DV.Count; i++)
                    {
                        if (Convert.ToInt16(DV[i]["CMYK_K"]) != 0 && Convert.ToInt16(DV[i]["CMYK_K"]) != 100)
                        {
                            SBstrBlackPatch.Append("<td class='row-content1' style='padding-left:2px;' align='center'>" + Fuji.Common.CommonFunctions.IntToString(Convert.ToString(DV[i]["CMYK_K"])) + "%</td>");
                            SBstrBlackPatchActual.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[i]["Dotgain"])) + "</td>");
                            SBstrBlackPatchTarget.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[i]["TargetDotGain"])) + "</td>");
                            SBstrBlackPatchThreshold.Append("<td align='center' class='row-content1'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[i]["Threshold"])) + "</td>");
                        }
                    }
                }
                SBstrBlackPatch.Append("</tr>");
                SBstrBlackPatchActual.Append("</tr>");
                SBstrBlackPatchTarget.Append("</tr>");
                SBstrBlackPatchThreshold.Append("</tr>");

                SBStr.Replace("###BlackTVIPathValues###", SBstrBlackPatch.ToString() + SBstrBlackPatchActual.ToString() + SBstrBlackPatchTarget.ToString() + SBstrBlackPatchThreshold + "</table>");
            }
            else
            {
                SBStr.Replace("###DBP###", "0px");
                SBStr.Replace("###BlackTVIPathValues###", "<br/><br/><br/><br/>");
            }

            #endregion

            #endregion

            #endregion

            #region Page 4


            #region Gray Balance E
            SBStr.Replace("###GrayBalanceE###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandardsISO_Page3_ISOGrayBalanceE.Jpeg"));
            #endregion

            #region Gray Balance L* a* b*
            SBStr.Replace("###GrayBalanceChartL###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandardsISO_Page3_ISOGrayBalanceL.Jpeg"));
            SBStr.Replace("###GrayBalanceChartA###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandardsISO_Page3_ISOGrayBalanceA.Jpeg"));
            SBStr.Replace("###GrayBalanceChartB###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + Sessions.UserID + "_CPSPrintStandardsISO_Page3_ISOGrayBalanceB.Jpeg"));
            #endregion

            #region Gray Balance LAB Patch Value
            currentTable = DS_Common.Tables[12];
            StringBuilder SBstrGrayBalanceLABPatch = new StringBuilder();
            StringBuilder SBstrGrayBalanceL = new StringBuilder();
            StringBuilder SBstrGrayBalanceA = new StringBuilder();
            StringBuilder SBstrGrayBalanceB = new StringBuilder();

            SBstrGrayBalanceLABPatch.Append("<table width='100%' style='border-right: 1PX SOLID #C1C1C1;border-bottom: 1PX SOLID #C1C1C1;' border='0' align='center' cellpadding='0' cellspacing='0'>");
            SBstrGrayBalanceLABPatch.Append("<tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'><td class='row-content1' align='center'></td>");
            SBstrGrayBalanceLABPatch.Append("<td class='row-content1' align='center' colspan='3' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'><b>25% gray</b></td>");
            SBstrGrayBalanceLABPatch.Append("<td class='row-content1' align='center' colspan='3' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'><b>50% gray</b></td>");
            SBstrGrayBalanceLABPatch.Append("<td class='row-content1' align='center' colspan='3' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'><b>75% gray</b></td></tr>");

            SBstrGrayBalanceLABPatch.Append("<tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'><td class='row-content1' align='center'></td>");
            SBstrGrayBalanceLABPatch.Append("<td class='row-content1' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'><b>Actual</b></td>");
            SBstrGrayBalanceLABPatch.Append("<td class='row-content1' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'><b>Target</b></td>");
            SBstrGrayBalanceLABPatch.Append("<td class='row-content1' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'><b>Dev.</b></td>");
            SBstrGrayBalanceLABPatch.Append("<td class='row-content1' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'><b>Actual</b></td>");
            SBstrGrayBalanceLABPatch.Append("<td class='row-content1' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'><b>Target</b></td>");
            SBstrGrayBalanceLABPatch.Append("<td class='row-content1' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'><b>Dev.</b></td>");
            SBstrGrayBalanceLABPatch.Append("<td class='row-content1' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'><b>Actual</b></td>");
            SBstrGrayBalanceLABPatch.Append("<td class='row-content1' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'><b>Target</b></td>");
            SBstrGrayBalanceLABPatch.Append("<td class='row-content1' align='center' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'><b>Dev.</b></td></tr>");


            SBstrGrayBalanceL.Append("<tr class='row-1'><td class='row-content1' style='border-top: 1PX SOLID #C1C1C1;'><b>L*</b></td>");
            SBstrGrayBalanceA.Append("<tr class='row-1'><td class='row-content1' style='border-top: 1PX SOLID #C1C1C1;'><b>a*</b></td>");
            SBstrGrayBalanceB.Append("<tr class='row-1'><td class='row-content1' style='border-top: 1PX SOLID #C1C1C1;'><b>b*</b></td>");

            if (currentTable != null && currentTable.Rows.Count > 0 && currentTable.Rows[0][0].ToString() != "NO DATA")
            {
                SBStr.Replace("###DBP###", "2px");
                DV = currentTable.DefaultView;
                DV.Sort = "CMYK_C";
                if (DV.Count > 0)
                {
                    for (int i = 0; i < DV.Count; i++)
                    {
                        if (Fuji.Common.CommonFunctions.IntToString(Convert.ToString(DV[i]["CMYK_C"])) != "0" && Fuji.Common.CommonFunctions.IntToString(Convert.ToString(DV[i]["CMYK_C"])) != "100")
                        {
                            SBstrGrayBalanceL.Append("<td class='row-content1' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[i]["CIELAB_L*"])) + "</td>");
                            SBstrGrayBalanceA.Append("<td class='row-content1' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[i]["CIELAB_a*"])) + "</td>");
                            SBstrGrayBalanceB.Append("<td class='row-content1' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[i]["CIELAB_b*"])) + "</td>");

                            SBstrGrayBalanceL.Append("<td class='row-content1' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[i]["Target_L*"])) + "</td>");
                            SBstrGrayBalanceA.Append("<td class='row-content1' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[i]["Target_a*"])) + "</td>");
                            SBstrGrayBalanceB.Append("<td class='row-content1' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(DV[i]["Target_b*"])) + "</td>");

                            Decimal L = Convert.ToDecimal(DV[i]["CIELAB_L*"]) - Convert.ToDecimal(DV[i]["Target_L*"]);
                            Decimal a = Convert.ToDecimal(DV[i]["CIELAB_a*"]) - Convert.ToDecimal(DV[i]["Target_a*"]);
                            Decimal b = Convert.ToDecimal(DV[i]["CIELAB_b*"]) - Convert.ToDecimal(DV[i]["Target_b*"]);

                            SBstrGrayBalanceL.Append("<td class='row-content1' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(L)) + "</td>");
                            SBstrGrayBalanceA.Append("<td class='row-content1' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(a)) + "</td>");
                            SBstrGrayBalanceB.Append("<td class='row-content1' style='border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(b)) + "</td>");
                        }
                    }
                    SBstrGrayBalanceL.Append("</tr>");
                    SBstrGrayBalanceA.Append("</tr>");
                    SBstrGrayBalanceB.Append("</tr>");
                }

                SBStr.Replace("###GrayBalanceLABPathValues###", SBstrGrayBalanceLABPatch.ToString() + SBstrGrayBalanceL.ToString() + SBstrGrayBalanceA.ToString() + SBstrGrayBalanceB.ToString() + "</table>");
            }
            else
            {
                SBStr.Replace("###DBP###", "0px");
                SBStr.Replace("###GrayBalanceLABPathValues###", "<br/><br/><br/><br/>");
            }

            #endregion

            #endregion

            #region Page 5
            #region Run Information
            currentTable = DS_Common.Tables[0];
            string strCompany = "";
            if (currentTable != null && currentTable.Rows.Count > 0)
            {
                strCompany = Convert.ToString(currentTable.Rows[0]["CompanyName"]);
                strCompany += " - " + Convert.ToString(currentTable.Rows[0]["BranchName"]);
                strCompany += " - " + Convert.ToString(currentTable.Rows[0]["BranchLocation"]);

                SBStr.Replace("###RCompanyName###", strCompany);
                SBStr.Replace("###RPaperName###", "&nbsp;"); //currentTable.Rows[0]["Substrate"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["Substrate"].ToString());
                SBStr.Replace("###RPressName###", Convert.ToString(currentTable.Rows[0]["PressName"]));
                SBStr.Replace("###Project###", currentTable.Rows[0]["ProjectName"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["ProjectName"].ToString());
                SBStr.Replace("###RInk###", "&nbsp;");//currentTable.Rows[0]["Ink"].ToString() == "" ? "&nbsp;" : currentTable.Rows[0]["Ink"].ToString());
                SBStr.Replace("###RRunTimestamp###", Fuji.Common.CommonFunctions.GetDateByCultures(Convert.ToDateTime(currentTable.Rows[0]["StartDateTime"])));
            }
            else
            {
                SBStr.Replace("###RCompanyName###", "&nbsp;");
                SBStr.Replace("###RPaperName###", "&nbsp;");
                SBStr.Replace("###RPressName###", "&nbsp;");
                SBStr.Replace("###Project###", "&nbsp;");
                SBStr.Replace("###RInk###", "&nbsp;");
                SBStr.Replace("###RRunTimestamp###", "&nbsp;");
            }
            #endregion

            #region Primary Densitometric Print Target
            currentTable = DS_Common.Tables[13];
            DataView dvDotGain = DS_Common.Tables[14].DefaultView;
            dvDotGain.RowFilter = "CMYK_C <> 0 AND CMYK_C <> 100 AND ShowData = 1";
            List<decimal> colValues = new List<decimal>();
            foreach (DataRowView dr in dvDotGain)
            {
                colValues.Add(Convert.ToDecimal(dr["CMYK_C"]));
            }
            StringBuilder SBstrTablePrimaryDensPrintTar = new StringBuilder();
            if (currentTable != null && currentTable.Rows.Count > 0)
            {
                SBstrTablePrimaryDensPrintTar.Append("<table style='width: 100%; border-right: 1PX SOLID #C1C1C1;border-bottom: 1PX SOLID #C1C1C1' cellspacing='0' cellpadding=1'>");
                SBstrTablePrimaryDensPrintTar.Append("<tr>");
                SBstrTablePrimaryDensPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1; font-size:9px;border-top: 1PX SOLID #C1C1C1;width:130px;'></td>");
                SBstrTablePrimaryDensPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;font-size:9px;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;'></td>");
                SBstrTablePrimaryDensPrintTar.Append("<td class='BGRIDTITLE' align='center' colspan='3' style='border-left: 1PX SOLID #C1C1C1;font-size:9px;border-top: 1PX SOLID #C1C1C1;'>TVI</td>");
                SBstrTablePrimaryDensPrintTar.Append("</tr>");

                SBstrTablePrimaryDensPrintTar.Append("<tr>");
                SBstrTablePrimaryDensPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;font-size:9px;border-top: 1PX SOLID #C1C1C1;witdh:130px;'></td>");
                SBstrTablePrimaryDensPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;font-size:9px;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;witdh:15%;'>Density.</td>");
                foreach (decimal colVal in colValues)
                {
                    SBstrTablePrimaryDensPrintTar.Append("<td class='BGRIDTITLE' align='center' style='border-left: 1PX SOLID #C1C1C1;font-size:9px;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;witdh:20%'>" + Math.Round(colVal) + "%</td>");
                }
                SBstrTablePrimaryDensPrintTar.Append("</tr>");

                for (int i = 0; i < currentTable.Rows.Count; i++)
                {
                    SBstrTablePrimaryDensPrintTar.Append("<tr>");
                    if (Convert.ToString(currentTable.Rows[i]["PatchTypeID"]) == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString()) // "1")
                        SBstrTablePrimaryDensPrintTar.Append("<td style='border-left: 1PX SOLID #C1C1C1;font-size:9px;border-top: 1PX SOLID #C1C1C1;BACKGROUND-COLOR:#CCFFFF;COLOR:#000;width:130px;'><b>Cyan</b></td>");
                    else if (Convert.ToString(currentTable.Rows[i]["PatchTypeID"]) == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString()) //"2")
                        SBstrTablePrimaryDensPrintTar.Append("<td style='border-left: 1PX SOLID #C1C1C1;font-size:9px;border-top: 1PX SOLID #C1C1C1;BACKGROUND-COLOR:#FF99CC;COLOR:#000;width:130px;'><b>Magenta</b></td>");
                    else if (Convert.ToString(currentTable.Rows[i]["PatchTypeID"]) == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString()) //"3")
                        SBstrTablePrimaryDensPrintTar.Append("<td style='border-left: 1PX SOLID #C1C1C1;font-size:9px;border-top: 1PX SOLID #C1C1C1;BACKGROUND-COLOR:#FFFF99;COLOR:#000;width:130px;'><b>Yellow</b></td>");
                    else if (Convert.ToString(currentTable.Rows[i]["PatchTypeID"]) == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString()) //"4")
                        SBstrTablePrimaryDensPrintTar.Append("<td style='border-left: 1PX SOLID #C1C1C1;font-size:9px;border-top: 1PX SOLID #C1C1C1;BACKGROUND-COLOR:#C0C0C0;COLOR:#000;width:130px;'><b>Black</b></td>");


                    SBstrTablePrimaryDensPrintTar.Append("<td align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["Density"])) + "</td>");

                    foreach (decimal colVal in colValues)
                    {
                        if (Convert.ToString(currentTable.Rows[i]["PatchTypeID"]) == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString()) //"1")
                        {
                            dvDotGain.RowFilter = "CMYK_C=" + colVal.ToString();
                        }
                        else if (Convert.ToString(currentTable.Rows[i]["PatchTypeID"]) == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString()) //"2")
                        {
                            dvDotGain.RowFilter = "CMYK_M=" + colVal.ToString();
                        }
                        else if (Convert.ToString(currentTable.Rows[i]["PatchTypeID"]) == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString()) //"3")
                        {
                            dvDotGain.RowFilter = "CMYK_Y=" + colVal.ToString();
                        }
                        else if (Convert.ToString(currentTable.Rows[i]["PatchTypeID"]) == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString()) //"4")
                        {
                            dvDotGain.RowFilter = "CMYK_K=" + colVal.ToString();
                        }
                        if (dvDotGain.Count > 0)
                        {
                            SBstrTablePrimaryDensPrintTar.Append("<td align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(dvDotGain[0]["DotGain"])) + "</td>");
                        }
                        else
                        {
                            SBstrTablePrimaryDensPrintTar.Append("<td align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>&nbsp;</td>");
                        }
                    }
                    SBstrTablePrimaryDensPrintTar.Append("</tr>");
                }
                SBstrTablePrimaryDensPrintTar.Append("</table>");
            }
            SBStr.Replace("###PrimaryDensPrintTar###", SBstrTablePrimaryDensPrintTar.ToString());

            #endregion

            #region Primary Lab Print Targets
            currentTable = DS_Common.Tables[15];
            StringBuilder SBstrTablePrimaryLabPrintTar = new StringBuilder();
            if (currentTable != null && currentTable.Rows.Count > 0)
            {
                SBstrTablePrimaryLabPrintTar.Append("<table style='width: 100%; border-right: 1PX SOLID #C1C1C1;border-bottom: 1PX SOLID #C1C1C1;' cellspacing='0' cellpadding=1'>");
                SBstrTablePrimaryLabPrintTar.Append("<tr>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;width:130px;'></td>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;width:18%;'>&Delta;E Tol.</td>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:11%;'>L*</td>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:11%;border-right: 1PX SOLID #C1C1C1;'>a*</td>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:11%;'>b*</td>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:11%;border-right: 1PX SOLID #C1C1C1;'>C*</td>");
                SBstrTablePrimaryLabPrintTar.Append("<td class='BGRIDTITLE' align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:11%;'>h*</td>");
                SBstrTablePrimaryLabPrintTar.Append("</tr>");
                for (int i = 0; i < currentTable.Rows.Count; i++)
                {
                    SBstrTablePrimaryLabPrintTar.Append("<tr>");
                    SBstrTablePrimaryLabPrintTar.Append("<td style='font-size:8px;border-left: 1PX SOLID #C1C1C1;width:130px;background-color:" + Convert.ToString(currentTable.Rows[i]["HTMLColor"]) + "'><b>" + Convert.ToString(currentTable.Rows[i]["PatchType"]) + "</b></td>");
                    SBstrTablePrimaryLabPrintTar.Append("<td align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["ΔELimit"])) + "</td>");
                    SBstrTablePrimaryLabPrintTar.Append("<td align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["CIELAB_L*"])) + "</td>");
                    SBstrTablePrimaryLabPrintTar.Append("<td align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["CIELAB_a*"])) + "</td>");
                    SBstrTablePrimaryLabPrintTar.Append("<td align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["CIELAB_b*"])) + "</td>");
                    SBstrTablePrimaryLabPrintTar.Append("<td align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["C*"])) + "</td>");
                    SBstrTablePrimaryLabPrintTar.Append("<td align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["H'"])) + "</td>");
                    SBstrTablePrimaryLabPrintTar.Append("</tr>");
                }
                SBstrTablePrimaryLabPrintTar.Append("</table>");
            }
            SBStr.Replace("###PrimaryLabPrintTar###", SBstrTablePrimaryLabPrintTar.ToString());
            #endregion

            #region Gray Print Target
            currentTable = DS_Common.Tables[16];
            StringBuilder SBstrTableGrayPrintTarget = new StringBuilder();
            if (currentTable != null && currentTable.Rows.Count > 0)
            {
                SBstrTableGrayPrintTarget.Append("<table style='width: 100%; border-right: 1PX SOLID #C1C1C1;border-bottom: 1PX SOLID #C1C1C1;' cellspacing='0' cellpadding=1'>");
                SBstrTableGrayPrintTarget.Append("<tr>");
                SBstrTableGrayPrintTarget.Append("<td class='BGRIDTITLE' align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;width:130px;'></td>");
                SBstrTableGrayPrintTarget.Append("<td class='BGRIDTITLE' align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;width:18%;'>&Delta;E Tol.</td>");
                SBstrTableGrayPrintTarget.Append("<td class='BGRIDTITLE' align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:11%;'>L*</td>");
                SBstrTableGrayPrintTarget.Append("<td class='BGRIDTITLE' align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:11%;border-right: 1PX SOLID #C1C1C1;'>a*</td>");
                SBstrTableGrayPrintTarget.Append("<td class='BGRIDTITLE' align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:11%;'>b*</td>");
                SBstrTableGrayPrintTarget.Append("<td class='BGRIDTITLE' align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:11%;border-right: 1PX SOLID #C1C1C1;'>C*</td>");
                SBstrTableGrayPrintTarget.Append("<td class='BGRIDTITLE' align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:11%;'>h*</td>");
                SBstrTableGrayPrintTarget.Append("</tr>");
                for (int i = 0; i < currentTable.Rows.Count; i++)
                {
                    SBstrTableGrayPrintTarget.Append("<tr>");
                    SBstrTableGrayPrintTarget.Append("<td style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;width:130px;'><b>" + Convert.ToString(currentTable.Rows[i]["PatchType"]) + "</b></td>");
                    SBstrTableGrayPrintTarget.Append("<td align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["ΔETol"])) + "</td>");
                    SBstrTableGrayPrintTarget.Append("<td align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["CIELAB_L*"])) + "</td>");
                    SBstrTableGrayPrintTarget.Append("<td align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["CIELAB_a*"])) + "</td>");
                    SBstrTableGrayPrintTarget.Append("<td align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["CIELAB_b*"])) + "</td>");
                    SBstrTableGrayPrintTarget.Append("<td align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["C*"])) + "</td>");
                    SBstrTableGrayPrintTarget.Append("<td align='center' style='font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;'>" + Fuji.Common.CommonFunctions.DecimalToString(Convert.ToString(currentTable.Rows[i]["H*"])) + "</td>");
                    SBstrTableGrayPrintTarget.Append("</tr>");
                }
                SBstrTableGrayPrintTarget.Append("</table>");
            }
            SBStr.Replace("###GrayPrintTar###", SBstrTableGrayPrintTarget.ToString());

            #endregion

            #region Desired Print Target
            currentTable = DS_Common.Tables[17];
            if (currentTable != null && currentTable.Rows.Count > 0)
            {
                for (int i = 0; i < currentTable.Rows.Count; i++)
                {
                    #region Fill data from database
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        SBStr.Replace("###ISOCyanTL###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        SBStr.Replace("###ISOCyanTa###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        SBStr.Replace("###ISOCyanTb###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISOCyanTH###", "5");
                    SBStr.Replace("###ISOCyanHTH###", "2.50");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        SBStr.Replace("###ISOMagendaTL###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        SBStr.Replace("###ISOMagendaTa###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        SBStr.Replace("###ISOMagendaTb###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISOMagendaTH###", "5");
                    SBStr.Replace("###ISOMagendaHTH###", "2.50");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        SBStr.Replace("###ISOYellowTL###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        SBStr.Replace("###ISOYellowTa###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        SBStr.Replace("###ISOYellowTb###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISOYellowTH###", "5");
                    SBStr.Replace("###ISOYellowHTH###", "2.50");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        SBStr.Replace("###ISOBlackTL###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        SBStr.Replace("###ISOBlackTa###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        SBStr.Replace("###ISOBlackTb###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISOBlackTH###", "5");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        SBStr.Replace("###ISORedTL###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        SBStr.Replace("###ISORedTa###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Red).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        SBStr.Replace("###ISORedTb###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISORedTH###", "10");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        SBStr.Replace("###ISOGreenTL###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        SBStr.Replace("###ISOGreenTa###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Green).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        SBStr.Replace("###ISOGreenTb###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISOGreenTH###", "10");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        SBStr.Replace("###ISOBlueTL###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        SBStr.Replace("###ISOBlueTa###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Overprint_Blue).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        SBStr.Replace("###ISOBlueTb###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISOBlueTH###", "10");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        SBStr.Replace("###ISO3CTL###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        SBStr.Replace("###ISO3CTa###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Triplet_Solid_Gray).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        SBStr.Replace("###ISO3CTb###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISO3CTH###", "10");


                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_25).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        SBStr.Replace("###ISOGray25TL###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_25).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        SBStr.Replace("###ISOGray25Ta###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_25).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        SBStr.Replace("###ISOGray25Tb###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISOGray25TH###", "2");


                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_50).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        SBStr.Replace("###ISOGray50TL###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_50).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        SBStr.Replace("###ISOGray50Ta###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_50).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        SBStr.Replace("###ISOGray50Tb###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISOGray50TH###", "2");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_75).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        SBStr.Replace("###ISOGray75TL###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_75).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        SBStr.Replace("###ISOGray75Ta###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Gray_75).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        SBStr.Replace("###ISOGray75Tb###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISOGray75TH###", "2");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.L).ToString())
                        SBStr.Replace("###ISOAPPaperTL###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.a).ToString())
                        SBStr.Replace("###ISOAPPaperTa###", currentTable.Rows[i]["Target"].ToString());
                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Paper).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.b).ToString())
                        SBStr.Replace("###ISOAPPaperTb###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISOTHPaperTL###", "3");
                    SBStr.Replace("###ISOTHPaperTa###", "2");
                    SBStr.Replace("###ISOTHPaperTb###", "2");


                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Quarter_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        SBStr.Replace("###ISO25CA###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISO25CT###", "3");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Quarter_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        SBStr.Replace("###ISO25MA###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISO25MT###", "3");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Quarter_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        SBStr.Replace("###ISO25YA###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISO25YT###", "3");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Quarter_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        SBStr.Replace("###ISO25KA###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISO25KT###", "3");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Half_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        SBStr.Replace("###ISO50CA###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISO50CT###", "4");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Half_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        SBStr.Replace("###ISO50MA###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISO50MT###", "4");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Half_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        SBStr.Replace("###ISO50YA###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISO50YT###", "4");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Half_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        SBStr.Replace("###ISO50KA###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISO50KT###", "4");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan_Three_Quarter_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        SBStr.Replace("###ISO75CA###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISO75CT###", "3");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta_Three_Quarter_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        SBStr.Replace("###ISO75MA###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISO75MT###", "3");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow_Three_Quarter_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        SBStr.Replace("###ISO75YA###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISO75YT###", "3");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black_Three_Quarter_Tone).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.DotGain_TVI).ToString())
                        SBStr.Replace("###ISO75KA###", currentTable.Rows[i]["Target"].ToString());
                    SBStr.Replace("###ISO75KT###", "3");


                    SBStr.Replace("###ISOQuarterTone###", "3");
                    SBStr.Replace("###ISOMidTone###", "5");
                    SBStr.Replace("###ISO3QuarterTone###", "3");

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Cyan).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                        SBStr.Replace("###ISOCyanDT###", currentTable.Rows[i]["Target"].ToString());

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Magenta).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                        SBStr.Replace("###ISOMagendaDT###", currentTable.Rows[i]["Target"].ToString());

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Yellow).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                        SBStr.Replace("###ISOYellowDT###", currentTable.Rows[i]["Target"].ToString());

                    if (currentTable.Rows[i]["PatchTypeID"].ToString() == Convert.ToInt16(Enums.CPSyncPatchType.Black).ToString() && currentTable.Rows[i]["ColorCharID"].ToString() == Convert.ToInt16(Enums.CPSyncColorChar.Density).ToString())
                        SBStr.Replace("###ISOBalckDT###", currentTable.Rows[i]["Target"].ToString());
                    #endregion
                }
            }
            else
            {
                #region Fill blank value
                SBStr.Replace("###ISOCyanTL###", "");
                SBStr.Replace("###ISOCyanTa###", "");
                SBStr.Replace("###ISOCyanTb###", "");
                SBStr.Replace("###ISOCyanTH###", "5");
                SBStr.Replace("###ISOCyanHTH###", "2.50");
                SBStr.Replace("###ISOMagendaTL###", "");
                SBStr.Replace("###ISOMagendaTa###", "");
                SBStr.Replace("###ISOMagendaTb###", "");
                SBStr.Replace("###ISOMagendaTH###", "5");
                SBStr.Replace("###ISOMagendaHTH###", "2.50");
                SBStr.Replace("###ISOYellowTL###", "");
                SBStr.Replace("###ISOYellowTa###", "");
                SBStr.Replace("###ISOYellowTb###", "");
                SBStr.Replace("###ISOYellowTH###", "5");
                SBStr.Replace("###ISOYellowHTH###", "2.50");
                SBStr.Replace("###ISOBlackTL###", "");
                SBStr.Replace("###ISOBlackTa###", "");
                SBStr.Replace("###ISOBlackTb###", "");
                SBStr.Replace("###ISOBlackTH###", "5");
                SBStr.Replace("###ISORedTL###", "");
                SBStr.Replace("###ISORedTa###", "");
                SBStr.Replace("###ISORedTb###", "");
                SBStr.Replace("###ISORedTH###", "10");
                SBStr.Replace("###ISOGreenTL###", "");
                SBStr.Replace("###ISOGreenTa###", "");
                SBStr.Replace("###ISOGreenTb###", "");
                SBStr.Replace("###ISOGreenTH###", "10");
                SBStr.Replace("###ISOBlueTL###", "");
                SBStr.Replace("###ISOBlueTa###", "");
                SBStr.Replace("###ISOBlueTb###", "");
                SBStr.Replace("###ISOBlueTH###", "10");
                SBStr.Replace("###ISO3CTL###", "");
                SBStr.Replace("###ISO3CTa###", "");
                SBStr.Replace("###ISO3CTb###", "");
                SBStr.Replace("###ISO3CTH###", "10");
                SBStr.Replace("###ISOGray25TL###", "");
                SBStr.Replace("###ISOGray25Ta###", "");
                SBStr.Replace("###ISOGray25Tb###", "");
                SBStr.Replace("###ISOGray25TH###", "2");
                SBStr.Replace("###ISOGray50TL###", "");
                SBStr.Replace("###ISOGray50Ta###", "");
                SBStr.Replace("###ISOGray50Tb###", "");
                SBStr.Replace("###ISOGray50TH###", "2");
                SBStr.Replace("###ISOGray75TL###", "");
                SBStr.Replace("###ISOGray75Ta###", "");
                SBStr.Replace("###ISOGray75Tb###", "");
                SBStr.Replace("###ISOGray75TH###", "2");
                SBStr.Replace("###ISOAPPaperTL###", "");
                SBStr.Replace("###ISOAPPaperTa###", "");
                SBStr.Replace("###ISOAPPaperTb###", "");
                SBStr.Replace("###ISOTHPaperTL###", "3");
                SBStr.Replace("###ISOTHPaperTa###", "2");
                SBStr.Replace("###ISOTHPaperTb###", "2");
                SBStr.Replace("###ISO25CA###", "");
                SBStr.Replace("###ISO25CT###", "3");
                SBStr.Replace("###ISO25MA###", "");
                SBStr.Replace("###ISO25MT###", "3");
                SBStr.Replace("###ISO25YA###", "");
                SBStr.Replace("###ISO25YT###", "3");
                SBStr.Replace("###ISO25KA###", "");
                SBStr.Replace("###ISO25KT###", "3");
                SBStr.Replace("###ISO50CA###", "");
                SBStr.Replace("###ISO50CT###", "4");
                SBStr.Replace("###ISO50MA###", "");
                SBStr.Replace("###ISO50MT###", "4");
                SBStr.Replace("###ISO50YA###", "");
                SBStr.Replace("###ISO50YT###", "4");
                SBStr.Replace("###ISO50KA###", "");
                SBStr.Replace("###ISO50KT###", "4");
                SBStr.Replace("###ISO75CA###", "");
                SBStr.Replace("###ISO75CT###", "3");
                SBStr.Replace("###ISO75MA###", "");
                SBStr.Replace("###ISO75MT###", "3");
                SBStr.Replace("###ISO75YA###", "");
                SBStr.Replace("###ISO75YT###", "3");
                SBStr.Replace("###ISO75KA###", "");
                SBStr.Replace("###ISO75KT###", "3");
                SBStr.Replace("###ISOQuarterTone###", "3");
                SBStr.Replace("###ISOMidTone###", "5");
                SBStr.Replace("###ISO3QuarterTone###", "3");
                SBStr.Replace("###ISOCyanDT###", "");
                SBStr.Replace("###ISOMagendaDT###", "");
                SBStr.Replace("###ISOYellowDT###", "");
                SBStr.Replace("###ISOBalckDT###", "");
                #endregion
            }
            #endregion
            #endregion

            #region Page 6
            currentTable = DS_Common.Tables[18];

            StringBuilder SBstrTablePageHeader = new StringBuilder();
            StringBuilder SBstrTableHeader = new StringBuilder();
            StringBuilder SBstrTableDetail = new StringBuilder();
            StringBuilder SBstrCompleteTable = new StringBuilder();

            if (currentTable != null && currentTable.Rows.Count > 0)
            {


                SBstrTablePageHeader.Append("<table style='width: 660px' cellspacing='0' cellpadding='0' align='center' border='0'>");
                SBstrTablePageHeader.Append("<tr>");
                SBstrTablePageHeader.Append("<td align='center'>");
                SBstrTablePageHeader.Append("<table style='width: 660px' align='center' cellpadding='0' cellspacing='0' border='0'>");
                SBstrTablePageHeader.Append("<tr>");
                SBstrTablePageHeader.Append("<td width='9'><img src='##IMAGES##header-left-img.gif' /></td>");
                SBstrTablePageHeader.Append("<td class='header-bg' style='background: url(##URLIMAGES##header-middle-bg.gif) repeat-x left top;'>");
                SBstrTablePageHeader.Append("<table width='642' border='0' cellspacing='0' cellpadding='0'>");
                SBstrTablePageHeader.Append("<tr>");
                SBstrTablePageHeader.Append("<td class='header-title'>Measurement Report</td>");
                SBstrTablePageHeader.Append("<td>&nbsp;</td>");
                SBstrTablePageHeader.Append("<td width='116' valign='middle'><img src='##IMAGES##powered-logo.gif' /></td>");
                SBstrTablePageHeader.Append("</tr></table></td>");
                SBstrTablePageHeader.Append("<td width='9'><img src='##IMAGES##header-right-img.gif' /></td></tr></table>");
                SBstrTablePageHeader.Append("</td></tr>");
                SBstrTablePageHeader.Append("<tr><td align='center' valign='top'>###Table###</td></tr></table>");


                SBstrTableHeader.Append("<table style='width: 99%; border-right: 1PX SOLID #C1C1C1;border-bottom: 1PX SOLID #C1C1C1;' cellspacing='0' cellpadding=0' border='0' align='center'>");
                SBstrTableHeader.Append("<tr>");
                SBstrTableHeader.Append("<td style='width: 5%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' class='BGRIDTITLE' align='center'>&nbsp;</td>");
                SBstrTableHeader.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' class='BGRIDTITLE' align='center'>Sample<br>Name</td>");
                SBstrTableHeader.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' class='BGRIDTITLE' align='center'>C</td>");
                SBstrTableHeader.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' class='BGRIDTITLE' align='center'>M</td>");
                SBstrTableHeader.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' class='BGRIDTITLE' align='center'>Y</td>");
                SBstrTableHeader.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' class='BGRIDTITLE' align='center'>K</td>");
                SBstrTableHeader.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' class='BGRIDTITLE' align='center'>D_R</td>");
                SBstrTableHeader.Append("<td style='width: 7%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' class='BGRIDTITLE' align='center'>D_G</td>");
                SBstrTableHeader.Append("<td style='width: 7%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' class='BGRIDTITLE' align='center'>D_B</td>");
                SBstrTableHeader.Append("<td style='width: 7%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' class='BGRIDTITLE' align='center'>D_V</td>");
                SBstrTableHeader.Append("<td style='width: 7%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' class='BGRIDTITLE' align='center'>X</td>");
                SBstrTableHeader.Append("<td style='width: 7%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' class='BGRIDTITLE' align='center'>Y</td>");
                SBstrTableHeader.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' class='BGRIDTITLE' align='center'>Z</td>");
                SBstrTableHeader.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' class='BGRIDTITLE' align='center'>L*</td>");
                SBstrTableHeader.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' class='BGRIDTITLE' align='center'>a*</td>");
                SBstrTableHeader.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' class='BGRIDTITLE' align='center'>b*</td>");
                SBstrTableHeader.Append("</tr>");
                SBstrTableHeader.Append("###TableDetail###");

                Int64 RecNo = 0;

                for (int i = 0; i < currentTable.Rows.Count; i++)
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

                    string HtmlColor = "<span style='background-color:" + currentTable.Rows[i]["HTMLColor"].ToString() + "'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";

                    SBstrTableDetail.Append("<td style='width: 5%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' align='center'>" + HtmlColor + "</td>");
                    SBstrTableDetail.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' >" + currentTable.Rows[i]["SampleName"].ToString() + "</td>");
                    SBstrTableDetail.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' >" + Convert.ToInt16(currentTable.Rows[i]["CMYK_C"]).ToString() + "</td>");
                    SBstrTableDetail.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' >" + Convert.ToInt16(currentTable.Rows[i]["CMYK_M"]).ToString() + "</td>");
                    SBstrTableDetail.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' >" + Convert.ToInt16(currentTable.Rows[i]["CMYK_Y"]).ToString() + "</td>");
                    SBstrTableDetail.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' >" + Convert.ToInt16(currentTable.Rows[i]["CMYK_K"]).ToString() + "</td>");
                    SBstrTableDetail.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' >" + string.Format("{0:f2}", Convert.ToDecimal(currentTable.Rows[i]["D_R"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 7%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' >" + string.Format("{0:f2}", Convert.ToDecimal(currentTable.Rows[i]["D_G"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 7%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' >" + string.Format("{0:f2}", Convert.ToDecimal(currentTable.Rows[i]["D_B"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 7%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' >" + string.Format("{0:f2}", Convert.ToDecimal(currentTable.Rows[i]["D_V"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 7%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' >" + string.Format("{0:f2}", Convert.ToDecimal(currentTable.Rows[i]["CIEXYZ_X"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 7%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' >" + string.Format("{0:f2}", Convert.ToDecimal(currentTable.Rows[i]["CIEXYZ_Y"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' >" + string.Format("{0:f2}", Convert.ToDecimal(currentTable.Rows[i]["CIEXYZ_Z"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' >" + string.Format("{0:f2}", Convert.ToDecimal(currentTable.Rows[i]["CIELAB_L*"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' >" + string.Format("{0:f2}", Convert.ToDecimal(currentTable.Rows[i]["CIELAB_a*"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 6%;font-size:9px;border-left: 1PX SOLID #C1C1C1;border-top: 1PX SOLID #C1C1C1;border-right: 1PX SOLID #C1C1C1;' >" + string.Format("{0:f2}", Convert.ToDecimal(currentTable.Rows[i]["CIELAB_b*"])) + "</td>");
                    SBstrTableDetail.Append("</tr>");



                    if ((i == (currentTable.Rows.Count - 1)) && (RecNo <= 42))
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
            SBStr.Replace("###PatchDetail_Table###", SBstrCompleteTable.ToString());
            #endregion

            #region Assign Server

            SBStr.Replace("##URLIMAGES##", WebSettings.ImageURL + "Images/");
            SBStr.Replace("##IMAGES##", strServer);

            #endregion

            return SBStr;
        }



        #endregion
    }
}