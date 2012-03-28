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
            var ctrlStatusBar = FindControl("ucStatusbar1") as Statusbar;
            if (ctrlStatusBar != null)
            {
                ctrlStatusBar.DisplayMessage(e.ErrorMessage, false);
            }
        }

        #endregion
    }
}