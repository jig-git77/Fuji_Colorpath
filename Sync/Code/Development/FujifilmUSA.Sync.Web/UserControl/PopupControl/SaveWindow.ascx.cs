using System;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public partial class SaveWindow : System.Web.UI.UserControl, IViewConfirmSave
    {
        private PresenterConfirmSave _presenterConfirmSave;
        protected void Page_Init(object sender, EventArgs e)
        {
            _presenterConfirmSave = new PresenterConfirmSave(this);
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            SessionManager.Instance.CurrentProject.ProjectName = txtProjectName.Text;
            Project = SessionManager.Instance.CurrentProject;
            _presenterConfirmSave.SaveProject();
            SessionManager.Instance.CurrentProject = Project;
            Response.Redirect("Measurement.aspx");
        }

        #region IViewConfirmSave Members
        public string ProjectName { get; set; }
        public Project Project { get; set; }
        #endregion
    }
}