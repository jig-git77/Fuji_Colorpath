using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;


namespace FujifilmUSA.Sync.Business.Mock
{
    public class ProfilerOptionDetailMock :IProfilerOptionDetailBusiness
    {
        #region IProfilerOptionDetailBusiness Member
        public List<ProfilerOptionDetail> GetProfilerOptionDetails(int profileroptionId, ProfileType type)
        {
            return new List<ProfilerOptionDetail>();
        }
        #endregion
    }
}
