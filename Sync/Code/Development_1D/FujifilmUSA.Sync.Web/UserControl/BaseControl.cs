using System;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Common;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using System.Configuration;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public abstract class BaseControl : System.Web.UI.UserControl, IViewBase
    {
        public bool IsErrorOccured { get; set; }

        protected override void OnInit(EventArgs e)
        {
            //Start: check current status of user for secure connection (To redirect all http towards https automatically) 
            if (ConfigurationManager.AppSettings["IsHttpsUrl"].ToString() == "true") // stage and production code which supports only https
            {
                if (!Request.IsLocal && !Request.IsSecureConnection)
                {
                    // redirect the visitor to HTTPS - SSL connection  
                    Response.Redirect(Request.Url.AbsoluteUri.Replace("http://", "https://"));
                }
            }
            //End:

            IsErrorOccured = false;
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (SessionManager.Instance.CurrentUser != null)
            {
                CurrentUserId = SessionManager.Instance.CurrentUser.UserId;
            }
            base.OnLoad(e);
        }

        public string GetResourceString(string key, bool IsGlobal = false)
        {
            if (!IsGlobal)                                   
                return (GetLocalResourceObject(key) as string);
            return key;
        }

        public string getCurrentCultureLanguage()
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            return currentCulture.Name.ToString();
        }

        #region IViewBase Members

        public int CurrentUserId { get; set; }

        public virtual void ErrorOccured(object sender, Entity.ErrorArgs e)
        {
            IsErrorOccured = true;
            var ctrlStatusBar = FindControl("fujiStatusbar");
            if (ctrlStatusBar != null)
            {
                if (ctrlStatusBar.GetType().BaseType == typeof(Statusbar))
                {
                    ((Statusbar)ctrlStatusBar).DisplayMessage(e.ErrorMessage, false);
                }
                else if (ctrlStatusBar.GetType().BaseType == typeof(AdminStatusbar))
                {
                    ((AdminStatusbar)ctrlStatusBar).DisplayMessage(e.ErrorMessage, false);
                }
            }
        }

        #endregion

        protected void ExceptionHandling(Action<object,EventArgs> actionList,object sender, EventArgs e)
        {
            try
            {
                actionList(sender, e);
            }
            catch (Exception ex)
            {
                var presenterGlobal = new FujifilmUSA.Sync.Presenter.PresenterGlobal();
                presenterGlobal.LogApplicationError(ex);

                this.ErrorOccured(this, new Entity.ErrorArgs(ex.Message));
            }
        }       
    }
}