using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FujifilmUSA.Sync.Web.ColorOrganizer;
using System.Web.Security;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web
{
    /// <summary>
    /// Summary description for COProcessRequest
    /// </summary>
    public class COProcessRequest : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            if (HttpContext.Current != null)
            {
                var processor = new ColorOrganizerRequestProcessor();                
                if (!processor.ProcessRequest())
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Bad Request!Please contact your administrator.");
                }
            }
            else
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Error Occured!Please contact your administrator.");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}