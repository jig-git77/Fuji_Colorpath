using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Presenter;

namespace FujifilmUSA.Sync.Web
{
    public class ApplicationConfigurationManager : IViewApplicationConfiguration
    {
        private static readonly object _lock = new object();
        public static ApplicationConfigurationManager _instance;
        private readonly PresenterApplicationConfiguration presenterApplicationConfiguration;

        private ApplicationConfigurationManager()
        {
            presenterApplicationConfiguration = new PresenterApplicationConfiguration(this);
            presenterApplicationConfiguration.FillConfiguration();
        }

        public static ApplicationConfigurationManager Instance()
        {
            lock(_lock)
            {
                return _instance ?? (_instance = new ApplicationConfigurationManager());
            }
        }

        #region IViewApplicationConfiguration Members

        public bool IsSingleUserLoginEnabled { get; set; }

        #endregion

        #region IViewBase Members

        public int CurrentUserId { get; set; }
        
        public void ErrorOccured(object sender, Entity.ErrorArgs e)
        {
            //Do Nothing...
        }

        #endregion
    }
}