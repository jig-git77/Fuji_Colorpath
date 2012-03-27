using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.DataAccess;
using System.Data;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class SourceProfileBusinessImpl : ISourceProfileBusiness
    {
        #region ISourceProfileBusiness Members

        public Dictionary<int, string> GetRegionList()
        {
            var dstRegionList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                    {
                        var dsRegion = DataAccessFactory.Instance.GetDsResult("[CP].[GetRegion]");
                        if (dsRegion != null && dsRegion.Tables.Count > 0)
                        {
                            dstRegionList = (dsRegion.Tables[0].AsEnumerable().Select(
                                region => new
                                              {
                                                  Value = DataReader.ReadInt(region, "RegionID"),
                                                  Text = DataReader.ReadString(region, "Region")
                                              })).ToDictionary(region => region.Value, region => region.Text);
                        }
                    },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetRegions"));
            return dstRegionList;
        }

        public int InsertSourceProfile(SourceProfile SourceProfile)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                    {
                        returnValue = DataAccessFactory.
                            Instance.
                            ExecuteProcedure(
                                "[CP].[InsertSourceProfile]",
                                new object[]
                                    {
                                        SourceProfile.RegionId,
                                        SourceProfile.CompanyId,
                                        SourceProfile.ProfileName,
                                        SourceProfile.ProfileDesc,
                                        SourceProfile.Profile,
                                        SourceProfile.IsUsed
                                    });
                    },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertSourceProfile"));
            return returnValue;
        }

        public int UpdateSourceProfile(SourceProfile SourceProfile)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                    {
                        returnValue = DataAccessFactory.
                            Instance.
                            ExecuteProcedure(
                                "[CP].[UpdateourceProfile]",
                                new object[]
                                    {
                                        SourceProfile.SourceProfileId,
                                        SourceProfile.RegionId,
                                        SourceProfile.CompanyId,
                                        SourceProfile.ProfileName,
                                        SourceProfile.ProfileDesc,
                                        SourceProfile.Profile,
                                        SourceProfile.IsUsed
                                    });
                    },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateSourceProfile"));
            return returnValue;
        }

        public bool DeleteSourceProfile(int sourceProfileId)
        {
            Common.ImplementBusinessLogic(
                () => DataAccessFactory.
                          Instance.
                          ExecuteProcedure(
                              "[CP].[DeleteSourceProfile]",
                              new object[]
                                  {
                                      sourceProfileId
                                  }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteSourceProfile"));
            return true;
        }

        public List<SourceProfile> GetSourceProfiles()
        {
            var sourceProfiles = new List<SourceProfile>();
            Common.ImplementBusinessLogic(
                () =>
                    {
                        var dsSourceProfile = DataAccessFactory.Instance.GetDsResult("[CP].[GetSourceProfiles]");
                        if (dsSourceProfile != null && dsSourceProfile.Tables.Count > 0 && dsSourceProfile.Tables[0].Rows.Count > 0)
                        {
                            sourceProfiles = dsSourceProfile.Tables[0].AsEnumerable().
                                Select(x => new SourceProfile()
                                                {
                                                    SourceProfileId = DataReader.ReadShort(x, "SourceProfileID"),
                                                    ProfileName = DataReader.ReadString(x, "ProfileDesc"),
                                                    ProfileDesc = DataReader.ReadString(x, "ProfileName"),
                                                    Company = new Company() {CompanyId = DataReader.ReadInt(x, "CompanyID"), CompanyName = DataReader.ReadString(x, "CompanyName")},
                                                    Region = new Region() {RegionId = (short) DataReader.ReadInt(x, "RegionId"), RegionName = DataReader.ReadString(x, "Region")},
                                                    IsUsed = DataReader.ReadBoolean(x, "IsUsed")
                                                }).ToList();
                        }
                    },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetSourceProfiles"));
            return sourceProfiles;
        }

        public SourceProfile GetSourceProfile(int sourceProfileId)
        {
            var sourceProfiles = new SourceProfile();
            Common.ImplementBusinessLogic(
                () =>
                    {
                        var dsSourceProfile = DataAccessFactory.Instance.GetDsResult("[CP].[GetSourceProfile]", new object[] {sourceProfileId});
                        if (dsSourceProfile != null && dsSourceProfile.Tables.Count > 0 && dsSourceProfile.Tables[0].Rows.Count > 0)
                        {
                            sourceProfiles = dsSourceProfile.Tables[0].AsEnumerable().
                                Select(x => new SourceProfile()
                                                {
                                                    SourceProfileId = DataReader.ReadShort(x, "SourceProfileID"),
                                                    ProfileName = DataReader.ReadString(x, "ProfileDesc"),
                                                    ProfileDesc = DataReader.ReadString(x, "ProfileName"),
                                                    Company = new Company() {CompanyId = DataReader.ReadInt(x, "CompanyID"), CompanyName = DataReader.ReadString(x, "CompanyName")},
                                                    Region = new Region() {RegionId = (short) DataReader.ReadInt(x, "RegionId"), RegionName = DataReader.ReadString(x, "Region")},
                                                    IsUsed = DataReader.ReadBoolean(x, "IsUsed")

                                                }).SingleOrDefault();
                        }
                    },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetSourceProfileInformation"));
            return sourceProfiles;
        }
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
