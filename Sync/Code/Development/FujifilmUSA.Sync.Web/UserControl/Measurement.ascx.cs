using System;
using System.Linq;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Measurement : BaseControl
    {
        #region Private Variables - Globalization
        private const string _recnewPrintConditionText = "newPrintConditionText";
        private const string _recmeasureText = "measureText";
        #endregion

        #region Declaration
        public string AddMeasurement_DeleteMeasurement_Confirmation
        {
            get
            {
                return ResourceManager.GetResourceMessage("AddMeasurement_DeleteMeasurement_Confirmation");
            }
        }

        public bool IsXMFUser
        {
            get
            {
                return (SessionManager.Instance.CurrentUser.UserType == Convert.ToInt32(UserType.XmfColorOrganizerUser));
            }
        }
        #endregion

        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Measure;
                if (SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId == SessionManager.Instance.CurrentProject.OptimizeRuns.First().OptimizeRunId)
                {
                    header.InnerText = GetResourceString(_recnewPrintConditionText); //"New Print Condition - Measure (No DLP)";
                }
                else
                {
                    header.InnerText = string.Format(GetResourceString(_recmeasureText), SessionManager.Instance.CurrentOptimizeRun.LastDlpName);//Measure - {0}
                }
            }

            if (IsXMFUser && !string.IsNullOrEmpty(Request.QueryString["projectid"]))
            {
                ScriptManager.RegisterStartupScript(
                    this.Page,
                    this.Page.GetType(),
                    "messaging",
                    string.Format("window.parent.postMessage('{0}','{1}');",
                                  !string.IsNullOrEmpty(Request.QueryString["projectid"])
                                      ? Convert.ToInt32(Request.QueryString["projectid"])
                                      : 0,
                                  SessionManager.Instance.CurrentColorOrgRequest.SilverlightPostUrl),
                    true);
            }
        }

        #endregion

        #region Control Events
        protected void btnAnalyze_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.OptimizeRuns[0].HasMeasurements = true;
            SessionManager.Instance.CurrentOptimizeRun.HasMeasurements = true;
            Response.Redirect(
                SessionManager.Instance.CurrentUser.UserType == Convert.ToInt32(UserType.XmfColorOrganizerUser)
                    ? string.Format("COAnalyze.aspx?id={0}", Guid.NewGuid())
                    : "Analyze.aspx");
        }

        #endregion

    }
}
