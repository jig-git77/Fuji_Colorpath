using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewSyncRunAnalyze : IViewBaseSyncDevice
    {
        int SyncRunId { get; set; }
        int ProjectId { get; set; }
        string MeasurementIds { get; set; }
    }
}
