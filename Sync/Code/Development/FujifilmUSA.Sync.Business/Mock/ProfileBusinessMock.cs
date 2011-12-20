using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class ProfileBusinessMock : IProfileBusiness
    {
        #region IProfileBusiness Members

        public List<Measurement> GetMeasurement(int profileId, ProfileType typeId)
        {
            return new List<Measurement>();
        }

        #endregion
    }
}
