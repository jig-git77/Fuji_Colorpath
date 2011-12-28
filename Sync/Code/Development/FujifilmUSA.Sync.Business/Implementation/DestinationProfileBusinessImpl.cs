using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;
using System;
using FujifilmUSA.Sync.Business.ProfilerService;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class DestinationProfileBusinessImpl : IDestinationProfileBusiness
    {
        #region IDestinationProfileBusiness Members
        public List<DestinationProfile> GetDestinationProfiles(int syncDeviceId)
        {
            var destinationProfileList = new List<Entity.DestinationProfile>();
            var dsDestinationProfile = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetDestinationProfileList]", new object[] { syncDeviceId });

            if (dsDestinationProfile != null && dsDestinationProfile.Tables.Count > 0 && dsDestinationProfile.Tables[0].Rows.Count > 0)
            {
                destinationProfileList = dsDestinationProfile.Tables[0].AsEnumerable().Select(x => new Entity.DestinationProfile()
                {
                    ProfileId = DataReader.ReadInt(x, "DestinationProfileID"),
                    ProfileName = DataReader.ReadString(x, "DestinationProfileName"),
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

            objProjectInPeramater.Add(new Parameters() { ParameterName = "@DestinationProfileName", Size = 0, Type = DbType.String, Value = objDestinationProfile.ProfileName });
            objProjectInPeramater.Add(new Parameters() { ParameterName = "@SyncDeviceID", Size = 0, Type = DbType.Int16, Value = objDestinationProfile.SyncDeviceId });
            objProjectInPeramater.Add(new Parameters() { ParameterName = "@IsUse", Size = 0, Type = DbType.Boolean, Value = objDestinationProfile.IsUse });
            objProjectInPeramater.Add(new Parameters() { ParameterName = "@ProfilerOptionID", Size = 0, Type = DbType.Int16, Value = objDestinationProfile.ProfileOptionID });
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
                                objDestinationProfile.ProfileId =
                                    Convert.ToInt16(
                                        objProjectOutPeramater.Single(x => x.ParameterName == "@DestinationProfileID").Value);
                            }
                    });

            return objDestinationProfile.ProfileId;
        }

        public void UpdateDestinationProfile(int destinationProfileId, byte[] profile)
        {
            DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspUpdateDestinationProfile]", new object[] { destinationProfileId, profile });
        }

        public bool CreateDestinationProfile(string measurementIds, int destinationId,int profilerOptionId)
        {
            var destinationProfile = new DestinationProfile();
            var request = new CreateSourceProfileRequest()
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

            //Add options and call profile service to generate destination dlp
            var profilerOptionDetail = ((IProfilerOptionDetailBusiness)new BusinessFactory<IProfilerOptionDetailBusiness>().CreateObject());
            var optionDetails = profilerOptionDetail.GetProfilerOptionDetails(profilerOptionId, ProfileType.DestinationProfile);

            using (var client = new ProfilerClient())
            {
                request.Options = new List<Parameter>();
                optionDetails.ForEach(
                    option => request.Options.Add(new Parameter() {Name = option.Key, Value = option.Value}));
                var response = client.CreateSourceProfile(request);
                destinationProfile.Data.Profile = response.Profile.ProfileData;
            }

            if (destinationProfile.Data.Profile != null)
            {
                DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspUpdateDestinationProfile]", new object[] { destinationId, destinationProfile.Data.Profile });
            }

            return true;
        }

        public DestinationProfile GetDestinationProfile(int destinationProfileId)
        {
            var destinationProfile = new DestinationProfile();
            var dsDestinationProfile = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetDestinationProfileByDestinationProfileID]", new object[] { destinationProfileId });

            if (dsDestinationProfile != null && dsDestinationProfile.Tables.Count > 0 && dsDestinationProfile.Tables[0].Rows.Count > 0)
            {
                destinationProfile = dsDestinationProfile.Tables[0].
                    AsEnumerable().
                    Select(x => new DestinationProfile()
                                    {
                                        ProfileId = DataReader.ReadInt(x, "DestinationProfileID"),
                                        ProfileName = DataReader.ReadString(x, "DestinationProfileName"),
                                        IsUse = DataReader.ReadBoolean(x, "IsUse"),
                                        SyncDeviceId = DataReader.ReadInt(x, "SyncDeviceID"),
                                        Data = new ProfileData() { Profile = DataReader.ReadByteArray(x, "Profile") }
                                    }).FirstOrDefault();
            }
            return destinationProfile;
        }

        public int DeleteDestinationProfile(int destinationProfileId)
        {
            return DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspDeleteDestinationProfile]",
                                                           new object[] { destinationProfileId });
        }

        #endregion
    }
}

