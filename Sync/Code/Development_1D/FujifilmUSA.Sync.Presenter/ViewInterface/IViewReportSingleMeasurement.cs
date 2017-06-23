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
        string From_ΔE { get; set; }
        bool IsVerification { get; }
        DataSet DSReport { get; set; }
        DataSet DSReport_G7_Vali { get; set; }
        int ValidationType { get; set; }
        bool IsSync1DType { get; }
        bool IsAverageOfMeasurements { get; }
    }
}
