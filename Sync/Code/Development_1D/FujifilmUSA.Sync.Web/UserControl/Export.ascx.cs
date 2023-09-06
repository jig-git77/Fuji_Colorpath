using System;
using System.Linq;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Export : BaseControl, IViewExport
    {
        #region Private Variables - Globalization        
        private const string _recDownload = "downloadText";
        private const string _recDLP = "dlpText";
        private const string _recDownloadChartNotAvailable = "downloadChartNotAvailableText";
        private const string locDownloadChartAndPrintThroughWorkflowResourceNCP = "locDownloadChartAndPrintThroughWorkflowResource1NCP";

        #endregion

        #region Construtor
        public Export()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #endregion

        private PresenterExport _presenterExport;
        public string AddMeasurement_DeleteMeasurement_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddMeasurement_DeleteMeasurement_Confirmation");
            }
        }

        protected override void OnInit(EventArgs e)
        {
            _presenterExport = new PresenterExport(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdnBaseServiceUrl.Value = Utilities.ReadConfigValue("BaseServiceURL");
                hdfCurrentCulture.Value = getCurrentCultureLanguage();
                hdnOptimizationRunID.Value = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId.ToString();
                hdfAuthToken.Value = SessionManager.Instance.CurrentUser.APIAuthKey;
                hdnCurrentDlpSubOperation.Value = ((int)SessionManager.Instance.CurrentDlpSubOperation).ToString();
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Download;
                header.InnerText = string.Format(GetResourceString(_recDownload), SessionManager.Instance.CurrentProject.ProjectName + " " + GetResourceString(_recDLP) + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Month.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Day.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Hour.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Minute.ToString("00") + ".icc");
                lbtnMeasure.Visible = (SessionManager.Instance.CurrentProject.CurrentDisplayStatus != ECurrentProjectStatus.Edit);
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D) //----Sync1D Case
                {
                    trExportViewForSync1D.Visible = true;
                    trAdvancedDownload.Visible = false;
                    drpCurveType.SelectedValue = SessionManager.Instance.CurrentProject.CurveTypeID.ToString(); //--Display last time selected value first time page load
                    hdnCurveFileName.Value = SessionManager.Instance.CurrentProject.ProjectName + " " + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Month.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Day.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Hour.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Minute.ToString("00");
                }
                else
                {
                    if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.InkOptimizationSource) //---If "Ink Optimization Source" project type, then skip Measure button and Advanced Download button (since we don't have desctination profile in this case).
                    {
                        trAdvancedDownload.Visible = false;
                        lbtnMeasure.Visible = false;
                        Div_Chart.Visible = false;
                        header.InnerText = string.Format(GetResourceString(_recDownload), SessionManager.Instance.CurrentProject.ProjectName + ".icc");
                    }
                    else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary) //---If "Spot Target Library" project type, then skip Measure button and DLP Download button (since we don't have destination profile in this case).
                    {
                        Div_DLP.Visible = false; // Hide Download DLP in optimize mode of spot project
                        if (SessionManager.Instance.CurrentOptimizeRun.Status == EOptimizeRunStatus.Certify)
                        {
                            lbtnMeasure.Visible = false;
                            trDLPDownload.Visible = false; //Hide both Download DLP and Chart in Certify mode
                            //----Don't show CMYK export button for Run-0, since there is no previous run's CMYK data to export
                            trExportCMYKLibrary.Visible = !(SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId ==
                                SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.First().OptimizeRunId);
                        }
                        locDownloadDLPAndChart.Text = "Download Chart";
                        lbtnNamedColorICC.Visible = true;
                        header.InnerText = string.Format(GetResourceString(_recDownload), SessionManager.Instance.CurrentProject.SpotTargetProject.ProjectName + ".icc");
                        locDownloadChartAndPrintThroughWorkflow.Text = GetResourceString(locDownloadChartAndPrintThroughWorkflowResourceNCP);
                        if (SessionManager.Instance.CurrentProject.SpotTargetProject.MasterProjectLicenseType == ELicenseType.CustomBCO)
                        {
                            lbtnMeasure.Visible = false;
                        }
                    }
                    else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DestinationProfile) //---If Destination Profile"
                    {
                        trDLPDownload.Visible = false;
                        Div_DLP.Visible = false; // Hide Download DLP in optimize mode of spot project
                        trDestinationProfile.Visible = true;
                        trNewBCO.Visible = true;
                        trAdvancedDownload.Visible = false; 
                        if (SessionManager.Instance.CurrentOptimizeRun.Status == EOptimizeRunStatus.Certify)
                        {
                            lbtnMeasure.Visible = false;
                            trDLPDownload.Visible = false; //Hide both Download DLP and Chart in Certify mode
                            //----Don't show CMYK export button for Run-0, since there is no previous run's CMYK data to export
                            trExportCMYKLibrary.Visible = !(SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId ==
                                SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.First().OptimizeRunId);
                        }
                        locDownloadDest.Text = "Download Destination Profile ";
                        // header.InnerText = string.Format(GetResourceString(_recDownload), SessionManager.Instance.CurrentProject.SpotTargetProject.ProjectName + ".icc");
                        locDownloadChartAndPrintThroughWorkflow.Text = GetResourceString(locDownloadChartAndPrintThroughWorkflowResourceNCP);
                        lbtnMeasure.Visible = false;

                    }
                    else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.CustomSourceProfile || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.ReProfileCustomSource) //---If "Custom Source Profile" project type, then skip Measure button and Advanced Download button (since we don't have desctination profile in this case).
                    {
                        trSourceProfDownload.Visible = true;
                        trDLPDownload.Visible = false;
                        //trAdvancedDownload.Visible = true;
                        lbtnDownloadSourceProf.Visible = true;
                        lbtnDownloadDestinationICC.Visible = false;
                        lbtnMeasure.Visible = false;
                        header.InnerText = string.Format(GetResourceString(_recDownload), SessionManager.Instance.CurrentProject.ProjectName + ".icc");
                    }
                    else  //----Sync4D or Ink Optimization case
                    {
                        trAdvancedDownload.Visible = true;
                    }
                }
                if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.SpotLibrary &&
                    (SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 ||
                    (SessionManager.Instance.CurrentProject.OptimizeRuns.First().OptimizeRunId == SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId)))
                {
                    rbtnDetlaReflistClient.Items[1].Enabled = false;
                }
            }
        }

        #region Control Methods
        protected void lbtnMeasure_Click(object sender, EventArgs e)
        {
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary)
            {
                SpotProjectId = SessionManager.Instance.CurrentProject.SpotTargetProject.SpotProjectId;
                _presenterExport.CreateOptimizeRun_SpotProject();
                if (!IsErrorOccured)
                {
                    _presenterExport.GetProjectDetail_SpotProject();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Last();
                        Response.Redirect("Spot_Measurement.aspx");
                    }
                }
            }
            else
            {
                ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                CurveTypeId = Convert.ToInt16(HD_CurveType.Value);
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                    _presenterExport.Create1DOptimizeRun();
                else
                    _presenterExport.CreateOptimizeRun();
                if (!IsErrorOccured)
                {
                    _presenterExport.GetProjectDetail();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.OptimizeRuns.Last();
                        Response.Redirect("Measurement.aspx");
                    }
                }
            }
        }

        protected void lbtnDownloadDLP_Click(object sender, EventArgs e)
        {
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                _presenterExport.Get1DDeviceLinkProfile();
            else
                _presenterExport.GetDeviceLinkProfile();
            string DLPName = SessionManager.Instance.CurrentOptimizeRun.DlpName;
            if (SessionManager.Instance.CurrentOptimizeRun.EndDateTime.HasValue)
            {
                DLPName = SessionManager.Instance.CurrentProject.ProjectName + " " + GetResourceString(_recDLP) + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Month.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Day.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Hour.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Minute.ToString("00") + ".icc";
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.InkOptimizationSource) //---If "Ink Optimization Source" / "Custom Source Profile" project type, then only Project name as DLP name.
                {
                    DLPName = SessionManager.Instance.CurrentProject.ProjectName + ".icc";
                }
            }
            if (!IsErrorOccured)
            {
                if (DeviceLinkProfile != null)
                    Utilities.DownloadFile(DeviceLinkProfile, string.Format("{0}", DLPName));
            }
        }

        protected void lbtnDownloadSourceProf_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            _presenterExport.GetSourceProfile();
            if (!IsErrorOccured)
            {
                if (SourceProfile != null)
                    Utilities.DownloadFile(SourceProfile,
                                           string.Format("{0}.icc",
                                                         SessionManager.Instance.CurrentProject.ProjectName));
            }
        }


        protected void lbtnDownloadChart_Click(object sender, EventArgs e)
        {
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary)
            {
                Wedge wedge = new Wedge();
                OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                _presenterExport.GetSpotPatchDetailsForChart();

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
                SpotProjectId = SessionManager.Instance.CurrentProject.SpotTargetProject.SpotProjectId;
                _presenterExport.GetSpotProjectChartSetting();
                obj.Configurations.NumberOfPatches = (SpotProjectChartSetting.NumOfRows * SpotProjectChartSetting.NumOfCols);
                obj.Configurations.ChartPages = SpotProjectChartSetting.Pages;
                obj.Configurations.PatchWidth = Convert.ToInt32(SpotProjectChartSetting.PatchSizeX);
                obj.Configurations.PatchHeight = Convert.ToInt32(SpotProjectChartSetting.PatchSizeY);
                obj.Configurations.ChartBorder = SpotProjectChartSetting.ChartBorder;
                obj.Configurations.ChartSizeW = Convert.ToInt32(SpotProjectChartSetting.ChartSizeX);
                obj.Configurations.ChartSizeH = Convert.ToInt32(SpotProjectChartSetting.ChartSizeY);
                obj.Configurations.RowsPerChart = SpotProjectChartSetting.RowsPerPage;
                obj.Configurations.ColumsPerChart = SpotProjectChartSetting.NumOfCols;
                obj.Configurations.Vgap = SpotProjectChartSetting.Vgap;
                obj.Configurations.Hgap = SpotProjectChartSetting.Hgap;
                obj.Configurations.GapLeval = SpotProjectChartSetting.GapLeval;
                obj.Configurations.PreviousL = SpotProjectChartSetting.PreviousL;
                obj.Configurations.ChartName = SpotProjectChartSetting.ChartName;
                obj.Configurations.ProfileName = SessionManager.Instance.CurrentDevice.DeviceName + " - " + SessionManager.Instance.CurrentProject.SpotTargetProject.ProjectName;
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
                        string tempFileName = SessionManager.Instance.CurrentProject.SpotTargetProject.ProjectName;
                        tempFileName = tempFileName.Replace(" ", "");
                        contentStream = result.Content.ReadAsStreamAsync().Result;
                        result.Content.Headers.ContentDisposition.FileName = tempFileName + "-" + DateTime.Now.ToString("MMddyyHHmm") + ".zip";
                        zipFileName = result.Content.Headers.ContentDisposition.FileName;

                        using (var filestream = File.Create(zipFilPath + zipFileName))
                        {
                            contentStream.Seek(0, SeekOrigin.Begin);
                            contentStream.CopyTo(filestream);
                        }
                        Utilities.DownloadExportFile(zipFilPath, zipFileName, zipFileName);

                        //========== Download File ==========//
                        //System.IO.FileStream fs = System.IO.File.OpenRead(zipFilPath + zipFileName);
                        //byte[] data = new byte[fs.Length];
                        //fs.Read(data, 0, (int)fs.Length);
                        //System.Web.HttpContext.Current.Response.Clear();
                        //System.Web.HttpContext.Current.Response.ClearHeaders();

                        //System.Web.HttpContext.Current.Response.AddHeader("Content-Type", "application/*");
                        //System.Web.HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=\"" + zipFileName + "\"");
                        //System.Web.HttpContext.Current.Response.BinaryWrite(data);
                        //System.Web.HttpContext.Current.Response.Flush();
                        //if (File.Exists(zipFilPath + zipFileName))
                        //    File.Delete(zipFilPath + zipFileName);
                        //System.Web.HttpContext.Current.Response.End();                        
                    }
                }
            }
            else
            {
                //ReferenceChartTypeId = SessionManager.Instance.CurrentOptimizeRun.ReferenceChartType.Key;  
                //Here we are considering optimization chart as export will be visible only once DLP is created (i.e. success of Run0)
                ReferenceChartTypeId = SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Key;
                _presenterExport.GetChartPreview();
                if (!IsErrorOccured)
                {
                    if (ChartPreview != null)
                        Utilities.DownloadFile(ChartPreview, string.Format("{0}.pdf", SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Value), ".pdf");
                    else
                        fujiStatusbar.DisplayMessage(GetResourceString(_recDownloadChartNotAvailable), true);
                }
                else
                {
                    fujiStatusbar.DisplayMessage(GetResourceString(_recDownloadChartNotAvailable), true);
                }
            }
        }

        protected void lbtnDownloadDestinationICC_Click(object sender, EventArgs e)
        {
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary)
            {
                ProjectId = SessionManager.Instance.CurrentOptimizeRun.ProjectId;
                _presenterExport.GetSpotDestinationProfile();
                if (!IsErrorOccured)
                {
                    if (SourceProfile != null)
                        Utilities.DownloadFile(SourceProfile,
                                               string.Format("{0}.icc",
                                                             SessionManager.Instance.CurrentProject.SpotTargetProject.ProjectName));
                }
            }
            else
            {
                ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                _presenterExport.GetDestinationProfile();
                if (!IsErrorOccured)
                {
                    if (SourceProfile != null)
                        Utilities.DownloadFile(SourceProfile,
                                               string.Format("{0}.icc",
                                                             SessionManager.Instance.CurrentProject.ProjectName));
                }
            }
        }

        protected void lbtnNamedColorICC_Click(object sender, EventArgs e)
        {
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary)
            {
                OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                _presenterExport.GetNamedColorProfile();
                if (!IsErrorOccured)
                {
                    if (SourceProfile != null)
                        Utilities.DownloadFile(SourceProfile,
                                               string.Format("{0}.icc",
                                                             SessionManager.Instance.CurrentProject.SpotTargetProject.ProjectName + " Named Color Profile"));
                }
            }
        }

        protected void lnkGoToDeviceList_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentBranchName = null;
            SessionManager.Instance.CurrentDevice = null;
            SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.None;
            SessionManager.Instance.CurrentProject = null;
            SessionManager.Instance.CurrentOptimizeRun = null;
            Response.Redirect("~/Index.aspx");
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
        public string SpotProjectIDs { get; set; }

        #endregion

        protected void btnCreateBCO_Click(object sender, EventArgs e)
        {
            int ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UniqId", "window.parent.location='RedirectFromProjectList.aspx?RedirectType='AddSpotProject"& ", true);
            string url = "RedirectFromProjectList.aspx?RedirectType=AddSpotProject&ProjectID=" + ProjectId.ToString();
            //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UniqId", "window.parent.location='RedirectFromProjectList.aspx?RedirectType=\"AddSpotProject\"&ProjectID='"+ProjectId+"';", true);
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "UniqId", "window.parent.location='" + url + "';", true);
        }
    }
}