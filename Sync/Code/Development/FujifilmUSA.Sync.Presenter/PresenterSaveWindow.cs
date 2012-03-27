using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterSaveWindow
    {
        private readonly IViewSaveWindow _viewSaveWindow;

        public PresenterSaveWindow(IViewSaveWindow viewSaveWindow)
        {
            _viewSaveWindow = viewSaveWindow;
        }

        public void SaveProject()
        {
            var projectBusiness = BusinessFactory<IProjectBusiness>.CreateObject();
            projectBusiness.ErrorOccured += _viewSaveWindow.ErrorOccured;
            _viewSaveWindow.Project = projectBusiness.SaveProject(_viewSaveWindow.Project);
        }
    }
}
