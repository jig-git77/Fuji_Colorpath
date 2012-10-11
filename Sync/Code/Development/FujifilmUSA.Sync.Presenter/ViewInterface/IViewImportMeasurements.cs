using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewImportMeasurements : IViewBase
    {
        List<Project> Projects { get; set; }
        int ProjectId { get; set; }
        int DeviceId { get; set; }
        int LicenseTypeID { get; set; }
        KeyValuePair<short, string> Measurements { get; set; }
    }
}
