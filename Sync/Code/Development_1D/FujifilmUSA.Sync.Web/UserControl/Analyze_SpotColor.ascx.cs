using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.IO;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Analyze_SpotColor : BaseControl, IViewAnalyze
    {
        #region Variable Declarations
        private PresenterAnalyze _presenterAnalyze;
        private const string _recCreateDLP = "locCreateNCPResource1";
        private const string _recNext = "nextText";
        private const string _recnewPrintCondition = "newNCPPrintConditionText";
        private const string _recAnalyze = "analyze";
        private const string _recSummaryHeaderRun0 = "locSummaryHeaderRun0Resource1";
        private const string _recSummaryHeaderRun1 = "locSummaryHeaderRun1Resource1";
        public string DLPName = "";
        #endregion


        #region Page Method
        protected override void OnInit(EventArgs e)
        {
            _presenterAnalyze = new PresenterAnalyze(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
                if (SessionManager.Instance.CurrentProject.SpotTargetProject != null && SessionManager.Instance.CurrentProject.SpotTargetProject.PrintCondition != null)
                {
                    lblMedia.Text = SessionManager.Instance.CurrentProject.SpotTargetProject.PrintCondition.Media;
                    lblColorant.Text = SessionManager.Instance.CurrentProject.SpotTargetProject.PrintCondition.Colorant;
                    lblRipSettings.Text = SessionManager.Instance.CurrentProject.SpotTargetProject.PrintCondition.RipSettings;
                    lblSpotTarget.Text = SessionManager.Instance.CurrentProject.SpotTargetProject.SpotTarget.SpotTargetName;
                    hdnBaseServiceUrl.Value = Utilities.ReadConfigValue("BaseServiceURL");
                    OptimizeRunId = Convert.ToInt32(SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId);
                    hdfOptimizationRunID.Value = OptimizeRunId.ToString();
                    BindSpotSummary();
                    hdnIsNewNCP.Value = IsNewNCP.ToString();
                    hdnIsNcpCreated.Value = IsNCPCreated.ToString();
                    hdfCurrentCulture.Value = getCurrentCultureLanguage();
                }

                if (SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId == SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.First().OptimizeRunId)
                {
                    header.InnerText = GetResourceString(_recnewPrintCondition);//"New Print Condition - Analyze (No DLP)";
                    DLPName = "No NCP";
                    BtnExportCMYK.Visible = false;  //----Don't show CMYK export button for Run-0, since there is no previous run's CMYK data to export
                    locSummary_Header.Text = GetResourceString(_recSummaryHeaderRun0);
                }
                else
                {
                    header.InnerText = string.Format(GetResourceString(_recAnalyze), SessionManager.Instance.CurrentOptimizeRun.LastDlpName);//Analyze - {0}
                    DLPName = SessionManager.Instance.CurrentOptimizeRun.LastDlpName.Replace(SessionManager.Instance.CurrentProject.SpotTargetProject.ProjectName + " ", ""); //---DLPName to pass into report
                    DLPName = DLPName.Remove(DLPName.Length - 4);
                    locSummary_Header.Text = GetResourceString(_recSummaryHeaderRun1);
                }

                lbtnDownloadAndSaveNCP.Text = IsNewNCP
                                                  ? "<span><img id='imgAdd' src='Images/export.png'/>" + GetResourceString(_recCreateDLP) + "</span>"
                                                  : "<span><img id='imgAdd' src='Images/save_icon.png'/>" + GetResourceString(_recNext) + "</span>";//Create DLP - Next

                hdHasMeasurement.Value = SessionManager.Instance.CurrentOptimizeRun.HasMeasurements.ToString();

                //------------If this run is closed /certify run, then invisible download(Next) button, because there is no DLP available to download for this run-----//
                if (SessionManager.Instance.CurrentOptimizeRun.Status == EOptimizeRunStatus.Certify)
                    lbtnDownloadAndSaveNCP.Visible = false;
            }
        }

        #endregion


        #region Controls Method
        protected void lbtnDownloadAndSaveNCP_Click(object sender, EventArgs e)
        {
            if (!IsNCPCreated)
            {
                SpotProjectId = SessionManager.Instance.CurrentProject.SpotTargetProject.SpotProjectId;
                OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                ProjectStatus = Convert.ToInt32(EOptimizeRunStatus.Closed);
                _presenterAnalyze.UpdateOptimizeRun_SpotProject();
                if (!IsErrorOccured)
                {
                    _presenterAnalyze.GetProjectDetail_SpotProject();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Last();
                        Response.Redirect("Export.aspx");
                    }
                }
            }
            else
            {
                Response.Redirect("Export.aspx");
            }

        }

        protected void BtnExportCMYK_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CMYKFile")))
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("UploadFiles/CMYKFile"));

            //==========  Get Data Table and Bind ==========//
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary)
            {
                OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                _presenterAnalyze.GetSpotPatchDetailsForCMYKExport();

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
        #endregion


        #region Methods

        public void BindSpotSummary()
        {
            try
            {
                _presenterAnalyze.GetSpotPatchSummaryDetails();
                if (dtPatchDetails != null && dtPatchDetails.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dtPatchDetails.Rows[0]["MeasurementID"]) > 0)
                    {
                        tblSecondRunDeltas.Visible = true;
                        lblMeasurementID.Text= Convert.ToString(dtPatchDetails.Rows[0]["MeasurementID"]);
                        lblMeasurementName.Text = Convert.ToString(dtPatchDetails.Rows[0]["MeasurementName"]);                     

                        lblMinETP.Text = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["MinETP"]));
                        lblMaxETP.Text = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["MaxETP"]));
                        lblAvgETP.Text = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["AvgETP"]));
                                      
                        lblMinEMT.Text = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["MinEMT"]));
                        lblMaxEMT.Text = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["MaxEMT"]));
                        lblAvgEMT.Text = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["AvgEMT"]));
                                      
                        lblMinEMP.Text = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["MinEMP"]));
                        lblMaxEMP.Text = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["MaxEMP"]));
                        lblAvgEMP.Text = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["AvgEMP"]));

                        trMeasurementID.Visible = true;
                        trMeasurementName.Visible = true;
                    }
                    else
                    {                        
                        tblFirstRunDeltas.Visible = true;                        
                        lblMinDeltaE.Text = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["MinE"]));
                        lblMaxDeltaE.Text = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["MaxE"]));
                        lblAvgDeltaE.Text = String.Format("{0:0.###}", Convert.ToDecimal(dtPatchDetails.Rows[0]["AvgE"]));
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

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

        public bool IsNewNCP
        {
            get
            {
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary) //---If Sync spot library type of project
                {
                    return SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Count == 1;
                }
                return false;
            }
        }

        public bool IsNCPCreated
        {
            get { return SessionManager.Instance.CurrentOptimizeRun.HasProfile; }
        }
        #endregion


        #region IViewAnalyze Members

        public int ProjectId { get; set; }

        public int OptimizeRunId { get; set; }

        public List<int> MeasurementIds { get; set; }

        public List<Entity.Measurement> Measurements { get; set; }

        public Entity.Measurement Measurement { get; set; }

        public string DestinationProfileDescription { get; set; }

        public string DlpDescription { get; set; }

        public List<MeasurementPatch_AvgDelta> MeasurementsDelta { get; set; }

        public List<MeasurementPatchDetails> PatchDetails { get; set; }

        public int MeasurementId { get; set; }

        public List<AnalyzeChart> DTDeltaEPrimaryPatches { get; set; }

        public List<AnalyzeChart> DTPaperPatches { get; set; }

        public List<AnalyzeChart> DTDensity { get; set; }

        public List<Entity.AnalyzeChart_Gray> DTLABChart { get; set; }
        public List<Entity.AnalyzeChart_Gray> DTGaumatChart1 { get; set; }
        public List<Entity.AnalyzeChart_Gray> DTGaumatChart2 { get; set; }

        public string From_ΔE { get; set; }
        public bool IsVerification
        {
            get
            {
                return (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);
            }
        }


        public List<Entity.Measurement> MeasurementSummary { get; set; }

        public List<Entity.AnalyzeChart_G7> GrayFChart { get; set; }
        public List<Entity.AnalyzeChart_G7> GrayFSummary { get; set; }
        public List<Entity.AnalyzeChart_G7> BlackLChart { get; set; }
        public List<Entity.AnalyzeChart_G7> BlackLSummary { get; set; }
        public List<Entity.AnalyzeChart_G7> GrayLChart { get; set; }
        public List<Entity.AnalyzeChart_G7> GrayLSummary { get; set; }
        public List<Entity.AnalyzeChart_G7> BlackNPDChart { get; set; }
        public List<Entity.AnalyzeChart_G7> GrayNPDChart { get; set; }

        public DataTable dtPatchDetails { get; set; }
        public DataTable dtFilteredPatchDetails { get; set; }

        public bool useDLP { get; set; }
        public byte[] DeviceLinkProfile { get; set; }
        public PCSDataTypes PCSDataTypes { get; set; }
        public DensityTypes DensityType { get; set; }
        public bool IsSync1DType
        {
            get
            {
                return (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Sync1D);
            }
        }

        public List<Entity.AnalyzeChart_DotGain> DTTVIChart { get; set; }
        public List<Entity.AnalyzeChart_DotGain> DTISOTVIChart { get; set; }
        public List<Entity.AnalyzeChart_DotGain> DTSpreadChartData { get; set; }
        public List<Entity.AnalyzeChart_DotGain> DTSpreadChartData_CMYK { get; set; }
        public List<Entity.AnalyzeChart_DotGain> DTSpreadSplineRangeChartData { get; set; }
        public DataTable DTMaxSpreadChartData { get; set; }

        public bool IsAverageOfMeasurements
        {
            get
            {
                //return chkAnalyzeAverageOfMeasurements.Checked;
                return false;
            }
            set
            {
                IsAverageOfMeasurements = false;//chkAnalyzeAverageOfMeasurements.Checked;
            }
        }

        public bool Is1DMeasurementSmoothing
        {
            get
            {
                //return chk1DMeasurementSmoothing.Checked;
                return false;
            }
            set
            {
                Is1DMeasurementSmoothing = false;// chk1DMeasurementSmoothing.Checked;
            }
        }

        public string FileName;
        public int ProjectStatus { get; set; }
        public Project Project { get; set; }
        public int SpotProjectId { get; set; }
        public bool IsUsed { get; set; }
        #endregion

    }
}