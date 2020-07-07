using CPProfilerService.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CPProfilerService.Models.Entity.Enums;

namespace CPProfilerService.Models.Request
{
    
    public class CreateSourceProfileRequest
    {
        
        public double[] PCSData { get; set; }       
        public double[] DCSData { get; set; }        
        public List<Parameter> ProfileOptions { get; set; }      
        public PCSDataType PCSDataType { get; set; }       
        public IccProfile SourceProfile { get; set; }
    }
}