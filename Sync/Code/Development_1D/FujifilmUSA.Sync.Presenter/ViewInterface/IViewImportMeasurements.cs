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
        List<int> Measurements { get; set; }
        int MeasurementID { get; set; }
        int OptimizeRunID { get; set; }
    }
}
