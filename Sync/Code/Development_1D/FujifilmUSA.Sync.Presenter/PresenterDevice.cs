using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;
using System.Linq;

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
            _viewDevice.BranchList = miscDataBusiness.GetBranchList(_viewDevice.CompanyId, _viewDevice.CurrentUserId);
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
            _viewDevice.CompanyList = miscDataBusiness.GetCompanyList(_viewDevice.CurrentUserId);
        }

        public void DeviceListById()
        {
            var deviceBusiness = BusinessFactory<IDeviceBusiness>.CreateObject();
            deviceBusiness.ErrorOccured += _viewDevice.ErrorOccured;
            _viewDevice.Devices = deviceBusiness.GetDeviceListById(_viewDevice.CompanyId, _viewDevice.BranchId,
                                                                          _viewDevice.CurrentUserId);
        }

        public void DeviceListByDeviceId()
        {
            var deviceBusiness = BusinessFactory<IDeviceBusiness>.CreateObject();
            deviceBusiness.ErrorOccured += _viewDevice.ErrorOccured;
            _viewDevice.SelectedDevice = deviceBusiness.GetDevice(_viewDevice.DeviceId, _viewDevice.CurrentUserId).FirstOrDefault();
        }

        public void GetMeasurementDevices()
        {
            var measurementDeviceBusiness = BusinessFactory<IMeasurementDeviceBusiness>.CreateObject();
            measurementDeviceBusiness.ErrorOccured += _viewDevice.ErrorOccured;
            _viewDevice.MeasurementDevices = measurementDeviceBusiness.GetMeasurementDevice(0, _viewDevice.CurrentUserId);
        }

        public void GetMeasurementDevice()
        {
            var measurementDeviceBusiness = BusinessFactory<IMeasurementDeviceBusiness>.CreateObject();
            measurementDeviceBusiness.ErrorOccured += _viewDevice.ErrorOccured;
            _viewDevice.MeasurementDevice = measurementDeviceBusiness.GetMeasurementDevice(_viewDevice.MeasurementDevice.MeasurementDeviceId, _viewDevice.CurrentUserId).FirstOrDefault();
        }

        public int InsertMeasurementDevice()
        {
            var measurementDeviceBusiness = BusinessFactory<IMeasurementDeviceBusiness>.CreateObject();
            measurementDeviceBusiness.ErrorOccured += _viewDevice.ErrorOccured;
            return measurementDeviceBusiness.InsertMeasurementDevice(_viewDevice.MeasurementDevice);
        }

        public int UpdateMeasurementDevice()
        {
            var measurementDeviceBusiness = BusinessFactory<IMeasurementDeviceBusiness>.CreateObject();
            measurementDeviceBusiness.ErrorOccured += _viewDevice.ErrorOccured;
            return measurementDeviceBusiness.UpdateMeasurementDevice(_viewDevice.MeasurementDevice);
        }

        public void DeleteMeasurementDevice()
        {
            var measurementDeviceBusiness = BusinessFactory<IMeasurementDeviceBusiness>.CreateObject();
            measurementDeviceBusiness.ErrorOccured += _viewDevice.ErrorOccured;
            measurementDeviceBusiness.DeleteMeasurementDevice(_viewDevice.MeasurementDevice.MeasurementDeviceId);
        }

        public bool UpdateMeasurementDeviceStatus()
        {
            var measurementDeviceBusiness = BusinessFactory<IMeasurementDeviceBusiness>.CreateObject();
            measurementDeviceBusiness.ErrorOccured += _viewDevice.ErrorOccured;
            return measurementDeviceBusiness.UpdateMeasurementDeviceStatus(_viewDevice.MeasurementDevice.MeasurementDeviceId);
        }
    }
}