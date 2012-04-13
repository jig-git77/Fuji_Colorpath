using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class SourceProfileBusinessMock : ISourceProfileBusiness
    {
        #region ISourceProfileBusiness Members

        public Dictionary<int, string> GetRegionList(int currentUserId)
        {
            return new Dictionary<int, string>();
        }

        public int InsertSourceProfile(SourceProfile sourceProfile)
        {
            return 0;
        }

        public int UpdateSourceProfile(SourceProfile sourceProfile)
        {
            return 0;
        }

        public bool DeleteSourceProfile(int sourceProfileId)
        {
            return true;
        }

        public List<SourceProfile> GetSourceProfile(int sourceProfileId,int currentUserId)
        {
           return new List<SourceProfile>();
        }
         public byte[] GetIccProfileBySourceProfile(int sourceProfileId)
         {
             return null;
         }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
