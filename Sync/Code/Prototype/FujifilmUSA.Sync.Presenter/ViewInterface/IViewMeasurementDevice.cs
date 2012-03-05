using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewMeasurementDevice : IViewError
    {
        MeasurementDevice SyncMeasurementDevice { get; set; }
        List<MeasurementDevice> SyncMeasurementDeviceList { get; set; }
        MeasurementDevice SaveSyncMeasurementDevice { get; set; }
        int SyncMeasurementDeviceId { get; set; }
        Dictionary<int, string> DongleList { get; set; }
        Dictionary<int, string> MeasurementDeviceTypeList { get; set; }
        int BranchId { get; set; }
    }
}
