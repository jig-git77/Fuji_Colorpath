using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface ISyncRunBusiness : ITaskBase
    {
        DeviceLinkProfile OptimizeDeviceLinkProfile(string measurementIds, int syncRunId, int projectId, int profileOptionId);
        int GetMasterSyncRunId(int syncRunId);
        byte[] GetIccProfileBySyncRunId(int syncRunId);
        
    }
}
