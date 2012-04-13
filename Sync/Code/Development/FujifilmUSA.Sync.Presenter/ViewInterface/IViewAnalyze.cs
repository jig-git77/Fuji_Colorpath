using System.Collections.Generic;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAnalyze : IViewBase
    {
        int ProjectId { get; set; }
        int OptimizeRunId { get; set; }
        List<int> MeasurementIds { get; set; }
        List<Entity.Measurement> Measurements { get; set; }
        Entity.Measurement Measurement { get; set; }
    }
}
