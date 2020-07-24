using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class MToolVersionUpdateBusinessImpl : IMToolVersionListBusiness
    {
        #region  IMToolVersionListBusiness Members
        public int InsertMToolUpdateVersion(MToolVersionUpdateList mToolVersion)
        {
            return 0;
        }

        public int UpdateMToolUpdateVersion(MToolVersionUpdateList mToolVersion)
        {
            return 0;
        }

        public List<MToolVersionUpdateList> GetMToolUpdateVersionList()
        {
            return new List<MToolVersionUpdateList>();
        }

        public MToolVersionUpdateList GetMToolVersionSelect(short oSPlatformDetailsID)
        {
            return new MToolVersionUpdateList();
        }

        public Dictionary<int, string> GetMToolOSPlatformList()
        {
            return new Dictionary<int, string>();
        }

        public bool DeleteMToolVersion(int oSPlatformDetailsID)
        {
            return false;
        }


        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
