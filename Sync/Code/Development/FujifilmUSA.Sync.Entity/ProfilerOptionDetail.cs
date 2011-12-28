using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class ProfilerOptionDetail
    {
       public int ProfilerOptionDetailID { get; set;}
       public int ProfilerOptionID { get; set;}
       public string Key { get; set;}
       public string Value { get; set;}
       public int ProfilerType { get; set; }

    }
}
