using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public interface IBaseProfile
    {
        int ProfileId { get; set; }
        string ProfileName { get; set; }
        ProfileData Data { get; set; }
        int SyncDeviceId { get; set; }
        bool IsUse { get; set; }
    }
}
