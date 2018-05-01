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
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Download;
                header.InnerText = string.Format(GetResourceString(_recDownload), SessionManager.Instance.CurrentProject.ProjectName + " " + GetResourceString(_recDLP) + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Month.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Day.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Hour.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Minute.ToString("00") + ".icc");
                lbtnMeasure.Visible = (SessionManager.Instance.CurrentProject.CurrentDisplayStatus != ECurrentProjectStatus.Edit);
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D) //----Sync1D Case
                {
                    trExportViewForSync1D.Visible = true;
                    trAdvancedDownload.Visible = false;
                    drpCurveType.SelectedValue = SessionManager.Instance.CurrentProject.CurveTypeID.ToString(); //--Display last time selected value first time page load
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
                    }
                    else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.CustomSourceProfile || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.ReProfileCustomSource) //---If "Custom Source Profile" project type, then skip Measure button and Advanced Download button (since we don't have desctination profile in this case).
                    {
                        trSourceProfDownload.Visible = true;
                        trDLPDownload.Visible = false;
                        trAdvancedDownload.Visible = false;
                        lbtnMeasure.Visible = false;
                        header.InnerText = string.Format(GetResourceString(_recDownload), SessionManager.Instance.CurrentProject.ProjectName + ".icc");
                    }
                    else  //----Sync4D or Ink Optimization case
                    {
                        trAdvancedDownload.Visible = true;
                    }
                }
                if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.SpotLibrary && SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1)
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

                //if (dtPatchDetails.Rows.Count == 1872)
                //{
                obj.Configurations.NumberOfPatches = 1872;
                obj.Configurations.ChartPages = 3;
                obj.Configurations.PatchWidth = 27;
                obj.Configurations.PatchHeight = 27;
                obj.Configurations.ChartBorder = 40;
                obj.Configurations.ChartSizeW = 0;
                obj.Configurations.ChartSizeH = 0;
                obj.Configurations.RowsPerChart = 24;
                obj.Configurations.ColumsPerChart = 26;
                obj.Configurations.Vgap = 2;
                obj.Configurations.Hgap = 0;
                obj.Configurations.GapLeval = 75;
                obj.Configurations.PreviousL = 0;
                obj.Configurations.ChartName = "Pantone Test Chart";
                obj.Patches = wedge.listPatch;
                //}

                //else if (dtPatchDetails.Rows.Count == 48)
                //{
                //    obj.Configurations.NumberOfPatches = 48;
                //    obj.Configurations.ChartPages = 1;
                //    obj.Configurations.PatchWidth = 28;
                //    obj.Configurations.PatchHeight = 28;
                //    obj.Configurations.ChartBorder = 40;
                //    obj.Configurations.ChartSizeW = 0;
                //    obj.Configurations.ChartSizeH = 0;
                //    obj.Configurations.RowsPerChart = 2;
                //    obj.Configurations.ColumsPerChart = 24;
                //    obj.Configurations.Vgap = 2;
                //    obj.Configurations.Hgap = 2;
                //    obj.Configurations.GapLeval = 75;
                //    obj.Configurations.PreviousL = 0;
                //    obj.Configurations.ChartName = "Spot Color Test Chart";
                //    obj.Patches = wedge.listPatch;

                //}

                string BaseUri = Utilities.ReadConfigValue("BaseCpSpotChartPdfServiceURL");
                string methodUrl = "api/Chart/GenerateFiles";
                using (HttpClient httpClient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(obj), null, "application/json");
                    var result = httpClient.PostAsync(BaseUri + methodUrl, content).Result;
                    var resultString = result.Content.ReadAsStringAsync().Result;
                    var response = JsonConvert.DeserializeObject<BaseResponse<FileResponse>>(resultString);

                    WebClient client = new WebClient();
                    client.DownloadFile(response.Data.FilePath, System.Web.HttpContext.Current.Server.MapPath("UploadFiles/") + response.Data.FileName);

                    Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/"), response.Data.FileName, response.Data.FileName);
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

                //string CurveFileName = SessionManager.Instance.CurrentProject.ProjectName + " " + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Month.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Day.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Hour.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Minute.ToString("00");
                string CMYKFileName = SessionManager.Instance.CurrentProject.ProjectName + " " + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Day.ToString("00") + System.DateTime.Now.Hour.ToString("00") + System.DateTime.Now.Minute.ToString("00");

                string SpotName_Header, SpotName_Parameter = "";

                #region Create file and download txt file for CMYK values
                System.IO.StreamWriter sw;
                sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CMYKFile/") + CMYKFileName + "_CMYKFile.dbd");

                for (int i = 0; i < listPatch.Count; i++)
                {
                    SpotName_Header = listPatch[i].ColorName.Replace("_", "#20");
                    SpotName_Parameter = listPatch[i].ColorName.Replace("_", " ");

                    sw.WriteLine("%%N:/Proforma/ColorPools/2/ColorPool/" + SpotName_Header);
                    sw.WriteLine("Color_1:OBJECT Color");
                    sw.WriteLine("{");
                    sw.WriteLine("\tName:string = " + "\"" + SpotName_Parameter + "\"");
                    sw.WriteLine("\tFUJI" + "\\" + "PrintMode:string = " + "\"LargeGumatMerge\";");
                    sw.WriteLine("\tColorType:string = " + "\"Normal\";");
                    sw.WriteLine("\tNeutralDensity:double = " + String.Format("{0,12:e15}", GetNdensity(listPatch[i].Cie_L)) + ";");
                    sw.WriteLine("\tTempLab:double[3] = " + String.Format("{0,12:e15}", listPatch[i].Cie_L) + "," + String.Format("{0,12:e15}", listPatch[i].Cie_a) + "," + String.Format("{0,12:e15}", listPatch[i].Cie_b) + ";");
                    sw.WriteLine("\tCMYK:double[4] = " + String.Format("{0,12:e15}", listPatch[i].Cmyk_C) + "," + String.Format("{0,12:e15}", listPatch[i].Cmyk_M) + "," + String.Format("{0,12:e15}", listPatch[i].Cmyk_Y) + "," + String.Format("{0,12:e15}", listPatch[i].Cmyk_K) + ";");
                    sw.WriteLine("\tHideCMYK:string = " + "\"ShowAll\";");
                    sw.WriteLine("\tHideLab:string = " + "\"ShowAll\";");
                    sw.WriteLine("\tRawName:string = " + "\"\";");
                    sw.WriteLine("}");
                    sw.WriteLine("");
                    sw.WriteLine("%%E:");
                }
                //-----Footer portion of file------------//
                sw.WriteLine("%%N:/Proforma/ColorPools/2/ColorantAliasRules");
                sw.WriteLine("");
                sw.WriteLine("%%E:");
                sw.WriteLine("%%N:/Proforma/ColorPools/2/Info");
                sw.WriteLine("FUJI" + "\\" + "ColorPoolInfo_1:OBJECT FUJI" + "\\" + "ColorPoolInfo");
                sw.WriteLine("{");
                sw.WriteLine("\tName:string = " + "\"CustomSpotLib\";");
                sw.WriteLine("\tComment:string = " + "\" \";");
                sw.WriteLine("}");
                sw.WriteLine("");
                sw.WriteLine("%%E:");
                //--------End: Footer-------------------//

                sw.Close();

                Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CMYKFile/"), CMYKFileName + "_CMYKFile.dbd", CMYKFileName + "_CMYKFile.dbd");
                #endregion
            }
        }

        
        protected void BtnExportCurvesDeltaView_Click(object sender, EventArgs e)
        {
            try
            {
                if (SessionManager.Instance.CurveDeltaRefView == null || SessionManager.Instance.CurveDeltaPrevRunView == null)
                {
                    BindCurve(Convert.ToInt16(drpCurveType.SelectedValue)); //-----Rebind session if session is empty.
                }

                List<Entity.CPSync1D.OptimizeRunCurve> DT_CurveDelta = new List<Entity.CPSync1D.OptimizeRunCurve>();
                if (rbtnDetlaReflistClient.Items[0].Enabled == true && rbtnDetlaReflistClient.Items[0].Selected == true)
                {
                    DT_CurveDelta = (List<Entity.CPSync1D.OptimizeRunCurve>)SessionManager.Instance.CurveDeltaRefView;
                }
                else { DT_CurveDelta = (List<Entity.CPSync1D.OptimizeRunCurve>)SessionManager.Instance.CurveDeltaPrevRunView; }

                if (DT_CurveDelta != null)
                {
                    if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile")))
                        Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile"));

                    string CurveFileName = SessionManager.Instance.CurrentProject.ProjectName + " " + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Month.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Day.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Hour.ToString("00") + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Minute.ToString("00");

                    #region Create file and download txt file for all curve types
                    //---------------For all Curve Types -------------------//
                    System.IO.StreamWriter sw;
                    sw = System.IO.File.CreateText(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/") + CurveFileName + "_CurveDelta.txt");
                    sw.WriteLine("\"Fujifilm Colorpath Sync 1D\"");
                    sw.WriteLine("BEGIN_DATA_FORMAT");
                    sw.WriteLine("Reference\tCMYK_C\tCMYK_M\tCMYK_Y\tCMYK_K");
                    sw.WriteLine("END_DATA_FORMAT");
                    sw.WriteLine("NUMBER_OF_SETS\t" + DT_CurveDelta.Count);
                    sw.WriteLine("BEGIN_DATA");
                    for (int i = 0; i < DT_CurveDelta.Count; i++)
                    {
                        sw.WriteLine(DT_CurveDelta[i].ReferenceNo.ToString() + "\t" + DT_CurveDelta[i].CMYK_C.ToString() + "\t" + DT_CurveDelta[i].CMYK_M.ToString() + "\t" + DT_CurveDelta[i].CMYK_Y.ToString() + "\t" + DT_CurveDelta[i].CMYK_K.ToString());
                    }
                    sw.WriteLine("END_DATA");
                    sw.Close();

                    Utilities.DownloadExportFile(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CurvesFile/"), CurveFileName + "_CurveDelta.txt", CurveFileName + "_CurveDelta.txt");
                    #endregion
                }
            }
            catch (Exception Exc)
            {
                throw Exc;
            }
        }

        protected void rbtnDetlaReflistClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindCurve(Convert.ToInt16(drpCurveType.SelectedValue));
        }

        #endregion

        #region Methods
        private void BindCurve(short _CurveTypeID)
        {
            this.OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            if (_CurveTypeID == 100 || _CurveTypeID == 101) //---Set same value for "ColorGate-TVI" and "ColorGate-CGCX" like "5%" for export curve data
            {
                _CurveTypeID = 5;
            }
            if (_CurveTypeID == 102) //---Set same value for "Onyx G7 Import" like "Nexus" for export curve data
            {
                _CurveTypeID = 45;
            }
            this.CurveTypeId = _CurveTypeID;
            _presenterExport.GetCurveData();
            if (!IsErrorOccured)
            {
                SessionManager.Instance.CurveExportView = this.CurveData.CurveExportView;
                SessionManager.Instance.CurveDeltaRefView = this.CurveData.CurveDeltaRefView;
                SessionManager.Instance.CurveDeltaPrevRunView = this.CurveData.CurveDeltaPrevRunView;
            }
        }

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

        #endregion


    }
}