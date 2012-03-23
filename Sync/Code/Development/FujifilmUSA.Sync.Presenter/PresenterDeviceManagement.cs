using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterDeviceManagement 
    {
        private readonly IViewDeviceManagement _viewDeviceManagement;
        private readonly IViewAddDeviceManagement _viewAddDeviceManagement;
        private readonly IViewAddDeviceFamilyManagement _viewAddDeviceFamilyManagement;

        public PresenterDeviceManagement(IViewDeviceManagement viewDeviceManagement)
        {
            this._viewDeviceManagement = viewDeviceManagement;
        }
        public PresenterDeviceManagement(IViewAddDeviceManagement viewAddDeviceManagement)
        {
            this._viewAddDeviceManagement = viewAddDeviceManagement;
        }
        public PresenterDeviceManagement(IViewAddDeviceFamilyManagement viewAddDeviceFamilyManagement)
        {
            this._viewAddDeviceFamilyManagement = viewAddDeviceFamilyManagement;
        }


        public void GetDevices()
        {
            var task = BusinessFactory<IDeviceBusiness>.CreateObject();
            _viewDeviceManagement.DeviceList = task.DeviceList();
        }

        public void InsertDevice()
        {
            var task = BusinessFactory<IDeviceBusiness>.CreateObject();
            task.InsertDevice(_viewAddDeviceManagement.Device);
        }
        public void InsertDeviceFamily()
        {
            var task = BusinessFactory<IDeviceBusiness>.CreateObject();
            task.InsertDeviceFamily(_viewAddDeviceFamilyManagement.DeviceFamily);
        }

        public void UpdateDevice()
        {
            var task = BusinessFactory<IDeviceBusiness>.CreateObject();
            task.UpdateDevice(_viewAddDeviceManagement.Device);
        }

        public void DeleteDevice()
        {
            var task = BusinessFactory<IDeviceBusiness>.CreateObject();
            task.DeleteDevice(_viewDeviceManagement.Device.DeviceId);

        }

        public void GetCompanies()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewAddDeviceManagement.Compnaies = task.CompanyList();
        }
        public void GetBranches()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewAddDeviceManagement.Branches = task.BranchList(_viewAddDeviceManagement.CompanyId);
        }
        public void GetLicenses()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewAddDeviceManagement.Licenses = task.LicenseList(_viewAddDeviceManagement.BranchId);
        }
        public void GetDeviceFamilyList()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewAddDeviceManagement.DeviceFamilyList = task.DeviceFamilyList();
        }

    }
}
