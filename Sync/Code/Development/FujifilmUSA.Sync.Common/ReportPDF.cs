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
        
            string strJobStatus = "";

            DataTable DT = new DataTable();

            SBStr.Replace("#csspath#", CSSPath);

            // === Table 0 = "JobDetail" : Replace Job Detail In HTML Tamplate ==== //    
            #region ==== Table = 0,  Job Detail  ====
            if (DS_Common.Tables[0] != null && DS_Common.Tables[0].Rows.Count > 0)
            {
                DT = DS_Common.Tables[0];              

                //string Substrate = Convert.ToString(DT.Rows[0]["Substrate"]).Trim();
                //string Ink = Convert.ToString(DT.Rows[0]["Ink"]).Trim();
                //string Location = Convert.ToString(DT.Rows[0]["CompanyName"]).Trim();

                //Substrate = (Substrate.Trim() != "" ? Substrate.Trim() + (Convert.ToString(DT.Rows[0]["Substrate_SKU"]).Trim() != "" ? " - " + Convert.ToString(DT.Rows[0]["Substrate_SKU"]) : "") : (Convert.ToString(DT.Rows[0]["Substrate_SKU"]).Trim() != "" ? Convert.ToString(DT.Rows[0]["Substrate_SKU"]) : ""));
                //Substrate = (Substrate.Trim() != "" ? Substrate.Trim() + (Convert.ToString(DT.Rows[0]["Substrate_Batch"]).Trim() != "" ? " - " + Convert.ToString(DT.Rows[0]["Substrate_Batch"]) : "") : (Convert.ToString(DT.Rows[0]["Substrate_Batch"]).Trim() != "" ? Convert.ToString(DT.Rows[0]["Substrate_Batch"]) : ""));

                //Ink = (Ink.Trim() != "" ? Ink.Trim() + (Convert.ToString(DT.Rows[0]["Ink_SKU"]).Trim() != "" ? " - " + Convert.ToString(DT.Rows[0]["Ink_SKU"]) : "") : (Convert.ToString(DT.Rows[0]["Ink_SKU"]).Trim() != "" ? Convert.ToString(DT.Rows[0]["Ink_SKU"]) : ""));
                //Ink = (Ink.Trim() != "" ? Ink.Trim() + (Convert.ToString(DT.Rows[0]["Ink_Batch"]).Trim() != "" ? " - " + Convert.ToString(DT.Rows[0]["Ink_Batch"]) : "") : (Convert.ToString(DT.Rows[0]["Ink_Batch"]).Trim() != "" ? Convert.ToString(DT.Rows[0]["Ink_Batch"]) : ""));

                //Location = (Location.Trim() != "" ? Location.Trim() + (Convert.ToString(DT.Rows[0]["BranchName"]).Trim() != "" ? " - " + Convert.ToString(DT.Rows[0]["BranchName"]) : "") : (Convert.ToString(DT.Rows[0]["BranchName"]).Trim() != "" ? Convert.ToString(DT.Rows[0]["BranchName"]) : ""));
                //Location = (Location.Trim() != "" ? Location.Trim() + (Convert.ToString(DT.Rows[0]["DongleDescription"]).Trim() != "" ? " - " + Convert.ToString(DT.Rows[0]["DongleDescription"]) : "") : (Convert.ToString(DT.Rows[0]["DongleDescription"]).Trim() != "" ? Convert.ToString(DT.Rows[0]["DongleDescription"]) : ""));
              
                //SBStr.Replace("###JobName###", Convert.ToString(DT.Rows[0]["Job"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["Job"]).Trim());
                //SBStr.Replace("###Device###", Convert.ToString(DT.Rows[0]["Device"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["Device"]).Trim());
                //SBStr.Replace("###TimeStamp###", Common.Utilities.GetDateByCultures(Convert.ToDateTime(DT.Rows[0]["CreatedDT"])));
                //SBStr.Replace("###PrintTarget###", Convert.ToString(DT.Rows[0]["PrintTarget"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["PrintTarget"]).Trim());
                //SBStr.Replace("###Substrate###", Substrate.Trim() == "" ? "&nbsp;" : Substrate.Trim());
                //SBStr.Replace("###Ink###", Ink.Trim() == "" ? "&nbsp;" : Ink.Trim());
                //SBStr.Replace("###Location###", Location.Trim() == "" ? "&nbsp;" : Location.Trim());
                //SBStr.Replace("###Customer###", Convert.ToString(DT.Rows[0]["Customer"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["Customer"]).Trim());
                //SBStr.Replace("###Information###", Convert.ToString(DT.Rows[0]["Info"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["Info"]).Trim());
                //SBStr.Replace("###DeltaEFormula###", Convert.ToString(DT.Rows[0]["DeltaEFormula"]).Trim() == "" ? "&nbsp;" : Convert.ToString(DT.Rows[0]["DeltaEFormula"]).Trim());
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
            #region ==== Target Vs Actual AB Plot ====
            SBStr.Replace("###TargetVersusActualAB###", System.Web.HttpContext.Current.Server.MapPath("UploadFiles/temp/" + SessionManager.Instance.CurrentUser.UserId + "_SingleMeasurement_Page1_TargetVersusActualAB.Jpeg"));
            #endregion

            // === Table 2 = Chart of Summary Delta Value Graph Image And Table Value Ploted === //          
            #region ==== Summary Delta Value Graph Image ====
            DT = null;
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
            #region ==== Summary Delta E Value Graph Image ====
            DT = null;
            StringBuilder SBstrDeltaEValue = new StringBuilder();
            if (DT != null && DT.Rows.Count > 0)
            {
                string strStatusImg = "";

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                   
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
