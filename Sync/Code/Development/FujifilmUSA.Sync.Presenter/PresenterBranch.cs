using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;


namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterBranch
    {

        private readonly IViewSyncDevice _viewSyncDevice;

        public PresenterBranch(IViewSyncDevice viewSyncDevice)
        {
            this._viewSyncDevice = viewSyncDevice;
        }
        public void GetBranchList()
        {
            var task = (IBranchBusiness)new BusinessFactory<IBranchBusiness>().CreateObject();
            _viewSyncDevice.GetBranchList = task.GetBranchList(_viewSyncDevice.CompanyId);
        }
    }
}
