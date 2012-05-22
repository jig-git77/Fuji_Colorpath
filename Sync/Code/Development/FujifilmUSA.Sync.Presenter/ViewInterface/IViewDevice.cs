using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewDevice : IViewBase
    {
        int TaskeroUserId { get; set; }
        int RoleId { get; set; }
        int CompanyId { get; set; }
        int BranchId { get; set; }
        MeasurementDevice MeasurementDevice { get; set; }
        List<MeasurementDevice> MeasurementDevices { get; set; }

        Dictionary<int, string> BranchList { get; set; }
        Dictionary<int, string> CompanyList { get; set; }
        Dictionary<int, string> LicenseList { get; set; }
        Dictionary<int, string> InstrumentTypeList { get; set; }
        List<Device> DeviceListById { get; set; }
        Device SelectedDevice { get; set; }
        int DeviceId { get; set; }     
    }
}
