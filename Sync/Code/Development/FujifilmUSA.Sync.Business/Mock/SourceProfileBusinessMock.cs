using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class SourceProfileBusinessMock : ISourceProfileBusiness
    {
        #region ISourceProfileBusiness Members

        public Dictionary<int, string> GetSourceProfileList()
        {
            return new Dictionary<int, string>();
        }

        public SourceProfile GetSourceProfile(int sourceProfileId)
        {
            return new SourceProfile();
        }

        public List<SourceProfile> GetSourceProfiles()
        {
            return new List<SourceProfile>();
        }

        #endregion
    }
}
