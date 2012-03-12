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

        public void GetBranchList()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
           // task.ErrorOccured += _viewSyncDevice.ErrorOccured;
            _viewSyncDevice.GetBranchList = task.GetBranchList(_viewSyncDevice.CompanyId);
        }

        public void GetCompanyList()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
           // task.ErrorOccured += _viewSyncDevice.ErrorOccured;
            _viewSyncDevice.GetCompanyList = task.GetCompanyList();
        }

        public void GetSyncDeviceListByUserId()
        {
            var task = BusinessFactory<ISyncDeviceBusiness>.CreateObject();
            //task.ErrorOccured += _viewSyncDevice.ErrorOccured;
            _viewSyncDevice.GetSyncDeviceListByUserId = task.GetSyncDevicesByUserId(_viewSyncDevice.TaskeroUserId, _viewSyncDevice.RoleId, _viewSyncDevice.CompanyId, _viewSyncDevice.BranchId);
        }

       
    }
}
