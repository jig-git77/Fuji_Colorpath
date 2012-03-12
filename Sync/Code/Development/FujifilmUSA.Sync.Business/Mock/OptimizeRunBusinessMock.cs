using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using System;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class OptimizeRunBusinessMock : IOptimizeRunBusiness
    {
        #region IOptimizeRunBusiness Members

        public void CreateOptimizeRun(int projectId, int status, DateTime startDateTime)
        {
            
        }

        public void CreateFirstDlp(int optimizeRunId)
        {
            
        }

        public byte[] GetDeviceLinkProfile(int optimizeRunId)
        {
            return new byte[1];
        }

        #endregion
    }
}
