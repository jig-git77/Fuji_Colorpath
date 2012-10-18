using FujifilmUSA.Sync.Common;
using System;
using FujifilmUSA.Sync.Entity;

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

        public bool IsOperationVerification
        {
            set
            {
                tdNotes.Visible = value;
                tdNotesCaption.Visible = value;
                trCustomer.Visible = value;
            }
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
                hdIsVerification.Value = (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Verification).ToString().ToLower();
                hdProjectId.Value = SessionManager.Instance.CurrentProject.ProjectId.ToString();
                hdOptimizeRunId.Value = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId.ToString();
                if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Verification)
                {
                    hdReferenceChartTypeId.Value = SessionManager.Instance.CurrentProject.MeasurementSetting.VReferenceChartType.Key.ToString();
                    hdReferenceChartDisplayType.Value = Convert.ToInt32(EReferenceChartDisType.VerificationChart).ToString();
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
            }
            IsOperationVerification = (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Verification);
        }
    }
}
