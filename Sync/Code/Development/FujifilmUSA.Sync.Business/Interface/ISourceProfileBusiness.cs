using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface ISourceProfileBusiness : ITaskBase
    {
        SourceProfile GetSourceProfile(int sourceProfileId);
        List<SourceProfile> GetSourceProfiles();
        int InsertSourceProfile(SourceProfile objSourceProfile);
        bool CreateSourceProfile(string measurementIds, int destinationId, int profilerOptionId);
    }
}

