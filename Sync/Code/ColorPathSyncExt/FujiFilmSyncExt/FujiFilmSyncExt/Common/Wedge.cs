using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujiFilmSyncExt.Common
{
    public class Wedge
    {
        public Wedge()
        {
            listPatch = new List<Patch>();
        }
        public List<Patch> listPatch { get; set; }
    }
}
