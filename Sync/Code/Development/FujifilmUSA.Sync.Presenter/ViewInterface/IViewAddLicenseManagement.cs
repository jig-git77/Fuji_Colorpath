using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddLicenseManagement : IViewError
    {
        License License { get; set; }
        int CompanyId { get; set; }
        Dictionary<int, string> Compnaies { get; set; }
        Dictionary<int, string> Branches { get; set; }
        Dictionary<int, string> LicenseTypes { get; set; }
    }
}
