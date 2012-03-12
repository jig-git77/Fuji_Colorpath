using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IOptimizeRunBusiness : ITaskBase
    {
        void CreateOptimizeRun(int projectId, int status, DateTime startDateTime);
        void CreateFirstDlp(int optimizeRunId);
        byte[] GetDeviceLinkProfile(int optimizeRunId);
    }
}