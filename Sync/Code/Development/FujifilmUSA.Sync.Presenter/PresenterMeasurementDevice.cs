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
            task.ErrorOccured += _viewMeasurementDevice.ErrorOccured;  
            task.InsertSyncMeasurementDevice(_viewMeasurementDevice.SaveSyncMeasurementDevice);
        }

        public void UpdateSyncMeasurementDevice()
        {
            var task = (IMeasurementDeviceBusiness)new BusinessFactory<IMeasurementDeviceBusiness>().CreateObject();
            task.ErrorOccured += _viewMeasurementDevice.ErrorOccured;  
            task.UpdateSyncMeasurementDevice(_viewMeasurementDevice.SaveSyncMeasurementDevice);
        }

        public void DeleteSyncMeasurementDevice()
        {
            var task = (IMeasurementDeviceBusiness)new BusinessFactory<IMeasurementDeviceBusiness>().CreateObject();
            task.ErrorOccured += _viewMeasurementDevice.ErrorOccured; 
            task.DeleteSyncMeasurementDevice(_viewMeasurementDevice.SyncMeasurementDeviceId);
        }

        public void GetSyncMeasurementDeviceByMeasurementDeviceId()
        {
            var task = (IMeasurementDeviceBusiness)new BusinessFactory<IMeasurementDeviceBusiness>().CreateObject();
            task.ErrorOccured += _viewMeasurementDevice.ErrorOccured; 
            _viewMeasurementDevice.SyncMeasurementDevice = task.GetSyncMeasurementDevice(_viewMeasurementDevice.SyncMeasurementDeviceId);
        }

        public void GetSyncMeasurementDeviceList()
        {
            var task = (IMeasurementDeviceBusiness)new BusinessFactory<IMeasurementDeviceBusiness>().CreateObject();
            task.ErrorOccured += _viewMeasurementDevice.ErrorOccured; 
            _viewMeasurementDevice.SyncMeasurementDeviceList = task.GetSyncMeasurementDevices();
        }

        public void GetDongleList()
        {
            var task = (IMiscDataBusiness)new BusinessFactory<IMiscDataBusiness>().CreateObject();
            task.ErrorOccured += _viewMeasurementDevice.ErrorOccured;  
            _viewMeasurementDevice.DongleList = task.GetDongleList(_viewMeasurementDevice.BranchId);
        }
        public void GetMeasurementDeviceTypeList()
        {
            var task = (IMiscDataBusiness)new BusinessFactory<IMiscDataBusiness>().CreateObject();
            task.ErrorOccured += _viewMeasurementDevice.ErrorOccured; 
            _viewMeasurementDevice.MeasurementDeviceTypeList = task.GetMeasurementDeviceTypeList();
        }
    }
}
