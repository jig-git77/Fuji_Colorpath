using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface ISyncRunBusiness : ITaskBase
    {
        int SaveMeasurement(Measurement measurement);

        List<Measurement> GetMeasurement(int syncRunId);

        DeviceLinkProfile OptimizeDeviceLinkProfile(int measurementId, int syncRunId, int projectId);

        int GetMasterSyncRunId(int syncRunId);
    }
}
