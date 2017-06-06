using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FujiFilmUSA.Sync.Service.Models
{
    public class ICCFileParams
    {        
        public ICCFileParams()
        {
            wedge = new Wedge();
            property = new PDFProperty();
        }
        public Wedge wedge { get; set; }
        public PDFProperty property { get; set; }
    }
}