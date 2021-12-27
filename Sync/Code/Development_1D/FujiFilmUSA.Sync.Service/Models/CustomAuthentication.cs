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
using FujifilmUSA.Sync.Common;

namespace FujiFilmUSA.Sync.Service.Models
{
    public class CustomAuthentication : AuthorizationFilterAttribute, IViewLogin
    {
        #region "Declaration"
        private PresenterLogin _presenterlogin;
        #endregion

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            _presenterlogin = new PresenterLogin(this);
            EncyptedTocken = FetchFromHeader(actionContext);
            if (EncyptedTocken != null)
            {
                if (!_presenterlogin.ValidateUser())
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                return;
            }
            base.OnAuthorization(actionContext);
        }

        private string FetchFromHeader(HttpActionContext actionContext)
        {
            string requestToken = null;

            var authRequest = actionContext.Request.Headers.Authorization;
            if (authRequest != null && !string.IsNullOrEmpty(authRequest.Scheme) && authRequest.Scheme == "Basic")
                requestToken = authRequest.Parameter;

            return requestToken;
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

        public RevoriaUser RevoriaUser { get; set; }
        #endregion

        #region IViewBase Members

        public int CurrentUserId { get; set; }

        public virtual void ErrorOccured(object sender, FujifilmUSA.Sync.Entity.ErrorArgs e)
        {
        }

        #endregion
    }
}