using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class DestinationProfileBusinessImpl : IDestinationProfileBusiness
    {
        #region IDestinationProfileBusiness Members
        public List<DestinationProfile> GetDestinationProfileList(int syncDeviceId)
        {
            var destinationProfileList = new List<Entity.DestinationProfile>();
            var dsDestinationProfile = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetDestinationProfileList]", new object[] { syncDeviceId });

            if (dsDestinationProfile != null && dsDestinationProfile.Tables.Count > 0 && dsDestinationProfile.Tables[0].Rows.Count > 0)
            {
                destinationProfileList = dsDestinationProfile.Tables[0].AsEnumerable().Select(x => new Entity.DestinationProfile()
                {
                    DestinationProfileId = DataReader.ReadInt(x, "DestinationProfileID"),
                    DestinationProfileName = DataReader.ReadString(x, "DestinationProfileName"),
                    IsUse = DataReader.ReadBoolean(x, "IsUse"),
                    SyncDeviceId = DataReader.ReadInt(x, "SyncDeviceID"),
                }).ToList();
            }
            return destinationProfileList;
        }
        #endregion
    }
}

