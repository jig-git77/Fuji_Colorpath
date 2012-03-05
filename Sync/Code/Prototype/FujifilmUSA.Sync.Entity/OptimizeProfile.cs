using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class OptimizeProfile : IBaseProfile
    {
        public OptimizeProfile()
        {
            Data = new ProfileData();
        }
        #region IBaseProfile Members

        public int ProfileId { get; set; }
        
        public string ProfileName { get; set; }
        
        public ProfileData Data { get; set; }
        
        public int SyncDeviceId { get; set; }
        
        public bool IsUse { get; set; }
        
        #endregion
    }
}
