using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace FujiFilmUSA.Sync.Service.Controllers
{
    public class CommonController : BaseController
    {
        #region Variable Declarations

        #endregion

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
        }

        [HttpGet]
        public double getMinutesSinceUnixEpoch()
        {
            double minutesSinceUnixEpoch = 0;            
            try
            {
                DateTime date = DateTime.Now;
                DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                TimeSpan diff = date.ToUniversalTime() - origin;
                minutesSinceUnixEpoch = Math.Floor(diff.TotalMinutes);
                minutesSinceUnixEpoch = 26516460; // Hard coded time for right now, published on apistage
            }
            catch (Exception ex)
            {
                              
            }
            return minutesSinceUnixEpoch;
        }
    }
}
