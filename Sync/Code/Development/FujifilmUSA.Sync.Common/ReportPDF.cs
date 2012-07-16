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


                #region Case 13 : Prepress Single Measurement ReportSingleMeasurement


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
                SBStr.Replace("###TimeStamp###", Common.Utilities.GetDateByCultures(Convert.ToDateTime(DT.Rows[0]["CreatedDT"])));
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
            SBStr.Replace("###TargetVersusActualAB###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_TargetVersusActualAB.Jpeg"));
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
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["AverageDE_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["AverageDE_TH"])) + "&nbsp;</td>");
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
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["MaximumDE_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["MaximumDE_TH"])) + "&nbsp;</td>");
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
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["PrimaryDE_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["PrimaryDE_TH"])) + "&nbsp;</td>");
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
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["PrimaryDH_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["PrimaryDH_TH"])) + "&nbsp;</td>");
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
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["GrayAverageDH_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["GrayAverageDH_TH"])) + "&nbsp;</td>");
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
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["dE_95pct_Patches_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["dE_95pct_Patches_TH"])) + "&nbsp;</td>");
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
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["dE_OuterGamut_ACT"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[0]["dE_OuterGamut_TH"])) + "&nbsp;</td>");
                    SBstrDeltaValue.Append("</tr>");
                }
                #endregion

                SBStr.Replace("###DeltaSummaryDetail###", SBstrDeltaValue.ToString());
            }
            else
            {
                SBStr.Replace("###DeltaSummaryDetail###", "");
            }
            SBStr.Replace("###ResultSummary_DeltaImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_ResultSummary_Delta.Jpeg"));
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
                    SBstrDeltaEValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["DE"])) + "&nbsp;</td>");
                    SBstrDeltaEValue.Append("<td valign='middle' align='center' class='BottomBorder'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["TOL"])) + "&nbsp;</td>");
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
                SBStr.Replace("###GrayLDeviation_GraphImage###", "<table cellpadding='0' cellspacing='0' width='100%' border='0'><tr><td class='summary-bg' style='background: url(##URLIMAGES##summary-title-bg.gif) repeat-x left top;'><img src='##IMAGES##summary-icon.png' width='12' height='11' style='margin: 2px 0 0 5px;' /><span class='summary-title1'>Gray L* Deviation Graph</span></td></tr><tr><td ><img src='" + System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page2_GrayDeviation_L.Jpeg") + "' height='110px' width='658px' alt='Gray L* Deviation' id='img2' /></td></tr></table>");
                SBStr.Replace("###GrayADeviation_GraphImage###", "<table cellpadding='0' cellspacing='0' width='100%' border='0'><tr><td class='summary-bg' style='background: url(##URLIMAGES##summary-title-bg.gif) repeat-x left top;'><img src='##IMAGES##summary-icon.png' width='12' height='11' style='margin: 2px 0 0 5px;' /><span class='summary-title1'>Gray a* Deviation Graph</span></td></tr><tr><td ><img src='" + System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page2_GrayDeviation_A.Jpeg") + "' height='110px' width='658px' alt='Gray a* Deviation' id='img5' /></td></tr></table>");
                SBStr.Replace("###GrayBDeviation_GraphImage###", "<table cellpadding='0' cellspacing='0' width='100%' border='0'><tr><td class='summary-bg' style='background: url(##URLIMAGES##summary-title-bg.gif) repeat-x left top;'><img src='##IMAGES##summary-icon.png' width='12' height='11' style='margin: 2px 0 0 5px;' /><span class='summary-title1'>Gray b* Deviation Graph</span></td></tr><tr><td ><img src='" + System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page2_GrayDeviation_B.Jpeg") + "' height='110px' width='658px' alt='Gray b* Deviation' id='img6' /></td></tr></table>");

                SBstrGrayLABValue.Append("<table cellpadding='0' cellspacing='0' width='100%' border='0'><tr><td><table width='660' border='0' cellspacing='0' cellpadding='0'><tr><td class='summary-bg' style='background: url(##URLIMAGES##summary-title-bg.gif) repeat-x left top;'><img src='##IMAGES##summary-icon.png' width='12' height='11' style='margin: 2px 0 0 5px;' /><span class='summary-title1'>Gray Patch Values</span></td></tr></table></td></tr><tr><td><table cellpadding='0' cellspacing='0' width='100%' border='0'>###VALUE###</table></td></tr></table>");

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    SBstrPatch.Append("<td align='center' class='row-content1'>" + DT.Rows[i]["GRAY"].ToString() + "</td>");
                    SBstrLActual.Append("<td align='center' class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["DL"])) + "</td>");
                    SBstrAActual.Append("<td align='center' class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["DA"])) + "</td>");
                    SBstrBActual.Append("<td align='center' class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["DB"])) + "</td>");
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

                SBStr.Replace("###GrayNPD_GraphImage###", "<table cellpadding='0' cellspacing='0' width='100%' border='0' ><tr><td style='width:310px; height:270px'><img src='" + System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page2_GrayNPD.Jpeg") + "' height='270px' width='310px' alt='Gray NPD' id='img7' /></td></tr><tr><td height='3'></td></tr><tr><td align='center'>###NPDGrayValue###</td></tr></table>");
                SBstrGrayNPDValue.Append("<table cellpadding='0' cellspacing='0' width='100%' border='0'>");

                SBstrNPDGrayPatch.Append("<tr class='row-1' style='BACKGROUND-COLOR:#333333;COLOR:#FFF;'><td class='row-content1'>&nbsp;</td>");
                SBstrNPDGrayActual.Append("<tr class='row-1'><td class='row-content1' style='padding-left:2px'>Target</td>");

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    SBstrNPDGrayPatch.Append("<td align='center' class='row-content1'>" + Convert.ToInt64(DT.Rows[i]["GRAY"]).ToString() + "</td>");
                    SBstrNPDGrayActual.Append("<td align='center' class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Density"])) + "</td>");
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
            SBStr.Replace("###BlackNPD_GraphImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page2_BlackNPD.Jpeg"));
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
                    SBstrNPDBlackActual.Append("<td align='center' class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Density"])) + "</td>");
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


                SBStr.Replace("###DotgainActual_GraphImage###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page3_CHRT_DotgainActual.Jpeg"));
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
                    SBstrCDG.Append("<td align='center'>" + Common.Utilities.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                    SBstrMDG.Append("<td align='center'>" + Common.Utilities.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                    SBstrYDG.Append("<td align='center'>" + Common.Utilities.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                    SBstrKDG.Append("<td align='center'>" + Common.Utilities.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                }

                for (int i = 0; i < intArrDistingRec.Count; i++)
                {
                    if (intArrDistingRec[i].ToString() != "0")
                    {
                        SBstrDGPatch.Append("<td align='center'>" + intArrDistingRec[i].ToString() + "</td>");
                        DV.RowFilter = "CMYK_C = " + intArrDistingRec[i].ToString();
                        if (DV.Count > 0)
                            SBstrCDG.Append("<td align='center'>" + Common.Utilities.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                        else
                            SBstrCDG.Append("<td align='center'>&nbsp;</td>");

                        DV.RowFilter = "CMYK_M = " + intArrDistingRec[i].ToString();
                        if (DV.Count > 0)
                            SBstrMDG.Append("<td align='center'>" + Common.Utilities.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                        else
                            SBstrMDG.Append("<td align='center'>&nbsp;</td>");


                        DV.RowFilter = "CMYK_Y = " + intArrDistingRec[i].ToString();
                        if (DV.Count > 0)
                            SBstrYDG.Append("<td align='center'>" + Common.Utilities.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
                        else
                            SBstrYDG.Append("<td align='center'>&nbsp;</td>");


                        DV.RowFilter = "CMYK_K = " + intArrDistingRec[i].ToString();
                        if (DV.Count > 0)
                            SBstrKDG.Append("<td align='center'>" + Common.Utilities.DecimalToString(Convert.ToString(DV[0]["DotGain"])) + "</td>");
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

                SBStr.Replace("###DotgainDeviation_GraphImage###", "<td><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td class='summary-bg' align='center' style='background: url(##URLIMAGES##summary-title-bg.gif) repeat-x left top;'><span class='summary-title1'>Dotgain Deviation</span></td></tr><tr><td><img src='" + System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page3_DotgainDeviation.Jpeg") + "' height='330px' width='220px' alt='Dotgain Deviation' id='img11' /></td></tr></table></td>");

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

                    SBstrDotTarget.Append("<td align='center' class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Targ_DotGain"])) + "</td>");
                    SBstrDotActual.Append("<td align='center' class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["DotGain_ACT"])) + "</td>");
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
                SBStr.Replace("###DensityDeviation_GraphImage###", "<td><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td class='summary-bg' align='center' style='background: url(##URLIMAGES##summary-title-bg.gif) repeat-x left top;'><span class='summary-title1'>Density Deviation</span></td></tr><tr><td><img src='" + System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page3_DensityDeviation.Jpeg") + "' height='330px' width='220px' alt='Density Deviation' id='img13' /></td></tr></table></td>");
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

                    SBstrDenTarget.Append("<td align='center' class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Density_Targ"])) + "</td>");
                    SBstrDenActual.Append("<td align='center' class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Density"])) + "</td>");
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
                SBStr.Replace("###PrintContrastDeviation_GraphImage###", "<td><table width='100%' border='0' cellspacing='0' cellpadding='0'><tr><td class='summary-bg' align='center' style='background: url(##URLIMAGES##summary-title-bg.gif) repeat-x left top;'><span class='summary-title1'>Print Contrast Deviation</span></td></tr><tr><td><img src='" + System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page3_PrintContrastDeviation.Jpeg") + "' height='330px' width='220px' alt='Print Contrast Deviation' id='img15' /></td></tr></table></td>");
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

                    SBstrPrintTarget.Append("<td align='center' class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Print_Contrast_Tol"])) + "</td>");
                    //SBstrPrintActual.Append("<td align='center' class='row-content1'>" + Common.Utilities.DecimalToString(Convert.ToString(Formulas.DotGain.CalcPrintContrast(Convert.ToDecimal(DT.Rows[i]["D100"]), Convert.ToDecimal(DT.Rows[i]["D75"])))) + "</td>");
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

                   // string Tlbl = "<span style='background-color:" + ColorCalcuations.LabtoRGB(Convert.ToDouble(DT.Rows[i]["Target_L"]), Convert.ToDouble(DT.Rows[i]["Target_a"]), Convert.ToDouble(DT.Rows[i]["Target_b"])) + "'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                    //string Albl = "<span style='background-color:" + ColorCalcuations.LabtoRGB(Convert.ToDouble(DT.Rows[i]["Actual_L"]), Convert.ToDouble(DT.Rows[i]["Actual_a"]), Convert.ToDouble(DT.Rows[i]["Actual_b"])) + "'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>";
                   // SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + Tlbl + "&nbsp;" + Albl + "</td>");
                    SBstrTableDetail.Append("<td style='width: 7%' align='center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["DE"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + DT.Rows[i]["Sample_Name"].ToString() + "</td>");
                    SBstrTableDetail.Append("<td style='width: 7%' align='center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["CMYK_C"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 7%' align='center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["CMYK_M"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 7%' align='center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["CMYK_Y"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 7%' align='center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["CMYK_K"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Target_L"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Target_a"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Target_b"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Actual_L"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Actual_a"])) + "</td>");
                    SBstrTableDetail.Append("<td style='width: 8%' align='center'>" + Common.Utilities.DecimalToString(Convert.ToString(DT.Rows[i]["Actual_b"])) + "</td>");
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
            SBStr.Replace("##URLIMAGES##", System.Web.HttpContext.Current.Request.Url.ToString().Substring(0, System.Web.HttpContext.Current.Request.Url.ToString().LastIndexOf('/')) + "/Images/");
           // SBStr.Replace("##URLIMAGES##", WebSettings.ImageURL + "Images/");
            SBStr.Replace("##IMAGES##", strServer);
            #endregion

            return SBStr;
        }
        #endregion

    }
}
