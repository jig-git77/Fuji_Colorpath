using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Entity.CPSync1D;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class MeasurementCommonNew : BaseControl, IViewMeasurement
    {

        #region Declaration
        public string AddMeasurement_DeleteMeasurement_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddMeasurement_DeleteMeasurement_Confirmation");
            }
        }
        private PresenterMeasurementSetting _presenterMeasurementSetting;
        public int JsRandomNumber { get { return new Random().Next(1, 100); } }

        //public void SetVerificationControlVisibility()
        //{
        //    tdNotes.Visible = (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);
        //    tdNotesCaption.Visible = (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);
        //    trCustomer.Visible = (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);
        //    trValidationDetails.Visible = (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D) ? false : (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);
        //    if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.Sync1D && SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
        //        lblMeasurementName.Text = GetLocalResourceObject("JobNameCaption").ToString();
        //    else
        //        lblMeasurementName.Text = GetLocalResourceObject("MeasurementNameCaption").ToString();
        //    hdnPCSType.Value = Convert.ToInt32(SessionManager.Instance.CurrentProject.PCSDataTypes).ToString();
        //}
        #endregion

        protected override void OnInit(EventArgs e)
        {
            _presenterMeasurementSetting = new PresenterMeasurementSetting(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IsXmfUser && SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                {
                    //FillValidationDetailsFromXMF();
                }

                //hdnBasePluginServiceUrl.Value = Utilities.ReadConfigValue("BasePluginServiceURL");
                hdIsVerification.Value = (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation).ToString().ToLower();
                hdLicenseType.Value = ((int)SessionManager.Instance.CurrentProject.LicenseType).ToString();
                hdfAuthToken.Value = SessionManager.Instance.CurrentUser.APIAuthKey;
                btnImportMeasurementFile.Value = GetResourceString("btnImportMeasurementFile");
                btnImportMeasurementFile.Visible = (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLP ||
                   SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync4DCertification ||
                   SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D ||
                   SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary ||
                   SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLPPlus ||
                   SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLPMultiColor ||
                   SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1DMultiColor ||
                   SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DestinationProfile) &&
                   SessionManager.Instance.CurrentUser.Role.RoleId == (int)ERole.SystemAdmin;
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary && SessionManager.Instance.CurrentProject.SpotTargetProject != null)
                {
                    hdProjectId.Value = SessionManager.Instance.CurrentProject.SpotTargetProject.SpotProjectId.ToString();
                }
                else
                {
                    hdProjectId.Value = SessionManager.Instance.CurrentProject.ProjectId.ToString();
                }

                hdOptimizeRunId.Value = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId.ToString();
                hdDeviceType.Value = SessionManager.Instance.CurrentProject.MeasurementSetting.InstrumentTypeId.ToString();
                if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                {
                    hdReferenceChartTypeId.Value = SessionManager.Instance.CurrentProject.MeasurementSetting.VReferenceChartType.Key.ToString();
                    hdReferenceChartDisplayType.Value = Convert.ToInt32(EReferenceChartDisType.ValidationChart).ToString();
                }
                else if (SessionManager.Instance.CurrentProject.CurrDlpId > 0)
                {
                    hdReferenceChartTypeId.Value = SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Key.ToString();
                    hdReferenceChartDisplayType.Value = Convert.ToInt32(EReferenceChartDisType.OptimizationChart).ToString();
                }
                else
                {
                    hdReferenceChartTypeId.Value = SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key.ToString();
                    hdReferenceChartDisplayType.Value = Convert.ToInt32(EReferenceChartDisType.ProfileChart).ToString();
                }
                hdHasMeasurement.Value = SessionManager.Instance.CurrentOptimizeRun.HasMeasurements.ToString();
                //----------------"Import Measurements" button only available for "Sync4D" or "Ink Optimization" Project Type and only for initial 1st run (Not shown in any type of Validation)--------------//
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLP || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.InkOptimization || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLPPlus || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLPMultiColor || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1DMultiColor)
                {
                    if (SessionManager.Instance.CurrentDlpSubOperation != ELicenseType.Validation)
                    {
                        if (SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId == SessionManager.Instance.CurrentProject.OptimizeRuns.First().OptimizeRunId)
                        {
                            //if (SessionManager.Instance.CurrentProject.EnabledPaperScaling == false) //---If project has paper scalling enabled, then don't give option to import measurements
                            //{
                            hdDisplayImport.Value = "yes";
                            //}
                        }
                    }
                }
                hdSpotProjectType.Value = (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary).ToString().ToLower();

                Project = SessionManager.Instance.CurrentProject;
                ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                LicenseType = SessionManager.Instance.CurrentProject.LicenseType;
                LicenseTypeId = (int)SessionManager.Instance.CurrentProject.LicenseType;

                if (SessionManager.Instance.CurrentProject.EnabledPaperScaling)
                {
                    if (SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId != 0)
                    {
                        if (SessionManager.Instance.CurrentOptimizeRun.MasterRunId != -1)
                        {
                            // 1. take current optimize run id's before one.
                            // 2. based on previous optimize run id get measurement ids and get it's cielab values and take avg.
                            // 3. by avg values calculate target LAB / update new paper based on condition 4d or 1d.
                            // Current optimize run
                            var currentRun = SessionManager.Instance.CurrentOptimizeRun;

                            // Get previous optimize run
                            var previousRun = SessionManager.Instance.CurrentProject.OptimizeRuns
                                                .Where(x => x.OptimizeRunId < currentRun.OptimizeRunId)
                                                .OrderByDescending(x => x.OptimizeRunId)
                                                .FirstOrDefault();

                            if (previousRun != null)
                            {
                                OptimizeRunId = Project.OptimizeRuns.Select(x => x.OptimizeRunId).First();
                                // Get all measurement ids for previous run
                                _presenterMeasurementSetting.GetMeasurementIDs_RPS();

                                if (MeasurementIds != null && MeasurementIds.Any())
                                {
                                    decimal avgL = 0;
                                    decimal avgA = 0;
                                    decimal avgB = 0;
                                    int count = 0;

                                    foreach (int measurementId in MeasurementIds)
                                    {
                                        MeasurementId = measurementId;

                                        _presenterMeasurementSetting.GetActualValuesByMeasurementId();

                                        if (measurementPatchDetails != null)
                                        {
                                            avgL += Convert.ToDecimal(measurementPatchDetails.CielabL);
                                            avgA += Convert.ToDecimal(measurementPatchDetails.CielabA);
                                            avgB += Convert.ToDecimal(measurementPatchDetails.CielabB);
                                            count++;
                                        }
                                    }

                                    if (count > 0)
                                    {
                                        avgL /= count;
                                        avgA /= count;
                                        avgB /= count;

                                        avgL = Math.Round(avgL, 2, MidpointRounding.AwayFromZero);
                                        avgA = Math.Round(avgA, 2, MidpointRounding.AwayFromZero);
                                        avgB = Math.Round(avgB, 2, MidpointRounding.AwayFromZero);

                                        if (LicenseType == ELicenseType.DLP
                                            || LicenseType == ELicenseType.Sync4DCertification
                                            || LicenseType == ELicenseType.DLPMultiColor)
                                        {
                                            Project.NewPaper_White_a = avgA ;
                                            Project.NewPaper_White_b = avgB ;
                                            Project.NewPaper_White_L = avgL ;

                                            EnabledPaperScaling = SessionManager.Instance.CurrentProject.EnabledPaperScaling;
                                            IsUpdateNewPaper = true;
                                            IsUpdateDelta = false;

                                            _presenterMeasurementSetting.UpdateTargetLabList();
                                        }
                                        else if (LicenseType == ELicenseType.Sync1D
                                              || LicenseType == ELicenseType.Sync1DMultiColor
                                              || LicenseType == ELicenseType.DLPPlus)
                                        {
                                            if (SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeIDOld != 0)
                                                PrintstandardTypeId = SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeIDOld;
                                            else
                                                PrintstandardTypeId = SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeID;

                                            ProjectTypeId = SessionManager.Instance.CurrentProject.ProjectTypeId;
                                            _presenterMeasurementSetting.GetTargetLABList();

                                            EnabledPaperScaling = SessionManager.Instance.CurrentProject.EnabledPaperScaling;

                                            CalculateTargetLAB(Convert.ToDouble(avgL), Convert.ToDouble(avgA), Convert.ToDouble(avgB), true,false);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            //SetVerificationControlVisibility();

            //show the project details doe native app            
            lblPID.Text = SessionManager.Instance.CurrentOptimizeRun.ProjectId.ToString();
            lblORID.Text = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId.ToString();
            lblSN.Text = SessionManager.Instance.CurrentProject.MeasurementSetting.DeviceSerialNo;

            string projectType = "Profiler";
            if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.SpotLibrary)
            {
                if (hdReferenceChartDisplayType.Value == "2")
                {
                    projectType = "Optimization";
                }
                else if (hdReferenceChartDisplayType.Value == "3")
                {
                    projectType = "Validation";
                }
            }
            else
            {
                projectType = "Spot Project";
            }

            ProjectId = SessionManager.Instance.CurrentOptimizeRun.ProjectId;
            CurrentUserId = SessionManager.Instance.CurrentUser.UserId;
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            ProfileType = projectType;
            DeviceSerialNo = SessionManager.Instance.CurrentProject.MeasurementSetting.DeviceSerialNo;
            InstrumentTypeID = Convert.ToInt16(SessionManager.Instance.CurrentProject.MeasurementSetting.InstrumentTypeId);
            LicenseType = SessionManager.Instance.CurrentProject.LicenseType;


            if (SessionManager.Instance.CurrentProject.PrintCondition != null)
            {
                if (SessionManager.Instance.CurrentProject.PrintCondition.IsCustomPlugin)
                {
                    bool resultMesaurment = _presenterMeasurementSetting.CheckMeasurementPluginStatus();
                    if (resultMesaurment)
                    {
                        GetMToolMessage(1);
                        btnAddPluginStatus.CssClass = "green_btnotactive";
                        //btnAddPluginStatus.Enabled = false;
                    }
                    else
                    {
                        GetMToolMessage(0);
                        btnAddPluginStatus.CssClass = "green_bt";
                        //btnAddPluginStatus.Enabled = true;
                    }

                }
            }
            if (SessionManager.Instance.CurrentProject.SpotTargetProject != null)
            {
                if (SessionManager.Instance.CurrentProject.SpotTargetProject.PrintCondition.IsCustomPlugin)
                {
                    bool resultMesaurment = _presenterMeasurementSetting.CheckMeasurementPluginStatus();
                    if (resultMesaurment)
                    {
                        GetMToolMessage(1);
                        btnAddPluginStatus.CssClass = "green_btnotactive";
                        //btnAddPluginStatus.Enabled = false;
                    }
                    else
                    {
                        GetMToolMessage(0);
                        btnAddPluginStatus.CssClass = "green_bt";
                        //btnAddPluginStatus.Enabled = true;
                    }

                }
            }
            hdnbtnAnalyzeParent.Value = ((LinkButton)NamingContainer.FindControl("btnAnalyze")).ClientID;
            hdfHasDeletePermission.Value = SessionManager.Instance.CurrentUser.HasPermission(ERolePermission.CreateDeleteProject).ToString();
        }

        protected void btnAddPluginStatus_Click(object sender, EventArgs e)
        {
            try
            {
                int result = _presenterMeasurementSetting.CreateMeasurementPluginStatus();
                if (result > 0)
                {
                    GetMToolMessage(1);
                    ////Devlopment/prodcuction support only HTTP.

                    //Stage Code support only HTTPS.
                    if (ConfigurationManager.AppSettings["IsHttpsUrl"].ToString() == "true" && !Request.Url.ToString().Contains("https")) // Last '&&' condition is added to make it work for test 137 server location
                    {
                        Response.Redirect(Request.Url.ToString().Replace("http", "https")); //----This line is only for stage, move back to upper commented line for production future publish
                    }
                    else
                    {
                        Response.Redirect(Request.Url.ToString());
                    }
                }
                else
                {
                    lblMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void CalculateTargetLAB(double NewPaper_White_L, double NewPaper_White_a, double NewPaper_White_b, bool IsUpdateNewPaper_, bool IsUpdateDelta_)
        {
            if (ProjectTargetLABList != null && ProjectTargetLABList.Count > 0)
            {
                CIELab Finallab = new CIELab(0, 0, 0);
                CIEXYZ CurrentPaperxyz = new CIEXYZ(0, 0, 0);
                CIEXYZ NewPaperxyz = new CIEXYZ(0, 0, 0);
                CIEXYZ NewMinxyz = new CIEXYZ(0, 0, 0);

                //Create list for Paper Patch
                List<Entity.CPSync1D.ProjectTargetLAB> PaperProjectTargetList = new List<Entity.CPSync1D.ProjectTargetLAB>();
                PaperProjectTargetList = ProjectTargetLABList.ToList().Where(p => p.PatchType.PatchTypeId == 8).ToList();

                //Create list for new lab values
                List<Entity.CPSync1D.ProjectTargetLAB> NewProjectTargetList = new List<Entity.CPSync1D.ProjectTargetLAB>();

                double Black_L = Convert.ToDouble(SessionManager.Instance.CurrentProject.NewPaper_Black_L.ToString());
                double Black_a = Convert.ToDouble(SessionManager.Instance.CurrentProject.NewPaper_Black_a.ToString());
                double Black_b = Convert.ToDouble(SessionManager.Instance.CurrentProject.NewPaper_Black_b.ToString());

                //convert new Paper's LAB to XYZ
                NewPaperxyz = ColorMath.LabtoXYZ(NewPaper_White_L, NewPaper_White_a, NewPaper_White_b);
                NewMinxyz = ColorMath.LabtoXYZ(Black_L, Black_a, Black_b);

                //convert current Paper's LAB to XYZ
                CurrentPaperxyz = ColorMath.LabtoXYZ(Convert.ToDouble(PaperProjectTargetList[0].TargetL), Convert.ToDouble(PaperProjectTargetList[0].TargetA), Convert.ToDouble(PaperProjectTargetList[0].TargetB));

                for (int i = 0; i < ProjectTargetLABList.Count; i++)
                {
                    Finallab = ColorMath.CalcPaperScaleLAB(CurrentPaperxyz.X, CurrentPaperxyz.Y, CurrentPaperxyz.Z, NewPaperxyz.X, NewPaperxyz.Y, NewPaperxyz.Z, NewMinxyz.X, NewMinxyz.Y, NewMinxyz.Z, Convert.ToDouble(ProjectTargetLABList[i].TargetL), Convert.ToDouble(ProjectTargetLABList[i].TargetA), Convert.ToDouble(ProjectTargetLABList[i].TargetB));
                    NewProjectTargetList.Add(new Entity.CPSync1D.ProjectTargetLAB() { PatchType = ProjectTargetLABList[i].PatchType, TargetL = (decimal)Finallab.L, TargetA = (decimal)Finallab.A, TargetB = (decimal)Finallab.B });
                }
                int index = NewProjectTargetList.FindIndex(x => x.PatchType.PatchTypeId == 8);

                if (index != -1)
                {
                    NewProjectTargetList[index].TargetL = Convert.ToDecimal(NewPaper_White_L);
                    NewProjectTargetList[index].TargetA = Convert.ToDecimal(NewPaper_White_a);
                    NewProjectTargetList[index].TargetB = Convert.ToDecimal(NewPaper_White_b);
                }
                ProjectTargetLABList = NewProjectTargetList;
                Project.NewPaper_White_L = Convert.ToDecimal(NewPaper_White_L);
                Project.NewPaper_White_a = Convert.ToDecimal(NewPaper_White_a);
                Project.NewPaper_White_b = Convert.ToDecimal(NewPaper_White_b);
                IsUpdateNewPaper = IsUpdateNewPaper_;
                IsUpdateDelta = IsUpdateDelta_;
                _presenterMeasurementSetting.UpdateTargetLabList();
            }
        }

        //private void FillValidationDetailsFromXMF()
        //{
        //    if (SessionManager.Instance.CurrentValidationRequest != null)
        //    {
        //        txtMeasurementName.Text = SessionManager.Instance.CurrentValidationRequest.JobName;
        //        txtMeasurementName.Enabled = false;
        //        txtJDFJobId.Text = SessionManager.Instance.CurrentValidationRequest.JDFJobId;
        //        txtJDFJobId.Enabled = false;
        //        txtPageOrSurface.Text = SessionManager.Instance.CurrentValidationRequest.PageOrSurface;
        //        txtPageOrSurface.Enabled = false;
        //        txtCustomer.Text = SessionManager.Instance.CurrentValidationRequest.Customer;
        //        txtCustomer.Enabled = false;
        //    }
        //}


        #region Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recordExists"></param>
        /// <returns></returns>
        public void GetMToolMessage(byte recordExists)
        {
            StringBuilder str = new StringBuilder();
            if (recordExists == 1)
            {
                str.Append("<b>This project is currently activated for measurement.</b>");
                str.Append("<br />");
                str.Append("<b>Measure charts and upload using the ColorPath MTool, Return here when you're done.</b>");

            }
            else
            {
                str.Append("<b>This project is not currently activated for measurement.</b>");

            }
            lblMessage.Text = str.ToString();
        }
        #endregion
        #region Properties
        public bool IsXmfUser
        {
            get
            {
                return (SessionManager.Instance.CurrentUser.UserType == Convert.ToInt32(UserType.XmfColorOrganizerUser));
            }
        }
        #endregion

        #region IViewMeasurement
        public int ProjectId { get; set; }
        public int OptimizeRunId { get; set; }
        public string ProfileType { get; set; }
        public ELicenseType LicenseType { get; set; }
        public string DeviceSerialNo { get; set; }
        public short InstrumentTypeID { get; set; }
        public List<Entity.MeasurementLog> MeasurementLogs { get; set; }
        public Project Project { get; set; }
        public List<int> MeasurementIds { get; set; }
        public int MeasurementId { get; set; }
        public bool EnabledPaperScaling { get; set; }
        public bool IsUpdateNewPaper { get; set; }
        public MeasurementPatchDetails measurementPatchDetails { get; set; }
        public bool IsUpdateDelta { get; set; }
        public int LicenseTypeId { get; set; }
        public List<ProjectTargetLAB> ProjectTargetLABList { get; set; }
        public int ProjectTypeId { get; set; }
        public int PrintstandardTypeId { get; set; }
        #endregion


    }
}
