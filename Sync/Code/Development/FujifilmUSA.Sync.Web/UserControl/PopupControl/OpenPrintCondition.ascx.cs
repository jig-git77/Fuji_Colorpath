using System;
using System.Linq;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class OpenPrintCondition : System.Web.UI.UserControl, IViewOpenPrintCondition
    {
        private PresenterOpenPrintCondition _presenterOpenPrintCondition;
        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterOpenPrintCondition = new PresenterOpenPrintCondition(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblOpenPrintConditionMessage.Text =
                string.Format(
                    "Current {0} is open and has been inactive for 2.5 days.</br>Confirm which DLP is used in your workflow. </br> In order to continue optimizing this DLP your workflow must be using {1}",
                    Request.QueryString["ProjectName"], Request.QueryString["ProjectName"]);
        }

        protected void lbtnContinue_Click(object sender, EventArgs e)
        {
            ProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
            _presenterOpenPrintCondition.GetProjectDetail();
            SessionManager.Instance.CurrentProject = Project;
            SessionManager.Instance.CurrentOptimizeRun = Project.OptimizeRuns.Last();
            Common.Utilities.PopupWindowsRedirect(this, "Analyze.aspx");
        }

        #region IViewOpenPrintCondition Members
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        #endregion


    }
}