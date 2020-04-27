using CPProfilerService.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CPProfilerService.Models.Entity.Enums;

namespace CPProfilerService.Models.Request
{
    public class CreateG7AlignmentCurveRequest
    {
       
        public double[] PCSData { get; set; }      
        public double[] DCSData { get; set; }       
        public bool UseDLP { get; set; }       
        public IccProfile DeviceLinkProfile { get; set; }      
        public List<Parameter> ProfileOptions { get; set; }   
        public PCSDataType PCSDataType { get; set; }
    }
}