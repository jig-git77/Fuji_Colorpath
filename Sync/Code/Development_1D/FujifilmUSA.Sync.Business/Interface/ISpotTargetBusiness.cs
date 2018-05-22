using FujifilmUSA.Sync.Entity.CPSpot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface ISpotTargetBusiness : ITaskBase
    {
        List<SpotTarget> GetSpotTargetList(int UserID, int CurrentUserID);
    }
}
