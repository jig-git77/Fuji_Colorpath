using System;
using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Web.UserControl
{
    public abstract class BaseControl : System.Web.UI.UserControl, IViewError
    {
        public bool IsErrorOccured { get; set; }

        protected override void OnInit(EventArgs e)
        {
            IsErrorOccured = false;
            base.OnInit(e);
        }

        #region IViewError Members

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