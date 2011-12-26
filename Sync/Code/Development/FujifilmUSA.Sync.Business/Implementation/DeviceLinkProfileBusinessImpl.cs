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
        public List<DeviceLinkProfile> GetDeviceLinkProfiles(int sourceProfileId, int destinationProfileId)
        {
            var objDeviceLinkProfileList = new List<DeviceLinkProfile>();
            //========== Get data from data access=================//
            var dsDeviceLinkProfileList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetDLPList]", new object[] { sourceProfileId, destinationProfileId });

            if (dsDeviceLinkProfileList != null && dsDeviceLinkProfileList.Tables.Count > 0 && dsDeviceLinkProfileList.Tables[0].Rows.Count > 0)
            {
                objDeviceLinkProfileList = dsDeviceLinkProfileList.Tables[0].AsEnumerable().
                    Select(x => new DeviceLinkProfile()
                                    {
                                        ProfileId = DataReader.ReadInt(x, "DLPID"),
                                        ProfileName = DataReader.ReadString(x, "DLPName"),
                                        IsUse = DataReader.ReadBoolean(x, "IsUse"),
                                        SrcProfile =
                                            new SourceProfile { ProfileId = DataReader.ReadInt(x, "SourceProfileID"), ProfileName = DataReader.ReadString(x, "SourceProfileName") },
                                        DestProfile =
                                            new DestinationProfile
                                                {
                                                    ProfileId = DataReader.ReadInt(x, "DestinationProfileID"),
                                                    ProfileName = DataReader.ReadString(x, "DestinationProfileName")
                                                }

                                    }).ToList();
            }
            return objDeviceLinkProfileList;
        }
        
        public List<DeviceLinkProfile> GetDeviceLinkProfilesOnSyncDeviceId(int syncDeviceId)
        {
            var objDeviceLinkProfileList = new List<DeviceLinkProfile>();
            //========== Get data from data access=================//
            var dsDeviceLinkProfileList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetDLPListBySyncDeviceId]", new object[] { syncDeviceId });

            if (dsDeviceLinkProfileList != null && dsDeviceLinkProfileList.Tables.Count > 0 && dsDeviceLinkProfileList.Tables[0].Rows.Count > 0)
            {
                objDeviceLinkProfileList = dsDeviceLinkProfileList.Tables[0].AsEnumerable().
                    Select(x => new DeviceLinkProfile()
                    {
                        ProfileId = DataReader.ReadInt(x, "DLPID"),
                        ProfileName = DataReader.ReadString(x, "DLPName"),
                        IsUse = DataReader.ReadBoolean(x, "IsUse"),
                        DestProfile =
                            new DestinationProfile
                            {
                                ProfileId = DataReader.ReadInt(x, "DestinationProfileID"),
                                ProfileName = DataReader.ReadString(x, "DestinationProfileName")
                            },
                        SrcProfile =
                            new SourceProfile
                            {
                                ProfileId = DataReader.ReadInt(x, "SourceProfileID"),
                            }

                    }).ToList();
            }
            return objDeviceLinkProfileList;
        }
        
        public bool CreateDeviceLinkProfile(DeviceLinkProfile deviceLinkProfile)
        {
            var destinationTask = (IDestinationProfileBusiness)new BusinessFactory<IDestinationProfileBusiness>().CreateObject();
            deviceLinkProfile.DestProfile =
                destinationTask.GetDestinationProfile(deviceLinkProfile.DestProfile.ProfileId);

            var sourceTask = (ISourceProfileBusiness)new BusinessFactory<ISourceProfileBusiness>().CreateObject();
            deviceLinkProfile.SrcProfile =
                sourceTask.GetSourceProfile(deviceLinkProfile.SrcProfile.ProfileId);

            var request = new CreateDeviceLinkProfileRequest();

            //Add options and call profile service to generate optimize dlp
            using (var client = new ProfilerClient())
            {
                request.Options = new List<Parameter>
                                      {
                                          new Parameter() {Name = "ProfileType", Value = "DeviceLink"},
                                          new Parameter() {Name = "ProfileSize", Value = "2"},
                                          new Parameter() {Name = "Rendering", Value = "14"},
                                          new Parameter() {Name = "PerceptualCompression2", Value = "16"},
                                          new Parameter() {Name = "PerceptualCompressionMergeFactor", Value = "75"},
                                          new Parameter() {Name = "DLType", Value = "6"},
                                          new Parameter() {Name = "DLExceptions", Value = "65536"},
                                          new Parameter() {Name = "DLOptimizeExceptions", Value = "524289"},
                                          new Parameter() {Name = "DLExceptionRange", Value = "15"},
                                          new Parameter() {Name = "DLOptimizeInkRange", Value = "0.2"},
                                          new Parameter() {Name = "Description", Value = "7900/Xerox 1000 DLP Setting v1.0"},
                                          new Parameter() {Name = "BlackGeneration", Value = "GCR2Smooth"},
                                          new Parameter() {Name = "InkMax", Value = "79 79 77 95 BalanceCMY"},
                                          new Parameter() {Name = "InkTotal", Value = "330"},
                                          new Parameter() {Name = "BlackStart", Value = "10"},
                                          new Parameter() {Name = "BlackWidth", Value = "100"}
                                      };
                request.DestinationProfile = new IccProfile()
                                                 {
                                                     ProfileData = deviceLinkProfile.DestProfile.Data.Profile
                                                 };
                request.SourceProfile = new IccProfile()
                                            {
                                                ProfileData = deviceLinkProfile.SrcProfile.Data.Profile
                                            };
                var response = client.CreateDeviceLinkProfile(request);
                deviceLinkProfile.Data.Profile = response.DeviceLinkProfile.ProfileData;
            }
            if (deviceLinkProfile.Data.Profile != null)
            {
                DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspInsertDLPProfile]",
                                                            new object[]
                                                                {
                                                                    deviceLinkProfile.ProfileId,
                                                                    deviceLinkProfile.SrcProfile.ProfileId,
                                                                    deviceLinkProfile.DestProfile.ProfileId,
                                                                    deviceLinkProfile.ProfileName,
                                                                    deviceLinkProfile.IsUse,
                                                                    deviceLinkProfile.Data.Profile
                                                                });
            }

            return true;
        }

        public DeviceLinkProfile GetDeviceLinkProfile(int dlpId)
        {
            var objDeviceLinkProfileList = new DeviceLinkProfile();
            //========== Get data from data access=================//
            var dsDeviceLinkProfileList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetDLPListByDLPID]", new object[] {dlpId});

            if (dsDeviceLinkProfileList != null && dsDeviceLinkProfileList.Tables.Count > 0 && dsDeviceLinkProfileList.Tables[0].Rows.Count > 0)
            {
                objDeviceLinkProfileList = dsDeviceLinkProfileList.Tables[0].AsEnumerable().
                    Select(x => new DeviceLinkProfile()
                                    {
                                        ProfileId = DataReader.ReadInt(x, "DLPID"),
                                        ProfileName = DataReader.ReadString(x, "DLPName"),
                                        IsUse = DataReader.ReadBoolean(x, "IsUse"),
                                        SrcProfile =
                                            new SourceProfile {ProfileId = DataReader.ReadInt(x, "SourceProfileID"), ProfileName = DataReader.ReadString(x, "SourceProfileName")},
                                        DestProfile =
                                            new DestinationProfile
                                                {
                                                    ProfileId = DataReader.ReadInt(x, "DestinationProfileID"),
                                                    ProfileName = DataReader.ReadString(x, "DestinationProfileName")
                                                }

                                    }).FirstOrDefault();
            }
            return objDeviceLinkProfileList;
        }

        public int DeleteDeviceLinkProfile(int dlpId)
        {
            return DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspDeleteDeviceLinkProfile]",
                                                           new object[] { dlpId });
        }

        #endregion
    }

}
