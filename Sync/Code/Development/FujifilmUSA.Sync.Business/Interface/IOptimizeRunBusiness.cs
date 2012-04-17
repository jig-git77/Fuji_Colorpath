using System;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IOptimizeRunBusiness : ITaskBase
    {
        void CreateOptimizeRun(int projectId, int status, int closeStatus, DateTime startDateTime);
        void CreateInitialDestinationAndDeviceLinkProfiles(int projectId, int optimizeRunId, List<int> measurementIds, string destinationDescription, string dlpDescription);
        byte[] GetDeviceLinkProfile(int optimizeRunId);
        void OptimizeDeviceLinkProfile(int projectId, int optimizeRunId, List<int> measurementIds, string description);
        void CloseOptimizeRun(int optimizeRunId, int status, DateTime endDateTime);
        bool DeleteMeasurement(int measurementId);
        int GetLastOptimizeRunId(int projectId);
    }
}