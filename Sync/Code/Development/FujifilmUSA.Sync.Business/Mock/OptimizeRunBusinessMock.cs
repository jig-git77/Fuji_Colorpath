using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using System;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class OptimizeRunBusinessMock : IOptimizeRunBusiness
    {
        #region IOptimizeRunBusiness Members

        public void CreateOptimizeRun(int projectId, int status, DateTime startDateTime)
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

        #endregion
    }
}
