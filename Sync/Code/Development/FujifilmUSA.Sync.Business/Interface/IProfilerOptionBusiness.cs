using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IProfilerOptionBusiness
    {
        Dictionary<int, string> GetProfilerOption { get; set; }
    }
}
