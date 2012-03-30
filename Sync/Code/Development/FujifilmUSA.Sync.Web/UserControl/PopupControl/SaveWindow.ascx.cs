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
        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterSaveWindow = new PresenterSaveWindow(this);
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {

            SessionManager.Instance.CurrentProject.ProjectName = txtProjectName.Text;
            SessionManager.Instance.CurrentProject.CreateDate = DateTime.Now;

            Project = SStorage = SessionManager.Instance.CurrentProject;
            _presenterSaveWindow.SaveProject();
            if (!IsErrorOccured)
            {
                Project.PrintCondition = SStorage.PrintCondition;
                Project.SourceProfile = SStorage.SourceProfile;
                Project.ProjectOption = SStorage.ProjectOption; 
                Project.DeviceFamilyProfilerOptionId = SStorage.DeviceFamilyProfilerOptionId;
                Project.MeasurementSetting = SStorage.MeasurementSetting;

                SessionManager.Instance.CurrentProject = Project;
                SessionManager.Instance.CurrentOptimizeRun = SessionManager.Instance.CurrentProject.OptimizeRuns.Last();
                SessionManager.Instance.CurrentNavigation = ELeftMenuItemLevel.Measure;
                Utilities.PopupWindowsRedirect(this, "Measurement.aspx");
            }
        }

        #region IViewSaveWindow Members
        public Project Project { get; set; }
        public Project SStorage { get; set; }
        #endregion
    }
}
