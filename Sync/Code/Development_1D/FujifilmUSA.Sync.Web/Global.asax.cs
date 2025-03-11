using FujifilmUSA.Sync.Presenter;
using System;
using System.Configuration;
using System.Threading;
using System.Web;

namespace FujifilmUSA.Sync.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session != null)
            {
                int sessionnTimeOutInMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["SessionTimeOutInMinutes"]);
                DateTime started = (DateTime)HttpContext.Current.Session["SessionStarted"];
                DateTime current = DateTime.Now;
                double totalMinutesIdle = current.Subtract(started).TotalMinutes;
                if (totalMinutesIdle >= sessionnTimeOutInMinutes)
                {
                    Session.Abandon();
                    HttpContext.Current.Session.Abandon();
                    HttpContext.Current.Session.Contents.Abandon();
                }
                else
                {
                    Session["SessionStarted"] = DateTime.Now;
                    Session.Timeout = sessionnTimeOutInMinutes;
                    HttpContext.Current.Session.Timeout = sessionnTimeOutInMinutes;
                    HttpContext.Current.Session.Contents.Timeout = sessionnTimeOutInMinutes;
                }
            }
        }
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            int sessionnTimeOutInMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["SessionTimeOutInMinutes"]);
            Session["SessionStarted"] = DateTime.Now;
            Session.Timeout = sessionnTimeOutInMinutes;
            HttpContext.Current.Session.Timeout = sessionnTimeOutInMinutes;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

            var ex = Server.GetLastError();
            if (ex is ThreadAbortException)
                return;

            var presenterGlobal = new PresenterGlobal();
            presenterGlobal.LogApplicationError(ex.GetBaseException());
            Response.RedirectPermanent("ApplicationError.aspx");
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        public Exception ApplicationException { get; set; }

    }
}