using FujifilmUSA.Sync.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FujiFilmUSA.Sync.Service.Controllers
{
    public class BaseController : ApiController
    {
        public int CurrentUserId { get; set; }
        public void ErrorOccured(object sender, ErrorArgs e)
        {

        }
    }
}
