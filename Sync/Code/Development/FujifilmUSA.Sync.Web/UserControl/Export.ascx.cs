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
                header.InnerText = string.Format("Download - {0}", SessionManager.Instance.CurrentOptimizeRun.DlpName);
                lbtnMeasure.Visible = (SessionManager.Instance.CurrentProject.CurrentDisplayStatus != ECurrentProjectStatus.Edit);                
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

        protected void lbtnDownloadDLP_Click(object sender, EventArgs e)
        {
            OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
            _presenterExport.GetDeviceLinkProfile();
            string DLPName = SessionManager.Instance.CurrentOptimizeRun.DlpName;
            if (SessionManager.Instance.CurrentOptimizeRun.EndDateTime.HasValue)
            {
                DLPName = SessionManager.Instance.CurrentProject.ProjectName + " DLP " + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Month + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Day + " " + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Hour + SessionManager.Instance.CurrentOptimizeRun.EndDateTime.Value.Minute;
            }
            if (!IsErrorOccured)
            {
                if (DeviceLinkProfile != null)
                    Utilities.DownloadFile(DeviceLinkProfile, string.Format("{0}", DLPName));
            }
        }

        protected void lbtnDownloadChart_Click(object sender, EventArgs e)
        {
            ReferenceChartTypeId = SessionManager.Instance.CurrentOptimizeRun.ReferenceChartType.Key;
            _presenterExport.GetChartPreview();
            if (!IsErrorOccured)
            {
                if (ChartPreview != null)
                    Utilities.DownloadFile(ChartPreview, string.Format("{0}.pdf", SessionManager.Instance.CurrentOptimizeRun.ReferenceChartType.Value), ".pdf");
                else
                    fujiStatusbar.DisplayMessage("Download chart is not available", true);
            }
            else
            {
                fujiStatusbar.DisplayMessage("Download chart is not available", true);
            }
        }

        protected void lbtnDownloadDestinationICC_Click(object sender, EventArgs e)
        {
            ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
            _presenterExport.GetDestinationProfile();
            if (!IsErrorOccured)
            {
                if (SourceProfile != null)
                    Utilities.DownloadFile(SourceProfile,
                                           string.Format("{0}.icc",
                                                         SessionManager.Instance.CurrentProject.ProjectName));
            }
        }

        #region IViewExport Members
        public int ProjectId { get; set; }
        public int OptimizeRunId { get; set; }
        public byte[] DeviceLinkProfile { get; set; }
        public byte[] ChartPreview { get; set; }
        public byte[] SourceProfile { get; set; }
        public Project Project { get; set; }
        public int ReferenceChartTypeId { get; set; }
        #endregion


        
    }
}