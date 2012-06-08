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
            int RunNum = 0;

            if (Request.QueryString["TotalRuns"] != null && Request.QueryString["TotalRuns"].Length > 0)
            {
                RunNum = Convert.ToInt32(Request.QueryString["TotalRuns"]) - 1;
            }
            
            var sb = new StringBuilder();
            if (Convert.ToDecimal(Request.QueryString["days"]) <= 0)
                sb.Append("Current Run " + RunNum + " is open.");
            else
                sb.Append("Current Run " + RunNum + " is open and has been inactive for " + Request.QueryString["days"] + " days.");

            sb.Append("</br>");
            if (Request.QueryString["DlpName"].Contains("-001"))
            {
                sb.Append("Currently there is no DLP.");
            }
            else
            {
                sb.Append("Confirm which DLP is used in your workflow. The current DLP is \" " +
                          Request.QueryString["DlpName"] + "\". ");   
            }
            
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