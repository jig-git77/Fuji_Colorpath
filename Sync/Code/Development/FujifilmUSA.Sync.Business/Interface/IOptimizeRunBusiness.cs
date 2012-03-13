using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IOptimizeRunBusiness : ITaskBase
    {
        void CreateOptimizeRun(int projectId, int status, DateTime startDateTime);
        void CreateFirstDlp(int projectId, int optimizeRunId, List<int> measurementIds);
        byte[] GetDeviceLinkProfile(int optimizeRunId);
        List<Measurement> GetMeasurements(int optimizeRunId);
    }
}