using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FujiFilmUSA.Sync.Service.Models
{
    //public class CustomAuthentication: ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(HttpActionContext actionContext)
    //    {
    //        //actionContext.ControllerContext.Request.Headers
    //        string AuthToken = "";
    //        if(actionContext.ControllerContext.Request.Headers.Contains("Auth-Key"))
    //        {
    //            AuthToken = actionContext.ControllerContext.Request.Headers.GetValues("Auth-Key").First();
    //        }

    //        base.OnActionExecuting(actionContext);

    //    }
    //}

    public class CustomAuthentication : AuthorizationFilterAttribute, IViewLogin
    {
        private PresenterLogin _presenterlogin;

        public CustomAuthentication()
        {
            _presenterlogin = new PresenterLogin(this);
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.ControllerContext.Request.Headers.Contains("Auth-Key"))
            {
                this.EncyptedTocken = actionContext.ControllerContext.Request.Headers.GetValues("Auth-Key").First();
                int authResult = _presenterlogin.ValidateToken();
                if (authResult == -1)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    return;
                }
                else if (authResult == -2)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.PreconditionFailed);
                    return;
                }
                else
                {
                    base.OnAuthorization(actionContext);
                }
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }
            base.OnAuthorization(actionContext);
        }

        #region IViewTaskeroUser Members
        public string UserName { get; set; }
        public string Password { get; set; }
        public User CurrentUser { get; set; }

        public int UserId { get; set; }
        public string OSName { get; set; }
        public string BrowserName { get; set; }
        public string EntryDate { get; set; }

        public string EncyptedTocken { get; set; }
        public UserReferenceChart UserReferenceChart { get; set; }
        public ReferenceChartApp UserReferenceChartApp { get; set; }
        public string DeviceSerialNo { get; set; }
        public short InstrumentTypeID { get; set; }
        #endregion

        #region IViewBase Members

        public int CurrentUserId { get; set; }

        public virtual void ErrorOccured(object sender, FujifilmUSA.Sync.Entity.ErrorArgs e)
        {
            
        }

        #endregion

    }


}