using FujifilmUSA.Sync.Common;
using System;

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
        #endregion

        public MeasurementCommon()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdProjectId.Value = SessionManager.Instance.CurrentProject.ProjectId.ToString();
                hdOptimizeRunId.Value = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId.ToString();               
                hdReferenceChartTypeId.Value = SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key.ToString();                
                hdHasMeasurement.Value = SessionManager.Instance.CurrentOptimizeRun.HasMeasurements.ToString();
            }            
        }
    }
}
