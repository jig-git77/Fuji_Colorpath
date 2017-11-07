using FujifilmUSA.Sync.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace FujiFilmUSA.Sync.Service.Controllers
{
    public class BaseController : ApiController
    {
        public int CurrentUserId { get; set; }
        public void ErrorOccured(object sender, ErrorArgs e)
        {

        }

        public void DetermineCulture()
        {
            // Somehow determine the best-suited culture for the specified request,
            CultureInfo culture = CultureInfo.GetCultureInfo(Request.Headers.AcceptLanguage.First().ToString());
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
