using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewSaveWindow : IViewBase
    {
        short ReferenceChartTypeId { get; set; }
        byte[] ChartPreview { get; set; }
        Project Project { get; set; }
    }
}
