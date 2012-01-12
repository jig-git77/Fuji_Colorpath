using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewSyncDevice : IViewError
    {
        int TaskeroUserId { get; set; }
        int RoleId { get; set; }
        int CompanyId { get; set; }
        int BranchId { get; set; }

        Dictionary<int, string> GetBranchList { get; set; }
        Dictionary<int, string> GetCompanyList { get; set; }
        List<SyncDevice> GetSyncDeviceListByUserId { get; set; }

    }
}
