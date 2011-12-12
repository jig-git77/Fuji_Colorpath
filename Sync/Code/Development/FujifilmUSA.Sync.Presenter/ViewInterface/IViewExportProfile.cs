using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewExportProfile
    {
        int SyncRunId { get; set; }
        ECPSyncRunStatus Status { get; set; }
        DateTime EndDateTime { get; set; }
        int ProjectId { get; set;}
        byte[] IccProfile { get; set; }
    }
}
