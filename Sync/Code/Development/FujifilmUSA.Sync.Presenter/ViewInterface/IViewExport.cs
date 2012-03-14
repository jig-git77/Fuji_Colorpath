using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewExport
    {
        int ProjectId { get; set; }
        int OptimizeRunId { get; set; }
        byte[] DeviceLinkProfile { get; set; }
        byte[] ChartPreview { get; set; }
        byte[] SourceProfile { get; set; }
    }
}
