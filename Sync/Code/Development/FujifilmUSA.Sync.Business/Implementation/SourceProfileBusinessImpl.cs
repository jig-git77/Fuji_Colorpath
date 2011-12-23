using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class SourceProfileBusinessImpl : ISourceProfileBusiness
    {
        #region ISourceProfileBusiness Members

        public SourceProfile GetSourceProfileBySourceProfileId(int sourceProfileId)
        {
            var sourceProfile = new SourceProfile();
            var dsSourceProfile = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetSourceProfileBySourceProfileID]", new object[] { sourceProfileId });

            if (dsSourceProfile != null && dsSourceProfile.Tables.Count > 0 && dsSourceProfile.Tables[0].Rows.Count > 0)
            {
                sourceProfile = dsSourceProfile.Tables[0].AsEnumerable().Select(x => new SourceProfile()
                {
                    SourceProfileId = DataReader.ReadInt(x, "SourceProfileID"),
                    SourceProfileName = DataReader.ReadString(x, "SourceProfileName"),
                    IsUse = DataReader.ReadBoolean(x, "IsUse"),
                    Profile = DataReader.ReadByteArray(x, "Profile")
                }).FirstOrDefault();
            }
            return sourceProfile;
        }

        #endregion
    }
}
