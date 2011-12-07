using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewExportProfile
    {
        int SyncRunId { get; set; }
        int Status { get; set; }
        DateTime EndDateTime { get; set; }
    }
}
