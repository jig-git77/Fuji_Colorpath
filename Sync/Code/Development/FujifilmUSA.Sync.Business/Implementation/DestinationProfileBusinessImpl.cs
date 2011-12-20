using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System;

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

        public int InsertDestinationProfile(DestinationProfile objDestinationProfile)
        {
            var objProjectInPeramater = new List<Parameters>();
            var objProjectOutPeramater = new List<Parameters>();

            objProjectInPeramater.Add(new Parameters() { ParameterName = "@DestinationProfileName", Size = 0, Type = DbType.String, Value = objDestinationProfile.DestinationProfileName });
            objProjectInPeramater.Add(new Parameters() { ParameterName = "@SyncDeviceID", Size = 0, Type = DbType.Int16, Value = objDestinationProfile.SyncDeviceId });
            objProjectInPeramater.Add(new Parameters() { ParameterName = "@IsUse", Size = 0, Type = DbType.Boolean, Value = objDestinationProfile.IsUse });

            objProjectOutPeramater.Add(new Parameters() { ParameterName = "@DestinationProfileID", Size = 4, Type = DbType.Int16 });


            DataAccessFactory.Instance.ExecuteProcedureWithTransaction(
                new List<Action>()
                    {
                        () =>
                            {
                                objProjectOutPeramater =
                                    DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspInsertDestinationProfile]",
                                                                                objProjectInPeramater,
                                                                                objProjectOutPeramater);
                                objDestinationProfile.DestinationProfileId =
                                    Convert.ToInt16(
                                        objProjectOutPeramater.Single(x => x.ParameterName == "@DestinationProfileID").Value);
                            }
                    });

            return objDestinationProfile.DestinationProfileId;
        }

        public void UpdateDestinationProfile(int destinationProfileId, byte[]  profile)
        {
            DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspUpdateDestinationProfile]", new object[] { destinationProfileId, profile });
        }


        #endregion

        
    }
}

