using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Messages;
namespace FujifilmUSA.Sync.Business.Mock
{
    public class OptimizeRunBusinessMock : IOptimizeRunBusiness
    {
        #region IOptimizeRunBusiness Members

        public void CreateOptimizeRun(int projectId, int status, DateTime startDateTime)
        {

        }

        public void CreateInitialDestinationAndDeviceLinkProfiles(int projectId, int optimizeRunId, List<int> measurementIds, string destinationDescription, string dlpDescription, int status, DateTime endDateTime)
        {

        }

        public byte[] GetDeviceLinkProfile(int optimizeRunId)
        {
            return new byte[1];
        }

        public void OptimizeDeviceLinkProfile(int projectId, int optimizeRunId, List<int> measurementIds, string description, int status, DateTime endDateTime)
        {

        }

        public void CertifyOptimizeRun(int optimizeRunId, int ProjectId, int MasterRunId, int status, DateTime endDateTime)
        {

        }

        public void Certify1DOptimizeRun(int optimizeRunId, int ProjectId, int MasterRunId, int status, DateTime endDateTime)
        { 
        
        }

        public bool DeleteMeasurement(int measurementId)
        {
            return true;
        }

        public int GetLastOptimizeRunId(int projectId)
        {
            return 0;
        }

        public int Get1DLastOptimizeRunId(int projectId)
        {
            return 0;
        }

        public void DeleteOptimizeRun(int OptimizeRunID, int ProjectID, int MasterRunID, EOptimizeRunStatus RunStatus)
        {

        }

        public void Delete1DOptimizeRun(int OptimizeRunID, int ProjectID, int MasterRunID, EOptimizeRunStatus RunStatus)
        {

        }

        public EOptimizeRunStatus GetLastRunStatus(int OptimizeRunID)
        {
            return default(EOptimizeRunStatus);
        }

        public bool DeleteVMeasurement(int measurementId)
        {
            return true;
        }

        public void DeleteOptimizeRunDLP(int OptimizeRunID, int ProjectID)
        {

        }

        public void Delete1DOptimizeRunDLP(int OptimizeRunID, int ProjectID)
        {

        }

        public bool Delete1DMeasurement(int measurementId)
        {
            return true;
        }

        public void Create1DOptimizeRun(int projectId, int status, DateTime startDateTime, short CurveTypeId)
        {

        }

        public void CreateG7AlignmentCurve(int projectId, int optimizeRunId, List<int> measurementIds, bool useDLP, int status, DateTime endDateTime, bool Is1DMeasurementSmoothing)
        {

        }

        public void CreateISOAlignmentCurve(int projectId, int optimizeRunId, List<int> measurementIds, bool useDLP, int status, DateTime endDateTime, bool Is1DMeasurementSmoothing)
        {

        }

        public CurveDataResponse GetCurveData(int optimizeRunId, short curveTypeId)
        {
            return new Messages.CurveDataResponse();
        }

        public byte[] Get1DDeviceLinkProfile(int optimizeRunId)
        {
            return new byte[1];
        }

        public EOptimizeRunStatus Get1DLastRunStatus(int OptimizeRunId)
        {
            return 0;
        }

        public void CreateSourceToSourceDeviceLinkProfile(int projectId, int optimizeRunId, List<int> measurementIds, string destinationDescription, string dlpDescription, int status, DateTime endDateTime)
        {

        }

        public void CreateCustomSourceProfile(int projectId, int optimizeRunId, List<int> measurementIds, string destinationDescription, string dlpDescription, int status, DateTime endDateTime)
        {

        }

        public void CreateReTargetProject(int projectId, int optimizeRunId, int status, DateTime endDateTime)
        {

        }
        public void CreateSpotProjectOptimizeRun(int spotProjectId, int status, DateTime startDateTime)
        {

        }
        public void UpdateSpotProjectOptimizeRun(int optimizeRunID, int status, DateTime startDateTime)
        {

        }
        public void DeleteSpotOptimizeRun(int OptimizeRunID, int SpotProjectID, int MasterRunID, EOptimizeRunStatus RunStatus)
        {

        }
        public bool DeleteSpotMeasurement(int measurementId)
        {
            return true;
        }
        public void DeleteSpotOptimizeRunDLP(int OptimizeRunID, int SpotProjectID)
        {

        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
