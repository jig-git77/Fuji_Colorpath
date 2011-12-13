using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterMeasurementDevice
    {
        private readonly IViewMeasurementDevice _viewMeasurementDevice;
        public PresenterMeasurementDevice(IViewMeasurementDevice viewMeasurementDevice)
        {
            this._viewMeasurementDevice = viewMeasurementDevice;
        }

        public void InsertSyncMeasurementDevice()
        {
            var task = (IMeasurementDeviceBusiness)new BusinessFactory<IMeasurementDeviceBusiness>().CreateObject();
            _viewMeasurementDevice.SyncMeasurementDeviceResult = task.InsertSyncMeasurementDevice(_viewMeasurementDevice.SaveSyncMeasurementDevice);
        }

        public void UpdateSyncMeasurementDevice()
        {
            var task = (IMeasurementDeviceBusiness)new BusinessFactory<IMeasurementDeviceBusiness>().CreateObject();
            _viewMeasurementDevice.SyncMeasurementDeviceResult = task.UpdateSyncMeasurementDevice(_viewMeasurementDevice.SaveSyncMeasurementDevice);
        }

        public void DeleteSyncMeasurementDevice()
        {
            var task = (IMeasurementDeviceBusiness)new BusinessFactory<IMeasurementDeviceBusiness>().CreateObject();
            _viewMeasurementDevice.SyncMeasurementDeviceResult = task.DeleteSyncMeasurementDevice(_viewMeasurementDevice.SyncMeasurementDeviceId);
        }

        public void GetSyncMeasurementDeviceByMeasurementDeviceId()
        {
            var task = (IMeasurementDeviceBusiness)new BusinessFactory<IMeasurementDeviceBusiness>().CreateObject();
            _viewMeasurementDevice.SyncMeasurementDevice = task.GetSyncMeasurementDeviceByMeasurementDeviceId(_viewMeasurementDevice.SyncMeasurementDeviceId);
        }

        public void GetSyncMeasurementDeviceList()
        {
            var task = (IMeasurementDeviceBusiness)new BusinessFactory<IMeasurementDeviceBusiness>().CreateObject();
            _viewMeasurementDevice.SyncMeasurementDeviceList = task.GetSyncMeasurementDeviceList();
        }

        public void GetDongleList()
        {
            var task = (IMeasurementDeviceBusiness)new BusinessFactory<IMeasurementDeviceBusiness>().CreateObject();
            _viewMeasurementDevice.DongleList = task.GetDongleList(_viewMeasurementDevice.BranchId);
        }
    }
}
