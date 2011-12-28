using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IProfilerOptionDetailBusiness :ITaskBase
    {
        List<ProfilerOptionDetail> GetProfilerOptionDetailByProfilerID(int ProfileroptionID, ProfileType type);
    }
}
