using FujifilmUSA.Sync.Business.Messages;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface ISourceProfileBusiness : ITaskBase
    {
        Dictionary<int, string> GetRegionList();
        int InsertSourceProfile(SourceProfile sourceProfile);
        int UpdateSourceProfile(SourceProfile sourceProfile);
        bool DeleteSourceProfile(int sourceProfileId);
        List<SourceProfile> GetSourceProfile(int sourceProfileId);
        byte[] GetIccProfileBySourceProfile(int sourceProfileId);
    }
}
