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
                if (SessionManager.Instance.CurrentProject.SpotTargetProject != null &&
                    SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns != null &&
                    SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Count >= 1)
                {
                    CalculateSpotNamedColorProfile();
                }
            }
        }


        public void CalculateSpotNamedColorProfile()
        {
            try
            {
                if (SessionManager.Instance.CurrentProject != null)
                {
                    ProjectId = SessionManager.Instance.CurrentProject.SpotTargetProject.SpotProjectId;
                    OptimizeRunID = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                    ViewState["LastOptimizationRunID"] = OptimizeRunID;
                    if (SessionManager.Instance.CurrentOptimizeRun.HasProfile == false) //------If Named Color Profile already created, then don't do anything again----//
                    {
                        _presenterImportMeasurements.CreateSpotProjectInitialOptimization();
                        SessionManager.Instance.CurrentOptimizeRun.HasProfile = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnAnalyze_Click(object sender, EventArgs e)
        {
            if (ViewState["LastOptimizationRunID"] != null)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
                SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns[0].HasMeasurements = true;
                SessionManager.Instance.CurrentOptimizeRun.HasMeasurements = true;
                SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId = Convert.ToInt32(ViewState["LastOptimizationRunID"]);
                Response.Redirect("~/Analyze_SpotColor.aspx");
            }
        }

        #region IViewImportMeasurements Members
        public List<Project> Projects { get; set; }
        public int ProjectId { get; set; }
        public int DeviceId { get; set; }
        public int LicenseTypeID { get; set; }
        public List<int> Measurements { get; set; }
        public int MeasurementID { get; set; }
        public int OptimizeRunID { get; set; }
        public int UserID { get; set; }
        public Entity.Measurement Measurement { get; set; }
        #endregion  
    }
}