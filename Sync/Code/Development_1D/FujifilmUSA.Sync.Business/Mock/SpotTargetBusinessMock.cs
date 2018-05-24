using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity.CPSpot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Business.Mock
{
    class SpotTargetBusinessMock:ISpotTargetBusiness
    {

        public List<SpotTarget> GetSpotTargetList(int UserID, int CurrentUserID)
        {
            return new List<SpotTarget>();
        }
        public int CreateSpotTarget(SpotTarget spotTarget)
        {
            return 0;
        }
        public SpotTarget GetSpotTarget(int SpotTargetID)
        {
            return new SpotTarget();
        }
        public int UpdateSpotTarget(SpotTarget spotTarget)
        {
            return 0;
        }
        public bool DeleteSpotTarget(int spotTargetId)
        {
            return false;
        }
        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
