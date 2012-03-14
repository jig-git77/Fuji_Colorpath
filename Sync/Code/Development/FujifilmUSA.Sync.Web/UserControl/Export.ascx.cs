using System;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Export : System.Web.UI.UserControl, IViewExport
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

        protected void lbtnMeasure_Click(object sender, EventArgs e)
        {
            Response.Redirect("Measurement.aspx");
        }

        protected void lbtnDownLoadDLP_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            _presenterExport.GetDeviceLinkProfile();
        }

        protected void lbtnDownLoadChart_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            _presenterExport.GetChartPreview();
        }

        protected void lbtnDownloadSourceICC_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            _presenterExport.GetSourceProfile();
        }

        #region IViewExport Members
        public int ProjectId { get; set; }
        public int OptimizeRunId { get; set; }
        public byte[] DeviceLinkProfile { get; set; }
        public byte[] ChartPreview { get; set; }
        public byte[] SourceProfile { get; set; }
        #endregion
    }
}
