using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterSyncDevice
    {
        private readonly IViewSyncDevice _ViewSyncDevice;

        public PresenterSyncDevice(IViewSyncDevice ViewSyncDevice)
        {
            this._ViewSyncDevice = ViewSyncDevice;
        }
        public void GetSyncDeviceListByUserID()
        {
            ISyncDeviceBusiness task = (ISyncDeviceBusiness)new BusinessFactory<ISyncDeviceBusiness>().CreateObject();
            _ViewSyncDevice.GetSyncDeviceListByUserID = task.GetSyncDeviceListByUserID(_ViewSyncDevice.TaskeroUserID, _ViewSyncDevice.RoleID, _ViewSyncDevice.CompanyID, _ViewSyncDevice.BranchID);
        }
    }
}
