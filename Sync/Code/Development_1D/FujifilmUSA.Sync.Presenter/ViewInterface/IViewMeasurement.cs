using FujifilmUSA.Sync.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewMeasurement : IViewBase
    {
        int ProjectId { get; set; }
        int OptimizeRunId { get; set; }
        string ProfileType { get; set; }
        ELicenseType LicenseType { get; set; }
        string DeviceSerialNo { get; set; }
        short InstrumentTypeID { get; set; }
        List<MeasurementLog> MeasurementLogs { get; set; }
    }
}
