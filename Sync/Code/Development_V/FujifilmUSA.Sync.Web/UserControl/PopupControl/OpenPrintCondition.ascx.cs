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
        #region Private variables - Globalization
        private const string _reccurrentRunIsOpen = "currentRunIsOpen";
        private const string _reccurrentRunIsOpenAndInactive = "currentRunIsOpenAndInactive";
        private const string _recnoDLPText = "noDLPText";
        private const string _recconfirmDLPText = "confirmDLPText";
        #endregion

        #region Constructor
        public OpenPrintCondition()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #endregion

        private ELicenseType CurrentLicenseType
        {
            get
            {
                if (Request.QueryString["LicenseTypeId"] != null && Request.QueryString["LicenseTypeId"].Length > 0)
                    return (ELicenseType)Convert.ToInt32(Request.QueryString["LicenseTypeId"]);
                else
                    return ELicenseType.None;
            }
        }

        private PresenterOpenPrintCondition _presenterOpenPrintCondition;
        protected override void OnInit(EventArgs e)
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
                sb.Append(string.Format(GetResourceString(_reccurrentRunIsOpen), RunNum));
            else
                sb.Append(string.Format(GetResourceString(_reccurrentRunIsOpenAndInactive), RunNum, Request.QueryString["days"]));

            sb.Append("</br>");
            if (Server.UrlDecode(Request.QueryString["DlpName"]).Contains("-001"))
            {
                sb.Append(GetResourceString(_recnoDLPText));
            }
            else
            {
                sb.Append(string.Format(GetResourceString(_recconfirmDLPText), Server.UrlDecode(Request.QueryString["DlpName"])));
            }

            lblOpenPrintConditionMessage.Text = sb.ToString();
        }

        protected void lbtnContinue_Click(object sender, EventArgs e)
        {
            ProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
            SessionManager.Instance.CurrentOperation = CurrentLicenseType;
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