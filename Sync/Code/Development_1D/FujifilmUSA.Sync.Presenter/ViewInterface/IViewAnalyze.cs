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
        bool IsVerification { get; }

        List<Entity.Measurement> MeasurementSummary { get; set; }

        List<Entity.AnalyzeChart_G7> GrayFChart { get; set; }
        List<Entity.AnalyzeChart_G7> GrayFSummary { get; set; }       
        List<Entity.AnalyzeChart_G7> BlackLChart { get; set; }
        List<Entity.AnalyzeChart_G7> BlackLSummary { get; set; }
        List<Entity.AnalyzeChart_G7> GrayLChart { get; set; }
        List<Entity.AnalyzeChart_G7> GrayLSummary { get; set; }
        List<Entity.AnalyzeChart_G7> BlackNPDChart { get; set; }
        List<Entity.AnalyzeChart_G7> GrayNPDChart { get; set; }

        DataTable dtPatchDetails { get; set; }

        bool useDLP { get; set; }
        byte[] DeviceLinkProfile { get; set; }
        bool IsSync1DType { get; }

        List<Entity.AnalyzeChart_DotGain> DTTVIChart { get; set; }
    }
}
