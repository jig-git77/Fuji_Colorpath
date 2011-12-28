using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class ProfileroptionMock : IProfilerOptionBusiness
    {
        public Dictionary<int, string> GetProfilerOption()
        {
            return new Dictionary<int, string>();
        }
    }
}
