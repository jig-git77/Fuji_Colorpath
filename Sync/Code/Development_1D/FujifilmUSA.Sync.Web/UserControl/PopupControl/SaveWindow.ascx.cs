using System;
using System.Linq;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class SaveWindow : BaseControl, IViewSaveWindow
    {
        private PresenterSaveWindow _presenterSaveWindow;
        private const string _recDownloadChartNotAvailable = "downloadChartNotAvailable";
        private const string _recSaveAs = "saveAs";
        private const string _recSaveProjInkOptSource = "SavePrintCondition";

        public SaveWindow()
        {
            this.Load += (sender, e) => this.ExceptionHandling(Page_Load, sender, e);
        }

        #region Page Events
        protected override void OnInit(EventArgs e)
        {
            _presenterSaveWindow = new PresenterSaveWindow(this);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.InkOptimizationSource || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary) //---If  "Ink Optimization Source" / "Spot Project Library" project type, then don't display Download Chart option (since there is no charts in this type of project).
                {
                    Tr_DownloadChart.Visible = false;
                    Page.Title = GetResourceString(_recSaveProjInkOptSource);
                }

                //check the current project is spot or not
                string ProjectName = SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary ?
                                    SessionManager.Instance.CurrentProject.SpotTargetProject.ProjectName :
                                    SessionManager.Instance.CurrentProject.ProjectName;

                if (!string.IsNullOrEmpty(ProjectName))
                {
                    txtProjectName.Text = ProjectName;
                    Page.Title = GetResourceString(_recSaveAs);
                }
                //set the regex pattern for project name text box
                revProjectName.ValidationExpression = @"^(?![.\-])(?:[^\\\/:*?&'""<>|#+%$@!=;])*[^.\-\\\/:*?&'""<>|#+%$@!=;]$";
            }
        }
        #endregion

        #region "Control Events"
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.ProjectName = txtProjectName.Text;
            SessionManager.Instance.CurrentProject.CreateDate = DateTime.Now;
            CompanyId = SessionManager.Instance.SelectedCompany;
            // commented for testing existing DLP.
            //if (SessionManager.Instance.CurrentProject.PrintStandardType?.PrintStandardTypeIDOld != SessionManager.Instance.CurrentProject.PrintStandardType?.PrintStandardTypeID)
            //    SessionManager.Instance.CurrentProject.EnabledPaperScaling = false;

            SessionManager.Instance.CurrentProject.NewPaper_White_L_Old = Convert.ToDecimal(SessionManager.Instance.CurrentProject.NewPaper_White_L);
            SessionManager.Instance.CurrentProject.NewPaper_White_a_Old = Convert.ToDecimal(SessionManager.Instance.CurrentProject.NewPaper_White_a);
            SessionManager.Instance.CurrentProject.NewPaper_White_b_Old = Convert.ToDecimal(SessionManager.Instance.CurrentProject.NewPaper_White_b);

            Project = SessionManager.Instance.CurrentProject;
            if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.InkOptimizationSource) //---If  "Ink Optimization Source" project type
            {
                _presenterSaveWindow.SaveProject_InkOptSource(); //---Call save project function, w/o any ref charts details
                if (!IsErrorOccured)
                {
                    _presenterSaveWindow.GetProjectDetail();
                    WebLogActivityManager.LogActivity(LogActivityType.CreateProject, new object[] { Project.ProjectId, Project.ProjectName }, Request.UserHostAddress);
                    SessionManager.Instance.CurrentProject = Project;
                    SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.OptimizeRuns.Last();
                    SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Save;
                    Utilities.PopupWindowsRedirect(this, "InkOptSourceMiddle.aspx");
                }
            }
            else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.SpotLibrary) //---If  "Spot Target Library" project type
            {
                bool isNewSpotProject = SessionManager.Instance.CurrentProject.SpotTargetProject.SpotProjectId == 0 ? true : false;
                SessionManager.Instance.CurrentProject.SpotTargetProject.ProjectName = txtProjectName.Text;
                SessionManager.Instance.CurrentProject.CreateDate = DateTime.Now;
                _presenterSaveWindow.SaveProject_SpotTargetProject(); //---Call save project function, w/o any ref charts details
                if (!IsErrorOccured)
                {
                    _presenterSaveWindow.GetProjectDetail_SpotProject();
                    WebLogActivityManager.LogActivity(LogActivityType.CreateProject, new object[] { Project.ProjectId, Project.ProjectName }, Request.UserHostAddress);
                    SessionManager.Instance.CurrentProject = Project;
                    SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.SpotTargetProject.OptimizeRuns.Last();

                    if (isNewSpotProject)
                    {
                        SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Measure;
                        Utilities.PopupWindowsRedirect(this, "SpotMeasurement.aspx");
                    }
                    else
                    {
                        if (SessionManager.Instance.CurrentOptimizeRun.HasProfile)
                        {
                            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Download;
                            Utilities.PopupWindowsRedirect(this, "Export.aspx");
                        }
                        else if (SessionManager.Instance.CurrentOptimizeRun.HasMeasurements)
                        {
                            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
                            Utilities.PopupWindowsRedirect(this, "Analyze_SpotColor.aspx");
                        }
                        else
                        {
                            SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Measure;
                            Utilities.PopupWindowsRedirect(this, "Spot_Measurement.aspx");
                        }
                    }
                }
            }
            else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.CustomSourceProfile ||
                SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.FullGamutCustomSource) //---If  "Custom Source Profile" / "Full Gamut Source Profile" project type
            {
                SessionManager.Instance.CurrentProject.PrintStandardType = new Entity.CPSync1D.PrintStandardType()
                {
                    PrintStandardTypeID = 1 //---By default pass 1 as PrintStandard Type---//
                };
                _presenterSaveWindow.SaveProject_CustomSourceProfile(); //---Call save project function, w/o any ref charts details
                if (!IsErrorOccured)
                {
                    _presenterSaveWindow.GetProjectDetail();
                    WebLogActivityManager.LogActivity(LogActivityType.CreateProject, new object[] { Project.ProjectId, Project.ProjectName }, Request.UserHostAddress);
                    SessionManager.Instance.CurrentProject = Project;
                    AssignLeftPanel();
                }
            }
            else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.CustomBCO) //---If  "Custom BCO
            {
                SessionManager.Instance.CurrentProject.PrintStandardType = new Entity.CPSync1D.PrintStandardType()
                {
                    PrintStandardTypeID = 1 //---By default pass 1 as PrintStandard Type---//
                };
                _presenterSaveWindow.SaveProject_CustomBCO(); //---Call save project function, w/o any ref charts details
                if (!IsErrorOccured)
                {
                    _presenterSaveWindow.GetProjectDetail();
                    WebLogActivityManager.LogActivity(LogActivityType.CreateProject, new object[] { Project.ProjectId, Project.ProjectName }, Request.UserHostAddress);
                    SessionManager.Instance.CurrentProject = Project;
                    SessionManager.Instance.CurrentProject = null;
                    SessionManager.Instance.CurrentOptimizeRun = null;
                    Utilities.PopupWindowsRedirect(this, "ProjectList.aspx");
                }
            }
            else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DestinationProfile) //---If  "destination profile
            {
                _presenterSaveWindow.SaveProject_DestinationProfile(); //---Call save Project Destination Profile function, w/o any ref charts details
                if (!IsErrorOccured)
                {
                    _presenterSaveWindow.GetProjectDetail();
                    WebLogActivityManager.LogActivity(LogActivityType.CreateProject, new object[] { Project.ProjectId, Project.ProjectName }, Request.UserHostAddress);
                    SessionManager.Instance.CurrentProject = Project;
                    AssignLeftPanel();
                }
            }
            else //-----------For all other project types
            {
                _presenterSaveWindow.SaveProject();

                if (!IsErrorOccured)
                {
                    if (SessionManager.Instance.CurrentDlpSubOperation == ELicenseType.Validation)
                    {
                        _presenterSaveWindow.GetProjectDetailWithVerification();
                    }
                    else
                    {
                        _presenterSaveWindow.GetProjectDetail();

                    }
                    WebLogActivityManager.LogActivity(LogActivityType.CreateProject, new object[] { Project.ProjectId, Project.ProjectName }, Request.UserHostAddress);
                    //bool isCustomPlugin = SessionManager.Instance.CurrentProject.PrintCondition.IsCustomPlugin;
                    SessionManager.Instance.CurrentProject = Project;
                    SessionManager.Instance.CurrentProject.PrintStandardType.PrintStandardTypeIDOld = Project.PrintStandardType.PrintStandardTypeID;
                    //  SessionManager.Instance.CurrentProject.PrintCondition.IsCustomPlugin = isCustomPlugin;
                    //Disable links if project is already certified 
                    if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync4DCertification && SessionManager.Instance.CurrentProject.Status == EOptimizeRunStatus.Certify)
                    {
                        SessionManager.Instance.CurrentProject.CurrentDisplayStatus = ECurrentProjectStatus.Edit;
                    }
                    //For Sync1D Retargted projects
                    if ((SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1D || SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.Sync1DMultiColor) && SessionManager.Instance.CurrentDlpSubOperation != ELicenseType.Validation && SessionManager.Instance.CurrentProject.ReTargetProjectID > 0)
                    {
                        SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Save;
                        SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.OptimizeRuns.Last();
                        if (SessionManager.Instance.CurrentProject.OptimizeRuns != null && SessionManager.Instance.CurrentProject.OptimizeRuns.Count == 1 && !SessionManager.Instance.CurrentOptimizeRun.HasProfile)
                        {
                            Utilities.PopupWindowsRedirect(this, "InkOptSourceMiddle.aspx");
                        }
                    }
                    AssignLeftPanel();
                }
            }
        }
        protected void lbtnDownloadChart_Click(object sender, EventArgs e)
        {
            string ChartName = "";

            if (SessionManager.Instance.CurrentProject.MeasurementSetting != null && SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key > 0 && SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Key > 0)
            {
                //if (SessionManager.Instance.CurrentProject.OptimizeRuns == null || SessionManager.Instance.CurrentProject.OptimizeRuns.Count <= 1) //-----For Base DLP Chart
                if (SessionManager.Instance.CurrentProject.CurrDlpId < 1) //When no DLP load profile chart
                {
                    ReferenceChartTypeId = SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key;
                    ChartName = SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Value;
                }
                else   //----------For Optimization Chart //Load optimization chart once DLP is created
                {
                    ReferenceChartTypeId = SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Key;
                    ChartName = SessionManager.Instance.CurrentProject.MeasurementSetting.OptReferenceChartType.Value;
                }
                _presenterSaveWindow.GetChartPreview();
                if (!IsErrorOccured)
                {
                    if (ChartPreview != null)
                        Utilities.DownloadFile(ChartPreview, string.Format("{0}.pdf", ChartName), ".pdf");
                    else
                        fujiStatusbar.DisplayMessage(GetResourceString(_recDownloadChartNotAvailable), true);
                }
            }
            else if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DestinationProfile && SessionManager.Instance.CurrentProject.MeasurementSetting != null && SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key > 0) //----If project is Destination Profile License Type, since it doesn't have OptReferenceChartType = 0
            {
                ReferenceChartTypeId = SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Key;
                ChartName = SessionManager.Instance.CurrentProject.MeasurementSetting.ReferenceChartType.Value;

                _presenterSaveWindow.GetChartPreview();
                if (!IsErrorOccured)
                {
                    if (ChartPreview != null)
                        Utilities.DownloadFile(ChartPreview, string.Format("{0}.pdf", ChartName), ".pdf");
                    else
                        fujiStatusbar.DisplayMessage(GetResourceString(_recDownloadChartNotAvailable), true);
                }
            }
            else
            {
                fujiStatusbar.DisplayMessage(GetResourceString(_recDownloadChartNotAvailable), true);
            }
        }

        private void AssignLeftPanel()
        {
            SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.OptimizeRuns.Last();
            if (SessionManager.Instance.CurrentOptimizeRun.HasProfile)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Download;
                Utilities.PopupWindowsRedirect(this, "Export.aspx");
            }
            else if (SessionManager.Instance.CurrentOptimizeRun.HasMeasurements)
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Analyze;
                if (SessionManager.Instance.CurrentProject.LicenseType == ELicenseType.DestinationProfile) //---If  "destination profile
                {
                    Utilities.PopupWindowsRedirect(this, "Analyze_DestinationProfile.aspx");
                }
                else
                    Utilities.PopupWindowsRedirect(this, "Analyze.aspx");
            }
            else
            {
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Measure;
                Utilities.PopupWindowsRedirect(this, "Measurement.aspx");
            }
        }

        #endregion

        #region IViewSaveWindow Members
        public short ReferenceChartTypeId { get; set; }
        public byte[] ChartPreview { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }
        public int DeviceId { get; set; }
        public int LicenseId { get; set; }
        public int CompanyId { get; set; }
        #endregion
    }
}
