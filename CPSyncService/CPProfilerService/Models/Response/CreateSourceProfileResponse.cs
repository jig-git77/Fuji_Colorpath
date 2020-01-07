using CPProfilerService.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CPProfilerService.Models.Response
{
    public class CreateSourceProfileResponse:BaseResponse
    {
        public IccProfile Profile { get; set; }
    }
}