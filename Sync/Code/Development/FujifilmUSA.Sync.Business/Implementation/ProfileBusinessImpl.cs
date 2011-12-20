using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.DataAccess;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class ProfileBusinessImpl : IProfileBusiness
    {
        #region IProfileBusiness Members

        public List<Measurement> GetMeasurement(int profileId, ProfileType typeId)
        {
            if (profileId <= 0 || typeId == ProfileType.None) return new List<Measurement>();
            return SyncRunBusinessImpl.ConvertMeasurement(DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetMeasurementByProfileID]", new object[] {profileId, typeId}));
        }

        #endregion
    }
}
