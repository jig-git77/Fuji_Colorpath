using System;
using System.Linq;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Export : BaseControl, IViewExport
    {
        private PresenterExport _presenterExport;
        public string AddMeasurement_DeleteMeasurement_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddMeasurement_DeleteMeasurement_Confirmation");
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterExport = new PresenterExport(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Download;
            }
        }

        protected void lbtnMeasure_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            _presenterExport.CreateOptimizeRun();
            if (!IsErrorOccured)
            {
                _presenterExport.GetProjectDetail();
                if (!IsErrorOccured)
                {
                    SessionManager.Instance.CurrentProject = Project;
                    SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.OptimizeRuns.Last();
                    Response.Redirect("Measurement.aspx");
                }
            }
        }

        protected void lbtnDownLoadDLP_Click(object sender, EventArgs e)
        {
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            _presenterExport.GetDeviceLinkProfile();
            if (!IsErrorOccured)
            {
                if (DeviceLinkProfile != null)
                    Utilities.DownloadFile(DeviceLinkProfile, "Profile.icc");
            }
        }

        protected void lbtnDownLoadChart_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            _presenterExport.GetChartPreview();
            if (!IsErrorOccured)
            {
                if (ChartPreview != null)
                    Utilities.DownloadFile(ChartPreview, "chart.jpg");
            }
        }

        protected void lbtnDownloadSourceICC_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            _presenterExport.GetSourceProfile();
            if (!IsErrorOccured)
            {
                if (SourceProfile != null)
                    Utilities.DownloadFile(SourceProfile, "SourceProfile.icc");
            }
        }

        #region IViewExport Members
        public int ProjectId { get; set; }
        public int OptimizeRunId { get; set; }
        public byte[] DeviceLinkProfile { get; set; }
        public byte[] ChartPreview { get; set; }
        public byte[] SourceProfile { get; set; }
        public Project Project { get; set; }
        #endregion
    }
}