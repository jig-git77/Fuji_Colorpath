using System;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using System.Linq;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class LeftMenuProjectItem : BaseControl, IViewLeftMenuProjectItem
    {
        #region Variable Declarations
        public ELeftMenuItemLevel ItemLevel { get; set; }
        public string ItemName { get; set; }
        public OptimizeRun CurrentRun { get; set; }
        #endregion

        #region "Declare Variable"
        private PresenterLeftMenuProjectItem _presenterLeftMenuProjectItem;
        #endregion

        #region Constructor
        public LeftMenuProjectItem()
        {
            this.PreRender += (sender, e) => this.ExceptionHandling(Page_PreRender, sender, e);
        }
        #endregion

        #region "Page Events"

        protected override void OnInit(EventArgs e)
        {
            _presenterLeftMenuProjectItem = new PresenterLeftMenuProjectItem(this);
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            lnkRunName.Text = ItemName;
            //check the current project type
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary)
            {
                switch (ItemLevel)
                {
                    case ELeftMenuItemLevel.OptimizeRun:
                        if (CurrentRun.Status == EOptimizeRunStatus.Closed)
                        {
                            lnkMeasurement.Visible = false;
                        }
                        //----------If current run is closed (certify) run-----//
                        else if (CurrentRun.Status == EOptimizeRunStatus.Certify)
                        {
                            lnkMeasurement.Visible = false;
                            //lnkDownload.Visible = false;

                            //------If current run is closed (certify) and it is last run of this project--------//
                            if (SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId ==
                                SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Last().OptimizeRunId)
                            {
                                lnkAddRun.Visible = true;
                            }
                        }

                        if (SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Count > 1)
                        {
                            //---------------Display Delete button for last run----------------//
                            if (CurrentRun.OptimizeRunId == SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Last().OptimizeRunId)
                            {
                                btnRunDelete.Visible = true;
                                //---------------If last run has already created DLP, invisible measurement link---------//
                                if (CurrentRun.HasProfile)
                                {
                                    lnkMeasurement.Visible = false;
                                    //------------Only visible DeleteDLP button, if DLP is created for this last run---------//
                                    btnDLPDelete.Visible = true;
                                    btnDLPDelete.ToolTip = (string)GetLocalResourceObject("btnNCPDeleteResource1");
                                }
                                else
                                {
                                    //-------------It is a certify run, don't give option to delete if it is Run-1-------------//
                                    if (SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Count == 2 &&
                                        (SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Last().Status == EOptimizeRunStatus.Closed ||
                                         SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Last().Status == EOptimizeRunStatus.Certify))
                                    {
                                        btnRunDelete.Visible = false;
                                    }
                                }
                            }
                        }

                        if (CurrentRun != null && SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId == CurrentRun.OptimizeRunId)
                        {
                            ulSubMenu.Visible = true;
                            lnkRunName.Font.Bold = true;
                            lnkMeasurement.Font.Bold = SessionManager.Instance.CurrentNavigation == ELeftMenuItemLevel.Measure;
                            lnkAnalyse.Font.Bold = SessionManager.Instance.CurrentNavigation == ELeftMenuItemLevel.Analyze;
                            lnkDownload.Font.Bold = SessionManager.Instance.CurrentNavigation == ELeftMenuItemLevel.Download;
                            lnkAnalyse.Enabled = lnkDownload.Enabled = CurrentRun.HasMeasurements;
                            lnkDownload.Enabled = CurrentRun.HasProfile || CurrentRun.Status == EOptimizeRunStatus.Certify;
                        }
                        else
                        {
                            ulSubMenu.Visible = false;
                        }
                        break;
                    case ELeftMenuItemLevel.Validation:
                        lnkDownload.Visible = false;
                        if (SessionManager.Instance.CurrentProject.SpotTargetProject.CurrentDisplayStatus == ECurrentProjectStatus.Run)
                        {
                            ulSubMenu.Visible = true;
                            lnkRunName.Font.Bold = true;
                            lnkMeasurement.Font.Bold = SessionManager.Instance.CurrentNavigation == ELeftMenuItemLevel.Measure;
                            lnkAnalyse.Font.Bold = SessionManager.Instance.CurrentNavigation == ELeftMenuItemLevel.Analyze;
                            lnkAnalyse.Enabled = lnkDownload.Enabled = CurrentRun.HasMeasurements;
                        }
                        else
                        {
                            ulSubMenu.Visible = false;
                        }
                        break;
                }
            }
            else
            {
                switch (ItemLevel)
                {
                    case ELeftMenuItemLevel.OptimizeRun:
                        if (CurrentRun.Status == EOptimizeRunStatus.Closed)
                        {
                            lnkMeasurement.Visible = false;
                        }
                        //----------If current run is closed (certify) run-----//
                        else if (CurrentRun.Status == EOptimizeRunStatus.Certify)
                        {
                            lnkMeasurement.Visible = false;
                            lnkDownload.Visible = false;

                            //----------------If LicenseTypeID != 3 (Ink Optimization)----------------//
                            if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.InkOptimization)
                            {
                                //------If current run is closed (certify) and it is last run of this project--------//
                                if (SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId ==
                                    SessionManager.Instance.CurrentProject.OptimizeRuns.Last().OptimizeRunId)
                                {
                                    lnkAddRun.Visible = true;
                                }
                            }
                        }

                        if (SessionManager.Instance.CurrentProject.OptimizeRuns.Count > 1)
                        {
                            //---------------Display Delete button for last run----------------//
                            if (CurrentRun.OptimizeRunId == SessionManager.Instance.CurrentProject.OptimizeRuns.Last().OptimizeRunId)
                            {
                                btnRunDelete.Visible = true;
                                //---------------If last run has already created DLP, invisible measurement link---------//
                                if (CurrentRun.HasProfile)
                                {
                                    lnkMeasurement.Visible = false;
                                    //------------Only visible DeleteDLP button, if DLP is created for this last run---------//
                                    btnDLPDelete.Visible = true;
                                }
                                else
                                {
                                    //-------------It is a certify run, don't give option to delete if it is Run-1-------------//
                                    if (SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 2 &&
                                        (SessionManager.Instance.CurrentProject.OptimizeRuns.Last().Status == EOptimizeRunStatus.Closed ||
                                         SessionManager.Instance.CurrentProject.OptimizeRuns.Last().Status == EOptimizeRunStatus.Certify))
                                    {
                                        btnRunDelete.Visible = false;
                                    }
                                }
                            }
                        }

                        if (CurrentRun != null && SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId == CurrentRun.OptimizeRunId)
                        {
                            ulSubMenu.Visible = true;
                            lnkRunName.Font.Bold = true;
                            lnkMeasurement.Font.Bold = SessionManager.Instance.CurrentNavigation == ELeftMenuItemLevel.Measure;
                            lnkAnalyse.Font.Bold = SessionManager.Instance.CurrentNavigation == ELeftMenuItemLevel.Analyze;
                            lnkDownload.Font.Bold = SessionManager.Instance.CurrentNavigation == ELeftMenuItemLevel.Download;
                            lnkAnalyse.Enabled = lnkDownload.Enabled = CurrentRun.HasMeasurements;
                            lnkDownload.Enabled = CurrentRun.HasProfile;
                        }
                        else
                        {
                            ulSubMenu.Visible = false;
                        }
                        break;
                    case ELeftMenuItemLevel.Validation:
                        lnkDownload.Visible = false;
                        if (SessionManager.Instance.CurrentProject.CurrentDisplayStatus == ECurrentProjectStatus.Run)
                        {
                            ulSubMenu.Visible = true;
                            lnkRunName.Font.Bold = true;
                            lnkMeasurement.Font.Bold = SessionManager.Instance.CurrentNavigation == ELeftMenuItemLevel.Measure;
                            lnkAnalyse.Font.Bold = SessionManager.Instance.CurrentNavigation == ELeftMenuItemLevel.Analyze;
                            lnkAnalyse.Enabled = lnkDownload.Enabled = CurrentRun.HasMeasurements;
                        }
                        else
                        {
                            ulSubMenu.Visible = false;
                        }
                        break;
                }
            }

            lnkMeasurement.ForeColor = lnkMeasurement.Enabled ? System.Drawing.Color.Black : System.Drawing.Color.Silver;
            lnkAnalyse.ForeColor = lnkAnalyse.Enabled ? System.Drawing.Color.Black : System.Drawing.Color.Silver;
            lnkDownload.ForeColor = lnkDownload.Enabled ? System.Drawing.Color.Black : System.Drawing.Color.Silver;
            if (lnkAddRun.Visible)
            {
                lnkAddRun.Font.Underline = true;
                lnkAddRun.ForeColor = System.Drawing.Color.Blue;
            }
        }

        #endregion

        #region Control events
        protected void lnkItem_Click(object sender, EventArgs e)
        {
            if (sender != null)
            {
                var lnkItem = sender as LinkButton;
                if (lnkItem != null)
                {
                    SessionManager.Instance.CurrentOptimizeRun = this.CurrentRun;
                    SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Run;
                    if (lnkItem.ID == "lnkMeasurement")
                    {
                        if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.SpotLibrary)
                            Response.Redirect("Measurement.aspx");
                        else
                            Response.Redirect("Spot_Measurement.aspx");
                    }
                    else if (lnkItem.ID == "lnkAnalyse")
                    {
                        if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.SpotLibrary)
                            Response.Redirect("Analyze.aspx");
                        else
                            Response.Redirect("Analyze_SpotColor.aspx");
                    }
                    else if (lnkItem.ID == "lnkDownload")
                    {
                        Response.Redirect("Export.aspx");
                    }
                }
            }
        }

        protected void lnkRunName_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Run;

            switch (ItemLevel)
            {
                case ELeftMenuItemLevel.OptimizeRun:
                    SessionManager.Instance.CurrentOptimizeRun = this.CurrentRun;
                    if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.SpotLibrary)
                        Response.Redirect(
                            CurrentRun.HasMeasurements
                                ? "Analyze.aspx"
                                : "Measurement.aspx");
                    else
                        Response.Redirect(
                        CurrentRun.HasMeasurements
                            ? "Analyze_SpotColor.aspx"
                            : "Spot_Measurement.aspx");

                    break;
            }
        }
        #endregion

        protected void lnkAddRun_Click(object sender, EventArgs e)
        {

            if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.SpotLibrary)
            {
                ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                    _presenterLeftMenuProjectItem.Create1DOptimizeRun();
                else
                    _presenterLeftMenuProjectItem.CreateOptimizeRun();
                if (!IsErrorOccured)
                {
                    _presenterLeftMenuProjectItem.GetProjectDetail();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = Project.OptimizeRuns.Last();
                        SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Run;
                        Response.Redirect("~/Measurement.aspx");
                    }
                }
            }
            else
            {
                SpotProjectId = SessionManager.Instance.CurrentProject.SpotTargetProject.SpotProjectId;
                _presenterLeftMenuProjectItem.CreateOptimizeRun_SpotProject();
                if (!IsErrorOccured)
                {
                    _presenterLeftMenuProjectItem.GetProjectDetail_SpotProject();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Last();
                        SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Run;
                        Response.Redirect("Spot_Measurement.aspx");
                    }
                }
            }

        }

        protected void btnRunDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {

            if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.SpotLibrary)
            {
                //--------------Delete the current run and move control to analyze page of last optimized run for this project-------------//
                ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                OptimizeRunID = SessionManager.Instance.CurrentProject.OptimizeRuns.Last().OptimizeRunId;
                MasterRunID = Convert.ToInt32(SessionManager.Instance.CurrentOptimizeRun.MasterRunId);
                RunStatus = SessionManager.Instance.CurrentProject.OptimizeRuns.Last().Status;
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                    _presenterLeftMenuProjectItem.Delete1DOptimizeRun();
                else
                    _presenterLeftMenuProjectItem.DeleteOptimizeRun();

                if (!IsErrorOccured)
                {
                    _presenterLeftMenuProjectItem.GetProjectDetail();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = Project.OptimizeRuns.Last();
                        if (Project.OptimizeRuns.Count > 1)
                            SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Run;
                        else
                            SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.New;

                        if ((SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D) && (SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && SessionManager.Instance.CurrentProject.ReTargetProjectID > 0)) //---If Sync 1D, ReTargeted project and in Run-0 mode only, then go directly to Export page, since there is no Analyze Page for Run - 0
                            Response.Redirect("~/Export.aspx");
                        else
                            Response.Redirect("~/Analyze.aspx");
                    }
                }
            }
            else
            {
                //current project spot library project
                //--------------Delete the current run and move control to analyze page of last optimized run for this project-------------//
                SpotProjectId = SessionManager.Instance.CurrentProject.SpotTargetProject.SpotProjectId;
                OptimizeRunID = SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Last().OptimizeRunId;
                MasterRunID = Convert.ToInt32(SessionManager.Instance.CurrentOptimizeRun.MasterRunId);
                RunStatus = SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Last().Status;

                _presenterLeftMenuProjectItem.DeleteSpotOptimizeRun();
                if (!IsErrorOccured)
                {
                    _presenterLeftMenuProjectItem.GetProjectDetail_SpotProject();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = Project.SpotTargetProject.OptimizeRuns.Last();
                        if (Project.SpotTargetProject.OptimizeRuns.Count > 1)
                            SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Run;
                        else
                            SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.New;

                        if (SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Count == 1)
                            Response.Redirect("~/Export.aspx");
                        else
                            Response.Redirect("~/Analyze_SpotColor.aspx");
                    }
                }
            }
        }

        #region IViewLeftMenuProjectItem Members

        public int ProjectId { get; set; }

        public Entity.Project Project { get; set; }

        public int OptimizeRunID { get; set; }

        public int MasterRunID { get; set; }

        public EOptimizeRunStatus RunStatus { get; set; }

        public int SpotProjectId { get; set; }
        #endregion

        protected void btnDLPDelete_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            //--------------Delete the current run and move control to analyze page of last optimized run for this project-------------//            
            if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.SpotLibrary)
            {
                ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                OptimizeRunID = SessionManager.Instance.CurrentProject.OptimizeRuns.Last().OptimizeRunId;
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                    _presenterLeftMenuProjectItem.Delete1DOptimizeRunDLP();
                else
                    _presenterLeftMenuProjectItem.DeleteOptimizeRunDLP();

                if (!IsErrorOccured)
                {
                    _presenterLeftMenuProjectItem.GetProjectDetail();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = Project.OptimizeRuns.Last();
                        if (Project.OptimizeRuns.Count > 1)
                        {
                            SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Run;
                        }
                        else
                        {
                            SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.New;
                        }
                        Response.Redirect("~/Analyze.aspx");
                    }
                }
            }
            else
            {
                //current project spot library project
                //--------------Delete the current run and move control to analyze page of last optimized run for this project-------------//
                SpotProjectId = SessionManager.Instance.CurrentProject.SpotTargetProject.SpotProjectId;
                OptimizeRunID = SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Last().OptimizeRunId;
                _presenterLeftMenuProjectItem.DeleteSpotOptimizeRunDLP();
                if (!IsErrorOccured)
                {
                    _presenterLeftMenuProjectItem.GetProjectDetail_SpotProject();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = Project.SpotTargetProject.OptimizeRuns.Last();
                        if (Project.SpotTargetProject.OptimizeRuns.Count > 1)
                            SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Run;
                        else
                            SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.New;

                        Response.Redirect("~/Analyze_SpotColor.aspx");
                    }
                }
            }
        }
    }
}