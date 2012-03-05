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
    public class SourceProfileBusinessImpl : ISourceProfileBusiness
    {
        #region ISourceProfileBusiness Members

        public SourceProfile GetSourceProfile(int sourceProfileId)
        {
            var sourceProfile = new SourceProfile();

            Common.ImplementBusinessLogic(() =>
            {
                var dsSourceProfile = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetSourceProfileBySourceProfileID]", new object[] { sourceProfileId });

                if (dsSourceProfile != null && dsSourceProfile.Tables.Count > 0 && dsSourceProfile.Tables[0].Rows.Count > 0)
                {
                    sourceProfile = dsSourceProfile.Tables[0].
                        AsEnumerable().
                        Select(x => new SourceProfile()
                        {
                            ProfileId = DataReader.ReadInt(x, "SourceProfileID"),
                            ProfileName = DataReader.ReadString(x, "SourceProfileName"),
                            IsUse = DataReader.ReadBoolean(x, "IsUse"),
                            Data = new ProfileData()
                            {
                                Profile = DataReader.ReadByteArray(x, "Profile")
                            }
                        }).FirstOrDefault();
                }
            }, this, ErrorOccured);

            return sourceProfile;
        }

        public List<SourceProfile> GetSourceProfiles()
        {
            var sourceProfileList = new List<SourceProfile>();
           
            Common.ImplementBusinessLogic(() =>
            {
                var dsSourceProfile = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetSourceProfiles]");

                if (dsSourceProfile != null && dsSourceProfile.Tables.Count > 0 && dsSourceProfile.Tables[0].Rows.Count > 0)
                {
                    sourceProfileList = dsSourceProfile.Tables[0].AsEnumerable().Select(x => new SourceProfile()
                    {
                        ProfileId = DataReader.ReadInt(x, "SourceProfileID"),
                        ProfileName = DataReader.ReadString(x, "SourceProfileName"),
                        IsUse = DataReader.ReadBoolean(x, "IsUse")
                    }).ToList();
                }
            }, this, ErrorOccured);
            
            return sourceProfileList;
        }

        public int InsertSourceProfile(SourceProfile objSourceProfile)
        {
            Common.ImplementBusinessLogic(() =>
            {
                var objProjectInPeramater = new List<Parameters>();
                var objProjectOutPeramater = new List<Parameters>();

                objProjectInPeramater.Add(new Parameters() { ParameterName = "@SourceProfileName", Size = 0, Type = DbType.String, Value = objSourceProfile.ProfileName });
                objProjectInPeramater.Add(new Parameters() { ParameterName = "@IsUse", Size = 0, Type = DbType.Boolean, Value = objSourceProfile.IsUse });
                objProjectOutPeramater.Add(new Parameters() { ParameterName = "@SourceProfileID", Size = 4, Type = DbType.Int16 });


                DataAccessFactory.Instance.ExecuteProcedureWithTransaction(
                    new List<Action>()
                    {
                        () =>
                            {
                                objProjectOutPeramater =
                                    DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspInsertSourceProfile]",
                                                                                objProjectInPeramater,
                                                                                objProjectOutPeramater);
                                objSourceProfile.ProfileId =
                                    Convert.ToInt16(
                                        objProjectOutPeramater.Single(x => x.ParameterName == "@SourceProfileID").Value);
                            }
                    });
            }, this, ErrorOccured);
            return objSourceProfile.ProfileId;
        }
        public bool CreateSourceProfile(string measurementIds, int sourceId, int profilerOptionId)
        {
            Common.ImplementBusinessLogic(() =>
            {
                var sourceProfile = new SourceProfile();
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
                var optionDetails = profilerOptionDetail.GetProfilerOptionDetails(profilerOptionId, ProfileType.SourceProfile);

                using (var client = new ProfilerClient())
                {
                    request.Options = new List<Parameter>();
                    optionDetails.ForEach(
                        option => request.Options.Add(new Parameter() { Name = option.Key, Value = option.Value }));
                    var response = client.CreateSourceProfile(request);
                    sourceProfile.Data.Profile = response.Profile.ProfileData;
                }

                if (sourceProfile.Data.Profile != null)
                {
                    DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspUpdateSourceProfile]", new object[] { sourceId, sourceProfile.Data.Profile });

                    //Cleanup of Masurement

                    DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspDeleteMeasurementLink]", new object[] { sourceId, ProfileType.SourceProfile });
                }

            }, this, ErrorOccured);
            return true;

        }

        public int DeleteSourceProfile(int sourceProfileID)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(() =>
            {
                returnValue = DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspDeleteSourceProfile]",
                                               new object[] { sourceProfileID });
            }, this, ErrorOccured);
            return returnValue;
        }


        #endregion
     
        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
