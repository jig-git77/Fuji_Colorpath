using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewPluginDataService : IViewBase
    {
        Measurement Measurement { get; set; }
        int ProjectId { get; set; }
        ReferenceChartData ReferenceChartData { get; set; }
        int OptimizeRunId { get; set; }
        List<Measurement> Measurements { get; set; }
        int MeasurementId { get; set; }
    }
}
