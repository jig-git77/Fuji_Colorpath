using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class SpotMeasurement : BaseControl, IViewImportMeasurements
    {
        #region Variable Declaration
        private PresenterImportMeasurements _presenterImportMeasurements;
        #endregion

        #region Private Variables - Globalization
        private const string _recnewPrintConditionText = "newPrintConditionText";
        private const string _recmeasureText = "measureText";
        private const string _recValidationMeasure = "validationMeasureText";
        int LastOptimizationRunID = 0;

        #endregion
        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterImportMeasurements = new PresenterImportMeasurements(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        public void LoadData()
        {
            if (SessionManager.Instance.CurrentProject != null)
            {
                ProjectId = SessionManager.Instance.CurrentProject.SpotTargetProject.SpotProjectId;
                OptimizeRunID = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                LastOptimizationRunID = _presenterImportMeasurements.ImportDummyMeasurement();
                ViewState["LastOptimizationRunID"] = LastOptimizationRunID;
            }
        }      

        protected void btnAnalyze_Click(object sender, EventArgs e)
        {
            if (ViewState["LastOptimizationRunID"] != null)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
                LastOptimizationRunID = Convert.ToInt32(ViewState["LastOptimizationRunID"]);
                Response.Redirect("~/Analyze_SpotColor.aspx?OptimizeRunID=" + LastOptimizationRunID.ToString());
            }
        }

        public List<Project> Projects { get; set; }
        public int ProjectId { get; set; }
        public int DeviceId { get; set; }
        public int LicenseTypeID { get; set; }
        public List<int> Measurements { get; set; }
        public int MeasurementID { get; set; }
        public int OptimizeRunID { get; set; }
    }
}