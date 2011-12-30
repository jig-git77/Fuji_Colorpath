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
            return sourceProfile;
        }

        public List<SourceProfile> GetSourceProfiles()
        {
            var sourceProfileList = new List<SourceProfile>();
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
            return sourceProfileList;
        }

        public int InsertSourceProfile(SourceProfile objSourceProfile)
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

            return objSourceProfile.ProfileId;
        }
        #endregion
    }
}
