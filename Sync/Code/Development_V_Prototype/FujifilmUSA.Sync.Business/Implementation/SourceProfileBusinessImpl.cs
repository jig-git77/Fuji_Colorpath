using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.DataAccess;
using System.Data;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.CustomExceptions;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class SourceProfileBusinessImpl : ISourceProfileBusiness
    {
        #region ISourceProfileBusiness Members

        public Dictionary<int, string> GetRegionList(int currentUserId)
        {
            var dstRegionList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsRegion = DataAccessFactory.Instance.GetDsResult("[CP].[GetRegion]", new object[] { currentUserId });
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
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetRegionList"));
            return dstRegionList;
        }

        public int InsertSourceProfile(SourceProfile sourceProfile)
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
                                        sourceProfile.RegionId,
                                        sourceProfile.CompanyId,
                                        sourceProfile.ProfileName,
                                        sourceProfile.ProfileDesc,
                                        sourceProfile.Profile,
                                        sourceProfile.IsUsed
                                    });
                    if (returnValue == -1)
                    {
                        throw new DuplicateException() { UserFriendlyMessage = "Duplicate_InsertSourceProfile" };
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertSourceProfile"));
            return returnValue;
        }

        public int UpdateSourceProfile(SourceProfile sourceProfile)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[UpdateSourceProfile]",
                            new object[]
                                    {
                                        sourceProfile.SourceProfileId,
                                        sourceProfile.RegionId,
                                        sourceProfile.CompanyId,
                                        sourceProfile.ProfileName,
                                        sourceProfile.ProfileDesc,
                                        sourceProfile.Profile,
                                        sourceProfile.IsUsed
                                    });
                    if (returnValue == -1)
                    {
                        throw new DuplicateException() { UserFriendlyMessage = "Duplicate_UpdateSourceProfile" };
                    }
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

        public List<SourceProfile> GetSourceProfile(int sourceProfileId, int currentUserId)
        {
            var sourceProfiles = new List<SourceProfile>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsSourceProfile = DataAccessFactory.Instance.GetDsResult("[CP].[GetSourceProfile]", new object[] { sourceProfileId, currentUserId });
                    if (dsSourceProfile != null && dsSourceProfile.Tables.Count > 0 && dsSourceProfile.Tables[0].Rows.Count > 0)
                    {
                        sourceProfiles = dsSourceProfile.Tables[0].AsEnumerable().
                            Select(x => new SourceProfile()
                                            {
                                                SourceProfileId = DataReader.ReadShort(x, "SourceProfileID"),
                                                RegionId = DataReader.ReadShort(x, "RegionId"),
                                                CompanyId = DataReader.ReadShort(x, "CompanyId"),
                                                ProfileName = DataReader.ReadString(x, "ProfileName"),
                                                ProfileDesc = DataReader.ReadString(x, "ProfileDesc"),
                                                Profile = DataReader.ReadByteArray(x, "Profile"),
                                                IsUsed = DataReader.ReadBoolean(x, "IsUsed"),
                                                Company = new Company() { CompanyId = DataReader.ReadInt(x, "CompanyID"), CompanyName = DataReader.ReadString(x, "CompanyName") },
                                                Region = new Region() { RegionId = DataReader.ReadShort(x, "RegionId"), RegionName = DataReader.ReadString(x, "Region") }
                                            }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetSourceProfile"));
            return sourceProfiles;
        }

        public byte[] GetIccProfileBySourceProfile(int sourceProfileId)
        {
            byte[] iccProfile = null;

            Common.ImplementBusinessLogic(
            () =>
            {
                var dsIccProfile = DataAccessFactory.Instance.GetDsResult("[CP].[GetICCSourceProfile]", new object[] { sourceProfileId });
                iccProfile = dsIccProfile.Tables[0].AsEnumerable().Select(x => DataReader.ReadByteArray(x, "Profile")).FirstOrDefault();
            },
            new BusinessHandlerRequest(this, ErrorOccured, "Error_GetIccProfileBySourceProfile"));

            return iccProfile;
        }


        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
