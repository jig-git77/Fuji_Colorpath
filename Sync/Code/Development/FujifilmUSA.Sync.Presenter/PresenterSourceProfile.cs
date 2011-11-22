using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterSourceProfile
    {
        private readonly IViewAddProject _viewAddProject;
        public PresenterSourceProfile(IViewAddProject viewAddProject)
        {
            this._viewAddProject = viewAddProject;
        }

        public void GetSourceProfileList()
        {
            var task = (ISourceProfileBusiness)new BusinessFactory<ISourceProfileBusiness>().CreateObject();
            _viewAddProject.GetSourceProfileList = task.GetSourceProfileList();
        }
    }
}
