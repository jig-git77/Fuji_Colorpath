using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;
using System.Data;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewExport : IViewBase
    {
        int ProjectId { get; set; }
        int OptimizeRunId { get; set; }
        short ReferenceChartTypeId { get; set; }
        byte[] DeviceLinkProfile { get; set; }
        byte[] ChartPreview { get; set; }
        byte[] SourceProfile { get; set; }
        Project Project { get; set; }
        short CurveTypeId { get; set; }
        FujifilmUSA.Sync.Business.Messages.CurveDataResponse CurveData { get; set; }
        int SpotProjectId { get; set; }
        DataTable dtPatchDetails { get; set; }
        SpotProjectChartSetting SpotProjectChartSetting { get; set; }
        string SpotProjectIDs { get; set; }
    }
}
