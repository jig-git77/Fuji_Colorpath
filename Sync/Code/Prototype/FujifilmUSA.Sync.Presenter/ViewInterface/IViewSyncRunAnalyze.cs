using System;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewSyncRunAnalyze : IViewBaseSyncDevice, IViewError
    {
        ECPSyncRunStatus Status { get; set; }
        DateTime EndDateTime { get; set; }
        int SyncRunId { get; set; }
        int ProjectId { get; set; }
        int ProfileOptionId { get; set; }
        string MeasurementIds { get; set; }
        Measurement Measurement { get; set; }
        List<Measurement> lstMeasurement { get; set; }
    }
}
