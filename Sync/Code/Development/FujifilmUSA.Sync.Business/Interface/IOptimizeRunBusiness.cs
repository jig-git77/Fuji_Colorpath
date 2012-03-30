using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IOptimizeRunBusiness : ITaskBase
    {
        void CreateOptimizeRun(int projectId, int status,int closeStatus, DateTime startDateTime);
        void CreateInitialDestinationAndDeviceLinkProfiles(int projectId, int optimizeRunId, List<int> measurementIds);
        byte[] GetDeviceLinkProfile(int optimizeRunId);
        List<Measurement> GetMeasurements(int optimizeRunId);
        void OptimizeDeviceLinkProfile(int projectId, int optimizeRunId, List<int> measurementIds);
        void CloseOptimizeRun(int optimizeRunId, int status, DateTime endDateTime);
        bool DeleteMeasurement(int measurementId);
    }
}