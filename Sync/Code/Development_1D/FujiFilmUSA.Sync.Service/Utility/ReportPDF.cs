using FujifilmUSA.Sync.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using WebSupergoo.ABCpdf10;


namespace FujiFilmUSA.Sync.Service.Utility
{
    //Create another class for report generation of client charts
    public partial class ReportPDF
    {
        #region Declaration

        //string strServer = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Substring(0, System.Web.HttpContext.Current.Request.Url.AbsoluteUri.LastIndexOf("/") + 1);
        string strServer = System.Web.HttpContext.Current.Server.MapPath("Images") + "\\";
        int _ReportType = 0;
        string _PDFPath;
        string _PDF_NAME;
        string CSSPath = "";
        int theCount = 0;
        string _From_ΔE;
        string _DLPName;
        string _ReportName;
        string _WebURL;
        bool _IsValidation;
        private ChartContent _chartContent;
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
        public ChartContent chartContent
        {
            get { return _chartContent; }
            set { _chartContent = value; }
        }
        public string BaseWebURL
        {
            get { return _WebURL; }
            set { _WebURL = value; }
        }
        #endregion

        #region Constructor
        public ReportPDF()
        {
            
        }
        #endregion

        #region Declaration
       
        #endregion

        #region Property
       
        #endregion

        public void GenerateClientReportPDF()
        {
            try
            {
                string FileName = "~/Templates/KendoReportTest.htm";

                Doc theDoc = new Doc();
                //theDoc.HtmlOptions.Engine = WebSupergoo.ABCpdf8.EngineType.Gecko;
                //========== Reads the template ==========//
                StreamReader fp;

                //========== The rectangle for the content on pdf ==========//
                theDoc.Rect.Left = 0;
                theDoc.Rect.Bottom = 30;
                theDoc.Rect.Width = 610;
                theDoc.Rect.Height = 770;
                theDoc.Page = theDoc.AddPage();


                //========== The page ID Default is 1 ==========//
                int theID;

                fp = File.OpenText(System.Web.HttpContext.Current.Server.MapPath(FileName));
                StringBuilder SBStr = new StringBuilder(fp.ReadToEnd());
                SBStr = SetPDFContent(SBStr);
                fp.Close();
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

                if (FileName.Trim() == "")
                {
                    throw new Exception("Please Enter Report Type.");
                }

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
        public StringBuilder SetPDFContent(StringBuilder SBStr)
        {
            try
            {
                string currentBaseUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority +
                                    HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";

                //** Add Styles Path ***//
                string cssFullPath = currentBaseUrl + "Templates/style.css";
                SBStr.Replace("#csspath#", cssFullPath);

                //** Add Images Path ***//
                string imagesPath = this.BaseWebURL + "Images/";
                SBStr.Replace("##IMAGES##", imagesPath);

                //** Add Content ***//
                SBStr.Replace("###ReportName###", this.ReportName);

                SBStr.Replace("###TargetVersusActualAB###", this.chartContent.ABComparisionSpiderChart);
                SBStr.Replace("###ABComparison###", this.chartContent.ABActualTargetChart);
                SBStr.Replace("###LComparison###", this.chartContent.LComparisionChart);
                SBStr.Replace("###GrayL###", this.chartContent.LGrayBalanceChart);
                SBStr.Replace("###GrayA###", this.chartContent.AGrayBalanceChart);
                SBStr.Replace("###GrayB###", this.chartContent.BGrayBalanceChart);

            }
            catch (Exception ex)
            {
                throw;
            }

            return SBStr;
        }

    }
}