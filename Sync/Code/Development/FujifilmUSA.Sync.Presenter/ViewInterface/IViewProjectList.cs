using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewProjectList
    {
        int SyncDeviceId { get; set; }
        int ProjectId { get; set; }
        List<Project> GetProjectListBySyncDeviceId { get; set; }
        int DeleteProject { get; set; }
    }
}
