using System;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Analyze : BaseControl, IViewAnalyze
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
            get { return SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1; }
        }

        public bool IsDlpCreated
        {
            get { return SessionManager.Instance.CurrentOptimizeRun.HasProfile; }
        }

        public string UpdateProgressClientID
        {
            get { return ((UpdateProgress) Page.Master.FindControl("UpdateProgress")).ClientID; }
        }

        public string DLPMessage
        {
            get
            {
                return string.Format("Once optimization is complete, apply DLP 000 to workflow and repaint the {0} patch chart.", SessionManager.Instance.CurrentOptimizeRun.ReferenceChartType.Value);
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

                //Set sync patch
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
            }
            
            header.InnerText = IsNewDlp
                                   ? string.Format("New Print Condition - Analyze (No DLP)")
                                   : string.Format("Analyze - {0} DLP {1}.icc", SessionManager.Instance.CurrentProject.ProjectName, SessionManager.Instance.CurrentProject.DlpNo);
            lbtnDownloadAndSaveDLP.Text = IsNewDlp
                                              ? "<span><img id='imgAdd' src='Images/export.png'/>Download DLP<span>"
                                              : "<span><img id='imgAdd' src='Images/save_icon.png'/>Next<span>";
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
            if (!IsErrorOccured)
            {
                AnalyzeChartManager manager = new AnalyzeChartManager(chartAnalyze, Measurements);
                manager.CurrentMeasurementID = Convert.ToInt32(hdMeasurement.Value);
                manager.BindChart();
                hdMeasurement.Value = manager.CurrentMeasurementID.ToString();
            }
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
            if (!IsErrorOccured)
            {
                BindMesurementChart();
            }
        }
        protected void lbtnDownloadAndSaveDLP_Click(object sender, EventArgs e)
        {
            if (!IsNewDlp && IsDlpCreated)
            {
                Response.Redirect("Export.aspx");
            }
            else
            {
                ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                _presenterAnalyze.GetMeasurementsOnRunId();
                if (!IsErrorOccured)
                {
                    _presenterAnalyze.CreateDestinationAndDlp();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject.OptimizeRuns[0].HasProfile = true;
                        Response.Redirect("Export.aspx");
                    }
                }
            }
        }
        #endregion

        #region IViewAnalyze Members

        public int ProjectId { get; set; }

        public int OptimizeRunId { get; set; }

        public List<int> MeasurementIds { get; set; }

        public List<Entity.Measurement> Measurements { get; set; }

        public Entity.Measurement Measurement { get; set; }

        #endregion
    }
}
