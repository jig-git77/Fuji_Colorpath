
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterDLP
    {
        private readonly IViewAddProject _viewAddProject;
        public PresenterDLP(IViewAddProject viewAddProject)
        {
            this._viewAddProject = viewAddProject;
        }

        public void GetDLPList()
        {
            var task = (IDLPBusiness)new BusinessFactory<IDLPBusiness>().CreateObject();
            _viewAddProject.GetDLPList = task.GetDLPList();

        }
    }
}
