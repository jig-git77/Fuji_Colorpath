using System;
using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business.ProfilerService;
using FujifilmUSA.Sync.Entity;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class DeviceLinkProfileBusinessImpl : IDeviceLinkProfileBusiness
    {
        #region IDeviceLinkProfileBusiness Members

        public List<DeviceLinkProfile> GetDeviceLinkProfileList(int sourceProfileId, int destinationProfileId)
        {
            var objDeviceLinkProfileList = new List<DeviceLinkProfile>();
            //========== Get data from data access=================//
            var dsDeviceLinkProfileList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetDLPList]", new object[] {sourceProfileId, destinationProfileId});

            if (dsDeviceLinkProfileList != null && dsDeviceLinkProfileList.Tables.Count > 0 && dsDeviceLinkProfileList.Tables[0].Rows.Count > 0)
            {
                objDeviceLinkProfileList = dsDeviceLinkProfileList.Tables[0].AsEnumerable().
                    Select(x => new DeviceLinkProfile()
                                    {
                                        DeviceLinkProfileId = DataReader.ReadInt(x, "DLPID"),
                                        DeviceLinkProfileName = DataReader.ReadString(x, "DLPName"),
                                        //Profile = DsReader.ReadString(x, "Profile"),
                                        IsUse = DataReader.ReadBoolean(x, "IsUse"),
                                        SrcProfile =
                                            new SourceProfile
                                                {SourceProfileId = DataReader.ReadInt(x, "SourceProfileID"), SourceProfileName = DataReader.ReadString(x, "SourceProfileName")},
                                        DestProfile =
                                            new DestinationProfile
                                                {
                                                    DestinationProfileId = DataReader.ReadInt(x, "DestinationProfileID"),
                                                    DestinationProfileName = DataReader.ReadString(x, "DestinationProfileName")
                                                }

                                    }).ToList();
            }
            return objDeviceLinkProfileList;
        }

        #endregion
    }
}
