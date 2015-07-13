using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewReportGrading : IViewBase
    {
        int MeasurementId { get; set; }
        int ProjectId { get; set; }
        string From_ΔE { get; set; }
        bool IsVerification { get; }
        DataSet DSReport { get; set; }
        int ValidationType { get; set; }
        bool IsAverageOfMeasurements { get; }
    }
}
