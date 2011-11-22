using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;


namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterCompany
    {
        
        private readonly IViewSyncDevice _viewSyncDevice;

        public PresenterCompany(IViewSyncDevice viewSyncDevice)
        {
            this._viewSyncDevice = viewSyncDevice;
        }
        public void GetCompanyList()
        {
            var task = (ICompanyBusiness)new BusinessFactory<ICompanyBusiness>().CreateObject();
            _viewSyncDevice.GetCompanyList = task.GetCompanyList();
        }
    }
}
