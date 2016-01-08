using System;
using System.Linq;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ClosePrintCondition : BaseControl, IViewClosePrintCondition
    {
        #region "Declare Variable"
        private PresenterClosePrintCondition _presenterClosePrintCondition;
        private const string _recprintOptionsText = "printOptionsText";
        private const string _recoptimizeCompleteText = "optimizeCompleteText";
        private const string _recnoDLPText = "noDLPText";
        private const string _recnoSourceText = "noSourceText";

        #endregion
        public ClosePrintCondition()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
            this.PreRender += (sender, e) => this.ExceptionHandling(Page_PreRender, sender, e);
        }

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
                lblClosePrintConditionMessage.Text = string.Format(GetResourceString(_recprintOptionsText), Request.QueryString["ProjectName"]);

                if (CurrentLicenseType == ELicenseType.InkOptimization) //-------If LicenseTypeID = 3 (Ink Optimization), then they can not create optimize runs, they has to close after 1st run--------------------//
                {
                    lbtnReOptimize.Visible = false;
                }
                else if (CurrentLicenseType == ELicenseType.InkOptimizationSource) //-------If LicenseTypeID = 4 (Ink Optimization Source), then they can not create optimize runs, they has to close after 1st run--------------------//
                {
                    lbtnReOptimize.Visible = false;
                    lbtnViewLastAnalysis.Visible = false;
                    lbtnViewDetail.Visible = true;
                }
                else if (CurrentLicenseType == ELicenseType.Sync4DCertification) //---If Sync 4D Certification type of project, then they can not create optimize runs or Download DLP since there is no DLP
                {
                    lbtnReOptimize.Visible = false;
                    lbtnDownloadCurrentDLP.Visible = false;
                }
                else if (CurrentLicenseType == ELicenseType.CustomSourceProfile || CurrentLicenseType == ELicenseType.ReProfileCustomSource)  //-------If LicenseTypeID = 6 (Custom Source Profile) or LicenseTypeID = 7 (ReProfile Custom Source), then they can not create optimize runs, they has to close after 1st run--------------------//
                {
                    lbtnDownloadCurrentDLP.Visible = false;
                    lbtnReOptimize.Visible = false;
                    lbtnViewLastAnalysis.Visible = false;
                    lbtnDownloadSourceProf.Visible = true;
                    lbtnViewDetail.Visible = true;
                }
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Request.QueryString["ProjectId"] != null)
            {
                ProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
                _presenterClosePrintCondition.GetProjectDetail();
                if (!IsErrorOccured && Project != null)
                {
                    ((Label)updateProgress.FindControl("ucCreateDLP").FindControl("lblMessage")).Text = string.Format(
                        GetResourceString(_recoptimizeCompleteText),
                        Project.OptimizeRuns.Last().DlpName,
                        ((Project.OptimizeRuns != null)
                             ? Project.OptimizeRuns.Last().ReferenceChartType.Value
                             : string.Empty));
                }
            }
        }
        protected void lbtnReOptimize_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ProjectId"] != null)
            {
                ProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
                DeviceName = SessionManager.Instance.CurrentDevice.DeviceName;

                #region Verification mode - Check for querystring of DLP Sub Operation
                if (Request.QueryString["ToSubOperation"] != null && Convert.ToInt32(Request.QueryString["ToSubOperation"]) == (int)ELicenseType.None)
                {
                    //Verification mode

                    //As we also using this popup in Verification -> Optimize
                    //So while opening this page we pass querystring ToSubOperation as 0 which says we need to set current DLP sub operation as NONE

                    SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.None;
                }
                #endregion

                if (!IsErrorOccured)
                {
                    if (Request.QueryString["licenseTypeId"] != null)
                    {
                        if ((ELicenseType)Convert.ToInt16(Request.QueryString["licenseTypeId"]) == ELicenseType.Sync1D)
                            _presenterClosePrintCondition.Create1DOptimizeRun();
                        else
                            _presenterClosePrintCondition.CreateOptimizeRun();
                    }
                    if (!IsErrorOccured)
                    {
                        _presenterClosePrintCondition.GetProjectDetail();
                        if (!IsErrorOccured)
                        {
                            SessionManager.Instance.CurrentProject = Project;
                            SessionManager.Instance.CurrentOptimizeRun = Project.OptimizeRuns.Last();
                            SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Run;

                            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UniqId", "window.parent.location='Measurement.aspx';", true);
                        }
                    }
                }
            }
        }
        protected void lbtnViewLastAnalysis_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ProjectId"] != null)
            {
                #region Verification mode - Check for querystring of DLP Sub Operation
                if (Request.QueryString["ToSubOperation"] != null && Convert.ToInt32(Request.QueryString["ToSubOperation"]) == (int)ELicenseType.None)
                {
                    //Verification mode

                    //As we also using this popup in Verification -> Optimize
                    //So while opening this page we pass querystring ToSubOperation as 0 which says we need to set current DLP sub operation as NONE

                    SessionManager.Instance.CurrentDlpSubOperation = ELicenseType.None;
                }
                #endregion
                ProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
                _presenterClosePrintCondition.GetProjectDetail();
                if (!IsErrorOccured)
                {
                    SessionManager.Instance.CurrentProject = Project;
                    SessionManager.Instance.CurrentOptimizeRun = Project.OptimizeRuns.Last();
                    SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Run;
                    if (SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1)
                    {
                        SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.New;

                        if (Request.QueryString["licenseTypeId"] != null)  //----Sync4D Certification Project type
                        {
                            if ((ELicenseType)Convert.ToInt16(Request.QueryString["licenseTypeId"]) == ELicenseType.Sync4DCertification)
                                SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Edit;
                        }
                    }
                    if (((ELicenseType)Convert.ToInt16(Request.QueryString["licenseTypeId"]) == ELicenseType.Sync1D) && (SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && SessionManager.Instance.CurrentProject.ReTargetProjectID > 0)) //---If Sync 1D, ReTargeted project and in Run-0 mode only, then go directly to Export page, since there is no Analyze Page for Run - 0
                    {
                        SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Download;
                        Utilities.PopupWindowsRedirect(this, "Export.aspx");
                    }
                    else //---For all other types of projects
                    {
                        SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
                        Utilities.PopupWindowsRedirect(this, "Analyze.aspx");
                    }
                }
            }

        }

        protected void lbtnViewDetail_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["ProjectId"] != null)
            {
                ProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
                _presenterClosePrintCondition.GetProjectDetail();
                if (!IsErrorOccured)
                {
                    SessionManager.Instance.CurrentProject = Project;
                    SessionManager.Instance.CurrentOptimizeRun = Project.OptimizeRuns.Last();
                    SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Edit;
                   
                    SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Download;
                    Utilities.PopupWindowsRedirect(this, "Export.aspx");
                }
            }
        }

        protected void lbtnDownloadCurrentDLP_Click(object sender, EventArgs e)
        {
            string DLPName = "Profile.icc";
            if (Request.QueryString["DlpName"] != null)
            {
                DLPName = Request.QueryString["DlpName"].ToString();
            }

            ProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);
            if (Request.QueryString["licenseTypeId"] != null)
            {
                if ((ELicenseType)Convert.ToInt16(Request.QueryString["licenseTypeId"]) == ELicenseType.Sync1D)
                    _presenterClosePrintCondition.Get1DCurrentDeviceLinkProfile();
                else
                    _presenterClosePrintCondition.GetCurrentDeviceLinkProfile();

            }
            if (!IsErrorOccured)
            {
                //CurrentDlp = null;
                if (CurrentDlp != null)
                {
                    Utilities.DownloadFile(CurrentDlp, DLPName);
                }
                else
                {
                    fujiStatusbar.DisplayMessage(GetResourceString(_recnoDLPText), false);
                }
            }
        }
        #endregion

        #region IViewClosePrintCondition Members

        public int ProjectId { get; set; }

        public byte[] CurrentDlp { get; set; }

        public Entity.Project Project { get; set; }

        public string DeviceName { get; set; }

        #endregion

        protected void lbtnDownloadSourceProf_Click(object sender, EventArgs e)
        {
            string DLPName = Request.QueryString["ProjectName"] + ".icc";
            ProjectId = Convert.ToInt32(Request.QueryString["ProjectId"]);

            _presenterClosePrintCondition.GetCurrentDeviceLinkProfile();

            if (!IsErrorOccured)
            {
                if (CurrentDlp != null)
                {
                    Utilities.DownloadFile(CurrentDlp, DLPName);
                }
                else
                {
                    fujiStatusbar.DisplayMessage(GetResourceString(_recnoSourceText), false);
                }
            }
        }
    }
}