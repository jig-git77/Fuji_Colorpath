using System.Collections.Generic;
using System.Data;

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
        string From_ΔE { get; set; }
        List<Entity.AnalyzeChart> DTDeltaEPrimaryPatches { get; set; }
        List<Entity.AnalyzeChart> DTPaperPatches { get; set; }
        List<Entity.AnalyzeChart_Gray> DTLABChart { get; set; }
        List<Entity.AnalyzeChart_Gray> DTGaumatChart1 { get; set; }
        List<Entity.AnalyzeChart_Gray> DTGaumatChart2 { get; set; }
    }
}
