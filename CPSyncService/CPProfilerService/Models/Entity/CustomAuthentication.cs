using CPProfilerService.Business;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CPProfilerService.Models.Entity
{
    public class CustomAuthentication : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string id = ConfigurationManager.AppSettings["IsAuthorizationAllowed"];
            if (id == "1")
            {
                string EncrytpValue = string.Empty;
                string DecryptValue = string.Empty;
                string EncyptedTocken = FetchFromHeader(actionContext);
                if (!string.IsNullOrWhiteSpace(EncyptedTocken))
                {
                    bool IsValid = CryptoEngine.Decrypt(EncyptedTocken);
                    if (!IsValid)
                    {
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        return;
                    }
                }
                else
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    return;
                }
            }
            base.OnAuthorization(actionContext);
        }

        private string FetchFromHeader(HttpActionContext actionContext)
        {
            string requestToken = null;

            var authRequest = actionContext.Request.Headers.Authorization;
            if (authRequest != null && !string.IsNullOrEmpty(authRequest.Scheme) && authRequest.Scheme == "Basic")
            {
                requestToken = authRequest.Parameter;
            }

            return requestToken;
        }


    }
}