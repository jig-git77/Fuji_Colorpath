using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewProjectList
    {
        int SyncDeviceID { get; set; }
        List<Project> GetProjectListBySyncDeviceID { get; set; }
    }
}
