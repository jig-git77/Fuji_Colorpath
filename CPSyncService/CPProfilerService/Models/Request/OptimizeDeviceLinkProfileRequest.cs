using CPProfilerService.Models.Entity;
using System.Collections.Generic;
using static CPProfilerService.Models.Entity.Enums;

namespace CPProfilerService.Models.Request
{
    public class OptimizeDeviceLinkProfileRequest
    {
       
        public IccProfile SourceProfile { get; set; }       
        public IccProfile DestinationProfile { get; set; }      
        public IccProfile DeviceLinkProfile { get; set; }       
        public List<Parameter> ProfileOptions { get; set; }    
        public double[] PCSData { get; set; }        
        public double[] DCSData { get; set; }      
        public PCSDataType PCSDataType { get; set; }
    }
}