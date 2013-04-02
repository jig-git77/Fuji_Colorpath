using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity.CPSync1D
{
    public class PrintStandardTVI
    {
        public PatchType PatchType { get; set; }
        public decimal TVI25 { get; set; }
        public decimal TVI50 { get; set; }
        public decimal TVI75 { get; set; }
    }
}
