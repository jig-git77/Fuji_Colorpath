using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewDevice
    {
        int TaskeroUserId { get; set; }
        int RoleId { get; set; }
        int CompanyId { get; set; }
        int BranchId { get; set; }

        Dictionary<int, string> BranchList { get; set; }
        Dictionary<int, string> CompanyList { get; set; }
        List<Device> DeviceListById{ get; set; }

    }
}
