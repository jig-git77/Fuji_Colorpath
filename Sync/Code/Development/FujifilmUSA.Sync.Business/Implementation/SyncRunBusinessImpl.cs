using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business.ProfilerService;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
using System.Data;
using System.Linq;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class SyncRunBusinessImpl : ISyncRunBusiness
    {
        #region ISyncRunBusiness Members

        public DeviceLinkProfile OptimizeDeviceLinkProfile(string measurementIds, int syncRunId, int projectId)
        {
            var optimizeDlp = new DeviceLinkProfile();
            var request = new OptimizeDeviceLinkProfileRequest()
                              {
                                  DCSData = new List<double>(),
                                  PCSData = new List<double>(),
                              };

            //Load pcs and dcs Data
            var dsPcsDcsData = DataAccessFactory.Instance.GetDsResult("CPSync4D.uspGetPCSData", new object[] { measurementIds });
            if (dsPcsDcsData.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in dsPcsDcsData.Tables[0].Rows)
                {
                    request.PCSData.Add(DataReader.ReadDouble(row, "CIEXYZ_X"));
                    request.PCSData.Add(DataReader.ReadDouble(row, "CIEXYZ_Y"));
                    request.PCSData.Add(DataReader.ReadDouble(row, "CIEXYZ_Z"));

                    request.DCSData.Add(DataReader.ReadDouble(row, "CMYK_C"));
                    request.DCSData.Add(DataReader.ReadDouble(row, "CMYK_M"));
                    request.DCSData.Add(DataReader.ReadDouble(row, "CMYK_Y"));
                    request.DCSData.Add(DataReader.ReadDouble(row, "CMYK_K"));
                }
            }

            var masterSyncRunId = GetMasterSyncRunId(syncRunId);
            if (masterSyncRunId == 0) return optimizeDlp;

            //Get Existing DLP profile
            var dlpProfile = DataAccessFactory.Instance.GetScalar("[CPSync4D].[uspGetICCProfile]", new object[] { masterSyncRunId });
            if (dlpProfile == System.DBNull.Value) return optimizeDlp;

            request.DeviceLinkProfile = new IccProfile()
                                            {
                                                ProfileData = (byte[])dlpProfile
                                            };

            //Get Source and Destination DLP Profile
            var dsSrcDestProfiles = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetProjectSrcAndDestProfiles]", new object[] { projectId });
            if (dsSrcDestProfiles.Tables[0].Rows.Count != 1) return optimizeDlp;

            request.SourceProfile = new IccProfile()
                                        {
                                            ProfileData = DataReader.ReadByteArray(dsSrcDestProfiles.Tables[0].Rows[0], "SourceProfile")
                                        };

            request.DestinationProfile = new IccProfile()
                                             {
                                                 ProfileData = DataReader.ReadByteArray(dsSrcDestProfiles.Tables[0].Rows[0], "DestinationProfile")
                                             };

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
                var response = client.OptimizeDeviceLinkProfile(request);
                optimizeDlp.Data.Profile = response.OptimizedProfile.ProfileData;
            }

            //Store optimize dlp to current sync run
            DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspUpdateICCProfile]", new object[] { syncRunId, optimizeDlp.Data.Profile});

            return optimizeDlp;
        }

        public int GetMasterSyncRunId(int syncRunId)
        {
            var dsMasterSyncRunData = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetMasterSyncRun]", new object[] { syncRunId });
            var masterSyncRunId = 0;
            if (dsMasterSyncRunData.Tables[0].Rows.Count == 1)
            {
                masterSyncRunId = DataReader.ReadInt(dsMasterSyncRunData.Tables[0].Rows[0], "MasterSyncRunID");
            }
            return masterSyncRunId;
        }

        public byte[] GetIccProfileBySyncRunId(int syncRunId)
        {
            byte[] iccProfile = null;

            var dsSyncRunIccProfile = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetICCProfile]", new object[] { syncRunId });

            iccProfile = dsSyncRunIccProfile.Tables[0].AsEnumerable().Select(x => DataReader.ReadByteArray(x, "ICCProfile")).FirstOrDefault();


            return iccProfile;
        }

        #endregion

        
    }
}