using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewSyncDevice
    {
        int TaskeroUserID { get; set; }
        int RoleID { get; set; }
        int CompanyID { get; set; }
        int BranchID { get; set; }

        Dictionary<int, string> GetBranchList { get; set; }
        Dictionary<int, string> GetCompanyList { get; set; }
        List<SyncDevice> GetSyncDeviceListByUserID { get; set; }

    }
}
