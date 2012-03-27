using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using System;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class OptimizeRunBusinessMock : IOptimizeRunBusiness
    {
        #region IOptimizeRunBusiness Members

        public void CreateOptimizeRun(int projectId, int status, int closeStatus, DateTime startDateTime)
        {

        }

        public void CreateInitialDestinationAndDeviceLinkProfiles(int projectId, int optimizeRunId, List<int> measurementIds)
        {

        }

        public byte[] GetDeviceLinkProfile(int optimizeRunId)
        {
            return new byte[1];
        }

        public List<Measurement> GetMeasurements(int optimizeRunId)
        {
            return new List<Measurement>();
        }

        public void OptimizeDeviceLinkProfile(int projectId, int optimizeRunId, List<int> measurementIds)
        {

        }

        public void CloseOptimizeRun(int optimizeRunId, int status, DateTime endDateTime)
        {

        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
