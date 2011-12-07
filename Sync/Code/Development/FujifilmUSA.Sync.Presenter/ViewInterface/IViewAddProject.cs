using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddProject
    {
        Dictionary<int, string> GetSourceProfileList { get; set; }
        Dictionary<int, string> GetDestinationProfileList { get; set; }

        int SourceProfileId { get; set; }
        int DestinationProfileId { get; set; }

        List<DeviceLinkProfile> GetDeviceLinkProfileList { get; set; }
        int SyncDeviceId { get; set; }
        Dictionary<string , string> GetMeasurementDeviceListBySyncDeviceId { get; set; }

        int MeasurementDeviceId { get; set; }
        Dictionary<int, string> GetReferenceChartTypeListByMeasurementDeviceId { get; set; }

        Project ObjProject { get; set; }
        MeasurementSetting ObjMeasurementSetting { get; set; }
        Project InsertProjectAndMeasurementSetting { get; set; }




    }
}
