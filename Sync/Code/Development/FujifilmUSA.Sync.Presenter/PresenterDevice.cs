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
            _viewDevice = viewSyncDevice;
        }
        public void BranchList()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewDevice.ErrorOccured;
            _viewDevice.BranchList = miscDataBusiness.GetBranchList(_viewDevice.CompanyId);
        }

        public void LicenseList()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewDevice.ErrorOccured;
            _viewDevice.LicenseList = miscDataBusiness.GetLicenseList(_viewDevice.BranchId);
        }

        public void InstrumentTypeList()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewDevice.ErrorOccured;
            _viewDevice.InstrumentTypeList = miscDataBusiness.GetInstrumentTypeList();
        }

        public void CompanyList()
        {
            var miscDataBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            miscDataBusiness.ErrorOccured += _viewDevice.ErrorOccured;
            _viewDevice.CompanyList = miscDataBusiness.GetCompanyList();
        }

        public void DeviceListById()
        {
            var deviceBusiness = BusinessFactory<IDeviceBusiness>.CreateObject();
            deviceBusiness.ErrorOccured += _viewDevice.ErrorOccured;
            _viewDevice.DeviceListById = deviceBusiness.GetDeviceListById(_viewDevice.CompanyId, _viewDevice.BranchId);
        }

        public void GetMeasurementDevices()
        {
            var measurementDeviceBusiness = BusinessFactory<IMeasurementDeviceBusiness>.CreateObject();
            measurementDeviceBusiness.ErrorOccured += _viewDevice.ErrorOccured;
            _viewDevice.MeasurementDevices = measurementDeviceBusiness.GetMeasurementDevices();
        }

        public void GetMeasurementDevice()
        {
            var measurementDeviceBusiness = BusinessFactory<IMeasurementDeviceBusiness>.CreateObject();
            measurementDeviceBusiness.ErrorOccured += _viewDevice.ErrorOccured;
            _viewDevice.MeasurementDevice = measurementDeviceBusiness.GetMeasurementDevice(_viewDevice.MeasurementDevice.MeasurementDeviceId);
        }

        public void InsertMeasurementDevice()
        {
            var measurementDeviceBusiness = BusinessFactory<IMeasurementDeviceBusiness>.CreateObject();
            measurementDeviceBusiness.ErrorOccured += _viewDevice.ErrorOccured;
            measurementDeviceBusiness.InsertMeasurementDevice(_viewDevice.MeasurementDevice);
        }

        public void UpdateMeasurementDevice()
        {
            var measurementDeviceBusiness = BusinessFactory<IMeasurementDeviceBusiness>.CreateObject();
            measurementDeviceBusiness.ErrorOccured += _viewDevice.ErrorOccured;
            measurementDeviceBusiness.UpdateMeasurementDevice(_viewDevice.MeasurementDevice);
        }

        public void DeleteMeasurementDevice()
        {
            var measurementDeviceBusiness = BusinessFactory<IMeasurementDeviceBusiness>.CreateObject();
            measurementDeviceBusiness.ErrorOccured += _viewDevice.ErrorOccured;
            measurementDeviceBusiness.DeleteMeasurementDevice(_viewDevice.MeasurementDevice.MeasurementDeviceId);
        }
    }
}