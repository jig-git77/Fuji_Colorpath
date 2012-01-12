using System;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewExportProfile : IViewBaseProfile, IViewError
    {
        ECPSyncRunStatus Status { get; set; }
        int SyncRunId { get; set; }
        DateTime EndDateTime { get; set; }
        int ProjectId { get; set; }
    }
}
