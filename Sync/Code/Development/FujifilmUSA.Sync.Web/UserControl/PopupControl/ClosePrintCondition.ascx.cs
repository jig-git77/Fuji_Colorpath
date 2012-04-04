using System;
using System.Linq;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ClosePrintCondition : BaseControl, IViewClosePrintCondition
    {
        private PresenterClosePrintCondition _presenterClosePrintCondition;

        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterClosePrintCondition = new PresenterClosePrintCondition(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblClosePrintConditionMessage.Text = string.Format("Options for {0} print condition", Request.QueryString["ProjectName"]);
            }
        }

        protected void lbtnReOptimize_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ProjectId"] != null)
            {
                ProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
                _presenterClosePrintCondition.CreateOptimizeRun();
                if (!IsErrorOccured)
                {
                    _presenterClosePrintCondition.GetProjectDetail();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = Project.OptimizeRuns.Last();
                        SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Run;
                        Utilities.PopupWindowsRedirect(this, "Measurement.aspx");
                    }
                }
            }
        }

        protected void lbtnDownloadCurrentDLP_Click(object sender, EventArgs e)
        {
            ProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
            _presenterClosePrintCondition.GetCurrentDeviceLinkProfile();
            if (!IsErrorOccured)
            {
                //CurrentDlp = null;
                if (CurrentDlp != null)
                {
                    Utilities.DownloadFile(CurrentDlp, "Profile.icc");
                }
                else
                {
                    fujiStatusbar.DisplayMessage("Sorry!There is no dlp to download", false);
                }
            }
        }

        #region IViewClosePrintCondition Members

        public int ProjectId { get; set; }

        public byte[] CurrentDlp { get; set; }

        public Entity.Project Project { get; set; }

        #endregion
    }
}