using CPProfilerService.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPProfilerService.Models.Response
{
   
    public class CreateISOAlignmentCurveResponse : BaseResponse
    {
        public CreateISOAlignmentCurveResponse()
        {
            Cyan = new double[101];
            Magenta = new double[101];
            Yellow = new double[101];
            Key = new double[101];
            DeviceLinkProfile = new IccProfile();
        }
        public IccProfile DeviceLinkProfile { get; set; }       
        public double[] Cyan { get; set; }      
        public double[] Magenta { get; set; }        
        public double[] Yellow { get; set; }        
        public double[] Key { get; set; }
    }
}