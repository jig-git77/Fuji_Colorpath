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
            if (Request.QueryString["days"] != null && Request.QueryString["days"].Length > 0)
            {
                if (Convert.ToDecimal(Request.QueryString["days"]) <= 0)
                    sb.Append(string.Format(GetResourceString(_reccurrentRunIsOpen), RunNum));
                else
                    sb.Append(string.Format(GetResourceString(_reccurrentRunIsOpenAndInactive), RunNum, Request.QueryString["days"]));
            }
            sb.Append("</br>");
            if (Request.QueryString["DlpName"] != null && Request.QueryString["DlpName"].Length > 0)
            {
                if (Server.UrlDecode(Request.QueryString["DlpName"]).Contains("-001"))
                {
                    sb.Append(GetResourceString(_recnoDLPText));
                }
                else
                {
                    sb.Append(string.Format(GetResourceString(_recconfirmDLPText), Server.UrlDecode(Request.QueryString["DlpName"])));
                }
            }
            lblOpenPrintConditionMessage.Text = sb.ToString();
        }

        protected void lbtnContinue_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(Request.QueryString["isSpotProject"]) != "" && Convert.ToBoolean(Request.QueryString["isSpotProject"]))
            {
                SpotProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
                _presenterOpenPrintCondition.GetProjectDetail_SpotProject();
                if (!IsErrorOccured)
                {
                    SessionManager.Instance.CurrentProject = Project;
                    SessionManager.Instance.CurrentOptimizeRun = Project.SpotTargetProject.OptimizeRuns.Last();

                    if (Project.SpotTargetProject.OptimizeRuns.Count == 1)
                        Utilities.PopupWindowsRedirect(this, "Analyze_SpotColor.aspx");
                    else
                    {
                        Utilities.PopupWindowsRedirect(this,
                                                           SessionManager.Instance.CurrentOptimizeRun.HasMeasurements
                                                               ? "Analyze_SpotColor.aspx"
                                                               : "Spot_Measurement.aspx");
                    }

                    //Utilities.PopupWindowsRedirect(this,
                    //                                   SessionManager.Instance.CurrentOptimizeRun.HasMeasurements
                    //                                       ? "Analyze_SpotColor.aspx"
                    //                                       : "Spot_Measurement.aspx");
                }
            }
            else
            {
                ProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
                _presenterOpenPrintCondition.GetProjectDetail();
                if (!IsErrorOccured)
                {
                    SessionManager.Instance.CurrentProject = Project;
                    SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeIDOld = Project.PrintStandardType.PrintStandardTypeID;
                    SessionManager.Instance.CurrentOptimizeRun = Project.OptimizeRuns.Last();
                    if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D && (SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && SessionManager.Instance.CurrentProject.ReTargetProjectID > 0 && !SessionManager.Instance.CurrentOptimizeRun.HasProfile)) //---If Sync 1D, ReTargeted project and in Run-0 mode only, then go directly to Export page, since there is no Analyze Page for Run - 0
                    {
                        Utilities.PopupWindowsRedirect(this, "InkOptSourceMiddle.aspx");
                    }
                    else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.InkOptimizationSource && (SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && !SessionManager.Instance.CurrentOptimizeRun.HasProfile)) //---If Source to Source project and in Run-0 mode only, then go directly to Middle Create DLP page, since there is no Measurement and Analyze Pages
                    {
                        Utilities.PopupWindowsRedirect(this, "InkOptSourceMiddle.aspx");
                    }
                    else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DestinationProfile)
                    {
                        Utilities.PopupWindowsRedirect(this,
                                                      SessionManager.Instance.CurrentOptimizeRun.HasMeasurements
                                                          ? "Analyze_DestinationProfile.aspx"
                                                          : "Measurement.aspx");
                    }
                    else
                    {
                        Utilities.PopupWindowsRedirect(this,
                                                       SessionManager.Instance.CurrentOptimizeRun.HasMeasurements
                                                           ? "Analyze.aspx"
                                                           : "Measurement.aspx");
                    }
                }
            }
        }

        #region IViewOpenPrintCondition Members
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public int SpotProjectId { get; set; }
        #endregion
    }
}