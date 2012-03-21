using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAnalyze
    {
        int ProjectId { get; set; }
        int OptimizeRunId { get; set; }
        List<int> MeasurementIds { get; set; }

        //For Anaylze Chart
        List<Entity.Measurement> lstMeasurement { get; set; }
        Entity.Measurement Measurement { get; set; }
    }
}
