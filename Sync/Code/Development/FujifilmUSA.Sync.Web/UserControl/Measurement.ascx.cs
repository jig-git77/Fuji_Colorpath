using System;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Measurement : BaseControl
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

        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Measure;
                header.InnerText = (SessionManager.Instance.CurrentProject.OptimizeRuns.Count > 1)
                                   ? string.Format("Measure - {0}", SessionManager.Instance.CurrentProject.CurrentDlpName)
                                   : string.Format("New Print Condition - Measure (No DLP)");
            }
        }

        #endregion

        #region Control Events
        protected void btnAnalyze_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.OptimizeRuns[0].HasMeasurements = true;
            Response.Redirect("Analyze.aspx");
        }
        #endregion

    }
}
