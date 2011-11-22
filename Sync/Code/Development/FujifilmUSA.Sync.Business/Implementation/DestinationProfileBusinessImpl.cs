using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class DestinationProfileBusinessImpl : IDestinationProfileBusiness
    {

        #region IDestinationProfileBusiness Members

        public Dictionary<int, string> GetDestinationProfileList()
        {
            var dstDestinationProfileList = new Dictionary<int, string>();
            var dsDestinationProfileList = DataAccessFactory.Instance.GetResult("[CPSync4D].[uspGetDestinationProfileList]");
            if (dsDestinationProfileList != null && dsDestinationProfileList.Tables.Count > 0)
            {
                dstDestinationProfileList = (dsDestinationProfileList.Tables[0].AsEnumerable().Select(
                    destinationProfile => new
                                              {
                                                  Value = DsReader.ReadInt(destinationProfile,"DestinationProfileID"),
                                                  Text = DsReader.ReadString(destinationProfile,"DestinationProfileName")
                                              })).ToDictionary(destinationProfile => destinationProfile.Value, destinationProfile => destinationProfile.Text);
            }
            return dstDestinationProfileList;
        }

        #endregion
    }
}
