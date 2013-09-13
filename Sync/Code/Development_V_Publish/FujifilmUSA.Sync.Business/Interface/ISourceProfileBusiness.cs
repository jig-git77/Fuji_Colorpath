using FujifilmUSA.Sync.Business.Messages;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface ISourceProfileBusiness : ITaskBase
    {
        Dictionary<int, string> GetRegionList(int currentUserId);
        int InsertSourceProfile(SourceProfile sourceProfile);
        int UpdateSourceProfile(SourceProfile sourceProfile);
        bool DeleteSourceProfile(int sourceProfileId);
        List<SourceProfile> GetSourceProfile(int sourceProfileId,int currentUserId);
        byte[] GetIccProfileBySourceProfile(int sourceProfileId);
    }
}
