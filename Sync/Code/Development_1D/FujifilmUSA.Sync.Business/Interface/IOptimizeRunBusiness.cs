using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.Messages;
namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IOptimizeRunBusiness : ITaskBase
    {
        void CreateOptimizeRun(int projectId, int status, DateTime startDateTime);
        void CreateInitialDestinationAndDeviceLinkProfiles(int projectId, int optimizeRunId, List<int> measurementIds, string destinationDescription, string dlpDescription, int status, DateTime endDateTime);
        byte[] GetDeviceLinkProfile(int optimizeRunId);
        void OptimizeDeviceLinkProfile(int projectId, int optimizeRunId, List<int> measurementIds, string description, int status, DateTime endDateTime);
        void CertifyOptimizeRun(int optimizeRunId, int ProjectId, int MasterRunId, int status, DateTime endDateTime);
        void Certify1DOptimizeRun(int optimizeRunId, int ProjectId, int MasterRunId, int status, DateTime endDateTime);
        bool DeleteMeasurement(int measurementId);
        int GetLastOptimizeRunId(int projectId);
        void DeleteOptimizeRun(int OptimizeRunID, int ProjectID, int MasterRunID, EOptimizeRunStatus RunStatus);
        EOptimizeRunStatus GetLastRunStatus(int OptimizeRunID);
        bool DeleteVMeasurement(int measurementId);
        void DeleteOptimizeRunDLP(int OptimizeRunID, int ProjectID);
        bool Delete1DMeasurement(int measurementId);
        void Create1DOptimizeRun(int projectId, int status, DateTime startDateTime);

        void CreateG7AlignmentCurve(int projectId, int optimizeRunId, List<int> measurementIds, bool useDLP, int status, DateTime endDateTime);
        void CreateISOAlignmentCurve(int projectId, int optimizeRunId, List<int> measurementIds, bool useDLP, int status, DateTime endDateTime);
        CurveDataResponse GetCurveData(int optimizeRunId, short curveTypeId);
        byte[] Get1DDeviceLinkProfile(int optimizeRunId);
        EOptimizeRunStatus Get1DLastRunStatus(int OptimizeRunId);
        void Delete1DOptimizeRun(int OptimizeRunID, int ProjectID, int MasterRunID, EOptimizeRunStatus RunStatus);
        void Delete1DOptimizeRunDLP(int OptimizeRunID, int ProjectID);

        void CreateSourceToSourceDeviceLinkProfile(int projectId, int optimizeRunId, List<int> measurementIds, string destinationDescription, string dlpDescription, int status, DateTime endDateTime);
        void CreateReTargetProject(int projectId, int optimizeRunId, int status, DateTime endDateTime);
    }
}