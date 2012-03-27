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
            _viewDeviceManagement = viewDeviceManagement;
        }
        public PresenterDeviceManagement(IViewAddDeviceManagement viewAddDeviceManagement)
        {
            _viewAddDeviceManagement = viewAddDeviceManagement;
        }
        public PresenterDeviceManagement(IViewAddDeviceFamilyManagement viewAddDeviceFamilyManagement)
        {
            _viewAddDeviceFamilyManagement = viewAddDeviceFamilyManagement;
        }

        public void GetDevices()
        {
            var deviceBusiness = BusinessFactory<IDeviceBusiness>.CreateObject();
            deviceBusiness.ErrorOccured += _viewDeviceManagement.ErrorOccured;
            _viewDeviceManagement.DeviceList = deviceBusiness.GetDeviceList();
        }

        public void InsertDevice()
        {
            var deviceBusiness = BusinessFactory<IDeviceBusiness>.CreateObject();
            deviceBusiness.ErrorOccured += _viewAddDeviceManagement.ErrorOccured;
            deviceBusiness.InsertDevice(_viewAddDeviceManagement.Device);
        }
        public void InsertDeviceFamily()
        {
            var deviceBusiness = BusinessFactory<IDeviceBusiness>.CreateObject();
            deviceBusiness.ErrorOccured += _viewAddDeviceFamilyManagement.ErrorOccured;
            deviceBusiness.InsertDeviceFamily(_viewAddDeviceFamilyManagement.DeviceFamily);
        }

        public void UpdateDevice()
        {
            var deviceBusiness = BusinessFactory<IDeviceBusiness>.CreateObject();
            deviceBusiness.ErrorOccured += _viewAddDeviceManagement.ErrorOccured;
            deviceBusiness.UpdateDevice(_viewAddDeviceManagement.Device);
        }

        public void DeleteDevice()
        {
            var deviceBusiness = BusinessFactory<IDeviceBusiness>.CreateObject();
            deviceBusiness.ErrorOccured += _viewDeviceManagement.ErrorOccured;
            deviceBusiness.DeleteDevice(_viewDeviceManagement.Device.DeviceId);
        }

        public void GetCompanies()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddDeviceManagement.ErrorOccured;
            _viewAddDeviceManagement.Compnaies = miscDataBusiness.GetCompanyList();
        }
        public void GetBranches()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddDeviceManagement.ErrorOccured;
            _viewAddDeviceManagement.Branches = miscDataBusiness.GetBranchList(_viewAddDeviceManagement.CompanyId);
        }
        public void GetLicenses()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddDeviceManagement.ErrorOccured;
            _viewAddDeviceManagement.Licenses = miscDataBusiness.GetLicenseList(_viewAddDeviceManagement.BranchId);
        }
        public void GetDeviceFamilyList()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewAddDeviceManagement.ErrorOccured;
            _viewAddDeviceManagement.DeviceFamilyList = miscDataBusiness.GetDeviceFamilyList();
        }
    }
}