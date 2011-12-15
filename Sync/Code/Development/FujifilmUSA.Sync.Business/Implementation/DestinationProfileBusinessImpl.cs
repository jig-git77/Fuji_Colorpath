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

        public Dictionary<int, string> GetDestinationProfileforCombo()
        {
            var dstDestinationProfileList = new Dictionary<int, string>();
            var dsDestinationProfileList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetDestinationProfileList]");
            if (dsDestinationProfileList != null && dsDestinationProfileList.Tables.Count > 0)
            {
                dstDestinationProfileList = (dsDestinationProfileList.Tables[0].AsEnumerable().Select(
                    destinationProfile => new
                                              {
                                                  Value = DataReader.ReadInt(destinationProfile, "DestinationProfileID"),
                                                  Text = DataReader.ReadString(destinationProfile, "DestinationProfileName")
                                              })).ToDictionary(destinationProfile => destinationProfile.Value, destinationProfile => destinationProfile.Text);
            }
            return dstDestinationProfileList;
        }
            public List<Entity.DestinationProfile> GetDestinationProfileList()
            {
                List<Entity.DestinationProfile> destinationProfileList = new List<Entity.DestinationProfile>();
                var dsDestinationProfile = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[CPSync4D.uspGetDestinationProfile]");

                if (dsDestinationProfile != null && dsDestinationProfile.Tables.Count > 0 && dsDestinationProfile.Tables[0].Rows.Count > 0)
                {
                    destinationProfileList = dsDestinationProfile.Tables[0].AsEnumerable().Select(x => new Entity.DestinationProfile()
                    {
                        DestinationProfileId = DataReader.ReadInt(x, "DestinationProfileID"),
                        DestinationProfileName = DataReader.ReadString(x, "DestinationProfileName"),
                        IsUse =  DataReader.ReadBoolean(x, "IsUse")                                                
                    }).ToList();
                }
                return destinationProfileList;
            }
            #endregion
        }
    }      

