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

        int InsertPrintStandardType(Entity.CPSync1D.PrintStandardType printStandardType);
        int UpdatePrintStandardType(Entity.CPSync1D.PrintStandardType printStandardType);
        bool DeletePrintStandardType(int printStandardTypeId);
        List<Entity.CPSync1D.PrintStandardType> GetPrintStandardType(int printStandardTypeId, int currentUserId);
        Dictionary<int, string> GetSourceProfileList(int currentUserId);
        Dictionary<int, string> GetPrintStandardTypeList(int currentUserId);
        List<Entity.CPSync1D.PrintStandardTVI> GetPrintStandardTVI(int printStandardTypeId);
        List<Entity.CPSync1D.ProjectTargetLAB> GetPrintStandardLAB(int printStandardTypeId, int ProjectTypeId);
        List<Entity.CPSync1D.ProjectTargetDensity> GetPrintStandardDensity(int printStandardTypeId);
        int InsertLookupTVILABDensity(int printStandardTypeId, List<Entity.CPSync1D.ProjectTargetLAB> PrintStandardLAB, List<Entity.CPSync1D.PrintStandardTVI> PrintStandardTVI, List<Entity.CPSync1D.ProjectTargetDensity> PrintStandardDensity);
    }
}
