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

        public Dictionary<int, string> RegionList()
        {
            var dstRegionList = new Dictionary<int, string>();
            var dsRegion = DataAccessFactory.Instance.GetDsResult("[CP].[uspGetRegion]");
            if (dsRegion != null && dsRegion.Tables.Count > 0)
            {
                dstRegionList = (dsRegion.Tables[0].AsEnumerable().Select(
                    region => new
                    {
                        Value = DataReader.ReadInt(region, "RegionID"),
                        Text = DataReader.ReadString(region, "Region")
                    })).ToDictionary(region => region.Value, region => region.Text);
            }
            return dstRegionList;
        }

        public int InsertSourceProfiel(SourceProfile SourceProfile)
        {
            int returnValue = 0;
            returnValue = DataAccessFactory.Instance.ExecuteProcedure("[CP].[uspInsertSourceProfile]", new object[]{
                                                                                                                SourceProfile.RegionId,
                                                                                                                SourceProfile.CompanyId,
                                                                                                                SourceProfile.ProfileName,
                                                                                                                SourceProfile.ProfileDesc,
                                                                                                                SourceProfile.Profile,
                                                                                                                SourceProfile.IsUsed
                                                                                                });
            return returnValue;
        }

        public int UpdateSourceProfile(SourceProfile SourceProfile)
        {
            int returnValue = 0;
            returnValue = DataAccessFactory.Instance.ExecuteProcedure("[CP].[uspUpdateourceProfile]", new object[]{
                                                                                                                SourceProfile.SourceProfileId,
                                                                                                                SourceProfile.RegionId,
                                                                                                                SourceProfile.CompanyId,
                                                                                                                SourceProfile.ProfileName,
                                                                                                                SourceProfile.ProfileDesc,
                                                                                                                SourceProfile.Profile,
                                                                                                                SourceProfile.IsUsed
                                                                                                 });
            return returnValue;
        }

        public bool DeleteSourceProfile(int SourceProfileId)
        {
            DataAccessFactory.Instance.ExecuteProcedure("[CP].[uspDeleteSourceProfile]", new object[]{
             SourceProfileId
            });
            return true;
        }

        public List<SourceProfile> GetSourceProfiles()
        {
            var SourceProfiles = new List<SourceProfile>();
            var dsSourceProfile = DataAccessFactory.Instance.GetDsResult("[CP].[uspGetSourceProfiles]");
            if (dsSourceProfile != null && dsSourceProfile.Tables.Count > 0 && dsSourceProfile.Tables[0].Rows.Count > 0)
            {
                SourceProfiles = dsSourceProfile.Tables[0].AsEnumerable().
                    Select(x => new SourceProfile()
                    {
                        SourceProfileId = DataReader.ReadShort(x, "SourceProfileID"),
                        ProfileName = DataReader.ReadString(x, "ProfileDesc"),
                        ProfileDesc= DataReader.ReadString(x, "ProfileName"),
                        Company = new Company() {CompanyId = DataReader.ReadInt(x, "CompanyID"), CompanyName = DataReader.ReadString(x, "CompanyName") },
                        Region = new Region() { RegionId =(short)DataReader.ReadInt(x, "RegionId"), RegionName = DataReader.ReadString(x, "Region") },
                        IsUsed = DataReader.ReadBoolean(x, "IsUsed")
                    }).ToList();
            }
            return SourceProfiles;
        }

        public SourceProfile GetSourceProfile(int iSourceProfileID)
        {
            var SourceProfiles = new SourceProfile();
            var dsSourceProfile = DataAccessFactory.Instance.GetDsResult("[CP].[uspGetSourceProfile]", new object[] { iSourceProfileID });
            if (dsSourceProfile != null && dsSourceProfile.Tables.Count > 0 && dsSourceProfile.Tables[0].Rows.Count > 0)
            {
                SourceProfiles = dsSourceProfile.Tables[0].AsEnumerable().
                    Select(x => new SourceProfile()
                    {
                        SourceProfileId = DataReader.ReadShort(x, "SourceProfileID"),
                        ProfileName = DataReader.ReadString(x, "ProfileDesc"),
                        ProfileDesc = DataReader.ReadString(x, "ProfileName"),
                        Company = new Company() { CompanyId = DataReader.ReadInt(x, "CompanyID"), CompanyName = DataReader.ReadString(x, "CompanyName") },
                        Region = new Region() { RegionId = (short)DataReader.ReadInt(x, "RegionId"), RegionName = DataReader.ReadString(x, "Region") },
                        IsUsed = DataReader.ReadBoolean(x, "IsUsed")

                    }).SingleOrDefault();
            }
            return SourceProfiles;
        }
        #endregion


       
    }
}
