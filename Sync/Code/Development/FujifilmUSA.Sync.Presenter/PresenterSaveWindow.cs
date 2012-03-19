using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterSaveWindow
    {
        private IViewSaveWindow _viewSaveWindow;

        public PresenterSaveWindow(IViewSaveWindow viewSaveWindow)
        {
            _viewSaveWindow = viewSaveWindow;
        }

        public void SaveProject()
        {
            var task = BusinessFactory<IProjectBusiness>.CreateObject();
            _viewSaveWindow.Project = task.SaveProject(_viewSaveWindow.Project);
        }
    }
}
