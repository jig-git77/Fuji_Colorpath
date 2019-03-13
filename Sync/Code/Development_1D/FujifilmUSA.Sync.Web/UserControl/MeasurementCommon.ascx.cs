using FujifilmUSA.Sync.Common;
using System;
using FujifilmUSA.Sync.Entity;
using System.Linq;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class MeasurementCommon : BaseControl
    {
        #region Declaration
        public string AddMeasurement_DeleteMeasurement_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddMeasurement_DeleteMeasurement_Confirmation");
            }
        }

        public int JsRandomNumber { get { return new Random().Next(1, 100); } }

        public void SetVerificationControlVisibility()
        {
            tdNotes.Visible = (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);
            tdNotesCaption.Visible = (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);
            trCustomer.Visible = (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);
            trValidationDetails.Visible = (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D) ? false : (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation);            
            if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.Sync1D && SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                lblMeasurementName.Text = GetLocalResourceObject("JobNameCaption").ToString();
            else
                lblMeasurementName.Text = GetLocalResourceObject("MeasurementNameCaption").ToString();
            hdnPCSType.Value = Convert.ToInt32(SessionManager.Instance.CurrentProject.PCSDataTypes).ToString();
        }
        #endregion

        public MeasurementCommon()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IsXmfUser && SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                {
                    FillValidationDetailsFromXMF();
                }
                hdnBasePluginServiceUrl.Value = Utilities.ReadConfigValue("BasePluginServiceURL");
                hdIsVerification.Value = (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation).ToString().ToLower();
                hdLicenseType.Value = ((int)SessionManager.Instance.CurrentProject.LicenseType).ToString();
                btnImportMeasurementFile.Value = GetResourceString("btnImportMeasurementFile");
                btnImportMeasurementFile.Visible = (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLP || 
                    SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync4DCertification ||
                    SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D ||
                    SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary) && 
                    SessionManager.Instance.CurrentUser.Role.RoleId == (int)ERole.SystemAdmin;

                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary && SessionManager.Instance.CurrentProject.SpotTargetProject != null)
                    hdProjectId.Value = SessionManager.Instance.CurrentProject.SpotTargetProject.SpotProjectId.ToString();
                else
                    hdProjectId.Value = SessionManager.Instance.CurrentProject.ProjectId.ToString();

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
                //----------------"Import Measurements" button only available for "Sync4D", "Ink Optimization" and "Sync1D" Project Type and only for initial 1st run (Not shown in any type of Validation)--------------//
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLP || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.InkOptimization || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                {
                    if (SessionManager.Instance.CurrentDlpSubOperation != ELicenseType.Validation)
                    {
                        if (SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId == SessionManager.Instance.CurrentProject.OptimizeRuns.First().OptimizeRunId)
                        {
                            if (SessionManager.Instance.CurrentProject.EnabledPaperScaling == false) //---If project has paper scalling enabled, then don't give option to import measurements
                            {
                                hdDisplayImport.Value = "yes";
                            }
                        }
                    }
                }
                hdSpotProjectType.Value = (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary).ToString().ToLower();
            }
            SetVerificationControlVisibility();
        }

        private void FillValidationDetailsFromXMF()
        {
            if (SessionManager.Instance.CurrentValidationRequest != null)
            {
                txtMeasurementName.Text = SessionManager.Instance.CurrentValidationRequest.JobName;
                txtMeasurementName.Enabled = false;
                txtJDFJobId.Text = SessionManager.Instance.CurrentValidationRequest.JDFJobId;
                txtJDFJobId.Enabled = false;
                txtPageOrSurface.Text = SessionManager.Instance.CurrentValidationRequest.PageOrSurface;
                txtPageOrSurface.Enabled = false;
                txtCustomer.Text = SessionManager.Instance.CurrentValidationRequest.Customer;
                txtCustomer.Enabled = false;
            }
        }

        #region Properties
        public bool IsXmfUser
        {
            get
            {
                return (SessionManager.Instance.CurrentUser.UserType == Convert.ToInt32(UserType.XmfColorOrganizerUser));
            }
        }
        #endregion
    }
}
