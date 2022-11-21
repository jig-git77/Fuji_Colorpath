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
        List<SpotTarget> GetSpotTargetList_CustomBCO(int UserID, int CurrentUserID);
        int CreateSpotTarget(SpotTarget spotTarget);
        SpotTarget GetSpotTarget(int SpotTargetID);
        int UpdateSpotTarget(SpotTarget spotTarget);
        int DeleteSpotTarget(int spotTargetId);
        int DeleteSpotTargetList(List<string> lstDeleteSpotTargetID);
    }
}
