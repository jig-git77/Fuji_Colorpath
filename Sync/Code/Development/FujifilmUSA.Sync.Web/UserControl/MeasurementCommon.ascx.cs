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

                //----------------"Import Measurements" button only available for "Ink Optimization" Project Type and only for initial 1st run--------------//
                if (SessionManager.Instance.CurrentOperation == ELicenseType.InkOptimization)
                {
                    if (SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId == SessionManager.Instance.CurrentProject.OptimizeRuns.First().OptimizeRunId)
                    {
                        Div_ImportMeasurements.Visible = true;
                    }
                }  
            }            
        }
    }
}
