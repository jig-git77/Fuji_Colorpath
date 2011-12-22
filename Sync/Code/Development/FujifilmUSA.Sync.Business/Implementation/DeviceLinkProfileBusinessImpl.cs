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

        public List<DeviceLinkProfile> GetDeviceLinkProfileListOnSyncDeviceId(int SyncDeviceId)
        {
            var objDeviceLinkProfileList = new List<DeviceLinkProfile>();
            //========== Get data from data access=================//
            var dsDeviceLinkProfileList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetDLPListBySyncDeviceId]", new object[] { SyncDeviceId });

            if (dsDeviceLinkProfileList != null && dsDeviceLinkProfileList.Tables.Count > 0 && dsDeviceLinkProfileList.Tables[0].Rows.Count > 0)
            {
                objDeviceLinkProfileList = dsDeviceLinkProfileList.Tables[0].AsEnumerable().
                    Select(x => new DeviceLinkProfile()
                    {
                        DeviceLinkProfileId = DataReader.ReadInt(x, "DLPID"),
                        DeviceLinkProfileName = DataReader.ReadString(x, "DLPName"),
                        //Profile = DsReader.ReadString(x, "Profile"),
                        IsUse = DataReader.ReadBoolean(x, "IsUse"),                        
                        DestProfile =
                            new DestinationProfile
                            {
                                DestinationProfileId = DataReader.ReadInt(x, "DestinationProfileID"),
                                DestinationProfileName = DataReader.ReadString(x, "DestinationProfileName")
                            },
                        SrcProfile =
                            new SourceProfile
                            {
                                SourceProfileId = DataReader.ReadInt(x, "SourceProfileID"),                                
                            }
                            
                    }).ToList();
            }
            return objDeviceLinkProfileList;
        }

        public int InsertDeviceLinkProfile(DeviceLinkProfile objDeviceLinkProfile)
        {
            var objProjectInPeramater = new List<Parameters>();
            var objProjectOutPeramater = new List<Parameters>();

            objProjectInPeramater.Add(new Parameters() { ParameterName = "@SourceProfileID", Size = 0, Type = DbType.Int16, Value = objDeviceLinkProfile.SrcProfile.SourceProfileId });
            objProjectInPeramater.Add(new Parameters() { ParameterName = "@DestinationProfileID", Size = 0, Type = DbType.Int16, Value = objDeviceLinkProfile.DestProfile.DestinationProfileId });
            objProjectInPeramater.Add(new Parameters() { ParameterName = "@DLPName", Size = 0, Type = DbType.String, Value = objDeviceLinkProfile.DeviceLinkProfileName });
            objProjectInPeramater.Add(new Parameters() { ParameterName = "@IsUse", Size = 0, Type = DbType.Boolean, Value = objDeviceLinkProfile.IsUse });

            objProjectOutPeramater.Add(new Parameters() { ParameterName = "@DLPID", Size = 4, Type = DbType.Int16 });


            DataAccessFactory.Instance.ExecuteProcedureWithTransaction(
                new List<Action>()
                    {
                        () =>
                            {
                                objProjectOutPeramater =
                                    DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspInsertDLPProfile]",
                                                                                objProjectInPeramater,
                                                                                objProjectOutPeramater);
                                objDeviceLinkProfile.DeviceLinkProfileId =
                                    Convert.ToInt16(
                                        objProjectOutPeramater.Single(x => x.ParameterName == "@DLPID").Value);
                            }
                    });

            return objDeviceLinkProfile.DeviceLinkProfileId;
        }

        public void UpdateDestinationProfile(int deviceLinkProfileId, byte[] profile)
        {
            DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspUpdateDLPProfile]", new object[] { deviceLinkProfileId, profile });
        }

        public DeviceLinkProfile GetDeviceLinkProfileListByDLPID(int DLPID)
        {
            var objDeviceLinkProfileList = new DeviceLinkProfile();
            //========== Get data from data access=================//
            var dsDeviceLinkProfileList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetDLPListByDLPID]", new object[] { DLPID});

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
                            new SourceProfile { SourceProfileId = DataReader.ReadInt(x, "SourceProfileID"), SourceProfileName = DataReader.ReadString(x, "SourceProfileName") },
                        DestProfile =
                            new DestinationProfile
                            {
                                DestinationProfileId = DataReader.ReadInt(x, "DestinationProfileID"),
                                DestinationProfileName = DataReader.ReadString(x, "DestinationProfileName")
                            }

                    }).FirstOrDefault();
            }
            return objDeviceLinkProfileList;
        }
        #endregion
    }

}
