using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FujifilmUSA.Sync.DataAccess
{
    public class Parameters
    {
        public string ParameterName { get; set; }
        public DbType Type { get; set; }
        public int Size { get; set; }
        public object Value { get; set; }
    }
}
