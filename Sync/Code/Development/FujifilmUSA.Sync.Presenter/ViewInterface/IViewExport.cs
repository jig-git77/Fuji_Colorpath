using FujifilmUSA.Sync.Entity;

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
    }
}
