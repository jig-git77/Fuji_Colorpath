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

        
        public int InsertPrintStandardType(Entity.CPSync1D.PrintStandardType printStandardType)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[InsertPrintStandardType]",
                            new object[]
                                    {
                                        printStandardType.PrintStandard,
                                        printStandardType.IsSync1DType,
                                        printStandardType.SourceProfile.SourceProfileId                                       
                                    });
                    if (returnValue == -1)
                    {
                        throw new DuplicateException() { UserFriendlyMessage = "Duplicate_InsertPrintStandardType" };
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertPrintStandardType"));
            return returnValue;
        }

        public int UpdatePrintStandardType(Entity.CPSync1D.PrintStandardType printStandardType)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[UpdatePrintStandardType]",
                            new object[]
                                    {
                                        printStandardType.PrintStandardTypeID,
                                        printStandardType.PrintStandard,
                                        printStandardType.IsSync1DType,
                                        printStandardType.SourceProfile.SourceProfileId                                     
                                    });
                    if (returnValue == -1)
                    {
                        throw new DuplicateException() { UserFriendlyMessage = "Duplicate_UpdatePrintStandardType" };
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdatePrintStandardType"));
            return returnValue;
        }

        public bool DeletePrintStandardType(int printStandardTypeId)
        {
            Common.ImplementBusinessLogic(
                () => DataAccessFactory.
                          Instance.
                          ExecuteProcedure(
                              "[CP].[DeletePrintStandardType]",
                              new object[]
                                  {
                                      printStandardTypeId
                                  }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeletePrintStandardType"));
            return true;
        }

        public List<Entity.CPSync1D.PrintStandardType> GetPrintStandardType(int printStandardTypeId, int currentUserId)
        {
            var printStandardTypes = new List<Entity.CPSync1D.PrintStandardType>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsPrintStandardType = DataAccessFactory.Instance.GetDsResult("[CP].[GetPrintStandardType]", new object[] { printStandardTypeId, currentUserId });
                    if (dsPrintStandardType != null && dsPrintStandardType.Tables.Count > 0 && dsPrintStandardType.Tables[0].Rows.Count > 0)
                    {
                        printStandardTypes = dsPrintStandardType.Tables[0].AsEnumerable().
                            Select(x => new Entity.CPSync1D.PrintStandardType()
                            {
                                PrintStandardTypeID = DataReader.ReadShort(x, "PrintStandardTypeID"),
                                PrintStandard = DataReader.ReadString(x, "PrintStandard"),
                                IsSync1DType = DataReader.ReadBoolean(x, "IsSync1DType"),
                                SourceProfile = new SourceProfile() { SourceProfileId = DataReader.ReadShort(x, "SourceProfileID"), ProfileName = DataReader.ReadString(x, "ProfileName") }                               
                            }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetPrintStandardType"));
            return printStandardTypes;
        }

        public Dictionary<int, string> GetSourceProfileList(int currentUserId)
        {
            var dstSourceProfileList = new Dictionary<int, string>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsSourceProfile = DataAccessFactory.Instance.GetDsResult("[CP].[GetSourceProfile]", new object[] { 0, currentUserId });
                    if (dsSourceProfile != null && dsSourceProfile.Tables.Count > 0)
                    {
                        dstSourceProfileList = (dsSourceProfile.Tables[0].AsEnumerable().Select(
                            region => new
                            {
                                Value = DataReader.ReadInt(region, "SourceProfileId"),
                                Text = DataReader.ReadString(region, "ProfileName")
                            })).ToDictionary(region => region.Value, region => region.Text);
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetSourceProfileList"));
            return dstSourceProfileList;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
