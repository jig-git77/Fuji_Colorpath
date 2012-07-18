using System;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Common;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public abstract class BaseControl : System.Web.UI.UserControl, IViewBase
    {
        public bool IsErrorOccured { get; set; }

        protected override void OnInit(EventArgs e)
        {
            IsErrorOccured = false;
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
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
    }
}