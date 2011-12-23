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
        public bool CreateDeviceLinkProfile(DeviceLinkProfile objDeviceLinkProfile)
        {
            objDeviceLinkProfile.DestProfile =
                (new DestinationProfileBusinessImpl()).GetDestinationProfileByDestinationProfileId(objDeviceLinkProfile.DestProfile.DestinationProfileId);

            objDeviceLinkProfile.SrcProfile =
                (new SourceProfileBusinessImpl()).GetSourceProfileBySourceProfileId(objDeviceLinkProfile.SrcProfile.SourceProfileId);

            var request = new CreateDeviceLinkProfileRequest();

            //Add options and call profile service to generate optimize dlp
            using (var client = new ProfilerClient())
            {
                request.Options = new List<Parameter>
                                      {
                                          new Parameter() {Name = "ProfileType", Value = "OptimizeDeviceLink"},
                                          new Parameter() {Name = "ProfileSize", Value = "2"},
                                          new Parameter() {Name = "Rendering", Value = "3"},
                                          new Parameter() {Name = "DLBypassBlack", Value = "100"},
                                          new Parameter() {Name = "DLExceptions", Value = "129"},
                                          new Parameter() {Name = "ProfileFormat", Value = "2"},
                                          new Parameter() {Name = "DLOptimizationLimit", Value = "0"},
                                          new Parameter() {Name = "Flags", Value = "0"},
                                          new Parameter() {Name = "Attributes0", Value = "0"},
                                          new Parameter() {Name = "Attributes1", Value = "0"},
                                          new Parameter() {Name = "SaveProfilerInfo", Value = "1"}
                                      };
                request.DestinationProfile = new IccProfile()
                                                 {
                                                     ProfileData = objDeviceLinkProfile.DestProfile.Profile
                                                 };
                request.SourceProfile = new IccProfile()
                                            {
                                                ProfileData = objDeviceLinkProfile.SrcProfile.Profile
                                            };
                var response = client.CreateDeviceLinkProfile(request);
                objDeviceLinkProfile.Profile = response.DeviceLinkProfile.ProfileData;


            }
            if (objDeviceLinkProfile.Profile != null)
            {
                DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspInsertDLPProfile]",
                                                            new object[]
                                                                {
                                                                    objDeviceLinkProfile.DeviceLinkProfileId, objDeviceLinkProfile.SrcProfile.SourceProfileId,
                                                                    objDeviceLinkProfile.DestProfile.DestinationProfileId, objDeviceLinkProfile.DeviceLinkProfileName,
                                                                    objDeviceLinkProfile.IsUse, objDeviceLinkProfile.Profile
                                                                });
            }

            return true;
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
