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


         public int InsertPrintStandardType(Entity.CPSync1D.PrintStandardType printStandardType)
         {
             return 0;
         }

         public int UpdatePrintStandardType(Entity.CPSync1D.PrintStandardType printStandardType)
         {
             return 0;
         }

         public bool DeletePrintStandardType(int printStandardTypeId)
         {
             return true;
         }

         public List<Entity.CPSync1D.PrintStandardType> GetPrintStandardType(int printStandardTypeId, int currentUserId)
         {
             return new List<Entity.CPSync1D.PrintStandardType>();
         }

         public Dictionary<int, string> GetSourceProfileList(int currentUserId)
         {
             return new Dictionary<int, string>();
         }
         public Dictionary<int, string> GetPrintStandardTypeList(int currentUserId)
         {
             return new Dictionary<int, string>();
         }
         public List<Entity.CPSync1D.PrintStandardTVI> GetPrintStandardTVI(int printStandardTypeId)
         {
             return new List<Entity.CPSync1D.PrintStandardTVI>();
         }
         public List<Entity.CPSync1D.ProjectTargetLAB> GetPrintStandardLAB(int printStandardTypeId, int ProjectTypeId)
         {
             return new List<Entity.CPSync1D.ProjectTargetLAB>();
         }
         public List<Entity.CPSync1D.ProjectTargetDensity> GetPrintStandardDensity(int printStandardTypeId)
         {
             return new List<Entity.CPSync1D.ProjectTargetDensity>();
         }
         public int InsertLookupTVILABDensity(int printStandardTypeId, List<Entity.CPSync1D.ProjectTargetLAB> PrintStandardLAB, List<Entity.CPSync1D.PrintStandardTVI> PrintStandardTVI, List<Entity.CPSync1D.ProjectTargetDensity> PrintStandardDensity)
         {
             return 0;
         }
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
