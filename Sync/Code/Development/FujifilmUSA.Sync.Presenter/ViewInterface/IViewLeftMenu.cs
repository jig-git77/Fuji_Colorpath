using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewLeftMenu
    {
        int ProjectId { get; set; }
        Entity.ECPSyncRunStatus Status { get; set; }
        DateTime CreatedDateTime { get; set; }
        Project CurrentProject { get; set; }
    }
}
