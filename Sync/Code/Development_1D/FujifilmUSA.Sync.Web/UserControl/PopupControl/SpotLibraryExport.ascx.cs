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
    public partial class SpotLibraryExport :  BaseControl, IViewExport
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
                {
                    ExportColradFile(listPatch, txtFileName.Text);
                }
                else
                {
                    ExportColorgateFile(listPatch, txtFileName.Text);
                } 
               
            }
        }
        #endregion

        #region Methods
        #region Create file and download .xml file for Caldera CMYK type
        private void ExportColradFile(List<Patch> listPatch, string CMYKFileName)
        {
            string SpotName_Header, SpotName_Parameter = "";

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
            sw.WriteLine("<?xml version=" + "\"1.0" + "\"" + " encoding=" + "\"UTF-8" + "\"?>");
            sw.WriteLine("<ColorTable xmlns=" + "\"urn:schemas-colorgate-com:colortable"+ "\"" + " Name=" + "\"CustomSpotLib_colorgate" + "\"" + " Version=" + "\"2" + ">");
            //--------End: Header--------------//

            for (int i = 0; i < listPatch.Count; i++)
            {
                SpotName_Parameter = listPatch[i].ColorName.Replace("_", " ");
                sw.WriteLine("<ColorEntry Name=" + "\"" + SpotName_Parameter + "\"" + " ReplacementEnabled=" + "\"true" + "\"" + " ReplacementType=" + "\"23" + "\">");
                sw.WriteLine("<ColorIn ColorModel=" + "\"Spot"  + "\"/>");
                sw.WriteLine("<ColorOut ColorModel=" + "\"Custom" + "\"" + " CustomColorModelId=" + "\"1" + "\"" + " ColorAliasName=" + "\"" + "\">");
                sw.WriteLine("<ComponentValue>" + listPatch[i].Cmyk_C.ToString("N1") + "</ComponentValue>");
                sw.WriteLine("<ComponentValue>" + listPatch[i].Cmyk_M.ToString("N1") + "</ComponentValue>");
                sw.WriteLine("<ComponentValue>" + listPatch[i].Cmyk_Y.ToString("N1") + "</ComponentValue>");
                sw.WriteLine("<ComponentValue>" + listPatch[i].Cmyk_K.ToString("N1") + "</ComponentValue>");
                sw.WriteLine("</ColorOut>");
                sw.WriteLine("</ColorEntry>");                
            }
            //--------Start: Footer------------//
            sw.WriteLine("</ColorTable>");
            //--------End: Footer--------------//

            sw.Close();

            Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CMYKFile/"), CMYKFileName + ".cct", CMYKFileName + ".cct");

        }
        #endregion

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

        protected void cmbCMYKLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCMYKLibrary.SelectedValue.ToString() == "1")
            {
                trColradRow1.Visible = true;
                trColradRow2.Visible = true;
            }
            else
            {
                trColradRow1.Visible = false;
                trColradRow2.Visible = false;
            }
            
        }
    }
}