using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FujifilmUSA.Sync.Web.UserControl.PopupControl
{
    public partial class SpotClosePrintCondition : BaseControl, IViewClosePrintCondition
    {
        #region "Declare Variable"
        private PresenterClosePrintCondition _presenterClosePrintCondition;
        private const string _recprintOptionsText = "printOptionsText";
        private const string _recoptimizeCompleteText = "optimizeCompleteText";
        private const string _recnoNCPText = "noNCPText";
        private const string _recnoSourceText = "noSourceText";
        private const string _recspotprintOptionsText = "spotprintOptionsText";

        #endregion

        #region "Constructor"
        public SpotClosePrintCondition()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }
        #endregion

        #region Property
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
        #endregion

        #region "Event"
        protected override void OnInit(EventArgs e)
        {
            _presenterClosePrintCondition = new PresenterClosePrintCondition(this);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //lblClosePrintConditionMessage.Text = string.Format(GetResourceString(_recprintOptionsText), Request.QueryString["ProjectName"]);
                lblClosePrintConditionMessage.Text = GetResourceString(_recspotprintOptionsText);
                hdfServicePath.Value = Utilities.ReadConfigValue("BaseServiceURL");
                lbtnDownloadChart.Attributes.Add("onclick", "javascript: return DownloadChart(" + Request.QueryString["ProjectId"].ToString() + ")");
            }
        }
   

        protected void lbtnReOptimize_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ProjectId"] != null)
            {
                SpotProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
                _presenterClosePrintCondition.CreateOptimizeRun_SpotProject();
                if (!IsErrorOccured)
                {
                    _presenterClosePrintCondition.GetProjectDetail_SpotProject();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Last();
                        SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Run;
                        Utilities.PopupWindowsRedirect(this, "Spot_Measurement.aspx");
                        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UniqId", "window.parent.location='Spot_Measurement.aspx';", true);
                    }
                }
            }
        }
        protected void lbtnViewLastAnalysis_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ProjectId"] != null)
            {
                SpotProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
                _presenterClosePrintCondition.GetProjectDetail_SpotProject();
                if (!IsErrorOccured)
                {
                    SessionManager.Instance.CurrentProject = Project;
                    SessionManager.Instance.CurrentOptimizeRun = Project.SpotTargetProject.OptimizeRuns.Last();
                    SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.SpotLibrary;
                    SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
                    Utilities.PopupWindowsRedirect(this, "Analyze_SpotColor.aspx");
                }
            }
        }
     
        #endregion

        #region IViewClosePrintCondition Members

        public int ProjectId { get; set; }

        public byte[] CurrentDlp { get; set; }

        public Entity.Project Project { get; set; }

        public string DeviceName { get; set; }
        public int SpotProjectId { get; set; }

        #endregion
    }
}