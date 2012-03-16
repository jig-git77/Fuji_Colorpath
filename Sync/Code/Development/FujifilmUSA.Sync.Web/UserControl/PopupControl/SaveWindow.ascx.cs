using System;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class SaveWindow : System.Web.UI.UserControl, IViewSaveWindow
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
            Project = SessionManager.Instance.CurrentProject;
            _presenterSaveWindow.SaveProject();
            SessionManager.Instance.CurrentProject = Project;
            Common.Utilities.PopupWindowsRedirect(this, "Measurement.aspx");
        }

        #region IViewSaveWindow Members
        public Project Project { get; set; }
        #endregion
    }
}