using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class SourceProfileBusinessMock : ISourceProfileBusiness
    {
        #region ISourceProfileBusiness Members

        public Dictionary<int, string> RegionList()
        {
            return new Dictionary<int, string>();
        }

        public int InsertSourceProfiel(SourceProfile SourceProfile)
        {
            return 0;
        }

        public int UpdateSourceProfile(SourceProfile SourceProfile)
        {
            return 0;
        }

        public bool DeleteSourceProfile(int SourceProfileId)
        {
            return true;
        }

        public List<SourceProfile> GetSourceProfiles()
        {
           return new List<SourceProfile>();
        }

        public SourceProfile GetSourceProfile(int iSourceProfileID)
        {
            return new SourceProfile();
        }

        #endregion


        
    }
}
