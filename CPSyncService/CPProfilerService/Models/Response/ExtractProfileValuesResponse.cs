using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPProfilerService.Models.Response
{
   
    public class ExtractProfileValuesResponse : BaseResponse
    {
        public double[] C_X { get; set; }        
        public double[] M_Y { get; set; }        
        public double[] Y_Z { get; set; }        
        public double[] K { get; set; }
    }
}