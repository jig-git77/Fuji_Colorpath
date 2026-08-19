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
        Project Project { get; set; }
        List<int> MeasurementIds { get; set; }
        int MeasurementId { get; set; }
        bool EnabledPaperScaling { get; set; }
        bool IsUpdateNewPaper { get; set; }
        MeasurementPatchDetails measurementPatchDetails { get; set; }
        bool IsUpdateDelta { get; set; }
        int LicenseTypeId { get; set; }
        List<Entity.CPSync1D.ProjectTargetLAB> ProjectTargetLABList { get; set; }
        int ProjectTypeId { get; set; }
        int PrintstandardTypeId { get; set; }
    }
}
