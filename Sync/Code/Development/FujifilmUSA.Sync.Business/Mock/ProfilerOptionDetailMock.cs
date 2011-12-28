using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class ProfilerOptionDetailMock
    {
        public List<ProfilerOptionDetail> GetProfilerOptionDetailByProfilerID(int ProfileroptionID)
        {
            return new List<ProfilerOptionDetail>();
        }
    }
}
