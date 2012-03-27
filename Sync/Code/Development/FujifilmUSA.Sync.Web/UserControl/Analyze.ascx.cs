using System;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Analyze : System.Web.UI.UserControl, IViewAnalyze
    {
        #region Variable Declarations
        private PresenterAnalyze _presenterAnalyze;
        public string AddMeasurementDeleteMeasurementConfirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddMeasurement_DeleteMeasurement_Confirmation");
            }
        }

        public bool IsNewDlp
        {
            get
            {
                return SessionManager.Instance.CurrentProject.IsNewlyCreated;
            }
        }

        #endregion

        #region Page Method
        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterAnalyze = new PresenterAnalyze(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
            }
            header.InnerText = IsNewDlp
                                   ? string.Format("New Print Condition - Analyze (No DLP)")
                                   : string.Format("Analyze - {0}.icc", SessionManager.Instance.CurrentProject.ProjectName);
            lbtnDownloadAndSaveDLP.Text = IsNewDlp
                                              ? "<span><img id='imgAdd' src='Images/export.png'/>Download DLP<span>"
                                              : "<span><img id='imgAdd' src='Images/save_icon.png'/>Save<span>";
            if (!Page.IsPostBack)
            {
                BindMesurementChart();
            }
        }
        #endregion

        #region Methods

        private void BindMesurementChart()
        {
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            Measurements = new List<Entity.Measurement>();
            _presenterAnalyze.GetAnalyzeChartMeasurement();

            AnalyzeChartManager manager = new AnalyzeChartManager(chartAnalyze, Measurements);
            manager.CurrentMeasurementID = Convert.ToInt32(hdMeasurement.Value);
            manager.BindChart();
            hdMeasurement.Value = manager.CurrentMeasurementID.ToString();

        }
        
        #endregion

        #region Control Events
        protected void hdChartButton_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(hdCheckBoxChange.Value) == 1)
            {
                Measurement = new Entity.Measurement();
                Measurement.MeasurementId = Convert.ToInt32(hdMeasurement.Value);
                Measurement.IsUsed = hdChartCheckBox.Checked;
                _presenterAnalyze.UpdateMeasurementByAnalyzeChart();
            }
            BindMesurementChart();
        }
        protected void lbtnDownloadAndSaveDLP_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            _presenterAnalyze.GetMeasurementsOnRunId();
            _presenterAnalyze.CreateDestinationAndDlp();
            Response.Redirect("Export.aspx");
        }
        #endregion

        #region IViewAnalyze Members

        public int ProjectId { get; set; }

        public int OptimizeRunId { get; set; }

        public List<int> MeasurementIds { get; set; }

        public List<Entity.Measurement> Measurements { get; set; }

        public Entity.Measurement Measurement { get; set; }

        #endregion

        #region IViewError Members

        public void ErrorOccured(object sender, ErrorArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
