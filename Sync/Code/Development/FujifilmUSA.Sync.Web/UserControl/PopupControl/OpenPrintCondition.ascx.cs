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
            int dlpIndex = 0;
            var dlpSplits = Request.QueryString["DlpName"].Split(' ');
            if(dlpSplits[dlpSplits.Length-1].Contains(".icc"))
            {
                dlpIndex = Convert.ToInt32(dlpSplits[dlpSplits.Length - 1].Split('.')[0]);
            }
            var sb = new StringBuilder();
            if (Convert.ToDecimal(Request.QueryString["days"]) <= 0)
                sb.Append("Current Run " + dlpIndex + " is open.");
            else
                sb.Append("Current Run " + dlpIndex + " is open and has been inactive for " + Request.QueryString["days"] + " days.");
            
            sb.Append("</br>");
            sb.Append("Confirm which DLP is used in your workflow. The current DLP is \" " + Request.QueryString["DlpName"] + " \" .");
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
                Utilities.PopupWindowsRedirect(this,
                                               SessionManager.Instance.CurrentOptimizeRun.HasMeasurements
                                                   ? "Analyze.aspx"
                                                   : "Measurement.aspx");
            }
        }

        #region IViewOpenPrintCondition Members
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        #endregion
    }
}