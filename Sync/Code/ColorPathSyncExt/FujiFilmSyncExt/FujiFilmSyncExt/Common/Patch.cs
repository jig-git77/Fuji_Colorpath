using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujiFilmSyncExt.Common
{
    public class Patch
    {
        public int PatchID { get; set; }
        public string ColorName { get; set; }

        public double cmyk_C { get; set; }
        public double cmyk_M { get; set; }
        public double cmyk_Y { get; set; }
        public double cmyk_K { get; set; }
        public double spot1 { get; set; }
        public double cie_L { get; set; }
        public double cie_a { get; set; }
        public double cie_b { get; set; }
    }
}
