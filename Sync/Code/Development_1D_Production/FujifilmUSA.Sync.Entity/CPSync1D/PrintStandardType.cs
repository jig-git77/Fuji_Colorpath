using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity.CPSync1D
{
    [Serializable]
    public class PrintStandardType
    {
        public short PrintStandardTypeID { get; set; }
        public string PrintStandard { get; set; }
        public bool IsSync1DType { get; set; }
        public SourceProfile SourceProfile { get; set; }
    }
}
