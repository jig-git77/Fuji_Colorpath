using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujiFilmUSA.Sync.Service.Models
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