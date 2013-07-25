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

namespace FujifilmUSA.Sync.Common
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
        string _From_ΔE;
        string _DLPName;
        string _ReportName;
        bool _IsValidation;
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

        #region Report 13 : Single Measureament Report
        private StringBuilder TextReturn_ReportSingleMeasurement(StringBuilder SBStr)
        {
            // === Image string to replace in chart tables === //

            DataTable DT = new DataTable();

            SBStr.Replace("#csspath#", CSSPath);
            SBStr.Replace("###ReportName###", ReportName);

            // === Table 0 = "JobDetail" : Replace Job Detail In HTML Tamplate ==== //    
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

                //--------For G7 / ISO Reports
                if (ReportType == 16 || ReportType == 17)
                {
                    SBStr.Replace("###JobStatus###", Convert.ToString(DT.Rows[0]["JobStatus"]).Trim() == "" ? "<td class='row-title'>Meas. Status:</td><td class='row-content'>&nbsp;</td>" : (Convert.ToString(DT.Rows[0]["JobStatus"]).Contains("OK")) ? "<td class='row-title'>Meas. Status:</td><td class='row-content'>" + Convert.ToString(DT.Rows[0]["JobStatus"]).Trim() + "</td>" : "<td class='row-title'  style='color:Red;'>Meas. Status:</td><td class='row-content'  style='color:Red;'>" + Convert.ToString(DT.Rows[0]["JobStatus"]).Trim() + "</td>");
                    SBStr.Replace("###MetricPreset###", Convert.ToString(DT.Rows[0]["MetricTargetPresetName"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["MetricTargetPresetName"]).Trim());
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

                 //--------For G7 / ISO Reports
                if (ReportType == 16 || ReportType == 17)
                {
                    SBStr.Replace("###JobStatus###", "<td class='row-title'>Meas. Status:</td><td class='row-content'>&nbsp;</td>");
                    SBStr.Replace("###MetricPreset###", "&nbsp;");
                }
            }
            #endregion

            // === Table 1 = Chart of Target Vs Actual AB Plot === //           
            #region ==== Target Vs Actual AB Plot ====
            SBStr.Replace("###TargetVersusActualAB###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_TargetVersusActualAB.Jpeg"));
            #endregion

            // === Table 2 = Chart of Summary Delta Value Graph Image And Table Value Ploted === //          
            #region ==== Summary Delta Value Graph Image ====
                       
            DT = DS_Common.Tables[2];
           
            StringBuilder SBstrDeltaValue = new StringBuilder();
            if (DT != null && DT.Rows.Count > 0)
            {
                string strStatusImg = "";

                //--------For G7 / ISO Reports
                if (ReportType == 16 || ReportType == 17)
                {
                    for (int i = 0; i < DT.Rows.Count; i++)
                    {
                        if (Convert.ToDecimal(DT.Rows[i]["MeasuredValue"]) > Convert.ToDecimal(DT.Rows[i]["TargetValue"].ToString()))
                        {
                            SBstrDeltaValue.Append("<tr style='height: 27px; color:Red;'>");
                        }
                        else
                        {
                            SBstrDeltaValue.Append("<tr style='height: 27px'>");
                        }                       
                        SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>" + DT.Rows[i]["MetricSumName"] + "</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["TargetValue"])) + "&nbsp;</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["MeasuredValue"])) + "&nbsp;</td>");
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
                        SBstrDeltaValue.Append("<td valign='middle' class='BottomBorder' style='width: 160px;'>Average &Delta; " + From_ΔE + "</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                        SBstrDeltaValue.Append("</tr>");
                    }
                    else
                    {
                        SBstrDeltaValue.Append("<tr style='height: 27px'>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Average &Delta; " + From_ΔE + "</td>");
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
                        SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Maximum &Delta; " + From_ΔE + "</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                        SBstrDeltaValue.Append("</tr>");
                    }
                    else
                    {

                        SBstrDeltaValue.Append("<tr style='height: 27px'>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Maximum &Delta; " + From_ΔE + "</td>");
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
                        SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Max Primary &Delta; " + From_ΔE + "</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                        SBstrDeltaValue.Append("</tr>");
                    }
                    else
                    {
                        SBstrDeltaValue.Append("<tr style='height: 27px'>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Max Primary &Delta; " + From_ΔE + "</td>");
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
                        SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Max Primary &Delta; H</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                        SBstrDeltaValue.Append("</tr>");
                    }
                    else
                    {

                        SBstrDeltaValue.Append("<tr style='height: 27px'>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Max Primary &Delta; H</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["PriΔH"])) + "&nbsp;</td>");
                        SBstrDeltaValue.Append("</tr>");
                    }
                    #endregion

                    #region "GrayAverageDH"
                    if (Convert.IsDBNull(DT.Rows[0]["GrayΔE"]))
                    {
                        SBstrDeltaValue.Append("<tr style='height: 27px'>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>&nbsp;</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Gray Average &Delta; " + From_ΔE + "</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                        SBstrDeltaValue.Append("</tr>");
                    }
                    else
                    {

                        SBstrDeltaValue.Append("<tr style='height: 27px'>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>Gray Average &Delta; " + From_ΔE + "</td>");
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
                        SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>95th Percentile &Delta; " + From_ΔE + "</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>&nbsp;</td>");
                        SBstrDeltaValue.Append("</tr>");
                    }
                    else
                    {

                        SBstrDeltaValue.Append("<tr style='height: 27px'>");
                        SBstrDeltaValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                        SBstrDeltaValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>95th Percentile &Delta; " + From_ΔE + "</td>");
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

            // === Table 3 = Chart of Summary Delta E Value Graph Image And Table Value Ploted  === //         
            #region ==== Summary Delta E Value Graph Image ====
            DT = DS_Common.Tables[3];
            StringBuilder SBstrDeltaEValue = new StringBuilder();
            if (DT != null && DT.Rows.Count > 0)
            {
                string strStatusImg = "";

                for (int i = 0; i < DT.Rows.Count; i++)
                {

                    SBstrDeltaEValue.Append("<tr style='height: 20px'>");
                    SBstrDeltaEValue.Append("<td valign='middle' align='center' style='width: 25px; padding-right: 5px' class='BottomBorder'>" + strStatusImg + "</td>");
                    SBstrDeltaEValue.Append("<td valign='middle' align='left' class='BottomBorder' style='width: 160px;'>" + Convert.ToString(DT.Rows[i]["PatchType"]) + "</td>");
                    //--------For G7 / ISO Reports put Tolerance
                    if (ReportType == 16 || ReportType == 17) 
                    {
                        SBstrDeltaEValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["ΔELimit"])) + "&nbsp;</td>");
                    }
                    else
                        SBstrDeltaEValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + "&nbsp;</td>");
                    SBstrDeltaEValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["ΔE"])) + "&nbsp;</td>");
                    SBstrDeltaEValue.Append("</tr>");
                }
                SBStr.Replace("###DeltaESummaryDetail###", SBstrDeltaEValue.ToString());
            }
            else
            {
                SBStr.Replace("###DeltaESummaryDetail###", "");
            }
            SBStr.Replace("###ResultSummary_DeltaEImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_ResultSummary_DeltaEValue.Jpeg"));
            #endregion

            //--------For G7 / All Validation Report
            if (IsValidation)
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
            }
            #region Assign Server
            SBStr.Replace("##IMAGES##", strServer);
            #endregion

            return SBStr;
        }
        #endregion

    }
}
