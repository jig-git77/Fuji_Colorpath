using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterDevice
    {
        private readonly IViewDevice _viewDevice;

        public PresenterDevice(IViewDevice viewSyncDevice)
        {
            this._viewDevice = viewSyncDevice;
        }

        public void BranchList()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewDevice.BranchList = task.BranchList(_viewDevice.CompanyId);
        }

        public void CompanyList()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewDevice.CompanyList = task.CompanyList();
        }

        public void DeviceListById()
        {
            var task = BusinessFactory<IDeviceBusiness>.CreateObject();
            _viewDevice.DeviceListById = task.DeviceListById(_viewDevice.CompanyId, _viewDevice.BranchId);
        }

       
    }
}
