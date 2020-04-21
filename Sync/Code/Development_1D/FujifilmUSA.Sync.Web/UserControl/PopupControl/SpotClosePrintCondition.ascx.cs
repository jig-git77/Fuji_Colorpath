using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl.PopupControl
{
    public partial class SpotClosePrintCondition : BaseControl, IViewClosePrintCondition, IViewExport
    {
        #region "Declare Variable"
        private PresenterClosePrintCondition _presenterClosePrintCondition;
        private PresenterExport _presenterExport;
        private const string _recprintOptionsText = "printOptionsText";
        private const string _recoptimizeCompleteText = "optimizeCompleteText";
        private const string _recnoNCPText = "noNCPText";
        private const string _recnoSourceText = "noSourceText";
        private const string _recspotprintOptionsText = "spotprintOptionsText";

        #endregion

        #region "Constructor"
        public SpotClosePrintCondition()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #endregion

        #region Property
        private ELicenseType CurrentLicenseType
        {
            get
            {
                if (Request.QueryString["LicenseTypeId"] != null && Request.QueryString["LicenseTypeId"].Length > 0)
                    return (ELicenseType)Convert.ToInt32(Request.QueryString["LicenseTypeId"]);
                else
                    return ELicenseType.None;
            }
        }
        #endregion

        #region "Event"
        protected override void OnInit(EventArgs e)
        {
            _presenterClosePrintCondition = new PresenterClosePrintCondition(this);
            _presenterExport = new PresenterExport(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //lblClosePrintConditionMessage.Text = string.Format(GetResourceString(_recprintOptionsText), Request.QueryString["ProjectName"]);
                lblClosePrintConditionMessage.Text = GetResourceString(_recspotprintOptionsText);
                hdfServicePath.Value = Utilities.ReadConfigValue("BaseServiceURL");
                //lbtnDownloadChart.Attributes.Add("onclick", "javascript: return DownloadChart(" + Request.QueryString["ProjectId"].ToString() + ")");
            }
        }


        protected void lbtnReOptimize_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ProjectId"] != null)
            {
                SpotProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
                _presenterClosePrintCondition.CreateOptimizeRun_SpotProject();
                if (!IsErrorOccured)
                {
                    _presenterClosePrintCondition.GetProjectDetail_SpotProject();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Last();
                        SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Run;
                        Utilities.PopupWindowsRedirect(this, "Spot_Measurement.aspx");
                        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UniqId", "window.parent.location='Spot_Measurement.aspx';", true);
                    }
                }
            }
        }
        protected void lbtnViewLastAnalysis_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ProjectId"] != null)
            {
                SpotProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
                _presenterClosePrintCondition.GetProjectDetail_SpotProject();
                if (!IsErrorOccured)
                {
                    SessionManager.Instance.CurrentProject = Project;
                    SessionManager.Instance.CurrentOptimizeRun = Project.SpotTargetProject.OptimizeRuns.Last();
                    SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.SpotLibrary;
                    SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
                    Utilities.PopupWindowsRedirect(this, "Analyze_SpotColor.aspx");
                }
            }
        }

        protected void lbtnDownloadChart_Click(object sender, EventArgs e)
        {
            SpotProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
            Wedge wedge = new Wedge();
            _presenterExport.GetSpotPatchDetailsForChartBySpotProject();

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
                        Cie_L = Convert.ToDouble(dr["CIELAB_L"]),
                        Cie_a = Convert.ToDouble(dr["CIELAB_a"]),
                        Cie_b = Convert.ToDouble(dr["CIELAB_b"]),
                        Spot1 = 0
                    };
                    listPatch.Add(patch);
                }
            }
            wedge.listPatch = listPatch;
            IccFileParams obj = new IccFileParams();

            //get spot project chart setting
            
            _presenterExport.GetSpotProjectChartSetting();
            obj.Configurations.NumberOfPatches = (SpotProjectChartSetting.Pages * SpotProjectChartSetting.NumOfRows * SpotProjectChartSetting.NumOfCols);
            obj.Configurations.ChartPages = SpotProjectChartSetting.Pages;
            obj.Configurations.PatchWidth = Convert.ToInt32(SpotProjectChartSetting.PatchSizeX);
            obj.Configurations.PatchHeight = Convert.ToInt32(SpotProjectChartSetting.PatchSizeY);
            obj.Configurations.ChartBorder = SpotProjectChartSetting.ChartBorder;
            obj.Configurations.ChartSizeW = Convert.ToInt32(SpotProjectChartSetting.ChartSizeX);
            obj.Configurations.ChartSizeH = Convert.ToInt32(SpotProjectChartSetting.ChartSizeY);
            obj.Configurations.RowsPerChart = SpotProjectChartSetting.NumOfRows;
            obj.Configurations.ColumsPerChart = SpotProjectChartSetting.NumOfCols;
            obj.Configurations.Vgap = SpotProjectChartSetting.Vgap;
            obj.Configurations.Hgap = SpotProjectChartSetting.Hgap;
            obj.Configurations.GapLeval = SpotProjectChartSetting.GapLeval;
            obj.Configurations.PreviousL = SpotProjectChartSetting.PreviousL;
            obj.Configurations.ChartName = SpotProjectChartSetting.ChartName;
            obj.Patches = wedge.listPatch;

            string BaseUri = Utilities.ReadConfigValue("BaseCpSpotChartPdfServiceURL");
            string methodUrl = "api/Chart/GenerateFiles";

            string zipFilPath = System.Web.HttpContext.Current.Server.MapPath("UploadFiles/ChartExports/");
            if (!Directory.Exists(zipFilPath))
                Directory.CreateDirectory(zipFilPath);
            using (HttpClient httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(obj), null, "application/json");
                var result = httpClient.PostAsync(BaseUri + methodUrl, content).Result;
                Stream contentStream = null;
                string zipFileName = "";
                if (result.IsSuccessStatusCode)
                {
                    string prjName = Convert.ToString(Request.QueryString["projectName"]);
                    contentStream = result.Content.ReadAsStreamAsync().Result;
                    result.Content.Headers.ContentDisposition.FileName = prjName + "-" + DateTime.Now.ToString("MMddyyHHmm") + ".zip";
                    zipFileName = result.Content.Headers.ContentDisposition.FileName;

                    using (var filestream = File.Create(zipFilPath + zipFileName))
                    {
                        contentStream.Seek(0, SeekOrigin.Begin);
                        contentStream.CopyTo(filestream);
                    }
                    Utilities.DownloadExportFile(zipFilPath, zipFileName, zipFileName);
                }
            }
        }

        #endregion

        #region IViewClosePrintCondition Members

        public int ProjectId { get; set; }

        public byte[] CurrentDlp { get; set; }

        public Entity.Project Project { get; set; }

        public string DeviceName { get; set; }
        public int SpotProjectId { get; set; }


        #endregion

        #region IViewExport Members
        public int OptimizeRunId { get; set; }
        public short ReferenceChartTypeId { get; set; }
        public byte[] DeviceLinkProfile { get; set; }
        public byte[] ChartPreview { get; set; }
        public byte[] SourceProfile { get; set; }
        public short CurveTypeId { get; set; }
        public FujifilmUSA.Sync.Business.Messages.CurveDataResponse CurveData { get; set; }
        public DataTable dtPatchDetails { get; set; }
        public SpotProjectChartSetting SpotProjectChartSetting { get; set; }
        public string SpotProjectIDs { get; set; }
        #endregion
    }
}