using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewLicenseManagement 
    {
        License License { get; set; }
        List<License> LicenseList { get; set; }
    }
}
