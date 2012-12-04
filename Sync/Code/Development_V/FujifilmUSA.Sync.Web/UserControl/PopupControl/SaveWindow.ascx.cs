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
        private const string _recDownloadChartNotAvailable = "downloadChartNotAvailable";
        private const string _recSaveAs = "saveAs";

        public SaveWindow()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterSaveWindow = new PresenterSaveWindow(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {              
                if (!string.IsNullOrEmpty(SessionManager.Instance.CurrentProject.ProjectName))
                {
                    txtProjectName.Text = SessionManager.Instance.CurrentProject.ProjectName;
                    Page.Title = GetResourceString(_recSaveAs);
                }
            }
        } 
        #endregion

        #region "Control Events"        
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.ProjectName = txtProjectName.Text;
            SessionManager.Instance.CurrentProject.CreateDate = DateTime.Now;            
            Project = SessionManager.Instance.CurrentProject;
            _presenterSaveWindow.SaveProject();
            if (!IsErrorOccured)
            {
                if(SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                {
                    _presenterSaveWindow.GetProjectDetailWithVerification();
                }
                else
                {
                    _presenterSaveWindow.GetProjectDetail();
                }
                WebLogActivityManager.LogActivity(LogActivityType.CreateProject, new object[] {Project.ProjectId, Project.ProjectName}, Request.UserHostAddress);
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
            string ChartName = "";
            if (SessionManager.Instance.CurrentProject.MeasurementSetting != null && SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key > 0 && SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Key > 0)
            {
                if (SessionManager.Instance.CurrentProject.OptimizeRuns == null || SessionManager.Instance.CurrentProject.OptimizeRuns.Count <= 1) //-----For Base DLP Chart
                {
                    ReferenceChartTypeId = SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key;
                    ChartName = SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Value;
                }
                else   //----------For Optimization Chart
                {
                    ReferenceChartTypeId = SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Key;
                    ChartName = SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Value;
                }
                _presenterSaveWindow.GetChartPreview();
                if (!IsErrorOccured)
                {
                    if (ChartPreview != null)
                        Utilities.DownloadFile(ChartPreview, string.Format("{0}.pdf", ChartName), ".pdf");
                    else
                        fujiStatusbar.DisplayMessage(GetResourceString(_recDownloadChartNotAvailable), true);
                }
            }
            else
            {
                fujiStatusbar.DisplayMessage(GetResourceString(_recDownloadChartNotAvailable), true);
            }
        }
        #endregion

        #region IViewSaveWindow Members
        public short ReferenceChartTypeId { get; set; }
        public byte[] ChartPreview { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        #endregion
    }
}
