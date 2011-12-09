using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewExportProfile
    {
        int SyncRunId { get; set; }
        Entity.ECPSyncRunStatus Status { get; set; }
        DateTime EndDateTime { get; set; }
        int ProjectId { get; set;}
        string MeasurementIds { get; set; }
    }
}
