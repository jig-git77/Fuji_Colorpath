using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public  interface IViewDeviceLinkProfile
    {
       public List<DeviceLinkProfile> GetDeviceLinkProfileList { get; set; }
       public int SyncDeviceId;
    }
}
