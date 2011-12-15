using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class DestinationProfileBusinessMock : IDestinationProfileBusiness
    {
        #region IDestinationProfileBusiness Members

        public Dictionary<int, string> GetDestinationProfiles()
        {
            return new Dictionary<int, string>();
        }
        public List<Entity.DestinationProfile> GetDestinationProfileList()
        {
            return new List<Entity.DestinationProfile>();
        }
        #endregion
    }
}
