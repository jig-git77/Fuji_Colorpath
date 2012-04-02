using System;
using System.Linq;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using System.Text;
namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class OpenPrintCondition : BaseControl, IViewOpenPrintCondition
    {
        private PresenterOpenPrintCondition _presenterOpenPrintCondition;
        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterOpenPrintCondition = new PresenterOpenPrintCondition(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.Append("Current Run " + Convert.ToInt32(Request.QueryString["DlpNo"]) + " is open and has been inactive for " + Request.QueryString["days"] + " days.");
            sb.Append("</br>");
            sb.Append("Confirm which DLP is used in your workflow. The current DLP is \" " + Request.QueryString["ProjectName"] + " DLP " + (Convert.ToInt32(Request.QueryString["DlpNo"]) - 1) + " \" .");
            lblOpenPrintConditionMessage.Text = sb.ToString();
        }

        protected void lbtnContinue_Click(object sender, EventArgs e)
        {
            ProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
            _presenterOpenPrintCondition.GetProjectDetail();
            if (!IsErrorOccured)
            {
                SessionManager.Instance.CurrentProject = Project;
                SessionManager.Instance.CurrentOptimizeRun = Project.OptimizeRuns.Last();
                if (SessionManager.Instance.CurrentOptimizeRun.HasMeasurements)
                {
                    Utilities.PopupWindowsRedirect(this, "Analyze.aspx");
                }
                else
                {
                    Utilities.PopupWindowsRedirect(this, "Measurement.aspx");
                }
            }
        }

        #region IViewOpenPrintCondition Members
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        #endregion
    }
}