using System;
using System.Linq;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class SaveWindow : BaseControl, IViewSaveWindow
    {
        private PresenterSaveWindow _presenterSaveWindow;
        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterSaveWindow = new PresenterSaveWindow(this);
        }

        #region "Control Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(SessionManager.Instance.CurrentProject.ProjectName))
                {
                    txtProjectName.Text = SessionManager.Instance.CurrentProject.ProjectName;
                }
            }
        }
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.ProjectName = txtProjectName.Text;
            SessionManager.Instance.CurrentProject.CreateDate = DateTime.Now;
            Project = SessionManager.Instance.CurrentProject;
            _presenterSaveWindow.SaveProject();

            if (!IsErrorOccured)
            {
                SessionManager.Instance.CurrentProject = Project;
                SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.OptimizeRuns.Last();
                if (SessionManager.Instance.CurrentOptimizeRun.HasProfile)
                {
                    SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Download;
                    Utilities.PopupWindowsRedirect(this, "Export.aspx");
                }
                else if (SessionManager.Instance.CurrentOptimizeRun.HasMeasurements)
                {
                    SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
                    Utilities.PopupWindowsRedirect(this, "Analyze.aspx");
                }
                else
                {
                    SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Measure;
                    Utilities.PopupWindowsRedirect(this, "Measurement.aspx");
                }
            }
        }
        protected void lbtnDownloadChart_Click(object sender, EventArgs e)
        {
            if (SessionManager.Instance.CurrentProject.MeasurementSetting != null && SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key > 0)
            {
                ReferenceChartTypeId = SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key;
                _presenterSaveWindow.GetChartPreview();
                if (!IsErrorOccured)
                {
                    if (ChartPreview != null)
                        Utilities.DownloadFile(ChartPreview, string.Format("{0}", SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Value), ".jpg");
                    else
                        fujiStatusbar.DisplayMessage("Download chart is not available", true);
                }
            }
            else
            {
                fujiStatusbar.DisplayMessage("Download chart is not available", true);
            }
        }
        #endregion

        #region IViewSaveWindow Members
        public int ReferenceChartTypeId { get; set; }
        public byte[] ChartPreview { get; set; }
        public Project Project { get; set; }
        #endregion
    }
}
