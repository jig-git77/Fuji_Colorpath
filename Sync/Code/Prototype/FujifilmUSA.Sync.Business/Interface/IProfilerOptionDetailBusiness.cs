using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IProfilerOptionDetailBusiness :ITaskBase
    {
        List<ProfilerOptionDetail> GetProfilerOptionDetails(int profileroptionId, ProfileType type);
    }
}
