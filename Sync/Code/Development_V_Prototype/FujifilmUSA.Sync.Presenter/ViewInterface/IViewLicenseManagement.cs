using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewLicenseManagement : IViewBase
    {
        License License { get; set; }
        List<License> LicenseList { get; set; }
    }
}
