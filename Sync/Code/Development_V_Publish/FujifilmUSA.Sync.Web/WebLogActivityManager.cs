using System;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Presenter;

namespace FujifilmUSA.Sync.Web
{
    public class WebLogActivityManager
    {
        public static void LogActivity(LogActivityType logActivityType, object[] parameters, string ipAddress)
        {
            var logManager = new WebLogActivity(logActivityType, parameters, ipAddress);
            logManager.LogActivity();
        }
    }

    public class WebLogActivity : IViewWebLogActivity
    {
        private readonly PresenterWebLogActivity _presenterWebLogActivityManager;

        public WebLogActivity(LogActivityType logActivityType, object[] parameters, string ipAddress)
        {
            _presenterWebLogActivityManager = new PresenterWebLogActivity(this);
            ((IViewWebLogActivity)this).ActivityType = logActivityType;
            ((IViewWebLogActivity)this).Paramaters = parameters;
            ((IViewWebLogActivity)this).IpAddress = ipAddress;
        }

        public void LogActivity()
        {
            _presenterWebLogActivityManager.LogActivity();
        }

        #region IViewWebLogActivityManager Members

        LogActivityType IViewWebLogActivity.ActivityType { get; set; }

        object[] IViewWebLogActivity.Paramaters { get; set; }

        string IViewWebLogActivity.IpAddress { get; set; }

        #endregion

        #region IViewBase Members

        int IViewBase.CurrentUserId { get; set; }

        void IViewBase.ErrorOccured(object sender, ErrorArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}