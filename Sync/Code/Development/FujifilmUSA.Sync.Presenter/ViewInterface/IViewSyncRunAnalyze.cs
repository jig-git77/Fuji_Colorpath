using System;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewSyncRunAnalyze : IViewBaseSyncDevice
    {
        ECPSyncRunStatus Status { get; set; }
        DateTime EndDateTime { get; set; }
        int SyncRunId { get; set; }
        int ProjectId { get; set; }
        string MeasurementIds { get; set; }
    }
}
