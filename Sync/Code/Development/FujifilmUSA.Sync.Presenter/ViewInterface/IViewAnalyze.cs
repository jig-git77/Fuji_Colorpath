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
        string DestinationProfileDescription { get; set; }
        string DlpDescription { get; set; }
        List<Entity.MeasurementPatch_AvgDelta> MeasurementsDelta { get; set; }
        List<Entity.MeasurementPatchDetails> PatchDetails { get; set; }
        int MeasurementId { get; set; }
       
    }
}
