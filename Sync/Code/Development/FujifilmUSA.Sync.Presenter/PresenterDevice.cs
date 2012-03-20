using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterDevice
    {
        private readonly IViewDevice _viewDevice;
        private readonly IViewAddMeasurementDevice _viewAddMeasurementDevice;

        public PresenterDevice(IViewDevice viewSyncDevice)
        {
            this._viewDevice = viewSyncDevice;
        }
        public PresenterDevice(IViewAddMeasurementDevice viewAddMeasurementDevice)
        {
            this._viewAddMeasurementDevice = viewAddMeasurementDevice;
        }

        public void BranchList()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewDevice.BranchList = task.BranchList(_viewDevice.CompanyId);
        }

        public void LicenseList()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewDevice.LicenseList = task.LicenseList(_viewDevice.BranchId);
        }

        public void InstrumentTypeList()
        {
            var task = BusinessFactory<IMiscDataBusiness>.CreateObject();
            _viewDevice.InstrumentTypeList = task.InstrumentTypeList();
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

        public void GetMeasurementDevices()
        {
            var task = BusinessFactory<IMeasurementDeviceBusiness>.CreateObject();
            _viewDevice.MeasurementDevices = task.GetMeasurementDevices();
        }

        public void GetMeasurementDevice()
        {
            var task = BusinessFactory<IMeasurementDeviceBusiness>.CreateObject();
            _viewDevice.MeasurementDevice = task.GetMeasurementDevice(_viewDevice.MeasurementDevice.MeasurementDeviceId);
        }

        public void InsertMeasurementDevice()
        {
            var task = BusinessFactory<IMeasurementDeviceBusiness>.CreateObject();
            task.InsertMeasurementDevice(_viewDevice.MeasurementDevice);
        }

        public void UpdateMeasurementDevice()
        {
            var task = BusinessFactory<IMeasurementDeviceBusiness>.CreateObject();
            task.UpdateMeasurementDevice(_viewDevice.MeasurementDevice);
        }

        public void DeleteMeasurementDevice()
        {
            var task = BusinessFactory<IMeasurementDeviceBusiness>.CreateObject();
            task.DeleteMeasurementDevice(_viewDevice.MeasurementDevice.MeasurementDeviceId);
        }



       
    }
}
