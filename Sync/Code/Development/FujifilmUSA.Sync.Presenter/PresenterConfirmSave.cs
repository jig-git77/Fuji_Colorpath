using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterConfirmSave
    {
        private readonly IViewConfirmSave _viewConfirmSave;
        public PresenterConfirmSave(IViewConfirmSave viewConfirmSave)
        {
            _viewConfirmSave = viewConfirmSave;
        }

        public void SaveProject()
        {
            //Apply procedure for saving a project.
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            _viewConfirmSave.Project = projectBusiness.GetProjectDetail(_viewConfirmSave.Project.ProjectId);
        }
    }
}