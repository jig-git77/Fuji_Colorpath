using FujifilmUSA.Sync.Common;
using System;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class MeasurementCommon : System.Web.UI.UserControl
    {
        #region Declaration
        public string AddMeasurement_DeleteMeasurement_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddMeasurement_DeleteMeasurement_Confirmation");
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hdProjectId.Value = SessionManager.Instance.CurrentProject.ProjectId.ToString();
                hdOptimizeRunId.Value = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId.ToString();
                hdReferenceChartTypeId.Value = SessionManager.Instance.CurrentProject.ReferenceChartTypeId.ToString();
            }
        }
    }
}
