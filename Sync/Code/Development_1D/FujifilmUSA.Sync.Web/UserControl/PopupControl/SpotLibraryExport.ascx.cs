using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl.PopupControl
{
    public partial class SpotLibraryExport : BaseControl, IViewExport
    {
        #region Private Variables - Globalization     
        private PresenterExport _presenterExport;
        private const string _recSelectText = "selectText";
        #endregion
        #region Construtor
        public SpotLibraryExport()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #endregion

        #region Page Methods
        protected override void OnInit(EventArgs e)
        {
            _presenterExport = new PresenterExport(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        #endregion

        #region Control Methods
        protected void lbtnExportCMYK_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CMYKFile")))
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CMYKFile"));

            //==========  Get Data Table and Bind ==========//
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary)
            {
                OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                _presenterExport.GetSpotPatchDetailsForCMYKExport();

                List<Patch> listPatch = new List<Patch>();
                if (dtPatchDetails != null && dtPatchDetails.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtPatchDetails.Rows)
                    {
                        Patch patch = new Patch()
                        {
                            PatchID = Convert.ToInt32(dr["SpotID"]),
                            ColorName = Convert.ToString(dr["SpotName"]),
                            Cmyk_C = Convert.ToDouble(dr["CMYK_C"]),
                            Cmyk_M = Convert.ToDouble(dr["CMYK_M"]),
                            Cmyk_Y = Convert.ToDouble(dr["CMYK_Y"]),
                            Cmyk_K = Convert.ToDouble(dr["CMYK_K"]),
                            Cie_L = Convert.ToDouble(dr["TargetL"]),
                            Cie_a = Convert.ToDouble(dr["Targeta"]),
                            Cie_b = Convert.ToDouble(dr["Targetb"]),
                            Spot1 = 0
                        };
                        listPatch.Add(patch);
                    }
                }

                if (cmbCMYKLibrary.SelectedValue.ToString() == "1")
                    ExportColderaFile(listPatch, txtFileName.Text);
                else if (cmbCMYKLibrary.SelectedValue.ToString() == "2")
                    ExportColorgateFile(listPatch, txtFileName.Text);
                else
                    ExportColdera2File(listPatch, txtFileName.Text);

            }
        }

        protected void cmbCMYKLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCMYKLibrary.SelectedValue.ToString() == "1")
            {
                trCalderaRow1.Visible = true;
                trCalderaRow2.Visible = true;
                trCaldera2Row1.Visible = false;
            }
            else if (cmbCMYKLibrary.SelectedValue.ToString() == "3")
            {
                trCalderaRow1.Visible = true;
                trCalderaRow2.Visible = true;
                trCaldera2Row1.Visible = true;
            }
            else
            {
                trCalderaRow1.Visible = false;
                trCalderaRow2.Visible = false;
                trCaldera2Row1.Visible = false;
            }

        }
        #endregion

        #region Methods

        #region Create file and download .xml file for Caldera CMYK type
        private void ExportColderaFile(List<Patch> listPatch, string CMYKFileName)
        {
            string SpotName_Parameter = "";

            System.IO.StreamWriter sw;
            sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CMYKFile/") + CMYKFileName + ".xml");

            //--------Start: Header------------//
            sw.WriteLine("<MediaDeviceColors>");
            sw.WriteLine("<ColorList server=" + "\"" + txtServer.Text + "\"" + " model=" + "\"" + txtModel.Text + "\"" + " media=" + "\"" + txtMedia.Text + "\"" + " resolution=" + "\"" + txtResolution.Text + "\"" + " mode=" + "\"" + "CMYK" + "\"" + ">");
            //--------End: Header--------------//

            for (int i = 0; i < listPatch.Count; i++)
            {
                SpotName_Parameter = listPatch[i].ColorName.Replace("_", " ");
                sw.WriteLine("<Color>" + SpotName_Parameter + " : " + "CMYK " + listPatch[i].Cmyk_C.ToString("N1") + " " + listPatch[i].Cmyk_M.ToString("N1") + " " + listPatch[i].Cmyk_Y.ToString("N1") + " " + listPatch[i].Cmyk_K.ToString("N1") + " </Color>");
            }
            //--------Start: Footer------------//
            sw.WriteLine("</ColorList>");
            sw.WriteLine("</MediaDeviceColors>");
            //--------End: Footer--------------//

            sw.Close();

            Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CMYKFile/"), CMYKFileName + ".xml", CMYKFileName + ".xml");

        }
        #endregion

        #region Create file and download .cct file for Colorgate CMYK type
        private void ExportColorgateFile(List<Patch> listPatch, string CMYKFileName)
        {
            string SpotName_Parameter = "";

            System.IO.StreamWriter sw;
            sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CMYKFile/") + CMYKFileName + ".cct");

            //--------Start: Header------------//
            sw.Write("<?xml version=" + "\"1.0" + "\"" + " encoding=" + "\"UTF-8" + "\"?>");
            sw.Write("<ColorTable xmlns=" + "\"urn:schemas-colorgate-com:colortable" + "\"" + " Name=" + "\"" + CMYKFileName + "\"" + " Version=" + "\"2" + "\">");
            //--------End: Header--------------//
            
            for (int i = 0; i < listPatch.Count; i++)
            {
                SpotName_Parameter = listPatch[i].ColorName.Replace("_", " ");
                sw.Write("<ColorEntry Name=" + "\"" + SpotName_Parameter + "\"" + " ReplacementEnabled=" + "\"true" + "\"" + " ReplacementType=" + "\"23" + "\">");
                sw.Write("<ColorIn ColorModel=" + "\"Spot" + "\"/>");
                sw.Write("<ColorOut ColorModel=" + "\"Custom" + "\"" + " CustomColorModelId=" + "\"1" + "\"" + " ColorAliasName=" + "\"" + "\">");
                sw.Write("<ComponentValue>" + listPatch[i].Cmyk_C.ToString("N0") + "</ComponentValue>");
                sw.Write("<ComponentValue>" + listPatch[i].Cmyk_M.ToString("N0") + "</ComponentValue>");
                sw.Write("<ComponentValue>" + listPatch[i].Cmyk_Y.ToString("N0") + "</ComponentValue>");
                sw.Write("<ComponentValue>" + listPatch[i].Cmyk_K.ToString("N0") + "</ComponentValue>");
                sw.Write("</ColorOut>");
                sw.Write("</ColorEntry>");
            }
            //--------Start: Footer------------//
            sw.Write("</ColorTable>");
            //--------End: Footer--------------//

            sw.Close();

            Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CMYKFile/"), CMYKFileName + ".cct", CMYKFileName + ".cct");

        }


        #endregion

        #region Create file and download .xml file for Caldera2 CMYK type
        private void ExportColdera2File(List<Patch> listPatch, string CMYKFileName)
        {
            string SpotName_Parameter = "";

            System.IO.StreamWriter sw;
            sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CMYKFile/") + CMYKFileName + ".xml");

            //--------Start: Header------------//
            sw.WriteLine("<MediaDeviceColors>");
            sw.WriteLine("<ColorList mode=" + "\"" + txtMode.Text + "\"" + " quality=" + "\"" + txtQuality.Text + "\"" + " server=" + "\"" + txtServer.Text + "\"" + " model=" + "\"" + txtModel.Text + "\"" + " media=" + "\"" + txtMedia.Text + "\"" + " resolution=" + "\"" + txtResolution.Text + "\"" + ">");
            //--------End: Header--------------//

            for (int i = 0; i < listPatch.Count; i++)
            {
                SpotName_Parameter = listPatch[i].ColorName.Replace("_", " ");
                sw.WriteLine("<Color>" + SpotName_Parameter + " : " + "CMYK " + listPatch[i].Cmyk_C.ToString("N1") + " " + listPatch[i].Cmyk_M.ToString("N1") + " " + listPatch[i].Cmyk_Y.ToString("N1") + " " + listPatch[i].Cmyk_K.ToString("N1") + " </Color>");
            }
            //--------Start: Footer------------//
            sw.WriteLine("</ColorList>");
            sw.WriteLine("</MediaDeviceColors>");
            //--------End: Footer--------------//

            sw.Close();

            Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CMYKFile/"), CMYKFileName + ".xml", CMYKFileName + ".xml");

        }
        #endregion

        #region Create file and download .dbd file for CMYK values
        //private void ExportCMYK_DBDFile(List<Patch> listPatch, string CMYKFileName)
        //{
        //    //string CMYKFileName = SessionManager.Instance.CurrentProject.ProjectName + " " + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Day.ToString("00") + System.DateTime.Now.Hour.ToString("00") + System.DateTime.Now.Minute.ToString("00");

        //    string SpotName_Header, SpotName_Parameter;
        //    System.IO.StreamWriter sw;
        //    sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CMYKFile/") + CMYKFileName + "_CMYKFile.dbd");

        //    for (int i = 0; i < listPatch.Count; i++)
        //    {
        //        SpotName_Header = listPatch[i].ColorName.Replace("_", "#20");
        //        SpotName_Parameter = listPatch[i].ColorName.Replace("_", " ");

        //        sw.WriteLine("%%N:/Proforma/ColorPools/2/ColorPool/" + SpotName_Header);
        //        sw.WriteLine("Color_1:OBJECT Color");
        //        sw.WriteLine("{");
        //        sw.WriteLine("\tName:string = " + "\"" + SpotName_Parameter + "\"");
        //        sw.WriteLine("\tFUJI" + "\\" + "PrintMode:string = " + "\"LargeGumatMerge\";");
        //        sw.WriteLine("\tColorType:string = " + "\"Normal\";");
        //        sw.WriteLine("\tNeutralDensity:double = " + String.Format("{0,12:e15}", GetNdensity(listPatch[i].Cie_L)) + ";");
        //        sw.WriteLine("\tTempLab:double[3] = " + String.Format("{0,12:e15}", listPatch[i].Cie_L) + "," + String.Format("{0,12:e15}", listPatch[i].Cie_a) + "," + String.Format("{0,12:e15}", listPatch[i].Cie_b) + ";");
        //        sw.WriteLine("\tCMYK:double[4] = " + String.Format("{0,12:e15}", listPatch[i].Cmyk_C) + "," + String.Format("{0,12:e15}", listPatch[i].Cmyk_M) + "," + String.Format("{0,12:e15}", listPatch[i].Cmyk_Y) + "," + String.Format("{0,12:e15}", listPatch[i].Cmyk_K) + ";");
        //        sw.WriteLine("\tHideCMYK:string = " + "\"ShowAll\";");
        //        sw.WriteLine("\tHideLab:string = " + "\"ShowAll\";");
        //        sw.WriteLine("\tRawName:string = " + "\"\";");
        //        sw.WriteLine("}");
        //        sw.WriteLine("");
        //        sw.WriteLine("%%E:");
        //    }
        //    //-----Footer portion of file------------//
        //    sw.WriteLine("%%N:/Proforma/ColorPools/2/ColorantAliasRules");
        //    sw.WriteLine("");
        //    sw.WriteLine("%%E:");
        //    sw.WriteLine("%%N:/Proforma/ColorPools/2/Info");
        //    sw.WriteLine("FUJI" + "\\" + "ColorPoolInfo_1:OBJECT FUJI" + "\\" + "ColorPoolInfo");
        //    sw.WriteLine("{");
        //    sw.WriteLine("\tName:string = " + "\"CustomSpotLib\";");
        //    sw.WriteLine("\tComment:string = " + "\" \";");
        //    sw.WriteLine("}");
        //    sw.WriteLine("");
        //    sw.WriteLine("%%E:");
        //    //--------End: Footer-------------------//

        //    sw.Close();

        //    Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CMYKFile/"), CMYKFileName + "_CMYKFile.dbd", CMYKFileName + "_CMYKFile.dbd");

        //}
        #endregion


        //Get density of LabL to export CMYK values
        double GetNdensity(double Lab_L)
        {
            double var_Y;
            if ((Lab_L > 8))
            {
                var_Y = (100 * Math.Pow(((Lab_L + 16) / 116), 3));
            }
            else
            {
                var_Y = (100 * ((108 / 841) * (((Lab_L + 16) / 116) - (4 / 29))));
            }

            if ((var_Y > 0))
            {
                return (0.13353 + Math.Log10((100 / var_Y)));
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region IViewExport Members
        public int ProjectId { get; set; }
        public int OptimizeRunId { get; set; }
        public byte[] DeviceLinkProfile { get; set; }
        public byte[] ChartPreview { get; set; }
        public byte[] SourceProfile { get; set; }
        public Project Project { get; set; }
        public short ReferenceChartTypeId { get; set; }
        public short CurveTypeId { get; set; }
        public Business.Messages.CurveDataResponse CurveData { get; set; }
        public int SpotProjectId { get; set; }
        public DataTable dtPatchDetails { get; set; }
        public SpotProjectChartSetting SpotProjectChartSetting { get; set; }
        #endregion

        
    }
}