using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IProfileBusiness : ITaskBase
    {
        List<Measurement> GetMeasurement(int profileId, ProfileType typeId);
    }
}
