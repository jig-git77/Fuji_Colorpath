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
                hdIsVerification.Value = (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation).ToString().ToLower();
                hdLicenseType.Value = ((int)SessionManager.Instance.CurrentProject.LicenseType).ToString();
                hdProjectId.Value = SessionManager.Instance.CurrentProject.ProjectId.ToString();
                hdOptimizeRunId.Value = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId.ToString();
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
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DLP || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.InkOptimization)
                {
                    if (SessionManager.Instance.CurrentDlpSubOperation != ELicenseType.Validation)
                    {
                        if (SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId == SessionManager.Instance.CurrentProject.OptimizeRuns.First().OptimizeRunId)
                        {
                            hdDisplayImport.Value = "yes";
                        }
                    }
                }
            }
            SetVerificationControlVisibility();
        }
    }
}
