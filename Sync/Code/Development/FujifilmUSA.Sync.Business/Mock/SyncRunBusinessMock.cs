using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class SyncRunBusinessMock : ISyncRunBusiness
    {
        #region ISyncRunBusiness Members

        public int SaveMeasurement(Measurement measurement)
        {
            return 1;
        }

        public List<Measurement> GetMeasurement(int syncRunId)
        {
            return new List<Measurement>();
        }

        public DeviceLinkProfile OptimizeDeviceLinkProfile(string measurementIds, int syncRunId, int projectId)
        {
            return new DeviceLinkProfile();
        }

        public int GetMasterSyncRunId(int syncRunId)
        {
            return 0;
        }


        public byte[] GetIccProfileBySyncRunId(int syncRunId)
        {
            return null;
        }

        #endregion
    }
}
