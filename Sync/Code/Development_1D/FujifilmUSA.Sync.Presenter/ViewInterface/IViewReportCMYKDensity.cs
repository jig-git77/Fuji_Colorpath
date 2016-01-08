using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewReportCMYKDensity : IViewBase
    {
        int MeasurementId { get; set; }
        int OptimizeRunId { get; set; }
        int ProjectId { get; set; }
        DataTable DSDensity_C { get; set; }
        DataTable DSDensity_M { get; set; }
        DataTable DSDensity_Y { get; set; }
        DataTable DSDensity_K { get; set; }
    }
}
