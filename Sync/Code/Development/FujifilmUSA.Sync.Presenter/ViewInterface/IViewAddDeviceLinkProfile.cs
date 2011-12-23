using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewAddDeviceLinkProfile
    {
        DeviceLinkProfile ObjDeviceLinkProfile { get; set; }
        bool CreateResult { get; set; }
    }
}
