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
            var dsDeviceLinkProfileList = DataAccessFactory.Instance.GetResult("[CPSync4D].[uspGetDLPList]", new object[] {sourceProfileId, destinationProfileId});

            if (dsDeviceLinkProfileList != null && dsDeviceLinkProfileList.Tables.Count > 0 && dsDeviceLinkProfileList.Tables[0].Rows.Count > 0)
            {
                objDeviceLinkProfileList = dsDeviceLinkProfileList.Tables[0].AsEnumerable().
                    Select(x => new DeviceLinkProfile()
                                    {
                                        DeviceLinkProfileId = DsReader.ReadInt(x, "DLPID"),
                                        DeviceLinkProfileName = DsReader.ReadString(x, "DLPName"),
                                        //Profile = DsReader.ReadString(x, "Profile"),
                                        IsUse = DsReader.ReadBoolean(x, "IsUse"),
                                        SrcProfile =
                                            new SourceProfile
                                                {SourceProfileId = DsReader.ReadInt(x, "SourceProfileID"), SourceProfileName = DsReader.ReadString(x, "SourceProfileName")},
                                        DestProfile =
                                            new DestinationProfile
                                                {
                                                    DestinationProfileId = DsReader.ReadInt(x, "DestinationProfileID"),
                                                    DestinationProfileName = DsReader.ReadString(x, "DestinationProfileName")
                                                }

                                    }).ToList();
            }
            return objDeviceLinkProfileList;
        }

        #endregion
    }
}
