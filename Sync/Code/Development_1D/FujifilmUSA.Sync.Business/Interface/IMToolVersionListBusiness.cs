using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMToolVersionListBusiness : ITaskBase
    {
        int InsertMToolUpdateVersion(MToolVersionUpdateList mToolVersion);
        int UpdateMToolUpdateVersion(MToolVersionUpdateList mToolVersion);        
        List<MToolVersionUpdateList> GetMToolUpdateVersionList();
        MToolVersionUpdateList GetMToolVersionSelect(short oSPlatformDetailsID);
        Dictionary<int, string> GetMToolOSPlatformList();
    }
}
