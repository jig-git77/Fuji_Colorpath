using System;
using System.Linq;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Common;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using FujifilmUSA.Sync.Entity;
using System.Collections;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class ConfirmSave : BaseControl, IViewConfirmSave
    {
        #region Private variables - Globalization
        private const string _recDLPText = "dlpText";
        private const string _recOptimizeCompleteMsg = "optimizeCompleteMsg";
        #endregion

        private PresenterConfirmSave _presenterConfirmSave;
        public DateTime EndDate;

        public string DLPMessage
        {
            get
            {
                string DLPName = Hidden_ProjectName.Value + " " + GetResourceString(_recDLPText) + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Day.ToString("00") + System.DateTime.Now.Hour.ToString("00") + System.DateTime.Now.Minute.ToString("00") + ".icc";
                return
                    (SessionManager.Instance.CurrentProject != null && SessionManager.Instance.CurrentOptimizeRun != null)
                        ? string.Format(
                            GetResourceString(_recOptimizeCompleteMsg),
                            DLPName,
                            SessionManager.Instance.CurrentOptimizeRun.ReferenceChartType.Value)
                        : string.Empty;
            }
        }
        //public string NCPMessage //message for spot library project
        //{
        //    get
        //    {
        //        string DLPName = Hidden_ProjectName.Value + " " + GetResourceString(_recDLPText) + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Day.ToString("00") + System.DateTime.Now.Hour.ToString("00") + System.DateTime.Now.Minute.ToString("00") + ".icc";
        //        return
        //            (SessionManager.Instance.CurrentProject != null && SessionManager.Instance.CurrentOptimizeRun != null)
        //                ? string.Format(
        //                    GetResourceString(_recOptimizeCompleteMsg),
        //                    DLPName,
        //                    SessionManager.Instance.CurrentOptimizeRun.ReferenceChartType.Value)
        //                : string.Empty;
        //    }
        //}
        public ConfirmSave()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
            this.PreRender += (sender, e) => this.ExceptionHandling(Page_PreRender, sender, e);
        }
        protected override void OnInit(EventArgs e)
        {
            _presenterConfirmSave = new PresenterConfirmSave(this);
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            ((Label)updateProgress.FindControl("ucCreateDLP").FindControl("lblMessage")).Text = DLPMessage;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (SessionManager.Instance.CurrentProject.LicenseType != ELicenseType.SpotLibrary) //---If Sync spot library type of project
                {
                    //enable message for non-spot color project
                    locSatisfiedColorMatchMsg.Visible = true;
                    locOptimizeDLP.Visible = true;
                    Hidden_ProjectName.Value = SessionManager.Instance.CurrentProject.ProjectName;
                    //-----------------Checking the Optimization Run limit set in web.config file, when user hits on "No, Optimize" button-------------//
                    int i, TotalOptRuns = 0;
                    for (i = SessionManager.Instance.CurrentProject.OptimizeRuns.Count - 2; i > 0; i--)
                    {
                        if (SessionManager.Instance.CurrentProject.OptimizeRuns[i].Status == EOptimizeRunStatus.Closed && SessionManager.Instance.CurrentProject.OptimizeRuns[i].HasProfile)
                        {
                            TotalOptRuns = TotalOptRuns + 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                    hdOptRunLimit.Value = TotalOptRuns.ToString();

                    if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync4DCertification) //---If Sync 4D Certification type of project, then only Certify option available----//
                    {
                        lbtnOptimize.Visible = false;
                        Tr_Optimize.Visible = false;
                    }
                    //----------------If CurrentOperation = 3 (Ink Optimization), then they can not create optimize runs, they has to close run at 1st run--------------------//
                    else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.InkOptimization)
                    {
                        if (SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 2)
                        {
                            lbtnOptimize.Visible = false;
                            Tr_Optimize.Visible = false;
                        }
                    }
                    else
                    {
                        //-------------Have to check whether its previous run is Certify Run, if yes, only visible Optimize button to user,
                        // as he can not create any simultanously certify run--------------------//
                        if (SessionManager.Instance.CurrentProject.OptimizeRuns.Count > 2)
                        {
                            OptimizeRunId = Convert.ToInt32(SessionManager.Instance.CurrentOptimizeRun.MasterRunId);
                            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                                _presenterConfirmSave.Get1DLastRunStatus();
                            else
                                _presenterConfirmSave.GetLastRunStatus();

                            if (LastRunStatus == EOptimizeRunStatus.Certify)
                            {
                                lbtnYes.Visible = false;
                                Tr_Certify.Visible = false;
                            }
                        }
                    }
                }
                else
                {
                    //enable message for spot color project
                    locSatisfiedColorMatchMsgNCP.Visible = true;
                    locOptimizeNCP.Visible = true;
                    Hidden_ProjectName.Value = SessionManager.Instance.CurrentProject.SpotTargetProject.ProjectName;
                    //-----------------Checking the Optimization Run limit set in web.config file, when user hits on "No, Optimize" button-------------//
                    int i, TotalOptRuns = 0;
                    for (i = SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Count - 2; i > 0; i--)
                    {
                        if (SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns[i].Status == EOptimizeRunStatus.Closed && SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns[i].HasProfile)
                        {
                            TotalOptRuns = TotalOptRuns + 1;
                        }
                        else
                        {
                            break;
                        }
                    }
                    hdOptRunLimit.Value = TotalOptRuns.ToString();
                    //-------------Have to check whether its previous run is Certify Run, if yes, only visible Optimize button to user,
                    // as he can not create any simultanously certify run--------------------//
                    if (SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Count > 2)
                    {
                        OptimizeRunId = Convert.ToInt32(SessionManager.Instance.CurrentOptimizeRun.MasterRunId);
                        _presenterConfirmSave.GetSpotLastRunStatus();
                        if (LastRunStatus == EOptimizeRunStatus.Certify)
                        {
                            lbtnYes.Visible = false;
                            Tr_Certify.Visible = false;
                        }
                    }
                }

            }
        }

        protected void lbtnOptimize_Click(object sender, EventArgs e)
        {
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary) //---If spot library type of project
            {
                SpotProjectId = SessionManager.Instance.CurrentProject.SpotTargetProject.SpotProjectId;
                OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                ProjectStatus = Convert.ToInt32(EOptimizeRunStatus.Closed);
                _presenterConfirmSave.OptmizeSpotLibrary();

                if (!IsErrorOccured)
                {
                    _presenterConfirmSave.GetProjectDetail_SpotProject();
                    if (!IsErrorOccured)
                    {
                        SessionManager.Instance.CurrentProject = Project;
                        SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Last();
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UniqId", "window.parent.location='Export.aspx';", true);
                    }
                }
            }
            else
            { 
                ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                    _presenterConfirmSave.Get1DMeasurementsOnRunId();
                else
                    _presenterConfirmSave.GetMeasurementsOnRunId();
                if (!IsErrorOccured)
                {
                    if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                    {
                        PCSDataTypes = SessionManager.Instance.CurrentProject.PCSDataTypes;
                        DensityType = SessionManager.Instance.CurrentProject.DensityType;

                        EndDate = System.DateTime.Now;
                        ProfileDescription = string.Format("{0}.icc", SessionManager.Instance.CurrentProject.ProjectName + " " + GetResourceString(_recDLPText) + EndDate.Month.ToString("00") + EndDate.Day.ToString("00") + EndDate.Hour.ToString("00") + EndDate.Minute.ToString("00"));

                        if (SessionManager.Instance.CurrentProject.ProjectTypeId == (short)EProjectType.G7)
                            _presenterConfirmSave.CreateG7AlignmentCurve();
                        else//ISO                                                    
                            _presenterConfirmSave.CreateISOAlignmentCurve();
                    }                   
                    else
                    {

                        EndDate = System.DateTime.Now;
                        ProfileDescription = string.Format("{0}.icc", SessionManager.Instance.CurrentProject.ProjectName + " " + GetResourceString(_recDLPText) + EndDate.Month.ToString("00") + EndDate.Day.ToString("00") + EndDate.Hour.ToString("00") + EndDate.Minute.ToString("00"));

                        _presenterConfirmSave.OptimizeDlp();
                    }
                    if (!IsErrorOccured)
                    {
                        WebLogActivityManager.LogActivity(LogActivityType.OptimizeDlp, new object[] { ProfileDescription, SessionManager.Instance.CurrentProject.ProjectId },
                                                          Request.UserHostAddress);
                        _presenterConfirmSave.GetProjectDetail();
                        if (!IsErrorOccured)
                        {
                            SessionManager.Instance.CurrentProject = Project;
                            SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.OptimizeRuns.Last();
                            if (SessionManager.Instance.CurrentUser.UserType == Convert.ToInt32(UserType.XmfColorOrganizerUser))
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UniqId",
                                                                    string.Format("window.parent.location='COTransferSiteControl.aspx?projectid={0}&id={1}';",
                                                                                  SessionManager.Instance.CurrentProject.ProjectId, Guid.NewGuid()), true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "UniqId", "window.parent.location='Export.aspx';", true);
                            }
                        }
                    }
                    else
                    {
                        WebLogActivityManager.LogActivity(LogActivityType.OptimizeDlpError, new object[] { SessionManager.Instance.CurrentProject.ProjectId },
                                                             Request.UserHostAddress);
                    }
                }
            }
         
        }

        protected void lbtnYes_Click(object sender, EventArgs e)
        {
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary) //---If spot library type of project
            {
                OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                SpotProjectId = SessionManager.Instance.CurrentProject.SpotTargetProject.SpotProjectId;
                MasterRunId = Convert.ToInt32(SessionManager.Instance.CurrentOptimizeRun.MasterRunId);
                _presenterConfirmSave.CertifySpotOptimizeRun();
                if (!IsErrorOccured)
                {
                    SessionManager.Instance.CurrentOptimizeRun.Status = EOptimizeRunStatus.Certify;
                    Utilities.PopupWindowsRedirect(this, "Export.aspx");
                }
            }
            else
            { 
                OptimizeRunId = SessionManager.Instance.CurrentOptimizeRun.OptimizeRunId;
                ProjectId = SessionManager.Instance.CurrentProject.ProjectId;
                MasterRunId = Convert.ToInt32(SessionManager.Instance.CurrentOptimizeRun.MasterRunId);
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D)
                {
                    _presenterConfirmSave.Certify1DOptimizeRun();
                }
                else
                {
                    _presenterConfirmSave.CertifyOptimizeRun();
                }
                if (!IsErrorOccured)
                {
                    SessionManager.Instance.CurrentProject = null;
                    SessionManager.Instance.CurrentOptimizeRun = null;
                    if (SessionManager.Instance.CurrentUser.UserType == Convert.ToInt32(UserType.XmfColorOrganizerUser))
                    {
                        Utilities.PopupWindowsRedirect(this, string.Format("COTransferSiteControl.aspx?projectid={0}&id={1}", ProjectId, Guid.NewGuid()));
                    }
                    else
                    {
                        Utilities.PopupWindowsRedirect(this, "ProjectList.aspx");
                    }
                }
            }
          
        }

        #region IViewConfirmSave Members
        public int ProjectId { get; set; }
        public int OptimizeRunId { get; set; }
        public int MasterRunId { get; set; }
        public List<int> MeasurementIds { get; set; }
        public Entity.Project Project { get; set; }
        public string ProfileDescription { get; set; }
        public EOptimizeRunStatus LastRunStatus { get; set; }
        public PCSDataTypes PCSDataTypes { get; set; }
        public DensityTypes DensityType { get; set; }
        public int SpotProjectId { get; set; }
        public bool Is1DMeasurementSmoothing
        {
            get
            {
                return SessionManager.Instance.CurrentProject.Is1DMeasurementSmoothing;
            }
        }
        public int ProjectStatus { get; set; }
        #endregion




    }
}
