using CPProfilerService.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CPProfilerService.Models.Entity.Enums;

namespace CPProfilerService.Models.Request
{
    public class ExtractProfileValuesRequest
    {
       
        public ProfileExtractType ExtractType { get; set; }       
        public IccProfile Profile { get; set; }       
        public List<Parameter> ProfileOptions { get; set; }       
        public double[] C_X { get; set; }       
        public double[] M_Y { get; set; }        
        public double[] Y_Z { get; set; }       
        public double[] K { get; set; }
    }
}