using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class DestinationProfileBusinessMock : IDestinationProfileBusiness
    {
        #region IDestinationProfileBusiness Members

        public Dictionary<int, string> GetDestinationProfileList()
        {
            return new Dictionary<int, string>();
        }

        #endregion
    }
}
