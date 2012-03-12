using System;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.DataAccess;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class OptimizeRunBusinessImpl : IOptimizeRunBusiness
    {
        #region IOptimizeRunBusiness Members

        public void CreateOptimizeRun(int projectId, int status, DateTime startDateTime)
        {
            DataAccessFactory.Instance.ExecuteProcedure("CPSync4D.CreateOptimizeRun",
                                            new object[] { projectId, status, startDateTime });
        }

        public void CreateFirstDlp(int optimizeRunId)
        {
            DataAccessFactory.Instance.GetDsResult("CPSync4D.GetSourceAndDestinationProfileData", new object[] {optimizeRunId});
        }

        public byte[] GetDeviceLinkProfile(int optimizeRunId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
