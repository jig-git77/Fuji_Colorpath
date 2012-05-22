using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddDeviceManagement : IViewBase
    {
        Device Device { get; set; }
        int CompanyId { get; set; }
        int BranchId { get; set; }
        Dictionary<int, string> Compnaies { get; set; }
        Dictionary<int, string> Branches { get; set; }
        Dictionary<int, string> Licenses { get; set; }
        License License { get; set; }
        Dictionary<int, string> DeviceFamilyList { get; set; }
    }
}
