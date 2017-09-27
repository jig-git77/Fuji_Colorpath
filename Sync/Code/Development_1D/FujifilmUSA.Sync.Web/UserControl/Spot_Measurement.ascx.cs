using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class Spot_Measurement : BaseControl
    {
        #region Private Variables - Globalization
        private const string _recnewPrintConditionText = "newPrintConditionText";
        private const string _recmeasureText = "measureText";
        private const string _recValidationMeasure = "validationMeasureText";
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

        #region Constructor
        public Spot_Measurement()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #endregion

        #region "Page Events"
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Measure;
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.CustomSourceProfile || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.ReProfileCustomSource || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.FullGamutCustomSource) //---If custom Source profile / ReProfile Custom Source / Full Gamut Custom Source, then directly pass on to next middle page instead of Analyze page
                {
                    btnCustomSourceProfile.Visible = true;
                    btnAnalyze.Visible = false;
                }
                else
                {
                    btnCustomSourceProfile.Visible = false;
                }
                if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                {
                    header.InnerText = GetResourceString(_recValidationMeasure); //"Validation - Measure";
                }
                else
                {
                    if (SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId == SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.First().OptimizeRunId)
                    {
                        header.InnerText = GetResourceString(_recnewPrintConditionText); //"New Print Condition - Measure (No DLP)";
                    }
                    else
                    {
                        header.InnerText = string.Format(GetResourceString(_recmeasureText), SessionManager.Instance.CurrentOptimizeRun.LastDlpName);//Measure - {0}
                    }
                }
                if (IsXMFUser)
                {
                    if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                    {
                        btnValidationSetup.Visible = true; //---If Validation mode, visible Validation Setup button
                    }
                    else
                    {
                        btnMeasurementSetup.Visible = true; //---Otherwise, visible Measurement Setup button
                    }
                    if (SessionManager.Instance.CurrentDlpSubOperation != ELicenseType.Validation && !string.IsNullOrEmpty(Request.QueryString["projectid"]) && Convert.ToInt32(Request.QueryString["projectid"]) > 0)
                    {
                        ScriptManager.RegisterStartupScript(
                            this.Page,
                            this.Page.GetType(),
                            "messaging",
                            string.Format("window.parent.postMessage('Action: Project Created. ProjectId:{0}','{1}');",
                                          Convert.ToInt32(Request.QueryString["projectid"]),
                                          SessionManager.Instance.CurrentColorOrgRequest.SilverlightPostUrl),
                            true);
                    }
                }
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

        protected void btnValidationSetup_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("COValidationSetup.aspx?id={0}", Guid.NewGuid()));
        }

        protected void btnMeasurementSetup_Click(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("COMeasurementSetup.aspx?id={0}", Guid.NewGuid()));
        }

        protected void btnCustomSourceProfile_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.OptimizeRuns[0].HasMeasurements = true;
            SessionManager.Instance.CurrentOptimizeRun.HasMeasurements = true;
            Response.Redirect(string.Format("InkOptSourceMiddle.aspx"));
        }

        #endregion      
    }
}