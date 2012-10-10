using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewReportSingleMeasurement : IViewBase
    {
        int MeasurementId { get; set; }
        DataSet DSReport { get; set; }
        string From_ΔE { get; set; }
    }
}
