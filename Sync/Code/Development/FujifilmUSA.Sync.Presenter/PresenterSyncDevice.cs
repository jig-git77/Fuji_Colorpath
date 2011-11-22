using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterSyncDevice
    {
        private readonly IViewSyncDevice _viewSyncDevice;

        public PresenterSyncDevice(IViewSyncDevice viewSyncDevice)
        {
            this._viewSyncDevice = viewSyncDevice;
        }
        public void GetSyncDeviceListByUserId()
        {
            var task = (ISyncDeviceBusiness)new BusinessFactory<ISyncDeviceBusiness>().CreateObject();
            _viewSyncDevice.GetSyncDeviceListByUserId = task.GetSyncDeviceListByUserId(_viewSyncDevice.TaskeroUserId, _viewSyncDevice.RoleId, _viewSyncDevice.CompanyId, _viewSyncDevice.BranchId);
        }
    }
}
